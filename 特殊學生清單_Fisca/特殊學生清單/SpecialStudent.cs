using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.Customization.PlugIn.Report;
using SmartSchool.Customization.PlugIn;


namespace 特殊學生清單
{
    public class SpecicalStudent
    {
        private ButtonAdapter classButton;
        
        public SpecicalStudent()
        {

            //新增ischool plugin
            classButton = new ButtonAdapter();
            classButton.Text = "德行特殊學生清單";                                       //ischool plugin名稱
            classButton.Path = "高苑報表";                                                        //ischool plugin路徑
            classButton.OnClick += new EventHandler(classButton_OnClick); //實際執行ischool plugin方法

            ClassReport.AddReport(classButton);            

        }
        
        //主要執行外掛的事件
        void classButton_OnClick(object sender, EventArgs e)
        {
            
            frmSpecialStudent SpecialStudentForm = new frmSpecialStudent();
            SpecialStudentForm.ShowDialog();
        }
        

    }
}
