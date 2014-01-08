using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FISCA.Permission;
using FISCA.Presentation;
using K12.Presentation;
using FISCA;
namespace SemScoreYearScoreByPassProcent
{
    public static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [FISCA.MainMethod]
        public static void Main() 
        {
            RibbonFeature Feature = new RibbonFeature("SHEvaluation.Report20130123","學分數未達標準名單");
            Catalog ClassCatalog = FISCA.Permission.RoleAclSource.Instance["班級"]["報表"];
            ClassCatalog.Add(Feature);

            K12.Presentation.NLDPanels.Class.RibbonBarItems["資料統計"]["報表"]["成績相關報表"]["學分數未達標準名單"].Enable = FISCA.Permission.UserAcl.Current["SHEvaluation.Report20130123"].Executable;
            K12.Presentation.NLDPanels.Class.RibbonBarItems["資料統計"]["報表"]["成績相關報表"]["學分數未達標準名單"].Click += (sender, e) =>
                {
                    Form1 f1 = new Form1();
                    f1.ShowDialog();
                };
        }      
   
    }
}
