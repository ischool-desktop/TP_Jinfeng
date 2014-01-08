using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.Customization.Data;
using SmartSchool.Customization.PlugIn.Report;
using SmartSchool.Customization.PlugIn;
using SmartSchool.Customization.Data.StudentExtension;
using Aspose.Words;
using Aspose.Words.Drawing;
using System.Drawing;

namespace 每月生活通知單
{
    public class ActivityNotification:ISchoolDocument
    {
        private ButtonAdapter classButton;
        private ButtonAdapter studentButton;
        private Aspose.Words.Document mdoc;
        private Aspose.Words.Document classdoc;
        private ActivityNotificationConfig mconfig;
        private Boolean ResourceKind;
        //private SmartSchool.ePaper.ElectronicPaper paperForClass;
        //private SmartSchool.ePaper.ElectronicPaper paperForStudent;
        public ActivityNotification()
        {

            //新增ischool plugin
            classButton = new ButtonAdapter();
            classButton.Text = "每月生活通知單";                                       //ischool plugin名稱
            classButton.Path = "高苑報表";                                                        //ischool plugin路徑
            classButton.OnClick += new EventHandler(classButton_OnClick); //實際執行ischool plugin方法

            ClassReport.AddReport(classButton);
            //新增ischool plugin
            studentButton = new ButtonAdapter();
            studentButton.Text = "每月生活通知單";                                       //ischool plugin名稱
            studentButton.Path = "高苑報表";                                                        //ischool plugin路徑
            studentButton.OnClick += new EventHandler(studentButton_OnClick); //實際執行ischool plugin方法

            StudentReport.AddReport(studentButton);

        }

        public object ExtraInfo(string value)
        {
            return null;
        }

        public Aspose.Words.Document Document
        {
            get
            {
                return mdoc;
            }
        }

        public int ProcessDocument()
        {
            AccessHelper helper = new AccessHelper();

            mdoc = new Document();
            mdoc.Sections.Clear();


            ActivityNotificationTemplate   template = new ActivityNotificationTemplate(mconfig.ApplicationPath + "\\Customize\\每月生活通知單.doc",mconfig.MeetingList,mconfig.GeneralList);
            template.ProcessDocument();

            if (ResourceKind)
            {
                List<ClassRecord> classes = helper.ClassHelper.GetSelectedClass();               
                
                //宣告paperForClass為電子報表物件
                // SmartSchool.ePaper.ElectronicPaper電子報表物件
                //if (mconfig.chkePaper)
                //{
                    SmartSchool.ePaper.ElectronicPaper paperForClass = new SmartSchool.ePaper.ElectronicPaper("每月生活通知單",
                    SmartSchool.Customization.Data.SystemInformation.SchoolYear.ToString(),
                    SmartSchool.Customization.Data.SystemInformation.Semester.ToString(),
                    SmartSchool.ePaper.ViewerType.Class);
                //}

                foreach (ClassRecord crecord in classes)
                {
                    //產生依班級之word文件 
                    if (mconfig.chkePaper)
                    {
                        classdoc = new Document();
                        classdoc.Sections.Clear();
                    }
                    helper.StudentHelper.FillReward(crecord.Students);
                    helper.StudentHelper.FillAttendance(crecord.Students);
                    helper.StudentHelper.FillContactInfo(crecord.Students);
                    helper.StudentHelper.FillParentInfo(crecord.Students);
                   
                    foreach (StudentRecord student in crecord.Students)
                    {
                        ISchoolDocument studentdoc = new ActivityNotificationDocument(student, mconfig, template);

                        studentdoc.ProcessDocument();

                        mdoc.Sections.Add(mdoc.ImportNode(studentdoc.Document.Sections[0], true));
                        //將班級學生文件收集
                        if (mconfig.chkePaper)
                           classdoc.Sections.Add(classdoc.ImportNode(studentdoc.Document.Sections[0], true));
                    }
                    //建立電子報表
                    if (mconfig.chkePaper)
                    {
                        System.IO.MemoryStream stream = new System.IO.MemoryStream();

                        classdoc.Document.Save(stream, SaveFormat.Doc);

                        paperForClass.Append(new SmartSchool.ePaper.PaperItem(SmartSchool.ePaper.PaperFormat.Office2003Doc, stream, crecord.ClassID));
                    }
                    
                }
                //發佈電子報表
                if (mconfig.chkePaper)                
                   SmartSchool.ePaper.DispatcherProvider.Dispatch(paperForClass);

                return 0;
            }
            else
            {
                List<StudentRecord> students = helper.StudentHelper.GetSelectedStudent();
                //宣告電子報表
                //if (mconfig.chkePaper)
                //{
                    SmartSchool.ePaper.ElectronicPaper paperForStudent = new SmartSchool.ePaper.ElectronicPaper("每月生活通知單",
                    SmartSchool.Customization.Data.SystemInformation.SchoolYear.ToString(),
                    SmartSchool.Customization.Data.SystemInformation.Semester.ToString(),
                    SmartSchool.ePaper.ViewerType.Student);
                //}
                    helper.StudentHelper.FillReward(students);
                
                helper.StudentHelper.FillAttendance(students);
                helper.StudentHelper.FillContactInfo(students);
                helper.StudentHelper.FillParentInfo(students);

                foreach (StudentRecord student in students)
                {
                    ISchoolDocument studentdoc = new ActivityNotificationDocument(student, mconfig, template);

                    studentdoc.ProcessDocument();

                    mdoc.Sections.Add(mdoc.ImportNode(studentdoc.Document.Sections[0], true));
                    //建立電子報表
                    if (mconfig.chkePaper)
                    {
                        System.IO.MemoryStream stream = new System.IO.MemoryStream();

                        studentdoc.Document.Save(stream, SaveFormat.Doc);

                        paperForStudent.Append(new SmartSchool.ePaper.PaperItem(SmartSchool.ePaper.PaperFormat.Office2003Doc, stream, student.StudentID));
                    }
                }
                //發佈電子報表
                if (mconfig.chkePaper)
                    SmartSchool.ePaper.DispatcherProvider.Dispatch(paperForStudent);

                return 0;
            }

        }

        //主要執行外掛的事件
        void classButton_OnClick(object sender, EventArgs e)
        {
            ResourceKind = true;
            frmActivityNotification ActivityNotificationForm = new frmActivityNotification();
            
            if (ActivityNotificationForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                mconfig = new ActivityNotificationConfig(ActivityNotificationForm);
                ProcessDocument();
                try
                {
                    Document.Save(mconfig.FileName);                   
                    System.Diagnostics.Process.Start(mconfig.FileName);
                }
                catch
                {
                    System.Windows.Forms.MessageBox.Show("指定路徑無法存取。", "建立檔案失敗", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
            }
        }
        void studentButton_OnClick(object sender, EventArgs e)
        {
            ResourceKind = false;
            frmActivityNotification ActivityNotificationForm = new frmActivityNotification();

            if (ActivityNotificationForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                mconfig = new ActivityNotificationConfig(ActivityNotificationForm);
                ProcessDocument();
                try
                {
                    Document.Save(mconfig.FileName);
                   
                    System.Diagnostics.Process.Start(mconfig.FileName);
                }
                catch
                {
                    System.Windows.Forms.MessageBox.Show("指定路徑無法存取。", "建立檔案失敗", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
            }
        }
    }
}