namespace ServiceRecords
{
    partial class frmCheckReport
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvReport = new System.Windows.Forms.DataGridView();
            this.colV = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_report = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Number = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Summa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Valuta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SummaAmountReceived = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SummaReport = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DebtReport = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DateEdit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.typeCashNonCash = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Mix = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btClose = new System.Windows.Forms.Button();
            this.dateTimeStart = new System.Windows.Forms.DateTimePicker();
            this.dateTimeEnd = new System.Windows.Forms.DateTimePicker();
            this.cmbStatusReport = new System.Windows.Forms.ComboBox();
            this.cmbDebt = new System.Windows.Forms.ComboBox();
            this.lbStatusReport = new System.Windows.Forms.Label();
            this.lbHasDebt = new System.Windows.Forms.Label();
            this.btnAccept = new System.Windows.Forms.Button();
            this.btnRefuse = new System.Windows.Forms.Button();
            this.btnPrintReport = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lbDebt = new System.Windows.Forms.Label();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tbNumber = new System.Windows.Forms.TextBox();
            this.tbDiscript = new System.Windows.Forms.TextBox();
            this.btViewHardwareList = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReport)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvReport
            // 
            this.dgvReport.AllowUserToAddRows = false;
            this.dgvReport.AllowUserToDeleteRows = false;
            this.dgvReport.AllowUserToResizeRows = false;
            this.dgvReport.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvReport.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvReport.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvReport.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReport.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colV,
            this.id,
            this.id_report,
            this.Number,
            this.Description,
            this.Summa,
            this.Valuta,
            this.SummaAmountReceived,
            this.SummaReport,
            this.DebtReport,
            this.DateEdit,
            this.typeCashNonCash,
            this.id_Status,
            this.Mix,
            this.cStatus});
            this.dgvReport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dgvReport.Location = new System.Drawing.Point(21, 97);
            this.dgvReport.MultiSelect = false;
            this.dgvReport.Name = "dgvReport";
            this.dgvReport.ReadOnly = true;
            this.dgvReport.RowHeadersVisible = false;
            this.dgvReport.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvReport.Size = new System.Drawing.Size(977, 342);
            this.dgvReport.TabIndex = 0;
            this.dgvReport.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvReport_CellContentClick);
            this.dgvReport.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvReport_CellDoubleClick);
            this.dgvReport.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dgvReport_ColumnWidthChanged);
            this.dgvReport.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dgvReport_RowPrePaint);
            this.dgvReport.SelectionChanged += new System.EventHandler(this.dgvReport_SelectionChanged);
            // 
            // colV
            // 
            this.colV.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colV.FalseValue = "false";
            this.colV.HeaderText = "V";
            this.colV.MinimumWidth = 35;
            this.colV.Name = "colV";
            this.colV.ReadOnly = true;
            this.colV.TrueValue = "true";
            this.colV.Width = 35;
            // 
            // id
            // 
            this.id.DataPropertyName = "id";
            this.id.HeaderText = "idSZ";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Visible = false;
            // 
            // id_report
            // 
            this.id_report.DataPropertyName = "id_report";
            this.id_report.HeaderText = "id_report";
            this.id_report.Name = "id_report";
            this.id_report.ReadOnly = true;
            this.id_report.Visible = false;
            // 
            // Number
            // 
            this.Number.DataPropertyName = "Number";
            this.Number.HeaderText = "№ СЗ";
            this.Number.MinimumWidth = 50;
            this.Number.Name = "Number";
            this.Number.ReadOnly = true;
            this.Number.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // Description
            // 
            this.Description.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Description.DataPropertyName = "Description";
            this.Description.HeaderText = "Описание";
            this.Description.MinimumWidth = 120;
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            this.Description.Width = 120;
            // 
            // Summa
            // 
            this.Summa.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Summa.DataPropertyName = "Summa";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.Format = "N2";
            dataGridViewCellStyle2.NullValue = "0";
            this.Summa.DefaultCellStyle = dataGridViewCellStyle2;
            this.Summa.HeaderText = "Сумма СЗ";
            this.Summa.MinimumWidth = 70;
            this.Summa.Name = "Summa";
            this.Summa.ReadOnly = true;
            this.Summa.Width = 70;
            // 
            // Valuta
            // 
            this.Valuta.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Valuta.DataPropertyName = "Valuta";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Valuta.DefaultCellStyle = dataGridViewCellStyle3;
            this.Valuta.HeaderText = "Валюта";
            this.Valuta.MinimumWidth = 50;
            this.Valuta.Name = "Valuta";
            this.Valuta.ReadOnly = true;
            this.Valuta.Width = 50;
            // 
            // SummaAmountReceived
            // 
            this.SummaAmountReceived.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.SummaAmountReceived.DataPropertyName = "sumGet";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.Format = "C2";
            dataGridViewCellStyle4.NullValue = "0";
            this.SummaAmountReceived.DefaultCellStyle = dataGridViewCellStyle4;
            this.SummaAmountReceived.HeaderText = "Взято";
            this.SummaAmountReceived.MinimumWidth = 70;
            this.SummaAmountReceived.Name = "SummaAmountReceived";
            this.SummaAmountReceived.ReadOnly = true;
            this.SummaAmountReceived.Width = 70;
            // 
            // SummaReport
            // 
            this.SummaReport.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.SummaReport.DataPropertyName = "SummaReport";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.Format = "C2";
            dataGridViewCellStyle5.NullValue = "0";
            this.SummaReport.DefaultCellStyle = dataGridViewCellStyle5;
            this.SummaReport.HeaderText = "Сумма отчета";
            this.SummaReport.MinimumWidth = 70;
            this.SummaReport.Name = "SummaReport";
            this.SummaReport.ReadOnly = true;
            this.SummaReport.Width = 70;
            // 
            // DebtReport
            // 
            this.DebtReport.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.DebtReport.DataPropertyName = "DebtReport";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.Format = "C2";
            dataGridViewCellStyle6.NullValue = "0";
            this.DebtReport.DefaultCellStyle = dataGridViewCellStyle6;
            this.DebtReport.HeaderText = "Долг";
            this.DebtReport.MinimumWidth = 70;
            this.DebtReport.Name = "DebtReport";
            this.DebtReport.ReadOnly = true;
            this.DebtReport.Width = 70;
            // 
            // DateEdit
            // 
            this.DateEdit.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.DateEdit.DataPropertyName = "DateEdit";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.Format = "d";
            dataGridViewCellStyle7.NullValue = null;
            this.DateEdit.DefaultCellStyle = dataGridViewCellStyle7;
            this.DateEdit.HeaderText = "Дата и время предоставления отчета";
            this.DateEdit.MinimumWidth = 103;
            this.DateEdit.Name = "DateEdit";
            this.DateEdit.ReadOnly = true;
            this.DateEdit.Width = 103;
            // 
            // typeCashNonCash
            // 
            this.typeCashNonCash.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.typeCashNonCash.DataPropertyName = "typeCashNonCash";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.typeCashNonCash.DefaultCellStyle = dataGridViewCellStyle8;
            this.typeCashNonCash.HeaderText = "Оплата";
            this.typeCashNonCash.MinimumWidth = 100;
            this.typeCashNonCash.Name = "typeCashNonCash";
            this.typeCashNonCash.ReadOnly = true;
            // 
            // id_Status
            // 
            this.id_Status.DataPropertyName = "id_Status";
            this.id_Status.HeaderText = "id_Status";
            this.id_Status.Name = "id_Status";
            this.id_Status.ReadOnly = true;
            this.id_Status.Visible = false;
            // 
            // Mix
            // 
            this.Mix.DataPropertyName = "Mix";
            this.Mix.HeaderText = "Mix";
            this.Mix.Name = "Mix";
            this.Mix.ReadOnly = true;
            this.Mix.Visible = false;
            // 
            // cStatus
            // 
            this.cStatus.DataPropertyName = "nameStatusReport";
            this.cStatus.HeaderText = "Статус";
            this.cStatus.Name = "cStatus";
            this.cStatus.ReadOnly = true;
            // 
            // btClose
            // 
            this.btClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btClose.Image = global::ServiceRecords.Properties.Resources.exit_8633;
            this.btClose.Location = new System.Drawing.Point(966, 456);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(32, 32);
            this.btClose.TabIndex = 40;
            this.btClose.UseVisualStyleBackColor = true;
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
            // 
            // dateTimeStart
            // 
            this.dateTimeStart.Location = new System.Drawing.Point(70, 11);
            this.dateTimeStart.Name = "dateTimeStart";
            this.dateTimeStart.Size = new System.Drawing.Size(137, 20);
            this.dateTimeStart.TabIndex = 60;
            // 
            // dateTimeEnd
            // 
            this.dateTimeEnd.Location = new System.Drawing.Point(70, 41);
            this.dateTimeEnd.Name = "dateTimeEnd";
            this.dateTimeEnd.Size = new System.Drawing.Size(137, 20);
            this.dateTimeEnd.TabIndex = 68;
            // 
            // cmbStatusReport
            // 
            this.cmbStatusReport.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatusReport.FormattingEnabled = true;
            this.cmbStatusReport.Location = new System.Drawing.Point(272, 11);
            this.cmbStatusReport.Name = "cmbStatusReport";
            this.cmbStatusReport.Size = new System.Drawing.Size(135, 21);
            this.cmbStatusReport.TabIndex = 69;
            this.cmbStatusReport.SelectionChangeCommitted += new System.EventHandler(this.cmbStatusReport_SelectionChangeCommitted);
            // 
            // cmbDebt
            // 
            this.cmbDebt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDebt.FormattingEnabled = true;
            this.cmbDebt.Location = new System.Drawing.Point(508, 11);
            this.cmbDebt.Name = "cmbDebt";
            this.cmbDebt.Size = new System.Drawing.Size(134, 21);
            this.cmbDebt.TabIndex = 70;
            this.cmbDebt.SelectedIndexChanged += new System.EventHandler(this.cmbDebt_SelectedIndexChanged);
            // 
            // lbStatusReport
            // 
            this.lbStatusReport.AutoSize = true;
            this.lbStatusReport.Location = new System.Drawing.Point(219, 15);
            this.lbStatusReport.Name = "lbStatusReport";
            this.lbStatusReport.Size = new System.Drawing.Size(41, 13);
            this.lbStatusReport.TabIndex = 71;
            this.lbStatusReport.Text = "Статус";
            // 
            // lbHasDebt
            // 
            this.lbHasDebt.AutoSize = true;
            this.lbHasDebt.Location = new System.Drawing.Point(420, 15);
            this.lbHasDebt.Name = "lbHasDebt";
            this.lbHasDebt.Size = new System.Drawing.Size(82, 13);
            this.lbHasDebt.TabIndex = 72;
            this.lbHasDebt.Text = "Наличие долга";
            // 
            // btnAccept
            // 
            this.btnAccept.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAccept.Enabled = false;
            this.btnAccept.Location = new System.Drawing.Point(404, 456);
            this.btnAccept.Margin = new System.Windows.Forms.Padding(2);
            this.btnAccept.MaximumSize = new System.Drawing.Size(85, 32);
            this.btnAccept.MinimumSize = new System.Drawing.Size(85, 32);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(85, 32);
            this.btnAccept.TabIndex = 73;
            this.btnAccept.Text = "Подтвердить";
            this.btnAccept.UseVisualStyleBackColor = true;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // btnRefuse
            // 
            this.btnRefuse.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefuse.Enabled = false;
            this.btnRefuse.Location = new System.Drawing.Point(319, 456);
            this.btnRefuse.Margin = new System.Windows.Forms.Padding(2);
            this.btnRefuse.MaximumSize = new System.Drawing.Size(81, 32);
            this.btnRefuse.MinimumSize = new System.Drawing.Size(81, 32);
            this.btnRefuse.Name = "btnRefuse";
            this.btnRefuse.Size = new System.Drawing.Size(81, 32);
            this.btnRefuse.TabIndex = 74;
            this.btnRefuse.Text = "Отклонить";
            this.btnRefuse.UseVisualStyleBackColor = true;
            this.btnRefuse.Click += new System.EventHandler(this.btnRefuse_Click);
            // 
            // btnPrintReport
            // 
            this.btnPrintReport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrintReport.Image = global::ServiceRecords.Properties.Resources.klpq_2511;
            this.btnPrintReport.Location = new System.Drawing.Point(928, 456);
            this.btnPrintReport.Name = "btnPrintReport";
            this.btnPrintReport.Size = new System.Drawing.Size(32, 32);
            this.btnPrintReport.TabIndex = 75;
            this.btnPrintReport.UseVisualStyleBackColor = true;
            this.btnPrintReport.Click += new System.EventHandler(this.btnPrintReport_Click);
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel4.Location = new System.Drawing.Point(21, 462);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(20, 20);
            this.panel4.TabIndex = 76;
            // 
            // lbDebt
            // 
            this.lbDebt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbDebt.AutoSize = true;
            this.lbDebt.Location = new System.Drawing.Point(47, 466);
            this.lbDebt.Name = "lbDebt";
            this.lbDebt.Size = new System.Drawing.Size(57, 13);
            this.lbDebt.TabIndex = 77;
            this.lbDebt.Text = "- Долг > 0";
            this.lbDebt.Click += new System.EventHandler(this.lbDebt_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdate.BackgroundImage = global::ServiceRecords.Properties.Resources.reload_8055;
            this.btnUpdate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnUpdate.Location = new System.Drawing.Point(966, 28);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(30, 30);
            this.btnUpdate.TabIndex = 78;
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(22, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 13);
            this.label5.TabIndex = 79;
            this.label5.Text = "Дата с";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(18, 45);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 13);
            this.label6.TabIndex = 80;
            this.label6.Text = "Дата по";
            // 
            // tbNumber
            // 
            this.tbNumber.Location = new System.Drawing.Point(21, 71);
            this.tbNumber.Name = "tbNumber";
            this.tbNumber.Size = new System.Drawing.Size(100, 20);
            this.tbNumber.TabIndex = 81;
            this.tbNumber.TextChanged += new System.EventHandler(this.tbNumber_TextChanged);
            // 
            // tbDiscript
            // 
            this.tbDiscript.Location = new System.Drawing.Point(127, 71);
            this.tbDiscript.Name = "tbDiscript";
            this.tbDiscript.Size = new System.Drawing.Size(100, 20);
            this.tbDiscript.TabIndex = 81;
            this.tbDiscript.TextChanged += new System.EventHandler(this.tbNumber_TextChanged);
            // 
            // btViewHardwareList
            // 
            this.btViewHardwareList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btViewHardwareList.Image = global::ServiceRecords.Properties.Resources.screen_monitor_computer1;
            this.btViewHardwareList.Location = new System.Drawing.Point(856, 456);
            this.btViewHardwareList.Name = "btViewHardwareList";
            this.btViewHardwareList.Size = new System.Drawing.Size(32, 32);
            this.btViewHardwareList.TabIndex = 82;
            this.btViewHardwareList.UseVisualStyleBackColor = true;
            this.btViewHardwareList.Click += new System.EventHandler(this.btViewHardwareList_Click);
            // 
            // frmCheckReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1010, 496);
            this.ControlBox = false;
            this.Controls.Add(this.btViewHardwareList);
            this.Controls.Add(this.tbDiscript);
            this.Controls.Add(this.tbNumber);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.lbDebt);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.btnPrintReport);
            this.Controls.Add(this.btnRefuse);
            this.Controls.Add(this.btnAccept);
            this.Controls.Add(this.lbHasDebt);
            this.Controls.Add(this.lbStatusReport);
            this.Controls.Add(this.cmbDebt);
            this.Controls.Add(this.cmbStatusReport);
            this.Controls.Add(this.dateTimeEnd);
            this.Controls.Add(this.dateTimeStart);
            this.Controls.Add(this.btClose);
            this.Controls.Add(this.dgvReport);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCheckReport";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Проверка отчетов";
            this.Load += new System.EventHandler(this.frmCheckReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvReport)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvReport;
        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.DateTimePicker dateTimeStart;
        private System.Windows.Forms.DateTimePicker dateTimeEnd;
        private System.Windows.Forms.ComboBox cmbStatusReport;
        private System.Windows.Forms.ComboBox cmbDebt;
        private System.Windows.Forms.Label lbStatusReport;
        private System.Windows.Forms.Label lbHasDebt;
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.Button btnRefuse;
        private System.Windows.Forms.Button btnPrintReport;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label lbDebt;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbNumber;
        private System.Windows.Forms.TextBox tbDiscript;
        private System.Windows.Forms.Button btViewHardwareList;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colV;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_report;
        private System.Windows.Forms.DataGridViewTextBoxColumn Number;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn Summa;
        private System.Windows.Forms.DataGridViewTextBoxColumn Valuta;
        private System.Windows.Forms.DataGridViewTextBoxColumn SummaAmountReceived;
        private System.Windows.Forms.DataGridViewTextBoxColumn SummaReport;
        private System.Windows.Forms.DataGridViewTextBoxColumn DebtReport;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateEdit;
        private System.Windows.Forms.DataGridViewTextBoxColumn typeCashNonCash;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn Mix;
        private System.Windows.Forms.DataGridViewTextBoxColumn cStatus;
    }
}