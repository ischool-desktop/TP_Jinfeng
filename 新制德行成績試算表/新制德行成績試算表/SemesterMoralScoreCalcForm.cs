using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using SmartSchool.Common;

namespace �w�榨�Z�պ��
{
    public partial class SemesterMoralScoreCalcForm : SmartSchool.Evaluation.Reports.SelectSemesterForm
    {
        private Dictionary<string, List<string>> _userType = new Dictionary<string, List<string>>();
        public Dictionary<string, List<string>> UserDefinedType
        {
            get { return _userType; }
        }

        private bool _over100 = false;
        public bool AllowMoralScoreOver100
        {
            get { return _over100; }
        }

        private int _sizeIndex = 0;
        public int PaperSize
        {
            get { return _sizeIndex; }
        }

        public SemesterMoralScoreCalcForm()
        {
            InitializeComponent();
            LoadPreference();
        }

        private void LoadPreference()
        {
            XmlElement config = SmartSchool.Customization.Data.SystemInformation.Preference["�w�榨�Z�պ��"];
            if (config != null)
            {
                //�C�L��T
                XmlElement print = (XmlElement)config.SelectSingleNode("Print");
                if (print != null)
                {
                    if (!string.IsNullOrEmpty(print.GetAttribute("AllowMoralScoreOver100")))
                        _over100 = bool.Parse(print.GetAttribute("AllowMoralScoreOver100"));
                    if (print.HasAttribute("PaperSize"))
                        _sizeIndex = int.Parse(print.GetAttribute("PaperSize"));
                }
                else
                {
                    XmlElement newPrint = config.OwnerDocument.CreateElement("Print");
                    newPrint.SetAttribute("AllowMoralScoreOver100", "False");
                    newPrint.SetAttribute("PaperSize", "0");
                    _over100 = false;
                    _sizeIndex = 0;
                    config.AppendChild(newPrint);
                    SmartSchool.Customization.Data.SystemInformation.Preference["�w�榨�Z�պ��"] = config;
                }

                //�ϥΪ̳]�w�����O
                _userType.Clear();

                foreach (XmlElement type in config.SelectNodes("Type"))
                {
                    string typeName = type.GetAttribute("Text");

                    if (!_userType.ContainsKey(typeName))
                        _userType.Add(typeName, new List<string>());

                    foreach (XmlElement absence in type.SelectNodes("Absence"))
                    {
                        string absenceName = absence.GetAttribute("Text");

                        if (!_userType[typeName].Contains(absenceName))
                            _userType[typeName].Add(absenceName);
                    }
                }
            }
            else
            {
                #region ���ͪťճ]�w��
                config = new XmlDocument().CreateElement("�w�榨�Z�պ��");
                SmartSchool.Customization.Data.SystemInformation.Preference["�w�榨�Z�պ��"] = config;
                #endregion
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SemesterMoralScoreCalcConfig config = new SemesterMoralScoreCalcConfig(_over100, _sizeIndex);
            if (config.ShowDialog() == DialogResult.OK)
            {
                LoadPreference();
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SmartSchool.Evaluation.Reports.SelectTypeForm typeForm = new SmartSchool.Evaluation.Reports.SelectTypeForm("�w�榨�Z�պ��");
            if (typeForm.ShowDialog() == DialogResult.OK)
            {
                LoadPreference();
            }
        }

        private void buttonX1_Click_1(object sender, EventArgs e)
        {

        }
    }
}