using System;
using System.Collections.Generic;
using System.Text;

namespace 月考評量排名
{
    class StudentData
    {
        private string _StudentID;      // StudentID
        private string _StudentNum;     // 學號
        private string _ClassName;      // 班級
        private string _ClassYear;      // 年級
        private string _SeatNo;         // 座號
        private string _Name;           // 姓名        
        private decimal _ClassSumRank;   // 班級總分排名
        private decimal _ClassSumRankA;  // 班級加權總分排名
        private decimal _ClassAvgRank;   // 班級平均排名
        private decimal _ClassAvgRankA;  // 班級加權平均排名
        private decimal _YearSumRank;    // 總分年排名
        private decimal _YearSumRankA;   // 加權總分年排名
        private decimal _YearAvgRank;    // 平均年排名
        private decimal _YearAvgRankA;   // 加權平均年排名
        private decimal _SumScore=decimal.MinValue ;      // 總分
        private decimal _AvgScore=decimal.MinValue ;      // 平均
        private decimal _SumScoreA=decimal.MinValue ;     // 加權總分
        private decimal _AvgScoreA=decimal.MinValue ;     // 加權平均
        private decimal _ClassSumRank1;   // 班級總分排名(前試別)
        private decimal _ClassSumRankA1;  // 班級加權總分排名(前試別)
        private decimal _ClassAvgRank1;   // 班級平均排名(前試別)
        private decimal _ClassAvgRankA1;  // 班級加權平均排名(前試別)
        private decimal _ClassAvgRankAD;  // 班級加權平均排名(進退步)
        private decimal _ClassAvgRankAS;  // 班級加權平均成績排名(進退步)
        private decimal _YearSumRank1;    // 總分年排名(前試別)
        private decimal _YearSumRankA1;   // 加權總分年排名(前試別)
        private decimal _YearAvgRank1;    // 平均年排名(前試別)
        private decimal _YearAvgRankA1;   // 加權平均年排名(前試別)
        private decimal _SumScore1 = decimal.MinValue;      // 總分(前試別)
        private decimal _AvgScore1 = decimal.MinValue;      // 平均(前試別)
        private decimal _SumScoreA1 = decimal.MinValue;     // 加權總分(前試別)
        private decimal _AvgScoreA1 = decimal.MinValue;     // 加權平均(前試別)

       

        public List<ExamData> lstStudExamScore = new List<ExamData>();
        public List<ExamData> lstStudExamScoreOld = new List<ExamData>();

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

        public decimal ClassSumRank
        { get { return _ClassSumRank; } set { _ClassSumRank = value; } }

        public decimal ClassSumRankA
        { get { return _ClassSumRankA; } set { _ClassSumRankA = value; } }

        public decimal ClassAvgRank
        { get { return _ClassAvgRank; } set { _ClassAvgRank = value; } }

        public decimal ClassAvgRankA
        { get { return _ClassAvgRankA; } set { _ClassAvgRankA = value; } }

        public decimal YearSumRank
        { get { return _YearSumRank; } set { _YearSumRank = value; } }

        public decimal YearSumRankA
        { get { return _YearSumRankA; } set { _YearSumRankA = value; } }

        public decimal YearAvgRank
        { get { return _YearAvgRank; } set { _YearAvgRank = value; } }

        public decimal YearAvgRankA
        { get { return _YearAvgRankA; } set { _YearAvgRankA = value; } }


        public decimal SumScore
        { get { return _SumScore; } set { _SumScore = value; } }

        public decimal SumScoreA
        { get { return _SumScoreA; } set { _SumScoreA = value; } }

        public decimal AvgScore
        { get { return _AvgScore; } set { _AvgScore = value; } }

        public decimal AvgScoreA
        { get { return _AvgScoreA; } set { _AvgScoreA = value; } }

        public decimal ClassSumRank1
        { get { return _ClassSumRank1; } set { _ClassSumRank1 = value; } }

        public decimal ClassSumRankA1
        { get { return _ClassSumRankA1; } set { _ClassSumRankA1 = value; } }

        public decimal ClassAvgRank1
        { get { return _ClassAvgRank1; } set { _ClassAvgRank1 = value; } }

        public decimal ClassAvgRankA1
        { get { return _ClassAvgRankA1; } set { _ClassAvgRankA1 = value; } }

        public decimal ClassAvgRankAD
        { get { return _ClassAvgRankAD; } set { _ClassAvgRankAD = value; } }

        public decimal ClassAvgRankAS
        { get { return _ClassAvgRankAS; } set { _ClassAvgRankAS = value; } }

        public decimal YearSumRank1
        { get { return _YearSumRank1; } set { _YearSumRank1 = value; } }

        public decimal YearSumRankA1
        { get { return _YearSumRankA1; } set { _YearSumRankA1 = value; } }

        public decimal YearAvgRank1
        { get { return _YearAvgRank1; } set { _YearAvgRank1 = value; } }

        public decimal YearAvgRankA1
        { get { return _YearAvgRankA1; } set { _YearAvgRankA1 = value; } }


        public decimal SumScore1
        { get { return _SumScore1; } set { _SumScore1 = value; } }

        public decimal SumScoreA1
        { get { return _SumScoreA1; } set { _SumScoreA1 = value; } }

        public decimal AvgScore1
        { get { return _AvgScore1; } set { _AvgScore1 = value; } }

        public decimal AvgScoreA1
        { get { return _AvgScoreA1; } set { _AvgScoreA1 = value; } }

 
    }
}
