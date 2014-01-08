using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ¯ÊÃm³qª¾³æ
{
    public partial class AbsenceNotificationConditionForm : SmartSchool.Common.BaseForm
    {

        private List<string> AbsenceType;
        private List<string> PeriodType;
        private ActivityNotificationPreference mPreference;

        private void SetComboBoxValue(List<string> vList, DevComponents.DotNetBar.Controls.ComboBoxEx vCombobox,bool FirstBlank)
        {
            if (FirstBlank)
                vCombobox.Items.Add("");
    
            foreach (string str in vList)
                vCombobox.Items.Add(str);
        }


        
        private void LoadPreference()
        {
            mPreference = ActivityNotificationPreference.GetInstance();

            AttendanceConditions Conditions = mPreference.Conditions;

            if (Conditions != null)
            {
                cmbTotalOperator.SelectedItem = Conditions.Operator;
                txtTotalNumber.Text = Conditions.Number.ToString();
                chkUseTotal.Checked = Conditions.IsUseTotal;

                if (Conditions[0] != null)
                {
                    cmbType01.SelectedItem = Conditions[0].PeriodType;
                    cmbAbsence01.SelectedItem = Conditions[0].AbsenceType;
                    cmbOperator01.SelectedItem = Conditions[0].Operator;
                    txtNumber01.Text = Conditions[0].Number.ToString();
                }

                if (Conditions[1] != null)
                {
                    cmbType02.SelectedItem = Conditions[1].PeriodType;
                    cmbAbsence02.SelectedItem = Conditions[1].AbsenceType;
                    cmbOperator02.SelectedItem = Conditions[1].Operator;
                    txtNumber02.Text = Conditions[1].Number.ToString();
                }

                if (Conditions[2] != null)
                {
                    cmbType03.SelectedItem = Conditions[2].PeriodType;
                    cmbAbsence03.SelectedItem = Conditions[2].AbsenceType;
                    cmbOperator03.SelectedItem = Conditions[2].Operator;
                    txtNumber03.Text = Conditions[2].Number.ToString();
                }
            }
            
        }

        private void SavePreference()
        {
            AttendanceConditions Conditions = new AttendanceConditions(cmbTotalOperator,txtTotalNumber,chkUseTotal.Checked);

            Conditions.Add(AttendanceCondition.NewInstance(cmbType01, cmbAbsence01, cmbOperator01, txtNumber01));
            Conditions.Add(AttendanceCondition.NewInstance(cmbType02, cmbAbsence02, cmbOperator02, txtNumber02));
            Conditions.Add(AttendanceCondition.NewInstance(cmbType03, cmbAbsence03, cmbOperator03, txtNumber03));

            mPreference.Conditions = Conditions;
        }

        private void RefreshOperatorLevel()
        {
                cmbOperator01.Enabled = !chkUseTotal.Checked;
                cmbOperator02.Enabled = !chkUseTotal.Checked;
                cmbOperator03.Enabled = !chkUseTotal.Checked;
                txtNumber01.Enabled = !chkUseTotal.Checked;
                txtNumber02.Enabled = !chkUseTotal.Checked;
                txtNumber03.Enabled = !chkUseTotal.Checked;

                cmbTotalOperator.Enabled = chkUseTotal.Checked;
                txtTotalNumber.Enabled = chkUseTotal.Checked;
        }

        public AbsenceNotificationConditionForm()
        {
            InitializeComponent();

            AbsenceType=ActivityNotificationRecord.AbsenceType;
            PeriodType = ActivityNotificationRecord.PeriodType;

            SetComboBoxValue(PeriodType,cmbType01,true);
            SetComboBoxValue(PeriodType, cmbType02,true);
            SetComboBoxValue(PeriodType, cmbType03,true);

            SetComboBoxValue(AbsenceType, cmbAbsence01,true);
            SetComboBoxValue(AbsenceType, cmbAbsence02,true);
            SetComboBoxValue(AbsenceType, cmbAbsence03,true);

            List<string> OperatorList=new List<string> ();

            OperatorList.Add(">=");
            OperatorList.Add("=");
            OperatorList.Add("<=");

            SetComboBoxValue(OperatorList,cmbOperator01,true);
            SetComboBoxValue(OperatorList, cmbOperator02,true);
            SetComboBoxValue(OperatorList, cmbOperator03,true);
            SetComboBoxValue(OperatorList, cmbTotalOperator, true);

            LoadPreference();

            RefreshOperatorLevel();

        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            SavePreference();
        }

        private void chkUseTotal_CheckedChanged(object sender, EventArgs e)
        {
            RefreshOperatorLevel();
        }
    }
}