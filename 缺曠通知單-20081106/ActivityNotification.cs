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
using System.IO;
using System.Windows.Forms;

namespace 缺曠通知單
{
    public class ActivityNotification : ISchoolDocument
    {
        private ButtonAdapter classButton;
        private ButtonAdapter studentButton;
        private Aspose.Words.Document mdoc;
        private ActivityNotificationConfig mconfig;
        private ActivityNotificationPreference mPreference;
        private bool IsClassPlugin;

        public ActivityNotification()
        {

            //新增classButton
            classButton = new ButtonAdapter();
            classButton.Text = "缺曠通知單";                                       //ischool plugin名稱
            classButton.Path = "自訂報表";                                                        //ischool plugin路徑
            classButton.OnClick += new EventHandler(classButton_OnClick); //實際執行ischool plugin方法

            ClassReport.AddReport(classButton);

            //新增studentButton
            studentButton = new ButtonAdapter();
            studentButton.Text = "缺曠通知單";                                       //ischool plugin名稱
            studentButton.Path = "自訂報表";                                                        //ischool plugin路徑
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
            AccessHelper helper = new AccessHelper(); //helper物件用來取得ischool相關資料。

            mdoc = new Document(); //產生新的文件，用來合併每位學生的每月生活通知單
            mdoc.Sections.Clear(); //先將新文件的節做清除

            //根據使用者的選項來建立樣版，主要在於使用者選取缺曠的一般及集會項目差別而產生不同的樣版。
            ActivityNotificationTemplate template;

            List<string> vMeetingList = mconfig.Preference.UseAdvanceCondition ? mconfig.Preference.Conditions.MeetingList : mconfig.MeetingList;
            List<string> vGeneralList = mconfig.Preference.UseAdvanceCondition ? mconfig.Preference.Conditions.GeneralList : mconfig.GeneralList;

            if (mPreference.UseDefaultTemplate)
                template = new ActivityNotificationTemplate(new MemoryStream(Properties.Resources.缺曠通知單), vMeetingList, vGeneralList);
            else
                template = new ActivityNotificationTemplate(mPreference.CustomizeTemplate, vMeetingList, vGeneralList);

            template.ProcessDocument();

            if (IsClassPlugin)
            {

                //取得使用者所選取的班級。
                List<ClassRecord> classes = helper.ClassHelper.GetSelectedClass();

                //循訪班級記錄
                foreach (ClassRecord crecord in classes)
                {
                    //將班級學生填入獎懲記錄
                    helper.StudentHelper.FillReward(crecord.Students);

                    //將班級學生填入缺曠記錄
                    helper.StudentHelper.FillAttendance(crecord.Students);

                    //將班級學生填入連絡資訊
                    helper.StudentHelper.FillContactInfo(crecord.Students);

                    //將班級學生填入家長資訊
                    helper.StudentHelper.FillParentInfo(crecord.Students);

                    //循訪每位學生記錄，並建立ActivityNotificationDocument物件來產生報表
                    foreach (StudentRecord student in crecord.Students)
                    {
                        ISchoolDocument studentdoc = new AttendanceDocument(student, mconfig, template);

                        //將單一學生的每月生活通知單加入到主要的報表當中
                        if (studentdoc.ProcessDocument()==0)
                            mdoc.Sections.Add(mdoc.ImportNode(studentdoc.Document.Sections[0], true));
                    }
                }
            }
            else
            {
                //取得使用者所選取的班級。
                List<StudentRecord> students = helper.StudentHelper.GetSelectedStudent();

                    helper.StudentHelper.FillReward(students);

                    //將班級學生填入缺曠記錄
                    helper.StudentHelper.FillAttendance(students);

                    //將班級學生填入連絡資訊
                    helper.StudentHelper.FillContactInfo(students);

                    //將班級學生填入家長資訊
                    helper.StudentHelper.FillParentInfo(students);

                    //循訪每位學生記錄，並建立ActivityNotificationDocument物件來產生報表
                    foreach (StudentRecord student in students)
                    {
                        ISchoolDocument studentdoc = new AttendanceDocument(student, mconfig, template);

                        //將單一學生的每月生活通知單加入到主要的報表當中
                        if (studentdoc.ProcessDocument()==0)
                            mdoc.Sections.Add(mdoc.ImportNode(studentdoc.Document.Sections[0], true));
                    }
            }

            return 0;
        }

