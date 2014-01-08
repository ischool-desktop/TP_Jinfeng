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
using System.IO;


namespace 缺曠通知單
{
    public class AttendanceDocument : ISchoolDocument
    {
        private StudentRecord mstudent;
        private ActivityNotificationConfig mconfig;
        private ISchoolDocument mtemplate;
        private Aspose.Words.Document mdoc;

        public AttendanceDocument(StudentRecord student, ActivityNotificationConfig config, ActivityNotificationTemplate template)
        {
            mstudent = student;
            mconfig = config;
            mtemplate = template;
        }

        public int ProcessDocument()
        {
            if (mconfig.Preference.UseAdvanceCondition)
                if (!AttendanceStatistics.IsQualified(mstudent.AttendanceList, mconfig.Preference.Conditions,mconfig.StartDate,mconfig.EndDate))
                    return 1;

                List<string> vMeetingList = mconfig.Preference.UseAdvanceCondition ? mconfig.Preference.Conditions.MeetingList : mconfig.MeetingList;
                List<string> vGeneralList = mconfig.Preference.UseAdvanceCondition ? mconfig.Preference.Conditions.GeneralList : mconfig.GeneralList;

                ActivityNotificationRecord ANR = new ActivityNotificationRecord(mstudent, mconfig);
                AttendanceStatistics AS = new AttendanceStatistics(mstudent.AttendanceList, mconfig.StartDate, mconfig.EndDate, vMeetingList, vGeneralList);

                string[] key = new string[] { "郵遞區號第一碼", "郵遞區號第二碼", "郵遞區號第三碼", "郵遞區號第四碼", "郵遞區號第五碼", "收件人地址", "收件人姓名", "學校名稱", "學校地址", "學校電話", "資料期間", "班級", "座號", "學號", "學生姓名", "導師", "缺曠明細" };
                object[] value = new object[] { ANR.ZipCode01, ANR.ZipCode02, ANR.ZipCode03, "", "", ANR.ReceiverAddress, ANR.Receiver, ANR.SchoolName, ANR.SchoolAddress, ANR.SchoolTel, ANR.DataRange, ANR.ClassName, ANR.SeatNo, ANR.StudentNumber, ANR.StudentName, ANR.TeacherName, AS.DocumentAttendanceDetail };


                //合併列印
                Document eachSection = new Document();
                eachSection.Sections.Clear();
                eachSection.Sections.Add(eachSection.ImportNode(mtemplate.Document.Sections[0], true));

                eachSection.MailMerge.MergeField += new Aspose.Words.Reporting.MergeFieldEventHandler(AbsenceNotification_MailMerge_MergeField);
                eachSection.MailMerge.RemoveEmptyParagraphs = true;
                eachSection.MailMerge.Execute(key, value);

                //填寫缺曠記錄
                Table eachTable = eachSection.Sections[0].Body.Tables[0];
                int columnIndex = 1;

                int StartRowIndex = (int)mtemplate.ExtraInfo("StartRowIndex");

                for (int i = 0; i < vGeneralList.Count; i++)
                {
                    eachTable.Rows[StartRowIndex + 2].Cells[columnIndex + i].Paragraphs[0].Runs[0].Text = AS.GeneralSemesterCount[vGeneralList[i]].ToString();
                    eachTable.Rows[StartRowIndex + 3].Cells[columnIndex + i].Paragraphs[0].Runs[0].Text = AS.GeneralCount[vGeneralList[i]].ToString();
                }

                columnIndex = vGeneralList.Count;

                for (int i = 0; i < vMeetingList.Count; i++)
                {
                    eachTable.Rows[StartRowIndex + 2].Cells[columnIndex + i].Paragraphs[0].Runs[0].Text = AS.MeetingSemesterCount[vMeetingList[i]].ToString();
                    eachTable.Rows[StartRowIndex + 3].Cells[columnIndex + i].Paragraphs[0].Runs[0].Text = AS.MeetingCount[vMeetingList[i]].ToString();
                }

                mdoc = eachSection;

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