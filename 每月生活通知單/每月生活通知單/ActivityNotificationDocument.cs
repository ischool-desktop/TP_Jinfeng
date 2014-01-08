using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.Customization.Data;
using SmartSchool.Customization.PlugIn.Report;
using SmartSchool.Customization.PlugIn;
using SmartSchool.Customization.Data.StudentExtension;
using Aspose.Words;
using Aspose.Words.Drawing;
using System.Drawing;


namespace 每月生活通知單
{
    public class ActivityNotificationDocument:ISchoolDocument
    {
        private StudentRecord mstudent;
        private ActivityNotificationConfig mconfig;
        private ISchoolDocument mtemplate;
        private Aspose.Words.Document mdoc;

        public ActivityNotificationDocument(StudentRecord student, ActivityNotificationConfig config, ActivityNotificationTemplate template)
        {
            mstudent = student;
            mconfig = config;
            mtemplate = template;
        }

        public int ProcessDocument()
        {
            Document eachSection = new Document();
            eachSection.Sections.Clear();
            eachSection.Sections.Add(eachSection.ImportNode(mtemplate.Document.Sections[0], true));

            ActivityNotificationRecord ANR = new ActivityNotificationRecord(mstudent, mconfig);
            RewardStatistics RS = new RewardStatistics(mstudent.RewardList,mconfig.MinReward, mconfig.MinRewardCount , mconfig.StartDate , mconfig.EndDate);
            AttendanceStatistics AS = new AttendanceStatistics(mstudent.AttendanceList, mconfig.StartDate, mconfig.EndDate,mconfig.MeetingList,mconfig.GeneralList );

            string[] key = new string[] { "郵遞區號第一碼", "郵遞區號第二碼", "郵遞區號第三碼", "郵遞區號第四碼", "郵遞區號第五碼", "收件人地址", "收件人姓名", "學校名稱", "學校地址", "學校電話", "資料期間", "班級", "座號", "學號", "學生姓名", "導師", "學期累計大功", "學期累計小功", "學期累計嘉獎", "學期累計大過", "學期累計小過", "學期累計警告", "本期累計大功", "本期累計小功", "本期累計嘉獎", "本期累計大過", "本期累計小過", "本期累計警告", "獎懲明細", "缺曠明細" };
            object[] value = new object[] { ANR.ZipCode01, ANR.ZipCode02, ANR.ZipCode03, "", "", ANR.ReceiverAddress, ANR.Receiver, ANR.SchoolName, ANR.SchoolAddress, ANR.SchoolTel, ANR.DataRange, ANR.ClassName.Substring(0,4), ANR.SeatNo, ANR.StudentNumber, ANR.StudentName, ANR.TeacherName,RS.AwardASemesterCount ,RS.AwardBSemesterCount,RS.AwardCSemesterCount,RS.FaultASemesterCount ,RS.FaultBSemesterCount,RS.FaultCSemesterCount ,RS.AwardACount,RS.AwardBCount,RS.AwardCCount,RS.FaultACount ,RS.FaultBCount ,RS.FaultCCount , RS.RewardCommentList, AS.DocumentAttendanceDetail };

            //合併列印
            eachSection.MailMerge.MergeField += new Aspose.Words.Reporting.MergeFieldEventHandler(AbsenceNotification_MailMerge_MergeField);
            eachSection.MailMerge.RemoveEmptyParagraphs = true;
            eachSection.MailMerge.Execute(key, value);

            //填寫缺曠記錄
            Table eachTable = eachSection.Sections[0].Body.Tables[0];
            int columnIndex = 1;

            int StartRowIndex = (int)mtemplate.ExtraInfo("StartRowIndex");

            for (int i = 0; i < mconfig.GeneralList.Count; i++)
            {
                eachTable.Rows[StartRowIndex + 2].Cells[columnIndex + i].Paragraphs[0].Runs[0].Text = AS.GeneralSemesterCount[mconfig.GeneralList[i]].ToString();
                eachTable.Rows[StartRowIndex + 3].Cells[columnIndex + i].Paragraphs[0].Runs[0].Text = AS.GeneralCount[mconfig.GeneralList[i]].ToString();
            }

            columnIndex = mconfig.GeneralList.Count;

            for (int i = 0; i < mconfig.MeetingList.Count; i++)
            {
                eachTable.Rows[StartRowIndex + 2].Cells[columnIndex + i].Paragraphs[0].Runs[0].Text = AS.MeetingSemesterCount[mconfig.MeetingList[i]].ToString();
                eachTable.Rows[StartRowIndex + 3].Cells[columnIndex + i].Paragraphs[0].Runs[0].Text = AS.MeetingCount[mconfig.MeetingList[i]].ToString();
            }


            mdoc=eachSection;

            return 0;
        }

