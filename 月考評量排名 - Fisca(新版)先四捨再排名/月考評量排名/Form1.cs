using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Diagnostics;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FISCA.Presentation.Controls;
using SmartSchool.Customization.Data.StudentExtension;
using SmartSchool.Customization.Data;
using Aspose.Cells;
using Aspose.Words;



namespace 月考評量排名
{
    public partial class frm1 : BaseForm
    {
        public frm1()
        {
            InitializeComponent();
        }

        List<ExamSubj> ExSubj = new List<ExamSubj>();
        AccessHelper accHelper = new AccessHelper();
        List<StudentData> StudDatas = new List<StudentData>();        
        List<StudentRecord> StudRecs = new List<StudentRecord>();
        List<ClassRecord> ClassRecs = new List<ClassRecord>();
        string ExName = "";
        //string ExName1 = "";
        //string tmpClass = "";

        public string NumToChiChar(int nNum)
        {

            const string strChiDeg = "萬億兆";
            const string strChiTDeg = "十百千";                 //也可改為 "拾佰仟"
            const string strChiDigit = "一二三四五六七八九";    //也可改為 "壹貳參肆伍陸柒捌玖"
            const string strChiZero = "○";

            int nDigit;
            int nDeg;
            string strResult;
            Boolean bZeroFlag;

            nDeg = 0;
            strResult = "";
            bZeroFlag = true;
            while (nNum != 0)
            {
                nDigit = nNum % 10;
                nNum = (nNum - nDigit) / 10;

                if (nDigit != 0)
                {
                       
                    if (!bZeroFlag)
                    {
                        strResult = strChiZero + strResult;
                        bZeroFlag =true;
                    }    
                        if (nDeg >= 1)
                            strResult = strChiDigit.Substring( nDigit - 1, 1) + strChiTDeg.Substring( nDeg - 1, 1) + strResult;
                        else
                            if (nDeg >= 4)
                                strResult = strChiDigit.Substring( nDigit - 1, 1) + strChiTDeg.Substring(nDeg - 1, 1) + strChiDeg.Substring( ((nDeg - (nDeg % 4)) / 4 - 1), 1) + strResult;
                            else
                                strResult = strChiDigit.Substring( nDigit - 1, 1) + strResult;                               
                                    
                    
                }
                else                 
                   bZeroFlag = false;
                   
                nDeg = nDeg + 1;
            }

            if (strResult.Length>=2) 
               if (strResult.Substring(0, 2) == strChiDigit.Substring(0, 1) + strChiTDeg.Substring(0, 1))
                   strResult = strResult.Substring(1, strResult.Length- 1);
           if (strResult.Length > 1)
               if (strResult.Substring(strResult.Length - 1, 1) == strChiZero)
                   strResult = strResult.Substring(0, strResult.Length - 1);

            if (strResult == "")
                strResult = strChiZero;

            return strResult;
        }  
        private void frm1_Load(object sender, EventArgs e)
        {

            // 排名種類
            cboSortType.Items.Add("總分排名");
            cboSortType.Items.Add("平均排名");
            cboSortType.Items.Add("加權總分排名");
            cboSortType.Items.Add("加權平均排名");
            cboSortType.Items.Add("科目排名");
            cboSortType.Text = cboSortType.Items[0].ToString();
            // 報表輸出排序種類
            cboReportSortType.Items.Add("班級總分排名");
            cboReportSortType.Items.Add("班級加權總分排名");
            cboReportSortType.Items.Add("班級平均排名");
            cboReportSortType.Items.Add("班級加權平均排名");
            cboReportSortType.Items.Add("總分年排名");
            cboReportSortType.Items.Add("加權總分年排名");
            cboReportSortType.Items.Add("加權平均年排名");
            cboReportSortType.Items.Add("班級加權平均進步排名");
            cboReportSortType.Items.Add("班級加權平均成績進步排名");
            cboReportSortType.Text = cboReportSortType.Items[0].ToString();
            cboSortType.Text = cboSortType.Items[0].ToString();
            ClassRecs = accHelper.ClassHelper.GetSelectedClass();
            lstSubject.CheckBoxes = true;
            string x = "";
            // 加入所選取學生
            foreach (ClassRecord cr in ClassRecs)
                StudRecs.AddRange(cr.Students);
            List<string> chkExamSubj = new List<string>();
            lstSubject.CheckBoxes = true;

            // 加入學生考試成績
            accHelper.StudentHelper.FillExamScore(SmartSchool.Customization.Data.SystemInformation.SchoolYear, SmartSchool.Customization.Data.SystemInformation.Semester, StudRecs);

            // 放入科目
            foreach (StudentRecord StudRec in StudRecs)
            {
                //                    if(StudRec.RefClass != null)                        
                foreach (ExamScoreInfo esi in StudRec.ExamScoreList)
                {
                    x = esi.ExamName + esi.Subject + esi.SubjectLevel;
                    if (!chkExamSubj.Contains(x))
                    {
                        chkExamSubj.Add(x);
                        ExamSubj es1 = new ExamSubj();
                        es1.SubjName = esi.Subject;
                        es1.ExamName = esi.ExamName;
                        es1.SubjLevel = esi.SubjectLevel;
                        ExSubj.Add(es1);
                        es1 = null;
                    }

                }
                //break;
                //    }
                //tmpClass = StudRec.RefClass.ClassName;
            }

            // 放入試別
            foreach (ExamSubj es in ExSubj)
            {
                if (!cboExam.Items.Contains(es.ExamName))
                    cboExam.Items.Add(es.ExamName);
                if (!cboExamlst.Items.Contains(es.ExamName))
                    cboExamlst.Items.Add(es.ExamName);
            }
        }

