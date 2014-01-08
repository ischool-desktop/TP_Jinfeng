using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA;
using FISCA.Presentation;

namespace AgeWarningList
{
    public class Program
    {
        [MainMethod()]
        static public void Main()
        {
            
            K12.Presentation.NLDPanels.Class.RibbonBarItems["資料統計"]["報表"]["世界客製"]["一級轉二級預警名單"].Enable =true;
            K12.Presentation.NLDPanels.Class.RibbonBarItems["資料統計"]["報表"]["世界客製"]["一級轉二級預警名單"].Click += delegate
            {
                轉級預警名單 a = new 轉級預警名單();
                a.ShowDialog();
            };
           

        }
       
    }
}
