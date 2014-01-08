using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FISCA.Presentation.Controls;
//using K12.Data;
using SHSchool.Data;
using SmartSchool.Customization.Data;
using Aspose.Cells;
namespace ExamScorePercent
{
    public partial class 評量學業成績比率清單 : BaseForm
    {
        public 評量學業成績比率清單()
        {
            InitializeComponent();
        }
        List<ExamSubj> ExSubj = new List<ExamSubj>();
        AccessHelper accHelper = new AccessHelper();       
        List<StudentRecord> StudRecs = new List<StudentRecord>();
        List<ClassRecord> ClassRecs = new List<ClassRecord>();
        List<StudentData> StudDatas=new List<StudentData>();
        //string ExName = "";
        private void buttonX2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            // 加入學生考試成績
            ClassRecs.Clear();
            StudRecs.Clear();

            if (intSchoolYear.Value != 0 && intSemester.Value != 0 && cboExam.Text != "" && intPercent.Value!=0)
            {
                ClassRecs = accHelper.ClassHelper.GetSelectedClass();

                // 加入所選取學生
                foreach (ClassRecord cr in ClassRecs)
                    StudRecs.AddRange(cr.Students);
                accHelper.StudentHelper.FillExamScore(intSchoolYear.Value, intSemester.Value, StudRecs);
                Calc_ScoreAndRank();
                Workbook wb = new Workbook();
                int wksheets = 0, row = 0, i = 0;
                Style defaultStyle = wb.DefaultStyle;
                defaultStyle.Font.Size = 12;
                defaultStyle.Font.Name = "標楷體";
                wb.DefaultStyle = defaultStyle;
                wksheets = ClassRecs.Count;

                for (i = 0; i < wksheets; i++)
                    wb.Worksheets.Add();

                bool chkdata1 = false;
                row = 1;
                i = 0;
                foreach (ClassRecord cr in ClassRecs)
                {
                    foreach (StudentData sd in StudDatas)
                    {
                        if (cr.ClassName == sd.ClassName)
                        {
                            chkdata1 = false;
                            // 處理總表先
                            wb.Worksheets[i].Name = "總表";
                            foreach (ExamData ed in sd.lstStudExamScore) 
                            {
                                if (((ed.ClassScoreRank * 100) / sd.ClassNum) > (100 - intPercent.Value))
                                {

                                    wb.Worksheets[i].Cells[0, 5].PutValue("科目名稱");
                                    wb.Worksheets[i].Cells[row, 5].PutValue(ed.SubjectName + LevelChange(ed.SubjectLevel));
                                    wb.Worksheets[i].Cells[0, 6].PutValue("分數");
                                    wb.Worksheets[i].Cells[row, 6].PutValue(ed.Score);
                                    wb.Worksheets[i].Cells[0, 7].PutValue("班排名");
                                    wb.Worksheets[i].Cells[row, 7].PutValue(ed.ClassScoreRank);
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
                wb.Worksheets[0].AutoFitRows();
                wb.Worksheets[0].AutoFitColumns();
                 // 分班
                i = 1;
                foreach (ClassRecord cr in ClassRecs)
                {
                    row = 1;
                    wb.Worksheets[i].Name = cr.ClassName;
                    foreach (StudentData sd in StudDatas)
                    {
                        if (cr.ClassName == sd.ClassName)
                        {
                            chkdata1 = false;
                            foreach (ExamData ed in sd.lstStudExamScore)
                            {
                                if (((ed.ClassScoreRank*100) / sd.ClassNum)  > (100 - intPercent.Value))
                                {
                                    wb.Worksheets[i].Cells[0, 5].PutValue("科目名稱");
                                    wb.Worksheets[i].Cells[row, 5].PutValue(ed.SubjectName + LevelChange(ed.SubjectLevel));
                                    wb.Worksheets[i].Cells[0, 6].PutValue("分數");
                                    wb.Worksheets[i].Cells[row, 6].PutValue(ed.Score);
                                    wb.Worksheets[i].Cells[0, 7].PutValue("班排名");
                                    wb.Worksheets[i].Cells[row, 7].PutValue(ed.ClassScoreRank);
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
                    wb.Worksheets[i].AutoFitRows();
                    wb.Worksheets[i].AutoFitColumns();
                    i = i + 1;
                }
                try
                {
                    wb.Save(Application.StartupPath + "\\Reports\\" + cboExam.Text + "評量學業成績比率清單.xls", FileFormatType.Excel2003);
                    System.Diagnostics.Process.Start(Application.StartupPath + "\\Reports\\" + cboExam.Text + "評量學業成績比率清單.xls");
                }
                catch
                {
                    System.Windows.Forms.SaveFileDialog sd1 = new System.Windows.Forms.SaveFileDialog();
                    sd1.Title = "另存新檔";
                    sd1.FileName = cboExam.Text + "評量學業成績比率清單.xls";
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
        private void Calc_ScoreAndRank()
        {
           
            if (cboExam.Text != "" )
            {                
                StudDatas.Clear();
                
                foreach (StudentRecord StudRec in StudRecs)
                { 
                    StudentData sd = new StudentData();                   
                    sd.StudentID = StudRec.StudentID;
                    sd.StudentNum = StudRec.StudentNumber;
                    sd.ClassName = StudRec.RefClass.ClassName;
                    sd.SeatNo = StudRec.SeatNo;
                    sd.Name = StudRec.StudentName;
                    sd.ClassYear = StudRec.RefClass.GradeYear;                    
                    foreach (CategoryInfo ci in StudRec.StudentCategorys)
                          if (ci.Name=="成績身分")
                            sd.SubCategory= ci.SubCategory;
                    // 加入考試成績 and 計算
                    string abc="";
                    foreach (ExamScoreInfo esi in StudRec.ExamScoreList)
                    {                        
                        abc= esi.ExamName+esi.Subject+esi.SubjectLevel+esi.StudentName ;
                        if (cboExam.Text == esi.ExamName )
                        { 
                            ExamData ed = new ExamData();                            
                            ed.CousreID = esi.CourseID;
                            ed.CourseName = esi.CourseName;                            
                            ed.ExamName = esi.ExamName;
                            ed.SubjectName = esi.Subject;
                            ed.SubjectLevel = esi.SubjectLevel;
                            ed.SchoolYear = esi.SchoolYear;
                            ed.Semester = esi.Semester;
                            ed.Score = esi.ExamScore;                                                                                   
                            Boolean chkData=false;
                            //不重覆加入科目
                            foreach (ExamData est in sd.lstStudExamScore)
                               if ((ed.CousreID+ed.ExamName+ ed.SubjectName + ed.SubjectLevel) == (est.CousreID+est.ExamName+ est.SubjectName + est.SubjectLevel ))
                                   chkData=true;
                            if (!chkData)
                               sd.lstStudExamScore.Add(ed);
                            ed = null; 
                     }                      
                 }
                    StudDatas.Add(sd);
              }
            if ( cboExam.Text != "")
                {
                  // 班科排名
                 Dictionary<string,List<decimal>> tmpSubjRank=new Dictionary<string,List<decimal>>() ;
                 foreach (ClassRecord cr in ClassRecs)
                    {
                        foreach (StudentData sd in StudDatas)
                        {
                            if (cr.ClassName == sd.ClassName)
                            {
                                sd.ClassNum = cr.Students.Count;
                                foreach (ExamData ed in sd.lstStudExamScore)
                                {
                                    if (!tmpSubjRank.ContainsKey(ed.SubjectName + ed.SubjectLevel))
                                        tmpSubjRank.Add(ed.SubjectName + ed.SubjectLevel, new List<decimal>());
                                    tmpSubjRank[ed.SubjectName + ed.SubjectLevel].Add(ed.Score);
                                }
                            }

                        }
                        foreach (string Subj in tmpSubjRank.Keys)
                        {
                            tmpSubjRank[Subj].Sort();
                            tmpSubjRank[Subj].Reverse();
                        }

                        foreach (StudentData sd in StudDatas)
                        {
                            if (cr.ClassName == sd.ClassName)
                                foreach (ExamData ed in sd.lstStudExamScore)
                                    ed.ClassScoreRank = tmpSubjRank[ed.SubjectName + ed.SubjectLevel].IndexOf(ed.Score) + 1;
                        }
                        tmpSubjRank=new Dictionary<string,List<decimal>>();
                    }  
                }
            }
            else
                MessageBox.Show("資料設定不完整!");
        }
        
        private void 評量學業成績比率清單_Load(object sender, EventArgs e)
        {
               intSchoolYear.Value = int.Parse(K12.Data.School.DefaultSchoolYear);
               intSemester.Value = int.Parse(K12.Data.School.DefaultSemester);
                // 放入試別
                foreach (K12.Data.ExamRecord er in K12.Data.Exam.SelectAll())
                {
                    if (!cboExam.Items.Contains(er.Name))
                        cboExam.Items.Add(er.Name);
                }
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