        //主要執行外掛的事件
        void studentButton_OnClick(object sender, EventArgs e)
        {
            IsClassPlugin = false;


            AbsenceNotificationSelectDateRangeForm DateRangeForm = new AbsenceNotificationSelectDateRangeForm();

            if (DateRangeForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                mPreference = ActivityNotificationPreference.GetInstance();
                mconfig = new ActivityNotificationConfig(mPreference);
                ProcessDocument();
                try
                {
                    #region 儲存並開啟檔案

                    string reportName = "缺曠通知單";
                    string path = Path.Combine(Application.StartupPath, "Reports");
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);
                    path = Path.Combine(path, reportName + ".doc");

                    if (File.Exists(path))
                    {
                        int i = 1;
                        while (true)
                        {
                            string newPath = Path.GetDirectoryName(path) + "\\" + Path.GetFileNameWithoutExtension(path) + (i++) + Path.GetExtension(path);
                            if (!File.Exists(newPath))
                            {
                                path = newPath;
                                break;
                            }
                        }
                    }

                    try
                    {
                        Document.Save(path, SaveFormat.Doc);
                        System.Diagnostics.Process.Start(path);
                    }
                    catch
                    {
                        SaveFileDialog sd = new SaveFileDialog();
                        sd.Title = "另存新檔";
                        sd.FileName = reportName + ".doc";
                        sd.Filter = "Word檔案 (*.doc)|*.doc|所有檔案 (*.*)|*.*";
                        if (sd.ShowDialog() == DialogResult.OK)
                        {
                            try
                            {
                                Document.Save(sd.FileName, SaveFormat.AsposePdf);
                            }
                            catch
                            {

                                MessageBox.Show("指定路徑無法存取。", "建立檔案失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }
                    #endregion
                }
                catch
                {
                    System.Windows.Forms.MessageBox.Show("指定路徑無法存取。", "建立檔案失敗", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
            }
        }

        //主要執行外掛的事件
        void classButton_OnClick(object sender, EventArgs e)
        {
            IsClassPlugin = true;

            AbsenceNotificationSelectDateRangeForm DateRangeForm = new AbsenceNotificationSelectDateRangeForm();

            //AbsenceNotificationConditionForm ConditionForm = new AbsenceNotificationConditionForm();
            //ConditionForm.ShowDialog();

            if (DateRangeForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                mPreference=ActivityNotificationPreference.GetInstance();
                mconfig = new ActivityNotificationConfig(mPreference);
                ProcessDocument();
                try
                {
                    #region 儲存並開啟檔案

                    string reportName = "缺曠通知單";
                    string path = Path.Combine(Application.StartupPath, "Reports");
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);
                    path = Path.Combine(path, reportName + ".doc");

                    if (File.Exists(path))
                    {
                        int i = 1;
                        while (true)
                        {
                            string newPath = Path.GetDirectoryName(path) + "\\" + Path.GetFileNameWithoutExtension(path) + (i++) + Path.GetExtension(path);
                            if (!File.Exists(newPath))
                            {
                                path = newPath;
                                break;
                            }
                        }
                    }

                    try
                    {
                        Document.Save(path, SaveFormat.Doc);
                        System.Diagnostics.Process.Start(path);
                    }
                    catch
                    {
                        SaveFileDialog sd = new SaveFileDialog();
                        sd.Title = "另存新檔";
                        sd.FileName = reportName + ".doc";
                        sd.Filter = "Word檔案 (*.doc)|*.doc|所有檔案 (*.*)|*.*";
                        if (sd.ShowDialog() == DialogResult.OK)
                        {
                            try
                            {
                                Document.Save(sd.FileName, SaveFormat.AsposePdf);
                            }
                            catch
                            {

                                MessageBox.Show("指定路徑無法存取。", "建立檔案失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }
                    #endregion
                }
                catch
                {
                    System.Windows.Forms.MessageBox.Show("指定路徑無法存取。", "建立檔案失敗", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
            }
             }

    }
}