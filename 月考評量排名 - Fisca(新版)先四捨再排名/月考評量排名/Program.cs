using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FISCA.Permission;
using FISCA.Presentation;
using K12.Presentation;
using FISCA;
namespace 月考評量排名
{
    public static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [MainMethod ()]
        public static void Main() 
        {
            RibbonFeature Feature = new RibbonFeature("SHEvaluation.Report1011205", "月考評量優等");
            Catalog ClassCatalog = FISCA.Permission.RoleAclSource.Instance["班級"]["報表"];
            ClassCatalog.Add(Feature);

            K12.Presentation.NLDPanels.Class.RibbonBarItems["資料統計"]["報表"]["成績相關報表"]["月考評量優等"].Enable = FISCA.Permission.UserAcl.Current["SHEvaluation.Report1011205"].Executable;
            K12.Presentation.NLDPanels.Class.RibbonBarItems["資料統計"]["報表"]["成績相關報表"]["月考評量優等"].Click += (sender, e) =>
                {
                    frm1 f1 = new frm1();
                    f1.ShowDialog();
                };
        }

       
    }
}