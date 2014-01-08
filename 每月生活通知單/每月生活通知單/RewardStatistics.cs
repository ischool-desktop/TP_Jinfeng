using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.Customization.Data;
using SmartSchool.Customization.PlugIn.Report;
using SmartSchool.Customization.PlugIn;
using SmartSchool.Customization.Data.StudentExtension;


namespace 每月生活通知單
{
    public class RewardStatistics
    {
        private List<string> mRewardCommentList;
        private List<RewardInfo> mRewardList;

        private string mMinReward;
        private int mMinRewardCount;
        
        private DateTime mStartDate;
        private DateTime mEndDate;

        private int mAwardACount = 0;
        private int mAwardBCount = 0;
        private int mAwardCCount = 0;
        private int mFaultACount = 0;
        private int mFaultBCount = 0;
        private int mFaultCCount = 0;

        private int mAwardASemesterCount = 0;
        private int mAwardBSemesterCount = 0;
        private int mAwardCSemesterCount = 0;
        private int mFaultASemesterCount = 0;
        private int mFaultBSemesterCount = 0;
        private int mFaultCSemesterCount = 0;


        private string GetRewardComment(RewardInfo Reward)
        {
            string strRewardComment = "";
            if (Reward.UltimateAdmonition)
            {
                strRewardComment = Reward.OccurDate.Month.ToString() + "/" + Reward.OccurDate.Day.ToString();
                strRewardComment += " " + Reward.OccurReason + "留校查看";
                return strRewardComment;
            }
            //警告
            if (mMinReward.Equals("警告"))
            {
                if (Reward.AwardA > 0)
                {
                    strRewardComment = Reward.OccurDate.Month.ToString() + "/" + Reward.OccurDate.Day.ToString();
                    strRewardComment += " " + Reward.OccurReason;
                    
                    strRewardComment += " 大功" + Reward.AwardA.ToString() + " 次";
                    mAwardACount += Reward.AwardA;
                    if (Reward.AwardB > 0)
                    {
                        strRewardComment += " 小功" + Reward.AwardB.ToString() + " 次";
                        mAwardBCount += Reward.AwardB;
                    }
                    if (Reward.AwardC > 0)
                    {
                        strRewardComment += " 嘉獎" + Reward.AwardC.ToString() + " 次";
                        mAwardCCount += Reward.AwardC;                    
                    }
                    return strRewardComment;
                }

                if (Reward.AwardB > 0)
                {
                    strRewardComment = Reward.OccurDate.Month.ToString() + "/" + Reward.OccurDate.Day.ToString();
                    strRewardComment += " " + Reward.OccurReason;
                    
                    strRewardComment += " 小功" + Reward.AwardB.ToString() + " 次";
                    mAwardBCount += Reward.AwardB;
                    if (Reward.AwardC > 0)
                    {
                        strRewardComment += " 嘉獎" + Reward.AwardC.ToString() + " 次";
                        mAwardCCount += Reward.AwardC;
                    }
                    return strRewardComment;
                }

                if (Reward.AwardC > 0)
                {
                   
                    strRewardComment = Reward.OccurDate.Month.ToString() + "/" + Reward.OccurDate.Day.ToString();
                    strRewardComment += " " + Reward.OccurReason;
                    
                    strRewardComment += " 嘉獎" + Reward.AwardC.ToString() + " 次";
                    mAwardCCount += Reward.AwardC;
                    return strRewardComment;
                }

                if (Reward.FaultA > 0)
                {                    
                    strRewardComment = Reward.OccurDate.Month.ToString() + "/" + Reward.OccurDate.Day.ToString();
                    strRewardComment += " " + Reward.OccurReason;
                    strRewardComment += " 大過" + Reward.FaultA.ToString() + " 次";
                    if (Reward.FaultB > 0)
                        strRewardComment += " 小過" + Reward.FaultB.ToString() + " 次";
                    if (Reward.FaultC>0)
                        strRewardComment += " 警告" + Reward.FaultC.ToString() + " 次";

                    if (Reward.Cleared)
                        strRewardComment += "(已銷)";
                    else
                    {
                        mFaultACount += Reward.FaultA;
                        mFaultBCount += Reward.FaultB;
                        mFaultCCount += Reward.FaultC;
                    }
                    return strRewardComment;
                }

                if (Reward.FaultB > 0)
                {
                   
                    strRewardComment = Reward.OccurDate.Month.ToString() + "/" + Reward.OccurDate.Day.ToString();
                    strRewardComment += " " + Reward.OccurReason;
                    
                    strRewardComment += " 小過" + Reward.FaultB.ToString() + " 次";
                    if (Reward.FaultC > 0)
                        strRewardComment += " 警告" + Reward.FaultC.ToString() + " 次";
                    if (Reward.Cleared)
                        strRewardComment += "(已銷)";
                    else
                    {
                        mFaultBCount += Reward.FaultB;
                        mFaultCCount += Reward.FaultC;
                    }
                    return strRewardComment;
                }

                if (Reward.FaultC >= mMinRewardCount && Reward.FaultC>0)
                {
                   
                    strRewardComment = Reward.OccurDate.Month.ToString() + "/" + Reward.OccurDate.Day.ToString();
                    strRewardComment += " " + Reward.OccurReason;
                    
                    strRewardComment += " 警告" + Reward.FaultC.ToString() + " 次";
                    if (Reward.Cleared)
                        strRewardComment += "(已銷)";
                    else 
                        mFaultCCount += Reward.FaultC;
                    return strRewardComment;
                }
            }

            //小過
            if (mMinReward.Equals("小過"))
            {
                if (Reward.AwardA > 0)
                {
                    strRewardComment = Reward.OccurDate.Month.ToString() + "/" + Reward.OccurDate.Day.ToString();
                    strRewardComment += " " + Reward.OccurReason;

                    strRewardComment += " 大功" + Reward.AwardA.ToString() + " 次";
                    mAwardACount += Reward.AwardA;
                    if (Reward.AwardB > 0)
                    {
                        strRewardComment += " 小功" + Reward.AwardB.ToString() + " 次";
                        mAwardBCount += Reward.AwardB;
                    }
                    if (Reward.AwardC > 0)
                    {
                        strRewardComment += " 嘉獎" + Reward.AwardC.ToString() + " 次";
                        mAwardCCount += Reward.AwardC;
                    }
                    return strRewardComment;
                }

                if (Reward.AwardB > 0)
                {
                    strRewardComment = Reward.OccurDate.Month.ToString() + "/" + Reward.OccurDate.Day.ToString();
                    strRewardComment += " " + Reward.OccurReason;

                    strRewardComment += " 小功" + Reward.AwardB.ToString() + " 次";
                    mAwardBCount += Reward.AwardB;
                    if (Reward.AwardC > 0)
                    {
                        strRewardComment += " 嘉獎" + Reward.AwardC.ToString() + " 次";
                        mAwardCCount += Reward.AwardC;
                    }
                    return strRewardComment;
                }

                if (Reward.AwardC > 0)
                {

                    strRewardComment = Reward.OccurDate.Month.ToString() + "/" + Reward.OccurDate.Day.ToString();
                    strRewardComment += " " + Reward.OccurReason;

                    strRewardComment += " 嘉獎" + Reward.AwardC.ToString() + " 次";
                    mAwardCCount += Reward.AwardC;
                    return strRewardComment;
                }

                if (Reward.FaultA > 0)
                {
                    strRewardComment = Reward.OccurDate.Month.ToString() + "/" + Reward.OccurDate.Day.ToString();
                    strRewardComment += " " + Reward.OccurReason;
                    strRewardComment += " 大過" + Reward.FaultA.ToString() + " 次";
                    if (Reward.FaultB > 0)
                        strRewardComment += " 小過" + Reward.FaultB.ToString() + " 次";
                    if (Reward.FaultC > 0)
                        strRewardComment += " 警告" + Reward.FaultC.ToString() + " 次";

                    if (Reward.Cleared)
                        strRewardComment += "(已銷)";
                    else
                    {
                        mFaultACount += Reward.FaultA;
                        mFaultBCount += Reward.FaultB;
                        mFaultCCount += Reward.FaultC;
                    }
                    return strRewardComment;
                }

                if (Reward.FaultB >= mMinRewardCount && Reward.FaultB > 0)
                {

                    strRewardComment = Reward.OccurDate.Month.ToString() + "/" + Reward.OccurDate.Day.ToString();
                    strRewardComment += " " + Reward.OccurReason;

                    strRewardComment += " 小過" + Reward.FaultB.ToString() + " 次";
                    if (Reward.FaultC > 0)
                        strRewardComment += " 警告" + Reward.FaultC.ToString() + " 次";
                    if (Reward.Cleared)
                        strRewardComment += "(已銷)";
                    else
                    {
                        mFaultBCount += Reward.FaultB;
                        mFaultCCount += Reward.FaultC;
                    }
                    return strRewardComment;
                }
               
            }

            //大過
              if (mMinReward.Equals("大過"))
             {

                 if (Reward.AwardA > 0)
                 {
                     strRewardComment = Reward.OccurDate.Month.ToString() + "/" + Reward.OccurDate.Day.ToString();
                     strRewardComment += " " + Reward.OccurReason;

                     strRewardComment += " 大功" + Reward.AwardA.ToString() + " 次";
                     mAwardACount += Reward.AwardA;
                     if (Reward.AwardB > 0)
                     {
                         strRewardComment += " 小功" + Reward.AwardB.ToString() + " 次";
                         mAwardBCount += Reward.AwardB;
                     }
                     if (Reward.AwardC > 0)
                     {
                         strRewardComment += " 嘉獎" + Reward.AwardC.ToString() + " 次";
                         mAwardCCount += Reward.AwardC;
                     }
                     return strRewardComment;
                 }

                 if (Reward.AwardB > 0)
                 {
                     strRewardComment = Reward.OccurDate.Month.ToString() + "/" + Reward.OccurDate.Day.ToString();
                     strRewardComment += " " + Reward.OccurReason;

                     strRewardComment += " 小功" + Reward.AwardB.ToString() + " 次";
                     mAwardBCount += Reward.AwardB;
                     if (Reward.AwardC > 0)
                     {
                         strRewardComment += " 嘉獎" + Reward.AwardC.ToString() + " 次";
                         mAwardCCount += Reward.AwardC;
                     }
                     return strRewardComment;
                 }

                 if (Reward.AwardC > 0)
                 {

                     strRewardComment = Reward.OccurDate.Month.ToString() + "/" + Reward.OccurDate.Day.ToString();
                     strRewardComment += " " + Reward.OccurReason;

                     strRewardComment += " 嘉獎" + Reward.AwardC.ToString() + " 次";
                     mAwardCCount += Reward.AwardC;
                     return strRewardComment;
                 }

                 if (Reward.FaultA >= mMinRewardCount && Reward.FaultA > 0)
                 {
                     strRewardComment = Reward.OccurDate.Month.ToString() + "/" + Reward.OccurDate.Day.ToString();
                     strRewardComment += " " + Reward.OccurReason;
                     strRewardComment += " 大過" + Reward.FaultA.ToString() + " 次";
                     if (Reward.FaultB > 0)
                         strRewardComment += " 小過" + Reward.FaultB.ToString() + " 次";
                     if (Reward.FaultC > 0)
                         strRewardComment += " 警告" + Reward.FaultC.ToString() + " 次";

                     if (Reward.Cleared)
                         strRewardComment += "(已銷)";
                     else
                     {
                         mFaultACount += Reward.FaultA;
                         mFaultBCount += Reward.FaultB;
                         mFaultCCount += Reward.FaultC;
                     }
                     return strRewardComment;
                 }
               
            }

            //嘉獎
            if (mMinReward.Equals("嘉獎"))
            {

                if (Reward.AwardA > 0)
                {
                    strRewardComment = Reward.OccurDate.Month.ToString() + "/" + Reward.OccurDate.Day.ToString();
                    strRewardComment += " " + Reward.OccurReason;

                    strRewardComment += " 大功" + Reward.AwardA.ToString() + " 次";
                    mAwardACount += Reward.AwardA;
                    if (Reward.AwardB > 0)
                    {
                        strRewardComment += " 小功" + Reward.AwardB.ToString() + " 次";
                        mAwardBCount += Reward.AwardB;
                    }
                    if (Reward.AwardC > 0)
                    {
                        strRewardComment += " 嘉獎" + Reward.AwardC.ToString() + " 次";
                        mAwardCCount += Reward.AwardC;
                    }
                    return strRewardComment;
                }

                if (Reward.AwardB > 0)
                {
                    strRewardComment = Reward.OccurDate.Month.ToString() + "/" + Reward.OccurDate.Day.ToString();
                    strRewardComment += " " + Reward.OccurReason;

                    strRewardComment += " 小功" + Reward.AwardB.ToString() + " 次";
                    mAwardBCount += Reward.AwardB;
                    if (Reward.AwardC > 0)
                    {
                        strRewardComment += " 嘉獎" + Reward.AwardC.ToString() + " 次";
                        mAwardCCount += Reward.AwardC;
                    }
                    return strRewardComment;
                }

                if (Reward.AwardC > 0)
                {

                    strRewardComment = Reward.OccurDate.Month.ToString() + "/" + Reward.OccurDate.Day.ToString();
                    strRewardComment += " " + Reward.OccurReason;

                    strRewardComment += " 嘉獎" + Reward.AwardC.ToString() + " 次";
                    mAwardCCount += Reward.AwardC;
                    return strRewardComment;
                }
            }

            //小功
            if (mMinReward.Equals("小功"))
            {

                if (Reward.AwardA > 0)
                {
                    strRewardComment = Reward.OccurDate.Month.ToString() + "/" + Reward.OccurDate.Day.ToString();
                    strRewardComment += " " + Reward.OccurReason;

                    strRewardComment += " 大功" + Reward.AwardA.ToString() + " 次";
                    mAwardACount += Reward.AwardA;
                    if (Reward.AwardB > 0)
                    {
                        strRewardComment += " 小功" + Reward.AwardB.ToString() + " 次";
                        mAwardBCount += Reward.AwardB;
                    }
                    if (Reward.AwardC > 0)
                    {
                        strRewardComment += " 嘉獎" + Reward.AwardC.ToString() + " 次";
                        mAwardCCount += Reward.AwardC;
                    }
                    return strRewardComment;
                }

                if (Reward.AwardB >= mMinRewardCount && Reward.AwardB > 0)
                {
                    strRewardComment = Reward.OccurDate.Month.ToString() + "/" + Reward.OccurDate.Day.ToString();
                    strRewardComment += " " + Reward.OccurReason;

                    strRewardComment += " 小功" + Reward.AwardB.ToString() + " 次";
                    mAwardBCount += Reward.AwardB;
                    if (Reward.AwardC > 0)
                    {
                        strRewardComment += " 嘉獎" + Reward.AwardC.ToString() + " 次";
                        mAwardCCount += Reward.AwardC;
                    }
                    return strRewardComment;
                }

            }

            //大功
            if (mMinReward.Equals("大功"))
            {

                strRewardComment = Reward.OccurDate.Month.ToString() + "/" + Reward.OccurDate.Day.ToString();
                strRewardComment += " " + Reward.OccurReason;

                strRewardComment += " 大功" + Reward.AwardA.ToString() + " 次";
                mAwardACount += Reward.AwardA;
                if (Reward.AwardB > 0)
                {
                    strRewardComment += " 小功" + Reward.AwardB.ToString() + " 次";
                    mAwardBCount += Reward.AwardB;
                }
                if (Reward.AwardC > 0)
                {
                    strRewardComment += " 嘉獎" + Reward.AwardC.ToString() + " 次";
                    mAwardCCount += Reward.AwardC;
                }
                return strRewardComment;
            }
            return strRewardComment;
        }

