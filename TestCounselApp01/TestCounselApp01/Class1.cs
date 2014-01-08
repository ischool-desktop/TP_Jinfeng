using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using K12.Data;
// 輔導模組
using Counsel_System.DAO;

namespace TestCounselApp01
{
    public class Class1
    {
        public void Test()
        {
            // 取得所有學生
            List<string> StudentIDList = new List<string>();
            List<StudentRecord> allStudentList= Student.SelectAll();
            foreach (StudentRecord StudRec in allStudentList)
                StudentIDList.Add(StudRec.ID);

            UDTTransfer udtTransfer= new UDTTransfer ();
            // 取得學生晤談紀錄
            List<UDT_CounselStudentInterviewRecordDef> StudentInterviewRecordList = udtTransfer.GetCounselStudentInterviewRecordByStudentIDList(StudentIDList);

            foreach (UDT_CounselStudentInterviewRecordDef data in StudentInterviewRecordList)
            { 
                string StudentID=data.StudentID.ToString();
                
                // 解析參與人員 key:參與人員:學生,value=1，
                Dictionary<string,string> AttendeesDict= Counsel_System.Utility.GetConvertCounselXMLVal_Attendees(data.Attendees);

                // 解析輔導方式 key:輔導方式:暫時結案,value=1
                Dictionary<string, string> CounselTypeDict = Counsel_System.Utility.GetConvertCounselXMLVal_CounselType(data.CounselType);
                // 解析輔導歸類,key:輔導歸類:違規,value=1
                Dictionary<string, string> TypeKindDict = Counsel_System.Utility.GetConvertCounselXMLVal_CounselTypeKind(data.CounselTypeKind);                
            
            }
        
            // 取得個案會議紀錄
            List<UDT_CounselCaseMeetingRecordDef> CaseMeetingRecordList = udtTransfer.GetCaseMeetingRecordListByStudentIDList(StudentIDList);
            foreach (UDT_CounselCaseMeetingRecordDef data in CaseMeetingRecordList)
            {                
                string StudentID = data.StudentID.ToString();
                // 解析參與人員 key:參與人員:學生,value=1，
                Dictionary<string, string> AttendeesDict = Counsel_System.Utility.GetConvertCounselXMLVal_Attendees(data.Attendees);

                // 解析輔導方式 key:輔導方式:暫時結案,value=1
                Dictionary<string, string> CounselTypeDict = Counsel_System.Utility.GetConvertCounselXMLVal_CounselType(data.CounselType);
                // 解析輔導歸類,key:輔導歸類:違規,value=1
                Dictionary<string, string> TypeKindDict = Counsel_System.Utility.GetConvertCounselXMLVal_CounselTypeKind(data.CounselTypeKind);                
            
            }

        }
        
    }
}
