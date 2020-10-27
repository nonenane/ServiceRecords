namespace ReportPays
{
    partial class frmReport
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
            this.btClose = new System.Windows.Forms.Button();
            this.btPrint = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.cmbObject = new System.Windows.Forms.ComboBox();
            this.lObject = new System.Windows.Forms.Label();
            this.lDepsBonus = new System.Windows.Forms.Label();
            this.cmbDepsBonus = new System.Windows.Forms.ComboBox();
            this.chbShowUser = new System.Windows.Forms.CheckBox();
            this.lType = new System.Windows.Forms.Label();
            this.lDeps = new System.Windows.Forms.Label();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.cmbDeps = new System.Windows.Forms.ComboBox();
            this.lTypeDoc = new System.Windows.Forms.Label();
            this.cmbTypeDoc = new System.Windows.Forms.ComboBox();
            this.dgvStatus = new System.Windows.Forms.DataGridView();
            this.lStatus = new System.Windows.Forms.Label();
            this.cName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cV = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStatus)).BeginInit();
            this.SuspendLayout();
            // 
            // btClose
            // 
            this.btClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btClose.Image = global::ReportPays.Properties.Resources.door_out;
            this.btClose.Location = new System.Drawing.Point(435, 406);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(32, 32);
            this.btClose.TabIndex = 0;
            this.btClose.UseVisualStyleBackColor = true;
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
            // 
            // btPrint
            // 
            this.btPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btPrint.Image = global::ReportPays.Properties.Resources.gtk_print_report;
            this.btPrint.Location = new System.Drawing.Point(397, 406);
            this.btPrint.Name = "btPrint";
            this.btPrint.Size = new System.Drawing.Size(32, 32);
            this.btPrint.TabIndex = 0;
            this.btPrint.UseVisualStyleBackColor = true;
            this.btPrint.Click += new System.EventHandler(this.btPrint_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Период с";
            // 
            // dtpStart
            // 
            this.dtpStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStart.Location = new System.Drawing.Point(74, 13);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size(91, 20);
            this.dtpStart.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(171, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(19, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "по";
            // 
            // dtpEnd
            // 
            this.dtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEnd.Location = new System.Drawing.Point(196, 13);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(91, 20);
            this.dtpEnd.TabIndex = 2;
            // 
            // cmbObject
            // 
            this.cmbObject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbObject.FormattingEnabled = true;
            this.cmbObject.Location = new System.Drawing.Point(259, 39);
            this.cmbObject.Name = "cmbObject";
            this.cmbObject.Size = new System.Drawing.Size(208, 21);
            this.cmbObject.TabIndex = 3;
            this.cmbObject.DropDown += new System.EventHandler(this.cmbObject_DropDown);
            // 
            // lObject
            // 
            this.lObject.AutoSize = true;
            this.lObject.Location = new System.Drawing.Point(15, 43);
            this.lObject.Name = "lObject";
            this.lObject.Size = new System.Drawing.Size(45, 13);
            this.lObject.TabIndex = 1;
            this.lObject.Text = "Объект";
            // 
            // lDepsBonus
            // 
            this.lDepsBonus.AutoSize = true;
            this.lDepsBonus.Location = new System.Drawing.Point(15, 67);
            this.lDepsBonus.Name = "lDepsBonus";
            this.lDepsBonus.Size = new System.Drawing.Size(141, 13);
            this.lDepsBonus.TabIndex = 1;
            this.lDepsBonus.Text = "Отдел начисления премии";
            // 
            // cmbDepsBonus
            // 
            this.cmbDepsBonus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDepsBonus.FormattingEnabled = true;
            this.cmbDepsBonus.Location = new System.Drawing.Point(259, 63);
            this.cmbDepsBonus.Name = "cmbDepsBonus";
            this.cmbDepsBonus.Size = new System.Drawing.Size(208, 21);
            this.cmbDepsBonus.TabIndex = 3;
            this.cmbDepsBonus.DropDown += new System.EventHandler(this.cmbObject_DropDown);
            // 
            // chbShowUser
            // 
            this.chbShowUser.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chbShowUser.Location = new System.Drawing.Point(15, 90);
            this.chbShowUser.Name = "chbShowUser";
            this.chbShowUser.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chbShowUser.Size = new System.Drawing.Size(452, 24);
            this.chbShowUser.TabIndex = 4;
            this.chbShowUser.Text = "Выводить по сотрудникам";
            this.chbShowUser.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chbShowUser.UseVisualStyleBackColor = true;
            // 
            // lType
            // 
            this.lType.AutoSize = true;
            this.lType.Location = new System.Drawing.Point(15, 129);
            this.lType.Name = "lType";
            this.lType.Size = new System.Drawing.Size(84, 13);
            this.lType.TabIndex = 1;
            this.lType.Text = "Тип нарушения";
            // 
            // lDeps
            // 
            this.lDeps.AutoSize = true;
            this.lDeps.Location = new System.Drawing.Point(15, 155);
            this.lDeps.Name = "lDeps";
            this.lDeps.Size = new System.Drawing.Size(96, 13);
            this.lDeps.TabIndex = 1;
            this.lDeps.Text = "Отдел нарушения";
            // 
            // cmbType
            // 
            this.cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Location = new System.Drawing.Point(259, 125);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(208, 21);
            this.cmbType.TabIndex = 3;
            this.cmbType.DropDown += new System.EventHandler(this.cmbObject_DropDown);
            // 
            // cmbDeps
            // 
            this.cmbDeps.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDeps.FormattingEnabled = true;
            this.cmbDeps.Location = new System.Drawing.Point(259, 151);
            this.cmbDeps.Name = "cmbDeps";
            this.cmbDeps.Size = new System.Drawing.Size(208, 21);
            this.cmbDeps.TabIndex = 3;
            this.cmbDeps.DropDown += new System.EventHandler(this.cmbObject_DropDown);
            // 
            // lTypeDoc
            // 
            this.lTypeDoc.AutoSize = true;
            this.lTypeDoc.Location = new System.Drawing.Point(15, 182);
            this.lTypeDoc.Name = "lTypeDoc";
            this.lTypeDoc.Size = new System.Drawing.Size(83, 13);
            this.lTypeDoc.TabIndex = 1;
            this.lTypeDoc.Text = "Тип документа";
            // 
            // cmbTypeDoc
            // 
            this.cmbTypeDoc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTypeDoc.FormattingEnabled = true;
            this.cmbTypeDoc.Location = new System.Drawing.Point(259, 178);
            this.cmbTypeDoc.Name = "cmbTypeDoc";
            this.cmbTypeDoc.Size = new System.Drawing.Size(208, 21);
            this.cmbTypeDoc.TabIndex = 3;
            this.cmbTypeDoc.DropDown += new System.EventHandler(this.cmbObject_DropDown);
            // 
            // dgvStatus
            // 
            this.dgvStatus.AllowUserToAddRows = false;
            this.dgvStatus.AllowUserToDeleteRows = false;
            this.dgvStatus.AllowUserToResizeRows = false;
            this.dgvStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvStatus.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvStatus.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvStatus.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStatus.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cName,
            this.cV});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvStatus.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvStatus.Location = new System.Drawing.Point(116, 212);
            this.dgvStatus.MultiSelect = false;
            this.dgvStatus.Name = "dgvStatus";
            this.dgvStatus.RowHeadersVisible = false;
            this.dgvStatus.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvStatus.Size = new System.Drawing.Size(351, 188);
            this.dgvStatus.TabIndex = 5;
            // 
            // lStatus
            // 
            this.lStatus.AutoSize = true;
            this.lStatus.Location = new System.Drawing.Point(15, 212);
            this.lStatus.Name = "lStatus";
            this.lStatus.Size = new System.Drawing.Size(98, 13);
            this.lStatus.TabIndex = 1;
            this.lStatus.Text = "Статус документа";
            // 
            // cName
            // 
            this.cName.DataPropertyName = "cName";
            this.cName.HeaderText = "Наименование статуса";
            this.cName.Name = "cName";
            this.cName.ReadOnly = true;
            // 
            // cV
            // 
            this.cV.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.cV.DataPropertyName = "isSelect";
            this.cV.HeaderText = "V";
            this.cV.MinimumWidth = 45;
            this.cV.Name = "cV";
            this.cV.Width = 45;
            // 
            // frmReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(479, 450);
            this.Controls.Add(this.dgvStatus);
            this.Controls.Add(this.chbShowUser);
            this.Controls.Add(this.cmbTypeDoc);
            this.Controls.Add(this.cmbDeps);
            this.Controls.Add(this.cmbType);
            this.Controls.Add(this.cmbDepsBonus);
            this.Controls.Add(this.cmbObject);
            this.Controls.Add(this.lStatus);
            this.Controls.Add(this.lTypeDoc);
            this.Controls.Add(this.dtpEnd);
            this.Controls.Add(this.lDeps);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lDepsBonus);
            this.Controls.Add(this.lType);
            this.Controls.Add(this.dtpStart);
            this.Controls.Add(this.lObject);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btPrint);
            this.Controls.Add(this.btClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmReport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Отчёт по оплатам";
            this.Load += new System.EventHandler(this.frmReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvStatus)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.Button btPrint;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpStart;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.Label lObject;
        private System.Windows.Forms.Label lDepsBonus;
        private System.Windows.Forms.ComboBox cmbDepsBonus;
        private System.Windows.Forms.CheckBox chbShowUser;
        private System.Windows.Forms.Label lType;
        private System.Windows.Forms.Label lDeps;
        private System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.ComboBox cmbDeps;
        private System.Windows.Forms.Label lTypeDoc;
        private System.Windows.Forms.ComboBox cmbTypeDoc;
        private System.Windows.Forms.DataGridView dgvStatus;
        private System.Windows.Forms.Label lStatus;
        private System.Windows.Forms.ComboBox cmbObject;
        private System.Windows.Forms.DataGridViewTextBoxColumn cName;
        private System.Windows.Forms.DataGridViewCheckBoxColumn cV;
    }
}