        private void SetSemesterCount(RewardInfo Reward)
        {
            //警告
            if (mMinReward.Equals("警告") && !Reward.Cleared)
            {
                if (Reward.AwardA > 0)
                    mAwardASemesterCount += Reward.AwardA;

                if (Reward.AwardB > 0)
                    mAwardBSemesterCount += Reward.AwardB;

                if (Reward.AwardC > 0)
                    mAwardCSemesterCount += Reward.AwardC;

                if (Reward.FaultA > 0)
                    mFaultASemesterCount += Reward.FaultA;

                if (Reward.FaultB > 0)
                    mFaultBSemesterCount += Reward.FaultB;

                if (Reward.FaultC >= mMinRewardCount && Reward.FaultC > 0)
                    mFaultCSemesterCount += Reward.FaultC;
            }

            //小過
            if (mMinReward.Equals("小過") && !Reward.Cleared)
            {
                if (Reward.AwardA > 0)
                    mAwardASemesterCount += Reward.AwardA;

                if (Reward.AwardB > 0)
                    mAwardBSemesterCount += Reward.AwardB;

                if (Reward.AwardC > 0)
                    mAwardCSemesterCount += Reward.AwardC;

                if (Reward.FaultA > 0)
                    mFaultASemesterCount += Reward.FaultA;

                if (Reward.FaultB >= mMinRewardCount && Reward.FaultB > 0)
                    mFaultBSemesterCount += Reward.FaultB;
            }

            //大過
            if (mMinReward.Equals("大過") && !Reward.Cleared)
            {

                if (Reward.AwardA > 0)
                    mAwardASemesterCount += Reward.AwardA;

                if (Reward.AwardB > 0)
                    mAwardBSemesterCount += Reward.AwardB;

                if (Reward.AwardC > 0)
                    mAwardCSemesterCount += Reward.AwardC;

                if (Reward.FaultA >= mMinRewardCount && Reward.FaultA > 0)
                    mFaultASemesterCount += Reward.FaultA;
            }

            //嘉獎
            if (mMinReward.Equals("嘉獎"))
            {

                if (Reward.AwardA > 0)
                    mAwardASemesterCount += Reward.AwardA;

                if (Reward.AwardB > 0)
                    mAwardBSemesterCount += Reward.AwardB;

                if (Reward.AwardC >= mMinRewardCount && Reward.AwardC > 0)
                    mAwardCSemesterCount += Reward.AwardC;
            }

            //小功
            if (mMinReward.Equals("小功"))
            {

                if (Reward.AwardA > 0)
                    mAwardASemesterCount += Reward.AwardA;

                if (Reward.AwardB >= mMinRewardCount && Reward.AwardB > 0)
                    mAwardBSemesterCount += Reward.AwardB;

            }

            //大功
            if (mMinReward.Equals("大功"))
            {

                if (Reward.AwardA >= mMinRewardCount && Reward.AwardA > 0)
                    mAwardASemesterCount += Reward.AwardA;
            }
 
        }

