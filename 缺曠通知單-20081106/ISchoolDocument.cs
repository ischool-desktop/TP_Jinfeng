using System;
using System.Collections.Generic;
using System.Text;

namespace 缺曠通知單
{
    interface ISchoolDocument
    {

        int ProcessDocument();

        object ExtraInfo(string value);

        Aspose.Words.Document Document
        {
            get;
        }

    }
}