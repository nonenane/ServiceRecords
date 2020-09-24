namespace ServiceRecords.settings
{
    partial class frmSettings
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tbGlobal = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.dgvDepsSettings = new System.Windows.Forms.DataGridView();
            this.cNameDepsProg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cVDeps = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.rbNotNeed = new System.Windows.Forms.RadioButton();
            this.rbNeed = new System.Windows.Forms.RadioButton();
            this.nudTimeSafe = new System.Windows.Forms.NumericUpDown();
            this.dtpLimit = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbLink = new System.Windows.Forms.TabPage();
            this.btAdd = new System.Windows.Forms.Button();
            this.btDel = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tbDeps = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbRoute = new System.Windows.Forms.ComboBox();
            this.dgvDepVsRoute = new System.Windows.Forms.DataGridView();
            this.cDeps = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cRouteDep = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbDeps = new System.Windows.Forms.ComboBox();
            this.dgvRoute = new System.Windows.Forms.DataGridView();
            this.cRoute = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbBlockDeps = new System.Windows.Forms.TabPage();
            this.btAddBlock = new System.Windows.Forms.Button();
            this.btEditBlock = new System.Windows.Forms.Button();
            this.btDelBlock = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dgvBlokVsDeps = new System.Windows.Forms.DataGridView();
            this.cBlockVsDeps = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbObjectsHandbook = new System.Windows.Forms.TabPage();
            this.picbDontWork = new System.Windows.Forms.PictureBox();
            this.lbDontWork = new System.Windows.Forms.Label();
            this.btnDeleteObject = new System.Windows.Forms.Button();
            this.btnEditObject = new System.Windows.Forms.Button();
            this.btnAddObject = new System.Windows.Forms.Button();
            this.cbVievWorkObjects = new System.Windows.Forms.CheckBox();
            this.tbSearchObject = new System.Windows.Forms.TextBox();
            this.dgvObjectsHandbook = new System.Windows.Forms.DataGridView();
            this.id_Object = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colObjects = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.is_Active = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.count = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btSelect = new System.Windows.Forms.Button();
            this.btClose = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tbGlobal.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDepsSettings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTimeSafe)).BeginInit();
            this.tbLink.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDepVsRoute)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRoute)).BeginInit();
            this.tbBlockDeps.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBlokVsDeps)).BeginInit();
            this.tbObjectsHandbook.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picbDontWork)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvObjectsHandbook)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tbGlobal);
            this.tabControl1.Controls.Add(this.tbLink);
            this.tabControl1.Controls.Add(this.tbBlockDeps);
            this.tabControl1.Controls.Add(this.tbObjectsHandbook);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(816, 504);
            this.tabControl1.TabIndex = 0;
            // 
            // tbGlobal
            // 
            this.tbGlobal.Controls.Add(this.groupBox4);
            this.tbGlobal.Controls.Add(this.rbNotNeed);
            this.tbGlobal.Controls.Add(this.rbNeed);
            this.tbGlobal.Controls.Add(this.nudTimeSafe);
            this.tbGlobal.Controls.Add(this.dtpLimit);
            this.tbGlobal.Controls.Add(this.label3);
            this.tbGlobal.Controls.Add(this.label4);
            this.tbGlobal.Controls.Add(this.label2);
            this.tbGlobal.Controls.Add(this.label1);
            this.tbGlobal.Location = new System.Drawing.Point(4, 22);
            this.tbGlobal.Name = "tbGlobal";
            this.tbGlobal.Padding = new System.Windows.Forms.Padding(3);
            this.tbGlobal.Size = new System.Drawing.Size(808, 478);
            this.tbGlobal.TabIndex = 0;
            this.tbGlobal.Text = "Общие";
            this.tbGlobal.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.dgvDepsSettings);
            this.groupBox4.Location = new System.Drawing.Point(386, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(411, 460);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Выбор отдела для ...?";
            // 
            // dgvDepsSettings
            // 
            this.dgvDepsSettings.AllowUserToAddRows = false;
            this.dgvDepsSettings.AllowUserToDeleteRows = false;
            this.dgvDepsSettings.AllowUserToResizeRows = false;
            this.dgvDepsSettings.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDepsSettings.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDepsSettings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDepsSettings.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cNameDepsProg,
            this.cVDeps});
            this.dgvDepsSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDepsSettings.Location = new System.Drawing.Point(3, 16);
            this.dgvDepsSettings.MultiSelect = false;
            this.dgvDepsSettings.Name = "dgvDepsSettings";
            this.dgvDepsSettings.RowHeadersVisible = false;
            this.dgvDepsSettings.Size = new System.Drawing.Size(405, 441);
            this.dgvDepsSettings.TabIndex = 1;
            // 
            // cNameDepsProg
            // 
            this.cNameDepsProg.DataPropertyName = "cName";
            this.cNameDepsProg.HeaderText = "Отдел";
            this.cNameDepsProg.Name = "cNameDepsProg";
            // 
            // cVDeps
            // 
            this.cVDeps.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.cVDeps.DataPropertyName = "isSelect";
            this.cVDeps.HeaderText = "V";
            this.cVDeps.MinimumWidth = 45;
            this.cVDeps.Name = "cVDeps";
            this.cVDeps.Width = 45;
            // 
            // rbNotNeed
            // 
            this.rbNotNeed.AutoSize = true;
            this.rbNotNeed.Checked = true;
            this.rbNotNeed.Location = new System.Drawing.Point(258, 74);
            this.rbNotNeed.Name = "rbNotNeed";
            this.rbNotNeed.Size = new System.Drawing.Size(108, 17);
            this.rbNotNeed.TabIndex = 3;
            this.rbNotNeed.TabStop = true;
            this.rbNotNeed.Text = "необязательное";
            this.rbNotNeed.UseVisualStyleBackColor = true;
            this.rbNotNeed.Click += new System.EventHandler(this.rbNotNeed_Click);
            // 
            // rbNeed
            // 
            this.rbNeed.AutoSize = true;
            this.rbNeed.Location = new System.Drawing.Point(258, 97);
            this.rbNeed.Name = "rbNeed";
            this.rbNeed.Size = new System.Drawing.Size(96, 17);
            this.rbNeed.TabIndex = 3;
            this.rbNeed.Text = "обязательное";
            this.rbNeed.UseVisualStyleBackColor = true;
            this.rbNeed.Click += new System.EventHandler(this.rbNeed_Click);
            // 
            // nudTimeSafe
            // 
            this.nudTimeSafe.Location = new System.Drawing.Point(258, 41);
            this.nudTimeSafe.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.nudTimeSafe.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nudTimeSafe.Name = "nudTimeSafe";
            this.nudTimeSafe.ReadOnly = true;
            this.nudTimeSafe.Size = new System.Drawing.Size(85, 20);
            this.nudTimeSafe.TabIndex = 2;
            this.nudTimeSafe.Value = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.nudTimeSafe.ValueChanged += new System.EventHandler(this.nudTimeSafe_ValueChanged);
            this.nudTimeSafe.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.nudTimeSafe_KeyPress);
            // 
            // dtpLimit
            // 
            this.dtpLimit.CustomFormat = "HH:mm";
            this.dtpLimit.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpLimit.Location = new System.Drawing.Point(258, 12);
            this.dtpLimit.Name = "dtpLimit";
            this.dtpLimit.ShowUpDown = true;
            this.dtpLimit.Size = new System.Drawing.Size(85, 20);
            this.dtpLimit.TabIndex = 1;
            this.dtpLimit.ValueChanged += new System.EventHandler(this.dtpLimit_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(224, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Прикрепление файлов \"Отчет по ДС к СЗ\"";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(349, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "дней";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(74, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(174, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Срок хранения обработанных СЗ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(224, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ограничение по времени на получение ДС";
            // 
            // tbLink
            // 
            this.tbLink.Controls.Add(this.btAdd);
            this.tbLink.Controls.Add(this.btDel);
            this.tbLink.Controls.Add(this.groupBox2);
            this.tbLink.Controls.Add(this.groupBox1);
            this.tbLink.Location = new System.Drawing.Point(4, 22);
            this.tbLink.Name = "tbLink";
            this.tbLink.Padding = new System.Windows.Forms.Padding(3);
            this.tbLink.Size = new System.Drawing.Size(808, 478);
            this.tbLink.TabIndex = 1;
            this.tbLink.Text = "Связь маршрутов и отделов";
            this.tbLink.UseVisualStyleBackColor = true;
            // 
            // btAdd
            // 
            this.btAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btAdd.Image = global::ServiceRecords.Properties.Resources.old_edit_redo;
            this.btAdd.Location = new System.Drawing.Point(358, 165);
            this.btAdd.Name = "btAdd";
            this.btAdd.Size = new System.Drawing.Size(32, 32);
            this.btAdd.TabIndex = 11;
            this.btAdd.UseVisualStyleBackColor = true;
            this.btAdd.Click += new System.EventHandler(this.btAdd_Click);
            // 
            // btDel
            // 
            this.btDel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btDel.Image = global::ServiceRecords.Properties.Resources.old_edit_undo;
            this.btDel.Location = new System.Drawing.Point(358, 118);
            this.btDel.Name = "btDel";
            this.btDel.Size = new System.Drawing.Size(32, 32);
            this.btDel.TabIndex = 11;
            this.btDel.UseVisualStyleBackColor = true;
            this.btDel.Click += new System.EventHandler(this.btDel_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tbDeps);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.cmbRoute);
            this.groupBox2.Controls.Add(this.dgvDepVsRoute);
            this.groupBox2.Location = new System.Drawing.Point(411, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(373, 429);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Связанные маршруты и отделы";
            // 
            // tbDeps
            // 
            this.tbDeps.Location = new System.Drawing.Point(6, 44);
            this.tbDeps.MaxLength = 50;
            this.tbDeps.Name = "tbDeps";
            this.tbDeps.Size = new System.Drawing.Size(179, 20);
            this.tbDeps.TabIndex = 15;
            this.tbDeps.TextChanged += new System.EventHandler(this.tbDeps_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(130, 18);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Маршрут:";
            // 
            // cmbRoute
            // 
            this.cmbRoute.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRoute.FormattingEnabled = true;
            this.cmbRoute.Location = new System.Drawing.Point(191, 14);
            this.cmbRoute.Name = "cmbRoute";
            this.cmbRoute.Size = new System.Drawing.Size(176, 21);
            this.cmbRoute.TabIndex = 13;
            this.cmbRoute.SelectionChangeCommitted += new System.EventHandler(this.cmbRoute_SelectionChangeCommitted);
            // 
            // dgvDepVsRoute
            // 
            this.dgvDepVsRoute.AllowUserToAddRows = false;
            this.dgvDepVsRoute.AllowUserToDeleteRows = false;
            this.dgvDepVsRoute.AllowUserToResizeRows = false;
            this.dgvDepVsRoute.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDepVsRoute.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvDepVsRoute.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDepVsRoute.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cDeps,
            this.cRouteDep});
            this.dgvDepVsRoute.Location = new System.Drawing.Point(6, 70);
            this.dgvDepVsRoute.MultiSelect = false;
            this.dgvDepVsRoute.Name = "dgvDepVsRoute";
            this.dgvDepVsRoute.ReadOnly = true;
            this.dgvDepVsRoute.RowHeadersVisible = false;
            this.dgvDepVsRoute.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDepVsRoute.Size = new System.Drawing.Size(361, 353);
            this.dgvDepVsRoute.TabIndex = 0;
            // 
            // cDeps
            // 
            this.cDeps.DataPropertyName = "name";
            this.cDeps.HeaderText = "Отдел";
            this.cDeps.Name = "cDeps";
            this.cDeps.ReadOnly = true;
            // 
            // cRouteDep
            // 
            this.cRouteDep.DataPropertyName = "cName";
            this.cRouteDep.HeaderText = "Маршрут";
            this.cRouteDep.Name = "cRouteDep";
            this.cRouteDep.ReadOnly = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cmbDeps);
            this.groupBox1.Controls.Add(this.dgvRoute);
            this.groupBox1.Location = new System.Drawing.Point(8, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(339, 233);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Доступные маршруты";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Отдел:";
            // 
            // cmbDeps
            // 
            this.cmbDeps.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDeps.FormattingEnabled = true;
            this.cmbDeps.Location = new System.Drawing.Point(62, 18);
            this.cmbDeps.Name = "cmbDeps";
            this.cmbDeps.Size = new System.Drawing.Size(271, 21);
            this.cmbDeps.TabIndex = 1;
            // 
            // dgvRoute
            // 
            this.dgvRoute.AllowUserToAddRows = false;
            this.dgvRoute.AllowUserToDeleteRows = false;
            this.dgvRoute.AllowUserToResizeRows = false;
            this.dgvRoute.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvRoute.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvRoute.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRoute.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cRoute});
            this.dgvRoute.Location = new System.Drawing.Point(6, 56);
            this.dgvRoute.MultiSelect = false;
            this.dgvRoute.Name = "dgvRoute";
            this.dgvRoute.ReadOnly = true;
            this.dgvRoute.RowHeadersVisible = false;
            this.dgvRoute.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRoute.Size = new System.Drawing.Size(327, 171);
            this.dgvRoute.TabIndex = 0;
            // 
            // cRoute
            // 
            this.cRoute.DataPropertyName = "cName";
            this.cRoute.HeaderText = "Маршруты";
            this.cRoute.Name = "cRoute";
            this.cRoute.ReadOnly = true;
            // 
            // tbBlockDeps
            // 
            this.tbBlockDeps.Controls.Add(this.btAddBlock);
            this.tbBlockDeps.Controls.Add(this.btEditBlock);
            this.tbBlockDeps.Controls.Add(this.btDelBlock);
            this.tbBlockDeps.Controls.Add(this.groupBox3);
            this.tbBlockDeps.Location = new System.Drawing.Point(4, 22);
            this.tbBlockDeps.Name = "tbBlockDeps";
            this.tbBlockDeps.Size = new System.Drawing.Size(808, 478);
            this.tbBlockDeps.TabIndex = 2;
            this.tbBlockDeps.Text = "Настройка блоков/отделов";
            this.tbBlockDeps.UseVisualStyleBackColor = true;
            // 
            // btAddBlock
            // 
            this.btAddBlock.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btAddBlock.Image = global::ServiceRecords.Properties.Resources.document_add;
            this.btAddBlock.Location = new System.Drawing.Point(344, 433);
            this.btAddBlock.Name = "btAddBlock";
            this.btAddBlock.Size = new System.Drawing.Size(32, 32);
            this.btAddBlock.TabIndex = 18;
            this.btAddBlock.UseVisualStyleBackColor = true;
            this.btAddBlock.Click += new System.EventHandler(this.btAddBlock_Click);
            // 
            // btEditBlock
            // 
            this.btEditBlock.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btEditBlock.Image = global::ServiceRecords.Properties.Resources.edit;
            this.btEditBlock.Location = new System.Drawing.Point(382, 433);
            this.btEditBlock.Name = "btEditBlock";
            this.btEditBlock.Size = new System.Drawing.Size(32, 32);
            this.btEditBlock.TabIndex = 18;
            this.btEditBlock.UseVisualStyleBackColor = true;
            this.btEditBlock.Click += new System.EventHandler(this.btEditBlock_Click);
            // 
            // btDelBlock
            // 
            this.btDelBlock.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btDelBlock.Image = global::ServiceRecords.Properties.Resources.document_delete;
            this.btDelBlock.Location = new System.Drawing.Point(420, 433);
            this.btDelBlock.Name = "btDelBlock";
            this.btDelBlock.Size = new System.Drawing.Size(32, 32);
            this.btDelBlock.TabIndex = 18;
            this.btDelBlock.UseVisualStyleBackColor = true;
            this.btDelBlock.Click += new System.EventHandler(this.btDelBlock_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dgvBlokVsDeps);
            this.groupBox3.Location = new System.Drawing.Point(8, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(450, 415);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Используемые блоки/отделы";
            // 
            // dgvBlokVsDeps
            // 
            this.dgvBlokVsDeps.AllowUserToAddRows = false;
            this.dgvBlokVsDeps.AllowUserToDeleteRows = false;
            this.dgvBlokVsDeps.AllowUserToResizeRows = false;
            this.dgvBlokVsDeps.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvBlokVsDeps.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvBlokVsDeps.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBlokVsDeps.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cBlockVsDeps});
            this.dgvBlokVsDeps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvBlokVsDeps.Location = new System.Drawing.Point(3, 16);
            this.dgvBlokVsDeps.MultiSelect = false;
            this.dgvBlokVsDeps.Name = "dgvBlokVsDeps";
            this.dgvBlokVsDeps.ReadOnly = true;
            this.dgvBlokVsDeps.RowHeadersVisible = false;
            this.dgvBlokVsDeps.Size = new System.Drawing.Size(444, 396);
            this.dgvBlokVsDeps.TabIndex = 0;
            // 
            // cBlockVsDeps
            // 
            this.cBlockVsDeps.DataPropertyName = "name";
            this.cBlockVsDeps.HeaderText = "Блоки/отделы";
            this.cBlockVsDeps.Name = "cBlockVsDeps";
            this.cBlockVsDeps.ReadOnly = true;
            // 
            // tbObjectsHandbook
            // 
            this.tbObjectsHandbook.Controls.Add(this.picbDontWork);
            this.tbObjectsHandbook.Controls.Add(this.lbDontWork);
            this.tbObjectsHandbook.Controls.Add(this.btnDeleteObject);
            this.tbObjectsHandbook.Controls.Add(this.btnEditObject);
            this.tbObjectsHandbook.Controls.Add(this.btnAddObject);
            this.tbObjectsHandbook.Controls.Add(this.cbVievWorkObjects);
            this.tbObjectsHandbook.Controls.Add(this.tbSearchObject);
            this.tbObjectsHandbook.Controls.Add(this.dgvObjectsHandbook);
            this.tbObjectsHandbook.Location = new System.Drawing.Point(4, 22);
            this.tbObjectsHandbook.Name = "tbObjectsHandbook";
            this.tbObjectsHandbook.Padding = new System.Windows.Forms.Padding(3);
            this.tbObjectsHandbook.Size = new System.Drawing.Size(808, 478);
            this.tbObjectsHandbook.TabIndex = 3;
            this.tbObjectsHandbook.Text = "Справочник объектов";
            this.tbObjectsHandbook.UseVisualStyleBackColor = true;
            // 
            // picbDontWork
            // 
            this.picbDontWork.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.picbDontWork.Location = new System.Drawing.Point(199, 449);
            this.picbDontWork.Margin = new System.Windows.Forms.Padding(2);
            this.picbDontWork.Name = "picbDontWork";
            this.picbDontWork.Size = new System.Drawing.Size(14, 14);
            this.picbDontWork.TabIndex = 17;
            this.picbDontWork.TabStop = false;
            // 
            // lbDontWork
            // 
            this.lbDontWork.AutoSize = true;
            this.lbDontWork.Location = new System.Drawing.Point(217, 449);
            this.lbDontWork.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbDontWork.Name = "lbDontWork";
            this.lbDontWork.Size = new System.Drawing.Size(94, 13);
            this.lbDontWork.TabIndex = 16;
            this.lbDontWork.Text = "- недействующие";
            // 
            // btnDeleteObject
            // 
            this.btnDeleteObject.BackgroundImage = global::ServiceRecords.Properties.Resources.document_delete;
            this.btnDeleteObject.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnDeleteObject.Location = new System.Drawing.Point(566, 442);
            this.btnDeleteObject.Margin = new System.Windows.Forms.Padding(2);
            this.btnDeleteObject.Name = "btnDeleteObject";
            this.btnDeleteObject.Size = new System.Drawing.Size(30, 32);
            this.btnDeleteObject.TabIndex = 14;
            this.btnDeleteObject.UseVisualStyleBackColor = true;
            this.btnDeleteObject.Click += new System.EventHandler(this.btnDeleteObject_Click);
            // 
            // btnEditObject
            // 
            this.btnEditObject.BackgroundImage = global::ServiceRecords.Properties.Resources.edit_1761;
            this.btnEditObject.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnEditObject.Location = new System.Drawing.Point(532, 442);
            this.btnEditObject.Margin = new System.Windows.Forms.Padding(2);
            this.btnEditObject.Name = "btnEditObject";
            this.btnEditObject.Size = new System.Drawing.Size(30, 32);
            this.btnEditObject.TabIndex = 13;
            this.btnEditObject.UseVisualStyleBackColor = true;
            this.btnEditObject.Click += new System.EventHandler(this.btnEditObject_Click);
            // 
            // btnAddObject
            // 
            this.btnAddObject.BackgroundImage = global::ServiceRecords.Properties.Resources.document_add;
            this.btnAddObject.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnAddObject.Location = new System.Drawing.Point(498, 442);
            this.btnAddObject.Margin = new System.Windows.Forms.Padding(2);
            this.btnAddObject.Name = "btnAddObject";
            this.btnAddObject.Size = new System.Drawing.Size(30, 32);
            this.btnAddObject.TabIndex = 12;
            this.btnAddObject.UseVisualStyleBackColor = true;
            this.btnAddObject.Click += new System.EventHandler(this.btnAddObject_Click);
            // 
            // cbVievWorkObjects
            // 
            this.cbVievWorkObjects.AutoSize = true;
            this.cbVievWorkObjects.Location = new System.Drawing.Point(177, 449);
            this.cbVievWorkObjects.Margin = new System.Windows.Forms.Padding(2);
            this.cbVievWorkObjects.Name = "cbVievWorkObjects";
            this.cbVievWorkObjects.Size = new System.Drawing.Size(15, 14);
            this.cbVievWorkObjects.TabIndex = 11;
            this.cbVievWorkObjects.UseVisualStyleBackColor = true;
            this.cbVievWorkObjects.CheckedChanged += new System.EventHandler(this.cbVievWorkObjects_CheckedChanged);
            // 
            // tbSearchObject
            // 
            this.tbSearchObject.Location = new System.Drawing.Point(177, 26);
            this.tbSearchObject.Margin = new System.Windows.Forms.Padding(2);
            this.tbSearchObject.Name = "tbSearchObject";
            this.tbSearchObject.Size = new System.Drawing.Size(419, 20);
            this.tbSearchObject.TabIndex = 10;
            this.tbSearchObject.TextChanged += new System.EventHandler(this.tbSearchObject_TextChanged_1);
            // 
            // dgvObjectsHandbook
            // 
            this.dgvObjectsHandbook.AllowUserToAddRows = false;
            this.dgvObjectsHandbook.AllowUserToDeleteRows = false;
            this.dgvObjectsHandbook.AllowUserToResizeRows = false;
            this.dgvObjectsHandbook.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvObjectsHandbook.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id_Object,
            this.colObjects,
            this.is_Active,
            this.count});
            this.dgvObjectsHandbook.Location = new System.Drawing.Point(177, 50);
            this.dgvObjectsHandbook.Margin = new System.Windows.Forms.Padding(2);
            this.dgvObjectsHandbook.MultiSelect = false;
            this.dgvObjectsHandbook.Name = "dgvObjectsHandbook";
            this.dgvObjectsHandbook.ReadOnly = true;
            this.dgvObjectsHandbook.RowHeadersVisible = false;
            this.dgvObjectsHandbook.RowTemplate.Height = 24;
            this.dgvObjectsHandbook.Size = new System.Drawing.Size(419, 385);
            this.dgvObjectsHandbook.TabIndex = 9;
            this.dgvObjectsHandbook.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dgvObjectsHandbook_RowPrePaint);
            // 
            // id_Object
            // 
            this.id_Object.DataPropertyName = "id_Object";
            this.id_Object.HeaderText = "id_Object";
            this.id_Object.Name = "id_Object";
            this.id_Object.ReadOnly = true;
            this.id_Object.Visible = false;
            // 
            // colObjects
            // 
            this.colObjects.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colObjects.DataPropertyName = "name_Object";
            this.colObjects.HeaderText = "                                          Наименование объекта";
            this.colObjects.Name = "colObjects";
            this.colObjects.ReadOnly = true;
            // 
            // is_Active
            // 
            this.is_Active.DataPropertyName = "is_Active";
            this.is_Active.HeaderText = "is_Active";
            this.is_Active.Name = "is_Active";
            this.is_Active.ReadOnly = true;
            this.is_Active.Visible = false;
            // 
            // count
            // 
            this.count.DataPropertyName = "count";
            this.count.HeaderText = "count";
            this.count.Name = "count";
            this.count.ReadOnly = true;
            this.count.Visible = false;
            // 
            // btSelect
            // 
            this.btSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btSelect.Image = global::ServiceRecords.Properties.Resources.save_edit;
            this.btSelect.Location = new System.Drawing.Point(730, 510);
            this.btSelect.Name = "btSelect";
            this.btSelect.Size = new System.Drawing.Size(32, 32);
            this.btSelect.TabIndex = 16;
            this.btSelect.UseVisualStyleBackColor = true;
            this.btSelect.Click += new System.EventHandler(this.btSelect_Click);
            // 
            // btClose
            // 
            this.btClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btClose.Image = global::ServiceRecords.Properties.Resources.exit_8633;
            this.btClose.Location = new System.Drawing.Point(772, 510);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(32, 32);
            this.btClose.TabIndex = 17;
            this.btClose.UseVisualStyleBackColor = true;
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
            // 
            // frmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(816, 549);
            this.Controls.Add(this.btSelect);
            this.Controls.Add(this.btClose);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Настройки";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSettings_FormClosing);
            this.Load += new System.EventHandler(this.frmSettings_Load);
            this.tabControl1.ResumeLayout(false);
            this.tbGlobal.ResumeLayout(false);
            this.tbGlobal.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDepsSettings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTimeSafe)).EndInit();
            this.tbLink.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDepVsRoute)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRoute)).EndInit();
            this.tbBlockDeps.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBlokVsDeps)).EndInit();
            this.tbObjectsHandbook.ResumeLayout(false);
            this.tbObjectsHandbook.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picbDontWork)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvObjectsHandbook)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tbGlobal;
        private System.Windows.Forms.TabPage tbLink;
        private System.Windows.Forms.Button btSelect;
        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.TabPage tbBlockDeps;
        private System.Windows.Forms.RadioButton rbNotNeed;
        private System.Windows.Forms.RadioButton rbNeed;
        private System.Windows.Forms.NumericUpDown nudTimeSafe;
        private System.Windows.Forms.DateTimePicker dtpLimit;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgvDepVsRoute;
        private System.Windows.Forms.DataGridView dgvRoute;
        private System.Windows.Forms.Button btAdd;
        private System.Windows.Forms.Button btDel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbDeps;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbRoute;
        private System.Windows.Forms.TextBox tbDeps;
        private System.Windows.Forms.DataGridViewTextBoxColumn cRoute;
        private System.Windows.Forms.DataGridViewTextBoxColumn cDeps;
        private System.Windows.Forms.DataGridViewTextBoxColumn cRouteDep;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dgvBlokVsDeps;
        private System.Windows.Forms.Button btAddBlock;
        private System.Windows.Forms.Button btEditBlock;
        private System.Windows.Forms.Button btDelBlock;
        private System.Windows.Forms.DataGridViewTextBoxColumn cBlockVsDeps;
        private System.Windows.Forms.TabPage tbObjectsHandbook;
        private System.Windows.Forms.PictureBox picbDontWork;
        private System.Windows.Forms.Label lbDontWork;
        private System.Windows.Forms.Button btnDeleteObject;
        private System.Windows.Forms.Button btnEditObject;
        private System.Windows.Forms.Button btnAddObject;
        private System.Windows.Forms.CheckBox cbVievWorkObjects;
        private System.Windows.Forms.TextBox tbSearchObject;
        private System.Windows.Forms.DataGridView dgvObjectsHandbook;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_Object;
        private System.Windows.Forms.DataGridViewTextBoxColumn colObjects;
        private System.Windows.Forms.DataGridViewTextBoxColumn is_Active;
        private System.Windows.Forms.DataGridViewTextBoxColumn count;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.DataGridView dgvDepsSettings;
        private System.Windows.Forms.DataGridViewTextBoxColumn cNameDepsProg;
        private System.Windows.Forms.DataGridViewCheckBoxColumn cVDeps;
    }
}