using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FISCA.Presentation.Controls;
using K12.Data;
using SHSchool.Data;
using Aspose.Cells;
namespace SemScorePercent
{
    public partial class 學期學業成績比率清單 : BaseForm
    {
        public 學期學業成績比率清單()
        {
            InitializeComponent();
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (intSchoolYear.Value != 0 && intSemester.Value != 0 &&  intPercent.Value != 0)
            {                           
                List<SHSemesterScoreRecord> StudSemesterScoreRecs = new List<SHSemesterScoreRecord>();
                Dictionary<string, List<SHSemesterScoreRecord>> dicStuSemsRecs = new Dictionary<string, List<SHSemesterScoreRecord>>();
                //收集學期成績
                int ClassCount = K12.Presentation.NLDPanels.Class.SelectedSource.Count;
                int nowCount = 0;
                foreach (ClassRecord cr in K12.Data.Class.SelectByIDs(K12.Presentation.NLDPanels.Class.SelectedSource))
                {
                    //設定進度Bar
                    ProgBar1.Value = (nowCount++*30/ClassCount);                                           
                    StudSemesterScoreRecs = SHSemesterScore.SelectByStudents(cr.Students);
                    foreach (SHSemesterScoreRecord SemsScore in StudSemesterScoreRecs)
                    {
                        //判斷科目成績是否為指定學年度學期
                        if (SemsScore.SchoolYear == intSchoolYear.Value && SemsScore.Semester == intSemester.Value)
                        {
                            if (!dicStuSemsRecs.ContainsKey(SemsScore.RefStudentID))
                                dicStuSemsRecs.Add(SemsScore.RefStudentID, new List<SHSemesterScoreRecord>());
                            dicStuSemsRecs[SemsScore.RefStudentID].Add(SemsScore);

                        }
                    }                         
                                        
                   }               
                //放置基本資料及成績
                nowCount = 0;
                Dictionary<string, List<StudentData>> StudDatas = new Dictionary<string, List<StudentData>>();
                foreach (ClassRecord cr in K12.Data.Class.SelectByIDs(K12.Presentation.NLDPanels.Class.SelectedSource))
                {
                    //設定進度Bar
                    ProgBar1.Value = (nowCount++*30 / ClassCount)+30;
                    foreach (StudentRecord StudRec in cr.Students)
                    {
                        if (StudRec.Status == SHStudentRecord.StudentStatus.一般)
                        {
                            StudentData sd = new StudentData();
                            sd.StudentID = StudRec.ID;
                            sd.StudentNum = StudRec.StudentNumber;
                            sd.ClassName = cr.Name;
                            sd.SeatNo = StudRec.SeatNo.ToString();
                            sd.Name = StudRec.Name;
                            sd.ClassYear = cr.GradeYear.ToString();
                            sd.ClassNum = cr.Students.Count();
                            foreach(SHStudentTagRecord st in SHStudentTag.SelectByStudentID(StudRec.ID))
                                if (st.Prefix=="成績身分")
                                     sd.SubCategory=st.Name;
                            if (dicStuSemsRecs.ContainsKey(sd.StudentID))
                                foreach (SHSemesterScoreRecord SemsScore in dicStuSemsRecs[sd.StudentID])
                                {
                                    foreach (string SemsSubjID in SemsScore.Subjects.Keys)
                                    {
                                        SemsScore ss = new SemsScore();
                                        if (SemsScore.Subjects.ContainsKey(SemsSubjID))
                                        {
                                            SHSubjectScore SemSubj = SemsScore.Subjects[SemsSubjID];
                                            if (SemSubj.NotIncludedInCredit == false)
                                            {
                                                ss.SubjectName = SemSubj.Subject;
                                                if (SemSubj.Level != null)
                                                    ss.SubjectLevel = SemSubj.Level.Value.ToString();
                                                else
                                                    ss.SubjectLevel = "0";
                                                ss.SchoolYear = SemsScore.SchoolYear;
                                                ss.Semester = SemsScore.Semester;
                                                if (SemSubj.Score != null)
                                                    ss.Score = SemSubj.Score.Value;
                                                else
                                                    ss.Score = 0;
                                                sd.lstStuSemsScore.Add(ss);
                                            }
                                        }
                                    }

                                }
                            if (!StudDatas.ContainsKey(cr.Name))
                                StudDatas.Add(cr.Name, new List<StudentData>());
                            StudDatas[cr.Name].Add(sd);
                        }
                    }
                                      
                }
               
                // 班科排名
                Dictionary<string, List<decimal>> tmpSubjRank = new Dictionary<string, List<decimal>>();
                nowCount = 0;
                foreach (ClassRecord cr in K12.Data.Class.SelectByIDs(K12.Presentation.NLDPanels.Class.SelectedSource))
                {
                    //設定進度Bar
                    ProgBar1.Value = (nowCount++*30 / ClassCount) +60;
                    if (StudDatas.ContainsKey(cr.Name))
                        {
                        foreach (StudentData sd in StudDatas[cr.Name])
                        {                               
                            sd.ClassNum = cr.Students.Count;
                            foreach (SemsScore ss in sd.lstStuSemsScore)
                            {
                                if (!tmpSubjRank.ContainsKey(ss.SubjectName + ss.SubjectLevel))
                                    tmpSubjRank.Add(ss.SubjectName + ss.SubjectLevel, new List<decimal>());
                                tmpSubjRank[ss.SubjectName + ss.SubjectLevel].Add(ss.Score);
                            }      
                        }
                        foreach (string Subj in tmpSubjRank.Keys)
                        {
                            tmpSubjRank[Subj].Sort();
                            tmpSubjRank[Subj].Reverse();
                        }

                        foreach (StudentData sd in StudDatas[cr.Name])
                        {                             
                            foreach (SemsScore ss in sd.lstStuSemsScore)
                                ss.ClassScoreRank = tmpSubjRank[ss.SubjectName + ss.SubjectLevel].IndexOf(ss.Score) + 1;
                        }
                        tmpSubjRank = new Dictionary<string, List<decimal>>();
                    }  
                }
               
                //列印報表
                Workbook wb = new Workbook();
                int wksheets = 0, row = 0, i = 0;
                Style defaultStyle = wb.DefaultStyle;
                defaultStyle.Font.Size = 12;
                defaultStyle.Font.Name = "標楷體";
                wb.DefaultStyle = defaultStyle;
                wksheets = StudDatas.Keys.Count();

                for (i = 0; i < wksheets; i++)
                    wb.Worksheets.Add();

                bool chkdata1 = false;
                row = 1;
                i = 0;
                nowCount = 0;
                foreach (ClassRecord cr in K12.Data.Class.SelectByIDs(K12.Presentation.NLDPanels.Class.SelectedSource))
                {
                    //設定進度Bar
                    ProgBar1.Value = (nowCount++*5 / ClassCount) +90;
                   
                    if (StudDatas.ContainsKey(cr.Name))
                    {
                        foreach (StudentData sd in StudDatas[cr.Name])
                            {                           
                            chkdata1 = false;
                            // 處理總表先
                            wb.Worksheets[i].Name = "總表";
                            foreach (SemsScore ss in sd.lstStuSemsScore)
                            {
                                if (((ss.ClassScoreRank * 100) / sd.ClassNum) > (100 - intPercent.Value))
                                {

                                    wb.Worksheets[i].Cells[0, 5].PutValue("科目名稱");
                                    wb.Worksheets[i].Cells[row, 5].PutValue(ss.SubjectName + LevelChange(ss.SubjectLevel));
                                    wb.Worksheets[i].Cells[0, 6].PutValue("分數");
                                    wb.Worksheets[i].Cells[row, 6].PutValue(ss.Score);
                                    wb.Worksheets[i].Cells[0, 7].PutValue("班排名");
                                    wb.Worksheets[i].Cells[row, 7].PutValue(ss.ClassScoreRank);
                                    chkdata1 = true;
                                    if (chkdata1 == true)
                                    {
                                        wb.Worksheets[i].Cells[0, 0].PutValue("學號");
                                        wb.Worksheets[i].Cells[0, 1].PutValue("班級");
                                        wb.Worksheets[i].Cells[0, 2].PutValue("座號");
                                        wb.Worksheets[i].Cells[0, 3].PutValue("姓名");
                                        wb.Worksheets[i].Cells[0, 4].PutValue("成績身分");
                                        wb.Worksheets[i].Cells[row, 0].PutValue(sd.StudentNum);
                                        wb.Worksheets[i].Cells[row, 1].PutValue(sd.ClassName);
                                        wb.Worksheets[i].Cells[row, 2].PutValue(sd.SeatNo);
                                        wb.Worksheets[i].Cells[row, 3].PutValue(sd.Name);
                                        wb.Worksheets[i].Cells[row, 4].PutValue(sd.SubCategory);
                                        row++;
                                    }
                                }
                            }
                        }                        
                    }
                                       
                }
                wb.Worksheets[i].AutoFitRows();
                wb.Worksheets[i].AutoFitColumns();
                // 分班
                i = 1;
                nowCount = 0;
                foreach (ClassRecord cr in K12.Data.Class.SelectByIDs(K12.Presentation.NLDPanels.Class.SelectedSource))
                {
                    //設定進度Bar
                    ProgBar1.Value = (nowCount++*5 / ClassCount) +95;
                   
                    row = 1;
                    wb.Worksheets[i].Name = cr.Name;
                    if (StudDatas.ContainsKey(cr.Name))
                    {
                        foreach (StudentData sd in StudDatas[cr.Name])
                        {          
                            chkdata1 = false;                               
                            foreach (SemsScore ss in sd.lstStuSemsScore)
                            {
                                if (((ss.ClassScoreRank * 100) / sd.ClassNum) > (100 - intPercent.Value))
                                {
                                    wb.Worksheets[i].Cells[0, 5].PutValue("科目名稱");
                                    wb.Worksheets[i].Cells[row, 5].PutValue(ss.SubjectName + LevelChange(ss.SubjectLevel));
                                    wb.Worksheets[i].Cells[0, 6].PutValue("分數");
                                    wb.Worksheets[i].Cells[row, 6].PutValue(ss.Score);
                                    wb.Worksheets[i].Cells[0, 7].PutValue("班排名");
                                    wb.Worksheets[i].Cells[row, 7].PutValue(ss.ClassScoreRank);
                                    chkdata1 = true;
                                    if (chkdata1 == true)
                                    {
                                        wb.Worksheets[i].Cells[0, 0].PutValue("學號");
                                        wb.Worksheets[i].Cells[0, 1].PutValue("班級");
                                        wb.Worksheets[i].Cells[0, 2].PutValue("座號");
                                        wb.Worksheets[i].Cells[0, 3].PutValue("姓名");
                                        wb.Worksheets[i].Cells[0, 4].PutValue("成績身分");
                                        wb.Worksheets[i].Cells[row, 0].PutValue(sd.StudentNum);
                                        wb.Worksheets[i].Cells[row, 1].PutValue(sd.ClassName);
                                        wb.Worksheets[i].Cells[row, 2].PutValue(sd.SeatNo);
                                        wb.Worksheets[i].Cells[row, 3].PutValue(sd.Name);
                                        wb.Worksheets[i].Cells[row, 4].PutValue(sd.SubCategory);
                                        row++;
                                    }
                                }
                            }
                           
                    }
                    wb.Worksheets[i].AutoFitRows();
                    wb.Worksheets[i].AutoFitColumns();
                    i = i + 1;
                    }
                    
                }               
                try
                {
                    wb.Save(Application.StartupPath + "\\Reports\\學期學業成績比率清單.xls", FileFormatType.Excel2003);
                    System.Diagnostics.Process.Start(Application.StartupPath + "\\Reports\\學期學業成績比率清單.xls");
                }
                catch
                {
                    System.Windows.Forms.SaveFileDialog sd1 = new System.Windows.Forms.SaveFileDialog();
                    sd1.Title = "另存新檔";
                    sd1.FileName = "學期學業成績比率清單.xls";
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
            else
                MessageBox.Show("資料設定不完整!");
        }

        private void 學期學業成績比率清單_Load(object sender, EventArgs e)
        {
            intSchoolYear.Value = int.Parse(K12.Data.School.DefaultSchoolYear);
            intSemester.Value =int.Parse( K12.Data.School.DefaultSemester);           

        }
        private string LevelChange(string level)
        {
            string levelstr = "";
            switch (level)
            {
                case "0":
                    levelstr = "";
                    break;
                case "1":
                    levelstr = "Ⅰ";
                    break;
                case "2":
                    levelstr = "Ⅱ";
                    break;
                case "3":
                    levelstr = "Ⅲ";
                    break;
                case "4":
                    levelstr = "Ⅳ";
                    break;
                case "5":
                    levelstr = "Ⅴ";
                    break;
                case "6":
                    levelstr = "Ⅵ";
                    break;
            }
            return levelstr;
        }
    }
}
