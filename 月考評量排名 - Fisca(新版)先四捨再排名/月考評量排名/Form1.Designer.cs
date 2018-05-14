namespace 月考評量排名
{
    partial class frm1
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
            this.label1 = new System.Windows.Forms.Label();
            this.btnToX1s = new DevComponents.DotNetBar.ButtonX();
            this.chkSelAll = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.rb04 = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.txtBox03 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.rb06 = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.rb05 = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.rb03 = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.rb02 = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.rb01 = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.txtBox01 = new System.Windows.Forms.TextBox();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.lblExam = new DevComponents.DotNetBar.LabelX();
            this.cboExam = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.cboSortType = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.lblSortType = new DevComponents.DotNetBar.LabelX();
            this.lblExamlst = new DevComponents.DotNetBar.LabelX();
            this.btnToX2s = new DevComponents.DotNetBar.ButtonX();
            this.cboExamlst = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.lstSubject = new DevComponents.DotNetBar.Controls.ListViewEx();
            this.lbxSubjct = new System.Windows.Forms.ListBox();
            this.txtBox02 = new System.Windows.Forms.TextBox();
            this.cbYear = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.groupBox1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.cbClass = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.cboReportSortType = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.groupPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(16, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 16);
            this.label1.TabIndex = 1;
            // 
            // btnToX1s
            // 
            this.btnToX1s.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnToX1s.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnToX1s.BackColor = System.Drawing.Color.Transparent;
            this.btnToX1s.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnToX1s.Location = new System.Drawing.Point(196, 576);
            this.btnToX1s.Name = "btnToX1s";
            this.btnToX1s.Size = new System.Drawing.Size(104, 30);
            this.btnToX1s.TabIndex = 15;
            this.btnToX1s.Text = "產生報表";
            this.btnToX1s.Click += new System.EventHandler(this.btnToX1s_Click);
            // 
            // chkSelAll
            // 
            this.chkSelAll.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.chkSelAll.BackgroundStyle.Class = "";
            this.chkSelAll.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chkSelAll.Location = new System.Drawing.Point(145, 353);
            this.chkSelAll.Name = "chkSelAll";
            this.chkSelAll.Size = new System.Drawing.Size(107, 16);
            this.chkSelAll.TabIndex = 16;
            this.chkSelAll.Text = "科目全選";
            this.chkSelAll.CheckedChanged += new System.EventHandler(this.chkSelAll_CheckedChanged);
            // 
            // groupPanel1
            // 
            this.groupPanel1.BackColor = System.Drawing.Color.Transparent;
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel1.Controls.Add(this.rb04);
            this.groupPanel1.Controls.Add(this.labelX4);
            this.groupPanel1.Controls.Add(this.txtBox03);
            this.groupPanel1.Controls.Add(this.rb06);
            this.groupPanel1.Controls.Add(this.rb05);
            this.groupPanel1.Controls.Add(this.rb03);
            this.groupPanel1.Controls.Add(this.labelX3);
            this.groupPanel1.Controls.Add(this.rb02);
            this.groupPanel1.Controls.Add(this.rb01);
            this.groupPanel1.Controls.Add(this.txtBox01);
            this.groupPanel1.Controls.Add(this.labelX2);
            this.groupPanel1.Location = new System.Drawing.Point(13, 41);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(289, 201);
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
            this.groupPanel1.Style.Class = "";
            this.groupPanel1.Style.CornerDiameter = 4;
            this.groupPanel1.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel1.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.groupPanel1.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel1.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.groupPanel1.StyleMouseDown.Class = "";
            this.groupPanel1.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.groupPanel1.StyleMouseOver.Class = "";
            this.groupPanel1.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel1.TabIndex = 17;
            this.groupPanel1.Text = "功能選項";
            // 
            // rb04
            // 
            // 
            // 
            // 
            this.rb04.BackgroundStyle.Class = "";
            this.rb04.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.rb04.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.rb04.Location = new System.Drawing.Point(0, 89);
            this.rb04.Name = "rb04";
            this.rb04.Size = new System.Drawing.Size(232, 23);
            this.rb04.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.rb04.TabIndex = 24;
            this.rb04.Text = "加權總平均＿班級進步前三名";
            this.rb04.CheckedChanged += new System.EventHandler(this.rb04_CheckedChanged);
            // 
            // labelX4
            // 
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.Class = "";
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Location = new System.Drawing.Point(203, 152);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(24, 17);
            this.labelX4.TabIndex = 23;
            this.labelX4.Text = "分";
            // 
            // txtBox03
            // 
            // 
            // 
            // 
            this.txtBox03.Border.Class = "TextBoxBorder";
            this.txtBox03.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtBox03.Location = new System.Drawing.Point(158, 147);
            this.txtBox03.Name = "txtBox03";
            this.txtBox03.Size = new System.Drawing.Size(38, 25);
            this.txtBox03.TabIndex = 22;
            // 
            // rb06
            // 
            // 
            // 
            // 
            this.rb06.BackgroundStyle.Class = "";
            this.rb06.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.rb06.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.rb06.Location = new System.Drawing.Point(1, 147);
            this.rb06.Name = "rb06";
            this.rb06.Size = new System.Drawing.Size(207, 23);
            this.rb06.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.rb06.TabIndex = 21;
            this.rb06.Text = "加權總平均＿個人進步";
            this.rb06.CheckedChanged += new System.EventHandler(this.rb06_CheckedChanged);
            // 
            // rb05
            // 
            // 
            // 
            // 
            this.rb05.BackgroundStyle.Class = "";
            this.rb05.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.rb05.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.rb05.Location = new System.Drawing.Point(0, 118);
            this.rb05.Name = "rb05";
            this.rb05.Size = new System.Drawing.Size(227, 23);
            this.rb05.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.rb05.TabIndex = 20;
            this.rb05.Text = "加權總平均排名＿班級進步前三名";
            this.rb05.CheckedChanged += new System.EventHandler(this.rb05_CheckedChanged);
            // 
            // rb03
            // 
            // 
            // 
            // 
            this.rb03.BackgroundStyle.Class = "";
            this.rb03.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.rb03.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.rb03.Location = new System.Drawing.Point(0, 61);
            this.rb03.Name = "rb03";
            this.rb03.Size = new System.Drawing.Size(100, 17);
            this.rb03.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.rb03.TabIndex = 19;
            this.rb03.Text = "加權總平均";
            this.rb03.CheckedChanged += new System.EventHandler(this.rb03_CheckedChanged);
            // 
            // labelX3
            // 
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.Class = "";
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(109, 33);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(22, 19);
            this.labelX3.TabIndex = 18;
            this.labelX3.Text = "名";
            // 
            // rb02
            // 
            // 
            // 
            // 
            this.rb02.BackgroundStyle.Class = "";
            this.rb02.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.rb02.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.rb02.Location = new System.Drawing.Point(0, 32);
            this.rb02.Name = "rb02";
            this.rb02.Size = new System.Drawing.Size(44, 18);
            this.rb02.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.rb02.TabIndex = 17;
            this.rb02.Text = "前";
            this.rb02.CheckedChanged += new System.EventHandler(this.rb02_CheckedChanged);
            // 
            // rb01
            // 
            // 
            // 
            // 
            this.rb01.BackgroundStyle.Class = "";
            this.rb01.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.rb01.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.rb01.Location = new System.Drawing.Point(0, 8);
            this.rb01.Name = "rb01";
            this.rb01.Size = new System.Drawing.Size(100, 17);
            this.rb01.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.rb01.TabIndex = 16;
            this.rb01.Text = "全部";
            this.rb01.CheckedChanged += new System.EventHandler(this.rb01_CheckedChanged);
            // 
            // txtBox01
            // 
            this.txtBox01.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtBox01.Location = new System.Drawing.Point(50, 31);
            this.txtBox01.Name = "txtBox01";
            this.txtBox01.Size = new System.Drawing.Size(49, 22);
            this.txtBox01.TabIndex = 10;
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.Class = "";
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(140, 49);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(50, 50);
            this.labelX2.TabIndex = 0;
            this.labelX2.Text = "以上";
            // 
            // labelX1
            // 
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.Class = "";
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(12, 352);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(109, 20);
            this.labelX1.TabIndex = 20;
            this.labelX1.Text = "科目名稱與級別";
            // 
            // lblExam
            // 
            this.lblExam.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblExam.BackgroundStyle.Class = "";
            this.lblExam.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblExam.Location = new System.Drawing.Point(16, 14);
            this.lblExam.Name = "lblExam";
            this.lblExam.Size = new System.Drawing.Size(75, 23);
            this.lblExam.TabIndex = 23;
            this.lblExam.Text = "試別：";
            // 
            // cboExam
            // 
            this.cboExam.DisplayMember = "Text";
            this.cboExam.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboExam.FormattingEnabled = true;
            this.cboExam.ItemHeight = 19;
            this.cboExam.Location = new System.Drawing.Point(66, 12);
            this.cboExam.Name = "cboExam";
            this.cboExam.Size = new System.Drawing.Size(236, 25);
            this.cboExam.TabIndex = 25;
            this.cboExam.SelectedIndexChanged += new System.EventHandler(this.cboExam_SelectedIndexChanged);
            // 
            // cboSortType
            // 
            this.cboSortType.DisplayMember = "Text";
            this.cboSortType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboSortType.FormattingEnabled = true;
            this.cboSortType.ItemHeight = 19;
            this.cboSortType.Location = new System.Drawing.Point(80, 249);
            this.cboSortType.Name = "cboSortType";
            this.cboSortType.Size = new System.Drawing.Size(222, 25);
            this.cboSortType.TabIndex = 30;
            this.cboSortType.SelectedIndexChanged += new System.EventHandler(this.cboSortType_SelectedIndexChanged);
            // 
            // lblSortType
            // 
            this.lblSortType.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblSortType.BackgroundStyle.Class = "";
            this.lblSortType.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblSortType.Location = new System.Drawing.Point(13, 249);
            this.lblSortType.Name = "lblSortType";
            this.lblSortType.Size = new System.Drawing.Size(81, 26);
            this.lblSortType.TabIndex = 29;
            this.lblSortType.Text = "排名方式：";
            // 
            // lblExamlst
            // 
            this.lblExamlst.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblExamlst.BackgroundStyle.Class = "";
            this.lblExamlst.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblExamlst.Location = new System.Drawing.Point(13, 324);
            this.lblExamlst.Name = "lblExamlst";
            this.lblExamlst.Size = new System.Drawing.Size(78, 25);
            this.lblExamlst.TabIndex = 31;
            this.lblExamlst.Text = "前次試別：";
            // 
            // btnToX2s
            // 
            this.btnToX2s.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnToX2s.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnToX2s.BackColor = System.Drawing.Color.Transparent;
            this.btnToX2s.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnToX2s.Location = new System.Drawing.Point(11, 576);
            this.btnToX2s.Name = "btnToX2s";
            this.btnToX2s.Size = new System.Drawing.Size(95, 30);
            this.btnToX2s.TabIndex = 32;
            this.btnToX2s.Text = "製作獎狀";
            this.btnToX2s.Visible = false;
            this.btnToX2s.Click += new System.EventHandler(this.btnToX2s_Click);
            // 
            // cboExamlst
            // 
            this.cboExamlst.DisplayMember = "Text";
            this.cboExamlst.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboExamlst.FormattingEnabled = true;
            this.cboExamlst.ItemHeight = 19;
            this.cboExamlst.Location = new System.Drawing.Point(93, 321);
            this.cboExamlst.Name = "cboExamlst";
            this.cboExamlst.Size = new System.Drawing.Size(209, 25);
            this.cboExamlst.TabIndex = 33;
            this.cboExamlst.SelectedIndexChanged += new System.EventHandler(this.cboExamlst_SelectedIndexChanged);
            // 
            // lstSubject
            // 
            // 
            // 
            // 
            this.lstSubject.Border.Class = "ListViewBorder";
            this.lstSubject.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lstSubject.Location = new System.Drawing.Point(14, 375);
            this.lstSubject.Name = "lstSubject";
            this.lstSubject.Size = new System.Drawing.Size(288, 156);
            this.lstSubject.TabIndex = 18;
            this.lstSubject.UseCompatibleStateImageBehavior = false;
            this.lstSubject.View = System.Windows.Forms.View.List;
            // 
            // lbxSubjct
            // 
            this.lbxSubjct.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lbxSubjct.FormattingEnabled = true;
            this.lbxSubjct.ItemHeight = 17;
            this.lbxSubjct.Location = new System.Drawing.Point(12, 374);
            this.lbxSubjct.Name = "lbxSubjct";
            this.lbxSubjct.Size = new System.Drawing.Size(290, 157);
            this.lbxSubjct.TabIndex = 34;
            // 
            // txtBox02
            // 
            this.txtBox02.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtBox02.Location = new System.Drawing.Point(106, 124);
            this.txtBox02.Name = "txtBox02";
            this.txtBox02.Size = new System.Drawing.Size(49, 22);
            this.txtBox02.TabIndex = 36;
            // 
            // cbYear
            // 
            this.cbYear.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.cbYear.BackgroundStyle.Class = "";
            this.cbYear.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.cbYear.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.cbYear.Location = new System.Drawing.Point(82, 3);
            this.cbYear.Name = "cbYear";
            this.cbYear.Size = new System.Drawing.Size(155, 23);
            this.cbYear.TabIndex = 38;
            this.cbYear.Text = "年級(依選取班級排名)";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupBox1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupBox1.Controls.Add(this.cbClass);
            this.groupBox1.Controls.Add(this.cbYear);
            this.groupBox1.Location = new System.Drawing.Point(12, 280);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(290, 35);
            // 
            // 
            // 
            this.groupBox1.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupBox1.Style.BackColorGradientAngle = 90;
            this.groupBox1.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupBox1.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupBox1.Style.BorderBottomWidth = 1;
            this.groupBox1.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupBox1.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupBox1.Style.BorderLeftWidth = 1;
            this.groupBox1.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupBox1.Style.BorderRightWidth = 1;
            this.groupBox1.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupBox1.Style.BorderTopWidth = 1;
            this.groupBox1.Style.Class = "";
            this.groupBox1.Style.CornerDiameter = 4;
            this.groupBox1.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupBox1.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.groupBox1.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupBox1.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.groupBox1.StyleMouseDown.Class = "";
            this.groupBox1.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.groupBox1.StyleMouseOver.Class = "";
            this.groupBox1.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupBox1.TabIndex = 39;
            // 
            // cbClass
            // 
            this.cbClass.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.cbClass.BackgroundStyle.Class = "";
            this.cbClass.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.cbClass.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.cbClass.Location = new System.Drawing.Point(4, 2);
            this.cbClass.Name = "cbClass";
            this.cbClass.Size = new System.Drawing.Size(67, 27);
            this.cbClass.TabIndex = 39;
            this.cbClass.Text = "班級";
            // 
            // cboReportSortType
            // 
            this.cboReportSortType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cboReportSortType.DisplayMember = "Text";
            this.cboReportSortType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboReportSortType.FormattingEnabled = true;
            this.cboReportSortType.ItemHeight = 19;
            this.cboReportSortType.Location = new System.Drawing.Point(104, 545);
            this.cboReportSortType.Name = "cboReportSortType";
            this.cboReportSortType.Size = new System.Drawing.Size(196, 25);
            this.cboReportSortType.TabIndex = 41;
            // 
            // labelX5
            // 
            this.labelX5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelX5.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.Class = "";
            this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX5.Location = new System.Drawing.Point(13, 545);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(101, 25);
            this.labelX5.TabIndex = 40;
            this.labelX5.Text = "報表排序方式：";
            // 
            // frm1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(314, 608);
            this.Controls.Add(this.cboReportSortType);
            this.Controls.Add(this.labelX5);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtBox02);
            this.Controls.Add(this.lbxSubjct);
            this.Controls.Add(this.cboExamlst);
            this.Controls.Add(this.btnToX2s);
            this.Controls.Add(this.lblExamlst);
            this.Controls.Add(this.cboSortType);
            this.Controls.Add(this.lblSortType);
            this.Controls.Add(this.cboExam);
            this.Controls.Add(this.lblExam);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.lstSubject);
            this.Controls.Add(this.chkSelAll);
            this.Controls.Add(this.btnToX1s);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupPanel1);
            this.DoubleBuffered = true;
            this.MaximumSize = new System.Drawing.Size(330, 800);
            this.MinimumSize = new System.Drawing.Size(330, 647);
            this.Name = "frm1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "月考評量優等";
            this.Load += new System.EventHandler(this.frm1_Load);
            this.groupPanel1.ResumeLayout(false);
            this.groupPanel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private DevComponents.DotNetBar.ButtonX btnToX1s;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkSelAll;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        private System.Windows.Forms.TextBox txtBox01;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX lblExam;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboExam;
        private DevComponents.DotNetBar.LabelX lblSortType;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboSortType;
        private DevComponents.DotNetBar.LabelX lblExamlst;
        private DevComponents.DotNetBar.ButtonX btnToX2s;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboExamlst;
        private DevComponents.DotNetBar.Controls.ListViewEx lstSubject;
        private System.Windows.Forms.ListBox lbxSubjct;
        private System.Windows.Forms.TextBox txtBox02;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.CheckBoxX rb06;
        private DevComponents.DotNetBar.Controls.CheckBoxX rb05;
        private DevComponents.DotNetBar.Controls.CheckBoxX rb03;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.Controls.CheckBoxX rb02;
        private DevComponents.DotNetBar.Controls.CheckBoxX rb01;
        private DevComponents.DotNetBar.Controls.CheckBoxX cbYear;
        private DevComponents.DotNetBar.Controls.GroupPanel groupBox1;
        private DevComponents.DotNetBar.Controls.CheckBoxX cbClass;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.Controls.TextBoxX txtBox03;
        private DevComponents.DotNetBar.Controls.CheckBoxX rb04;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboReportSortType;
        private DevComponents.DotNetBar.LabelX labelX5;
    }
}

