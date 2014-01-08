namespace 特殊學生清單
{
    partial class frmSpecialStudent
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該公開 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改這個方法的內容。
        ///
        /// </summary>
        private void InitializeComponent()
        {
            this.label3 = new System.Windows.Forms.Label();
            this.SeatNo = new System.Windows.Forms.RadioButton();
            this.StudentNumber = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.AbsenceList = new System.Windows.Forms.ListView();
            this.chkSelAll = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.Absencelbl = new System.Windows.Forms.Label();
            this.chkMequialD = new System.Windows.Forms.CheckBox();
            this.lblPeriod = new System.Windows.Forms.Label();
            this.txtPeriod = new System.Windows.Forms.TextBox();
            this.chkSemester = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.B4 = new System.Windows.Forms.RadioButton();
            this.A4 = new System.Windows.Forms.RadioButton();
            this.ReportType = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.SpecialStudent = new System.Windows.Forms.RadioButton();
            this.Fault_3B = new System.Windows.Forms.RadioButton();
            this.Attendance_Half = new System.Windows.Forms.RadioButton();
            this.Attendance_21 = new System.Windows.Forms.RadioButton();
            this.Fault_3A = new System.Windows.Forms.RadioButton();
            this.VEScore_60 = new System.Windows.Forms.RadioButton();
            this.SortKey = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.chkDate = new System.Windows.Forms.CheckBox();
            this.groupPanel3 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.dtStartDate = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.dtEndDate = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.groupPanel2 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.btnReport = new DevComponents.DotNetBar.ButtonX();
            this.cboSemester = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboItem1 = new DevComponents.Editors.ComboItem();
            this.comboItem2 = new DevComponents.Editors.ComboItem();
            this.intSchoolYear = new DevComponents.Editors.IntegerInput();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.ReportType.SuspendLayout();
            this.SortKey.SuspendLayout();
            this.groupPanel1.SuspendLayout();
            this.groupPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtStartDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtEndDate)).BeginInit();
            this.groupPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.intSchoolYear)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label3.Location = new System.Drawing.Point(12, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "步驟一";
            // 
            // SeatNo
            // 
            this.SeatNo.AutoSize = true;
            this.SeatNo.Location = new System.Drawing.Point(3, 30);
            this.SeatNo.Name = "SeatNo";
            this.SeatNo.Size = new System.Drawing.Size(58, 20);
            this.SeatNo.TabIndex = 1;
            this.SeatNo.TabStop = true;
            this.SeatNo.Text = "座號";
            this.SeatNo.UseVisualStyleBackColor = true;
            // 
            // StudentNumber
            // 
            this.StudentNumber.AutoSize = true;
            this.StudentNumber.Checked = true;
            this.StudentNumber.Location = new System.Drawing.Point(3, 4);
            this.StudentNumber.Name = "StudentNumber";
            this.StudentNumber.Size = new System.Drawing.Size(58, 20);
            this.StudentNumber.TabIndex = 0;
            this.StudentNumber.TabStop = true;
            this.StudentNumber.Text = "學號";
            this.StudentNumber.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label5.Location = new System.Drawing.Point(296, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 16);
            this.label5.TabIndex = 7;
            this.label5.Text = "步驟三";
            // 
            // AbsenceList
            // 
            this.AbsenceList.CheckBoxes = true;
            this.AbsenceList.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AbsenceList.Location = new System.Drawing.Point(23, 189);
            this.AbsenceList.Name = "AbsenceList";
            this.AbsenceList.Size = new System.Drawing.Size(212, 106);
            this.AbsenceList.TabIndex = 12;
            this.AbsenceList.UseCompatibleStateImageBehavior = false;
            this.AbsenceList.View = System.Windows.Forms.View.SmallIcon;
            // 
            // chkSelAll
            // 
            this.chkSelAll.BackColor = System.Drawing.Color.Transparent;
            this.chkSelAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.chkSelAll.Location = new System.Drawing.Point(23, 295);
            this.chkSelAll.Name = "chkSelAll";
            this.chkSelAll.Size = new System.Drawing.Size(97, 27);
            this.chkSelAll.TabIndex = 11;
            this.chkSelAll.Text = "選擇全部";
            this.chkSelAll.TextColor = System.Drawing.Color.Black;
            this.chkSelAll.CheckedChanged += new System.EventHandler(this.chkSelAll_CheckedChanged);
            // 
            // Absencelbl
            // 
            this.Absencelbl.AutoSize = true;
            this.Absencelbl.Location = new System.Drawing.Point(23, 170);
            this.Absencelbl.Name = "Absencelbl";
            this.Absencelbl.Size = new System.Drawing.Size(136, 16);
            this.Absencelbl.TabIndex = 7;
            this.Absencelbl.Text = "並選取計算之假別";
            // 
            // chkMequialD
            // 
            this.chkMequialD.AutoSize = true;
            this.chkMequialD.Location = new System.Drawing.Point(26, 29);
            this.chkMequialD.Name = "chkMequialD";
            this.chkMequialD.Size = new System.Drawing.Size(155, 20);
            this.chkMequialD.TabIndex = 5;
            this.chkMequialD.Text = "功過相抵方式計算";
            this.chkMequialD.UseVisualStyleBackColor = true;
            // 
            // lblPeriod
            // 
            this.lblPeriod.AutoSize = true;
            this.lblPeriod.Location = new System.Drawing.Point(23, 145);
            this.lblPeriod.Name = "lblPeriod";
            this.lblPeriod.Size = new System.Drawing.Size(88, 16);
            this.lblPeriod.TabIndex = 4;
            this.lblPeriod.Text = "請輸入節次";
            // 
            // txtPeriod
            // 
            this.txtPeriod.Location = new System.Drawing.Point(117, 142);
            this.txtPeriod.Name = "txtPeriod";
            this.txtPeriod.Size = new System.Drawing.Size(64, 27);
            this.txtPeriod.TabIndex = 3;
            // 
            // chkSemester
            // 
            this.chkSemester.AutoSize = true;
            this.chkSemester.Location = new System.Drawing.Point(26, 3);
            this.chkSemester.Name = "chkSemester";
            this.chkSemester.Size = new System.Drawing.Size(123, 20);
            this.chkSemester.TabIndex = 0;
            this.chkSemester.Text = "在學期間累計";
            this.chkSemester.UseVisualStyleBackColor = true;
            this.chkSemester.CheckedChanged += new System.EventHandler(this.chkSemester_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label4.Location = new System.Drawing.Point(12, 341);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 16);
            this.label4.TabIndex = 5;
            this.label4.Text = "步驟二";
            // 
            // B4
            // 
            this.B4.AutoSize = true;
            this.B4.Location = new System.Drawing.Point(85, 3);
            this.B4.Name = "B4";
            this.B4.Size = new System.Drawing.Size(42, 20);
            this.B4.TabIndex = 1;
            this.B4.TabStop = true;
            this.B4.Text = "B4";
            this.B4.UseVisualStyleBackColor = true;
            // 
            // A4
            // 
            this.A4.AutoSize = true;
            this.A4.Checked = true;
            this.A4.Location = new System.Drawing.Point(6, 3);
            this.A4.Name = "A4";
            this.A4.Size = new System.Drawing.Size(42, 20);
            this.A4.TabIndex = 0;
            this.A4.TabStop = true;
            this.A4.Text = "A4";
            this.A4.UseVisualStyleBackColor = true;
            // 
            // ReportType
            // 
            this.ReportType.BackColor = System.Drawing.Color.Transparent;
            this.ReportType.CanvasColor = System.Drawing.SystemColors.Control;
            this.ReportType.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.ReportType.Controls.Add(this.SpecialStudent);
            this.ReportType.Controls.Add(this.Fault_3B);
            this.ReportType.Controls.Add(this.Attendance_Half);
            this.ReportType.Controls.Add(this.Attendance_21);
            this.ReportType.Controls.Add(this.Fault_3A);
            this.ReportType.Controls.Add(this.VEScore_60);
            this.ReportType.Location = new System.Drawing.Point(15, 80);
            this.ReportType.Name = "ReportType";
            this.ReportType.Size = new System.Drawing.Size(254, 203);
            // 
            // 
            // 
            this.ReportType.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.ReportType.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.ReportType.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.ReportType.Style.BorderBottomWidth = 1;
            this.ReportType.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.ReportType.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.ReportType.Style.BorderLeftColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.ReportType.Style.BorderLeftWidth = 1;
            this.ReportType.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.ReportType.Style.BorderRightColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.ReportType.Style.BorderRightWidth = 1;
            this.ReportType.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.ReportType.Style.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.ReportType.Style.BorderTopWidth = 1;
            this.ReportType.Style.CornerDiameter = 4;
            this.ReportType.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.ReportType.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.ReportType.TabIndex = 12;
            this.ReportType.Text = "報表類別";
            // 
            // SpecialStudent
            // 
            this.SpecialStudent.AutoSize = true;
            this.SpecialStudent.Location = new System.Drawing.Point(3, 133);
            this.SpecialStudent.Name = "SpecialStudent";
            this.SpecialStudent.Size = new System.Drawing.Size(122, 20);
            this.SpecialStudent.TabIndex = 5;
            this.SpecialStudent.Text = "留校查看名單";
            this.SpecialStudent.UseVisualStyleBackColor = true;
            this.SpecialStudent.CheckedChanged += new System.EventHandler(this.SpecialStudent_CheckedChanged);
            // 
            // Fault_3B
            // 
            this.Fault_3B.AutoSize = true;
            this.Fault_3B.Location = new System.Drawing.Point(3, 3);
            this.Fault_3B.Name = "Fault_3B";
            this.Fault_3B.Size = new System.Drawing.Size(122, 20);
            this.Fault_3B.TabIndex = 0;
            this.Fault_3B.Text = "三支小過以上";
            this.Fault_3B.UseVisualStyleBackColor = true;
            this.Fault_3B.CheckedChanged += new System.EventHandler(this.Fault_3B_CheckedChanged);
            // 
            // Attendance_Half
            // 
            this.Attendance_Half.AutoSize = true;
            this.Attendance_Half.Location = new System.Drawing.Point(3, 107);
            this.Attendance_Half.Name = "Attendance_Half";
            this.Attendance_Half.Size = new System.Drawing.Size(186, 20);
            this.Attendance_Half.TabIndex = 4;
            this.Attendance_Half.Text = "缺席節次超過指定節次";
            this.Attendance_Half.UseVisualStyleBackColor = true;
            this.Attendance_Half.CheckedChanged += new System.EventHandler(this.Attendance_Half_CheckedChanged);
            // 
            // Attendance_21
            // 
            this.Attendance_21.AutoSize = true;
            this.Attendance_21.Location = new System.Drawing.Point(3, 29);
            this.Attendance_21.Name = "Attendance_21";
            this.Attendance_21.Size = new System.Drawing.Size(186, 20);
            this.Attendance_21.TabIndex = 1;
            this.Attendance_21.Text = "出缺勤扣分超過21分者";
            this.Attendance_21.UseVisualStyleBackColor = true;
            this.Attendance_21.CheckedChanged += new System.EventHandler(this.Attendance_21_CheckedChanged);
            // 
            // Fault_3A
            // 
            this.Fault_3A.AutoSize = true;
            this.Fault_3A.Location = new System.Drawing.Point(3, 81);
            this.Fault_3A.Name = "Fault_3A";
            this.Fault_3A.Size = new System.Drawing.Size(106, 20);
            this.Fault_3A.TabIndex = 3;
            this.Fault_3A.Text = "滿三大過者";
            this.Fault_3A.UseVisualStyleBackColor = true;
            this.Fault_3A.CheckedChanged += new System.EventHandler(this.Fault_3A_CheckedChanged);
            // 
            // VEScore_60
            // 
            this.VEScore_60.AutoSize = true;
            this.VEScore_60.Location = new System.Drawing.Point(3, 55);
            this.VEScore_60.Name = "VEScore_60";
            this.VEScore_60.Size = new System.Drawing.Size(138, 20);
            this.VEScore_60.TabIndex = 2;
            this.VEScore_60.Text = "德行成績不及格";
            this.VEScore_60.UseVisualStyleBackColor = true;
            this.VEScore_60.CheckedChanged += new System.EventHandler(this.VEScore_60_CheckedChanged);
            // 
            // SortKey
            // 
            this.SortKey.BackColor = System.Drawing.Color.Transparent;
            this.SortKey.CanvasColor = System.Drawing.SystemColors.Control;
            this.SortKey.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.SortKey.Controls.Add(this.SeatNo);
            this.SortKey.Controls.Add(this.StudentNumber);
            this.SortKey.Location = new System.Drawing.Point(15, 372);
            this.SortKey.Name = "SortKey";
            this.SortKey.Size = new System.Drawing.Size(254, 83);
            // 
            // 
            // 
            this.SortKey.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.SortKey.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.SortKey.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.SortKey.Style.BorderBottomWidth = 1;
            this.SortKey.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.SortKey.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.SortKey.Style.BorderLeftColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.SortKey.Style.BorderLeftWidth = 1;
            this.SortKey.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.SortKey.Style.BorderRightColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.SortKey.Style.BorderRightWidth = 1;
            this.SortKey.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.SortKey.Style.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.SortKey.Style.BorderTopWidth = 1;
            this.SortKey.Style.CornerDiameter = 4;
            this.SortKey.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.SortKey.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.SortKey.TabIndex = 13;
            this.SortKey.Text = "列印順序";
            // 
            // groupPanel1
            // 
            this.groupPanel1.BackColor = System.Drawing.Color.Transparent;
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel1.Controls.Add(this.AbsenceList);
            this.groupPanel1.Controls.Add(this.chkSemester);
            this.groupPanel1.Controls.Add(this.chkSelAll);
            this.groupPanel1.Controls.Add(this.txtPeriod);
            this.groupPanel1.Controls.Add(this.Absencelbl);
            this.groupPanel1.Controls.Add(this.lblPeriod);
            this.groupPanel1.Controls.Add(this.chkMequialD);
            this.groupPanel1.Controls.Add(this.chkDate);
            this.groupPanel1.Controls.Add(this.groupPanel3);
            this.groupPanel1.Location = new System.Drawing.Point(291, 37);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(246, 355);
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
            this.groupPanel1.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel1.Style.TextShadowColor = System.Drawing.Color.Transparent;
            this.groupPanel1.TabIndex = 14;
            this.groupPanel1.Text = "選項設定";
            // 
            // chkDate
            // 
            this.chkDate.AutoSize = true;
            this.chkDate.Location = new System.Drawing.Point(26, 53);
            this.chkDate.Name = "chkDate";
            this.chkDate.Size = new System.Drawing.Size(139, 20);
            this.chkDate.TabIndex = 17;
            this.chkDate.Text = "依日期區間累計";
            this.chkDate.UseVisualStyleBackColor = true;
            // 
            // groupPanel3
            // 
            this.groupPanel3.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel3.Controls.Add(this.dtStartDate);
            this.groupPanel3.Controls.Add(this.dtEndDate);
            this.groupPanel3.Controls.Add(this.labelX4);
            this.groupPanel3.Controls.Add(this.labelX3);
            this.groupPanel3.Location = new System.Drawing.Point(17, 63);
            this.groupPanel3.Name = "groupPanel3";
            this.groupPanel3.Size = new System.Drawing.Size(200, 73);
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
            this.groupPanel3.TabIndex = 18;
            // 
            // dtStartDate
            // 
            // 
            // 
            // 
            this.dtStartDate.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dtStartDate.ButtonDropDown.Visible = true;
            this.dtStartDate.Format = DevComponents.Editors.eDateTimePickerFormat.Long;
            this.dtStartDate.Location = new System.Drawing.Point(35, 8);
            // 
            // 
            // 
            this.dtStartDate.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtStartDate.MonthCalendar.BackgroundStyle.BackColor = System.Drawing.SystemColors.Window;
            this.dtStartDate.MonthCalendar.ClearButtonVisible = true;
            // 
            // 
            // 
            this.dtStartDate.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.dtStartDate.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
            this.dtStartDate.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.dtStartDate.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.dtStartDate.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.dtStartDate.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
            this.dtStartDate.MonthCalendar.DisplayMonth = new System.DateTime(2010, 3, 1, 0, 0, 0, 0);
            this.dtStartDate.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.dtStartDate.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtStartDate.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.dtStartDate.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
            this.dtStartDate.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.dtStartDate.MonthCalendar.TodayButtonVisible = true;
            this.dtStartDate.Name = "dtStartDate";
            this.dtStartDate.Size = new System.Drawing.Size(156, 27);
            this.dtStartDate.TabIndex = 13;
            // 
            // dtEndDate
            // 
            // 
            // 
            // 
            this.dtEndDate.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dtEndDate.ButtonDropDown.Visible = true;
            this.dtEndDate.Format = DevComponents.Editors.eDateTimePickerFormat.Long;
            this.dtEndDate.Location = new System.Drawing.Point(35, 38);
            // 
            // 
            // 
            this.dtEndDate.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtEndDate.MonthCalendar.BackgroundStyle.BackColor = System.Drawing.SystemColors.Window;
            this.dtEndDate.MonthCalendar.ClearButtonVisible = true;
            // 
            // 
            // 
            this.dtEndDate.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.dtEndDate.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
            this.dtEndDate.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.dtEndDate.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.dtEndDate.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.dtEndDate.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
            this.dtEndDate.MonthCalendar.DisplayMonth = new System.DateTime(2010, 3, 1, 0, 0, 0, 0);
            this.dtEndDate.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.dtEndDate.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtEndDate.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.dtEndDate.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
            this.dtEndDate.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.dtEndDate.MonthCalendar.TodayButtonVisible = true;
            this.dtEndDate.Name = "dtEndDate";
            this.dtEndDate.Size = new System.Drawing.Size(156, 27);
            this.dtEndDate.TabIndex = 15;
            // 
            // labelX4
            // 
            this.labelX4.Location = new System.Drawing.Point(6, 6);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(35, 30);
            this.labelX4.TabIndex = 14;
            this.labelX4.Text = "起：";
            // 
            // labelX3
            // 
            this.labelX3.Location = new System.Drawing.Point(6, 36);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(35, 30);
            this.labelX3.TabIndex = 16;
            this.labelX3.Text = "迄：";
            // 
            // groupPanel2
            // 
            this.groupPanel2.BackColor = System.Drawing.Color.Transparent;
            this.groupPanel2.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel2.Controls.Add(this.B4);
            this.groupPanel2.Controls.Add(this.A4);
            this.groupPanel2.Location = new System.Drawing.Point(291, 398);
            this.groupPanel2.Name = "groupPanel2";
            this.groupPanel2.Size = new System.Drawing.Size(162, 60);
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
            this.groupPanel2.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel2.TabIndex = 15;
            this.groupPanel2.Text = "紙張設定";
            // 
            // btnReport
            // 
            this.btnReport.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnReport.BackColor = System.Drawing.Color.Transparent;
            this.btnReport.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnReport.Location = new System.Drawing.Point(462, 413);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(75, 45);
            this.btnReport.TabIndex = 16;
            this.btnReport.Text = "執行";
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // cboSemester
            // 
            this.cboSemester.DisplayMember = "Text";
            this.cboSemester.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboSemester.FormattingEnabled = true;
            this.cboSemester.ItemHeight = 21;
            this.cboSemester.Items.AddRange(new object[] {
            this.comboItem1,
            this.comboItem2});
            this.cboSemester.Location = new System.Drawing.Point(190, 8);
            this.cboSemester.Name = "cboSemester";
            this.cboSemester.Size = new System.Drawing.Size(79, 27);
            this.cboSemester.TabIndex = 18;
            // 
            // comboItem1
            // 
            this.comboItem1.Text = "上學期";
            // 
            // comboItem2
            // 
            this.comboItem2.Text = "下學期";
            // 
            // intSchoolYear
            // 
            this.intSchoolYear.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.intSchoolYear.BackgroundStyle.Class = "DateTimeInputBackground";
            this.intSchoolYear.Location = new System.Drawing.Point(74, 8);
            this.intSchoolYear.MaxValue = 120;
            this.intSchoolYear.MinValue = 98;
            this.intSchoolYear.Name = "intSchoolYear";
            this.intSchoolYear.ShowUpDown = true;
            this.intSchoolYear.Size = new System.Drawing.Size(53, 27);
            this.intSchoolYear.TabIndex = 17;
            this.intSchoolYear.Value = 98;
            // 
            // labelX1
            // 
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            this.labelX1.Location = new System.Drawing.Point(15, 12);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(64, 23);
            this.labelX1.TabIndex = 19;
            this.labelX1.Text = "學年度";
            // 
            // labelX2
            // 
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            this.labelX2.Location = new System.Drawing.Point(150, 12);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(51, 23);
            this.labelX2.TabIndex = 20;
            this.labelX2.Text = "學期";
            // 
            // frmSpecialStudent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(571, 460);
            this.Controls.Add(this.cboSemester);
            this.Controls.Add(this.intSchoolYear);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.btnReport);
            this.Controls.Add(this.groupPanel2);
            this.Controls.Add(this.groupPanel1);
            this.Controls.Add(this.SortKey);
            this.Controls.Add(this.ReportType);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmSpecialStudent";
            this.Text = "特殊學生清單";
            this.Load += new System.EventHandler(this.frmSpecialStudent_Load);
            this.ReportType.ResumeLayout(false);
            this.ReportType.PerformLayout();
            this.SortKey.ResumeLayout(false);
            this.SortKey.PerformLayout();
            this.groupPanel1.ResumeLayout(false);
            this.groupPanel1.PerformLayout();
            this.groupPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtStartDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtEndDate)).EndInit();
            this.groupPanel2.ResumeLayout(false);
            this.groupPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.intSchoolYear)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.RadioButton SeatNo;
        public System.Windows.Forms.RadioButton StudentNumber;
        public System.Windows.Forms.CheckBox chkSemester;
        public System.Windows.Forms.TextBox txtPeriod;
        public System.Windows.Forms.CheckBox chkMequialD;
        public System.Windows.Forms.Label lblPeriod;
        private System.Windows.Forms.Label Absencelbl;
        public DevComponents.DotNetBar.Controls.CheckBoxX chkSelAll;
        public System.Windows.Forms.ListView AbsenceList;
        private System.Windows.Forms.RadioButton B4;
        private System.Windows.Forms.RadioButton A4;
        private DevComponents.DotNetBar.Controls.GroupPanel ReportType;
        public System.Windows.Forms.RadioButton SpecialStudent;
        public System.Windows.Forms.RadioButton Fault_3B;
        public System.Windows.Forms.RadioButton Attendance_Half;
        public System.Windows.Forms.RadioButton Attendance_21;
        public System.Windows.Forms.RadioButton Fault_3A;
        public System.Windows.Forms.RadioButton VEScore_60;
        private DevComponents.DotNetBar.Controls.GroupPanel SortKey;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel2;
        private DevComponents.DotNetBar.ButtonX btnReport;
        private DevComponents.Editors.ComboItem comboItem1;
        private DevComponents.Editors.ComboItem comboItem2;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.LabelX labelX4;
        public System.Windows.Forms.CheckBox chkDate;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel3;
        public DevComponents.Editors.DateTimeAdv.DateTimeInput dtEndDate;
        public DevComponents.Editors.DateTimeAdv.DateTimeInput dtStartDate;
        public DevComponents.DotNetBar.Controls.ComboBoxEx cboSemester;
        public DevComponents.Editors.IntegerInput intSchoolYear;

    }
}