        private void cboSortType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboSortType.Text == "科目排名")
            {
                lbxSubjct.Visible = true;
                lstSubject.Visible = false;
                chkSelAll.Visible = false;
            }
            else
            {
                lbxSubjct.Visible = false;
                lstSubject.Visible = true;
                chkSelAll.Visible=true;
            }
        }

        private void cboExam_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbxSubjct.Items.Clear();
            lstSubject.Items.Clear();
            chkSelAll.Checked = false;
            if (cboExam.Text!="")
            {
                foreach (ExamSubj es in ExSubj)
                {
                    if (cboExam.Text == es.ExamName)
                        lbxSubjct.Items.Add(es.SubjName + es.SubjLevel);
                }

                foreach (ExamSubj es in ExSubj)
                {
                    if (cboExam.Text == es.ExamName)
                        lstSubject.Items.Add(es.SubjName + es.SubjLevel);
                }
            }
            ExName = cboExam.Text;
            cboExamlst.Text = "";
        }
        private void Calc_ScoreAndRank()
        {
         bool chkBtn1 = true;
            bool chkBtn2 = true;
            if (rb01.Checked==true || rb02.Checked==true)
               if (cboSortType.Text=="" &&  cbClass.Checked == false && cbYear.Checked == false)
                   chkBtn1 = false;
            if (rb05.Checked==true|| rb06.Checked==true || rb04.Checked==true)
                if (cboExamlst.Text=="")
                    chkBtn1=false;
            if (rb01.Checked == false && rb02.Checked == false && rb03.Checked==false  && rb05.Checked==false && rb06.Checked==false && rb04.Checked==false )
                chkBtn2 = false;

            // 加權總分
            decimal SumSubjScoreA = 0;
            // 總分
            decimal SumSubjScore = 0;
            // 學分數加總
            int SumSubjCredits = 0;
            // 科目個數加總
            int SumSubjCots = 0;
            // 前次試別

            // 加權總分
            decimal SumSubj1ScoreA = 0;
            // 總分
            decimal SumSubj1Score = 0;
            // 學分數加總
            int SumSubj1Credits = 0;
            // 科目個數加總
            int SumSubj1Cots = 0;
           

            if (cboExam.Text != "" && chkBtn1 != false && chkBtn2 != false)
            {
                StudDatas.Clear();
                //foreach (ClassRecord cr in ClassRecs )
                //{
                foreach (StudentRecord StudRec in StudRecs)
                {
                    StudentData sd = new StudentData();                   
                    sd.StudentID = StudRec.StudentID;
                    sd.StudentNum = StudRec.StudentNumber;
                    sd.ClassName = StudRec.RefClass.ClassName;
                    sd.SeatNo = StudRec.SeatNo;
                    sd.Name = StudRec.StudentName;
                    sd.ClassYear = StudRec.RefClass.GradeYear;                   
                    // 初始化
                    SumSubjCots = 0;
                    SumSubjCredits = 0;
                    SumSubjScoreA = 0;
                    SumSubjScore = 0;
                    //
                    SumSubj1Cots = 0;
                    SumSubj1Credits = 0;
                    SumSubj1ScoreA = 0;
                    SumSubj1Score = 0;
                    sd.AvgScore = -1;
                    sd.AvgScoreA = -1;
                    sd.ClassAvgRank = 0;
                    sd.ClassAvgRankA = 0;
                    sd.ClassSumRank = 0;
                    sd.ClassSumRankA = 0;
                    sd.SumScore = -1;
                    sd.SumScoreA = -1;
                    sd.AvgScore1 = -1;
                    sd.AvgScoreA1= -1;
                    sd.ClassAvgRank1 = 0;
                    sd.ClassAvgRankA1 = 0;
                    sd.ClassSumRank1 = 0;
                    sd.ClassSumRankA1 = 0;
                    sd.SumScore1 = -1;
                    sd.SumScoreA1 = -1;
                    string abc;
                    // 加入考試成績 and 計算
                    foreach (ExamScoreInfo esi in StudRec.ExamScoreList)
                    {
                        abc= esi.ExamName+esi.Subject+esi.SubjectLevel+esi.StudentName ;
                        if (cboExam.Text == esi.ExamName || cboExamlst.Text==esi.ExamName)
                        {
                            if ((rb01.Checked==true || rb02.Checked==true ) && cboSortType.Text == "科目排名")
                            {

                                if ((esi.Subject + esi.SubjectLevel) == lbxSubjct.Text)
                                    {

                                        ExamData ed = new ExamData();
                                        ed.CousreID = esi.CourseID;
                                        ed.CourseName = esi.CourseName;
                                        ed.Credit = esi.Credit;
                                        ed.ExamName = esi.ExamName;
                                        ed.SubjectName = esi.Subject;
                                        ed.SubjectLevel = esi.SubjectLevel;
                                        ed.SchoolYear = esi.SchoolYear;
                                        ed.Semester = esi.Semester;
                                        ed.Score = esi.ExamScore;
                                        //判斷科目是否重覆，未重覆再加入
                                        Boolean chkData=false;
                                        foreach (ExamData est in sd.lstStudExamScore)
                                            if ((ed.CousreID+ed.ExamName+ ed.SubjectName + ed.SubjectLevel) == (est.CousreID+est.ExamName+ est.SubjectName + est.SubjectLevel ))
                                                chkData=true;
                                        if (!chkData)
                                        {
                                            sd.lstStudExamScore.Add(ed);
                                            SumSubjScoreA += esi.ExamScore * esi.Credit;
                                            SumSubjScore += esi.ExamScore;
                                            SumSubjCredits += esi.Credit;
                                            SumSubjCots++;
                                        }
                                        ed = null;

                                    }
                                
                            }
                            else
                            {
                                foreach (ListViewItem selSubject in lstSubject.CheckedItems)
                                {
                                    if ((esi.Subject + esi.SubjectLevel) == selSubject.Text)
                                    {

                                        ExamData ed = new ExamData();
                                        ed.CousreID = esi.CourseID;
                                        ed.CourseName = esi.CourseName;
                                        ed.Credit = esi.Credit;
                                        ed.ExamName = esi.ExamName;
                                        ed.SubjectName = esi.Subject;
                                        ed.SubjectLevel = esi.SubjectLevel;
                                        ed.SchoolYear = esi.SchoolYear;
                                        ed.Semester = esi.Semester;
                                        ed.Score = esi.ExamScore;
                                        //判斷科目是否重覆，未重覆再加入
                                        Boolean chkData = false;
                                        foreach (ExamData est in sd.lstStudExamScore)
                                            if ((ed.CousreID + ed.ExamName + ed.SubjectName + ed.SubjectLevel) == (est.CousreID + est.ExamName + est.SubjectName + est.SubjectLevel))
                                                chkData = true;
                                        if (!chkData)
                                        {
                                            if (cboExamlst.Text == esi.ExamName)
                                            {
                                                sd.lstStudExamScoreOld.Add(ed);
                                                SumSubj1ScoreA += esi.ExamScore * esi.Credit;
                                                SumSubj1Score += esi.ExamScore;
                                                SumSubj1Credits += esi.Credit;
                                                SumSubj1Cots++;
                                            }
                                            if (cboExam.Text == esi.ExamName)
                                            {
                                                sd.lstStudExamScore.Add(ed);
                                                SumSubjScoreA += esi.ExamScore * esi.Credit;
                                                SumSubjScore += esi.ExamScore;
                                                SumSubjCredits += esi.Credit;
                                                SumSubjCots++;
                                            }
                                        }
                                        ed = null;

                                    }
                                }
                            }
                        }
                    }

                    // 有成績才計算
                    if (SumSubjCots > 0)
                    {
                        sd.SumScore = SumSubjScore;
                        sd.SumScoreA = SumSubjScoreA;                       
                        sd.AvgScore = Math.Round(SumSubjScore / SumSubjCots,2,MidpointRounding.AwayFromZero);
                        sd.AvgScoreA = Math.Round(SumSubjScoreA / SumSubjCredits,2,MidpointRounding.AwayFromZero);
                    }
                    // 有成績才計算
                    if (SumSubj1Cots > 0)
                    {
                        sd.SumScore1 = SumSubj1Score;
                        sd.SumScoreA1 = SumSubj1ScoreA;
                        sd.AvgScore1 = Math.Round(SumSubj1Score / SumSubj1Cots,2,MidpointRounding.AwayFromZero);
                        sd.AvgScoreA1 = Math.Round(SumSubj1ScoreA / SumSubj1Credits,2,MidpointRounding.AwayFromZero);
                    }                    
                    StudDatas.Add(sd);
                } // StudRec

                // 班排名
                List<decimal> tmpSumRank = new List<decimal>();
                List<decimal> tmpSumRankA = new List<decimal>();
                List<decimal> tmpAvgRank = new List<decimal>();
                List<decimal> tmpAvgRankA = new List<decimal>();
                foreach (ClassRecord cr in ClassRecs)
                {
                    foreach (StudentData sd in StudDatas)
                        if (cr.ClassName == sd.ClassName)
                        {
                            tmpSumRank.Add(sd.SumScore);
                            tmpSumRankA.Add(sd.SumScoreA);
                            tmpAvgRank.Add(sd.AvgScore);
                            tmpAvgRankA.Add(sd.AvgScoreA);
                        }
                    tmpAvgRank.Sort();
                    tmpAvgRank.Reverse();
                    tmpAvgRankA.Sort();
                    tmpAvgRankA.Reverse();
                    tmpSumRank.Sort();
                    tmpSumRank.Reverse();
                    tmpSumRankA.Sort();
                    tmpSumRankA.Reverse();

                    // 放入排名
                    foreach (StudentData sd in StudDatas)
                        if (cr.ClassName == sd.ClassName)
                        {
                            sd.ClassSumRank = tmpSumRank.IndexOf(sd.SumScore) + 1;
                            sd.ClassSumRankA = tmpSumRankA.IndexOf(sd.SumScoreA) + 1;
                            sd.ClassAvgRank = tmpAvgRank.IndexOf(sd.AvgScore) + 1;
                            sd.ClassAvgRankA = tmpAvgRankA.IndexOf(sd.AvgScoreA) + 1;
                        }
                    tmpAvgRank.Clear();
                    tmpAvgRankA.Clear();
                    tmpSumRank.Clear();
                    tmpSumRankA.Clear();
                }

                // 年排名(select)





                foreach (StudentData sd in StudDatas)
                {
                    tmpSumRank.Add(sd.SumScore);
                    tmpSumRankA.Add(sd.SumScoreA);
                    tmpAvgRank.Add(sd.AvgScore);
                    tmpAvgRankA.Add(sd.AvgScoreA);
                }
                tmpAvgRank.Sort();
                tmpAvgRank.Reverse();
                tmpAvgRankA.Sort();
                tmpAvgRankA.Reverse();
                tmpSumRank.Sort();
                tmpSumRank.Reverse();
                tmpSumRankA.Sort();
                tmpSumRankA.Reverse();

                // 放入排名
                foreach (StudentData sd in StudDatas)
                {
                    sd.YearSumRank = tmpSumRank.IndexOf(sd.SumScore) + 1;
                    sd.YearSumRankA = tmpSumRankA.IndexOf(sd.SumScoreA) + 1;
                    sd.YearAvgRank = tmpAvgRank.IndexOf(sd.AvgScore) + 1;
                    sd.YearAvgRankA = tmpAvgRankA.IndexOf(sd.AvgScoreA) + 1;
                }
                tmpAvgRank.Clear();
                tmpAvgRankA.Clear();
                tmpSumRank.Clear();
                tmpSumRankA.Clear();
                //計算前次試別排名
                if (rb05.Checked == true || rb04.Checked==true)
                {
                    List<decimal> tmpAvgRankAD = new List<decimal>();
                    List<decimal> tmpAvgRankAS = new List<decimal>();
                    //班排名
                    foreach (ClassRecord cr in ClassRecs)
                    {
                        foreach (StudentData sd in StudDatas)
                            if (cr.ClassName == sd.ClassName)
                            {
                                tmpSumRank.Add(sd.SumScore1);
                                tmpSumRankA.Add(sd.SumScoreA1);
                                tmpAvgRank.Add(sd.AvgScore1);
                                tmpAvgRankA.Add(sd.AvgScoreA1);                                
                            }
                        tmpAvgRank.Sort();
                        tmpAvgRank.Reverse();
                        tmpAvgRankA.Sort();
                        tmpAvgRankA.Reverse();
                        tmpSumRank.Sort();
                        tmpSumRank.Reverse();
                        tmpSumRankA.Sort();
                        tmpSumRankA.Reverse();

                        // 放入排名
                        foreach (StudentData sd in StudDatas)
                            if (cr.ClassName == sd.ClassName)
                            {
                                sd.ClassSumRank1 = tmpSumRank.IndexOf(sd.SumScore1) + 1;
                                sd.ClassSumRankA1 = tmpSumRankA.IndexOf(sd.SumScoreA1) + 1;
                                sd.ClassAvgRank1 = tmpAvgRank.IndexOf(sd.AvgScore1) + 1;
                                sd.ClassAvgRankA1 = tmpAvgRankA.IndexOf(sd.AvgScoreA1) + 1;
                                if (sd.AvgScoreA != -1 && sd.AvgScoreA1 != -1)
                                {
                                    tmpAvgRankAD.Add(sd.ClassAvgRankA1 - sd.ClassAvgRankA);
                                    tmpAvgRankAS.Add(sd.AvgScoreA - sd.AvgScoreA1);
                                } 
                            }
                        tmpAvgRankAD.Sort();
                        tmpAvgRankAD.Reverse();
                        tmpAvgRankAS.Sort();
                        tmpAvgRankAS.Reverse();
                        foreach (StudentData sd in StudDatas)
                            if (cr.ClassName == sd.ClassName)
                            {
                                if (sd.AvgScoreA != -1 && sd.AvgScoreA1 != -1)
                                {
                                    sd.ClassAvgRankAD = tmpAvgRankAD.IndexOf(sd.ClassAvgRankA1 - sd.ClassAvgRankA) + 1;
                                    sd.ClassAvgRankAS = tmpAvgRankAS.IndexOf(sd.AvgScoreA - sd.AvgScoreA1) + 1;
                                }
                                else
                                {
                                    sd.ClassAvgRankAD = -99;
                                    sd.ClassAvgRankAS = -99;
                                }
                            }
                        tmpAvgRankAD.Clear();
                        tmpAvgRank.Clear();
                        tmpAvgRankA.Clear();
                        tmpSumRank.Clear();
                        tmpSumRankA.Clear();
                        tmpAvgRankAS.Clear();
                    }

                    // 年排名(select)





                    foreach (StudentData sd in StudDatas)
                    {
                        tmpSumRank.Add(sd.SumScore1);
                        tmpSumRankA.Add(sd.SumScoreA1);
                        tmpAvgRank.Add(sd.AvgScore1);
                        tmpAvgRankA.Add(sd.AvgScoreA1);
                    }
                    tmpAvgRank.Sort();
                    tmpAvgRank.Reverse();
                    tmpAvgRankA.Sort();
                    tmpAvgRankA.Reverse();
                    tmpSumRank.Sort();
                    tmpSumRank.Reverse();
                    tmpSumRankA.Sort();
                    tmpSumRankA.Reverse();

                    // 放入排名
                    foreach (StudentData sd in StudDatas)
                    {
                        sd.YearSumRank1 = tmpSumRank.IndexOf(sd.SumScore) + 1;
                        sd.YearSumRankA1 = tmpSumRankA.IndexOf(sd.SumScoreA) + 1;
                        sd.YearAvgRank1 = tmpAvgRank.IndexOf(sd.AvgScore) + 1;
                        sd.YearAvgRankA1 = tmpAvgRankA.IndexOf(sd.AvgScoreA) + 1;
                    }
                    tmpAvgRank.Clear();
                    tmpAvgRankA.Clear();
                    tmpSumRank.Clear();
                    tmpSumRankA.Clear();
                }



                if (lbxSubjct.Visible == true && cboExam.Text != "")
                {
                    // 班科排名
                    List<Decimal> tmpSubjRank = new List<decimal>();
                    foreach (ClassRecord cr in ClassRecs)
                    {
                        foreach (StudentData sd in StudDatas)
                        {

                            if (cr.ClassName == sd.ClassName)
                                foreach (ExamData ed in sd.lstStudExamScore)
                                    if ((ed.SubjectName + ed.SubjectLevel) == lbxSubjct.Text && (ed.ExamName == cboExam.Text))
                                        tmpSubjRank.Add(ed.Score);

                        }
                        tmpSubjRank.Sort();
                        tmpSubjRank.Reverse();

                        foreach (StudentData sd in StudDatas)
                        {
                            if (cr.ClassName == sd.ClassName)
                                foreach (ExamData ed in sd.lstStudExamScore)
                                    if ((ed.SubjectName + ed.SubjectLevel) == lbxSubjct.Text && (ed.ExamName == cboExam.Text))
                                        ed.ClassScoreRank = tmpSubjRank.IndexOf(ed.Score) + 1;
                        }
                        tmpSubjRank.Clear();
                    }

                    // 年科排名
                    foreach (StudentData sd in StudDatas)
                    {
                        foreach (ExamData ed in sd.lstStudExamScore)
                            if ((ed.SubjectName + ed.SubjectLevel) == lbxSubjct.Text && (ed.ExamName == cboExam.Text))
                                tmpSubjRank.Add(ed.Score);

                    }
                    tmpSubjRank.Sort();
                    tmpSubjRank.Reverse();

                    foreach (StudentData sd in StudDatas)
                    {
                        foreach (ExamData ed in sd.lstStudExamScore)
                            if ((ed.SubjectName + ed.SubjectLevel) == lbxSubjct.Text && (ed.ExamName == cboExam.Text))
                                ed.YearScoreRank = tmpSubjRank.IndexOf(ed.Score) + 1;
                    }
                    tmpSubjRank.Clear();


                }
        }
            else
                MessageBox.Show("排名方式或排名名次設定不完整!");
        }
        private void btnToX1s_Click(object sender, EventArgs e)
        {
            Calc_ScoreAndRank();
            if (cboReportSortType.Text!="" || cboReportSortType.Text!=null)
                StudDatas.Sort(CompareCode);
            Workbook wb = new Workbook();   
            int wksheets = 0, row = 0,  i = 0;

            wksheets = ClassRecs.Count;

            for (i = 0; i < wksheets; i++)
                wb.Worksheets.Add();

            bool chkdata1 = false;
            // 選全部+班排
            if (rb01.Checked == true && lbxSubjct.Visible == false)
                {
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

                            if (cbClass.Checked == true && cbYear.Checked == false && cboSortType.Text == "總分排名" && sd.SumScore != -1)
                            {

                                wb.Worksheets[i].Cells[0, 4].PutValue("總分");
                                wb.Worksheets[i].Cells[row, 4].PutValue(sd.SumScore);
                                wb.Worksheets[i].Cells[0, 5].PutValue("總分班排名");
                                wb.Worksheets[i].Cells[row, 5].PutValue(sd.ClassSumRank);
                                chkdata1 = true;
                            }

                            if (cbClass.Checked == true && cbYear.Checked == false && cboSortType.Text == "平均排名" && sd.AvgScore != -1)
                            {
                                wb.Worksheets[i].Cells[0, 4].PutValue("平均");
                                wb.Worksheets[i].Cells[row, 4].PutValue(sd.AvgScore);
                                wb.Worksheets[i].Cells[0, 5].PutValue("平均班排名");
                                wb.Worksheets[i].Cells[row, 5].PutValue(sd.ClassAvgRank);
                                chkdata1 = true;
                            }

                            if (cbClass.Checked == true && cbYear.Checked == false && cboSortType.Text == "加權總分排名" && sd.SumScoreA != -1)
                            {

                                wb.Worksheets[i].Cells[0, 4].PutValue("加權總分");
                                wb.Worksheets[i].Cells[row, 4].PutValue(sd.SumScoreA);
                                wb.Worksheets[i].Cells[0, 5].PutValue("加權總分班排名");
                                wb.Worksheets[i].Cells[row, 5].PutValue(sd.ClassSumRankA);
                                chkdata1 = true;
                            }

                            if (cbClass.Checked == true && cbYear.Checked == false && cboSortType.Text == "加權平均排名" && sd.AvgScoreA != -1)
                            {
                                wb.Worksheets[i].Cells[0, 4].PutValue("加權平均");
                                wb.Worksheets[i].Cells[row, 4].PutValue(sd.AvgScoreA);
                                wb.Worksheets[i].Cells[0, 5].PutValue("加權平均班排名");
                                wb.Worksheets[i].Cells[row, 5].PutValue(sd.ClassAvgRankA);
                                chkdata1 = true;
                            }

                            if (cbClass.Checked == false && cbYear.Checked == true && cboSortType.Text == "總分排名" && sd.SumScore != -1)
                            {
                                wb.Worksheets[i].Cells[0, 4].PutValue("總分");
                                wb.Worksheets[i].Cells[row, 4].PutValue(sd.SumScore);
                                wb.Worksheets[i].Cells[0, 5].PutValue("總分年排名");
                                wb.Worksheets[i].Cells[row, 5].PutValue(sd.YearSumRank);
                                chkdata1 = true;
                            }

                            if (cbClass.Checked == false && cbYear.Checked == true && cboSortType.Text == "平均排名" && sd.AvgScore != -1)
                            {
                                wb.Worksheets[i].Cells[0, 4].PutValue("平均");
                                wb.Worksheets[i].Cells[row, 4].PutValue(sd.AvgScore);
                                wb.Worksheets[i].Cells[0, 5].PutValue("平均年排名");
                                wb.Worksheets[i].Cells[row, 5].PutValue(sd.YearAvgRank);
                                chkdata1 = true;
                            }

                            if (cbClass.Checked == false && cbYear.Checked == true && cboSortType.Text == "加權總分排名" && sd.SumScoreA != -1)
                            {
                                wb.Worksheets[i].Cells[0, 4].PutValue("加權總分");
                                wb.Worksheets[i].Cells[row, 4].PutValue(sd.SumScoreA);
                                wb.Worksheets[i].Cells[0, 5].PutValue("加權總分年排名");
                                wb.Worksheets[i].Cells[row, 5].PutValue(sd.YearSumRankA);
                                chkdata1 = true;
                            }

                            if (cbClass.Checked == false && cbYear.Checked == true && cboSortType.Text == "加權平均排名" && sd.AvgScoreA != -1)
                            {
                                wb.Worksheets[i].Cells[0, 4].PutValue("加權平均");
                                wb.Worksheets[i].Cells[row, 4].PutValue(sd.AvgScoreA);
                                wb.Worksheets[i].Cells[0, 5].PutValue("加權平均年排名");
                                wb.Worksheets[i].Cells[row, 5].PutValue(sd.YearAvgRankA);
                                chkdata1 = true;
                            }


                            if (cbClass.Checked == true && cbYear.Checked == true && cboSortType.Text == "總分排名" && sd.SumScore != -1)
                            {
                                wb.Worksheets[i].Cells[0, 4].PutValue("總分");
                                wb.Worksheets[i].Cells[row, 4].PutValue(sd.SumScore);
                                wb.Worksheets[i].Cells[0, 5].PutValue("總分班排名");
                                wb.Worksheets[i].Cells[row, 5].PutValue(sd.ClassSumRank);
                                wb.Worksheets[i].Cells[0, 6].PutValue("總分年排名");
                                wb.Worksheets[i].Cells[row, 6].PutValue(sd.YearSumRank);
                                chkdata1 = true;
                            }

                            if (cbClass.Checked == true && cbYear.Checked == true && cboSortType.Text == "平均排名" && sd.AvgScore != -1)
                            {
                                wb.Worksheets[i].Cells[0, 4].PutValue("平均");
                                wb.Worksheets[i].Cells[row, 4].PutValue(sd.AvgScore);
                                wb.Worksheets[i].Cells[0, 5].PutValue("平均班排名");
                                wb.Worksheets[i].Cells[row, 5].PutValue(sd.ClassAvgRank);
                                wb.Worksheets[i].Cells[0, 6].PutValue("平均年排名");
                                wb.Worksheets[i].Cells[row, 6].PutValue(sd.YearAvgRank);
                                chkdata1 = true;
                            }

                            if (cbClass.Checked == true && cbYear.Checked == true && cboSortType.Text == "加權總分排名" && sd.SumScoreA != -1)
                            {
                                wb.Worksheets[i].Cells[0, 4].PutValue("加權總分");
                                wb.Worksheets[i].Cells[row, 4].PutValue(sd.SumScoreA);
                                wb.Worksheets[i].Cells[0, 5].PutValue("加權總分班排名");
                                wb.Worksheets[i].Cells[row, 5].PutValue(sd.ClassSumRankA);
                                wb.Worksheets[i].Cells[0, 6].PutValue("加權總分年排名");
                                wb.Worksheets[i].Cells[row, 6].PutValue(sd.YearSumRankA);

                                chkdata1 = true;
                            }

                            if (cbClass.Checked == true && cbYear.Checked == true && cboSortType.Text == "加權平均排名" && sd.AvgScoreA != -1)
                            {
                                wb.Worksheets[i].Cells[0, 4].PutValue("加權平均");
                                wb.Worksheets[i].Cells[row, 4].PutValue(sd.AvgScoreA);
                                wb.Worksheets[i].Cells[0, 5].PutValue("加權平均班排名");
                                wb.Worksheets[i].Cells[row, 5].PutValue(sd.ClassAvgRankA);
                                wb.Worksheets[i].Cells[0, 6].PutValue("加權平均年排名");
                                wb.Worksheets[i].Cells[row, 6].PutValue(sd.YearAvgRankA);
                                chkdata1 = true;
                            }

                            if (chkdata1 == true)
                            {
                                wb.Worksheets[i].Cells[0, 0].PutValue("學號");
                                wb.Worksheets[i].Cells[0, 1].PutValue("班級");
                                wb.Worksheets[i].Cells[0, 2].PutValue("座號");
                                wb.Worksheets[i].Cells[0, 3].PutValue("姓名");
                                wb.Worksheets[i].Cells[row, 0].PutValue(sd.StudentNum);
                                wb.Worksheets[i].Cells[row, 1].PutValue(sd.ClassName);
                                wb.Worksheets[i].Cells[row, 2].PutValue(sd.SeatNo);
                                wb.Worksheets[i].Cells[row, 3].PutValue(sd.Name);
                                row++;
                            }

                        }
                    }
                    }



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

                                if (cbClass.Checked == true && cbYear.Checked == false && cboSortType.Text == "總分排名" && sd.SumScore != -1)
                                {
                                    wb.Worksheets[i].Cells[0, 4].PutValue("總分");
                                    wb.Worksheets[i].Cells[row, 4].PutValue(sd.SumScore);
                                    wb.Worksheets[i].Cells[0, 5].PutValue("總分班排名");
                                    wb.Worksheets[i].Cells[row, 5].PutValue(sd.ClassSumRank);
                                    chkdata1 = true;
                                }

                                if (cbClass.Checked == true && cbYear.Checked == false && cboSortType.Text == "平均排名" && sd.AvgScore != -1)
                                {
                                    wb.Worksheets[i].Cells[0, 4].PutValue("平均");
                                    wb.Worksheets[i].Cells[row, 4].PutValue(Math.Round(sd.AvgScore, 2, MidpointRounding.AwayFromZero));
                                    wb.Worksheets[i].Cells[0, 5].PutValue("平均班排名");
                                    wb.Worksheets[i].Cells[row, 5].PutValue(sd.ClassAvgRank);
                                    chkdata1 = true;
                                }

                                if (cbClass.Checked == true && cbYear.Checked == false && cboSortType.Text == "加權總分排名" && sd.SumScoreA != -1)
                                {
                                    wb.Worksheets[i].Cells[0, 4].PutValue("加權總分");
                                    wb.Worksheets[i].Cells[row, 4].PutValue(sd.SumScoreA);
                                    wb.Worksheets[i].Cells[0, 5].PutValue("加權總分班排名");
                                    wb.Worksheets[i].Cells[row, 5].PutValue(sd.ClassSumRankA);
                                    chkdata1 = true;
                                }

                                if (cbClass.Checked == true && cbYear.Checked == false && cboSortType.Text == "加權平均排名" && sd.AvgScoreA != -1)
                                {
                                    wb.Worksheets[i].Cells[0, 4].PutValue("加權平均");
                                    wb.Worksheets[i].Cells[row, 4].PutValue(sd.AvgScoreA);
                                    wb.Worksheets[i].Cells[0, 5].PutValue("加權平均班排名");
                                    wb.Worksheets[i].Cells[row, 5].PutValue(sd.ClassAvgRankA);
                                    chkdata1 = true;
                                }

                                if (cbClass.Checked == false && cbYear.Checked == true && cboSortType.Text == "總分排名" && sd.SumScore != -1)
                                {
                                    wb.Worksheets[i].Cells[0, 4].PutValue("總分");
                                    wb.Worksheets[i].Cells[row, 4].PutValue(sd.SumScore);
                                    wb.Worksheets[i].Cells[0, 5].PutValue("總分年排名");
                                    wb.Worksheets[i].Cells[row, 5].PutValue(sd.YearSumRank);
                                    chkdata1 = true;
                                }

                                if (cbClass.Checked == false && cbYear.Checked == true && cboSortType.Text == "平均排名" && sd.AvgScore != -1)
                                {
                                    wb.Worksheets[i].Cells[0, 4].PutValue("平均");
                                    wb.Worksheets[i].Cells[row, 4].PutValue(sd.AvgScore);
                                    wb.Worksheets[i].Cells[0, 5].PutValue("平均年排名");
                                    wb.Worksheets[i].Cells[row, 5].PutValue(sd.YearAvgRank);
                                    chkdata1 = true;
                                }

                                if (cbClass.Checked == false && cbYear.Checked == true && cboSortType.Text == "加權總分排名" && sd.SumScoreA != -1)
                                {
                                    wb.Worksheets[i].Cells[0, 4].PutValue("加權總分");
                                    wb.Worksheets[i].Cells[row, 4].PutValue(sd.SumScoreA);
                                    wb.Worksheets[i].Cells[0, 5].PutValue("加權總分年排名");
                                    wb.Worksheets[i].Cells[row, 5].PutValue(sd.YearSumRankA);
                                    chkdata1 = true;
                                }

                                if (cbClass.Checked == false && cbYear.Checked == true && cboSortType.Text == "加權平均排名" && sd.AvgScoreA != -1)
                                {
                                    wb.Worksheets[i].Cells[0, 4].PutValue("加權平均");
                                    wb.Worksheets[i].Cells[row, 4].PutValue(sd.AvgScoreA);
                                    wb.Worksheets[i].Cells[0, 5].PutValue("加權平均年排名");
                                    wb.Worksheets[i].Cells[row, 5].PutValue(sd.YearAvgRankA);
                                    chkdata1 = true;
                                }


                                if (cbClass.Checked == true && cbYear.Checked == true && cboSortType.Text == "總分排名" && sd.SumScore != -1)
                                {
                                    wb.Worksheets[i].Cells[0, 4].PutValue("總分");
                                    wb.Worksheets[i].Cells[row, 4].PutValue(sd.SumScore);
                                    wb.Worksheets[i].Cells[0, 5].PutValue("總分班排名");
                                    wb.Worksheets[i].Cells[row, 5].PutValue(sd.ClassSumRank);
                                    wb.Worksheets[i].Cells[0, 6].PutValue("總分年排名");
                                    wb.Worksheets[i].Cells[row, 6].PutValue(sd.YearSumRank);
                                    chkdata1 = true;
                                }

                                if (cbClass.Checked == true && cbYear.Checked == true && cboSortType.Text == "平均排名" && sd.AvgScore != -1)
                                {
                                    wb.Worksheets[i].Cells[0, 4].PutValue("平均");
                                    wb.Worksheets[i].Cells[row, 4].PutValue(sd.AvgScore);
                                    wb.Worksheets[i].Cells[0, 5].PutValue("平均班排名");
                                    wb.Worksheets[i].Cells[row, 5].PutValue(sd.ClassAvgRank);
                                    wb.Worksheets[i].Cells[0, 6].PutValue("平均年排名");
                                    wb.Worksheets[i].Cells[row, 6].PutValue(sd.YearAvgRank);
                                    chkdata1 = true;
                                }

                                if (cbClass.Checked == true && cbYear.Checked == true && cboSortType.Text == "加權總分排名" && sd.SumScoreA != -1)
                                {
                                    wb.Worksheets[i].Cells[0, 4].PutValue("加權總分");
                                    wb.Worksheets[i].Cells[row, 4].PutValue(sd.SumScoreA);
                                    wb.Worksheets[i].Cells[0, 5].PutValue("加權總分班排名");
                                    wb.Worksheets[i].Cells[row, 5].PutValue(sd.ClassSumRankA);
                                    wb.Worksheets[i].Cells[0, 6].PutValue("加權總分年排名");
                                    wb.Worksheets[i].Cells[row, 6].PutValue(sd.YearSumRankA);

                                    chkdata1 = true;
                                }

                                if (cbClass.Checked == true && cbYear.Checked == true && cboSortType.Text == "加權平均排名" && sd.AvgScoreA != -1)
                                {
                                    wb.Worksheets[i].Cells[0, 4].PutValue("加權平均");
                                    wb.Worksheets[i].Cells[row, 4].PutValue(sd.AvgScoreA);
                                    wb.Worksheets[i].Cells[0, 5].PutValue("加權平均班排名");
                                    wb.Worksheets[i].Cells[row, 5].PutValue(sd.ClassAvgRankA);
                                    wb.Worksheets[i].Cells[0, 6].PutValue("加權平均年排名");
                                    wb.Worksheets[i].Cells[row, 6].PutValue(sd.YearAvgRankA);
                                    chkdata1 = true;
                                }

                                if (chkdata1 == true)
                                {
                                    wb.Worksheets[i].Cells[0, 0].PutValue("學號");
                                    wb.Worksheets[i].Cells[0, 1].PutValue("班級");
                                    wb.Worksheets[i].Cells[0, 2].PutValue("座號");
                                    wb.Worksheets[i].Cells[0, 3].PutValue("姓名");
                                    wb.Worksheets[i].Cells[row, 0].PutValue(sd.StudentNum);
                                    wb.Worksheets[i].Cells[row, 1].PutValue(sd.ClassName);
                                    wb.Worksheets[i].Cells[row, 2].PutValue(sd.SeatNo);
                                    wb.Worksheets[i].Cells[row, 3].PutValue(sd.Name);
                                    row++;
                                }


                            }
                        }
                        i++;
                    }
                } //if


                // ---
                int rankX = 0;
                int.TryParse(txtBox01.Text, out rankX);
                int ScoreX = 0;
                int.TryParse(txtBox02.Text, out ScoreX);
                int ScoreX2 = 0;
                int.TryParse(txtBox03.Text, out ScoreX2);
                // 選全部+班排(前幾名)
                if (rb02.Checked == true && lbxSubjct.Visible == false && rankX > 0 && txtBox01.Text.Trim() != "")
                {

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

                                if (cbClass.Checked == true && cbYear.Checked == false && cboSortType.Text == "總分排名" && sd.ClassSumRank <= rankX && sd.SumScore != -1)
                                {
                                    wb.Worksheets[i].Cells[0, 4].PutValue("總分");
                                    wb.Worksheets[i].Cells[row, 4].PutValue(sd.SumScore);
                                    wb.Worksheets[i].Cells[0, 5].PutValue("總分班排名");
                                    wb.Worksheets[i].Cells[row, 5].PutValue(sd.ClassSumRank);
                                    chkdata1 = true;
                                }

                                if (cbClass.Checked == true && cbYear.Checked == false && cboSortType.Text == "平均排名" && sd.ClassAvgRank <= rankX && sd.AvgScore != -1)
                                {
                                    wb.Worksheets[i].Cells[0, 4].PutValue("平均");
                                    wb.Worksheets[i].Cells[row, 4].PutValue(sd.AvgScore);
                                    wb.Worksheets[i].Cells[0, 5].PutValue("平均班排名");
                                    wb.Worksheets[i].Cells[row, 5].PutValue(sd.ClassAvgRank);
                                    chkdata1 = true;
                                }

                                if (cbClass.Checked == true && cbYear.Checked == false && cboSortType.Text == "加權總分排名" && sd.ClassSumRankA <= rankX && sd.SumScoreA != -1)
                                {
                                    wb.Worksheets[i].Cells[0, 4].PutValue("加權總分");
                                    wb.Worksheets[i].Cells[row, 4].PutValue(sd.SumScoreA);
                                    wb.Worksheets[i].Cells[0, 5].PutValue("加權總分班排名");
                                    wb.Worksheets[i].Cells[row, 5].PutValue(sd.ClassSumRankA);
                                    chkdata1 = true;
                                }

                                if (cbClass.Checked == true && cbYear.Checked == false && cboSortType.Text == "加權平均排名" && sd.ClassAvgRankA <= rankX && sd.AvgScoreA != -1)
                                {
                                    wb.Worksheets[i].Cells[0, 4].PutValue("加權平均");
                                    wb.Worksheets[i].Cells[row, 4].PutValue(sd.AvgScoreA);
                                    wb.Worksheets[i].Cells[0, 5].PutValue("加權平均班排名");
                                    wb.Worksheets[i].Cells[row, 5].PutValue(sd.ClassAvgRankA);
                                    chkdata1 = true;
                                }

                                if (cbClass.Checked == false && cbYear.Checked == true && cboSortType.Text == "總分排名" && sd.YearSumRank <= rankX && sd.SumScore != -1)
                                {
                                    wb.Worksheets[i].Cells[0, 4].PutValue("總分");
                                    wb.Worksheets[i].Cells[row, 4].PutValue(sd.SumScore);
                                    wb.Worksheets[i].Cells[0, 5].PutValue("總分年排名");
                                    wb.Worksheets[i].Cells[row, 5].PutValue(sd.YearSumRank);
                                    chkdata1 = true;
                                }

                                if (cbClass.Checked == false && cbYear.Checked == true && cboSortType.Text == "平均排名" && sd.YearAvgRank <= rankX && sd.AvgScore != -1)
                                {
                                    wb.Worksheets[i].Cells[0, 4].PutValue("平均");
                                    wb.Worksheets[i].Cells[row, 4].PutValue(sd.AvgScore);
                                    wb.Worksheets[i].Cells[0, 5].PutValue("平均年排名");
                                    wb.Worksheets[i].Cells[row, 5].PutValue(sd.YearAvgRank);
                                    chkdata1 = true;
                                }

                                if (cbClass.Checked == false && cbYear.Checked == true && cboSortType.Text == "加權總分排名" && sd.YearSumRankA <= rankX && sd.SumScoreA != -1)
                                {
                                    wb.Worksheets[i].Cells[0, 4].PutValue("加權總分");
                                    wb.Worksheets[i].Cells[row, 4].PutValue(sd.SumScoreA);
                                    wb.Worksheets[i].Cells[0, 5].PutValue("加權總分年排名");
                                    wb.Worksheets[i].Cells[row, 5].PutValue(sd.YearSumRankA);
                                    chkdata1 = true;
                                }

                                if (cbClass.Checked == false && cbYear.Checked == true && cboSortType.Text == "加權平均排名" && sd.YearAvgRankA <= rankX && sd.AvgScoreA != -1)
                                {
                                    wb.Worksheets[i].Cells[0, 4].PutValue("加權平均");
                                    wb.Worksheets[i].Cells[row, 4].PutValue(sd.AvgScoreA);
                                    wb.Worksheets[i].Cells[0, 5].PutValue("加權平均年排名");
                                    wb.Worksheets[i].Cells[row, 5].PutValue(sd.YearAvgRankA);
                                    chkdata1 = true;
                                }


                                if (cbClass.Checked == true && cbYear.Checked == true && cboSortType.Text == "總分排名" && (sd.ClassSumRankA < rankX && sd.YearSumRank <= rankX) && sd.SumScore != -1)
                                {
                                    wb.Worksheets[i].Cells[0, 4].PutValue("總分");
                                    wb.Worksheets[i].Cells[row, 4].PutValue(sd.SumScore);
                                    wb.Worksheets[i].Cells[0, 5].PutValue("總分班排名");
                                    wb.Worksheets[i].Cells[row, 5].PutValue(sd.ClassSumRank);
                                    wb.Worksheets[i].Cells[0, 6].PutValue("總分年排名");
                                    wb.Worksheets[i].Cells[row, 6].PutValue(sd.YearSumRank);
                                    chkdata1 = true;
                                }

                                if (cbClass.Checked == true && cbYear.Checked == true && cboSortType.Text == "平均排名" && (sd.ClassAvgRank <= rankX && sd.YearAvgRank <= rankX) && sd.AvgScore != -1)
                                {
                                    wb.Worksheets[i].Cells[0, 4].PutValue("平均");
                                    wb.Worksheets[i].Cells[row, 4].PutValue(sd.AvgScore);
                                    wb.Worksheets[i].Cells[0, 5].PutValue("平均班排名");
                                    wb.Worksheets[i].Cells[row, 5].PutValue(sd.ClassAvgRank);
                                    wb.Worksheets[i].Cells[0, 6].PutValue("平均年排名");
                                    wb.Worksheets[i].Cells[row, 6].PutValue(sd.YearAvgRank);
                                    chkdata1 = true;
                                }

                                if (cbClass.Checked == true && cbYear.Checked == true && cboSortType.Text == "加權總分排名" && (sd.ClassSumRankA <= rankX && sd.YearSumRankA <= rankX) && sd.SumScoreA != -1)
                                {
                                    wb.Worksheets[i].Cells[0, 4].PutValue("加權總分");
                                    wb.Worksheets[i].Cells[row, 4].PutValue(sd.SumScoreA);
                                    wb.Worksheets[i].Cells[0, 5].PutValue("加權總分班排名");
                                    wb.Worksheets[i].Cells[row, 5].PutValue(sd.ClassSumRankA);
                                    wb.Worksheets[i].Cells[0, 6].PutValue("加權總分年排名");
                                    wb.Worksheets[i].Cells[row, 6].PutValue(sd.YearSumRankA);

                                    chkdata1 = true;
                                }

                                if (cbClass.Checked == true && cbYear.Checked == true && cboSortType.Text == "加權平均排名" && (sd.ClassAvgRankA <= rankX && sd.YearAvgRankA <= rankX) && sd.AvgScoreA != -1)
                                {
                                    wb.Worksheets[i].Cells[0, 4].PutValue("加權平均");
                                    wb.Worksheets[i].Cells[row, 4].PutValue(sd.AvgScoreA);
                                    wb.Worksheets[i].Cells[0, 5].PutValue("加權平均班排名");
                                    wb.Worksheets[i].Cells[row, 5].PutValue(sd.ClassAvgRankA);
                                    wb.Worksheets[i].Cells[0, 6].PutValue("加權平均年排名");
                                    wb.Worksheets[i].Cells[row, 6].PutValue(sd.YearAvgRankA);
                                    chkdata1 = true;
                                }

                                if (chkdata1 == true)
                                {
                                    wb.Worksheets[i].Cells[0, 0].PutValue("學號");
                                    wb.Worksheets[i].Cells[0, 1].PutValue("班級");
                                    wb.Worksheets[i].Cells[0, 2].PutValue("座號");
                                    wb.Worksheets[i].Cells[0, 3].PutValue("姓名");
                                    wb.Worksheets[i].Cells[row, 0].PutValue(sd.StudentNum);
                                    wb.Worksheets[i].Cells[row, 1].PutValue(sd.ClassName);
                                    wb.Worksheets[i].Cells[row, 2].PutValue(sd.SeatNo);
                                    wb.Worksheets[i].Cells[row, 3].PutValue(sd.Name);
                                    row++;

                                }
                            }
                        }
                    }



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

                                if (cbClass.Checked == true && cbYear.Checked == false && cboSortType.Text == "總分排名" && sd.ClassSumRank <= rankX && sd.SumScore != -1)
                                {

                                    wb.Worksheets[i].Cells[0, 4].PutValue("總分");
                                    wb.Worksheets[i].Cells[row, 4].PutValue(sd.SumScore);
                                    wb.Worksheets[i].Cells[0, 5].PutValue("總分班排名");
                                    wb.Worksheets[i].Cells[row, 5].PutValue(sd.ClassSumRank);
                                    chkdata1 = true;
                                }

                                if (cbClass.Checked == true && cbYear.Checked == false && cboSortType.Text == "平均排名" && sd.ClassAvgRank <= rankX && sd.AvgScore != -1)
                                {
                                    wb.Worksheets[i].Cells[0, 4].PutValue("平均");
                                    wb.Worksheets[i].Cells[row, 4].PutValue(sd.AvgScore);
                                    wb.Worksheets[i].Cells[0, 5].PutValue("平均班排名");
                                    wb.Worksheets[i].Cells[row, 5].PutValue(sd.ClassAvgRank);
                                    chkdata1 = true;
                                }

                                if (cbClass.Checked == true && cbYear.Checked == false && cboSortType.Text == "加權總分排名" && sd.ClassSumRankA <= rankX && sd.SumScoreA != -1)
                                {
                                    wb.Worksheets[i].Cells[0, 4].PutValue("加權總分");
                                    wb.Worksheets[i].Cells[row, 4].PutValue(sd.SumScoreA);
                                    wb.Worksheets[i].Cells[0, 5].PutValue("加權總分班排名");
                                    wb.Worksheets[i].Cells[row, 5].PutValue(sd.ClassSumRankA);
                                    chkdata1 = true;
                                }

                                if (cbClass.Checked == true && cbYear.Checked == false && cboSortType.Text == "加權平均排名" && sd.ClassAvgRankA <= rankX && sd.AvgScoreA != -1)
                                {
                                    wb.Worksheets[i].Cells[0, 4].PutValue("加權平均");
                                    wb.Worksheets[i].Cells[row, 4].PutValue(sd.AvgScoreA);
                                    wb.Worksheets[i].Cells[0, 5].PutValue("加權平均班排名");
                                    wb.Worksheets[i].Cells[row, 5].PutValue(sd.ClassAvgRankA);
                                    chkdata1 = true;
                                }

                                if (cbClass.Checked == false && cbYear.Checked == true && cboSortType.Text == "總分排名" && sd.YearSumRank <= rankX && sd.SumScore != -1)
                                {
                                    wb.Worksheets[i].Cells[0, 4].PutValue("總分");
                                    wb.Worksheets[i].Cells[row, 4].PutValue(sd.SumScore);
                                    wb.Worksheets[i].Cells[0, 5].PutValue("總分年排名");
                                    wb.Worksheets[i].Cells[row, 5].PutValue(sd.YearSumRank);
                                    chkdata1 = true;
                                }

                                if (cbClass.Checked == false && cbYear.Checked == true && cboSortType.Text == "平均排名" && sd.YearAvgRankA <= rankX && sd.AvgScore != -1)
                                {
                                    wb.Worksheets[i].Cells[0, 4].PutValue("平均");
                                    wb.Worksheets[i].Cells[row, 4].PutValue(sd.AvgScore);
                                    wb.Worksheets[i].Cells[0, 5].PutValue("平均年排名");
                                    wb.Worksheets[i].Cells[row, 5].PutValue(sd.YearAvgRank);
                                    chkdata1 = true;
                                }

                                if (cbClass.Checked == false && cbYear.Checked == true && cboSortType.Text == "加權總分排名" && sd.YearSumRankA <= rankX && sd.SumScoreA != -1)
                                {
                                    wb.Worksheets[i].Cells[0, 4].PutValue("加權總分");
                                    wb.Worksheets[i].Cells[row, 4].PutValue(sd.SumScoreA);
                                    wb.Worksheets[i].Cells[0, 5].PutValue("加權總分年排名");
                                    wb.Worksheets[i].Cells[row, 5].PutValue(sd.YearSumRankA);
                                    chkdata1 = true;
                                }

                                if (cbClass.Checked == false && cbYear.Checked == true && cboSortType.Text == "加權平均排名" && sd.YearAvgRankA <= rankX && sd.AvgScoreA != -1)
                                {
                                    wb.Worksheets[i].Cells[0, 4].PutValue("加權平均");
                                    wb.Worksheets[i].Cells[row, 4].PutValue(sd.AvgScoreA);
                                    wb.Worksheets[i].Cells[0, 5].PutValue("加權平均年排名");
                                    wb.Worksheets[i].Cells[row, 5].PutValue(sd.YearAvgRankA);
                                    chkdata1 = true;
                                }


                                if (cbClass.Checked == true && cbYear.Checked == true && cboSortType.Text == "總分排名" && (sd.ClassSumRank <= rankX && sd.YearSumRank <= rankX) && sd.SumScore != -1)
                                {
                                    wb.Worksheets[i].Cells[0, 4].PutValue("總分");
                                    wb.Worksheets[i].Cells[row, 4].PutValue(sd.SumScore);
                                    wb.Worksheets[i].Cells[0, 5].PutValue("總分班排名");
                                    wb.Worksheets[i].Cells[row, 5].PutValue(sd.ClassSumRank);
                                    wb.Worksheets[i].Cells[0, 6].PutValue("總分年排名");
                                    wb.Worksheets[i].Cells[row, 6].PutValue(sd.YearSumRank);
                                    chkdata1 = true;
                                }

                                if (cbClass.Checked == true && cbYear.Checked == true && cboSortType.Text == "平均排名" && (sd.ClassAvgRank <= rankX && sd.YearAvgRank <= rankX) && sd.AvgScore != -1)
                                {
                                    wb.Worksheets[i].Cells[0, 4].PutValue("平均");
                                    wb.Worksheets[i].Cells[row, 4].PutValue(sd.AvgScore);
                                    wb.Worksheets[i].Cells[0, 5].PutValue("平均班排名");
                                    wb.Worksheets[i].Cells[row, 5].PutValue(sd.ClassAvgRank);
                                    wb.Worksheets[i].Cells[0, 6].PutValue("平均年排名");
                                    wb.Worksheets[i].Cells[row, 6].PutValue(sd.YearAvgRank);
                                    chkdata1 = true;
                                }

                                if (cbClass.Checked == true && cbYear.Checked == true && cboSortType.Text == "加權總分排名" && (sd.ClassSumRankA <= rankX && sd.YearSumRankA <= rankX) && sd.SumScoreA != -1)
                                {
                                    wb.Worksheets[i].Cells[0, 4].PutValue("加權總分");
                                    wb.Worksheets[i].Cells[row, 4].PutValue(sd.SumScoreA);
                                    wb.Worksheets[i].Cells[0, 5].PutValue("加權總分班排名");
                                    wb.Worksheets[i].Cells[row, 5].PutValue(sd.ClassSumRankA);
                                    wb.Worksheets[i].Cells[0, 6].PutValue("加權總分年排名");
                                    wb.Worksheets[i].Cells[row, 6].PutValue(sd.YearSumRankA);

                                    chkdata1 = true;
                                }

                                if (cbClass.Checked == true && cbYear.Checked == true && cboSortType.Text == "加權平均排名" && (sd.ClassAvgRankA <= rankX && sd.YearAvgRankA <= rankX) && sd.AvgScoreA != -1)
                                {
                                    wb.Worksheets[i].Cells[0, 4].PutValue("加權平均");
                                    wb.Worksheets[i].Cells[row, 4].PutValue(sd.AvgScoreA);
                                    wb.Worksheets[i].Cells[0, 5].PutValue("加權平均班排名");
                                    wb.Worksheets[i].Cells[row, 5].PutValue(sd.ClassAvgRankA);
                                    wb.Worksheets[i].Cells[0, 6].PutValue("加權平均年排名");
                                    wb.Worksheets[i].Cells[row, 6].PutValue(sd.YearAvgRankA);
                                    chkdata1 = true;
                                }

                                if (chkdata1 == true)
                                {
                                    wb.Worksheets[i].Cells[0, 0].PutValue("學號");
                                    wb.Worksheets[i].Cells[0, 1].PutValue("班級");
                                    wb.Worksheets[i].Cells[0, 2].PutValue("座號");
                                    wb.Worksheets[i].Cells[0, 3].PutValue("姓名");
                                    wb.Worksheets[i].Cells[row, 0].PutValue(sd.StudentNum);
                                    wb.Worksheets[i].Cells[row, 1].PutValue(sd.ClassName);
                                    wb.Worksheets[i].Cells[row, 2].PutValue(sd.SeatNo);
                                    wb.Worksheets[i].Cells[row, 3].PutValue(sd.Name);
                                    row++;
                                }
                            }
                        }
                        i++;
                    }

                } //if

                // ---- 科目排名開始

                if (lbxSubjct.Visible == true)
                {
                    if (rb01.Checked == true)
                    {
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

                                    if (cbClass.Checked == true && cbYear.Checked == false && cboSortType.Text == "科目排名")
                                    {

                                        foreach (ExamData ed in sd.lstStudExamScore)
                                            if (((ed.SubjectName + ed.SubjectLevel) == lbxSubjct.Text) && (ed.ExamName == ExName))
                                            {
                                                wb.Worksheets[i].Cells[0, 4].PutValue(ed.SubjectName + "分數");
                                                wb.Worksheets[i].Cells[row, 4].PutValue(ed.Score);
                                                wb.Worksheets[i].Cells[0, 5].PutValue("班排名");
                                                wb.Worksheets[i].Cells[row, 5].PutValue(ed.ClassScoreRank);
                                                chkdata1 = true;
                                            }
                                    }


                                    if (cbClass.Checked == false && cbYear.Checked == true && cboSortType.Text == "科目排名")
                                    {
                                        foreach (ExamData ed in sd.lstStudExamScore)
                                            if (((ed.SubjectName + ed.SubjectLevel) == lbxSubjct.Text) && (ed.ExamName == ExName))
                                            {
                                                wb.Worksheets[i].Cells[0, 4].PutValue(ed.SubjectName + "分數");
                                                wb.Worksheets[i].Cells[row, 4].PutValue(ed.Score);
                                                wb.Worksheets[i].Cells[0, 5].PutValue("年排名");
                                                wb.Worksheets[i].Cells[row, 5].PutValue(ed.YearScoreRank);
                                                chkdata1 = true;
                                            }
                                    }

                                    if (cbClass.Checked == true && cbYear.Checked == true && cboSortType.Text == "科目排名")
                                    {
                                        foreach (ExamData ed in sd.lstStudExamScore)
                                            if (((ed.SubjectName + ed.SubjectLevel) == lbxSubjct.Text) && (ed.ExamName == ExName))
                                            {
                                                wb.Worksheets[i].Cells[0, 4].PutValue(ed.SubjectName + "分數");
                                                wb.Worksheets[i].Cells[row, 4].PutValue(ed.Score);
                                                wb.Worksheets[i].Cells[0, 5].PutValue("班排名");
                                                wb.Worksheets[i].Cells[row, 5].PutValue(ed.ClassScoreRank);
                                                wb.Worksheets[i].Cells[0, 6].PutValue("年排名");
                                                wb.Worksheets[i].Cells[row, 6].PutValue(ed.YearScoreRank);
                                                chkdata1 = true;
                                            }
                                    }

                                    if (chkdata1 == true)
                                    {
                                        wb.Worksheets[i].Cells[0, 0].PutValue("學號");
                                        wb.Worksheets[i].Cells[0, 1].PutValue("班級");
                                        wb.Worksheets[i].Cells[0, 2].PutValue("座號");
                                        wb.Worksheets[i].Cells[0, 3].PutValue("姓名");
                                        wb.Worksheets[i].Cells[row, 0].PutValue(sd.StudentNum);
                                        wb.Worksheets[i].Cells[row, 1].PutValue(sd.ClassName);
                                        wb.Worksheets[i].Cells[row, 2].PutValue(sd.SeatNo);
                                        wb.Worksheets[i].Cells[row, 3].PutValue(sd.Name);
                                        row++;
                                    }

                                }
                            }
                        }



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
                                    if (cbClass.Checked == true && cbYear.Checked == false && cboSortType.Text == "科目排名")
                                    {

                                        foreach (ExamData ed in sd.lstStudExamScore)
                                            if (((ed.SubjectName + ed.SubjectLevel) == lbxSubjct.Text) && (ed.ExamName == ExName))
                                            {
                                                wb.Worksheets[i].Cells[0, 4].PutValue(ed.SubjectName + "分數");
                                                wb.Worksheets[i].Cells[row, 4].PutValue(ed.Score);
                                                wb.Worksheets[i].Cells[0, 5].PutValue("班排名");
                                                wb.Worksheets[i].Cells[row, 5].PutValue(ed.ClassScoreRank);
                                                chkdata1 = true;
                                            }
                                    }


                                    if (cbClass.Checked == false && cbYear.Checked == true && cboSortType.Text == "科目排名")
                                    {
                                        foreach (ExamData ed in sd.lstStudExamScore)
                                            if (((ed.SubjectName + ed.SubjectLevel) == lbxSubjct.Text) && (ed.ExamName == ExName))
                                            {
                                                wb.Worksheets[i].Cells[0, 4].PutValue(ed.SubjectName + "分數");
                                                wb.Worksheets[i].Cells[row, 4].PutValue(ed.Score);
                                                wb.Worksheets[i].Cells[0, 5].PutValue("年排名");
                                                wb.Worksheets[i].Cells[row, 5].PutValue(ed.YearScoreRank);
                                                chkdata1 = true;
                                            }
                                    }

                                    if (cbClass.Checked == true && cbYear.Checked == true && cboSortType.Text == "科目排名")
                                    {
                                        foreach (ExamData ed in sd.lstStudExamScore)
                                            if (((ed.SubjectName + ed.SubjectLevel) == lbxSubjct.Text) && (ed.ExamName == ExName))
                                            {
                                                wb.Worksheets[i].Cells[0, 4].PutValue(ed.SubjectName + "分數");
                                                wb.Worksheets[i].Cells[row, 4].PutValue(ed.Score);
                                                wb.Worksheets[i].Cells[0, 5].PutValue("班排名");
                                                wb.Worksheets[i].Cells[row, 5].PutValue(ed.ClassScoreRank);
                                                wb.Worksheets[i].Cells[0, 6].PutValue("年排名");
                                                wb.Worksheets[i].Cells[row, 6].PutValue(ed.YearScoreRank);
                                                chkdata1 = true;
                                            }
                                    }

                                    if (chkdata1 == true)
                                    {
                                        wb.Worksheets[i].Cells[0, 0].PutValue("學號");
                                        wb.Worksheets[i].Cells[0, 1].PutValue("班級");
                                        wb.Worksheets[i].Cells[0, 2].PutValue("座號");
                                        wb.Worksheets[i].Cells[0, 3].PutValue("姓名");
                                        wb.Worksheets[i].Cells[row, 0].PutValue(sd.StudentNum);
                                        wb.Worksheets[i].Cells[row, 1].PutValue(sd.ClassName);
                                        wb.Worksheets[i].Cells[row, 2].PutValue(sd.SeatNo);
                                        wb.Worksheets[i].Cells[row, 3].PutValue(sd.Name);
                                        row++;
                                    }
                                }
                            }
                            i++;
                        }
                    }
                } //if


                // --- 科前幾名計算
                int rankXs = 0;
                int.TryParse(txtBox01.Text, out rankXs);


                // 選全部+班排(前幾名)
                if (rb02.Checked == true && lbxSubjct.Visible == true && rankXs > 0 && txtBox01.Text.Trim() != "")
                {
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

                                if (cbClass.Checked == true && cbYear.Checked == false && cboSortType.Text == "科目排名")
                                {

                                    foreach (ExamData ed in sd.lstStudExamScore)
                                        if (((ed.SubjectName + ed.SubjectLevel) == lbxSubjct.Text) && (ed.ExamName == ExName) && (ed.ClassScoreRank <= rankXs))
                                        {
                                            wb.Worksheets[i].Cells[0, 4].PutValue(ed.SubjectName + "分數");
                                            wb.Worksheets[i].Cells[row, 4].PutValue(ed.Score);
                                            wb.Worksheets[i].Cells[0, 5].PutValue("班排名");
                                            wb.Worksheets[i].Cells[row, 5].PutValue(ed.ClassScoreRank);
                                            chkdata1 = true;
                                        }
                                }


                                if (cbClass.Checked == false && cbYear.Checked == true && cboSortType.Text == "科目排名")
                                {
                                    foreach (ExamData ed in sd.lstStudExamScore)
                                        if (((ed.SubjectName + ed.SubjectLevel) == lbxSubjct.Text) && (ed.ExamName == ExName) && (ed.YearScoreRank <= rankXs))
                                        {
                                            wb.Worksheets[i].Cells[0, 4].PutValue(ed.SubjectName + "分數");
                                            wb.Worksheets[i].Cells[row, 4].PutValue(ed.Score);
                                            wb.Worksheets[i].Cells[0, 5].PutValue("年排名");
                                            wb.Worksheets[i].Cells[row, 5].PutValue(ed.YearScoreRank);
                                            chkdata1 = true;
                                        }
                                }

                                if (cbClass.Checked == true && cbYear.Checked == true && cboSortType.Text == "科目排名")
                                {
                                    foreach (ExamData ed in sd.lstStudExamScore)
                                        if (((ed.SubjectName + ed.SubjectLevel) == lbxSubjct.Text) && (ed.ExamName == ExName) && (ed.ClassScoreRank <= rankXs && ed.YearScoreRank <= rankXs))
                                        {
                                            wb.Worksheets[i].Cells[0, 4].PutValue(ed.SubjectName + "分數");
                                            wb.Worksheets[i].Cells[row, 4].PutValue(ed.Score);
                                            wb.Worksheets[i].Cells[0, 5].PutValue("班排名");
                                            wb.Worksheets[i].Cells[row, 5].PutValue(ed.ClassScoreRank);
                                            wb.Worksheets[i].Cells[0, 6].PutValue("年排名");
                                            wb.Worksheets[i].Cells[row, 6].PutValue(ed.YearScoreRank);
                                            chkdata1 = true;
                                        }
                                }

                                if (chkdata1 == true)
                                {
                                    wb.Worksheets[i].Cells[0, 0].PutValue("學號");
                                    wb.Worksheets[i].Cells[0, 1].PutValue("班級");
                                    wb.Worksheets[i].Cells[0, 2].PutValue("座號");
                                    wb.Worksheets[i].Cells[0, 3].PutValue("姓名");
                                    wb.Worksheets[i].Cells[row, 0].PutValue(sd.StudentNum);
                                    wb.Worksheets[i].Cells[row, 1].PutValue(sd.ClassName);
                                    wb.Worksheets[i].Cells[row, 2].PutValue(sd.SeatNo);
                                    wb.Worksheets[i].Cells[row, 3].PutValue(sd.Name);
                                    row++;
                                }

                            }
                        }
                    }



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

                                if (cbClass.Checked == true && cbYear.Checked == false && cboSortType.Text == "科目排名")
                                {

                                    foreach (ExamData ed in sd.lstStudExamScore)
                                        if (((ed.SubjectName + ed.SubjectLevel) == lbxSubjct.Text) && (ed.ExamName == ExName) && (ed.ClassScoreRank <= rankXs))
                                        {
                                            wb.Worksheets[i].Cells[0, 4].PutValue(ed.SubjectName + "分數");
                                            wb.Worksheets[i].Cells[row, 4].PutValue(ed.Score);
                                            wb.Worksheets[i].Cells[0, 5].PutValue("班排名");
                                            wb.Worksheets[i].Cells[row, 5].PutValue(ed.ClassScoreRank);
                                            chkdata1 = true;
                                        }
                                }


                                if (cbClass.Checked == false && cbYear.Checked == true && cboSortType.Text == "科目排名")
                                {
                                    foreach (ExamData ed in sd.lstStudExamScore)
                                        if (((ed.SubjectName + ed.SubjectLevel) == lbxSubjct.Text) && (ed.ExamName == ExName) && (ed.YearScoreRank <= rankXs))
                                        {
                                            wb.Worksheets[i].Cells[0, 4].PutValue(ed.SubjectName + "分數");
                                            wb.Worksheets[i].Cells[row, 4].PutValue(ed.Score);
                                            wb.Worksheets[i].Cells[0, 5].PutValue("年排名");
                                            wb.Worksheets[i].Cells[row, 5].PutValue(ed.YearScoreRank);
                                            chkdata1 = true;
                                        }
                                }

                                if (cbClass.Checked == true && cbYear.Checked == true && cboSortType.Text == "科目排名")
                                {
                                    foreach (ExamData ed in sd.lstStudExamScore)
                                        if (((ed.SubjectName + ed.SubjectLevel) == lbxSubjct.Text) && (ed.ExamName == ExName) && (ed.ClassScoreRank <= rankXs && ed.YearScoreRank <= rankXs))
                                        {
                                            wb.Worksheets[i].Cells[0, 4].PutValue(ed.SubjectName + "分數");
                                            wb.Worksheets[i].Cells[row, 4].PutValue(ed.Score);
                                            wb.Worksheets[i].Cells[0, 5].PutValue("班排名");
                                            wb.Worksheets[i].Cells[row, 5].PutValue(ed.ClassScoreRank);
                                            wb.Worksheets[i].Cells[0, 6].PutValue("年排名");
                                            wb.Worksheets[i].Cells[row, 6].PutValue(ed.YearScoreRank);
                                            chkdata1 = true;
                                        }
                                }

                                if (chkdata1 == true)
                                {
                                    wb.Worksheets[i].Cells[0, 0].PutValue("學號");
                                    wb.Worksheets[i].Cells[0, 1].PutValue("班級");
                                    wb.Worksheets[i].Cells[0, 2].PutValue("座號");
                                    wb.Worksheets[i].Cells[0, 3].PutValue("姓名");
                                    wb.Worksheets[i].Cells[row, 0].PutValue(sd.StudentNum);
                                    wb.Worksheets[i].Cells[row, 1].PutValue(sd.ClassName);
                                    wb.Worksheets[i].Cells[row, 2].PutValue(sd.SeatNo);
                                    wb.Worksheets[i].Cells[row, 3].PutValue(sd.Name);
                                    row++;
                                }
                            }
                        }
                        i++;
                    }

                    //}



                } // if
                // 選加權總平均XX分以上
                if (rb03.Checked )
                {
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
                                if (sd.AvgScoreA>=ScoreX)
                                {
                                    wb.Worksheets[i].Cells[0, 4].PutValue("加權平均");
                                    wb.Worksheets[i].Cells[row, 4].PutValue(sd.AvgScoreA);
                                    wb.Worksheets[i].Cells[0, 5].PutValue("加權平均班排名");
                                    wb.Worksheets[i].Cells[row, 5].PutValue(sd.ClassAvgRankA);
                                    wb.Worksheets[i].Cells[0, 6].PutValue("加權平均年排名");
                                    wb.Worksheets[i].Cells[row, 6].PutValue(sd.YearAvgRankA);
                                    chkdata1 = true;
                                }                                

                                if (chkdata1 == true)
                                {
                                    wb.Worksheets[i].Cells[0, 0].PutValue("學號");
                                    wb.Worksheets[i].Cells[0, 1].PutValue("班級");
                                    wb.Worksheets[i].Cells[0, 2].PutValue("座號");
                                    wb.Worksheets[i].Cells[0, 3].PutValue("姓名");
                                    wb.Worksheets[i].Cells[row, 0].PutValue(sd.StudentNum);
                                    wb.Worksheets[i].Cells[row, 1].PutValue(sd.ClassName);
                                    wb.Worksheets[i].Cells[row, 2].PutValue(sd.SeatNo);
                                    wb.Worksheets[i].Cells[row, 3].PutValue(sd.Name);
                                    row++;
                                }

                            }
                        }
                    }
                    
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

                                if (sd.AvgScoreA >=ScoreX)
                                {
                                    wb.Worksheets[i].Cells[0, 4].PutValue("加權平均");
                                    wb.Worksheets[i].Cells[row, 4].PutValue(sd.AvgScoreA);
                                    wb.Worksheets[i].Cells[0, 5].PutValue("加權平均班排名");
                                    wb.Worksheets[i].Cells[row, 5].PutValue(sd.ClassAvgRankA);
                                    wb.Worksheets[i].Cells[0, 6].PutValue("加權平均年排名");
                                    wb.Worksheets[i].Cells[row, 6].PutValue(sd.YearAvgRankA);
                                    chkdata1 = true;
                                }

                                if (chkdata1 == true)
                                {
                                    wb.Worksheets[i].Cells[0, 0].PutValue("學號");
                                    wb.Worksheets[i].Cells[0, 1].PutValue("班級");
                                    wb.Worksheets[i].Cells[0, 2].PutValue("座號");
                                    wb.Worksheets[i].Cells[0, 3].PutValue("姓名");
                                    wb.Worksheets[i].Cells[row, 0].PutValue(sd.StudentNum);
                                    wb.Worksheets[i].Cells[row, 1].PutValue(sd.ClassName);
                                    wb.Worksheets[i].Cells[row, 2].PutValue(sd.SeatNo);
                                    wb.Worksheets[i].Cells[row, 3].PutValue(sd.Name);
                                    row++;
                                }


                            }
                        }
                        i++;
                    }
                } //if

                // 加權總平均＿班級進步前三名
                if (rb04.Checked)
                {
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
                                if (sd.ClassAvgRankAS <= 3 && sd.ClassAvgRankAS >= 0 && (sd.AvgScoreA - sd.AvgScoreA1)>0)
                                {
                                    wb.Worksheets[i].Cells[0, 4].PutValue("本次加權平均");
                                    wb.Worksheets[i].Cells[row, 4].PutValue(sd.AvgScoreA);
                                    wb.Worksheets[i].Cells[0, 5].PutValue("前次加權平均");
                                    wb.Worksheets[i].Cells[row, 5].PutValue(sd.AvgScoreA1);
                                    wb.Worksheets[i].Cells[0, 6].PutValue("加權平均分數差");
                                    wb.Worksheets[i].Cells[row, 6].PutValue(sd.AvgScoreA - sd.AvgScoreA1);
                                    wb.Worksheets[i].Cells[0, 7].PutValue("加權平均成績進步排名");
                                    wb.Worksheets[i].Cells[row, 7].PutValue(sd.ClassAvgRankAS);
                                    wb.Worksheets[i].Cells[0, 8].PutValue("加權平均班排名");
                                    wb.Worksheets[i].Cells[row, 8].PutValue(sd.ClassAvgRankA);
                                    wb.Worksheets[i].Cells[0, 9].PutValue("加權平均前班排名");
                                    wb.Worksheets[i].Cells[row, 9].PutValue(sd.ClassAvgRankA1);
                                    wb.Worksheets[i].Cells[0, 10].PutValue("進步名次");
                                    wb.Worksheets[i].Cells[row, 10].PutValue(sd.ClassAvgRankA1 - sd.ClassAvgRankA);
                                    wb.Worksheets[i].Cells[0, 11].PutValue("加權平均排名進步排名");
                                    wb.Worksheets[i].Cells[row, 11].PutValue(sd.ClassAvgRankAD);
                                    chkdata1 = true;
                                }

                                if (chkdata1 == true)
                                {
                                    wb.Worksheets[i].Cells[0, 0].PutValue("學號");
                                    wb.Worksheets[i].Cells[0, 1].PutValue("班級");
                                    wb.Worksheets[i].Cells[0, 2].PutValue("座號");
                                    wb.Worksheets[i].Cells[0, 3].PutValue("姓名");
                                    wb.Worksheets[i].Cells[row, 0].PutValue(sd.StudentNum);
                                    wb.Worksheets[i].Cells[row, 1].PutValue(sd.ClassName);
                                    wb.Worksheets[i].Cells[row, 2].PutValue(sd.SeatNo);
                                    wb.Worksheets[i].Cells[row, 3].PutValue(sd.Name);
                                    row++;
                                }

                            }
                        }
                    }

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

                                if (sd.ClassAvgRankAS <= 3 && sd.ClassAvgRankAS >= 0 && (sd.AvgScoreA - sd.AvgScoreA1) > 0)
                                {
                                    wb.Worksheets[i].Cells[0, 4].PutValue("本次加權平均");
                                    wb.Worksheets[i].Cells[row, 4].PutValue(sd.AvgScoreA);
                                    wb.Worksheets[i].Cells[0, 5].PutValue("前次加權平均");
                                    wb.Worksheets[i].Cells[row, 5].PutValue(sd.AvgScoreA1);
                                    wb.Worksheets[i].Cells[0, 6].PutValue("加權平均分數差");
                                    wb.Worksheets[i].Cells[row, 6].PutValue(sd.AvgScoreA - sd.AvgScoreA1);
                                    wb.Worksheets[i].Cells[0, 7].PutValue("加權平均成績進步排名");
                                    wb.Worksheets[i].Cells[row, 7].PutValue(sd.ClassAvgRankAS);
                                    wb.Worksheets[i].Cells[0, 8].PutValue("加權平均班排名");
                                    wb.Worksheets[i].Cells[row, 8].PutValue(sd.ClassAvgRankA);
                                    wb.Worksheets[i].Cells[0, 9].PutValue("加權平均前班排名");
                                    wb.Worksheets[i].Cells[row, 9].PutValue(sd.ClassAvgRankA1);
                                    wb.Worksheets[i].Cells[0, 10].PutValue("進步名次");
                                    wb.Worksheets[i].Cells[row, 10].PutValue(sd.ClassAvgRankA1 - sd.ClassAvgRankA);
                                    wb.Worksheets[i].Cells[0, 11].PutValue("加權平均排名進步排名");
                                    wb.Worksheets[i].Cells[row, 11].PutValue(sd.ClassAvgRankAD);
                                    chkdata1 = true;
                                }

                                if (chkdata1 == true)
                                {
                                    wb.Worksheets[i].Cells[0, 0].PutValue("學號");
                                    wb.Worksheets[i].Cells[0, 1].PutValue("班級");
                                    wb.Worksheets[i].Cells[0, 2].PutValue("座號");
                                    wb.Worksheets[i].Cells[0, 3].PutValue("姓名");
                                    wb.Worksheets[i].Cells[row, 0].PutValue(sd.StudentNum);
                                    wb.Worksheets[i].Cells[row, 1].PutValue(sd.ClassName);
                                    wb.Worksheets[i].Cells[row, 2].PutValue(sd.SeatNo);
                                    wb.Worksheets[i].Cells[row, 3].PutValue(sd.Name);
                                    row++;
                                }


                            }
                        }
                        i++;
                    }
                } //if
               
                // 加權總平均排名＿班級進步前三名
                if (rb05.Checked)
                {
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
                                if (sd.ClassAvgRankAD <= 3 && sd.ClassAvgRankAD >= 0 && (sd.AvgScoreA - sd.AvgScoreA1) > 0)
                                {
                                    wb.Worksheets[i].Cells[0, 4].PutValue("本次加權平均");
                                    wb.Worksheets[i].Cells[row, 4].PutValue(sd.AvgScoreA);
                                    wb.Worksheets[i].Cells[0, 5].PutValue("前次加權平均");
                                    wb.Worksheets[i].Cells[row, 5].PutValue(sd.AvgScoreA1);
                                    wb.Worksheets[i].Cells[0, 6].PutValue("進步分數");
                                    wb.Worksheets[i].Cells[row, 6].PutValue(sd.AvgScoreA - sd.AvgScoreA1);
                                    wb.Worksheets[i].Cells[0, 7].PutValue("加權平均班排名");
                                    wb.Worksheets[i].Cells[row, 7].PutValue(sd.ClassAvgRankA);
                                    wb.Worksheets[i].Cells[0, 8].PutValue("加權平均前班排名");
                                    wb.Worksheets[i].Cells[row, 8].PutValue(sd.ClassAvgRankA1);
                                    wb.Worksheets[i].Cells[0, 9].PutValue("進步名次");
                                    wb.Worksheets[i].Cells[row, 9].PutValue(sd.ClassAvgRankA1 - sd.ClassAvgRankA);
                                    wb.Worksheets[i].Cells[0, 10].PutValue("加權平均進步排名");
                                    wb.Worksheets[i].Cells[row, 10].PutValue(sd.ClassAvgRankAD);
                                    chkdata1 = true;
                                }

                                if (chkdata1 == true)
                                {
                                    wb.Worksheets[i].Cells[0, 0].PutValue("學號");
                                    wb.Worksheets[i].Cells[0, 1].PutValue("班級");
                                    wb.Worksheets[i].Cells[0, 2].PutValue("座號");
                                    wb.Worksheets[i].Cells[0, 3].PutValue("姓名");
                                    wb.Worksheets[i].Cells[row, 0].PutValue(sd.StudentNum);
                                    wb.Worksheets[i].Cells[row, 1].PutValue(sd.ClassName);
                                    wb.Worksheets[i].Cells[row, 2].PutValue(sd.SeatNo);
                                    wb.Worksheets[i].Cells[row, 3].PutValue(sd.Name);
                                    row++;
                                }

                            }
                        }
                    }

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

                                if (sd.ClassAvgRankAD <= 3 && sd.ClassAvgRankAD >= 0 && (sd.AvgScoreA - sd.AvgScoreA1) > 0)
                                {                                    
                                    wb.Worksheets[i].Cells[0, 4].PutValue("本次加權平均");
                                    wb.Worksheets[i].Cells[row, 4].PutValue(sd.AvgScoreA);
                                    wb.Worksheets[i].Cells[0, 5].PutValue("前次加權平均");
                                    wb.Worksheets[i].Cells[row, 5].PutValue(sd.AvgScoreA1);
                                    wb.Worksheets[i].Cells[0, 6].PutValue("進步分數");
                                    wb.Worksheets[i].Cells[row, 6].PutValue(sd.AvgScoreA - sd.AvgScoreA1);
                                    wb.Worksheets[i].Cells[0, 7].PutValue("加權平均班排名");
                                    wb.Worksheets[i].Cells[row, 7].PutValue(sd.ClassAvgRankA);
                                    wb.Worksheets[i].Cells[0, 8].PutValue("加權平均前班排名");
                                    wb.Worksheets[i].Cells[row, 8].PutValue(sd.ClassAvgRankA1);
                                    wb.Worksheets[i].Cells[0, 9].PutValue("進步名次");
                                    wb.Worksheets[i].Cells[row, 9].PutValue(sd.ClassAvgRankA1 - sd.ClassAvgRankA);
                                    wb.Worksheets[i].Cells[0, 10].PutValue("加權平均進步排名");
                                    wb.Worksheets[i].Cells[row, 10].PutValue(sd.ClassAvgRankAD);
                                    chkdata1 = true;
                                }

                                if (chkdata1 == true)
                                {
                                    wb.Worksheets[i].Cells[0, 0].PutValue("學號");
                                    wb.Worksheets[i].Cells[0, 1].PutValue("班級");
                                    wb.Worksheets[i].Cells[0, 2].PutValue("座號");
                                    wb.Worksheets[i].Cells[0, 3].PutValue("姓名");
                                    wb.Worksheets[i].Cells[row, 0].PutValue(sd.StudentNum);
                                    wb.Worksheets[i].Cells[row, 1].PutValue(sd.ClassName);
                                    wb.Worksheets[i].Cells[row, 2].PutValue(sd.SeatNo);
                                    wb.Worksheets[i].Cells[row, 3].PutValue(sd.Name);
                                    row++;
                                }


                            }
                        }
                        i++;
                    }
                } //if
                // 加權總平均＿個人進步XX分
                if (rb06.Checked)
                {
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

                                if (sd.AvgScoreA-sd.AvgScoreA1 >= ScoreX2 && (sd.AvgScoreA!=-1 && sd.AvgScoreA1!=-1))
                                {
                                    wb.Worksheets[i].Cells[0, 4].PutValue("本次加權平均");
                                    wb.Worksheets[i].Cells[row, 4].PutValue(sd.AvgScoreA);                                   
                                    wb.Worksheets[i].Cells[0, 5].PutValue("前次加權平均");
                                    wb.Worksheets[i].Cells[row, 5].PutValue(sd.AvgScoreA1);
                                    wb.Worksheets[i].Cells[0, 6].PutValue("進步分數");
                                    wb.Worksheets[i].Cells[row, 6].PutValue(sd.AvgScoreA-sd.AvgScoreA1);
                                    chkdata1 = true;
                                }

                                if (chkdata1 == true)
                                {
                                    wb.Worksheets[i].Cells[0, 0].PutValue("學號");
                                    wb.Worksheets[i].Cells[0, 1].PutValue("班級");
                                    wb.Worksheets[i].Cells[0, 2].PutValue("座號");
                                    wb.Worksheets[i].Cells[0, 3].PutValue("姓名");
                                    wb.Worksheets[i].Cells[row, 0].PutValue(sd.StudentNum);
                                    wb.Worksheets[i].Cells[row, 1].PutValue(sd.ClassName);
                                    wb.Worksheets[i].Cells[row, 2].PutValue(sd.SeatNo);
                                    wb.Worksheets[i].Cells[row, 3].PutValue(sd.Name);
                                    row++;
                                }

                            }
                        }
                    }

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

                                if (sd.AvgScoreA - sd.AvgScoreA1 >= ScoreX2 && (sd.AvgScoreA != -1 && sd.AvgScoreA1 != -1))
                                {
                                    wb.Worksheets[i].Cells[0, 4].PutValue("本次加權平均");
                                    wb.Worksheets[i].Cells[row, 4].PutValue(sd.AvgScoreA);
                                    wb.Worksheets[i].Cells[0, 5].PutValue("前次加權平均");
                                    wb.Worksheets[i].Cells[row, 5].PutValue(sd.AvgScoreA1);
                                    wb.Worksheets[i].Cells[0, 6].PutValue("進步分數");
                                    wb.Worksheets[i].Cells[row, 6].PutValue(sd.AvgScoreA - sd.AvgScoreA1);
                                    chkdata1 = true;
                                }

                                if (chkdata1 == true)
                                {
                                    wb.Worksheets[i].Cells[0, 0].PutValue("學號");
                                    wb.Worksheets[i].Cells[0, 1].PutValue("班級");
                                    wb.Worksheets[i].Cells[0, 2].PutValue("座號");
                                    wb.Worksheets[i].Cells[0, 3].PutValue("姓名");
                                    wb.Worksheets[i].Cells[row, 0].PutValue(sd.StudentNum);
                                    wb.Worksheets[i].Cells[row, 1].PutValue(sd.ClassName);
                                    wb.Worksheets[i].Cells[row, 2].PutValue(sd.SeatNo);
                                    wb.Worksheets[i].Cells[row, 3].PutValue(sd.Name);
                                    row++;
                                }


                            }
                        }
                        i++;
                    }
                } //if


                try
                {
                    wb.Save(Application.StartupPath + "\\Reports\\" + cboExam.Text + "評量排名.xls", FileFormatType.Excel2003);
                    System.Diagnostics.Process.Start(Application.StartupPath + "\\Reports\\" + cboExam.Text + "評量排名.xls");
                }
                catch
                {
                    System.Windows.Forms.SaveFileDialog sd1 = new System.Windows.Forms.SaveFileDialog();
                    sd1.Title = "另存新檔";
                    sd1.FileName = cboExam.Text + "評量排名.xls";
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

        private void btnToX2s_Click(object sender, EventArgs e)
        {
            Calc_ScoreAndRank();
            if (cboReportSortType.Text != "" || cboReportSortType.Text != null)
                StudDatas.Sort(CompareCode);
            int SchoolYear;
            string  Semester;
            
            // 開啟 Word 範本
            Document template = new Aspose.Words.Document(Application.StartupPath + "\\Customize\\獎狀範本.doc", LoadFormat.Doc, "");
            //DocumentBuilder builder = new DocumentBuilder(template);

            //Dictionary<string, List<string>> userDefinedConfig = new Dictionary<string, List<string>>();  
            // 建立 Word 合併資料欄位
            DataTable dt = new DataTable();
            dt.Columns.Add("班級", typeof(string));
            dt.Columns.Add("姓名", typeof(string));
            dt.Columns.Add("學年度", typeof(string));
            dt.Columns.Add("學期", typeof(string));
            dt.Columns.Add("獎勵內容", typeof(string));
            SchoolYear = SmartSchool.Customization.Data.SystemInformation.SchoolYear;
            if (SmartSchool.Customization.Data.SystemInformation.Semester == 1)
                Semester = "上學期";
            else
                Semester = "下學期";
           

            bool chkdata1 = false;
                
            string AwardString = "";
            int rankXs = 0;
            int.TryParse(txtBox01.Text, out rankXs);
            int ScoreXs = 0;
            int.TryParse(txtBox02.Text, out ScoreXs);
            int ScoreX2 = 0;
            int.TryParse(txtBox03.Text, out ScoreX2);
            if (rb02.Checked==false || rankXs<=0)
                rankXs=2000;//未填入名次或全部時,依2000名為最大值
            foreach (ClassRecord cr in ClassRecs)
            {
                foreach (StudentData sd in StudDatas)
                {
                    if (cr.ClassName == sd.ClassName)
                    {

                        chkdata1 = false;  //是否有資料
                        AwardString = "";  //獎勵內容
                        // 產生合併資料 

                        if (sd.SumScore != -1 || sd.SumScoreA != -1 || sd.AvgScore != -1 || sd.AvgScoreA != -1 )
                            chkdata1 = true;
                        if (lbxSubjct.Visible == true && cboSortType.Text == "科目排名")                            {
                                
                            foreach (ExamData ed in sd.lstStudExamScore)
                            {
                                if (cbYear.Checked == true && cbClass.Checked == true)
                                    if (((ed.SubjectName + ed.SubjectLevel) == lbxSubjct.Text) && (ed.ExamName == ExName) && ed.YearScoreRank <= rankXs && ed.ClassScoreRank <= rankXs)
                                        AwardString = AwardString = "參加" + cboExam.Text + "獲得全校" + NumToChiChar(int.Parse(sd.ClassYear)) + "年級" + ed.SubjectName + "科第" + NumToChiChar(ed.YearScoreRank) + "名,全班第" + NumToChiChar(ed.ClassScoreRank) + "名,";
                                    else
                                        chkdata1 = false;
                                if (cbClass.Checked == false && cbYear.Checked == true)
                                    if (((ed.SubjectName + ed.SubjectLevel) == lbxSubjct.Text) && (ed.ExamName == ExName) &&ed.YearScoreRank <= rankXs)
                                        AwardString = AwardString = "參加" + cboExam.Text + "獲得全校" + NumToChiChar(int.Parse(sd.ClassYear)) + "年級" + ed.SubjectName + "科第" + NumToChiChar(ed.YearScoreRank) + "名,";                                                
                                    else
                                        chkdata1 = false;
                                if (cbClass.Checked == true && cbYear.Checked == false)
                                    if (((ed.SubjectName + ed.SubjectLevel) == lbxSubjct.Text) && (ed.ExamName == ExName) &&ed.ClassScoreRank <= rankXs)                                                    
                                        AwardString = AwardString = "參加" + cboExam.Text + "獲得全班" + ed.SubjectName + "科第" + NumToChiChar(ed.ClassScoreRank) + "名,";
                                    else
                                        chkdata1 = false;
                            }
                            if (AwardString!="")
                                AwardString = AwardString.Substring(0, AwardString.Length - 1);
                                
                        }
                        if (cboSortType.Text == "總分排名" && cbClass.Checked == true && cbYear.Checked == false && sd.ClassSumRank<=rankXs )
                            AwardString = "參加" + cboExam.Text + "獲得全班第" + NumToChiChar((int)sd.ClassSumRank) + "名";
                        if (cboSortType.Text == "總分排名" && cbClass.Checked == true && cbYear.Checked == true && sd.YearSumRank <= rankXs && sd.ClassSumRank<=rankXs )
                            AwardString = "參加" + cboExam.Text + "獲得全校" + NumToChiChar(int.Parse(sd.ClassYear)) + "年級第" + NumToChiChar((int)sd.YearSumRank) + "名,全班第" + NumToChiChar((int)sd.ClassSumRank) + "名";
                        if (cboSortType.Text == "總分排名" && cbClass.Checked == false && cbYear.Checked == true && sd.YearSumRank <= rankXs)
                            AwardString = "參加" + cboExam.Text + "獲得全校" + NumToChiChar(int.Parse(sd.ClassYear)) + "年級第" + NumToChiChar((int)sd.YearSumRank) + "名";
                        if (cboSortType.Text == "加權總分排名" && cbClass.Checked == true && cbYear.Checked == false && sd.ClassSumRankA <= rankXs)
                            AwardString = "參加" + cboExam.Text + "獲得全班第" + NumToChiChar((int)sd.ClassSumRankA) + "名";
                        if (cboSortType.Text == "加權總分排名" && cbClass.Checked == true && cbYear.Checked == true && sd.YearSumRankA <= rankXs && sd.ClassSumRankA<=rankXs )
                            AwardString = "參加" + cboExam.Text + "獲得全校" + NumToChiChar(int.Parse(sd.ClassYear)) + "年級第" + NumToChiChar((int)sd.YearSumRankA) + "名,全班第" + NumToChiChar((int)sd.ClassSumRankA) + "名";
                        if (cboSortType.Text == "加權總分排名" && cbClass.Checked == false && cbYear.Checked == true && sd.YearSumRankA <= rankXs)
                            AwardString = "參加" + cboExam.Text + "獲得全校" + NumToChiChar(int.Parse(sd.ClassYear)) + "年級第" + NumToChiChar((int)sd.YearSumRankA) + "名";
                        if (cboSortType.Text == "平均排名" && cbClass.Checked == true && cbYear.Checked == false && sd.ClassAvgRank <= rankXs)
                            AwardString = "參加" + cboExam.Text + "獲得全班第" + NumToChiChar((int)sd.ClassAvgRank) + "名";
                        if (cboSortType.Text == "平均排名" && cbClass.Checked == true && cbYear.Checked == true && sd.YearAvgRank <= rankXs && sd.ClassAvgRank<=rankXs )
                            AwardString = "參加" + cboExam.Text + "獲得全校" + NumToChiChar(int.Parse(sd.ClassYear)) + "年級第" + NumToChiChar((int)sd.YearAvgRank) + "名,全班第" + NumToChiChar((int)sd.ClassAvgRank) + "名";
                        if (cboSortType.Text == "平均排名" && cbClass.Checked == false && cbYear.Checked == true && sd.YearAvgRank <= rankXs)
                            AwardString = "參加" + cboExam.Text + "獲得全校" + NumToChiChar(int.Parse(sd.ClassYear)) + "年級第" + NumToChiChar((int)sd.YearAvgRank) + "名";
                        if (cboSortType.Text == "加權平均排名" && cbClass.Checked == true && cbYear.Checked == false && sd.ClassAvgRankA <=rankXs )
                            AwardString = "參加" + cboExam.Text + "獲得全班第" + NumToChiChar((int)sd.ClassAvgRankA) + "名";
                        if (cboSortType.Text == "加權平均排名" && cbClass.Checked == true && cbYear.Checked == true && sd.ClassAvgRankA <=rankXs && sd.YearAvgRankA <=rankXs )
                            AwardString = "參加" + cboExam.Text + "獲得全校" + NumToChiChar(int.Parse(sd.ClassYear)) + "年級第" + NumToChiChar((int)sd.YearAvgRankA) + "名,全班第" + NumToChiChar((int)sd.ClassAvgRankA) + "名";
                        if (cboSortType.Text == "加權平均排名" && cbClass.Checked == false && cbYear.Checked == true && sd.YearAvgRankA <=rankXs )
                            AwardString = "參加" + cboExam.Text + "獲得全校" + NumToChiChar(int.Parse(sd.ClassYear)) + "年級第" + NumToChiChar((int)sd.YearAvgRankA) + "名";
                        if (rb03.Checked == true && sd.AvgScoreA>=ScoreXs)
                            AwardString = "參加" + cboExam.Text + "加權平均"+ScoreXs+"分以上";
                        if (rb04.Checked == true && sd.ClassAvgRankAS <= 3 && sd.ClassAvgRankAS >= 0 && (sd.AvgScoreA - sd.AvgScoreA1) > 0)
                            AwardString = "參加" + cboExam.Text + "獲得加權平均進步名次第" + NumToChiChar((int)(sd.ClassAvgRankAS)) + "名";
                        if (rb05.Checked == true && sd.ClassAvgRankAD <= 3 && sd.ClassAvgRankAD >= 0 && (sd.AvgScoreA - sd.AvgScoreA1) > 0)
                            AwardString = "參加" + cboExam.Text + "獲得加權平均進步名次第" + NumToChiChar((int)(sd.ClassAvgRankAD)) + "名";
                        if (rb06.Checked == true && (sd.AvgScoreA - sd.AvgScoreA1 >= ScoreX2) && (sd.AvgScoreA != -1 && sd.AvgScoreA1 != -1))
                            AwardString = "參加" + cboExam.Text + "加權平均進步"+ScoreX2+"分以上";
                        if (chkdata1 == true && AwardString !="")
                        {
                            dt.Rows.Add(
                                sd.ClassName.Substring(0,4),
                                sd.Name,
                                SchoolYear,
                                Semester,
                                AwardString
                                );

                        }
                            

                    }
                }
            }





            //template.MailMerge.MergeField += new MergeFieldEventHandler(MailMerge_MergeField);
            template.MailMerge.Execute(dt);

            DirectoryInfo dir = new DirectoryInfo(Application.StartupPath + "/Reports");
            if (!dir.Exists)
                dir.Create();
            template.Save(dir.FullName + "/獎狀.doc", SaveFormat.Doc);
            Process.Start(dir.FullName + "/獎狀.doc");          

        }

        private void chkSelAll_CheckedChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem selSubject in lstSubject.Items)
            {
                if (chkSelAll.Checked == true)
                    selSubject.Checked = true;
                else
                    selSubject.Checked = false;
            }
        }

        private void rb01_CheckedChanged(object sender, EventArgs e)
        {
            if (rb01.Checked)
            {
                cboExamlst.Visible=false;
                lblExamlst.Visible=false;
                lblSortType.Visible = true;
                cboSortType.Visible = true;
                cbYear.Visible = true;
                cbClass.Visible = true;
                groupBox1.Visible = true;
                cboExamlst.Text = "";
                cboExamlst.SelectedIndex = -1;
            }
            
        }

        private void rb02_CheckedChanged(object sender, EventArgs e)
        {
            if (rb02.Checked)
            {
                cboExamlst.Visible = false;
                lblExamlst.Visible = false;
                lblSortType.Visible = true;
                cboSortType.Visible = true;
                cbYear.Visible = true;
                cbClass.Visible = true;
                groupBox1.Visible = true;
                cboExamlst.Text = "";
                cboExamlst.SelectedIndex = -1;
            }
        }

        private void rb03_CheckedChanged(object sender, EventArgs e)
        {
            if (rb03.Checked)
            {
                cboExamlst.Visible = false;
                lblExamlst.Visible = false;
                lblSortType.Visible = false;
                cboSortType.Visible = false;
                cbYear.Visible = false;
                cbClass.Visible = false;
                groupBox1.Visible = false;
                cboExamlst.Text = "";
                cboExamlst.SelectedIndex = -1;
                cboSortType.Text = "";
            }
        }

        private void rb04_CheckedChanged(object sender, EventArgs e)
        {
            if (rb04.Checked)
            {
                cboExamlst.Visible = true;
                lblExamlst.Visible = true;
                lblSortType.Visible = false;
                cboSortType.Visible = false;
                cbYear.Visible = false;
                cbClass.Visible = false;
                groupBox1.Visible = false;
                cboSortType.Text = "";
            }
        }

        private void rb05_CheckedChanged(object sender, EventArgs e)
        {
            if (rb05.Checked)
            {
                cboExamlst.Visible = true;
                lblExamlst.Visible = true;
                lblSortType.Visible = false;
                cboSortType.Visible = false;
                cbYear.Visible = false;
                cbClass.Visible = false;
                groupBox1.Visible = false;
                cboSortType.Text = "";
            }
        }

        private void rb06_CheckedChanged(object sender, EventArgs e)
        {
            if (rb06.Checked)
            {
                cboExamlst.Visible = true;
                lblExamlst.Visible = true;
                lblSortType.Visible = false;
                cboSortType.Visible = false;
                cbYear.Visible = false;
                cbClass.Visible = false;
                groupBox1.Visible = false;
                cboSortType.Text = "";
            }
        }

        private void cboExamlst_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbxSubjct.Items.Clear();
            lstSubject.Items.Clear();
            List<string> SubjectName = new List<string>();
            chkSelAll.Checked = false;
            if (cboExam.Text != "")
            {
                foreach (ExamSubj es in ExSubj)
                {
                    if (cboExam.Text == es.ExamName)
                        lbxSubjct.Items.Add(es.SubjName + es.SubjLevel);
                }

                foreach (ExamSubj es in ExSubj)
                {
                    if (cboExam.Text == es.ExamName)
                    {
                        lstSubject.Items.Add(es.SubjName + es.SubjLevel);
                        SubjectName.Add(es.SubjName + es.SubjLevel);
                    }
                }
            }
            if (cboExamlst.Text != "")
            {
                foreach (ExamSubj es in ExSubj)
                {
                    if (cboExamlst.Text == es.ExamName)
                        if (!SubjectName.Contains(es.SubjName + es.SubjLevel))
                            lstSubject.Items.Add(es.SubjName + es.SubjLevel);
                }
            }
            
        }
        //依名次排序副程式
        private int CompareCode(StudentData a, StudentData b)
        {            
            switch  (cboReportSortType.Text)
            {
                case "班級總分排名":
                    return a.ClassSumRank.CompareTo(b.ClassSumRank);
                case "班級加權總分排名":
                     return a.ClassSumRankA.CompareTo(b.ClassSumRankA);
                case "班級平均排名":
                     return a.ClassAvgRank.CompareTo(b.ClassAvgRank);
                case "班級加權平均排名":
                     return a.ClassAvgRankA.CompareTo(b.ClassAvgRankA);
                case "總分年排名":
                     return a.YearSumRank.CompareTo(b.YearSumRank);
                case "加權總分年排名":
                     return a.YearSumRankA.CompareTo(b.YearSumRankA);
                case "加權平均年排名":
                     return a.YearAvgRankA.CompareTo(b.YearAvgRankA);
                case "班級加權平均排名進步":
                     return a.ClassAvgRankAD.CompareTo(b.ClassAvgRankAD);
                case "班級加權平均成績進步排名":
                     return a.ClassAvgRankAS.CompareTo(b.ClassAvgRankAS);
            }
            return 0;

        }

        
    }
}
