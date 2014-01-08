using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.Customization.PlugIn;

namespace 缺曠通知單
{
    //需宣告plugin主要方法為[MainMethod]
    public static class Program
    {

        [MainMethod]
        public static void Main()
        {

            new ActivityNotification();
   
        }
    }
}