using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.IO;
using System.Xml;
using SmartSchool.Common;
using DevComponents.DotNetBar.Rendering;

namespace ���m�q����
{
    public partial class AbsenceNotificationConfigForm : BaseForm
    {

        private ActivityNotificationPreference mActivityNotificationPreference;
        private string base64 = null;

        private void SetPreference()
        {
            mActivityNotificationPreference = ActivityNotificationPreference.GetInstance();

            //�]�w�O�_�ϥΦۭq�˪�
            if (mActivityNotificationPreference.UseDefaultTemplate)
                radioButton1.Checked = true;
            else
                radioButton2.Checked = true;

            //�]�w�O�_�u�C�L�����m�O���ǥ�
            checkBoxX1.Checked = mActivityNotificationPreference.PrintHasRecordOnly;

            //�]�w����϶�
            switch (mActivityNotificationPreference.DateModeRangeMode)
            {
                case DateRangeMode.Month:
                    radioButton3.Checked = true;
                    break;
                case DateRangeMode.Week:
                    radioButton4.Checked = true;
                    break;
                case DateRangeMode.Custom:
                    radioButton5.Checked = true;
                    break;
                default:
                    throw new Exception("Date Range Mode Error.");
            }

            comboBoxEx1.SelectedIndex = 0;
            comboBoxEx2.SelectedIndex = 0;

            //�]�w����H�m�W
            foreach (DevComponents.Editors.ComboItem var in comboBoxEx1.Items)
            {
                if (var.Text.Equals(mActivityNotificationPreference.ReceiveName))
                {
                    comboBoxEx1.SelectedIndex = comboBoxEx1.Items.IndexOf(var);
                    break;
                }
            }

            //�]�w����H�a�}
            foreach (DevComponents.Editors.ComboItem var in comboBoxEx2.Items)
            {
                if (var.Text.Equals(mActivityNotificationPreference.ReceiveAddress))
                {
                    comboBoxEx2.SelectedIndex = comboBoxEx2.Items.IndexOf(var);
                    break;
                }
            }
         }

        public AbsenceNotificationConfigForm()
        {
            InitializeComponent();

            SetPreference();
        }


        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
                radioButton2.Checked = false;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
                radioButton1.Checked = false;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "�t�s�s��";
            sfd.FileName = "���m�q����.doc";
            sfd.Filter = "Word�ɮ� (*.doc)|*.doc|�Ҧ��ɮ� (*.*)|*.*";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    FileStream fs = new FileStream(sfd.FileName, FileMode.Create);
                    fs.Write(Properties.Resources.���m�q����, 0, Properties.Resources.���m�q����.Length);
                    fs.Close();
                    System.Diagnostics.Process.Start(sfd.FileName);
                }
                catch
                {
                    MsgBox.Show("���w���|�L�k�s���C", "�t�s�ɮץ���", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "�t�s�s��";
            sfd.FileName = "�ۭq���m�q����.doc";
            sfd.Filter = "Word�ɮ� (*.doc)|*.doc|�Ҧ��ɮ� (*.*)|*.*";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    byte[] _buffer = mActivityNotificationPreference.CustomizeTemplateByte;
                    FileStream fs = new FileStream(sfd.FileName, FileMode.Create);
                    if (Aspose.Words.Document.DetectFileFormat(new MemoryStream(_buffer)) == Aspose.Words.LoadFormat.Doc)
                        fs.Write(_buffer, 0, _buffer.Length);
                    else
                        fs.Write(Properties.Resources.���m�q����, 0, Properties.Resources.���m�q����.Length);
                    fs.Close();
                    System.Diagnostics.Process.Start(sfd.FileName);
                }
                catch
                {
                    MsgBox.Show("���w���|�L�k�s���C", "�t�s�ɮץ���", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "��ܦۭq�����m�q����d��";
            ofd.Filter = "Word�ɮ� (*.doc)|*.doc";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (Aspose.Words.Document.DetectFileFormat(ofd.FileName) == Aspose.Words.LoadFormat.Doc)
                    {
                        FileStream fs = new FileStream(ofd.FileName, FileMode.Open);

                        byte[] tempBuffer = new byte[fs.Length];
                        fs.Read(tempBuffer, 0, tempBuffer.Length);
                        base64 = Convert.ToBase64String(tempBuffer);
                        fs.Close();
                        MsgBox.Show("�W�Ǧ��\�C");
                    }
                    else
                        MsgBox.Show("�W���ɮ׮榡����");
                }
                catch
                {
                    MsgBox.Show("���w���|�L�k�s���C", "�}���ɮץ���", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            mActivityNotificationPreference.Refresh();

            mActivityNotificationPreference.UseDefaultTemplate=radioButton1.Checked;
            mActivityNotificationPreference.PrintHasRecordOnly=checkBoxX1.Checked;

            if (radioButton3.Checked)
                mActivityNotificationPreference.DateModeRangeMode = DateRangeMode.Month;
            else if (radioButton4.Checked)
                mActivityNotificationPreference.DateModeRangeMode = DateRangeMode.Week;
            else if (radioButton5.Checked)
                mActivityNotificationPreference.DateModeRangeMode = DateRangeMode.Custom;

            mActivityNotificationPreference.ReceiveName=((DevComponents.Editors.ComboItem)comboBoxEx1.SelectedItem).Text;
            mActivityNotificationPreference.ReceiveAddress = ((DevComponents.Editors.ComboItem)comboBoxEx2.SelectedItem).Text;

            if (base64 != null)
                mActivityNotificationPreference.CustomizeTemplateString = base64;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                radioButton4.Checked = false;
                radioButton5.Checked = false;
            }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked)
            {
                radioButton3.Checked = false;
                radioButton5.Checked = false;
            }
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton5.Checked)
            {
                radioButton3.Checked = false;
                radioButton4.Checked = false;
            }
        }
    }
}