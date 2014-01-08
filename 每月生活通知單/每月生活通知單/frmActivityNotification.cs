using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace 每月生活通知單
{
    public partial class frmActivityNotification : Form
    {

        private string mfilename;

        public frmActivityNotification()
        {
            InitializeComponent();

            chkGeneral.Items.Clear();
            chkMeeting.Items.Clear();
            dateEnd.Value = DateTime.Today;
            dateStart.Value = DateTime.Today;
            foreach (string strAbsence in ActivityNotificationRecord.AbsenceType)
            {
                chkGeneral.Items.Add(strAbsence);
                chkMeeting.Items.Add(strAbsence);
            }
            
        }

        public List<string> MeetingList
        {
            get
            {
                List<string> vList=new List<string>();

                for(int i=0;i<chkMeeting.CheckedItems.Count;i++)
                    vList.Add(chkMeeting.CheckedItems[i].ToString());

                return vList;
            }
        }

        public List<string> GeneralList
        {
            get
            {
                List<string> vList = new List<string>();

                for (int i = 0; i < chkGeneral.CheckedItems.Count; i++)
                    vList.Add(chkGeneral.CheckedItems[i].ToString());

                return vList;
            }
        }


        public string MinReward
        {
            get
            {
                return cmbReward.Text;
            }
        }

        public int MinRewardCount
        {
            get
            {
                int intMinRewardCount=0;
                int.TryParse(txtMinRewardCount.Text,out intMinRewardCount);
                return intMinRewardCount;
            }
        }

        public string StartDate
        {
            get
            {
                return dateStart.Text;
            }
        }

        public string EndDate
        {
            get
            {
                return dateEnd.Text;
            }
        }


        public string ReceiverType
        {
            get
            {
                return cmbReceiver.Text;
            }
        }

        public string ReceiverAddressType
        {
            get
            {
                return cmbReceiverAddress.Text;
            }
        }

        public string FileName
        {
            get
            {
                return mfilename;
            }
        }

        public string ApplicationPath
        {
            get
            {
                return Application.StartupPath;
            }
        }
        private void frmActivityNotification_Load(object sender, EventArgs e)
        {

        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.SaveFileDialog saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();

            saveFileDialog1.FileName = "每月生活通知單.doc";
            saveFileDialog1.AddExtension = true;
            saveFileDialog1.DefaultExt = "doc";

            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                mfilename = saveFileDialog1.FileName;
            else
                mfilename = "";
            this.DialogResult = DialogResult.OK;
         }

        private void chkMeetingSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < chkMeeting.Items.Count; i++)
                if (chkMeetingSelectAll.Checked)
                    chkMeeting.SetItemCheckState(i, CheckState.Checked);
                else
                    chkMeeting.SetItemCheckState(i, CheckState.Unchecked);


        }
        private void chkGeneralSelectAll_CheckedChanged(object sender, EventArgs e)
        {
           
            for (int i = 0; i < chkGeneral.Items.Count; i++)
                if (chkGeneralSelectAll.Checked)
                    chkGeneral.SetItemCheckState(i, CheckState.Checked);
                else
                    chkGeneral.SetItemCheckState(i, CheckState.Unchecked);

        }

        private void labelX4_Click(object sender, EventArgs e)
        {

        }

        

        

    }
}