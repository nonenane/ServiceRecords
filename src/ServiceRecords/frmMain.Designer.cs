namespace ServiceRecords
{
    partial class frmMain
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.cmbDeps = new System.Windows.Forms.ComboBox();
            this.cmbBlock = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbArhiv = new System.Windows.Forms.RadioButton();
            this.rbWork = new System.Windows.Forms.RadioButton();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbNumber = new System.Windows.Forms.TextBox();
            this.tbInfo = new System.Windows.Forms.TextBox();
            this.cmsWorking = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmsiTakeMoney = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsiDropeMoney = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAddDoc = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiClose = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSetReport = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAnylSZ = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.помощьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chbUpdate = new System.Windows.Forms.CheckBox();
            this.timUpdate = new System.Windows.Forms.Timer(this.components);
            this.btnRefuse = new System.Windows.Forms.Button();
            this.btnAccept = new System.Windows.Forms.Button();
            this.lblObject = new System.Windows.Forms.Label();
            this.cmbObjects = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.btnCheckReport = new System.Windows.Forms.Button();
            this.btReportNoteMoreDeps = new System.Windows.Forms.Button();
            this.btReportNoteSingleDeps = new System.Windows.Forms.Button();
            this.btListNotePeriod = new System.Windows.Forms.Button();
            this.btChangeStatus = new System.Windows.Forms.Button();
            this.btViewHistoryStatus = new System.Windows.Forms.Button();
            this.btViewPayment = new System.Windows.Forms.Button();
            this.btAccept = new System.Windows.Forms.Button();
            this.btRefuse = new System.Windows.Forms.Button();
            this.btView = new System.Windows.Forms.Button();
            this.btUpdate = new System.Windows.Forms.Button();
            this.btClose = new System.Windows.Forms.Button();
            this.btAddBlock = new System.Windows.Forms.Button();
            this.btEditBlock = new System.Windows.Forms.Button();
            this.btDelBlock = new System.Windows.Forms.Button();
            this.chbKD = new System.Windows.Forms.CheckBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.dgvMain = new System.Windows.Forms.DataGridView();
            this.Enter = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.cNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cDateCreate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cNameBlock = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cSumma = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Valuta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cDataAdd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cNameStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cDateStatusChange = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cFIO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cScane = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.col_object = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TypeServiceRecordOnTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Creator = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.btnPrintFond = new System.Windows.Forms.Button();
            this.chbReportPreMonth = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.cmsWorking.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).BeginInit();
            this.panel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbDeps
            // 
            this.cmbDeps.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDeps.FormattingEnabled = true;
            this.cmbDeps.Location = new System.Drawing.Point(386, 64);
            this.cmbDeps.Name = "cmbDeps";
            this.cmbDeps.Size = new System.Drawing.Size(225, 21);
            this.cmbDeps.TabIndex = 34;
            this.cmbDeps.SelectionChangeCommitted += new System.EventHandler(this.cmbDeps_SelectionChangeCommitted);
            // 
            // cmbBlock
            // 
            this.cmbBlock.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBlock.FormattingEnabled = true;
            this.cmbBlock.Location = new System.Drawing.Point(386, 37);
            this.cmbBlock.Name = "cmbBlock";
            this.cmbBlock.Size = new System.Drawing.Size(225, 21);
            this.cmbBlock.TabIndex = 35;
            this.cmbBlock.SelectionChangeCommitted += new System.EventHandler(this.cmbBlock_SelectionChangeCommitted);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(350, 41);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 13);
            this.label4.TabIndex = 32;
            this.label4.Text = "Блок";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(344, 68);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 13);
            this.label5.TabIndex = 33;
            this.label5.Text = "Отдел";
            // 
            // dtpEnd
            // 
            this.dtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEnd.Location = new System.Drawing.Point(214, 37);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(98, 20);
            this.dtpEnd.TabIndex = 37;
            this.dtpEnd.CloseUp += new System.EventHandler(this.dtpEnd_CloseUp);
            this.dtpEnd.ValueChanged += new System.EventHandler(this.dtpEnd_ValueChanged);
            this.dtpEnd.Leave += new System.EventHandler(this.dtpEnd_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(186, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(19, 13);
            this.label3.TabIndex = 36;
            this.label3.Text = "по";
            // 
            // dtpStart
            // 
            this.dtpStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStart.Location = new System.Drawing.Point(82, 37);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size(98, 20);
            this.dtpStart.TabIndex = 37;
            this.dtpStart.CloseUp += new System.EventHandler(this.dtpStart_CloseUp);
            this.dtpStart.ValueChanged += new System.EventHandler(this.dtpStart_ValueChanged);
            this.dtpStart.Leave += new System.EventHandler(this.dtpStart_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 36;
            this.label1.Text = "Период с";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbArhiv);
            this.groupBox1.Controls.Add(this.rbWork);
            this.groupBox1.Controls.Add(this.cmbStatus);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(643, 37);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(328, 69);
            this.groupBox1.TabIndex = 38;
            this.groupBox1.TabStop = false;
            // 
            // rbArhiv
            // 
            this.rbArhiv.AutoSize = true;
            this.rbArhiv.Location = new System.Drawing.Point(202, 46);
            this.rbArhiv.Name = "rbArhiv";
            this.rbArhiv.Size = new System.Drawing.Size(55, 17);
            this.rbArhiv.TabIndex = 36;
            this.rbArhiv.Text = "Архив";
            this.rbArhiv.UseVisualStyleBackColor = true;
            this.rbArhiv.Click += new System.EventHandler(this.rbWork_Click);
            // 
            // rbWork
            // 
            this.rbWork.AutoSize = true;
            this.rbWork.Checked = true;
            this.rbWork.Location = new System.Drawing.Point(29, 46);
            this.rbWork.Name = "rbWork";
            this.rbWork.Size = new System.Drawing.Size(91, 17);
            this.rbWork.TabIndex = 36;
            this.rbWork.TabStop = true;
            this.rbWork.Text = "Все в работе";
            this.rbWork.UseVisualStyleBackColor = true;
            this.rbWork.Click += new System.EventHandler(this.rbWork_Click);
            // 
            // cmbStatus
            // 
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Location = new System.Drawing.Point(73, 19);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(214, 21);
            this.cmbStatus.TabIndex = 35;
            this.cmbStatus.SelectionChangeCommitted += new System.EventHandler(this.cmbStatus_SelectionChangeCommitted);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 32;
            this.label2.Text = "Статус";
            // 
            // tbNumber
            // 
            this.tbNumber.Location = new System.Drawing.Point(12, 120);
            this.tbNumber.Name = "tbNumber";
            this.tbNumber.Size = new System.Drawing.Size(64, 20);
            this.tbNumber.TabIndex = 44;
            this.tbNumber.TextChanged += new System.EventHandler(this.tbNumber_TextChanged);
            // 
            // tbInfo
            // 
            this.tbInfo.Location = new System.Drawing.Point(277, 120);
            this.tbInfo.Name = "tbInfo";
            this.tbInfo.Size = new System.Drawing.Size(157, 20);
            this.tbInfo.TabIndex = 44;
            this.tbInfo.TextChanged += new System.EventHandler(this.tbNumber_TextChanged);
            // 
            // cmsWorking
            // 
            this.cmsWorking.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmsWorking.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmsiTakeMoney,
            this.cmsiDropeMoney,
            this.tsmiAddDoc,
            this.tsmiClose,
            this.tsmiSetReport,
            this.tsmiAnylSZ});
            this.cmsWorking.Name = "cmsWorking";
            this.cmsWorking.Size = new System.Drawing.Size(181, 136);
            this.cmsWorking.Opening += new System.ComponentModel.CancelEventHandler(this.cmsWorking_Opening);
            // 
            // cmsiTakeMoney
            // 
            this.cmsiTakeMoney.Name = "cmsiTakeMoney";
            this.cmsiTakeMoney.Size = new System.Drawing.Size(180, 22);
            this.cmsiTakeMoney.Text = "Заказать Деньги";
            this.cmsiTakeMoney.Click += new System.EventHandler(this.cmsiTakeMoney_Click);
            // 
            // cmsiDropeMoney
            // 
            this.cmsiDropeMoney.Name = "cmsiDropeMoney";
            this.cmsiDropeMoney.Size = new System.Drawing.Size(180, 22);
            this.cmsiDropeMoney.Text = "Вернуть Деньги";
            this.cmsiDropeMoney.Click += new System.EventHandler(this.cmsiDropeMoney_Click);
            // 
            // tsmiAddDoc
            // 
            this.tsmiAddDoc.Name = "tsmiAddDoc";
            this.tsmiAddDoc.Size = new System.Drawing.Size(180, 22);
            this.tsmiAddDoc.Text = "Добавить документ";
            this.tsmiAddDoc.Click += new System.EventHandler(this.tsmiAddDoc_Click);
            // 
            // tsmiClose
            // 
            this.tsmiClose.Name = "tsmiClose";
            this.tsmiClose.Size = new System.Drawing.Size(180, 22);
            this.tsmiClose.Text = "Закрыть служебку";
            this.tsmiClose.Click += new System.EventHandler(this.tsmiClose_Click);
            // 
            // tsmiSetReport
            // 
            this.tsmiSetReport.Name = "tsmiSetReport";
            this.tsmiSetReport.Size = new System.Drawing.Size(180, 22);
            this.tsmiSetReport.Text = "Предоставить отчет";
            this.tsmiSetReport.Visible = false;
            this.tsmiSetReport.Click += new System.EventHandler(this.tsmiSetReport_Click);
            // 
            // tsmiAnylSZ
            // 
            this.tsmiAnylSZ.Name = "tsmiAnylSZ";
            this.tsmiAnylSZ.Size = new System.Drawing.Size(180, 22);
            this.tsmiAnylSZ.Text = "Анулировать СЗ";
            this.tsmiAnylSZ.Visible = false;
            this.tsmiAnylSZ.Click += new System.EventHandler(this.анулироватьСЗToolStripMenuItem_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.помощьToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1023, 24);
            this.menuStrip1.TabIndex = 49;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // помощьToolStripMenuItem
            // 
            this.помощьToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.помощьToolStripMenuItem.Name = "помощьToolStripMenuItem";
            this.помощьToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.помощьToolStripMenuItem.Text = "Помощь";
            this.помощьToolStripMenuItem.Click += new System.EventHandler(this.помощьToolStripMenuItem_Click);
            // 
            // chbUpdate
            // 
            this.chbUpdate.AutoSize = true;
            this.chbUpdate.Location = new System.Drawing.Point(805, 115);
            this.chbUpdate.Name = "chbUpdate";
            this.chbUpdate.Size = new System.Drawing.Size(172, 17);
            this.chbUpdate.TabIndex = 50;
            this.chbUpdate.Text = "Обновлять каждые 10 минут";
            this.chbUpdate.UseVisualStyleBackColor = true;
            this.chbUpdate.CheckedChanged += new System.EventHandler(this.chbUpdate_CheckedChanged);
            // 
            // timUpdate
            // 
            this.timUpdate.Tick += new System.EventHandler(this.timUpdate_Tick);
            // 
            // btnRefuse
            // 
            this.btnRefuse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRefuse.Enabled = false;
            this.btnRefuse.Location = new System.Drawing.Point(493, 594);
            this.btnRefuse.Margin = new System.Windows.Forms.Padding(2);
            this.btnRefuse.Name = "btnRefuse";
            this.btnRefuse.Size = new System.Drawing.Size(81, 32);
            this.btnRefuse.TabIndex = 51;
            this.btnRefuse.Text = "Отклонить";
            this.btnRefuse.UseVisualStyleBackColor = true;
            this.btnRefuse.Click += new System.EventHandler(this.btnRefuse_Click);
            // 
            // btnAccept
            // 
            this.btnAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAccept.Enabled = false;
            this.btnAccept.Location = new System.Drawing.Point(578, 594);
            this.btnAccept.Margin = new System.Windows.Forms.Padding(2);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(85, 32);
            this.btnAccept.TabIndex = 52;
            this.btnAccept.Text = "Подтвердить";
            this.btnAccept.UseVisualStyleBackColor = true;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // lblObject
            // 
            this.lblObject.AutoSize = true;
            this.lblObject.Location = new System.Drawing.Point(338, 93);
            this.lblObject.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblObject.Name = "lblObject";
            this.lblObject.Size = new System.Drawing.Size(45, 13);
            this.lblObject.TabIndex = 53;
            this.lblObject.Text = "Объект";
            // 
            // cmbObjects
            // 
            this.cmbObjects.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbObjects.FormattingEnabled = true;
            this.cmbObjects.Location = new System.Drawing.Point(387, 93);
            this.cmbObjects.Name = "cmbObjects";
            this.cmbObjects.Size = new System.Drawing.Size(224, 21);
            this.cmbObjects.TabIndex = 54;
            this.cmbObjects.SelectionChangeCommitted += new System.EventHandler(this.cmbObjects_SelectionChangeCommitted);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.BackColor = System.Drawing.Color.LightGreen;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.panel2);
            this.panel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.panel1.Location = new System.Drawing.Point(12, 528);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(20, 20);
            this.panel1.TabIndex = 55;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.panel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.panel2.Location = new System.Drawing.Point(1, 25);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(20, 20);
            this.panel2.TabIndex = 56;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(42, 532);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 13);
            this.label6.TabIndex = 56;
            this.label6.Text = "- Разовая СЗ";
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel3.BackColor = System.Drawing.Color.PowderBlue;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Location = new System.Drawing.Point(11, 555);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(20, 20);
            this.panel3.TabIndex = 57;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(42, 559);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(100, 13);
            this.label7.TabIndex = 58;
            this.label7.Text = "- Ежемесячная СЗ";
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel4.Location = new System.Drawing.Point(166, 528);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(20, 20);
            this.panel4.TabIndex = 59;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(192, 532);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(57, 13);
            this.label8.TabIndex = 60;
            this.label8.Text = "- Долг > 0";
            // 
            // btnCheckReport
            // 
            this.btnCheckReport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCheckReport.Image = global::ServiceRecords.Properties.Resources.report;
            this.btnCheckReport.Location = new System.Drawing.Point(123, 594);
            this.btnCheckReport.Name = "btnCheckReport";
            this.btnCheckReport.Size = new System.Drawing.Size(32, 32);
            this.btnCheckReport.TabIndex = 61;
            this.btnCheckReport.UseVisualStyleBackColor = true;
            this.btnCheckReport.Click += new System.EventHandler(this.btnCheckReport_Click);
            // 
            // btReportNoteMoreDeps
            // 
            this.btReportNoteMoreDeps.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btReportNoteMoreDeps.Image = global::ServiceRecords.Properties.Resources.edit_1761;
            this.btReportNoteMoreDeps.Location = new System.Drawing.Point(415, 594);
            this.btReportNoteMoreDeps.Name = "btReportNoteMoreDeps";
            this.btReportNoteMoreDeps.Size = new System.Drawing.Size(32, 32);
            this.btReportNoteMoreDeps.TabIndex = 48;
            this.btReportNoteMoreDeps.UseVisualStyleBackColor = true;
            this.btReportNoteMoreDeps.Click += new System.EventHandler(this.btReportNoteMoreDeps_Click);
            // 
            // btReportNoteSingleDeps
            // 
            this.btReportNoteSingleDeps.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btReportNoteSingleDeps.Image = global::ServiceRecords.Properties.Resources.x_office_spreadsheet;
            this.btReportNoteSingleDeps.Location = new System.Drawing.Point(377, 594);
            this.btReportNoteSingleDeps.Name = "btReportNoteSingleDeps";
            this.btReportNoteSingleDeps.Size = new System.Drawing.Size(32, 32);
            this.btReportNoteSingleDeps.TabIndex = 48;
            this.btReportNoteSingleDeps.UseVisualStyleBackColor = true;
            this.btReportNoteSingleDeps.Visible = false;
            this.btReportNoteSingleDeps.Click += new System.EventHandler(this.btReportNoteSingleDeps_Click);
            // 
            // btListNotePeriod
            // 
            this.btListNotePeriod.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btListNotePeriod.Image = global::ServiceRecords.Properties.Resources.klpq_2511;
            this.btListNotePeriod.Location = new System.Drawing.Point(339, 594);
            this.btListNotePeriod.Name = "btListNotePeriod";
            this.btListNotePeriod.Size = new System.Drawing.Size(32, 32);
            this.btListNotePeriod.TabIndex = 48;
            this.btListNotePeriod.UseVisualStyleBackColor = true;
            this.btListNotePeriod.Visible = false;
            this.btListNotePeriod.Click += new System.EventHandler(this.btListNotePeriod_Click);
            // 
            // btChangeStatus
            // 
            this.btChangeStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btChangeStatus.Image = global::ServiceRecords.Properties.Resources.document_library;
            this.btChangeStatus.Location = new System.Drawing.Point(47, 594);
            this.btChangeStatus.Name = "btChangeStatus";
            this.btChangeStatus.Size = new System.Drawing.Size(32, 32);
            this.btChangeStatus.TabIndex = 47;
            this.btChangeStatus.UseVisualStyleBackColor = true;
            this.btChangeStatus.Visible = false;
            this.btChangeStatus.Click += new System.EventHandler(this.btChangeStatus_Click);
            // 
            // btViewHistoryStatus
            // 
            this.btViewHistoryStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btViewHistoryStatus.Image = global::ServiceRecords.Properties.Resources.find_9299;
            this.btViewHistoryStatus.Location = new System.Drawing.Point(12, 594);
            this.btViewHistoryStatus.Name = "btViewHistoryStatus";
            this.btViewHistoryStatus.Size = new System.Drawing.Size(32, 32);
            this.btViewHistoryStatus.TabIndex = 47;
            this.btViewHistoryStatus.UseVisualStyleBackColor = true;
            this.btViewHistoryStatus.Click += new System.EventHandler(this.btViewHistoryStatus_Click);
            // 
            // btViewPayment
            // 
            this.btViewPayment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btViewPayment.Image = global::ServiceRecords.Properties.Resources.table;
            this.btViewPayment.Location = new System.Drawing.Point(85, 594);
            this.btViewPayment.Name = "btViewPayment";
            this.btViewPayment.Size = new System.Drawing.Size(32, 32);
            this.btViewPayment.TabIndex = 46;
            this.btViewPayment.UseVisualStyleBackColor = true;
            this.btViewPayment.Click += new System.EventHandler(this.btViewPayment_Click);
            // 
            // btAccept
            // 
            this.btAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btAccept.Image = global::ServiceRecords.Properties.Resources.old_edit_redo1;
            this.btAccept.Location = new System.Drawing.Point(737, 594);
            this.btAccept.Name = "btAccept";
            this.btAccept.Size = new System.Drawing.Size(32, 32);
            this.btAccept.TabIndex = 45;
            this.btAccept.UseVisualStyleBackColor = true;
            this.btAccept.Click += new System.EventHandler(this.btAccept_Click);
            // 
            // btRefuse
            // 
            this.btRefuse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btRefuse.Image = global::ServiceRecords.Properties.Resources.old_edit_undo1;
            this.btRefuse.Location = new System.Drawing.Point(696, 594);
            this.btRefuse.Name = "btRefuse";
            this.btRefuse.Size = new System.Drawing.Size(32, 32);
            this.btRefuse.TabIndex = 45;
            this.btRefuse.UseVisualStyleBackColor = true;
            this.btRefuse.Click += new System.EventHandler(this.btRefuse_Click);
            // 
            // btView
            // 
            this.btView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btView.Image = global::ServiceRecords.Properties.Resources.old_edit_find;
            this.btView.Location = new System.Drawing.Point(825, 594);
            this.btView.Name = "btView";
            this.btView.Size = new System.Drawing.Size(32, 32);
            this.btView.TabIndex = 43;
            this.btView.UseVisualStyleBackColor = true;
            this.btView.Click += new System.EventHandler(this.btView_Click);
            // 
            // btUpdate
            // 
            this.btUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btUpdate.Image = global::ServiceRecords.Properties.Resources.reload_8055;
            this.btUpdate.Location = new System.Drawing.Point(985, 46);
            this.btUpdate.Name = "btUpdate";
            this.btUpdate.Size = new System.Drawing.Size(32, 32);
            this.btUpdate.TabIndex = 39;
            this.btUpdate.UseVisualStyleBackColor = true;
            this.btUpdate.Click += new System.EventHandler(this.btUpdate_Click);
            // 
            // btClose
            // 
            this.btClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btClose.Image = global::ServiceRecords.Properties.Resources.exit_8633;
            this.btClose.Location = new System.Drawing.Point(979, 594);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(32, 32);
            this.btClose.TabIndex = 39;
            this.btClose.UseVisualStyleBackColor = true;
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
            // 
            // btAddBlock
            // 
            this.btAddBlock.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btAddBlock.Image = global::ServiceRecords.Properties.Resources.document_add;
            this.btAddBlock.Location = new System.Drawing.Point(863, 594);
            this.btAddBlock.Name = "btAddBlock";
            this.btAddBlock.Size = new System.Drawing.Size(32, 32);
            this.btAddBlock.TabIndex = 40;
            this.btAddBlock.UseVisualStyleBackColor = true;
            this.btAddBlock.Click += new System.EventHandler(this.btAddBlock_Click);
            // 
            // btEditBlock
            // 
            this.btEditBlock.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btEditBlock.Image = global::ServiceRecords.Properties.Resources.edit;
            this.btEditBlock.Location = new System.Drawing.Point(902, 594);
            this.btEditBlock.Name = "btEditBlock";
            this.btEditBlock.Size = new System.Drawing.Size(32, 32);
            this.btEditBlock.TabIndex = 41;
            this.btEditBlock.UseVisualStyleBackColor = true;
            this.btEditBlock.Click += new System.EventHandler(this.btEditBlock_Click);
            // 
            // btDelBlock
            // 
            this.btDelBlock.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btDelBlock.Image = global::ServiceRecords.Properties.Resources.document_delete;
            this.btDelBlock.Location = new System.Drawing.Point(941, 594);
            this.btDelBlock.Name = "btDelBlock";
            this.btDelBlock.Size = new System.Drawing.Size(32, 32);
            this.btDelBlock.TabIndex = 42;
            this.btDelBlock.UseVisualStyleBackColor = true;
            this.btDelBlock.Click += new System.EventHandler(this.btDelBlock_Click);
            // 
            // chbKD
            // 
            this.chbKD.AutoSize = true;
            this.chbKD.Checked = true;
            this.chbKD.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbKD.Location = new System.Drawing.Point(617, 115);
            this.chbKD.Name = "chbKD";
            this.chbKD.Size = new System.Drawing.Size(180, 17);
            this.chbKD.TabIndex = 62;
            this.chbKD.Text = "Требуется подтверждение КД";
            this.chbKD.UseVisualStyleBackColor = true;
            this.chbKD.Click += new System.EventHandler(this.chbKD_Click);
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel5.Location = new System.Drawing.Point(166, 555);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(20, 20);
            this.panel5.TabIndex = 60;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(194, 559);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(121, 13);
            this.label9.TabIndex = 63;
            this.label9.Text = "- Предварительная СЗ";
            // 
            // dgvMain
            // 
            this.dgvMain.AllowUserToAddRows = false;
            this.dgvMain.AllowUserToDeleteRows = false;
            this.dgvMain.AllowUserToResizeRows = false;
            this.dgvMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvMain.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMain.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMain.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Enter,
            this.cNumber,
            this.id,
            this.cDateCreate,
            this.cNameBlock,
            this.cDescription,
            this.cSumma,
            this.Valuta,
            this.cDataAdd,
            this.cNameStatus,
            this.cDateStatusChange,
            this.cFIO,
            this.cScane,
            this.col_object,
            this.TypeServiceRecordOnTime,
            this.Creator});
            this.dgvMain.Location = new System.Drawing.Point(12, 157);
            this.dgvMain.MultiSelect = false;
            this.dgvMain.Name = "dgvMain";
            this.dgvMain.ReadOnly = true;
            this.dgvMain.RowHeadersVisible = false;
            this.dgvMain.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMain.Size = new System.Drawing.Size(999, 353);
            this.dgvMain.TabIndex = 64;
            this.dgvMain.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMain_CellContentClick);
            this.dgvMain.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvMain_CellMouseClick);
            this.dgvMain.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvMain_CellMouseDoubleClick);
            this.dgvMain.CurrentCellChanged += new System.EventHandler(this.dgvMain_CurrentCellChanged);
            this.dgvMain.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvMain_RowPostPaint);
            this.dgvMain.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dgvMain_RowPrePaint);
            this.dgvMain.Sorted += new System.EventHandler(this.dgvMain_Sorted);
            // 
            // Enter
            // 
            this.Enter.DataPropertyName = "v_enter";
            this.Enter.FalseValue = "true";
            this.Enter.FillWeight = 7.216164F;
            this.Enter.HeaderText = "V";
            this.Enter.Name = "Enter";
            this.Enter.ReadOnly = true;
            // 
            // cNumber
            // 
            this.cNumber.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.cNumber.DataPropertyName = "Number";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.cNumber.DefaultCellStyle = dataGridViewCellStyle2;
            this.cNumber.FillWeight = 199.1216F;
            this.cNumber.HeaderText = "№ СЗ";
            this.cNumber.MinimumWidth = 40;
            this.cNumber.Name = "cNumber";
            this.cNumber.ReadOnly = true;
            this.cNumber.Width = 60;
            // 
            // id
            // 
            this.id.DataPropertyName = "id";
            this.id.HeaderText = "id";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Visible = false;
            // 
            // cDateCreate
            // 
            this.cDateCreate.DataPropertyName = "DateCreate";
            this.cDateCreate.FillWeight = 22.47935F;
            this.cDateCreate.HeaderText = "Дата и время создания";
            this.cDateCreate.Name = "cDateCreate";
            this.cDateCreate.ReadOnly = true;
            // 
            // cNameBlock
            // 
            this.cNameBlock.DataPropertyName = "nameBlock";
            this.cNameBlock.FillWeight = 22.47935F;
            this.cNameBlock.HeaderText = "Блок/Отдел";
            this.cNameBlock.Name = "cNameBlock";
            this.cNameBlock.ReadOnly = true;
            // 
            // cDescription
            // 
            this.cDescription.DataPropertyName = "Description";
            this.cDescription.FillWeight = 22.47935F;
            this.cDescription.HeaderText = "Описание";
            this.cDescription.Name = "cDescription";
            this.cDescription.ReadOnly = true;
            // 
            // cSumma
            // 
            this.cSumma.DataPropertyName = "Summa";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.cSumma.DefaultCellStyle = dataGridViewCellStyle3;
            this.cSumma.FillWeight = 22.47935F;
            this.cSumma.HeaderText = "Сумма СЗ";
            this.cSumma.Name = "cSumma";
            this.cSumma.ReadOnly = true;
            // 
            // Valuta
            // 
            this.Valuta.DataPropertyName = "Valuta";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Valuta.DefaultCellStyle = dataGridViewCellStyle4;
            this.Valuta.FillWeight = 18.70806F;
            this.Valuta.HeaderText = "Валюта";
            this.Valuta.Name = "Valuta";
            this.Valuta.ReadOnly = true;
            // 
            // cDataAdd
            // 
            this.cDataAdd.DataPropertyName = "DataSumma";
            this.cDataAdd.FillWeight = 22.47935F;
            this.cDataAdd.HeaderText = "Предполагаемая дата получения";
            this.cDataAdd.Name = "cDataAdd";
            this.cDataAdd.ReadOnly = true;
            // 
            // cNameStatus
            // 
            this.cNameStatus.DataPropertyName = "nameStatus";
            this.cNameStatus.FillWeight = 22.47935F;
            this.cNameStatus.HeaderText = "Статус";
            this.cNameStatus.Name = "cNameStatus";
            this.cNameStatus.ReadOnly = true;
            // 
            // cDateStatusChange
            // 
            this.cDateStatusChange.DataPropertyName = "DateStatusChange";
            this.cDateStatusChange.FillWeight = 22.47935F;
            this.cDateStatusChange.HeaderText = "Дата смены статуса";
            this.cDateStatusChange.Name = "cDateStatusChange";
            this.cDateStatusChange.ReadOnly = true;
            // 
            // cFIO
            // 
            this.cFIO.DataPropertyName = "FIO";
            this.cFIO.FillWeight = 22.47935F;
            this.cFIO.HeaderText = "ФИО изм. статус";
            this.cFIO.Name = "cFIO";
            this.cFIO.ReadOnly = true;
            // 
            // cScane
            // 
            this.cScane.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.cScane.DataPropertyName = "isScane";
            this.cScane.FillWeight = 530.8642F;
            this.cScane.HeaderText = "Скан";
            this.cScane.MinimumWidth = 75;
            this.cScane.Name = "cScane";
            this.cScane.ReadOnly = true;
            this.cScane.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.cScane.Width = 75;
            // 
            // col_object
            // 
            this.col_object.DataPropertyName = "id_Object";
            this.col_object.HeaderText = "id_Object";
            this.col_object.Name = "col_object";
            this.col_object.ReadOnly = true;
            this.col_object.Visible = false;
            // 
            // TypeServiceRecordOnTime
            // 
            this.TypeServiceRecordOnTime.DataPropertyName = "TypeServiceRecordOnTime";
            this.TypeServiceRecordOnTime.HeaderText = "TypeServiceRecordOnTime";
            this.TypeServiceRecordOnTime.Name = "TypeServiceRecordOnTime";
            this.TypeServiceRecordOnTime.ReadOnly = true;
            this.TypeServiceRecordOnTime.Visible = false;
            // 
            // Creator
            // 
            this.Creator.DataPropertyName = "id_Creator";
            this.Creator.HeaderText = "Creator";
            this.Creator.Name = "Creator";
            this.Creator.ReadOnly = true;
            this.Creator.Visible = false;
            // 
            // panel6
            // 
            this.panel6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel6.BackColor = System.Drawing.Color.Yellow;
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel6.Controls.Add(this.panel7);
            this.panel6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.panel6.Location = new System.Drawing.Point(331, 528);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(20, 20);
            this.panel6.TabIndex = 55;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.panel7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.panel7.Location = new System.Drawing.Point(1, 25);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(20, 20);
            this.panel7.TabIndex = 56;
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(361, 532);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(42, 13);
            this.label10.TabIndex = 56;
            this.label10.Text = "- Фонд";
            // 
            // btnPrintFond
            // 
            this.btnPrintFond.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPrintFond.Image = global::ServiceRecords.Properties.Resources.klpq_2511;
            this.btnPrintFond.Location = new System.Drawing.Point(787, 594);
            this.btnPrintFond.Name = "btnPrintFond";
            this.btnPrintFond.Size = new System.Drawing.Size(32, 32);
            this.btnPrintFond.TabIndex = 65;
            this.btnPrintFond.UseVisualStyleBackColor = true;
            this.btnPrintFond.Click += new System.EventHandler(this.btnPrintFond_Click);
            // 
            // chbReportPreMonth
            // 
            this.chbReportPreMonth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chbReportPreMonth.AutoSize = true;
            this.chbReportPreMonth.Checked = true;
            this.chbReportPreMonth.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbReportPreMonth.Location = new System.Drawing.Point(415, 530);
            this.chbReportPreMonth.Name = "chbReportPreMonth";
            this.chbReportPreMonth.Size = new System.Drawing.Size(225, 17);
            this.chbReportPreMonth.TabIndex = 66;
            this.chbReportPreMonth.Text = "предоставить отчет за прошлый месяц";
            this.chbReportPreMonth.UseVisualStyleBackColor = true;
            this.chbReportPreMonth.Click += new System.EventHandler(this.chbReportPreMonth_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1023, 638);
            this.Controls.Add(this.chbReportPreMonth);
            this.Controls.Add(this.btnPrintFond);
            this.Controls.Add(this.dgvMain);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.chbKD);
            this.Controls.Add(this.btnCheckReport);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.cmbObjects);
            this.Controls.Add(this.lblObject);
            this.Controls.Add(this.btnAccept);
            this.Controls.Add(this.btnRefuse);
            this.Controls.Add(this.chbUpdate);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.btReportNoteMoreDeps);
            this.Controls.Add(this.btReportNoteSingleDeps);
            this.Controls.Add(this.btListNotePeriod);
            this.Controls.Add(this.btChangeStatus);
            this.Controls.Add(this.btViewHistoryStatus);
            this.Controls.Add(this.btViewPayment);
            this.Controls.Add(this.btAccept);
            this.Controls.Add(this.btRefuse);
            this.Controls.Add(this.tbInfo);
            this.Controls.Add(this.tbNumber);
            this.Controls.Add(this.btView);
            this.Controls.Add(this.btUpdate);
            this.Controls.Add(this.btClose);
            this.Controls.Add(this.btAddBlock);
            this.Controls.Add(this.btEditBlock);
            this.Controls.Add(this.btDelBlock);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dtpStart);
            this.Controls.Add(this.dtpEnd);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbDeps);
            this.Controls.Add(this.cmbBlock);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.Shown += new System.EventHandler(this.frmMain_Shown);
            this.SizeChanged += new System.EventHandler(this.frmMain_SizeChanged);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.cmsWorking.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).EndInit();
            this.panel6.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox cmbDeps;
        private System.Windows.Forms.ComboBox cmbBlock;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpStart;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rbArhiv;
        private System.Windows.Forms.RadioButton rbWork;
        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.Button btAddBlock;
        private System.Windows.Forms.Button btEditBlock;
        private System.Windows.Forms.Button btDelBlock;
        private System.Windows.Forms.Button btView;
        private System.Windows.Forms.TextBox tbNumber;
        private System.Windows.Forms.TextBox tbInfo;
        private System.Windows.Forms.Button btUpdate;
        private System.Windows.Forms.Button btRefuse;
        private System.Windows.Forms.Button btAccept;
        private System.Windows.Forms.ContextMenuStrip cmsWorking;
        private System.Windows.Forms.ToolStripMenuItem cmsiTakeMoney;
        private System.Windows.Forms.ToolStripMenuItem cmsiDropeMoney;
        private System.Windows.Forms.Button btViewPayment;
        private System.Windows.Forms.Button btViewHistoryStatus;
        private System.Windows.Forms.Button btChangeStatus;
        private System.Windows.Forms.Button btListNotePeriod;
        private System.Windows.Forms.Button btReportNoteSingleDeps;
        private System.Windows.Forms.Button btReportNoteMoreDeps;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddDoc;
        private System.Windows.Forms.ToolStripMenuItem tsmiClose;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem помощьToolStripMenuItem;
        private System.Windows.Forms.CheckBox chbUpdate;
        private System.Windows.Forms.Timer timUpdate;
        private System.Windows.Forms.Button btnRefuse;
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.Label lblObject;
        private System.Windows.Forms.ComboBox cmbObjects;
        private System.Windows.Forms.ToolStripMenuItem tsmiSetReport;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnCheckReport;
        private System.Windows.Forms.CheckBox chbKD;
        private System.Windows.Forms.ToolStripMenuItem tsmiAnylSZ;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DataGridView dgvMain;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Enter;
        private System.Windows.Forms.DataGridViewTextBoxColumn cNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn cDateCreate;
        private System.Windows.Forms.DataGridViewTextBoxColumn cNameBlock;
        private System.Windows.Forms.DataGridViewTextBoxColumn cDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn cSumma;
        private System.Windows.Forms.DataGridViewTextBoxColumn Valuta;
        private System.Windows.Forms.DataGridViewTextBoxColumn cDataAdd;
        private System.Windows.Forms.DataGridViewTextBoxColumn cNameStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn cDateStatusChange;
        private System.Windows.Forms.DataGridViewTextBoxColumn cFIO;
        private System.Windows.Forms.DataGridViewCheckBoxColumn cScane;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_object;
        private System.Windows.Forms.DataGridViewTextBoxColumn TypeServiceRecordOnTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn Creator;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnPrintFond;
        private System.Windows.Forms.CheckBox chbReportPreMonth;
    }
}