        public object ExtraInfo(string value)
        {
            return null;
        }

        public Aspose.Words.Document Document
        {
            get
            {
                return mdoc;
            }
        }

        
        private void AbsenceNotification_MailMerge_MergeField(object sender, Aspose.Words.Reporting.MergeFieldEventArgs e)
        {
            if (e.FieldName == "獎懲明細")
            {
                List<string> eachStudentDisciplineDetail = (List<string>)e.FieldValue;

                Aspose.Words.DocumentBuilder builder = new Aspose.Words.DocumentBuilder(e.Document);

                builder.MoveToField(e.Field, false);
                builder.StartTable();
                builder.CellFormat.ClearFormatting();
                builder.CellFormat.Borders.ClearFormatting();
                builder.CellFormat.VerticalAlignment = Aspose.Words.CellVerticalAlignment.Center;
                builder.CellFormat.LeftPadding = 3.0;
                builder.RowFormat.LeftIndent = 0.0;
                builder.RowFormat.Height = 15.0;

                int rowNumber = 6;
/*
                if (eachStudentDisciplineDetail.Count > rowNumber * 2)
                {
                    rowNumber = eachStudentDisciplineDetail.Count / 2;
                    rowNumber += eachStudentDisciplineDetail.Count % 2;
                }
*/
                if (eachStudentDisciplineDetail.Count > rowNumber * 2)
                {
                    rowNumber += (eachStudentDisciplineDetail.Count - (rowNumber * 2)) / 2;
                    rowNumber += (eachStudentDisciplineDetail.Count - (rowNumber * 2)) % 2;
                }

                for (int j = 0; j < rowNumber; j++)
                {
                    builder.InsertCell();
                    builder.CellFormat.Borders.Right.LineStyle = Aspose.Words.LineStyle.Single;
                    builder.CellFormat.Borders.Right.Color = Color.Black;
                    if (j < eachStudentDisciplineDetail.Count)
                        builder.Write(eachStudentDisciplineDetail[j]);
                    builder.InsertCell();
                    if (j + rowNumber < eachStudentDisciplineDetail.Count)
                        builder.Write(eachStudentDisciplineDetail[j + rowNumber]);
                    builder.EndRow();
                }

                builder.EndTable();

                e.Text = string.Empty;
            }


            if (e.FieldName == "缺曠明細")
            {
                if (e.FieldValue == null)
                    return;

                object[] objectValues = (object[])e.FieldValue;
                Dictionary<string, Dictionary<string, string>> studentAbsenceDetail = (Dictionary<string, Dictionary<string, string>>)objectValues[0];
                List<string> periodList = (List<string>)objectValues[1];

                DocumentBuilder builder = new DocumentBuilder(e.Document);

                #region 缺曠明細部份
                builder.MoveToField(e.Field, false);
                Cell detailStartCell = (Cell)builder.CurrentParagraph.ParentNode;
                Row detailStartRow = (Row)detailStartCell.ParentNode;
                int detailStartRowIndex = e.Document.Sections[0].Body.Tables[0].IndexOf(detailStartRow);

                Table detailTable = builder.StartTable();
                builder.CellFormat.Borders.Left.LineWidth = 0.5;
                builder.CellFormat.Borders.Right.LineWidth = 0.5;

                builder.RowFormat.HeightRule = HeightRule.Auto;
                builder.RowFormat.Height = 12;
                builder.RowFormat.Alignment = RowAlignment.Center;

                int rowNumber = 8;
                if (studentAbsenceDetail.Count > rowNumber * 3)
                {
                    rowNumber = studentAbsenceDetail.Count / 3;
                    if (studentAbsenceDetail.Count % 3 > 0)
                        rowNumber++;
                }

                builder.InsertCell();

                for (int i = 0; i < 3; i++)
                {
                    builder.CellFormat.Borders.Right.Color = Color.Black;
                    builder.CellFormat.Borders.Left.Color = Color.Black;
                    builder.CellFormat.Width = 20;
                    builder.CellFormat.VerticalAlignment = CellVerticalAlignment.Center;
                    builder.Write("日期");
                    builder.InsertCell();

                    for (int j = 0; j < periodList.Count; j++)
                    {
                        builder.CellFormat.Borders.Right.Color = Color.White;
                        builder.CellFormat.Borders.Left.Color = Color.White;
                        builder.CellFormat.Width = 9;
                        builder.CellFormat.WrapText = true;
                        builder.CellFormat.LeftPadding = 0.5;
                        if (j < periodList.Count)
                            builder.Write(periodList[j]);
                        builder.InsertCell();
                    }
                }

                builder.EndRow();

                for (int x = 0; x < rowNumber; x++)
                {
                    builder.CellFormat.Borders.Right.Color = Color.Black;
                    builder.CellFormat.Borders.Left.Color = Color.Black;
                    builder.CellFormat.Borders.Left.LineWidth = 0.5;
                    builder.CellFormat.Borders.Right.LineWidth = 0.5;
                    builder.CellFormat.Borders.Top.LineWidth = 0.5;
                    builder.CellFormat.Borders.Bottom.LineWidth = 0.5;
                    builder.CellFormat.Borders.LineStyle = LineStyle.Dot;
                    builder.RowFormat.HeightRule = HeightRule.Exactly;
                    builder.RowFormat.Height = 12;
                    builder.RowFormat.Alignment = RowAlignment.Center;
                    builder.InsertCell();

                    for (int i = 0; i < 3; i++)
                    {
                        builder.CellFormat.Borders.Left.LineStyle = LineStyle.Single;
                        builder.CellFormat.Width = 20;
                        builder.Write("");
                        builder.InsertCell();

                        builder.CellFormat.Borders.LineStyle = LineStyle.Dot;

                        for (int j = 0; j < periodList.Count; j++)
                        {
                            builder.CellFormat.Width = 9;
                            builder.Write("");
                            builder.InsertCell();
                        }
                    }

                    builder.EndRow();
                }
                builder.EndTable();

                foreach (Cell var in detailTable.Rows[0].Cells)
                {
                    var.Paragraphs[0].ParagraphFormat.LineSpacingRule = LineSpacingRule.Exactly;
                    var.Paragraphs[0].ParagraphFormat.LineSpacing = 9;
                }
                #endregion

                #region 填寫缺曠明細
                int eachDetailRowIndex = 0;
                int eachDetailColIndex = 0;

                foreach (string date in studentAbsenceDetail.Keys)
                {
                    int eachDetailPeriodColIndex = eachDetailColIndex + 1;
                    string[] splitDate = date.Split('/');
                    Paragraph dateParagraph = detailTable.Rows[eachDetailRowIndex + 1].Cells[eachDetailColIndex].Paragraphs[0];
                    dateParagraph.ParagraphFormat.Alignment = ParagraphAlignment.Center;
                    dateParagraph.ParagraphFormat.LineSpacingRule = LineSpacingRule.Exactly;
                    dateParagraph.ParagraphFormat.LineSpacing = 9;
                    dateParagraph.Runs.Clear();
                    dateParagraph.Runs.Add(new Run(e.Document));
                    dateParagraph.Runs[0].Font.Size = 8;
                    dateParagraph.Runs[0].Text = splitDate[1] + "/" + splitDate[2];

                    foreach (string period in periodList)
                    {
                        string dataValue = "";
                        if (studentAbsenceDetail[date].ContainsKey(period))
                            dataValue = studentAbsenceDetail[date][period];
                        Cell miniCell = detailTable.Rows[eachDetailRowIndex + 1].Cells[eachDetailPeriodColIndex];
                        miniCell.Paragraphs.Clear();
                        miniCell.Paragraphs.Add(dateParagraph.Clone(true));
                        miniCell.Paragraphs[0].Runs[0].Font.Size = 14 - (int)(periodList.Count / 2); //依表格多寡縮小文字
                        miniCell.Paragraphs[0].Runs[0].Text = dataValue;
                        eachDetailPeriodColIndex++;
                    }
                    eachDetailRowIndex++;
                    if (eachDetailRowIndex >= rowNumber)
                    {
                        eachDetailRowIndex = 0;
                        eachDetailColIndex += (periodList.Count + 1);
                    }
                }
                #endregion

                e.Text = string.Empty;
            }
        }
    }
}