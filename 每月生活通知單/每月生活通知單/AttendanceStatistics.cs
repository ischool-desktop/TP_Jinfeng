using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.Customization.Data;
using SmartSchool.Customization.PlugIn.Report;
using SmartSchool.Customization.PlugIn;
using SmartSchool.Customization.Data.StudentExtension;

namespace 每月生活通知單
{
    public class AttendanceStatistics
    {
        //SmartSchool.Customization.Data.SystemInformation.getField("Period");
        //SmartSchool.Customization.Data.SystemInformation.Fields["Period"] as XmlElement;

        private Dictionary<string, Dictionary<string, string>> mAttendanceDetail;
        private Dictionary<string,string> mAbsenceLookup;
        private List<string> mMeetingList;
        private List<string> mGeneralList;
        private Dictionary<string, int> mMeetingDict;
        private Dictionary<string, int> mGeneralDict;
        private Dictionary<string, int> mMeetingSemesterDict;
        private Dictionary<string, int> mGeneralSemesterDict;
 
        private string GetDateString(DateTime Date)
        {
            return Date.ToShortDateString();
        }

        private bool IsRangeAttendance(AttendanceInfo Attendance)
        {
            //FISCA 及 AE版 沒有PeriodType
            //if (Attendance.PeriodType.Equals("集會") && mMeetingList.Contains(Attendance.Absence))
            if (Attendance.PeriodType.Equals("") && mMeetingList.Contains(Attendance.Absence))
            {

                mMeetingDict[Attendance.Absence]++;

                return true;
            }
            //else if (Attendance.PeriodType.Equals("一般") && mGeneralList.Contains(Attendance.Absence))
            else if (Attendance.PeriodType.Equals("") && mGeneralList.Contains(Attendance.Absence))
            {
                mGeneralDict[Attendance.Absence]++;

                return true;
            }

            return false;
        }

        private bool IsSemesterAttendance(AttendanceInfo Attendance)
        {
            if (Attendance.Semester == SmartSchool.Customization.Data.SystemInformation.Semester && Attendance.SchoolYear == SmartSchool.Customization.Data.SystemInformation.SchoolYear)
            {

                //if (Attendance.PeriodType.Equals("集會") && mMeetingList.Contains(Attendance.Absence))
                if (Attendance.PeriodType.Equals("") && mMeetingList.Contains(Attendance.Absence))
                {

                    mMeetingSemesterDict[Attendance.Absence]++;

                    return true;
                }
                //else if (Attendance.PeriodType.Equals("一般") && mGeneralList.Contains(Attendance.Absence))
                else if (Attendance.PeriodType.Equals("") && mGeneralList.Contains(Attendance.Absence))
                {
                    mGeneralSemesterDict[Attendance.Absence]++;

                    return true;
                }

            }
            return false;
        }


        private int CompareDate(AttendanceInfo a, AttendanceInfo b)
        {
            return a.OccurDate.CompareTo(b.OccurDate);
        }


        public AttendanceStatistics(List<AttendanceInfo> AttendanceList,string StartDate,string EndDate,List<string> MeetingList,List<string> GeneralList)
        {

            AttendanceList.Sort(CompareDate);

            mMeetingList = MeetingList;
            mGeneralList = GeneralList;
            mMeetingDict = new Dictionary<string, int>();
            mGeneralDict = new Dictionary<string, int>();
            mMeetingSemesterDict = new Dictionary<string, int>();
            mGeneralSemesterDict = new Dictionary<string, int>();


            for (int i = 0; i < mMeetingList.Count; i++)
            {
                mMeetingDict.Add(mMeetingList[i], 0);
                mMeetingSemesterDict.Add(mMeetingList[i], 0);
            }

            for (int i = 0; i < mGeneralList.Count; i++)
            {
                mGeneralDict.Add(mGeneralList[i], 0);
                mGeneralSemesterDict.Add(mGeneralList[i], 0);
            }

            mAttendanceDetail = new Dictionary<string, Dictionary<string, string>>();
            mAbsenceLookup = ActivityNotificationRecord.AbsenceLookup;
            //mAbsenceLookup.Add("病假","病");
            //mAbsenceLookup.Add("事假", "事");
            //mAbsenceLookup.Add("公假", "公");
            //mAbsenceLookup.Add("喪假", "喪");
            //mAbsenceLookup.Add("曠課","曠");


            foreach(AttendanceInfo Attendance in AttendanceList )
            {
                IsSemesterAttendance(Attendance);

                if (Attendance.OccurDate >= DateTime.Parse(StartDate) && Attendance.OccurDate <= DateTime.Parse(EndDate))
                {
                    if (IsRangeAttendance(Attendance))
                    {
                        string strOccurDate = GetDateString(Attendance.OccurDate);
                        if (!mAttendanceDetail.ContainsKey(strOccurDate))
                            mAttendanceDetail.Add(strOccurDate, new Dictionary<string, string>());
                        mAttendanceDetail[strOccurDate].Add(Attendance.Period, mAbsenceLookup[Attendance.Absence]);
                    }
                }
            }
        }

        public Dictionary<string, int> MeetingCount
        {
            get
            {
                return mMeetingDict;
            }
        }

        public Dictionary<string, int> GeneralCount
        {
            get
            {
                return mGeneralDict;
            }
        }

        public Dictionary<string, int> MeetingSemesterCount
        {
            get
            {
                return mMeetingSemesterDict;
            }
        }

        public Dictionary<string, int> GeneralSemesterCount
        {
            get
            {
                return mGeneralSemesterDict;
            }
        }


        public object[] DocumentAttendanceDetail
        {
            get
            {
                List<string> p = new List<string>(new string[] { "早修", "升旗", "一", "二", "三", "四", "午休", "五", "六", "七", "八" });
                object[] obj = new object[] { mAttendanceDetail, p };
                return obj;
            }
        }

    }
}