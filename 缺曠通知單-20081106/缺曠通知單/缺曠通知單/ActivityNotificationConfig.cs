using System;
using System.Collections.Generic;
using System.Text;

namespace 缺曠通知單
{
    public class ActivityNotificationConfig
    {
        private string mStartDate;
        private string mEndDate;
        private string mReceiverType;
        private string mReceiverAddressType;
        private string mApplicationPath;
        private string mFileName;
        private List<string> mMeetingList;
        private List<string> mGeneralList;
        private ActivityNotificationPreference mPreference;

        private string mMinReward;
        private int mMinRewardCount;

        public ActivityNotificationConfig()
        {
            mStartDate = DateTime.Now.ToShortDateString();
            mEndDate = DateTime.Now.ToShortDateString();
            mReceiverType = "學生姓名";
            mReceiverAddressType = "戶籍地址";
            mMinReward = "警告";
            mMinRewardCount = 0;
            
        }

        public ActivityNotificationConfig(ActivityNotificationPreference Preference)
        {
            mReceiverType = Preference.ReceiveName;
            mReceiverAddressType = Preference.ReceiveAddress;
            mMeetingList = Preference.MeetingList;
            mGeneralList = Preference.GeneralList;
            mStartDate = Preference.StartDate;
            mEndDate = Preference.EndDate;
            mPreference = Preference;
        }

        public ActivityNotificationPreference Preference
        {
            get 
            {
                return mPreference;
            }
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

        public string MinReward
        {
            get
            {
                return mMinReward;
            }
        }

        public int MinRewardCount
        {
            get
            {
                return mMinRewardCount;
            }
        }


        public string FileName
        {
            get
            {
                return mFileName;
            }
        }

        public string ApplicationPath
        {
            get
            {
                return mApplicationPath;
            }
        }

        public string StartDate
        {
            get
            {
                return mStartDate;
            }
            set
            {
                mStartDate = value;
            }
        }

        public string EndDate
        {
            get
            {
                return mEndDate;
            }
            set
            {
                mEndDate = value;
            }
        }

        public string ReceiverType
        {
            get
            {
                return mReceiverType;
            }
            set
            {
                mReceiverType = value;
            }
        }

        public string ReceiverAddressType
        {
            get
            {
                return mReceiverAddressType;
            }
            set
            {
                mReceiverAddressType = value;
            }
        }
    }
}