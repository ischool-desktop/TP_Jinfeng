using System;
using System.Collections.Generic;
using System.Text;
using Aspose.Cells;
using System.IO;
using System.Xml;
using System.ComponentModel;
using SmartSchool.Common;
using SmartSchool.Customization.PlugIn.Report;
using SmartSchool.Customization.Data.StudentExtension;
using SmartSchool.Customization.PlugIn;
using SmartSchool.Customization.Data;

namespace �w�榨�Z�պ��
{
    class SemesterMoralScoreCalc
    {
        private BackgroundWorker _BGWSemesterMoralScoresCalculate;
        ButtonAdapter button;

        public SemesterMoralScoreCalc()
        {
            button = new ButtonAdapter();
            button.Text = "�w���{�`��]�s��^";
            button.Path = "�ۭq����";
            button.OnClick += new EventHandler(button_OnClick);
            ClassReport.AddReport(button);
        }

        private void Completed(string inputReportName, Workbook inputWorkbook)
        {
            string reportName = inputReportName;

            string path = Path.Combine(System.Windows.Forms.Application.StartupPath, "Reports");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            path = Path.Combine(path, reportName + ".xls");

            Workbook wb = inputWorkbook;

            if (File.Exists(path))
            {
                int i = 1;
                while (true)
                {
                    string newPath = Path.GetDirectoryName(path) + "\\" + Path.GetFileNameWithoutExtension(path) + (i++) + Path.GetExtension(path);
                    if (!File.Exists(newPath))
                    {
                        path = newPath;
                        break;
                    }
                }
            }

            try
            {
                wb.Save(path, FileFormatType.Excel2003);
                System.Diagnostics.Process.Start(path);
            }
            catch
            {
                System.Windows.Forms.SaveFileDialog sd = new System.Windows.Forms.SaveFileDialog();
                sd.Title = "�t�s�s��";
                sd.FileName = reportName + ".xls";
                sd.Filter = "Excel�ɮ� (*.xls)|*.xls|�Ҧ��ɮ� (*.*)|*.*";
                if (sd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    try
                    {
                        wb.Save(sd.FileName, FileFormatType.Excel2003);

                    }
                    catch
                    {
                        MsgBox.Show("���w���|�L�k�s���C", "�إ��ɮץ���", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        return;
                    }
                }
            }
        }

        void button_OnClick(object sender, EventArgs e)
        {
            int schoolyear = 0;
            int semester = 0;
            bool over100 = false;
            int sizeIndex = 0;
            Dictionary<string, List<string>> type = null;

            SemesterMoralScoreCalcForm form = new SemesterMoralScoreCalcForm();
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                schoolyear = form.SchoolYear;
                semester = form.Semester;
                over100 = form.AllowMoralScoreOver100;
                sizeIndex = form.PaperSize;
                type = form.UserDefinedType;
            }
            else
                return;

            _BGWSemesterMoralScoresCalculate = new BackgroundWorker();
            _BGWSemesterMoralScoresCalculate.WorkerReportsProgress = true;
            _BGWSemesterMoralScoresCalculate.DoWork += new DoWorkEventHandler(_BGWSemesterMoralScoresCalculate_DoWork);
            _BGWSemesterMoralScoresCalculate.ProgressChanged += new ProgressChangedEventHandler(_BGWSemesterMoralScoresCalculate_ProgressChanged);
            _BGWSemesterMoralScoresCalculate.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_BGWSemesterMoralScoresCalculate_RunWorkerCompleted);
            _BGWSemesterMoralScoresCalculate.RunWorkerAsync(new object[] { schoolyear, semester, over100, sizeIndex, type });
        }

