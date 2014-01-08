using System;
using System.Collections.Generic;
using System.Text;

namespace 德行成績試算表
{
    public class UsefulPeriodAbsence
    {
        private string _Absence;
        private string _Period;
        private decimal _Subtract;
        private int _Aggregated;

        public UsefulPeriodAbsence(string absence, string period, decimal subtract, int aggregated)
        {
            _Absence = absence;
            _Period = period;
            _Subtract = subtract;
            _Aggregated = aggregated;
        }
        /// <summary>
        /// 假別
        /// </summary>
        public string Absence
        {
            get { return _Absence; }
        }
        /// <summary>
        /// 節次類別
        /// </summary>
        public string Period
        {
            get { return _Period; }
        }
        /// <summary>
        /// 扣分
        /// </summary>
        public decimal Subtract
        {
            get { return _Subtract; }
        }
        /// <summary>
        /// 累計次數
        /// </summary>
        public int Aggregated
        {
            get { return _Aggregated; }
        }
    }
}
