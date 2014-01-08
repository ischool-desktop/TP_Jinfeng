using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using FISCA;

namespace SemScorePercent
{
    public static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [MainMethod()]
        public static void Main()
        {

            K12.Presentation.NLDPanels.Class.RibbonBarItems["資料統計"]["報表"]["世界客製"]["學期學業成績比率清單"].Enable = true;

            K12.Presentation.NLDPanels.Class.RibbonBarItems["資料統計"]["報表"]["世界客製"]["學期學業成績比率清單"].Click += delegate
            {
                學期學業成績比率清單 a = new 學期學業成績比率清單();
                a.ShowDialog();
            };
        }
    }
}
