using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace 特殊學生清單
{
    public class SpecialStudentConfig
    {
        private int mReportType;
        private string mSortKey;
        private Boolean mSemester;
        private Boolean mMequialD;
        private List <string>  mAbsenceList=new List<string> ();
        private int mPeriod;
        private Boolean mDate;
        private DateTime? mStartDate;
        private DateTime? mEndDate;
        private int mthisSchoolYear;
        private int mthisSemester;

        public SpecialStudentConfig()
        {
           
            mReportType = 0;
            mSortKey = "StudentNumber";
            mSemester=false;
            mMequialD =false;
            mPeriod = 0;
            mDate = false;
            mStartDate = null;
            mEndDate = null;
            mthisSchoolYear = 0;
            mthisSemester = 0;
        }

        public SpecialStudentConfig(frmSpecialStudent SpecialStudentForm)
        {
            if (SpecialStudentForm.Fault_3B.Checked==true)
                mReportType = 1;
            if (SpecialStudentForm.Attendance_21.Checked==true)
                mReportType = 2;
            if (SpecialStudentForm.VEScore_60.Checked==true)
                mReportType = 3;
            if (SpecialStudentForm.Fault_3A.Checked==true)
                mReportType = 4;
            if (SpecialStudentForm.Attendance_Half.Checked==true)
                mReportType = 5;
            if (SpecialStudentForm.SpecialStudent.Checked == true)
                mReportType = 6;
            mthisSchoolYear = SpecialStudentForm.intSchoolYear.Value;
            mthisSemester = (SpecialStudentForm.cboSemester.Text == "上學期" ? 1 : 2);
            if (SpecialStudentForm.StudentNumber.Checked==true )
               mSortKey ="StudentNumber";
            else
                mSortKey="SeatNo";
            if ((mReportType == 1) || ( mReportType ==4))
            {
                mSemester = SpecialStudentForm.chkSemester.Checked;
                mMequialD =SpecialStudentForm.chkMequialD.Checked;
            }
            if (mReportType==5)
               {
                if (SpecialStudentForm.txtPeriod.Text!="")
                   mPeriod =Convert.ToInt16(SpecialStudentForm.txtPeriod.Text);
                foreach (ListViewItem var in SpecialStudentForm.AbsenceList.Items )
                {
                    if (var.Checked == true && var.Text!=null)
                    {
                        mAbsenceList.Add(var.Text);
                        
                    }
                }
               
            }
            if (SpecialStudentForm.chkDate.Checked)
            {
                mDate = true;
                if (SpecialStudentForm.dtStartDate.IsEmpty)
                    mStartDate = null;
                else
                   mStartDate = SpecialStudentForm.dtStartDate.Value;
               if (SpecialStudentForm.dtEndDate.IsEmpty)
                   mEndDate = null;
               else
                   mEndDate = SpecialStudentForm.dtEndDate.Value;
            }
            else
            {
                mDate = false;
                mStartDate = null;
                mEndDate = null;
            }
           

            
        }

        


        

        public int ReportType
        {
            get
            {
                return mReportType;
            }
            set
            {
                mReportType = value;
            }
        }

        public string SortKey
        {
            get
            {
                return mSortKey;
            }
            set
            {
                mSortKey = value;
            }
        }

        public Boolean  Semester
        {
            get
            {
                return mSemester;
            }
            set
            {
                mSemester = value;
            }
        }

        public Boolean MequialD
        {
            get
            {
                return mMequialD;
            }
            set
            {
                mMequialD = value;
            }
        }
        public List<string> AbsenceList
        {
            get
            {
                return mAbsenceList;
            }
            set
            {
                mAbsenceList = value;
            }
        }
        public int Period
        {
            get
            {
                return mPeriod;
            }
            set
            {
                mPeriod = value;
            }
        }
        public Boolean Date
        {
            get
            {
                return mDate;
            }
            set
            {
                mDate = value;
            }
        }
        public DateTime? StartDate
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
        public DateTime? EndDate
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
        public int thisSchoolYear
        {
            get
            {
                return mthisSchoolYear;
            }
            set
            {
                mthisSchoolYear = value;
            }
        }
        public int thisSemester
        {
            get
            {
                return mthisSemester;
            }
            set
            {
                mthisSemester = value;
            }
        }
    }
}
