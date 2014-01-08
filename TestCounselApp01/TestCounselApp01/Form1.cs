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
// 輔導模組
using Counsel_System.DAO;
namespace TestCounselApp01
{
    public partial class Form1 : BaseForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            if (dtEnd.Value == null || dtEnd.IsEmpty == true)
            {
                MsgBox.Show("結束日期不正確");
                return;
            }
            if (dtStart.Value == null || dtStart.IsEmpty == true)
            {
                MsgBox.Show( "開始日期不正確");
                return;
            }
            // 取得所有學生
            List<string> StudentIDList = new List<string>();
            StudentIDList = K12.Presentation.NLDPanels.Student.SelectedSource;
            List<StudentRecord> allStudentList = Student.SelectByIDs(StudentIDList);
            

            UDTTransfer udtTransfer= new UDTTransfer ();
            // 取得學生晤談紀錄
            List<UDT_CounselStudentInterviewRecordDef> StudentInterviewRecordList = udtTransfer.GetCounselStudentInterviewRecordByStudentIDList(StudentIDList);
            Dictionary <string,List <string>>  StudentKindDict=new Dictionary<string,List<string>>();
            Dictionary<string, int> StudentCountP = new Dictionary<string, int>();
            Dictionary<string, int> StudentCountT = new Dictionary<string, int>();
            Dictionary<string, string> TypeKindDict = new Dictionary<string, string>();            
            Workbook wb = new Workbook();
            Style defaultStyle = wb.DefaultStyle;
            defaultStyle.Font.Size = 12;
            defaultStyle.Font.Name = "標楷體";
            wb.DefaultStyle = defaultStyle;
            List<string> TypeKindString = new List<string>(new string[] { "違規", "遲曠", "學習", "生涯", "人際", "休退轉", "家庭", "師生", "情感", "精神", "家暴", "霸凌", "中輟", "性議題", "戒毒", "網路成癮", "情緒障礙","其它" });
            foreach (string TypeKind in TypeKindString)
            {
                if (!StudentCountP.ContainsKey(TypeKind))
                    StudentCountP.Add(TypeKind, 0);
                if (!StudentCountT.ContainsKey(TypeKind))
                    StudentCountT.Add(TypeKind, 0);
            }
            string kind;
            foreach (UDT_CounselStudentInterviewRecordDef data in StudentInterviewRecordList)
            {
                if (data.InterviewDate >= dtStart.Value.Date && data.InterviewDate <= dtEnd.Value.Date)
                {
                    string StudentID = data.StudentID.ToString();
                    // 解析輔導歸類,key:輔導歸類:違規,value=1
                    TypeKindDict = Counsel_System.Utility.GetConvertCounselXMLVal_CounselTypeKind(data.CounselTypeKind);
                    if (!StudentKindDict.ContainsKey(StudentID))
                        StudentKindDict.Add(StudentID, new List<string>());
                   
                    foreach (string TypeKind in TypeKindDict.Keys)
                    {
                        kind=TypeKind.Substring(5);
                        if (!StudentCountP.ContainsKey(kind))
                            StudentCountP.Add(kind, 0);
                        if (!StudentCountT.ContainsKey(kind))
                            StudentCountT.Add(kind, 0);
                        if (!StudentKindDict[StudentID].Contains(kind))
                        {
                            StudentKindDict[StudentID].Add(kind);
                            StudentCountP[kind]++;
                        }
                        StudentCountT[kind]++;
                    }
                }

            }
            //wb.Worksheets.Add();            
            wb.Worksheets[0].Name = "個案記錄輔導歸類";
            wb.Worksheets[0].Cells[0, 0].PutValue(K12.Data.School.ChineseName + "-個案記錄輔導歸類統計");
            wb.Worksheets[0].Cells[1, 0].PutValue(dtStart.Value.Date.ToShortDateString() + "-" + dtEnd.Value.Date.ToShortDateString());
            wb.Worksheets[0].Cells[3, 0].PutValue("類別");           
            wb.Worksheets[0].Cells[4, 0].PutValue("次數");
            wb.Worksheets[0].Cells[5, 0].PutValue("人數");
            int i=1;
            foreach (string TypeKind in StudentCountP.Keys)
            {
                
                wb.Worksheets[0].Cells[3, i].PutValue(TypeKind);
                wb.Worksheets[0].Cells[4, i].PutValue(StudentCountT[TypeKind]);
                wb.Worksheets[0].Cells[5, i].PutValue(StudentCountP[TypeKind]);
                i++;
            }
            for (int j = 0; j < 3; j++)
                for (int k = 0; k < i; k++)
                {
                    
                    wb.Worksheets[0].Cells[j + 3, k].Style.HorizontalAlignment = TextAlignmentType.Center;
                    wb.Worksheets[0].Cells[j + 3, k].Style.VerticalAlignment = TextAlignmentType.Center;
                    wb.Worksheets[0].Cells[j + 3, k].Style.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
                    wb.Worksheets[0].Cells[j + 3, k].Style.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
                    wb.Worksheets[0].Cells[j + 3, k].Style.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
                    wb.Worksheets[0].Cells[j + 3, k].Style.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
                }
            wb.Worksheets[0].Cells.Merge(0,0,1,i);
            wb.Worksheets[0].Cells.Merge(1,0, 1, i);
            wb.Worksheets[0].Cells[0,0].Style.HorizontalAlignment = TextAlignmentType.Center;
            wb.Worksheets[0].Cells[0,0].Style.VerticalAlignment = TextAlignmentType.Center;
            wb.Worksheets[0].Cells[1, 0].Style.HorizontalAlignment = TextAlignmentType.Center;
            wb.Worksheets[0].Cells[1, 0].Style.VerticalAlignment = TextAlignmentType.Center;
            wb.Worksheets[0].Cells[0, 0].Style.Font.Size = 24;
            wb.Worksheets[0].Cells.SetRowHeight(0, 30);
            wb.Worksheets[0].AutoFitColumns();
            wb.Worksheets[0].AutoFitRows();
            wb.Worksheets[0].Cells.SetRowHeight(1, 20);
            wb.Worksheets[0].Cells[1, 0].Style.Font.Size = 16;
            wb.Worksheets[0].Charts.Add(ChartType.Column, 7, 0, 40, i - 1);
            wb.Worksheets[0].Charts[0].Title.Text = K12.Data.School.ChineseName + "-個案記錄輔導歸類統計";
            if (i < 27)
            {
                wb.Worksheets[0].Charts[0].NSeries.Add("B5:" + (char)(64 + i) + "6", false);   //true,false 行或列                    
                wb.Worksheets[0].Charts[0].NSeries.CategoryData = "B4:" + (char)(64 + i) + "4";
            }
            else
            {
                wb.Worksheets[0].Charts[0].NSeries.Add("B5:A" + (char)(64 + i - 26) + "6", false);   //true,false 行或列                    
                wb.Worksheets[0].Charts[0].NSeries.CategoryData = "B4:A" + (char)(64 + i - 26) + "4";
            }           
            wb.Worksheets[0].Charts[0].NSeries[0].Name = "次數";
            wb.Worksheets[0].Charts[0].NSeries[1].Name = "人數";
            wb.Worksheets[0].Charts[0].CategoryAxis.TickLabels.Font.Name="@標楷體";
            wb.Worksheets[0].Charts[0].CategoryAxis.TickLabels.Rotation = -90;
            wb.Worksheets[0].Charts[0].Title.TextFont.Size = 20;
            // 取得個案會議紀錄
            StudentKindDict = new Dictionary<string, List<string>>();
            StudentCountP = new Dictionary<string, int>();
            StudentCountT = new Dictionary<string, int>();
            TypeKindDict = new Dictionary<string, string>();
            List<UDT_CounselCaseMeetingRecordDef> CaseMeetingRecordList = udtTransfer.GetCaseMeetingRecordListByStudentIDList(StudentIDList);
            foreach (string TypeKind in TypeKindString)
            {
                if (!StudentCountP.ContainsKey(TypeKind))
                    StudentCountP.Add(TypeKind, 0);
                if (!StudentCountT.ContainsKey(TypeKind))
                    StudentCountT.Add(TypeKind, 0);
            }
            foreach (UDT_CounselCaseMeetingRecordDef data in CaseMeetingRecordList)
            {
                if (data.MeetingDate >= dtStart.Value.Date && data.MeetingDate <= dtEnd.Value.Date)
                {
                    string StudentID = data.StudentID.ToString();

                    // 解析輔導歸類,key:輔導歸類:違規,value=1
                    TypeKindDict = Counsel_System.Utility.GetConvertCounselXMLVal_CounselTypeKind(data.CounselTypeKind);
                    if (!StudentKindDict.ContainsKey(StudentID))
                        StudentKindDict.Add(StudentID, new List<string>());
                    foreach (string TypeKind in TypeKindDict.Keys)
                    {
                        kind = TypeKind.Substring(5);
                        if (!StudentCountP.ContainsKey(kind))
                            StudentCountP.Add(kind, 0);
                        if (!StudentCountT.ContainsKey(kind))
                            StudentCountT.Add(kind, 0);
                        if (!StudentKindDict[StudentID].Contains(kind))
                        {
                            StudentKindDict[StudentID].Add(kind);
                            StudentCountP[kind]++;
                        }
                        StudentCountT[kind]++;
                    }
                }
            
            }
            wb.Worksheets.Add();
            wb.Worksheets[1].Name = "晤談記錄輔導歸類";
            wb.Worksheets[1].Cells[0, 0].PutValue(K12.Data.School.ChineseName + "-晤談記錄輔導歸類");
            wb.Worksheets[1].Cells[1, 0].PutValue(dtStart.Value.Date.ToShortDateString() + "-" + dtEnd.Value.Date.ToShortDateString());
            wb.Worksheets[1].Cells[3, 0].PutValue("類別");
            wb.Worksheets[1].Cells[4, 0].PutValue("次數");
            wb.Worksheets[1].Cells[5, 0].PutValue("人數");
            i = 1;
            foreach (string TypeKind in StudentCountP.Keys)
            {

                wb.Worksheets[1].Cells[3, i].PutValue(TypeKind);
                wb.Worksheets[1].Cells[4, i].PutValue(StudentCountT[TypeKind]);
                wb.Worksheets[1].Cells[5, i].PutValue(StudentCountP[TypeKind]);
                i++;
            }
            for (int j = 0; j < 3; j++)
                for (int k = 0; k < i; k++)
                {

                    wb.Worksheets[1].Cells[j + 3, k].Style.HorizontalAlignment = TextAlignmentType.Center;
                    wb.Worksheets[1].Cells[j + 3, k].Style.VerticalAlignment = TextAlignmentType.Center;
                    wb.Worksheets[1].Cells[j + 3, k].Style.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
                    wb.Worksheets[1].Cells[j + 3, k].Style.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
                    wb.Worksheets[1].Cells[j + 3, k].Style.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
                    wb.Worksheets[1].Cells[j + 3, k].Style.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
                }
            wb.Worksheets[1].Cells.Merge(0, 0, 1, i);
            wb.Worksheets[1].Cells.Merge(1, 0, 1, i);
            wb.Worksheets[1].Cells[0, 0].Style.HorizontalAlignment = TextAlignmentType.Center;
            wb.Worksheets[1].Cells[0, 0].Style.VerticalAlignment = TextAlignmentType.Center;
            wb.Worksheets[1].Cells[1, 0].Style.HorizontalAlignment = TextAlignmentType.Center;
            wb.Worksheets[1].Cells[1, 0].Style.VerticalAlignment = TextAlignmentType.Center;
            wb.Worksheets[1].Cells[0, 0].Style.Font.Size = 24;
            wb.Worksheets[1].Cells[1, 0].Style.Font.Size = 18;
            wb.Worksheets[1].Cells.SetRowHeight(0, 30);
            wb.Worksheets[1].AutoFitColumns();
            wb.Worksheets[1].AutoFitRows();
            wb.Worksheets[1].Cells.SetRowHeight(1, 20);
            wb.Worksheets[1].Charts.Add(ChartType.Column, 7, 0, 40, i - 1);
            wb.Worksheets[1].Charts[0].Title.Text = K12.Data.School.ChineseName + "-晤談記錄輔導歸類";
            if (i < 27)
            {
                wb.Worksheets[1].Charts[0].NSeries.Add("B5:" + (char)(64 + i) + "6", false);   //true,false 行或列                    
                wb.Worksheets[1].Charts[0].NSeries.CategoryData = "B4:" + (char)(64 + i) + "4";
            }
            else
            {
                wb.Worksheets[1].Charts[0].NSeries.Add("B5:A" + (char)(64 + i-26) + "6", false);   //true,false 行或列                    
                wb.Worksheets[1].Charts[0].NSeries.CategoryData = "B4:A" + (char)(64 + i-26) + "4";
            }
            wb.Worksheets[1].Charts[0].CategoryAxis.TickLabels.Font.Name = "@標楷體";
            wb.Worksheets[1].Charts[0].CategoryAxis.TickLabels.Rotation = -90;
            wb.Worksheets[1].Charts[0].NSeries[0].Name = "次數";
            wb.Worksheets[1].Charts[0].NSeries[1].Name = "人數";
            wb.Worksheets[1].Charts[0].Title.TextFont.Size = 20;
            try
            {
                wb.Save(Application.StartupPath + "\\Reports\\個案_晤諒記錄輔導歸類統計報表.xls", FileFormatType.Excel2003);
                System.Diagnostics.Process.Start(Application.StartupPath + "\\Reports\\個案_晤諒記錄輔導歸類統計報表.xls");
            }
            catch
            {
                System.Windows.Forms.SaveFileDialog sd1 = new System.Windows.Forms.SaveFileDialog();
                sd1.Title = "另存新檔";
                sd1.FileName = "\\Reports個案_晤諒記錄輔導歸類統計報表.xls";
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
        
    }
}
