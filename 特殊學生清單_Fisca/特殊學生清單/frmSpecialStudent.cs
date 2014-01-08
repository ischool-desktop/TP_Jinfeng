using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Customization.Data;
using SmartSchool.Customization.PlugIn.Report;
using SmartSchool.Customization.PlugIn;
using SmartSchool.Customization.Data.StudentExtension;
using Aspose.Cells;
using System.Xml;
using SmartSchool.Common;
using SHSchool.Data;


namespace 特殊學生清單
{
    public partial class frmSpecialStudent :BaseForm
    {
        private SpecialStudentConfig mconfig;
        private Aspose.Cells.Workbook SpecialStudentBook;
        public frmSpecialStudent()
        {
            InitializeComponent();

            #region 處理假別

            SmartSchool.Customization.Data.SystemInformation.getField("Absence");
            XmlElement absenceXml = SmartSchool.Customization.Data.SystemInformation.Fields["Absence"] as XmlElement;

            AbsenceList.Items.Clear();

            foreach (XmlElement absence in absenceXml.SelectNodes("Absence"))
            {
                 AbsenceList.Items.Add(new ListViewItem(absence.GetAttribute("Name")));
            }

            for (int x = 0; x < AbsenceList.Items.Count; x++)
            {
                AbsenceList.Items[x].Checked = true;
            }
            #endregion
        }        
       

