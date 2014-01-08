using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FISCA.Presentation.Controls;
using Aspose.Cells;
using K12.Data;
using K12.Presentation;
using SHSchool.Data;
namespace SemScoreYearScoreByPassProcent
{
    public partial class Form1 : BaseForm
    {
        public Form1()
        {
            InitializeComponent();
        }
        //AccessHelper accHelper = new AccessHelper();       
        //List<StudentRecord> StudRecs = new List<StudentRecord>();
        //List<ClassRecord> ClassRecs = new List<ClassRecord>();
        private void Form1_Load(object sender, EventArgs e)
        {
            cboSchoolYear.Items.Clear();
            cboSemester.Items.Clear();
            for (int i = DateTime.Now.Year - 1917; i <= DateTime.Now.Year - 1905; i++)
                cboSchoolYear.Items.Add(i);
            //cboSemester.Items.Add("無");
            cboSemester.Items.Add("上學期");
            cboSemester.Items.Add("下學期");
            cboSchoolYear.Text = K12.Data.School.DefaultSchoolYear;
            cboSemester.Text = (K12.Data.School.DefaultSemester=="1" ? "上學期" : "下學期");
            //ClassRecs = accHelper.ClassHelper.GetSelectedClass();
            // // 加入所選取學生
            //foreach (ClassRecord cr in ClassRecs)
            //    StudRecs.AddRange(cr.Students);            
            //accHelper.StudentHelper.FillSchoolYearSubjectScore(true,StudRecs);

            //accHelper.StudentHelper.FillSemesterSubjectScore(true, StudRecs);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (cboSchoolYear.Text == null || cboSchoolYear.Text == "")
            {
                 MsgBox.Show("學年度錯誤，無法列印");
                 return;
            }
            int SchoolYear = 0;            
            if (!int.TryParse(cboSchoolYear.Text, out SchoolYear))
             {
                 MsgBox.Show("學年度錯誤，無法列印");
                 return;
            }
            Workbook wb = new Workbook();
            int wksheets = 0, row = 0, i = 0;
            int PassProcent = 0;
            PassProcent=intProcent.Value;
            wksheets = K12.Presentation.NLDPanels.Class.SelectedSource.Count;
            Dictionary<string, List<string>> noPassSubject = new Dictionary<string, List<string>>();
            decimal TotalCredit = 0;
            decimal noPassCredit = 0;
            List<string> noPassStuID = new List<string>();
            for (i = 0; i < wksheets; i++)
                wb.Worksheets.Add();
            List<string> StudRecs = new List<string>();
            //List<SHSchoolYearScoreRecord> StudSchoolYearScoreRecs = new List<SHSchoolYearScoreRecord>();
            List<SHSemesterScoreRecord> StudSemesterScoreRecs = new List<SHSemesterScoreRecord>();
            //收集學期成績
            foreach (ClassRecord cr in K12.Data.Class.SelectByIDs(K12.Presentation.NLDPanels.Class.SelectedSource))
                foreach (StudentRecord sr in cr.Students)
                    if (!StudRecs.Contains(sr.ID))
                        StudRecs.Add(sr.ID);
            //StudSchoolYearScoreRecs = SHSchoolYearScore.SelectByStudentIDs(StudRecs);
            StudSemesterScoreRecs = SHSemesterScore.SelectByStudentIDs(StudRecs);
            //Dictionary<string, List<SHSchoolYearScoreRecord>> dicStuYearRecs = new Dictionary<string, List<SHSchoolYearScoreRecord>>();
            Dictionary<string, List<SHSemesterScoreRecord>> dicStuSemsRecs = new Dictionary<string, List<SHSemesterScoreRecord>>();
            //foreach (SHSchoolYearScoreRecord YearScore in StudSchoolYearScoreRecs)
            //{
            //    if (!dicStuYearRecs.ContainsKey(YearScore.RefStudentID))
            //        dicStuYearRecs.Add(YearScore.RefStudentID, new List<SHSchoolYearScoreRecord>());
            //    dicStuYearRecs[YearScore.RefStudentID].Add(YearScore);

            //}
            foreach (SHSemesterScoreRecord SemsScore in StudSemesterScoreRecs)
            {
                if (!dicStuSemsRecs.ContainsKey(SemsScore.RefStudentID))
                    dicStuSemsRecs.Add(SemsScore.RefStudentID, new List<SHSemesterScoreRecord>());
                dicStuSemsRecs[SemsScore.RefStudentID].Add(SemsScore);

            }
            //找出選擇的班級
            foreach (ClassRecord cr in K12.Data.Class.SelectByIDs(K12.Presentation.NLDPanels.Class.SelectedSource))
            {
                //找出各班所有學生
                foreach (StudentRecord sr in cr.Students)
                {
                    //判斷是否為一般生或延修生
                    if (sr.Status == K12.Data.StudentRecord.StudentStatus.一般 || sr.Status == K12.Data.StudentRecord.StudentStatus.延修)
                    {
                        TotalCredit = 0;  //總修學分
                        noPassCredit = 0;  //未取得學分
                        if (dicStuSemsRecs.ContainsKey(sr.ID))
                        {
                            foreach (SHSemesterScoreRecord SemsScore in dicStuSemsRecs[sr.ID])
                            {
                                //判斷科目成績是否為指定學年度學期
                                if (SemsScore.SchoolYear == SchoolYear && (cboSemester.Text == "無" || (SemsScore.Semester == (cboSemester.Text == "上學期" ? 1 : 2))))
                                {
                                    foreach (string SemsSubjID in SemsScore.Subjects.Keys)
                                    {
                                        if (SemsScore.Subjects.ContainsKey(SemsSubjID))
                                        {
                                            SHSubjectScore SemSubj = SemsScore.Subjects[SemsSubjID];
                                            //不計學分判斷
                                            if (SemSubj.NotIncludedInCredit == false)
                                            {
                                                TotalCredit += (decimal)SemSubj.Credit;
                                                //判斷是否取得學分
                                                if (SemSubj.Pass == false)
                                                {
                                                    noPassCredit += (decimal)SemSubj.Credit;
                                                    if (!noPassSubject.ContainsKey(sr.ID))
                                                    {
                                                        noPassSubject.Add(sr.ID, new List<string>());
                                                    }
                                                    noPassSubject[sr.ID].Add(SemSubj.Subject + LevelChange((SemSubj.Level == null ? 0 : (int)SemSubj.Level)) + "(" + SemSubj.Credit + ")" + "-" + SemSubj.Score);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        //判斷是否為未達標準學生
                        if (TotalCredit != 0 && ((Math.Round(noPassCredit / TotalCredit, 2, MidpointRounding.AwayFromZero) * 100) > PassProcent))
                        {
                            noPassStuID.Add(sr.ID);
                            if (!noPassSubject.ContainsKey(sr.ID))
                                noPassSubject.Add(sr.ID, new List<string>());
                            noPassSubject[sr.ID].Add(TotalCredit-noPassCredit + "/" + TotalCredit);
                        }
                    }
                }  
             }
            row = 1;
            i = 1;
            int classrow = 1;
            wb.Worksheets[0].Name = "總表";
            int col = 0;
            if (rbSchoolYear.Checked == true)
            {
                wb.Worksheets[0].Cells[0, 0].PutValue("學年度");
                col = 1;
            }
            else
            {
                wb.Worksheets[0].Cells[0, 0].PutValue("學年度");
                wb.Worksheets[0].Cells[0, 1].PutValue("學期");
                col = 2;
            }
            wb.Worksheets[0].Cells[0, col].PutValue("班級");
            wb.Worksheets[0].Cells[0,col+1 ].PutValue("座號");
            wb.Worksheets[0].Cells[0, col+2].PutValue("學號");
            wb.Worksheets[0].Cells[0, col+3].PutValue("姓名");
            wb.Worksheets[0].Cells[0, col+4].PutValue("取得學分");
             wb.Worksheets[0].Cells[0,col+5 ].PutValue("應取得學分");
            foreach (ClassRecord cr in K12.Data.Class.SelectByIDs(K12.Presentation.NLDPanels.Class.SelectedSource))
            {                
                wb.Worksheets[i].Name = cr.Name;
                classrow = 1;
                if (rbSchoolYear.Checked == true)
                {
                    wb.Worksheets[i].Cells[0, 0].PutValue("學年度");
                    col = 1;
                }
                else
                {
                    wb.Worksheets[i].Cells[0, 0].PutValue("學年度");
                    wb.Worksheets[i].Cells[0, 1].PutValue("學期");
                    col = 2;
                }
                wb.Worksheets[i].Cells[0, col ].PutValue("座號");
                wb.Worksheets[i].Cells[0, col +1].PutValue("學號");
                wb.Worksheets[i].Cells[0, col + 2].PutValue("姓名");
                wb.Worksheets[i].Cells[0, col + 3].PutValue("取得學分");
                wb.Worksheets[i].Cells[0, col + 4].PutValue("應取得學分");
                foreach (StudentRecord sr in cr.Students)
                {                    
                    if (noPassStuID.Contains(sr.ID))
                    {
                        if (rbSchoolYear.Checked == true)
                        {
                            wb.Worksheets[0].Cells[row, 0].PutValue(cboSchoolYear.Text);
                            wb.Worksheets[i].Cells[classrow, 0].PutValue(cboSchoolYear.Text);
                            col = 1;
                        }
                        else
                        {
                            wb.Worksheets[0].Cells[row, 0].PutValue(cboSchoolYear.Text);
                            wb.Worksheets[i].Cells[classrow, 0].PutValue(cboSchoolYear.Text);
                            wb.Worksheets[0].Cells[row, 1].PutValue(cboSemester.Text);
                            wb.Worksheets[i].Cells[classrow, 1].PutValue(cboSemester.Text);
                            col = 2;
                        }
                        wb.Worksheets[0].Cells[row, col].PutValue(cr.Name);
                        wb.Worksheets[0].Cells[row, col+1].PutValue(sr.SeatNo);
                        wb.Worksheets[0].Cells[row, col + 2].PutValue(sr.StudentNumber);
                        wb.Worksheets[0].Cells[row, col + 3].PutValue(sr.Name);
                        wb.Worksheets[i].Cells[classrow, col].PutValue(sr.SeatNo);
                        wb.Worksheets[i].Cells[classrow, col+1].PutValue(sr.StudentNumber);
                        wb.Worksheets[i].Cells[classrow, col+2].PutValue(sr.Name);
                        if (noPassSubject.ContainsKey(sr.ID))
                        {
                            foreach (string subjstr in noPassSubject[sr.ID])
                            {
                                //放入取得學分及總學分
                                if (subjstr.Contains("/"))
                                {
                                    if (rbSchoolYear.Checked == true)
                                    {
                                        wb.Worksheets[0].Cells[row,  5].PutValue(subjstr.Split('/')[0]);
                                        wb.Worksheets[0].Cells[row,  6].PutValue(subjstr.Split('/')[1]);
                                        wb.Worksheets[i].Cells[classrow, 4].PutValue(subjstr.Split('/')[0]);
                                        wb.Worksheets[i].Cells[classrow, 5].PutValue(subjstr.Split('/')[1]);
                                    }
                                    else
                                    {
                                        wb.Worksheets[0].Cells[row,  6].PutValue(subjstr.Split('/')[0]);
                                        wb.Worksheets[0].Cells[row,  7].PutValue(subjstr.Split('/')[1]);
                                        wb.Worksheets[i].Cells[classrow, 5].PutValue(subjstr.Split('/')[0]);
                                        wb.Worksheets[i].Cells[classrow, 6].PutValue(subjstr.Split('/')[1]);
                                    }
                                }
                                else
                                {
                                    if (rbSchoolYear.Checked == true)
                                    {
                                        wb.Worksheets[0].Cells[0, col + 6].PutValue("科目" + (col ));
                                        wb.Worksheets[i].Cells[0, col + 5].PutValue("科目" + (col ));
                                    }
                                    else
                                    {
                                        wb.Worksheets[0].Cells[0, col + 6].PutValue("科目" + (col - 1));
                                        wb.Worksheets[i].Cells[0, col + 5].PutValue("科目" + (col - 1)); 
                                    }
                                    wb.Worksheets[0].Cells[row, col + 6].PutValue(subjstr);
                                    wb.Worksheets[i].Cells[classrow, col + 5].PutValue(subjstr);
                                }
                                col++;                                
                            }
                        }
                        classrow++;
                        row++;
                        }
                }
                col = wb.Worksheets[i].Cells.MaxDataColumn;
                for (int j = 0; j < classrow; j++)
                 for (int k = 0; k < col + 1; k++)
                    {
                        //wb.Worksheets[i].Cells[i, j].Style.HorizontalAlignment = TextAlignmentType.Center;                        
                        wb.Worksheets[i].Cells[j, k].Style.VerticalAlignment = TextAlignmentType.Center;
                        wb.Worksheets[i].Cells[j, k].Style.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
                        wb.Worksheets[i].Cells[j, k].Style.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
                        wb.Worksheets[i].Cells[j, k].Style.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
                        wb.Worksheets[i].Cells[j, k].Style.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
                        wb.Worksheets[i].AutoFitColumn(k);
                    }  
                i++;
            }
            i = 0;
            col = wb.Worksheets[0].Cells.MaxDataColumn;
            for (int j = 0; j < row; j++)            
                for (int k = 0; k < col + 1; k++)
                {
                    //wb.Worksheets[0].Cells[i, j].Style.HorizontalAlignment = TextAlignmentType.Center;
                    wb.Worksheets[0].Cells[j, k].Style.VerticalAlignment = TextAlignmentType.Center;
                    wb.Worksheets[0].Cells[j, k].Style.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
                    wb.Worksheets[0].Cells[j, k].Style.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
                    wb.Worksheets[0].Cells[j, k].Style.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
                    wb.Worksheets[0].Cells[j, k].Style.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
                    wb.Worksheets[0].AutoFitColumn(k);
                }
            try
            {
                wb.Save(Application.StartupPath + "\\Reports\\學分數未達標準名單.xls", FileFormatType.Excel2003);
                System.Diagnostics.Process.Start(Application.StartupPath + "\\Reports\\學分數未達標準名單.xls");
            }
            catch
            {
                System.Windows.Forms.SaveFileDialog sd1 = new System.Windows.Forms.SaveFileDialog();
                sd1.Title = "另存新檔";
                sd1.FileName = "學分數未達標準名單.xls";
                sd1.Filter = "Excel檔案 (*.xls)|*.xls|所有檔案 (*.*)|*.*";
                if (sd1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    try
                    {
                        wb.Save(sd1.FileName, FileFormatType.Excel2003);
                    }
                    catch
                    {
                        System.Windows.Forms.MessageBox.Show("指定路徑無法存取。", "建立檔案失敗", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        return;
                    }
                }
            }
        }
        private string LevelChange(int level)
        {
            string levelstr = "";
            switch (level)
            {
                case 0:
                    levelstr = "";
                    break;
                case 1:
                    levelstr = "Ⅰ";
                    break;
                case 2:
                    levelstr = "Ⅱ";
                    break;
                case 3:
                    levelstr = "Ⅲ";
                    break;
                case 4:
                    levelstr = "Ⅳ";
                    break;
                case 5:
                    levelstr = "Ⅴ";
                    break;
                case 6:
                    levelstr = "Ⅵ";
                    break;
            }
            return levelstr;
        }

        private void rbSchoolYear_CheckedChanged(object sender, EventArgs e)
        {
            cboSchoolYear.Text = K12.Data.School.DefaultSchoolYear;
            if (rbSchoolYear.Checked == true)
            {
                cboSemester.Text = "無";
                cboSemester.Visible = false;
                labelX2.Visible = false;
            }
            else
            {
                cboSemester.Text = (K12.Data.School.DefaultSemester == "1" ? "上學期" : "下學期");
                cboSemester.Visible = true;
                labelX2.Visible = true;
            }
        }

        private void rbSemester_CheckedChanged(object sender, EventArgs e)
        {
            cboSchoolYear.Text = K12.Data.School.DefaultSchoolYear;
            if (rbSchoolYear.Checked == true)
            {
                cboSemester.Text = "無";
                cboSemester.Visible = false;
                labelX2.Visible = false;
            }
            else
            {
                cboSemester.Text = (K12.Data.School.DefaultSemester == "1" ? "上學期" : "下學期");
                cboSemester.Visible = true;
                labelX2.Visible = true;
            }
        }
    }
}
