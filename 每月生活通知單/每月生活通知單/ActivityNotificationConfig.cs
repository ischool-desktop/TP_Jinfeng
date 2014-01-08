using System;
using System.Collections.Generic;
using System.Text;

namespace 每月生活通知單
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

        private string mMinReward;
        private int mMinRewardCount;
        private Boolean  mchkePaper;

        public ActivityNotificationConfig()
        {
            mStartDate = DateTime.Now.ToShortDateString();
            mEndDate = DateTime.Now.ToShortDateString();
            mReceiverType = "學生姓名";
            mReceiverAddressType = "戶籍地址";
            mMinReward = "警告";
            mMinRewardCount = 0;
        }

        public ActivityNotificationConfig(frmActivityNotification ActivityNotificationForm)
        {
            mStartDate = ActivityNotificationForm.StartDate;
            mEndDate = ActivityNotificationForm.EndDate;
            mReceiverType = ActivityNotificationForm.ReceiverType;
            mReceiverAddressType = ActivityNotificationForm.ReceiverAddressType;
            mApplicationPath = ActivityNotificationForm.ApplicationPath;
            mFileName=ActivityNotificationForm.FileName;

            mMinReward = ActivityNotificationForm.MinReward;
            mMinRewardCount = ActivityNotificationForm.MinRewardCount;

            mMeetingList = ActivityNotificationForm.MeetingList;
            mGeneralList = ActivityNotificationForm.GeneralList;
            mchkePaper = ActivityNotificationForm.chkePaper.Checked ;
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

        public string  MinReward
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
        public Boolean  chkePaper
        {
            get
            {
                return mchkePaper;
            }
            set
            {
                mchkePaper = value;
            }
        }
    }
}