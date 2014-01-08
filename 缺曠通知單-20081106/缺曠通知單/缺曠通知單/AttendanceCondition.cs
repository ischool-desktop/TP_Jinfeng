using System;
using System.Collections.Generic;
using System.Text;

namespace 缺曠通知單
{
    public class AttendanceCondition
    {
        string mPeriodType = "";
        string mAbsenceType = "";
        string mOperator = "";
        int mNumber = 0;
        int mReallyNumber = 0;

       static public AttendanceCondition NewInstance(DevComponents.DotNetBar.Controls.ComboBoxEx vcmbType,DevComponents.DotNetBar.Controls.ComboBoxEx vcmbAbsence,DevComponents.DotNetBar.Controls.ComboBoxEx vcmbOperator,DevComponents.DotNetBar.Controls.TextBoxX vtxtNumber)
        {
            AttendanceCondition Condition = new AttendanceCondition();

           Condition.PeriodType = (vcmbType.SelectedItem != null) ? vcmbType.SelectedItem.ToString() : "";
           Condition.AbsenceType = (vcmbAbsence.SelectedItem != null) ? vcmbAbsence.SelectedItem.ToString() : "";
           Condition.Operator = (vcmbOperator.SelectedItem != null) ? vcmbOperator.SelectedItem.ToString() : "";

            int vNumber = 0;

            if (Int32.TryParse(vtxtNumber.Text, out vNumber))
                Condition.Number = vNumber;

            return Condition;
        }

        public AttendanceCondition()
        {
 
        }

        public AttendanceCondition(System.Xml.XmlElement vElm)
        {
            LoadFromXmlElement(vElm);
        }

        public AttendanceCondition(string vPeriodType,string vAbsenceType,string vOperator,int vNumber)
        {
            mPeriodType = vPeriodType;
            mAbsenceType = vAbsenceType;
            mOperator = vOperator;
            mNumber = vNumber;
        }

        public bool IsValidate
        {
            get
            {
                return !mPeriodType.Equals("") && !mAbsenceType.Equals("") && !mOperator.Equals("") && mNumber > 0;
            }
        }

        public bool IsQualified
        {
            get
            {
                if (IsValidate)
                {
                    if (mOperator.Equals(">="))
                        return mReallyNumber >= mNumber;
                    else if (mOperator.Equals("="))
                        return mReallyNumber == mNumber;
                    else if (mOperator.Equals("<="))
                        return mReallyNumber <= mNumber;
                    else
                        return false;
                }
                else
                    return false;
            }
        }

        public System.Xml.XmlElement GetDocumentElm(System.Xml.XmlDocument vdoc)
        {
            System.Xml.XmlElement Elm = vdoc.CreateElement("缺曠條件");

            Elm.SetAttribute("PeriodType", mPeriodType);
            Elm.SetAttribute("AbsenceType", mAbsenceType);
            Elm.SetAttribute("Operator", mOperator);
            Elm.SetAttribute("Number", mNumber.ToString());

            return Elm;
        }

        private void LoadFromXmlElement(System.Xml.XmlElement vElm)
        {
            mPeriodType = vElm.GetAttribute("PeriodType");
            mAbsenceType = vElm.GetAttribute("AbsenceType");
            mOperator = vElm.GetAttribute("Operator");
            mNumber = Int32.Parse(vElm.GetAttribute("Number"));
        }

        public System.Xml.XmlElement XMLElm
        {
            get
            {
                System.Xml.XmlElement Elm = (new System.Xml.XmlDocument()).CreateElement("缺曠條件");

                Elm.SetAttribute("PeriodType", mPeriodType);
                Elm.SetAttribute("AbsenceType", mAbsenceType);
                Elm.SetAttribute("Operator", mOperator);
                Elm.SetAttribute("Number", mNumber.ToString());

                return Elm;

            }
            set
            {
                LoadFromXmlElement(value);
            }
        }

        public string XML
        {
            get
            {
                return XMLElm.OuterXml;
            }
        }


        public string  PeriodType
        {
             get
             {
                 return mPeriodType;
             }
            set 
            {
                mPeriodType = value;
            }
        }   

        public string AbsenceType
        {
            get
            {
                return mAbsenceType;
            }
            set
            {
                mAbsenceType = value;
            }
        }

        public string Operator
        {
            get
            {
                return mOperator;
            }
            set
            {
                mOperator = value;
            }
        }

        public int Number
        {
            get
            {
                return mNumber;
            }
            set
            {
                mNumber = value;
            }
        }

        public int ReallNumber
        {
            get
            {
                return mReallyNumber;
            }
            set
            {
                mReallyNumber = value;
            }
        }
    }
}