using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using SmartSchool.Common;
using SmartSchool.Customization.PlugIn;
using SmartSchool.Customization.Data;
using SmartSchool.Customization.Data.StudentExtension;

namespace ¯ÊÃm³qª¾³æ
{
    public partial class AbsenceNotificationSelectDateRangeForm : SmartSchool.ClassRelated.RibbonBars.Reports.SelectDateRangeForm
    {
        private ActivityNotificationPreference  mActivityNotificationPreference;

        private DateTime GetMonthFirstDay(DateTime inputDate)
        {
            return DateTime.Parse(inputDate.Year + "/" + inputDate.Month + "/1");
        }

        private DateTime GetWeekFirstDay(DateTime inputDate)
        {
            switch (inputDate.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    return inputDate;
                case DayOfWeek.Tuesday:
                    return inputDate.AddDays(-1);
                case DayOfWeek.Wednesday:
                    return inputDate.AddDays(-2);
                case DayOfWeek.Thursday:
                    return inputDate.AddDays(-3);
                case DayOfWeek.Friday:
                    return inputDate.AddDays(-4);
                case DayOfWeek.Saturday:
                    return inputDate.AddDays(-5);
                default:
                    return inputDate.AddDays(-6);
            }
        }

        private void InitialDateRange()
        {
            switch (mActivityNotificationPreference.DateModeRangeMode)
            {
                case DateRangeMode.Month:
                    {
                        DateTime a = DateTime.Today;
                        a = GetMonthFirstDay(a.AddMonths(-1));
                        textBoxX1.Text = a.ToShortDateString();
                        textBoxX2.Text = a.AddMonths(1).AddDays(-1).ToShortDateString();
                        break;
                    }
                case DateRangeMode.Week:
                    {
                        DateTime b = DateTime.Today;
                        b = GetWeekFirstDay(b.AddDays(-7));
                        textBoxX1.Text = b.ToShortDateString();
                        textBoxX2.Text = b.AddDays(5).ToShortDateString();
                        break;
                    }
                case DateRangeMode.Custom:
                    {
                        textBoxX1.Text = DateTime.Today.ToShortDateString();
                        textBoxX2.Text = textBoxX1.Text;
                        break;
                    }
                default:
                    throw new Exception("Date Range Mode Error.");
            }
        }

        public AbsenceNotificationSelectDateRangeForm()
        {
            InitializeComponent();

            mActivityNotificationPreference = ActivityNotificationPreference.GetInstance();

            textBoxX2.Enabled = (mActivityNotificationPreference.DateModeRangeMode==DateRangeMode.Custom)?true:false;

            InitialDateRange();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AbsenceNotificationConfigForm configForm = new AbsenceNotificationConfigForm();

            if (configForm.ShowDialog() == DialogResult.OK)
                InitialDateRange();
        }

        protected override void textBoxX1_TextChanged(object sender, EventArgs e)
        {

        }

        protected override void textBoxX2_TextChanged(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SelectTypeForm form = new SelectTypeForm();
            form.ShowDialog();
        }

        private void buttonX1_Click_1(object sender, EventArgs e)
        {
            mActivityNotificationPreference.StartDate = textBoxX1.Text;
            mActivityNotificationPreference.EndDate = textBoxX2.Text;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void textBoxX2_TextChanged_1(object sender, EventArgs e)
        {

        }

    }
}