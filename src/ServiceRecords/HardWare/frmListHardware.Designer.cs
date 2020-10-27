namespace ServiceRecords.HardWare
{
    partial class frmListHardware
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
            this.dgvNote = new System.Windows.Forms.DataGridView();
            this.tbNumber = new System.Windows.Forms.TextBox();
            this.tbEan = new System.Windows.Forms.TextBox();
            this.tbName = new System.Windows.Forms.TextBox();
            this.tbNumSZ = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cEan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cLoc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cMaster = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNote)).BeginInit();
            this.SuspendLayout();
            // 
            // btClose
            // 
            this.btClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btClose.Image = global::ServiceRecords.Properties.Resources.exit_8633;
            this.btClose.Location = new System.Drawing.Point(1011, 331);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(32, 32);
            this.btClose.TabIndex = 42;
            this.btClose.UseVisualStyleBackColor = true;
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
            // 
            // dgvNote
            // 
            this.dgvNote.AllowUserToAddRows = false;
            this.dgvNote.AllowUserToDeleteRows = false;
            this.dgvNote.AllowUserToResizeRows = false;
            this.dgvNote.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvNote.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvNote.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvNote.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvNote.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cNum,
            this.cEan,
            this.cName,
            this.cType,
            this.cLoc,
            this.cMaster,
            this.cStatus});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvNote.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvNote.Location = new System.Drawing.Point(12, 70);
            this.dgvNote.MultiSelect = false;
            this.dgvNote.Name = "dgvNote";
            this.dgvNote.RowHeadersVisible = false;
            this.dgvNote.Size = new System.Drawing.Size(1031, 255);
            this.dgvNote.TabIndex = 43;
            this.dgvNote.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dgvNote_ColumnWidthChanged);
            // 
            // tbNumber
            // 
            this.tbNumber.Location = new System.Drawing.Point(11, 44);
            this.tbNumber.Name = "tbNumber";
            this.tbNumber.Size = new System.Drawing.Size(100, 20);
            this.tbNumber.TabIndex = 44;
            this.tbNumber.TextChanged += new System.EventHandler(this.tbNumber_TextChanged);
            // 
            // tbEan
            // 
            this.tbEan.Location = new System.Drawing.Point(117, 44);
            this.tbEan.Name = "tbEan";
            this.tbEan.Size = new System.Drawing.Size(100, 20);
            this.tbEan.TabIndex = 44;
            this.tbEan.TextChanged += new System.EventHandler(this.tbNumber_TextChanged);
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(223, 44);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(100, 20);
            this.tbName.TabIndex = 44;
            this.tbName.TextChanged += new System.EventHandler(this.tbNumber_TextChanged);
            // 
            // tbNumSZ
            // 
            this.tbNumSZ.Location = new System.Drawing.Point(73, 5);
            this.tbNumSZ.Name = "tbNumSZ";
            this.tbNumSZ.ReadOnly = true;
            this.tbNumSZ.Size = new System.Drawing.Size(542, 20);
            this.tbNumSZ.TabIndex = 44;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 45;
            this.label1.Text = "СЗ на ДС";
            // 
            // cNum
            // 
            this.cNum.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.cNum.DataPropertyName = "InventoryNumber";
            this.cNum.FillWeight = 45.60068F;
            this.cNum.HeaderText = "Инв. №";
            this.cNum.MinimumWidth = 50;
            this.cNum.Name = "cNum";
            this.cNum.ReadOnly = true;
            this.cNum.Width = 50;
            // 
            // cEan
            // 
            this.cEan.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.cEan.DataPropertyName = "EAN";
            this.cEan.FillWeight = 426.3959F;
            this.cEan.HeaderText = "EAN";
            this.cEan.MinimumWidth = 80;
            this.cEan.Name = "cEan";
            this.cEan.ReadOnly = true;
            this.cEan.Width = 110;
            // 
            // cName
            // 
            this.cName.DataPropertyName = "cName";
            this.cName.FillWeight = 45.60068F;
            this.cName.HeaderText = "Наименование";
            this.cName.MinimumWidth = 120;
            this.cName.Name = "cName";
            this.cName.ReadOnly = true;
            // 
            // cType
            // 
            this.cType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.cType.DataPropertyName = "nameType";
            this.cType.FillWeight = 45.60068F;
            this.cType.HeaderText = "Оборудование/ Комплектующие";
            this.cType.MinimumWidth = 90;
            this.cType.Name = "cType";
            this.cType.ReadOnly = true;
            this.cType.Width = 130;
            // 
            // cLoc
            // 
            this.cLoc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.cLoc.DataPropertyName = "nameLoc";
            this.cLoc.FillWeight = 45.60068F;
            this.cLoc.HeaderText = "Местоположение";
            this.cLoc.MinimumWidth = 100;
            this.cLoc.Name = "cLoc";
            this.cLoc.ReadOnly = true;
            this.cLoc.Width = 130;
            // 
            // cMaster
            // 
            this.cMaster.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.cMaster.DataPropertyName = "fio";
            this.cMaster.FillWeight = 45.60068F;
            this.cMaster.HeaderText = "Ответственный";
            this.cMaster.MinimumWidth = 85;
            this.cMaster.Name = "cMaster";
            this.cMaster.ReadOnly = true;
            this.cMaster.Width = 130;
            // 
            // cStatus
            // 
            this.cStatus.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.cStatus.DataPropertyName = "nameStatus";
            this.cStatus.FillWeight = 45.60068F;
            this.cStatus.HeaderText = "Статус";
            this.cStatus.MinimumWidth = 80;
            this.cStatus.Name = "cStatus";
            this.cStatus.ReadOnly = true;
            this.cStatus.Width = 80;
            // 
            // frmListHardware
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1051, 371);
            this.ControlBox = false;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbNumSZ);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.tbEan);
            this.Controls.Add(this.tbNumber);
            this.Controls.Add(this.btClose);
            this.Controls.Add(this.dgvNote);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmListHardware";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Список оборудования закупленного по СЗ";
            this.Load += new System.EventHandler(this.frmListHardware_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvNote)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.DataGridView dgvNote;
        private System.Windows.Forms.TextBox tbNumber;
        private System.Windows.Forms.TextBox tbEan;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.TextBox tbNumSZ;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn cNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn cEan;
        private System.Windows.Forms.DataGridViewTextBoxColumn cName;
        private System.Windows.Forms.DataGridViewTextBoxColumn cType;
        private System.Windows.Forms.DataGridViewTextBoxColumn cLoc;
        private System.Windows.Forms.DataGridViewTextBoxColumn cMaster;
        private System.Windows.Forms.DataGridViewTextBoxColumn cStatus;
    }
}