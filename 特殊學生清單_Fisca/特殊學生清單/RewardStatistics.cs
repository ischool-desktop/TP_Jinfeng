using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.Customization.Data;
using SmartSchool.Customization.PlugIn.Report;
using SmartSchool.Customization.PlugIn;
using SmartSchool.Customization.Data.StudentExtension;


namespace 特殊學生清單
{
    public class RewardStatistics
    {

        private int mAwardACount = 0;
        private int mAwardBCount = 0;
        private int mAwardCCount = 0;
        private int mFaultACount = 0;
        private int mFaultBCount = 0;
        private int mFaultCCount = 0;
        private List<RewardInfo> mRewardList;
        //private SpecialStudentConfig mconfig;
        public  RewardStatistics(List<RewardInfo> RewardList, int Kind,SpecialStudentConfig mconfig)
        {
            mRewardList = RewardList;           
            

            //獎懲記錄
            foreach (RewardInfo Reward in mRewardList)
            {
                if (Kind == 1 || Kind==2)
                {
                    if (Reward.SchoolYear ==mconfig.thisSchoolYear  && Reward.Semester == mconfig.thisSemester)
                        if (Kind == 3)
                        {
                            if (Reward.OccurDate >= mconfig.StartDate && Reward.OccurDate <= mconfig.EndDate)
                                SetCount(Reward);                        
                        }
                        else
                            SetCount(Reward);
                }
                else                   
                    SetCount(Reward);
            }

        }

        private void SetCount(RewardInfo Reward)
        {
          
          if (Reward.AwardA > 0)
              mAwardACount += Reward.AwardA;

          if (Reward.AwardB > 0)
              mAwardBCount += Reward.AwardB;

          if (Reward.AwardC > 0)
              mAwardCCount += Reward.AwardC;

          if (Reward.FaultA > 0 && !Reward.Cleared )
              mFaultACount += Reward.FaultA;

          if (Reward.FaultB > 0 && !Reward.Cleared )
              mFaultBCount += Reward.FaultB;

          if (Reward.FaultC > 0 && !Reward.Cleared)
              mFaultCCount += Reward.FaultC;
       
        }

        //取得大功數量
        public int AwardACount
        {
            get
            {
                return mAwardACount;
            }
        }

        //取得小功數量
        public int AwardBCount
        {
            get
            {
                return mAwardBCount;
            }
        }

        //取得嘉獎數量
        public int AwardCCount
        {
            get
            {
                return mAwardCCount;
            }
        }

        //取得大過數量
        public int FaultACount
        {
            get
            {
                return mFaultACount;
            }
        }

        //取得小過數量
        public int FaultBCount
        {
            get
            {
                return mFaultBCount;
            }
        }

        //取得警告數量
        public int FaultCCount
        {
            get
            {
                return mFaultCCount;
            }
        }

      
    }
}