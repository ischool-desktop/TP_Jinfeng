using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.Customization.PlugIn;

namespace 德行成績試算表
{
    public class Program
    {
        [MainMethod()]
        public static void Main()
        {

            new SemesterMoralScoreCalc();
        }
    }
}
