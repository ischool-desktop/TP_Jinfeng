using System;
using System.Collections.Generic;
using System.Text;

namespace ExamScorePercent
{
    class ExamData
    {
       
        private int _CourseID;
        private string _CourseName;
        private int _SchoolYear;
        private int _Semester;
        private string _SubjectName;
        private string _SubjectLevel;
        private string _ExamID;
        private string _ExamName;
        private decimal _Score=decimal.MinValue;
        
        private int _ClassScoreRank;        
        private bool _hasExamScore=false;
        
        public int CousreID
        { get { return _CourseID; } set { _CourseID = value; } }

        public string CourseName
        { get { return _CourseName; } set { _CourseName = value; } }

        public int SchoolYear
        { get { return _SchoolYear; } set { _SchoolYear = value; } }

        public int Semester
        { get { return _Semester; } set { _Semester = value; } }

        public string SubjectName
        { get { return _SubjectName; } set { _SubjectName = value; } }

        public string SubjectLevel
        { get { return _SubjectLevel; } set { _SubjectLevel = value; } }

        public string ExamID
        { get { return _ExamID; } set { _ExamID = value; } }

        public string ExamName
        { get { return _ExamName; } set { _ExamName = value; } }

        public decimal Score
        { get { return _Score; } set { _Score = value; } }

        

        public int ClassScoreRank
        { get { return _ClassScoreRank; } set { _ClassScoreRank = value; } }
       

        public bool hasExamScore
        { get { return _hasExamScore; } set { _hasExamScore = true; } }
    }
}
