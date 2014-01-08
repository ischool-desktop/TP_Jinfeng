using System;
using System.Collections.Generic;
using System.Text;

namespace 缺曠通知單
{

    public class AttendanceConditions
    {
        private List<AttendanceCondition> mConditions;
        private string mOperator = "";
        private int mNumber=0;
        private bool mIsUseTotal;

        public AttendanceConditions(DevComponents.DotNetBar.Controls.ComboBoxEx vcmbOperator, DevComponents.DotNetBar.Controls.TextBoxX vtxtNumber,bool vIsUseTotal)
        {
            mOperator =  (vcmbOperator.SelectedItem != null) ? vcmbOperator.SelectedItem.ToString() : "";

            int vNumber = 0;

            if (Int32.TryParse(vtxtNumber.Text, out vNumber))
                mNumber = vNumber;

            mIsUseTotal = vIsUseTotal;

            mConditions = new List<AttendanceCondition>();
        }

        public AttendanceConditions(System.Xml.XmlElement vElm)
        {
            if (vElm != null)
            {
                mConditions = new List<AttendanceCondition>();
                mConditions.Clear();
                mOperator = vElm.GetAttribute("Operator");
                mNumber = Int32.Parse(vElm.GetAttribute("Number"));
                mIsUseTotal = bool.Parse(vElm.GetAttribute("IsUseTotal"));

                foreach (System.Xml.XmlElement Node in vElm.SelectNodes("缺曠條件"))
                    mConditions.Add(new AttendanceCondition(Node));
            }
        }

        public void Add(AttendanceCondition vCondition)
        {
            mConditions.Add(vCondition);
        }

        public bool Remove(AttendanceCondition vCondition)
        {
            return mConditions.Remove(vCondition);
        }

        public AttendanceCondition this[int vIndex]
        {
            get
            {
                return mConditions[vIndex];
            }
            set 
            {
                mConditions[vIndex]=value;
            }
        }

        public bool Contains(AttendanceCondition vCondition)
        {
            return mConditions.Contains(vCondition);
        }

        public int Count
        {
            get
            {
                return mConditions.Count;
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

        public List<AttendanceCondition>  List
        {
             get
             {
                return mConditions;
             }
        }

        public List<string> MeetingList
        {
            get
            {
                List<string> vMeetingList = new List<string>();

                foreach (AttendanceCondition Condition in mConditions)
                    if (Condition.IsValidate)
                        if (Condition.PeriodType.Equals("集會"))
                            if (!vMeetingList.Contains(Condition.AbsenceType))
                                vMeetingList.Add(Condition.AbsenceType);

                return vMeetingList;
            }
        }

        public List<string> GeneralList
        {
            get
            {
                List<string> vGeneralList = new List<string>();

                foreach (AttendanceCondition Condition in mConditions)
                    if (Condition.IsValidate)
                        if (Condition.PeriodType.Equals("一般"))
                            if (!vGeneralList.Contains(Condition.AbsenceType))
                                vGeneralList.Add(Condition.AbsenceType);

                return vGeneralList;
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

        public int ReallyNumber
        {
            get
            {
                int vReallyNumber = 0;

                foreach (AttendanceCondition Condition in mConditions)
                    vReallyNumber += Condition.ReallNumber;

                return vReallyNumber;
            }
        }

        public bool IsValidate
        {
            get
            {
                return !mOperator.Equals("") && mNumber > 0;
            }
        }


        public bool IsQualified
        {
            get
            {
                if (IsUseTotal)
                {
                    if (IsValidate)
                    {
                        if (mOperator.Equals(">="))
                            return ReallyNumber >= mNumber;
                        else if (mOperator.Equals("="))
                            return ReallyNumber == mNumber;
                        else if (mOperator.Equals("<="))
                            return ReallyNumber <= mNumber;
                        else
                            return false;
                    }
                    else
                        return false;
                }
                else
                {
                    bool IsTotalQualified = false;

                    foreach (AttendanceCondition Condition in mConditions)
                        if (Condition.IsValidate)
                        {
                            if (Condition.IsQualified)
                                IsTotalQualified = true;
                            else
                            {
                                IsTotalQualified = false;
                                break;
                            }
                        }
                    return IsTotalQualified;
              }
            }
        }

        public bool IsUseTotal
        {
            get
            {
                return mIsUseTotal;
            }
            set
            {
                mIsUseTotal = value;
            }
        }

        public System.Xml.XmlElement GetDocumentElm(System.Xml.XmlDocument  vdoc)
        {
            System.Xml.XmlElement Elm =vdoc.CreateElement("缺曠條件集合");

            foreach (AttendanceCondition Condition in mConditions)
                Elm.AppendChild(Condition.GetDocumentElm(Elm.OwnerDocument));

            Elm.SetAttribute("Operator", mOperator);
            Elm.SetAttribute("Number", mNumber.ToString());
            Elm.SetAttribute("IsUseTotal", mIsUseTotal.ToString());

            return Elm; 
        }

        public System.Xml.XmlElement XMLElm
        {
            get
            {
                System.Xml.XmlElement Elm = (new System.Xml.XmlDocument()).CreateElement("缺曠條件集合");

                foreach (AttendanceCondition Condition in mConditions)
                    Elm.AppendChild(Condition.GetDocumentElm(Elm.OwnerDocument));

                Elm.SetAttribute("Operator", mOperator);
                Elm.SetAttribute("Number",mNumber.ToString());
                Elm.SetAttribute("IsUseTotal", mIsUseTotal.ToString());

                return Elm;
            }
        }

        public string XML
        {
            get
            {
                return XMLElm.OuterXml;
            }
        }
     }
}