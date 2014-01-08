using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA.Permission;
using FISCA.Presentation;
using K12.Presentation;
using FISCA;
namespace TestCounselApp01
{
    public static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [FISCA.MainMethod]
        public static void Main()
        {
            RibbonFeature Feature = new RibbonFeature("Counsel_System.Report20130326", "個案_晤談記錄輔導歸類");
            Catalog StudentCatalog = FISCA.Permission.RoleAclSource.Instance["學生"]["輔導"]["報表"];
            StudentCatalog.Add(Feature);

            K12.Presentation.NLDPanels.Student.RibbonBarItems["輔導"]["報表"]["個案_晤談記錄輔導歸類"].Enable = FISCA.Permission.UserAcl.Current["Counsel_System.Report20130326"].Executable;
            K12.Presentation.NLDPanels.Student.RibbonBarItems["輔導"]["報表"]["個案_晤談記錄輔導歸類"].Click += (sender, e) =>
            {
                Form1 f1 = new Form1();
                f1.ShowDialog();
            };
        }      
   
    }
}
