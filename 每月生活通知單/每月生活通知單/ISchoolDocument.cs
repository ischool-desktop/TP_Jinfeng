using System;
using System.Collections.Generic;
using System.Text;

namespace 每月生活通知單
{
    interface ISchoolDocument
    {

        int  ProcessDocument();

        object ExtraInfo(string value);

        Aspose.Words.Document Document
        {
            get;
        }

    }
}