using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.Customization.Data;
using SmartSchool.Customization.PlugIn.Report;
using SmartSchool.Customization.PlugIn;
using SmartSchool.Customization.Data.StudentExtension;

namespace 每月生活通知單
{
    public class ActivityNotificationRecord
    {
        private StudentRecord mstudent;
        private ActivityNotificationConfig  mconfig;

        static public Dictionary<string, string> AbsenceLookup
        {
            get
            {
                 Dictionary<string, string> mAbsenceLookup = new Dictionary<string, string>();

                SmartSchool.Customization.Data.SystemInformation.getField("Absence");

                System.Xml.XmlElement AbsenceElm = (System.Xml.XmlElement)SmartSchool.Customization.Data.SystemInformation.Fields["Absence"];

                foreach (System.Xml.XmlNode Node in AbsenceElm.SelectNodes("Absence"))
                    mAbsenceLookup.Add(Node.SelectSingleNode("@Name").InnerText,Node.SelectSingleNode("@Abbreviation").InnerText);

                return mAbsenceLookup;
            }
        }


        static public List<string> AbsenceType
        {
            get
            {
                List<string> lstAbsence = new List<string>();

                SmartSchool.Customization.Data.SystemInformation.getField("Absence");

                System.Xml.XmlElement  AbsenceElm=(System.Xml.XmlElement)SmartSchool.Customization.Data.SystemInformation.Fields["Absence"];

                foreach (System.Xml.XmlNode Node in AbsenceElm.SelectNodes("Absence"))
                    lstAbsence.Add(Node.SelectSingleNode("@Name").InnerText);

                 return lstAbsence;
            }
        }
    
        public ActivityNotificationRecord(StudentRecord student,ActivityNotificationConfig  config)
        {
            mstudent=student;
            mconfig=config;
        }

        public void FillAttendanceInfo()
        {
            //缺曠記錄
            foreach (AttendanceInfo Attend in mstudent.AttendanceList)
            {

            }
        }

        public string ZipCode
        {
            get
            {
                if (mconfig.ReceiverAddressType.Equals("戶籍地址"))
                    return mstudent.ContactInfo.PermanentAddress.ZipCode;
                else if (mconfig.ReceiverAddressType.Equals("連絡地址"))
                    return mstudent.ContactInfo.MailingAddress.ZipCode;
                else
                    return "";
            }
        }

        public string ZipCode01
        {
            get
            {
                return string.IsNullOrEmpty(ZipCode)?"":ZipCode.Substring(0, 1);
            }
        }

        public string ZipCode02
        {
            get
            {
                return string.IsNullOrEmpty(ZipCode) ? "" : ZipCode.Substring(1, 1);
            }
        }

        public string ZipCode03
        {
            get
            {
                return string.IsNullOrEmpty(ZipCode) ? "" : ZipCode.Substring(2, 1);
            }
        }

        public string ReceiverAddress
        {
            get
            {
                //戶籍地址
                string vReceiverAddress = "";

                if (mconfig.ReceiverAddressType.Equals("戶籍地址"))
                    vReceiverAddress = mstudent.ContactInfo.PermanentAddress.FullAddress;
                else if (mconfig.ReceiverAddressType.Equals("連絡地址"))
                    vReceiverAddress = mstudent.ContactInfo.MailingAddress.FullAddress;

                return vReceiverAddress.Equals("") ? vReceiverAddress : vReceiverAddress.Substring(4, vReceiverAddress.Length - 4);
            }
        }

        public string Receiver
        {
            get
            {
                if (mconfig.ReceiverType.Equals("學生姓名"))
                    return mstudent.StudentName;
                else if (mconfig.ReceiverType.Equals("監護人姓名"))
                    return mstudent.ParentInfo.CustodianName;
                else if (mconfig.ReceiverType.Equals("父親姓名"))
                    return mstudent.ParentInfo.FatherName;
                else if (mconfig.ReceiverType.Equals("母親姓名"))
                    return mstudent.ParentInfo.MotherName;
                else
                    return "";
            }
        }

        public string SchoolName
        {
            get
            {
                return SmartSchool.Customization.Data.SystemInformation.SchoolChineseName;
            }
        }

        public string SchoolAddress
        {
            get
            {
                return SmartSchool.Customization.Data.SystemInformation.Address;
            }
        }

        public string SchoolTel
        {
            get
            {
                return SmartSchool.Customization.Data.SystemInformation.Telephone;
            }
        }

        public string DataRange
        {
            get
            {
                return mconfig.StartDate + " ~ " + mconfig.EndDate;
            }
        }

        public string ClassName
        {
            get
            {
                return mstudent.RefClass.ClassName;
            }
        }

        public string SeatNo
        {
            get
            {
                return mstudent.SeatNo;
            }
        }

        public string StudentNumber
        {
            get
            {
                return mstudent.StudentNumber;
            }
        }

        public string StudentName
        {
            get
            {
                return mstudent.StudentName;
            }
        }

        public string TeacherName
        {
            get
            {
                return  mstudent.RefClass.RefTeacher.TeacherName;
            }
        }
    }
}