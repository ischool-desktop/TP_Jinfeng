using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SemScorePercent
{
    class SemsScore
    {        
        private int _SchoolYear;
        private int _Semester;
        private string _SubjectName;
        private string _SubjectLevel;       
        private decimal _Score = decimal.MinValue;        
        private int _ClassScoreRank;       

       
        public int SchoolYear
        { get { return _SchoolYear; } set { _SchoolYear = value; } }

        public int Semester
        { get { return _Semester; } set { _Semester = value; } }

        public string SubjectName
        { get { return _SubjectName; } set { _SubjectName = value; } }

        public string SubjectLevel
        { get { return _SubjectLevel; } set { _SubjectLevel = value; } }       

        public decimal Score
        { get { return _Score; } set { _Score = value; } }
       

        public int ClassScoreRank
        { get { return _ClassScoreRank; } set { _ClassScoreRank = value; } }


        
    }
}
