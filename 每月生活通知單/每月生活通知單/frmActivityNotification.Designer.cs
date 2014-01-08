namespace 每月生活通知單
{
    partial class frmActivityNotification
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cmbReceiver = new System.Windows.Forms.ComboBox();
            this.cmbReceiverAddress = new System.Windows.Forms.ComboBox();
            this.chkMeeting = new System.Windows.Forms.CheckedListBox();
            this.chkGeneral = new System.Windows.Forms.CheckedListBox();
            this.cmbReward = new System.Windows.Forms.ComboBox();
            this.directoryEntry1 = new System.DirectoryServices.DirectoryEntry();
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.dateEnd = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.dateStart = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.groupPanel2 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.chkGeneralSelectAll = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.chkMeetingSelectAll = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.groupPanel3 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.groupPanel4 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.labelX8 = new DevComponents.DotNetBar.LabelX();
            this.labelX7 = new DevComponents.DotNetBar.LabelX();
            this.txtMinRewardCount = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.btnReport = new DevComponents.DotNetBar.ButtonX();
            this.chkePaper = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.groupPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateEnd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateStart)).BeginInit();
            this.groupPanel2.SuspendLayout();
            this.groupPanel3.SuspendLayout();
            this.groupPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbReceiver
            // 
            this.cmbReceiver.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbReceiver.FormattingEnabled = true;
            this.cmbReceiver.Items.AddRange(new object[] {
            "學生姓名",
            "監護人姓名",
            "父親姓名",
            "母親姓名"});
            this.cmbReceiver.Location = new System.Drawing.Point(88, 12);
            this.cmbReceiver.Margin = new System.Windows.Forms.Padding(4);
            this.cmbReceiver.Name = "cmbReceiver";
            this.cmbReceiver.Size = new System.Drawing.Size(142, 24);
            this.cmbReceiver.TabIndex = 6;
            this.cmbReceiver.Text = "學生姓名";
            // 
            // cmbReceiverAddress
            // 
            this.cmbReceiverAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbReceiverAddress.FormattingEnabled = true;
            this.cmbReceiverAddress.Items.AddRange(new object[] {
            "戶籍地址",
            "連絡地址"});
            this.cmbReceiverAddress.Location = new System.Drawing.Point(88, 51);
            this.cmbReceiverAddress.Margin = new System.Windows.Forms.Padding(4);
            this.cmbReceiverAddress.Name = "cmbReceiverAddress";
            this.cmbReceiverAddress.Size = new System.Drawing.Size(142, 24);
            this.cmbReceiverAddress.TabIndex = 7;
            this.cmbReceiverAddress.Text = "戶籍地址";
            // 
            // chkMeeting
            // 
            this.chkMeeting.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkMeeting.FormattingEnabled = true;
            this.chkMeeting.Items.AddRange(new object[] {
            "曠課",
            "事假",
            "病假",
            "喪假",
            "公假"});
            this.chkMeeting.Location = new System.Drawing.Point(14, 40);
            this.chkMeeting.Margin = new System.Windows.Forms.Padding(4);
            this.chkMeeting.Name = "chkMeeting";
            this.chkMeeting.Size = new System.Drawing.Size(124, 140);
            this.chkMeeting.TabIndex = 9;
            this.chkMeeting.Visible = false;
            // 
            // chkGeneral
            // 
            this.chkGeneral.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkGeneral.FormattingEnabled = true;
            this.chkGeneral.Items.AddRange(new object[] {
            "曠課",
            "事假",
            "病假",
            "喪假",
            "公假"});
            this.chkGeneral.Location = new System.Drawing.Point(15, 23);
            this.chkGeneral.Margin = new System.Windows.Forms.Padding(4);
            this.chkGeneral.Name = "chkGeneral";
            this.chkGeneral.Size = new System.Drawing.Size(122, 157);
            this.chkGeneral.TabIndex = 10;
            // 
            // cmbReward
            // 
            this.cmbReward.FormattingEnabled = true;
            this.cmbReward.Items.AddRange(new object[] {
            "大功",
            "小功",
            "嘉獎",
            "大過",
            "小過",
            "警告"});
            this.cmbReward.Location = new System.Drawing.Point(88, 8);
            this.cmbReward.Margin = new System.Windows.Forms.Padding(4);
            this.cmbReward.Name = "cmbReward";
            this.cmbReward.Size = new System.Drawing.Size(104, 24);
            this.cmbReward.TabIndex = 0;
            this.cmbReward.Text = "警告";
            // 
            // groupPanel1
            // 
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel1.Controls.Add(this.labelX2);
            this.groupPanel1.Controls.Add(this.dateEnd);
            this.groupPanel1.Controls.Add(this.labelX1);
            this.groupPanel1.Controls.Add(this.dateStart);
            this.groupPanel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupPanel1.Location = new System.Drawing.Point(4, 16);
            this.groupPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(279, 110);
            // 
            // 
            // 
            this.groupPanel1.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel1.Style.BackColorGradientAngle = 90;
            this.groupPanel1.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel1.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderBottomWidth = 1;
            this.groupPanel1.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel1.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderLeftWidth = 1;
            this.groupPanel1.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderRightWidth = 1;
            this.groupPanel1.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderTopWidth = 1;
            this.groupPanel1.Style.CornerDiameter = 4;
            this.groupPanel1.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel1.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.groupPanel1.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel1.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            this.groupPanel1.TabIndex = 15;
            this.groupPanel1.Text = "列印區間";
            // 
            // labelX2
            // 
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            this.labelX2.Location = new System.Drawing.Point(16, 5);
            this.labelX2.Margin = new System.Windows.Forms.Padding(4);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(64, 31);
            this.labelX2.TabIndex = 9;
            this.labelX2.Text = "開始日期";
            // 
            // dateEnd
            // 
            // 
            // 
            // 
            this.dateEnd.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dateEnd.ButtonDropDown.Visible = true;
            this.dateEnd.Format = DevComponents.Editors.eDateTimePickerFormat.Long;
            this.dateEnd.Location = new System.Drawing.Point(88, 44);
            this.dateEnd.Margin = new System.Windows.Forms.Padding(4);
            // 
            // 
            // 
            this.dateEnd.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            this.dateEnd.MonthCalendar.ClearButtonVisible = true;
            // 
            // 
            // 
            this.dateEnd.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.dateEnd.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
            this.dateEnd.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.dateEnd.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.dateEnd.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.dateEnd.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
            this.dateEnd.MonthCalendar.DaySize = new System.Drawing.Size(30, 15);
            this.dateEnd.MonthCalendar.DisplayMonth = new System.DateTime(2009, 11, 6, 0, 0, 0, 0);
            this.dateEnd.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.dateEnd.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dateEnd.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.dateEnd.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
            this.dateEnd.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.dateEnd.MonthCalendar.TodayButtonVisible = true;
            this.dateEnd.Name = "dateEnd";
            this.dateEnd.Size = new System.Drawing.Size(174, 22);
            this.dateEnd.TabIndex = 6;
            this.dateEnd.Value = new System.DateTime(2009, 11, 6, 0, 0, 0, 0);
            // 
            // labelX1
            // 
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            this.labelX1.Location = new System.Drawing.Point(16, 40);
            this.labelX1.Margin = new System.Windows.Forms.Padding(4);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(64, 31);
            this.labelX1.TabIndex = 8;
            this.labelX1.Text = "結束日期";
            // 
            // dateStart
            // 
            // 
            // 
            // 
            this.dateStart.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dateStart.ButtonDropDown.Visible = true;
            this.dateStart.Format = DevComponents.Editors.eDateTimePickerFormat.Long;
            this.dateStart.Location = new System.Drawing.Point(88, 11);
            this.dateStart.Margin = new System.Windows.Forms.Padding(4);
            // 
            // 
            // 
            this.dateStart.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            this.dateStart.MonthCalendar.ClearButtonVisible = true;
            // 
            // 
            // 
            this.dateStart.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.dateStart.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
            this.dateStart.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.dateStart.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.dateStart.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.dateStart.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
            this.dateStart.MonthCalendar.DaySize = new System.Drawing.Size(30, 15);
            this.dateStart.MonthCalendar.DisplayMonth = new System.DateTime(2009, 11, 6, 0, 0, 0, 0);
            this.dateStart.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.dateStart.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dateStart.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.dateStart.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
            this.dateStart.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.dateStart.MonthCalendar.TodayButtonVisible = true;
            this.dateStart.Name = "dateStart";
            this.dateStart.Size = new System.Drawing.Size(174, 22);
            this.dateStart.TabIndex = 5;
            this.dateStart.Value = new System.DateTime(2009, 11, 6, 0, 0, 0, 0);
            // 
            // groupPanel2
            // 
            this.groupPanel2.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel2.Controls.Add(this.chkGeneralSelectAll);
            this.groupPanel2.Controls.Add(this.chkGeneral);
            this.groupPanel2.Controls.Add(this.chkMeetingSelectAll);
            this.groupPanel2.Controls.Add(this.chkMeeting);
            this.groupPanel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupPanel2.Location = new System.Drawing.Point(304, 13);
            this.groupPanel2.Margin = new System.Windows.Forms.Padding(4);
            this.groupPanel2.Name = "groupPanel2";
            this.groupPanel2.Size = new System.Drawing.Size(160, 252);
            // 
            // 
            // 
            this.groupPanel2.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel2.Style.BackColorGradientAngle = 90;
            this.groupPanel2.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel2.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderBottomWidth = 1;
            this.groupPanel2.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel2.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderLeftWidth = 1;
            this.groupPanel2.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderRightWidth = 1;
            this.groupPanel2.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderTopWidth = 1;
            this.groupPanel2.Style.CornerDiameter = 4;
            this.groupPanel2.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel2.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.groupPanel2.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel2.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            this.groupPanel2.TabIndex = 16;
            this.groupPanel2.Text = "假別設定";
            // 
            // chkGeneralSelectAll
            // 
            this.chkGeneralSelectAll.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.chkGeneralSelectAll.BackColor = System.Drawing.Color.Transparent;
            this.chkGeneralSelectAll.Location = new System.Drawing.Point(14, 191);
            this.chkGeneralSelectAll.Margin = new System.Windows.Forms.Padding(4);
            this.chkGeneralSelectAll.Name = "chkGeneralSelectAll";
            this.chkGeneralSelectAll.Size = new System.Drawing.Size(64, 22);
            this.chkGeneralSelectAll.TabIndex = 18;
            this.chkGeneralSelectAll.Text = "全選";
            this.chkGeneralSelectAll.CheckedChanged += new System.EventHandler(this.chkGeneralSelectAll_CheckedChanged);
            // 
            // chkMeetingSelectAll
            // 
            this.chkMeetingSelectAll.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.chkMeetingSelectAll.BackColor = System.Drawing.Color.Transparent;
            this.chkMeetingSelectAll.Location = new System.Drawing.Point(16, 188);
            this.chkMeetingSelectAll.Margin = new System.Windows.Forms.Padding(4);
            this.chkMeetingSelectAll.Name = "chkMeetingSelectAll";
            this.chkMeetingSelectAll.Size = new System.Drawing.Size(64, 22);
            this.chkMeetingSelectAll.TabIndex = 0;
            this.chkMeetingSelectAll.Text = "全選";
            this.chkMeetingSelectAll.Visible = false;
            this.chkMeetingSelectAll.CheckedChanged += new System.EventHandler(this.chkMeetingSelectAll_CheckedChanged);
            // 
            // groupPanel3
            // 
            this.groupPanel3.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel3.Controls.Add(this.cmbReceiverAddress);
            this.groupPanel3.Controls.Add(this.labelX6);
            this.groupPanel3.Controls.Add(this.labelX5);
            this.groupPanel3.Controls.Add(this.cmbReceiver);
            this.groupPanel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupPanel3.Location = new System.Drawing.Point(4, 146);
            this.groupPanel3.Margin = new System.Windows.Forms.Padding(4);
            this.groupPanel3.Name = "groupPanel3";
            this.groupPanel3.Size = new System.Drawing.Size(279, 109);
            // 
            // 
            // 
            this.groupPanel3.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel3.Style.BackColorGradientAngle = 90;
            this.groupPanel3.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel3.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel3.Style.BorderBottomWidth = 1;
            this.groupPanel3.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel3.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel3.Style.BorderLeftWidth = 1;
            this.groupPanel3.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel3.Style.BorderRightWidth = 1;
            this.groupPanel3.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel3.Style.BorderTopWidth = 1;
            this.groupPanel3.Style.CornerDiameter = 4;
            this.groupPanel3.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel3.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.groupPanel3.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel3.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            this.groupPanel3.TabIndex = 17;
            this.groupPanel3.Text = "收件人資訊";
            // 
            // labelX6
            // 
            this.labelX6.BackColor = System.Drawing.Color.Transparent;
            this.labelX6.Location = new System.Drawing.Point(10, 47);
            this.labelX6.Margin = new System.Windows.Forms.Padding(4);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(70, 31);
            this.labelX6.TabIndex = 13;
            this.labelX6.Text = "收件地址";
            // 
            // labelX5
            // 
            this.labelX5.BackColor = System.Drawing.Color.Transparent;
            this.labelX5.Location = new System.Drawing.Point(10, 8);
            this.labelX5.Margin = new System.Windows.Forms.Padding(4);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(57, 31);
            this.labelX5.TabIndex = 12;
            this.labelX5.Text = "收件人";
            // 
            // groupPanel4
            // 
            this.groupPanel4.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel4.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel4.Controls.Add(this.labelX8);
            this.groupPanel4.Controls.Add(this.labelX7);
            this.groupPanel4.Controls.Add(this.txtMinRewardCount);
            this.groupPanel4.Controls.Add(this.cmbReward);
            this.groupPanel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupPanel4.Location = new System.Drawing.Point(4, 263);
            this.groupPanel4.Margin = new System.Windows.Forms.Padding(4);
            this.groupPanel4.Name = "groupPanel4";
            this.groupPanel4.Size = new System.Drawing.Size(279, 99);
            // 
            // 
            // 
            this.groupPanel4.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel4.Style.BackColorGradientAngle = 90;
            this.groupPanel4.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel4.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel4.Style.BorderBottomWidth = 1;
            this.groupPanel4.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel4.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel4.Style.BorderLeftWidth = 1;
            this.groupPanel4.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel4.Style.BorderRightWidth = 1;
            this.groupPanel4.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel4.Style.BorderTopWidth = 1;
            this.groupPanel4.Style.CornerDiameter = 4;
            this.groupPanel4.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel4.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.groupPanel4.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel4.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            this.groupPanel4.TabIndex = 18;
            this.groupPanel4.Text = "獎懲設定";
            // 
            // labelX8
            // 
            this.labelX8.BackColor = System.Drawing.Color.Transparent;
            this.labelX8.Location = new System.Drawing.Point(4, 31);
            this.labelX8.Margin = new System.Windows.Forms.Padding(4);
            this.labelX8.Name = "labelX8";
            this.labelX8.Size = new System.Drawing.Size(59, 31);
            this.labelX8.TabIndex = 13;
            this.labelX8.Text = "數量";
            // 
            // labelX7
            // 
            this.labelX7.BackColor = System.Drawing.Color.Transparent;
            this.labelX7.Location = new System.Drawing.Point(4, 4);
            this.labelX7.Margin = new System.Windows.Forms.Padding(4);
            this.labelX7.Name = "labelX7";
            this.labelX7.Size = new System.Drawing.Size(76, 31);
            this.labelX7.TabIndex = 12;
            this.labelX7.Text = "最低獎懲";
            // 
            // txtMinRewardCount
            // 
            // 
            // 
            // 
            this.txtMinRewardCount.Border.Class = "TextBoxBorder";
            this.txtMinRewardCount.Location = new System.Drawing.Point(88, 40);
            this.txtMinRewardCount.Margin = new System.Windows.Forms.Padding(4);
            this.txtMinRewardCount.Name = "txtMinRewardCount";
            this.txtMinRewardCount.Size = new System.Drawing.Size(104, 22);
            this.txtMinRewardCount.TabIndex = 1;
            // 
            // btnReport
            // 
            this.btnReport.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnReport.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReport.Location = new System.Drawing.Point(304, 312);
            this.btnReport.Margin = new System.Windows.Forms.Padding(4);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(115, 35);
            this.btnReport.TabIndex = 19;
            this.btnReport.Text = "產生報表";
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // chkePaper
            // 
            this.chkePaper.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkePaper.Location = new System.Drawing.Point(304, 273);
            this.chkePaper.Margin = new System.Windows.Forms.Padding(4);
            this.chkePaper.Name = "chkePaper";
            this.chkePaper.Size = new System.Drawing.Size(115, 35);
            this.chkePaper.TabIndex = 20;
            this.chkePaper.Text = "電子報表";
            // 
            // frmActivityNotification
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightBlue;
            this.ClientSize = new System.Drawing.Size(489, 391);
            this.Controls.Add(this.chkePaper);
            this.Controls.Add(this.btnReport);
            this.Controls.Add(this.groupPanel4);
            this.Controls.Add(this.groupPanel3);
            this.Controls.Add(this.groupPanel2);
            this.Controls.Add(this.groupPanel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmActivityNotification";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "每月生活通知單";
            this.Load += new System.EventHandler(this.frmActivityNotification_Load);
            this.groupPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dateEnd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateStart)).EndInit();
            this.groupPanel2.ResumeLayout(false);
            this.groupPanel3.ResumeLayout(false);
            this.groupPanel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbReceiver;
        private System.Windows.Forms.ComboBox cmbReceiverAddress;
        private System.Windows.Forms.CheckedListBox chkMeeting;
        private System.Windows.Forms.CheckedListBox chkGeneral;
        private System.Windows.Forms.ComboBox cmbReward;
        private System.DirectoryServices.DirectoryEntry directoryEntry1;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.Editors.DateTimeAdv.DateTimeInput dateEnd;
        private DevComponents.DotNetBar.LabelX labelX1;
        public DevComponents.Editors.DateTimeAdv.DateTimeInput dateStart;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkMeetingSelectAll;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.LabelX labelX6;
        private DevComponents.DotNetBar.LabelX labelX8;
        private DevComponents.DotNetBar.LabelX labelX7;
        private DevComponents.DotNetBar.Controls.TextBoxX txtMinRewardCount;
        public DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        public DevComponents.DotNetBar.Controls.GroupPanel groupPanel2;
        public DevComponents.DotNetBar.Controls.GroupPanel groupPanel3;
        public DevComponents.DotNetBar.Controls.GroupPanel groupPanel4;
        public DevComponents.DotNetBar.ButtonX btnReport;
        public DevComponents.DotNetBar.Controls.CheckBoxX chkePaper;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkGeneralSelectAll;
    }
}