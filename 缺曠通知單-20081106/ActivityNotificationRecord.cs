using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.Customization.Data;
using SmartSchool.Customization.PlugIn.Report;
using SmartSchool.Customization.PlugIn;
using SmartSchool.Customization.Data.StudentExtension;

namespace 缺曠通知單
{
    public class ActivityNotificationRecord
    {
        private StudentRecord mstudent;
        private ActivityNotificationConfig mconfig;

        //取得系統中缺曠類別，有對照表的型態，例如曠課，對應到曠。
        static public Dictionary<string, string> AbsenceLookup
        {
            get
            {
                Dictionary<string, string> mAbsenceLookup = new Dictionary<string, string>();

                SmartSchool.Customization.Data.SystemInformation.getField("Absence");

                System.Xml.XmlElement AbsenceElm = (System.Xml.XmlElement)SmartSchool.Customization.Data.SystemInformation.Fields["Absence"];

                foreach (System.Xml.XmlNode Node in AbsenceElm.SelectNodes("Absence"))
                    mAbsenceLookup.Add(Node.SelectSingleNode("@Name").InnerText, Node.SelectSingleNode("@Abbreviation").InnerText);

                return mAbsenceLookup;
            }
        }

        //取得系統中缺曠類別，例如曠課、缺課、病假…等等。
        static public List<string> AbsenceType
        {
            get
            {
                List<string> lstAbsence = new List<string>();

                SmartSchool.Customization.Data.SystemInformation.getField("Absence");

                System.Xml.XmlElement AbsenceElm = (System.Xml.XmlElement)SmartSchool.Customization.Data.SystemInformation.Fields["Absence"];

                foreach (System.Xml.XmlNode Node in AbsenceElm.SelectNodes("Absence"))
                    if (!lstAbsence.Contains(Node.SelectSingleNode("@Name").InnerText))
                        lstAbsence.Add(Node.SelectSingleNode("@Name").InnerText);

                return lstAbsence;
            }
        }

        //取得系統中缺曠類別，例如曠課、缺課、病假…等等。
        static public List<string> PeriodType
        {
            get
            {
                List<string> lstPeriod = new List<string>();

                SmartSchool.Customization.Data.SystemInformation.getField("Period");

                System.Xml.XmlElement PeriodElm = (System.Xml.XmlElement)SmartSchool.Customization.Data.SystemInformation.Fields["Period"];

                foreach (System.Xml.XmlNode Node in PeriodElm.SelectNodes("Period"))
                    if (!lstPeriod.Contains(Node.SelectSingleNode("@Type").InnerText))
                       lstPeriod.Add(Node.SelectSingleNode("@Type").InnerText);

                return lstPeriod;
            }
        }


        public ActivityNotificationRecord(StudentRecord student, ActivityNotificationConfig config)
        {
            mstudent = student;
            mconfig = config;
        }

        public void FillAttendanceInfo()
        {
            //缺曠記錄
            foreach (AttendanceInfo Attend in mstudent.AttendanceList)
            {

            }
        }

        //取得學生郵遞區號
        public string ZipCode
        {
            get
            {
                if (mconfig.ReceiverAddressType.Equals("戶籍地址"))
                    return mstudent.ContactInfo.PermanentAddress.ZipCode;
                else if (mconfig.ReceiverAddressType.Equals("聯絡地址"))
                    return mstudent.ContactInfo.MailingAddress.ZipCode;
                else
                    return "";
            }
        }

        //取得郵遞區號第1碼
        public string ZipCode01
        {
            get
            {
                return string.IsNullOrEmpty(ZipCode) ? "" : ZipCode.Substring(0, 1);
            }
        }

        //取得郵遞區號第2碼
        public string ZipCode02
        {
            get
            {
                return string.IsNullOrEmpty(ZipCode) ? "" : ZipCode.Substring(1, 1);
            }
        }

        //取得郵遞區號第3碼
        public string ZipCode03
        {
            get
            {
                return string.IsNullOrEmpty(ZipCode) ? "" : ZipCode.Substring(2, 1);
            }
        }

        //取得郵遞地址，會依當初使用者選擇而有所不同
        public string ReceiverAddress
        {
            get
            {
                string vReceiverAddress = "";

                if (mconfig.ReceiverAddressType.Equals("戶籍地址"))
                    vReceiverAddress = mstudent.ContactInfo.PermanentAddress.FullAddress;
                else if (mconfig.ReceiverAddressType.Equals("聯絡地址"))
                    vReceiverAddress = mstudent.ContactInfo.MailingAddress.FullAddress;

                return vReceiverAddress.Equals("") ? vReceiverAddress : vReceiverAddress.Substring(4, vReceiverAddress.Length - 4);
            }
        }

        //取得收件人姓名，依當初使用者選擇而有所不同
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

        //取得學校名稱
        public string SchoolName
        {
            get
            {
                return SmartSchool.Customization.Data.SystemInformation.SchoolChineseName;
            }
        }

        //取得學校地址
        public string SchoolAddress
        {
            get
            {
                return SmartSchool.Customization.Data.SystemInformation.Address;
            }
        }

        //取得學校電話
        public string SchoolTel
        {
            get
            {
                return SmartSchool.Customization.Data.SystemInformation.Telephone;
            }
        }

        //取得資料期間字串
        public string DataRange
        {
            get
            {
                return mconfig.StartDate + " ~ " + mconfig.EndDate;
            }
        }

        //取得班級名稱
        public string ClassName
        {
            get
            {
                return mstudent.RefClass.ClassName;
            }
        }

        //取得學生座號
        public string SeatNo
        {
            get
            {
                return mstudent.SeatNo;
            }
        }

        //取得學生學份證
        public string StudentNumber
        {
            get
            {
                return mstudent.StudentNumber;
            }
        }

        //取得學生姓名
        public string StudentName
        {
            get
            {
                return mstudent.StudentName;
            }
        }

        //取得教師姓名
        public string TeacherName
        {
            get
            {
                return mstudent.RefClass.RefTeacher.TeacherName;
            }
        }
    }
}