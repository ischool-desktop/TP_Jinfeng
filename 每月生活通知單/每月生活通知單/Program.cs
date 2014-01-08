using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.Customization.PlugIn;

namespace 每月生活通知單
{
        //需宣告plugin主要方法為[MainMethod]
    public static class Program
    {

        [MainMethod]
        public static void Main()
        {
            //呼叫RegionChange物件來實際執行plugin程式
            new ActivityNotification();
        }
    }
}
