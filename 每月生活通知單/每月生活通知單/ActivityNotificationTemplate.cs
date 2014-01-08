using System;
using System.Collections.Generic;
using System.Text;
using Aspose.Words;
using Aspose.Words.Drawing;
using System.Drawing;

namespace 每月生活通知單
{
    public class ActivityNotificationTemplate:ISchoolDocument 
    {

        private int mStartRowIndex=0;
        private string mRawTemplatePath;
        private Aspose.Words.Document mtemplate;
        private List<string> mMeetingList;
        private List<string> mGeneralList;

        public ActivityNotificationTemplate(string RawTemplatePath,List<string > MeetingList,List<string> GeneralList)
        {
            mRawTemplatePath=RawTemplatePath;

            mMeetingList = MeetingList;
            mGeneralList = GeneralList;
        }

        public int ProcessDocument()
        {
                  Document template = new Aspose.Words.Document(mRawTemplatePath,LoadFormat.Doc,"");
                  DocumentBuilder builder = new DocumentBuilder(template);

                  Dictionary<string, List<string>> userDefinedConfig =  new Dictionary<string, List<string>>();

                  if (mGeneralList.Count>0)
                     userDefinedConfig.Add("一般",mGeneralList);
                  if (mMeetingList.Count >0)
                      userDefinedConfig.Add("集會",mMeetingList);
                  
                  //缺曠類別部份
                  #region 缺曠類別部份
                  builder.MoveToMergeField("缺曠類別");
                  Table table = template.Sections[0].Body.Tables[0];
                  Cell startCell = (Cell)builder.CurrentParagraph.ParentNode;
                  Row startRow = (Row)startCell.ParentNode;

                  double totalWidth = startCell.CellFormat.Width;
                  int startRowIndex = table.IndexOf(startRow);

                  mStartRowIndex = startRowIndex;
               

                  //假設假別為4
                  int columnNumber = 0;
              
                  
                  foreach (List<string> var in userDefinedConfig.Values)
                  {
                      columnNumber += var.Count;
                  }

                  double columnWidth = totalWidth / columnNumber;

                  for (int i = startRowIndex; i < startRowIndex + 4; i++)
                  {
                      table.Rows[i].RowFormat.HeightRule = HeightRule.Exactly;
                      table.Rows[i].RowFormat.Height = 12;
                  }

                  foreach (string attendanceType in userDefinedConfig.Keys)
                  {
                      Cell newCell = new Cell(template);
                      newCell.CellFormat.Width = userDefinedConfig[attendanceType].Count * columnWidth;
                      newCell.CellFormat.VerticalAlignment = CellVerticalAlignment.Center;
                      newCell.CellFormat.WrapText = true;
                      newCell.Paragraphs.Add(new Paragraph(template));
                      newCell.Paragraphs[0].ParagraphFormat.Alignment = ParagraphAlignment.Center;
                      newCell.Paragraphs[0].ParagraphFormat.LineSpacingRule = LineSpacingRule.Exactly;
                      newCell.Paragraphs[0].ParagraphFormat.LineSpacing = 12;
                      newCell.Paragraphs[0].Runs.Add(new Run(template, attendanceType));
                      newCell.Paragraphs[0].Runs[0].Font.Size = 8;
                      table.Rows[startRowIndex].Cells.Add(newCell.Clone(true));
                      foreach (string absenceType in userDefinedConfig[attendanceType])
                      {
                          newCell.CellFormat.Width = columnWidth;
                          newCell.Paragraphs[0].Runs[0].Text = absenceType;
                          table.Rows[startRowIndex + 1].Cells.Add(newCell.Clone(true));
                          newCell.Paragraphs[0].Runs[0].Text = "0";
                          table.Rows[startRowIndex + 2].Cells.Add(newCell.Clone(true));
                          table.Rows[startRowIndex + 3].Cells.Add(newCell.Clone(true));
                      }
                  }

                  for (int i = startRowIndex; i < startRowIndex + 4; i++)
                  {
                      if (userDefinedConfig.Count > 0)
                          table.Rows[i].Cells[1].Remove();
                      table.Rows[i].LastCell.CellFormat.Borders.Right.Color = Color.Black;
                      table.Rows[i].LastCell.CellFormat.Borders.Right.LineWidth = 2.25;
                  }
                  #endregion

                 mtemplate = template;

                 return 0;
        }

        public object ExtraInfo(string value)
        {
            if (value.Equals("StartRowIndex"))
                return mStartRowIndex;
           else
                return null;
        }

        public Aspose.Words.Document Document
        {
            get
            {
                return mtemplate;
            }
        }


    }
}