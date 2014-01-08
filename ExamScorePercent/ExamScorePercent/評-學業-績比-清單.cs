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
using SmartSchool.Customization.Data.StudentExtension;
namespace ExamScorePercent
{
    public partial class 評量學業成績比率清單 : BaseForm
    {
        public 評量學業成績比率清單()
        {
            InitializeComponent();
        }       
        AccessHelper accHelper = new AccessHelper();       
        List<StudentRecord> StudRecs = new List<StudentRecord>();
        List<ClassRecord> ClassRecs = new List<ClassRecord>();
        Dictionary<string, List<StudentData>> StudDatas = new Dictionary<string, List<StudentData>>();
        //string ExName = "";
        private void buttonX2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (intSchoolYear.Value != 0 && intSemester.Value != 0 && cboExam.Text != "" && intPercent.Value!=0 && cboSortKey.Text!="" && cboSubject.Text!="")
            {               
                Calc_ScoreAndRank();
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


                row = 1;
                i = 0;
                //nowCount = 0;
                foreach (ClassRecord cr in ClassRecs)
                {
                    //設定進度Bar
                    //ProgBar1.Value = (nowCount++*5 / ClassCount) +90;

                    if (StudDatas.ContainsKey(cr.ClassName))
                    {
                        foreach (StudentData sd in StudDatas[cr.ClassName])
                        {

                            // 處理總表先
                            wb.Worksheets[i].Name = "總表";
                            if (sd.ClassNum == 0)
                                continue;
                            if (((sd.ScoreRank * 100) / sd.ClassNum) > (100 - intPercent.Value))
                            {
                                wb.Worksheets[i].Cells[0, 5].PutValue("分數");
                                wb.Worksheets[i].Cells[row, 5].PutValue(sd.SemsScore);
                                wb.Worksheets[i].Cells[0, 6].PutValue("排名");
                                wb.Worksheets[i].Cells[row, 6].PutValue(sd.ScoreRank);
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
                // 分班
                i = 1;
                //nowCount = 0;
                foreach (ClassRecord cr in ClassRecs)
                {
                    //設定進度Bar
                    //ProgBar1.Value = (nowCount++*5 / ClassCount) +95;

                    row = 1;
                    
                    if (StudDatas.ContainsKey(cr.ClassName))
                    {
                        foreach (StudentData sd in StudDatas[cr.ClassName])
                        {
                            wb.Worksheets[i].Name = cr.ClassName;
                            if (sd.ClassNum == 0)
                                continue;
                            if (((sd.ScoreRank * 100) / sd.ClassNum) > (100 - intPercent.Value))
                            {
                                wb.Worksheets[i].Cells[0, 5].PutValue("分數");
                                wb.Worksheets[i].Cells[row, 5].PutValue(sd.SemsScore);
                                wb.Worksheets[i].Cells[0, 6].PutValue("排名");
                                wb.Worksheets[i].Cells[row, 6].PutValue(sd.ScoreRank);
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
                        wb.Worksheets[i].AutoFitRows();
                        wb.Worksheets[i].AutoFitColumns();
                        i = i + 1;
                    }

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
           
            //收集選取學生基本資料及考試成績           
            List<string> CountYear = new List<string>();
            List<string> CountDept = new List<string>();
            bool ScoreHave = false;
            StudDatas.Clear();
            foreach (StudentRecord StudRec in StudRecs)
            {
                if (!CountYear.Contains(StudRec.RefClass.GradeYear))
                    CountYear.Add(StudRec.RefClass.GradeYear);
                if (!CountDept.Contains(StudRec.RefClass.Department))
                    CountDept.Add(StudRec.RefClass.Department);
                StudentData sd = new StudentData();
                ScoreHave = false;
                sd.StudentID = StudRec.StudentID;
                sd.StudentNum = StudRec.StudentNumber;
                sd.ClassName = StudRec.RefClass.ClassName;
                sd.SeatNo = StudRec.SeatNo.ToString();
                sd.Name = StudRec.StudentName;
                sd.ClassYear = StudRec.RefClass.GradeYear;                    
                sd.DeptName = StudRec.RefClass.Department;
                foreach (CategoryInfo ci in StudRec.StudentCategorys)
                    if (ci.Name == "成績身分")
                        sd.SubCategory = ci.SubCategory;
                foreach (ExamScoreInfo esi in StudRec.ExamScoreList)                   
                        if (cboSubject.Text == esi.Subject + LevelChange(esi.SubjectLevel) && esi.ExamName==cboExam.Text)
                        {
                            ScoreHave = true;
                            sd.SemsScore=esi.ExamScore;
                        }
                if (ScoreHave==true)
                {
                    if (!StudDatas.ContainsKey(sd.ClassName))
                        StudDatas.Add(sd.ClassName, new List<StudentData>());

                    StudDatas[sd.ClassName].Add(sd);
                }
            }
            //收集排名成績
            if (cboSortKey.Text != "班級")
            {
                StudRecs.Clear();
                ClassRecs = accHelper.ClassHelper.GetAllClass();
                foreach (ClassRecord cr in ClassRecs)
                {
                    if (cboSortKey.Text == "年級")
                        if (CountYear.Contains(cr.GradeYear))
                            StudRecs.AddRange(cr.Students);

                    if (cboSortKey.Text == "科別")
                        if (CountDept.Contains(cr.Department))
                            StudRecs.AddRange(cr.Students);
                }
                accHelper.StudentHelper.FillExamScore(intSchoolYear.Value, intSemester.Value, StudRecs);
            }
                
            Dictionary<string, List<decimal>> tmpScoreRank = new Dictionary<string, List<decimal>>();
            foreach (StudentRecord StudRec in StudRecs)
                foreach (ExamScoreInfo esi in StudRec.ExamScoreList)
                {
                    if (cboSubject.Text == esi.Subject + LevelChange(esi.SubjectLevel) && esi.ExamName == cboExam.Text)
                    {
                        switch (cboSortKey.Text)
                        {
                            case "年級":
                                if (!tmpScoreRank.ContainsKey(StudRec.RefClass.GradeYear))
                                    tmpScoreRank.Add(StudRec.RefClass.GradeYear, new List<decimal>());
                                tmpScoreRank[StudRec.RefClass.GradeYear].Add(esi.ExamScore);
                                break;
                            case "班級":
                                if (!tmpScoreRank.ContainsKey(StudRec.RefClass.ClassName))
                                    tmpScoreRank.Add(StudRec.RefClass.ClassName, new List<decimal>());
                                tmpScoreRank[StudRec.RefClass.ClassName].Add(esi.ExamScore);
                                break;
                            case "科別":
                                if (!tmpScoreRank.ContainsKey(StudRec.RefClass.Department))
                                    tmpScoreRank.Add(StudRec.RefClass.Department, new List<decimal>());
                                tmpScoreRank[StudRec.RefClass.Department].Add(esi.ExamScore);
                                break;
                        }
                    }
                }
                
            //排名
            //nowCount = 0; 
            foreach (string Subj in tmpScoreRank.Keys)
                {
                    tmpScoreRank[Subj].Sort();
                    tmpScoreRank[Subj].Reverse();
                }
            ClassRecs = accHelper.ClassHelper.GetSelectedClass();
            foreach (ClassRecord cr in ClassRecs)
            {
                //設定進度Bar
                //ProgBar1.Value = (nowCount++*30 / ClassCount)+30;
                if (StudDatas.ContainsKey(cr.ClassName))
                {
                foreach (StudentData StudRec in StudDatas[cr.ClassName])
                {
                    switch (cboSortKey.Text)
                        {
                            case "年級":
                                if (tmpScoreRank.ContainsKey(StudRec.ClassYear))
                                {
                                    StudRec.ClassNum = tmpScoreRank[StudRec.ClassYear].Count;
                                    StudRec.ScoreRank = tmpScoreRank[StudRec.ClassYear].IndexOf(StudRec.SemsScore) + 1;
                                }
                                break;
                            case "班級":
                                if (tmpScoreRank.ContainsKey(StudRec.ClassName))
                                {
                                    StudRec.ClassNum = tmpScoreRank[StudRec.ClassName].Count();
                                    StudRec.ScoreRank = tmpScoreRank[StudRec.ClassName].IndexOf(StudRec.SemsScore) + 1;
                                }
                                break;
                            case "科別":
                                if (tmpScoreRank.ContainsKey(StudRec.DeptName))
                                {
                                    StudRec.ClassNum = tmpScoreRank[StudRec.DeptName].Count;
                                    StudRec.ScoreRank = tmpScoreRank[StudRec.DeptName].IndexOf(StudRec.SemsScore) + 1;
                                }
                                break;
                        }                            
                            
                    }
                }              
            }        
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
                cboSortKey.Items.Clear();
                cboSortKey.Items.Add("班級");
                cboSortKey.Items.Add("科別");
                cboSortKey.Items.Add("年級");
                GradeSubj();
           }
        private void GradeSubj()
        {

            // 傳統卡死畫面又讓UI有一點反映的作法在事件的程式裡頭加上 Application.DoEvents();
            Application.DoEvents();

            if (intSchoolYear.Value==0 || intSemester.Value==0 || cboExam.Text=="")
                return;
            List<ClassRecord> ClassRecs = new List<ClassRecord>();
            ClassRecs = accHelper.ClassHelper.GetSelectedClass();
            StudRecs.Clear();
            cboSubject.Items.Clear();
            foreach (ClassRecord cr in ClassRecs)
                StudRecs.AddRange(cr.Students);
            accHelper.StudentHelper.FillExamScore(intSchoolYear.Value, intSemester.Value, StudRecs);
            foreach (StudentRecord sr in StudRecs)
                 foreach (ExamScoreInfo esi in sr.ExamScoreList)                    
                        if (!cboSubject.Items.Contains(esi.Subject + LevelChange(esi.SubjectLevel)))
                            cboSubject.Items.Add(esi.Subject + LevelChange(esi.SubjectLevel));
                    
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

        private void intSchoolYear_ValueChanged(object sender, EventArgs e)
        {
            GradeSubj();
        }

        private void intSemester_ValueChanged(object sender, EventArgs e)
        {
            GradeSubj();
        }

        private void cboExam_SelectedValueChanged(object sender, EventArgs e)
        {
            GradeSubj();
        }

       
        
    }
}
