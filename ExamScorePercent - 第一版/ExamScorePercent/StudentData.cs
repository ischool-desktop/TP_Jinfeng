using System;
using System.Collections.Generic;
using System.Text;

namespace ExamScorePercent
{
    class StudentData
    {
        private string _StudentID;      // StudentID
        private string _StudentNum;     // 學號
        private string _ClassName;      // 班級
        private string _ClassYear;      // 年級
        private string _SeatNo;         // 座號
        private string _Name;           // 姓名 
        private int _ClassNum;          // 班級人數
        private string _SubCategory;     //成績身分
        public List<ExamData> lstStudExamScore = new List<ExamData>();       
        public string StudentID
        { get { return _StudentID; } set { _StudentID = value; } }

        public string StudentNum
        { get { return _StudentNum; } set { _StudentNum = value; } }

        public string ClassName
        { get { return _ClassName; } set { _ClassName = value; } }

        public string ClassYear
        { get { return _ClassYear; } set { _ClassYear = value; } }

        public string SeatNo
        { get { return _SeatNo; } set { _SeatNo = value; } }

        public string Name
        { get { return _Name; } set { _Name = value; } }

        public int ClassNum
        { get { return _ClassNum; } set { _ClassNum = value; } }

        public string  SubCategory
        { get { return _SubCategory; } set { _SubCategory = value; } }
    }
}
