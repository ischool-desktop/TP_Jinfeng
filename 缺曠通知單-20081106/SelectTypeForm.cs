using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace 缺曠通知單
{
    public partial class SelectTypeForm : SmartSchool.Common.BaseForm
    {

        private BackgroundWorker _BGWAbsenceAndPeriodList;
        private List<string> mTypeList = new List<string>();
        private List<string> mAbsenceList = new List<string>();
        private ActivityNotificationPreference mActivityNotificationPreference;

        public SelectTypeForm()
        {
            InitializeComponent();
            _BGWAbsenceAndPeriodList = new BackgroundWorker();
            _BGWAbsenceAndPeriodList.DoWork += new DoWorkEventHandler(_BGWAbsenceAndPeriodList_DoWork);
            _BGWAbsenceAndPeriodList.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_BGWAbsenceAndPeriodList_RunWorkerCompleted);
            _BGWAbsenceAndPeriodList.RunWorkerAsync();

            mActivityNotificationPreference = ActivityNotificationPreference.GetInstance();

            chkAdvanceCondition.Checked = mActivityNotificationPreference.UseAdvanceCondition;
        }

        void _BGWAbsenceAndPeriodList_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            System.Windows.Forms.DataGridViewTextBoxColumn colName = new DataGridViewTextBoxColumn();
            colName.HeaderText = "節次分類";
            colName.MinimumWidth = 70;
            colName.Name = "colName";
            colName.ReadOnly = true;
            colName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            colName.Width = 70;
            this.dataGridViewX1.Columns.Add(colName);

            foreach (string absence in mAbsenceList)
            {
                System.Windows.Forms.DataGridViewCheckBoxColumn newCol = new DataGridViewCheckBoxColumn();
                newCol.HeaderText = absence;
                newCol.Width = 55;
                newCol.ReadOnly = false;
                newCol.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
                newCol.Tag = absence;
                newCol.ValueType = typeof(bool);
                this.dataGridViewX1.Columns.Add(newCol);
            }
            foreach (string type in mTypeList)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dataGridViewX1, type);
                row.Tag = type;
                dataGridViewX1.Rows.Add(row);
            }

            #region 讀取列印設定 Preference
            XmlElement config = SmartSchool.Customization.Data.SystemInformation.Preference["缺曠別設定"];
            if (config != null)
            {
                #region 已有設定檔則將設定檔內容填回畫面上
                foreach (XmlElement type in config.SelectNodes("Type"))
                {
                    string typeName = type.GetAttribute("Text");
                    foreach (DataGridViewRow row in dataGridViewX1.Rows)
                    {
                        if (typeName == ("" + row.Tag))
                        {
                            foreach (XmlElement absence in type.SelectNodes("Absence"))
                            {
                                string absenceName = absence.GetAttribute("Text");
                                foreach (DataGridViewCell cell in row.Cells)
                                {
                                    if (cell.OwningColumn is DataGridViewCheckBoxColumn && ("" + cell.OwningColumn.Tag) == absenceName)
                                    {
                                        cell.Value = true;
                                    }
                                }
                            }
                            break;
                        }
                    }
                }
                #endregion
            }
            else
            {
                #region 產生空白設定檔
                config = new XmlDocument().CreateElement("缺曠別設定");
                SmartSchool.Customization.Data.SystemInformation.Preference["缺曠別設定"] = config;
                #endregion
            }

            #endregion
        }

        void _BGWAbsenceAndPeriodList_DoWork(object sender, DoWorkEventArgs e)
        {
            mAbsenceList = ActivityNotificationRecord.AbsenceType;
            mTypeList = ActivityNotificationRecord.PeriodType;
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            if (!CheckColumnNumber())
                return;

            #region 更新列印設定 Preference

            XmlElement config = SmartSchool.Customization.Data.SystemInformation.Preference["缺曠別設定"];

            config = (config ==null)?( new XmlDocument().CreateElement("缺曠別設定")):config;

            config.RemoveAll();

            foreach (DataGridViewRow row in dataGridViewX1.Rows)
            {
                bool needToAppend = false;
                XmlElement type = config.OwnerDocument.CreateElement("Type");
                type.SetAttribute("Text", "" + row.Tag);
                foreach (DataGridViewCell cell in row.Cells)
                {
                    XmlElement absence = config.OwnerDocument.CreateElement("Absence");
                    absence.SetAttribute("Text", "" + cell.OwningColumn.Tag);
                    if (cell.Value is bool && ((bool)cell.Value))
                    {
                        needToAppend = true;
                        type.AppendChild(absence);
                    }
                }
                if (needToAppend)
                    config.AppendChild(type);
            }

            SmartSchool.Customization.Data.SystemInformation.Preference["缺曠別設定"] = config;

            #endregion

            mActivityNotificationPreference.UseAdvanceCondition = chkAdvanceCondition.Checked;

            this.Close();
        }

        internal bool CheckColumnNumber()
        {
            int limit = 253;
            int columnNumber = 0;
            int block = 9;

            //foreach (TreeNode type in treeView1.Nodes)
            //{
            //    foreach (TreeNode var in type.Nodes)
            //    {
            //        if (var.Checked == true)
            //            columnNumber++;
            //    }
            //}
            foreach (DataGridViewRow row in dataGridViewX1.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Value is bool && ((bool)cell.Value))
                        columnNumber++;
                }
            }

            if (columnNumber * block > limit)
            {
                System.Windows.Forms.MessageBox.Show("您所選擇的假別超出 Excel 的最大欄位，請減少部分假別");
                return false;
            }
            else
                return true;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AbsenceNotificationConditionForm ConditionForm = new AbsenceNotificationConditionForm();
            ConditionForm.ShowDialog();
        }

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridViewX1.Rows)
                foreach (DataGridViewCell cell in row.Cells)
                    if (!(cell.Value is string))
                        cell.Value = chkSelectAll.Checked;

             
        }

    }
}