        void _BGWSemesterMoralScoresCalculate_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            button.SetBarMessage("�w���{�`��]�s��^���ͧ���");
            Completed("�w���{�`��]�s��^", (Workbook)e.Result);
        }

        void _BGWSemesterMoralScoresCalculate_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            button.SetBarMessage("�w���{�`��]�s��^���ͤ�...", e.ProgressPercentage);
        }

        void _BGWSemesterMoralScoresCalculate_DoWork(object sender, DoWorkEventArgs e)
        {
            object[] args = (object[])e.Argument;
            int schoolyear = (int)args[0];
            int semester = (int)args[1];
            bool over100 = (bool)args[2];
            int sizeIndex = (int)args[3];
            Dictionary<string, List<string>> userType = (Dictionary<string, List<string>>)args[4];

            _BGWSemesterMoralScoresCalculate.ReportProgress(1);

            #region ���o���

            AccessHelper dataSeed = new AccessHelper();
            List<ClassRecord> allClasses = dataSeed.ClassHelper.GetSelectedClass();
            List<StudentRecord> allStudents = new List<StudentRecord>();
            Dictionary<string, List<StudentRecord>> classStudents = new Dictionary<string, List<StudentRecord>>();
            AngelDemonComputer computer = new AngelDemonComputer();

            int maxStudents = 0;
            int totalStudent = 0;
            int currentStudent = 1;

            foreach (ClassRecord aClass in allClasses)
            {
                List<StudentRecord> studnetList = aClass.Students;
                if (studnetList.Count > maxStudents)
                    maxStudents = studnetList.Count;
                allStudents.AddRange(studnetList);

                classStudents.Add(aClass.ClassID, studnetList);
                totalStudent += studnetList.Count;
            }

            computer.FillDemonScore(dataSeed, schoolyear, semester, allStudents);

            if ( semester == 2 )
            {
                dataSeed.StudentHelper.FillSemesterEntryScore(true, allStudents);
                dataSeed.StudentHelper.FillSchoolYearEntryScore(true, allStudents);
                dataSeed.StudentHelper.FillSemesterHistory(allStudents);
            }

            SystemInformation.getField("DiffItem");
            SystemInformation.getField("Degree");

            Dictionary<string, decimal> degreeList = (Dictionary<string, decimal>)SystemInformation.Fields["Degree"];
            List<string> TextScoreList = computer.TextScoreList;

            #endregion

            #region ���ͪ��
            Workbook template = new Workbook();
            Workbook prototype = new Workbook();

            //�C�L�ؤo
            if (sizeIndex == 0)
                template.Open(new MemoryStream(Properties.Resources.�w���{�`��s��A3), FileFormatType.Excel2003);
            else if (sizeIndex == 1)
                template.Open(new MemoryStream(Properties.Resources.�w���{�`��s��A4), FileFormatType.Excel2003);
            else if (sizeIndex == 2)
                template.Open(new MemoryStream(Properties.Resources.�w���{�`��s��B4), FileFormatType.Excel2003);

            prototype.Copy(template);

            Worksheet templateSheet = template.Worksheets[0];
            Worksheet prototypeSheet = prototype.Worksheets[0];

            Range tempAbsence = templateSheet.Cells.CreateRange(9, 1, true);
            Range tempScoreText = templateSheet.Cells.CreateRange(10,1,true);
            Range tempAfterOtherDiff = templateSheet.Cells.CreateRange(11, 1, true);

            Dictionary<string, int> columnIndexTable = new Dictionary<string, int>();

            Dictionary<string, List<string>> periodAbsence = new Dictionary<string, List<string>>();

            //�������g�� Column Index
            columnIndexTable.Add("�j�\", 3);
            columnIndexTable.Add("�p�\", 4);
            columnIndexTable.Add("�ż�", 5);
            columnIndexTable.Add("�j�L", 6);
            columnIndexTable.Add("�p�L", 7);
            columnIndexTable.Add("ĵ�i", 8);

            //���m�[���
            int ptColIndex = 9;
            foreach (UsefulPeriodAbsence var in computer.UsefulPeriodAbsences )
            {
                if ( !periodAbsence.ContainsKey(var.Period) )
                    periodAbsence.Add(var.Period, new List<string>());
                if ( !periodAbsence[var.Period].Contains(var.Absence) )
                    periodAbsence[var.Period].Add(var.Absence);

                prototypeSheet.Cells.CreateRange(ptColIndex, 1, true).Copy(tempAbsence);
                ptColIndex += 1;
            }

            ptColIndex = 9;

            foreach (string period in periodAbsence.Keys)
            {
                prototypeSheet.Cells.CreateRange(2, ptColIndex, 1, periodAbsence[period].Count).Merge();
                prototypeSheet.Cells[2, ptColIndex].PutValue(period);

                foreach (string absence in periodAbsence[period])
                {
                    prototypeSheet.Cells[3, ptColIndex].PutValue(absence);
                    columnIndexTable.Add(period + "_" + absence, ptColIndex);
                    ptColIndex++;
                }
            }

            if (ptColIndex > 9)
            {
                prototypeSheet.Cells.CreateRange(1, 9, 1, ptColIndex - 9).Merge();
                prototypeSheet.Cells[1, 9].PutValue("���m");
            }

            //��r���q
            foreach (string textscore in TextScoreList)
            {
                columnIndexTable.Add(textscore, ptColIndex);
                prototypeSheet.Cells.CreateRange(ptColIndex, 1, true).Copy(tempScoreText);
                prototypeSheet.Cells[4, ptColIndex].PutValue(textscore);
                ptColIndex++;
            }

            prototypeSheet.Cells[1, ptColIndex - TextScoreList.Count].PutValue("�ǥͺ�X��{"); 
            
            if ((ptColIndex-TextScoreList.Count>0) && (TextScoreList.Count>0))
            {
                prototypeSheet.Cells.CreateRange(1, ptColIndex-TextScoreList.Count, 1, TextScoreList.Count).Merge();
                prototypeSheet.Cells.CreateRange(2, ptColIndex-TextScoreList.Count, 1, TextScoreList.Count).Merge();
                prototypeSheet.Cells.CreateRange(3, ptColIndex-TextScoreList.Count, 1, TextScoreList.Count).Merge();
            }

            prototypeSheet.Cells.CreateRange(ptColIndex, 1, true).Copy(tempAfterOtherDiff);
            columnIndexTable.Add("���y", ptColIndex++);

            //��J�s����
            prototypeSheet.Cells[0, 0].PutValue("�s�����G" + DateTime.Today.ToShortDateString());

            //��J���D
            prototypeSheet.Cells.CreateRange(0, 3, 1, ptColIndex - 3).Merge();
            prototypeSheet.Cells[0, 3].PutValue(SystemInformation.SchoolChineseName + " " + schoolyear + " �Ǧ~�� " + ((semester == 1) ? "�W" : "�U") + " �Ǵ� �w���{�`��]�s��^�@�@�@�@�@�@�@�@");

            Range ptEachRow = prototypeSheet.Cells.CreateRange(5, 1, false);

            for (int i = 5; i < maxStudents + 5; i++)
            {
                prototypeSheet.Cells.CreateRange(i, 1, false).Copy(ptEachRow);
            }

            //�[�W���u
            //prototypeSheet.Cells.CreateRange(maxStudents + 5, 0, 1, ptColIndex).SetOutlineBorder(BorderType.TopBorder, CellBorderType.Medium, System.Drawing.Color.Black);

            for (int i = 11; i >= ptColIndex; i--)
                prototypeSheet.Cells.DeleteColumn(i);

            Range pt = prototypeSheet.Cells.CreateRange(0, maxStudents + 5, false);

            #endregion

            #region ��J���
            Workbook wb = new Workbook();
            wb.Copy(prototype);
            Worksheet ws = wb.Worksheets[0];

            int index = 0;
            int dataIndex = 0;
            int classTotalRow = maxStudents + 5;

            foreach (ClassRecord aClass in allClasses)
            {
                //�ƻs�����᪺�˪O
                ws.Cells.CreateRange(index, classTotalRow, false).Copy(pt);

                //��J�Z�ŦW��
                ws.Cells[index + 1, 0].PutValue(aClass.ClassName);

                Dictionary<string, int> degreeCount = new Dictionary<string, int>();
                foreach (string key in degreeList.Keys)
                {
                    degreeCount.Add(key, 0);
                }

                dataIndex = index + 5;

                foreach (StudentRecord aStudent in classStudents[aClass.ClassID])
                {
                    ws.Cells[dataIndex, 0].PutValue(aStudent.SeatNo);
                    ws.Cells[dataIndex, 1].PutValue(aStudent.StudentName);
                    ws.Cells[dataIndex, 2].PutValue(aStudent.StudentNumber);

                    decimal score = 0;

                    XmlElement demonScore = (XmlElement)aStudent.Fields["DemonScore"];

                    score = decimal.Parse(demonScore.GetAttribute("Score"));

                    foreach (XmlElement var in demonScore.SelectNodes("SubScore"))
                    {
                        if (var.GetAttribute("Type") == "���g")
                        {
                            int colIndex = columnIndexTable[var.GetAttribute("Name")];
                            if (decimal.Parse(var.GetAttribute("Count")) != 0)
                                ws.Cells[dataIndex, colIndex].PutValue(var.GetAttribute("Count"));
                        }
                        else if (var.GetAttribute("Type") == "���m")
                        {
                            string pa = var.GetAttribute("PeriodType") + "_" + var.GetAttribute("Absence");
                            if (columnIndexTable.ContainsKey(pa))
                            {
                                int colIndex = columnIndexTable[pa];
                                if (decimal.Parse(var.GetAttribute("Count")) != 0)
                                    ws.Cells[dataIndex, colIndex].PutValue(var.GetAttribute("Count"));
                            }
                        }
                        else if (var.GetAttribute("Type") == "��r���q")
                        {
                            string strFace=var.GetAttribute("Face");
                            if (columnIndexTable.ContainsKey(strFace))
                            {
                                int colIndex = columnIndexTable[strFace];
                                ws.Cells[dataIndex, colIndex].PutValue(var.GetAttribute("FaceText"));
                            }
                        }
                    }

                    //���y
                    if (demonScore.SelectSingleNode("Others/@Comment") != null)
                        ws.Cells[dataIndex, columnIndexTable["���y"]].PutValue(demonScore.SelectSingleNode("Others/@Comment").InnerText);

                    dataIndex++;

                    //�^���i��
                    _BGWSemesterMoralScoresCalculate.ReportProgress((int)(currentStudent++ * 100.0 / totalStudent));
                }

                index += classTotalRow+2;
                ws.HPageBreaks.Add(index, ptColIndex);
            }

            #endregion

            e.Result = wb;
        }
    }
}