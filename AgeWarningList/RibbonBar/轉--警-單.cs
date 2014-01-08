using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FISCA.Presentation.Controls;
using SHSchool.Data;
//using K12.Data;
using SmartSchool.Customization.Data;
using SmartSchool.Customization.Data.StudentExtension;
using Aspose.Cells;
namespace AgeWarningList
{
    public partial class 轉級預警名單 : BaseForm
    {
        public 轉級預警名單()
        {
            InitializeComponent();
        }
        public List<string> AbsenceList = new List<string>();
        private void checkBoxX1_CheckedChanged(object sender, EventArgs e)
        {
            labelX1.Enabled = chkAge.Checked;
            dateBirthDay.Enabled = chkAge.Checked;
        }

        private void checkBoxX2_CheckedChanged(object sender, EventArgs e)
        {
            labelX3.Enabled = chkScore.Checked;
            intSchoolYear.Enabled = chkScore.Checked;
            labelX4.Enabled = chkScore.Checked;
            intSemester.Enabled = chkScore.Checked;
            labelX5.Enabled = chkScore.Checked;
            intSubjCount.Enabled = chkScore.Checked;
        }

        private void checkBoxX3_CheckedChanged(object sender, EventArgs e)
        {
            labelX2.Enabled = chkAttend.Checked;
            intAttendCount.Enabled = chkAttend.Checked;
            linkLabel1.Enabled = chkAttend.Checked;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            選擇缺曠別 別 = new 選擇缺曠別();
            別.ShowDialog();
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private bool chkData()
        {
           Boolean chkDataCorrect=true;
           List<string> AbsenceList = AbsenceSet.AbsenceList;
           if (chkAge.Checked)
                if (dateBirthDay.IsEmpty || dateBirthDay.Value==null)
                    chkDataCorrect=false;
            if (chkScore.Checked)
                if (intSchoolYear.Value==0 || intSemester.Value==0 || intSubjCount.Value==0)
                    chkDataCorrect=false;
            if (chkAttend.Checked)
                if (intAttendCount.Value==0 || AbsenceList.Count<1 || intSchoolYearA.Value==0 || intSemesterA.Value==0)
                    chkDataCorrect=false;
            return chkDataCorrect;


        }
        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (chkData())
            {
                Dictionary<string, int> NoPassCount = new Dictionary<string, int>();
                AccessHelper accHelper = new AccessHelper();
                List<ClassRecord> ClassRecs = new List<ClassRecord>();
                ClassRecs = accHelper.ClassHelper.GetSelectedClass();
                int ClassCount = ClassRecs.Count;
                int nowCount = 0;
                // 加入所選取學生
                List<StudentRecord> StudRecs = new List<StudentRecord>();


                foreach (ClassRecord cr in ClassRecs)
                    StudRecs.AddRange(cr.Students);
                if (chkScore.Checked)
                {
                    //List<SHSemesterScoreRecord> StudSemesterScoreRecs = new List<SHSemesterScoreRecord>();
                    //Dictionary<string, List<SHSemesterScoreRecord>> dicStuSemsRecs = new Dictionary<string, List<SHSemesterScoreRecord>>();
                    ////收集學期成績               
                    //foreach (ClassRecord cr in K12.Data.Class.SelectAll())
                    //{
                    //    //設定進度Bar
                    //    ProgBar1.Value = (nowCount++ * 20 / ClassCount);
                    //    StudSemesterScoreRecs = SHSemesterScore.SelectByStudents(cr.Students);
                    //    foreach (SHSemesterScoreRecord SemsScore in StudSemesterScoreRecs)
                    //    {
                    //        //判斷科目成績是否為指定學年度學期
                    //        if (SemsScore.SchoolYear == intSchoolYear.Value && SemsScore.Semester == intSemester.Value)
                    //        {
                    //            if (!dicStuSemsRecs.ContainsKey(SemsScore.RefStudentID))
                    //                dicStuSemsRecs.Add(SemsScore.RefStudentID, new List<SHSemesterScoreRecord>());
                    //            dicStuSemsRecs[SemsScore.RefStudentID].Add(SemsScore);

                    //        }
                    //    }                       
                    //}

                    accHelper.StudentHelper.FillExamScore(intSchoolYear.Value, intSemester.Value, StudRecs);
                    //計算不及格科目
                    nowCount = 0;
                    foreach (ClassRecord cr in ClassRecs)
                    {
                        //設定進度Bar
                        ProgBar1.Value = (nowCount++ * 30 / ClassCount) + 30;
                        foreach (StudentRecord StudRec in cr.Students)
                        {
                            if (StudRec.Status == "一般")
                            {
                                //if (dicStuSemsRecs.ContainsKey(StudRec.ID))
                                //    foreach (SHSemesterScoreRecord SemsScore in dicStuSemsRecs[StudRec.ID])
                                //    {
                                //        foreach (string SemsSubjID in SemsScore.Subjects.Keys)
                                //        {
                                //            if (SemsScore.Subjects.ContainsKey(SemsSubjID))
                                //            {
                                //                SHSubjectScore SemSubj = SemsScore.Subjects[SemsSubjID];
                                //                if (SemSubj.NotIncludedInCredit == false)
                                //                {
                                //                    if (!SemSubj.Pass)
                                //                    {
                                //                        if (!NoPassCount.ContainsKey(StudRec.ID))
                                //                            NoPassCount.Add(StudRec.ID, 0);
                                //                        NoPassCount[StudRec.ID]++;
                                //                    }
                                //                }
                                //            }
                                //        }

                                //    }                               
                                foreach (ExamScoreInfo esi in StudRec.ExamScoreList)
                                {
                                    if (cboExam.Text == esi.ExamName)
                                    {
                                        if (esi.ExamScore < 60)
                                        {
                                            if (!NoPassCount.ContainsKey(StudRec.StudentID))
                                                NoPassCount.Add(StudRec.StudentID, 0);
                                            NoPassCount[StudRec.StudentID]++;
                                        }
                                    }
                                }

                            }
                        }
                    }
                }
                Dictionary<string, int> dicStuAttendCount = new Dictionary<string, int>();
                if (chkAttend.Checked)
                {
                    List<string> AbsenceList = AbsenceSet.AbsenceList;
                    //List<AttendanceRecord> attRecord = new List<AttendanceRecord>();
                    //收集缺曠統計
                    nowCount = 0;
                    accHelper.StudentHelper.FillAttendance(intSchoolYearA.Value, intSemesterA.Value, StudRecs);
                    foreach (ClassRecord cr in ClassRecs)
                    {
                        //設定進度Bar
                        ProgBar1.Value = (nowCount++ * 20 / ClassCount) + 60;
                        foreach (StudentRecord sr in cr.Students)
                        {
                            foreach (AttendanceInfo ai in sr.AttendanceList)
                            {
                                if (AbsenceList.Contains(ai.Absence))
                                {
                                    if (!dicStuAttendCount.ContainsKey(sr.StudentID))
                                        dicStuAttendCount.Add(sr.StudentID, 0);
                                    dicStuAttendCount[sr.StudentID]++;
                                }
                            }
                        }
                    }
                }
                dataList.Rows.Clear();
                nowCount = 0;
                foreach (ClassRecord cr in ClassRecs)
                {
                    //設定進度Bar
                    ProgBar1.Value = (nowCount++ * 20 / ClassCount) + 80;
                    foreach (StudentRecord StudRec in cr.Students)
                    {
                        if (StudRec.Status == "一般" || StudRec.Status == "延修生")
                        {
                            Boolean print = false;
                            if (chkAttend.Checked)
                                if (dicStuAttendCount.ContainsKey(StudRec.StudentID))
                                    if (dicStuAttendCount[StudRec.StudentID] >= intAttendCount.Value)
                                        print = true;
                            if (chkScore.Checked)
                                if (NoPassCount.ContainsKey(StudRec.StudentID))
                                    if (NoPassCount[StudRec.StudentID] >= intSubjCount.Value)
                                        print = true;
                            if (chkAge.Checked)
                                if (StudRec.Birthday != "")
                                    if (DateTime.Parse(StudRec.Birthday) <= dateBirthDay.Value.AddYears(-18))
                                        print = true;
                            if (print)
                            {
                                List<object> items = new List<object>();
                                items.Add(cr.ClassName);
                                items.Add(StudRec.SeatNo);
                                items.Add(StudRec.StudentNumber);
                                items.Add(StudRec.StudentName);
                                Boolean ciprint=false;
                                foreach (CategoryInfo ci in StudRec.StudentCategorys)
                                    if (ci.Name == "成績身分")
                                    {
                                        items.Add(ci.SubCategory);
                                        ciprint = true;
                                    }
                                if (!ciprint)
                                    items.Add("");
                                if (chkAge.Checked)
                                    if (StudRec.Birthday != "")
                                        if (DateTime.Parse(StudRec.Birthday) <= dateBirthDay.Value.AddYears(-18))
                                            items.Add(dateBirthDay.Value.Year - DateTime.Parse(StudRec.Birthday).Year);
                                        else
                                            items.Add("");
                                    else
                                        items.Add("");
                                else
                                    items.Add("");

                                if (chkScore.Checked)
                                    if (NoPassCount.ContainsKey(StudRec.StudentID))
                                        if (NoPassCount[StudRec.StudentID] >= intSubjCount.Value)
                                            items.Add(NoPassCount[StudRec.StudentID]);
                                        else
                                            items.Add("");
                                    else
                                        items.Add("");
                                else
                                    items.Add("");
                                if (chkAttend.Checked)
                                    if (dicStuAttendCount.ContainsKey(StudRec.StudentID))
                                        if (dicStuAttendCount[StudRec.StudentID] >= intAttendCount.Value)
                                            items.Add(dicStuAttendCount[StudRec.StudentID]);
                                dataList.Rows.Add(items.ToArray());
                            }
                        }
                    }

                }
            }
            else
                MessageBox.Show("資料設定不完整!");
        }
        private void 轉級預警名單_Load(object sender, EventArgs e)
        {
            intSchoolYear.Value = int.Parse(K12.Data.School.DefaultSchoolYear);
            intSemester.Value = int.Parse(K12.Data.School.DefaultSemester);
            intSchoolYearA.Value = int.Parse(K12.Data.School.DefaultSchoolYear);
            intSemesterA.Value = int.Parse(K12.Data.School.DefaultSemester);
            // 放入試別
            foreach (K12.Data.ExamRecord er in K12.Data.Exam.SelectAll())
            {
                if (!cboExam.Items.Contains(er.Name))
                    cboExam.Items.Add(er.Name);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {

            Workbook wb = new Workbook();
            Style defaultStyle = wb.DefaultStyle;
            defaultStyle.Font.Size = 12;
            defaultStyle.Font.Name = "標楷體";
            
            wb.DefaultStyle = defaultStyle;
            int row, col;
            row = 0; col = 0;
            wb.Worksheets[0].Cells[row, col].PutValue(K12.Data.School.ChineseName + "一級轉二級預警名單");
            wb.Worksheets[0].Cells[row, col].Style.Font.Size = 18;
            wb.Worksheets[0].Cells.SetRowHeight(row, 30);
            //合併儲存格
            wb.Worksheets[0].Cells.Merge(row, col, 1, dataList.ColumnCount);
            //水平置中
            wb.Worksheets[0].Cells[row, col].Style.HorizontalAlignment = TextAlignmentType.Center;
            //標題列
            for (col = 0; col < dataList.ColumnCount; col++)
            {
                wb.Worksheets[0].Cells[row + 1, col].PutValue(dataList.Columns[col].HeaderText.ToString());
                wb.Worksheets[0].Cells[row + 1, col].Style.HorizontalAlignment = TextAlignmentType.Center;
                wb.Worksheets[0].Cells[row + 1, col].Style.VerticalAlignment = TextAlignmentType.Center;
                wb.Worksheets[0].Cells[row + 1, col].Style.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
                wb.Worksheets[0].Cells[row + 1, col].Style.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
                wb.Worksheets[0].Cells[row + 1, col].Style.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
                wb.Worksheets[0].Cells[row + 1, col].Style.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            }
            for (col = 0; col < dataList.ColumnCount; col++)
                for (row = 0; row < dataList.RowCount - 1; row++)
                {
                    wb.Worksheets[0].Cells[row + 2, col].PutValue(dataList[col, row].Value);
                    wb.Worksheets[0].Cells[row + 2, col].Style.HorizontalAlignment = TextAlignmentType.Center;
                    wb.Worksheets[0].Cells[row + 2, col].Style.VerticalAlignment = TextAlignmentType.Center;
                    wb.Worksheets[0].Cells[row + 2, col].Style.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
                    wb.Worksheets[0].Cells[row + 2, col].Style.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
                    wb.Worksheets[0].Cells[row + 2, col].Style.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
                    wb.Worksheets[0].Cells[row + 2, col].Style.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
                }

            wb.Worksheets[0].PageSetup.Orientation = PageOrientationType.Portrait;//設定直向列印
            wb.Worksheets[0].PageSetup.PrintTitleRows = "$1:$2"; //設定跨頁標題
            //wb.Worksheets[0].PageSetup.FitToPagesWide = 1;   //調整為一頁寬 
            wb.Worksheets[0].AutoFitRows();
            //wb.Worksheets[0].PageSetup.FitToPagesTall = (row - (row % 35)) / 35 + 1;   //調整頁高
            //設定邊界
            wb.Worksheets[0].PageSetup.BottomMargin = 1;
            wb.Worksheets[0].PageSetup.TopMargin = 1;
            wb.Worksheets[0].PageSetup.LeftMargin = 1;
            wb.Worksheets[0].PageSetup.RightMargin = 1;

            wb.Worksheets[0].AutoFitColumns(); //自動調整欄寬

            try
            {
                wb.Save(Application.StartupPath + "\\Reports\\一級轉二級預警名單.xls", FileFormatType.Excel2003);
                System.Diagnostics.Process.Start(Application.StartupPath + "\\Reports\\一級轉二級預警名單.xls");
            }
            catch
            {
                System.Windows.Forms.SaveFileDialog sd1 = new System.Windows.Forms.SaveFileDialog();
                sd1.Title = "另存新檔";
                sd1.FileName = "一級轉二級預警名單.xls";
                sd1.Filter = "Excel檔案 (*.xls)|*.xls|所有檔案 (*.*)|*.*";
                if (sd1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    try
                    {
                        wb.Save(sd1.FileName, FileFormatType.Excel2003);
                        System.Diagnostics.Process.Start(sd1.FileName);
                    }
                    catch
                    {
                        System.Windows.Forms.MessageBox.Show("指定路徑無法存取。", "建立檔案失敗", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        return;
                    }
                }
            }

        }
    }
}
