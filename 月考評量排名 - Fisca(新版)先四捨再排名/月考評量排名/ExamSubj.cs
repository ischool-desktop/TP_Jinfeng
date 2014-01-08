using System;
using System.Collections.Generic;
using System.Text;

namespace 月考評量排名
{
    class ExamSubj
    {
        private string _ExamName;
        private string _SubjName;
        private string _SubjLevel;

        public string ExamName
        { get { return _ExamName; } set { _ExamName = value; } }

        public string SubjName
        { get { return _SubjName; } set { _SubjName = value; } }

        public string SubjLevel
        { get { return _SubjLevel; } set { _SubjLevel = value; } }

    }
}