        private void btnReport_Click(object sender, EventArgs e)
        {
           
            
            System.Windows.Forms.SaveFileDialog saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            saveFileDialog1.FileName = "特殊學生清單.xls";
            saveFileDialog1.AddExtension = true;
            saveFileDialog1.DefaultExt = "xls";

            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK) 
            {              
                mconfig = new SpecialStudentConfig(this);
                if (mconfig.ReportType == 0)
                    MessageBox.Show("未選定報表類別，無法產生清單");
                else
                {
                    if (mconfig.ReportType == 5 && (mconfig.Period <= 0 || mconfig.AbsenceList == null))
                        MessageBox.Show("輸入資料不完整，無法產生清單");
                    else
                    {
                        if (mconfig.Date && (mconfig.StartDate == null || mconfig.EndDate == null))
                            MessageBox.Show("輸入資料不完整，無法產生清單");
                        else
                        {
                            ProcessDocument();
                            try
                            {
                                SpecialStudentBook.Save(saveFileDialog1.FileName);
                                System.Diagnostics.Process.Start(saveFileDialog1.FileName);
                            }
                            catch
                            {
                                System.Windows.Forms.MessageBox.Show("指定路徑無法存取。", "建立檔案失敗", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }           
            
        }
        public int ProcessDocument()
        {
            
            AccessHelper helper = new AccessHelper(); //helper物件用來取得ischool相關資料。
            //建立Workbook物件
            SpecialStudentBook = new Aspose.Cells.Workbook();

            Aspose.Cells.Worksheet SpecialStudentSheet = SpecialStudentBook.Worksheets[0];
            //取得使用者所選取的班級。
            List<ClassRecord> classes = helper.ClassHelper.GetSelectedClass();

            //德行加減分Helper
            SmartSchool.Evaluation.AngelDemonComputer computer = new SmartSchool.Evaluation.AngelDemonComputer(); ;


            int row;
            decimal Score;
            int period;
            Boolean SpecialStudent;
            RewardStatistics RS;
            string description="";
            SpecialStudentSheet.Cells[0, 0].PutValue(SmartSchool.Customization.Data.SystemInformation.SchoolChineseName);
            if (mconfig.Semester == true && mconfig.MequialD == false)
                description = "(在學期間累計)";
            if (mconfig.MequialD == true && mconfig.Semester == true)
                description = "(在學期間累計且功過相抵)";
            if (mconfig.MequialD == true && mconfig.Semester == false)
                description = "(功過相抵)";
            switch (mconfig.ReportType) 
            {
                case 1:
                   
                    SpecialStudentSheet.Cells[1, 0].PutValue("小過三支以上"+description );
                    break;
                case 2:
                    SpecialStudentSheet.Cells[1, 0].PutValue("出缺勤扣分超過21分者");
                    break;
                case 3:
                    SpecialStudentSheet.Cells[1, 0].PutValue("德行成績不及格");
                    break;
                case 4:
                    SpecialStudentSheet.Cells[1, 0].PutValue("滿三大過"+description );
                    break;
                case 5:
                    SpecialStudentSheet.Cells[1, 0].PutValue("全學期缺席節次超過" + mconfig.Period + "節");
                    break;
                case 6:
                    SpecialStudentSheet.Cells[1, 0].PutValue("留校查看名單" );
                    break;
            }
           
            SpecialStudentSheet.Cells[2, 0].PutValue("班級");
            SpecialStudentSheet.Cells[2, 1].PutValue("學號");
            SpecialStudentSheet.Cells[2, 2].PutValue("座號");
            SpecialStudentSheet.Cells[2, 3].PutValue("姓名");
            SpecialStudentSheet.Cells[2, 4].PutValue("大功");
            SpecialStudentSheet.Cells[2, 5].PutValue("小功");
            SpecialStudentSheet.Cells[2, 6].PutValue("嘉獎");
            SpecialStudentSheet.Cells[2, 7].PutValue("大過");
            SpecialStudentSheet.Cells[2, 8].PutValue("小過");
            SpecialStudentSheet.Cells[2, 9].PutValue("警告");
            for (int i = 0; i <= 9; i++)
            {
                SpecialStudentSheet.Cells.Merge(2, i, 2, 1);
                SpecialStudentSheet.Cells[2, i].Style.HorizontalAlignment = TextAlignmentType.Center;
                SpecialStudentSheet.Cells[2, i].Style.VerticalAlignment = TextAlignmentType.Center;                
            }
            //取得缺曠類別            

            #region 建立字典&範本
            
            Dictionary<string, int> AttList = new Dictionary<string, int>();
            Dictionary<string, string> dicPeriodType = new Dictionary<string, string>();
            List <string> PeriodType = new List<string>();
            List <string> AbsenceC = new List<string>();
            int test1 = 10;
            
            foreach (SHPeriodMappingInfo var in SHPeriodMapping.SelectAll())
            {
                if (!dicPeriodType.ContainsKey(var.Name))
                    dicPeriodType.Add(var.Name, var.Type);
                //string name = var.GetAttribute("Name"); //節次名稱
                //string type = var.GetAttribute("Type"); //類型
            }


            SmartSchool.Customization.Data.SystemInformation.getField("Absence");
            XmlElement absenceXml = SmartSchool.Customization.Data.SystemInformation.Fields["Absence"] as XmlElement;            

            foreach (XmlElement absence in absenceXml.SelectNodes("Absence"))            
                AbsenceC.Add(absence.GetAttribute("Name"));
           
            SmartSchool.Customization.Data.SystemInformation.getField("Period");
            absenceXml = SmartSchool.Customization.Data.SystemInformation.Fields["Period"] as XmlElement;

            foreach (XmlElement absence in absenceXml.SelectNodes("Period"))
                if (!PeriodType.Contains(absence.GetAttribute("Type")))
                   PeriodType.Add(absence.GetAttribute("Type"));
            

            //foreach (SmartSchool.Evaluation.AngelDemonComputer.UsefulPeriodAbsence var in computer.UsefulPeriodAbsences)
            //{
            //    if (!PeriodType.Contains(var.Period))                
            //        PeriodType.Add(var.Period); 
            //    if (!AbsenceC.Contains(var.Absence))
            //        AbsenceC.Add(var.Absence);
            //    if (!AttList.ContainsKey (var.Period + var.Absence))
            //        AttList.Add(var.Period + var.Absence,0);
            //}
            foreach (string p in PeriodType)
            {
                SpecialStudentSheet.Cells[2, test1].PutValue(p);
                foreach (string a in AbsenceC)
                {
                    SpecialStudentSheet.Cells[3, test1].PutValue(a);
                    AttList.Add(p+a, 0);
                    test1++;
                }
                SpecialStudentSheet.Cells.Merge(2, test1 -AbsenceC.Count , 1, AbsenceC.Count);
                SpecialStudentSheet.Cells[2, test1  -AbsenceC.Count].Style.HorizontalAlignment = TextAlignmentType.Center;
                SpecialStudentSheet.Cells[2, test1 - AbsenceC.Count].Style.VerticalAlignment = TextAlignmentType.Center;                
            }
            
            #endregion
            if (mconfig.ReportType == 3)
            {
                SpecialStudentSheet.Cells[2, test1 ].PutValue("德行");
                SpecialStudentSheet.Cells.Merge(2, test1, 2, 1);
                SpecialStudentSheet.Cells[2, test1].Style.HorizontalAlignment = TextAlignmentType.Center;
                SpecialStudentSheet.Cells[2, test1].Style.VerticalAlignment = TextAlignmentType.Center;
                test1 += 1;
            }
            //循訪班級記錄
            row = 4;
            foreach (ClassRecord crecord in classes)
            {
                //將班級學生填入獎懲記錄
                helper.StudentHelper.FillReward(crecord.Students);

                //將班級學生填入缺曠記錄
                if (mconfig.Semester==true)
                    helper.StudentHelper.FillAttendance(crecord.Students);
                else
                    helper.StudentHelper.FillAttendance(intSchoolYear.Value, (cboSemester.Text=="上學期"? 1:2), crecord.Students);
                //填入學生學期分項成績   
                helper.StudentHelper.FillSemesterEntryScore(false,crecord.Students);
                //取得該班學生資料，要排序不能使用 crecord.Students，改用Students
                List<StudentRecord> Students = crecord.Students;
                //將學生依學號排序
                if (mconfig.SortKey == "StudentNumber")               
                    Students.Sort(CompareSNum);
                
                //循訪每位學生記錄，並建立SpecialStudent清單來產生報表
                
                foreach (StudentRecord student in Students)
                {
                    Score = 0;
                    SpecialStudent = false;
                    period = 0;
                    //計算獎懲次數                    
                    if (mconfig.Semester == true)
                        RS = new RewardStatistics(student.RewardList, 0,mconfig);
                    else
                        if (chkDate.Checked)
                            RS = new RewardStatistics(student.RewardList, 2,mconfig);
                        else
                            RS = new RewardStatistics(student.RewardList, 1,mconfig);                    
                    switch (mconfig.ReportType)
                    {
                        case 1:  //小過三支以上
                            if (mconfig.MequialD == true)
                                if ((RS.FaultACount * 9 + RS.FaultBCount * 3 + RS.FaultCCount - RS.AwardACount * 9 - RS.AwardBCount * 3- RS.AwardCCount) >= 9)
                                    SpecialStudent = true;
                                else
                                    SpecialStudent = false;
                            else
                                if ((RS.FaultACount * 9 + RS.FaultBCount * 3 + RS.FaultCCount) >= 9)
                                    SpecialStudent = true;
                                else
                                    SpecialStudent = false;
                            break;

                        case 2:   //出缺勤扣分超過21分者
                            //計算缺曠節次
                            foreach (AttendanceInfo w in student.AttendanceList)
                            {
                                if (w.PeriodType == "")
                                {
                                    if (dicPeriodType.ContainsKey(w.Period))                                       
                                       if (AttList.ContainsKey(dicPeriodType[w.Period] + w.Absence))
                                         AttList[dicPeriodType[w.Period] + w.Absence]++;
                                }
                                else
                                  if (AttList.ContainsKey(w.PeriodType + w.Absence))
                                      AttList[w.PeriodType + w.Absence]++;
                            }
                            // 計算缺曠扣分
                            foreach (string p in PeriodType)
                                foreach (string a in AbsenceC )
                                    Score = Score + computer.ComputeAttendanceScore(p,a,AttList[p+a]);
                            if (Score <= -21)
                                SpecialStudent = true;
                            else
                            {
                                SpecialStudent = false;
                                foreach (string p in PeriodType)
                                    foreach (string a in AbsenceC)
                                        AttList[p + a] = 0;
                            }
                            break;
                        case 3:   //德行成績不及格  
                            Score= 0;
                            
                            List<SemesterEntryScoreInfo> SemScore= new List<SemesterEntryScoreInfo>();
                            SemScore = student.SemesterEntryScoreList;
                            if (SemScore.Count > 0)
                                foreach (SemesterEntryScoreInfo si in SemScore)
                                    if (si.SchoolYear == intSchoolYear.Value && si.Semester == (cboSemester.Text == "上學期" ? 1 : 2) && si.Entry=="德行")
                                        Score = si.Score; 
                            //helper.StudentHelper.FillField("本學期德行表現成績", student);
                            //XmlElement element = student.Fields["本學期德行表現成績"] as XmlElement;
                            //if (element != null && decimal.TryParse (element.GetAttribute("Score"),out Score))
                            //    Score = decimal.Parse(element.GetAttribute("Score"));                            
                            if (Score < 60)
                                SpecialStudent = true;
                            else
                                SpecialStudent = false;                           
                            
                            break;
                        case 4:    //大過三支以上                            
                            if (mconfig.MequialD == true)
                                if ((RS.FaultACount * 9 + RS.FaultBCount * 3 + RS.FaultCCount - RS.AwardACount * 9 -RS.AwardBCount * 3 - RS.AwardCCount) >= 27)
                                    SpecialStudent = true;
                                else
                                    SpecialStudent = false;
                            else
                                if ((RS.FaultACount * 9 + RS.FaultBCount * 3 + RS.FaultCCount) >= 27)
                                    SpecialStudent = true;
                                else
                                    SpecialStudent = false;
                            break;
                        case 5: //全學期缺席節次超過指定節次                            
                            foreach (AttendanceInfo w in student.AttendanceList)
                                if (mconfig.AbsenceList.Contains(w.Absence) == true)
                                    if (mconfig.Date)
                                    {
                                        if (w.OccurDate >= mconfig.StartDate && w.OccurDate <= mconfig.EndDate)
                                            period++;
                                    }
                                    else
                                        period++;
                            if (period > mconfig.Period)
                                SpecialStudent = true;
                            else
                                SpecialStudent = false;
                            break;
                        case 6: //留校查看
                            List<RewardInfo> RewardInfo = new List<RewardInfo>();
                            RewardInfo = student.RewardList;
                            SpecialStudent = false;
                            int SchoolYear = intSchoolYear.Value;
                            int Semester = (cboSemester.Text=="上學期"? 1:2);
                            foreach (RewardInfo r in RewardInfo)
                            {
                                if (r.SchoolYear == SchoolYear && r.Semester == Semester && r.UltimateAdmonition == true)
                                {
                                    if (mconfig.Date)
                                    {
                                        if (r.OccurDate >= mconfig.StartDate && r.OccurDate <= mconfig.EndDate)
                                            SpecialStudent = true;
                                    }
                                    else
                                        SpecialStudent = true;
                                }
                                
                            }
                            break;
                           
                    }
                    //符合特殊學生資格
                    if (SpecialStudent == true)
                    {
                        SpecialStudentSheet.Cells[row, 0].PutValue(student.RefClass.ClassName);
                        SpecialStudentSheet.Cells[row, 1].PutValue(student.StudentNumber);
                        SpecialStudentSheet.Cells[row, 2].PutValue(student.SeatNo );
                        SpecialStudentSheet.Cells[row, 3].PutValue(student.StudentName );
                        SpecialStudentSheet.Cells[row, 4].PutValue(RS.AwardACount);
                        SpecialStudentSheet.Cells[row, 5].PutValue(RS.AwardBCount);
                        SpecialStudentSheet.Cells[row, 6].PutValue(RS.AwardCCount);
                        SpecialStudentSheet.Cells[row, 7].PutValue(RS.FaultACount);
                        SpecialStudentSheet.Cells[row, 8].PutValue(RS.FaultBCount);
                        SpecialStudentSheet.Cells[row, 9].PutValue(RS.FaultCCount);                        
                        #region 處理缺曠內容
                        //計算缺曠節次
                        if (mconfig.ReportType != 2)
                        {
                            foreach (AttendanceInfo w in student.AttendanceList)
                                //if (mconfig.Date)
                                //{
                                //    if (w.OccurDate >= mconfig.StartDate && w.OccurDate <= mconfig.EndDate)
                                //        if (AttList.ContainsKey(w.PeriodType + w.Absence))
                                //            AttList[w.PeriodType + w.Absence]++;
                                //}
                                //else
                                if (w.PeriodType == "")
                                {
                                    if (dicPeriodType.ContainsKey(w.Period))
                                        if (AttList.ContainsKey(dicPeriodType[w.Period] + w.Absence))
                                            AttList[dicPeriodType[w.Period] + w.Absence]++;
                                }
                                else
                                    if (AttList.ContainsKey(w.PeriodType + w.Absence))
                                        AttList[w.PeriodType + w.Absence]++;
                        }
                        int DicNum = 10;
                        //
                        foreach (string p in PeriodType)
                            foreach (string a in AbsenceC)
                             {
                               SpecialStudentSheet.Cells[row, DicNum].PutValue(AttList[p+a]);
                               DicNum++;
                             }
                         foreach (string p in PeriodType)
                             foreach (string a in AbsenceC)
                                 AttList[p + a] = 0;
                        #endregion
                         if (mconfig.ReportType == 3)
                             SpecialStudentSheet.Cells[row, DicNum].PutValue(Score);
                        row++;
                    }                   

                }
            }
            if (mconfig.ReportType == 3)
            {
                SpecialStudentSheet.Cells.Merge(0, 0, 1, AttList.Count + 11);//合併儲存格
                SpecialStudentSheet.Cells.Merge(1, 0, 1, AttList.Count + 11);
            }
            else
            {
                SpecialStudentSheet.Cells.Merge(0, 0, 1, AttList.Count + 10);//合併儲存格
                SpecialStudentSheet.Cells.Merge(1, 0, 1, AttList.Count + 10);
            }
            SpecialStudentSheet.Cells[0,0].Style.HorizontalAlignment = TextAlignmentType.Center;//水平置中
            SpecialStudentSheet.Cells[1,0].Style.HorizontalAlignment = TextAlignmentType.Center;//水平置中
            SpecialStudentSheet.Cells[0, 0].Style.Font.Size = 18;
            SpecialStudentSheet.Cells[1, 0].Style.Font.Size = 18;
            SpecialStudentSheet.Cells.SetRowHeight(0, 24);
            SpecialStudentSheet.Cells.SetRowHeight(1, 24);
            if (B4.Checked == true)
                SpecialStudentSheet.PageSetup.PaperSize = PaperSizeType.PaperB4; //設定紙張
            else
                SpecialStudentSheet.PageSetup.PaperSize = PaperSizeType.PaperA4;

            SpecialStudentSheet.PageSetup.Orientation = PageOrientationType.Landscape;//設定橫向列印
            SpecialStudentSheet.PageSetup.PrintTitleRows  = "$1:$4"; //設定跨頁標題
            SpecialStudentSheet.PageSetup.FitToPagesWide=1;   //調整為一頁寬 
            SpecialStudentSheet.PageSetup.FitToPagesTall = (row - (row % 35)) / 35 + 1;   //調整頁高
            //設定邊界
            SpecialStudentSheet.PageSetup.BottomMargin = 1;
            SpecialStudentSheet.PageSetup.TopMargin = 1;
            SpecialStudentSheet.PageSetup.LeftMargin = 1;
            SpecialStudentSheet.PageSetup.RightMargin = 1;
            //劃框線            
            for (int i = 2; i < row; i++)
                for (int j = 0; j < test1; j++)
                {
                    SpecialStudentSheet.Cells[i, j].Style.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
                    SpecialStudentSheet.Cells[i, j].Style.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
                    SpecialStudentSheet.Cells[i, j].Style.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
                    SpecialStudentSheet.Cells[i, j].Style.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
                    SpecialStudentSheet.Cells[i, j].Style.Font.Size = 12;
                }
            //SpecialStudentSheet.AutoFitRows(); //自動調整列寬
            SpecialStudentSheet.AutoFitColumns(); //自動調整欄寬
            //調整功過欄寬，合併欄位無法自動調整
            for (int i = 4; i <= 9; i++)
                SpecialStudentSheet.Cells.SetColumnWidth(i, 6);
            for (int i = 2; i <= SpecialStudentSheet.Cells.MaxDataRow; i++)
                SpecialStudentSheet.Cells.SetRowHeight(i, 20);
            return 0;
        }

        private void Fault_3B_CheckedChanged(object sender, EventArgs e)
        {
            chkSemester.Checked = false;
            chkDate.Checked = false;
            if (Fault_3B.Checked)
            {
                txtPeriod.Visible = false;   //節次
                lblPeriod.Visible = false;
                AbsenceList.Visible  = false; //假別清單
                Absencelbl.Visible = false;
                chkSelAll.Visible = false;
                chkMequialD.Enabled = true;  //功過相抵
                chkSemester.Enabled = true; //學期累計
                chkDate.Enabled = true;
                dtEndDate.Enabled = true;
                dtStartDate.Enabled = true;
            }           

        }

        private void Attendance_21_CheckedChanged(object sender, EventArgs e)
        {
            chkSemester.Checked = false;
            chkDate.Checked = false;
            if (Attendance_21.Checked)
            {
                txtPeriod.Visible = false;   //節次
                lblPeriod.Visible = false;
                AbsenceList.Visible = false; //假別清單
                Absencelbl.Visible = false;
                chkSelAll.Visible = false;
                chkMequialD.Enabled = false;  //功過相抵
                chkSemester.Enabled = false; //學期累計
                chkDate.Enabled = false;
                dtEndDate.Enabled = false;
                dtStartDate.Enabled = false;
            }      
        }

        private void VEScore_60_CheckedChanged(object sender, EventArgs e)
        {
            chkSemester.Checked = false;
            chkDate.Checked = false;
            if (VEScore_60.Checked)
            {
                txtPeriod.Visible = false;  //節次
                lblPeriod.Visible = false;
                AbsenceList.Visible = false; //假別清單
                Absencelbl.Visible = false;
                chkSelAll.Visible = false;
                chkMequialD.Enabled = false;  //功過相抵
                chkSemester.Enabled = false; //學期累計
                chkDate.Enabled = false;
                dtEndDate.Enabled = false;
                dtStartDate.Enabled = false;
                
            }    
        }

        private void Fault_3A_CheckedChanged(object sender, EventArgs e)
        {
            chkSemester.Checked = false;
            chkDate.Checked = false;
            if (Fault_3A.Checked)
            {
                txtPeriod.Visible = false;   //節次
                lblPeriod.Visible = false;
                AbsenceList.Visible = false; //假別清單
                Absencelbl.Visible = false;
                chkSelAll.Visible = false;
                chkMequialD.Enabled = true;  //功過相抵
                chkSemester.Enabled = true; //學期累計
                chkDate.Enabled = false;
                dtEndDate.Enabled = false;
                dtStartDate.Enabled = false;
            }    
        }

        private void Attendance_Half_CheckedChanged(object sender, EventArgs e)
        {
            chkSemester.Checked = false;
            chkDate.Checked = false;
            if (Attendance_Half.Checked)
            {
                txtPeriod.Visible = true;   //節次
                lblPeriod.Visible = true;
                AbsenceList.Visible = true; //假別清單
                Absencelbl.Visible = true;
                chkSelAll.Visible = true;
                chkMequialD.Enabled = false;  //功過相抵
                chkSemester.Enabled = false; //學期累計 
                chkDate.Enabled = true;
                dtEndDate.Enabled = true;
                dtStartDate.Enabled = true;
            }    
        }
        //依學號排序副程式
        static int CompareSNum(StudentRecord a, StudentRecord b)
        {
            return a.StudentNumber.CompareTo(b.StudentNumber);
        }
        private void chkSelAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSelAll.Checked == true)
            {
                for (int x = 0; x < AbsenceList.Items.Count; x++)
                {
                    AbsenceList.Items[x].Checked = true;
                }
            }
            else
            {
                for (int x = 0; x < AbsenceList.Items.Count; x++)
                {
                    AbsenceList.Items[x].Checked = false;
                }
            }
        }

        private void SpecialStudent_CheckedChanged(object sender, EventArgs e)
        {
            chkSemester.Checked = false;
            chkDate.Checked = false;
            if (SpecialStudent.Checked)
            {
                txtPeriod.Visible = false;  //節次
                lblPeriod.Visible = false;
                AbsenceList.Visible = false; //假別清單
                Absencelbl.Visible = false;
                chkSelAll.Visible = false;
                chkMequialD.Enabled = false;  //功過相抵
                chkSemester.Enabled = false; //學期累計 
                chkDate.Enabled = true;
                dtEndDate.Enabled = true;
                dtStartDate.Enabled = true;
            }    
        }

        private void frmSpecialStudent_Load(object sender, EventArgs e)
        {
            intSchoolYear.Value = SmartSchool.Customization.Data.SystemInformation.SchoolYear;
            int Semester = SmartSchool.Customization.Data.SystemInformation.Semester;
            if (Semester==1)
                cboSemester.Text = "上學期";
            else
                cboSemester.Text="下學期";
        }

        private void chkSemester_CheckedChanged(object sender, EventArgs e)
        {
            chkDate.Checked= false;
            if (chkSemester.Checked)
            {
                chkDate.Enabled = false;
                dtEndDate.Enabled = false;
                dtStartDate.Enabled = false;
            }
            else
            {
                chkDate.Enabled = true;
                dtEndDate.Enabled = true;
                dtStartDate.Enabled = true;
            }
        }

        
       
        

       
       
        

        
        
    }
}