        public RewardStatistics(List<RewardInfo> RewardList,string MinReward,int MinRewardCount,string StartDate,string EndDate)
        {
            mRewardList = RewardList;
            mMinReward = MinReward;
            mMinRewardCount = MinRewardCount;

            if (!DateTime.TryParse(StartDate,out mStartDate))
                mStartDate=DateTime.Now;

            if (!DateTime.TryParse(EndDate,out mEndDate))
                mEndDate=DateTime.Now;

            mRewardCommentList = new List<string>();

            //獎懲記錄
            foreach (RewardInfo Reward in mRewardList)
            {
                if (Reward.SchoolYear ==SmartSchool.Customization.Data.SystemInformation.SchoolYear  && Reward.Semester==SmartSchool.Customization.Data.SystemInformation .Semester )
                  SetSemesterCount(Reward);

                if (Reward.OccurDate>=mStartDate && Reward.OccurDate<=mEndDate)
                {
                    string RewardComment = GetRewardComment(Reward);
                    if (!RewardComment.Equals(""))
                    mRewardCommentList.Add(RewardComment);
                }
            }

        }

        public List<string> RewardCommentList
        {
            get
            {
                return mRewardCommentList;
            }
        }

        public int AwardACount
        {
            get
            {
                return mAwardACount;
            }
        }

        public int AwardBCount
        {
            get
            {
                return mAwardBCount;
            }
        }

        public int AwardCCount
        {
            get
            {
                return mAwardCCount;
            }
        }

        public int FaultACount
        {
            get
            {
                return mFaultACount;
            }
        }

        public int FaultBCount
        {
            get
            {
                return mFaultBCount;
            }
        }

        public int FaultCCount
        {
            get
            {
                return mFaultCCount;
            }
        }

        public int AwardASemesterCount
        {
            get
            {
                return mAwardASemesterCount;
            }
        }

        public int AwardBSemesterCount
        {
            get
            {
                return mAwardBSemesterCount;
            }
        }

        public int AwardCSemesterCount
        {
            get
            {
                return mAwardCSemesterCount;
            }
        }

        public int FaultASemesterCount
        {
            get
            {
                return mFaultASemesterCount;
            }
        }

        public int FaultBSemesterCount
        {
            get
            {
                return mFaultBSemesterCount;
            }
        }

        public int FaultCSemesterCount
        {
            get
            {
                return mFaultCSemesterCount;
            }
        }
    }
}