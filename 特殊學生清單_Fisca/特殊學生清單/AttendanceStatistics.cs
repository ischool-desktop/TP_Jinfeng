using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.Customization.Data;
using SmartSchool.Customization.PlugIn.Report;
using SmartSchool.Customization.PlugIn;
using SmartSchool.Customization.Data.StudentExtension;

namespace 特殊學生清單
{
    public class AttendanceStatistics
    {
        //private Dictionary<string, Dictionary<string, string>> mAttendanceDetail;
        private Dictionary<string, string> mAbsenceLookup;
        //private List<string> mMeetingList;
        //private List<string> mGeneralList;
        private Dictionary<string, int> mMeetingDict;
        private Dictionary<string, int> mGeneralDict;
        private Dictionary<string, int> mMeetingSemesterDict;
        private Dictionary<string, int> mGeneralSemesterDict;

        //private string GetDateString(DateTime Date)
        //{
        //    return Date.ToShortDateString();
        //}

        //private bool IsAllAttendance(AttendanceInfo Attendance)
        //{
        //    if (Attendance.PeriodType.Equals("集會") && mMeetingList.Contains(Attendance.Absence))
        //    {

        //        mMeetingDict[Attendance.Absence]++;

        //        return true;
        //    }
        //    else if (Attendance.PeriodType.Equals("一般") && mGeneralList.Contains(Attendance.Absence))
        //    {
        //        mGeneralDict[Attendance.Absence]++;

        //        return true;
        //    }

        //    return false;
        //}

        private void CountAttendance(AttendanceInfo Attendance)
        {
            if (Attendance.Semester == SmartSchool.Customization.Data.SystemInformation.Semester && Attendance.SchoolYear == SmartSchool.Customization.Data.SystemInformation.SchoolYear)
            {

                if (Attendance.PeriodType.Equals("集會") && mMeetingList.Contains(Attendance.Absence))
                {

                    mMeetingSemesterDict[Attendance.Absence]++;
                    
                }
                else if (Attendance.PeriodType.Equals("一般") && mGeneralList.Contains(Attendance.Absence))
                {
                    mGeneralSemesterDict[Attendance.Absence]++;
                    
                }

            }
            if (Attendance.PeriodType.Equals("集會") && mMeetingList.Contains(Attendance.Absence))
            {

                mMeetingDict[Attendance.Absence]++;
                
            }
            else if (Attendance.PeriodType.Equals("一般") && mGeneralList.Contains(Attendance.Absence))
            {
                mGeneralDict[Attendance.Absence]++;
                
            }
            
        }


        //private int CompareDate(AttendanceInfo a, AttendanceInfo b)
        //{
        //    return a.OccurDate.CompareTo(b.OccurDate);
        //}

        private void InternalStatistics(List<AttendanceInfo> AttendanceList, List<string> MeetingList, List<string> GeneralList)
        {
            //AttendanceList.Sort(CompareDate);

            //mMeetingList = MeetingList;
            //mGeneralList = GeneralList;
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

            //mAttendanceDetail = new Dictionary<string, Dictionary<string, string>>();
            //mAbsenceLookup = ActivityNotificationRecord.AbsenceLookup;

            foreach (AttendanceInfo Attendance in AttendanceList)
            {
                CountAttendance(Attendance);
                
                //if (Attendance.OccurDate >= DateTime.Parse(StartDate) && Attendance.OccurDate <= DateTime.Parse(EndDate))
                //{
                    //if (IsRangeAttendance(Attendance))
                    //{
                    //    string strOccurDate = GetDateString(Attendance.OccurDate);
                    //    if (!mAttendanceDetail.ContainsKey(strOccurDate))
                    //        mAttendanceDetail.Add(strOccurDate, new Dictionary<string, string>());
                    //    mAttendanceDetail[strOccurDate].Add(Attendance.Period, mAbsenceLookup[Attendance.Absence]);
                    //}
               // }
            }
        }

        //static public bool IsQualified(List<AttendanceInfo> AttendanceList, AttendanceConditions vConditions, string StartDate, string EndDate)
        //{
        //    //將實際符合缺曠條件的數字歸0
        //    foreach (AttendanceCondition Condition in vConditions.List)
        //        Condition.ReallNumber = 0;

        //    //統計符合節次類別及缺曠假別數
        //    foreach (AttendanceInfo Attendance in AttendanceList)
        //        if (Attendance.OccurDate >= DateTime.Parse(StartDate) && Attendance.OccurDate <= DateTime.Parse(EndDate))
        //        {

        //            foreach (AttendanceCondition Condition in vConditions.List)
        //                if (Attendance.PeriodType.Equals(Condition.PeriodType) && Attendance.Absence.Equals(Condition.AbsenceType))
        //                    Condition.ReallNumber++;
        //        }

        //    return vConditions.IsQualified;
        //}

        public AttendanceStatistics(List<AttendanceInfo> AttendanceList,  List<string> MeetingList, List<string> GeneralList)
        {
            InternalStatistics(AttendanceList,  MeetingList, GeneralList);
        }

        public List<string> MeetingList
        {
            get
            {
                return mMeetingList;
            }
        }

        public List<string> GeneralList
        {
            get
            {
                return mGeneralList;
            }
        }

        //取得集會缺曠統計，根據在學期間
        public Dictionary<string, int> MeetingCount
        {
            get
            {
                return mMeetingDict;
            }
        }

        //取得一般缺曠統計，根據根據在學期間
        public Dictionary<string, int> GeneralCount
        {
            get
            {
                return mGeneralDict;
            }
        }

        //取得集會缺曠統計，根據本學期來統計
        public Dictionary<string, int> MeetingSemesterCount
        {
            get
            {
                return mMeetingSemesterDict;
            }
        }
        //取得一般缺曠統計，根據本學期來統計
        public Dictionary<string, int> GeneralSemesterCount
        {
            get
            {
                return mGeneralSemesterDict;
            }
        }

        ////取得缺曠明細
        //public object[] DocumentAttendanceDetail
        //{
        //    get
        //    {
        //        List<string> p = new List<string>(new string[] { "早修", "升旗", "一", "二", "三", "四", "午休", "五", "六", "七", "八" });
        //        object[] obj = new object[] { mAttendanceDetail, p };
        //        return obj;
        //    }
        //}
    }
}
