namespace DicSettingDepCalBonus
{
    partial class frmAdd
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
            this.btClose = new System.Windows.Forms.Button();
            this.btSave = new System.Windows.Forms.Button();
            this.cmbDeps = new System.Windows.Forms.ComboBox();
            this.lDeps = new System.Windows.Forms.Label();
            this.tbMinSumma = new System.Windows.Forms.TextBox();
            this.lMinSum = new System.Windows.Forms.Label();
            this.tbPercent = new System.Windows.Forms.TextBox();
            this.lPercent = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btClose
            // 
            this.btClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btClose.Image = global::DicSettingDepCalBonus.Properties.Resources.door_out;
            this.btClose.Location = new System.Drawing.Point(389, 52);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(32, 32);
            this.btClose.TabIndex = 1;
            this.btClose.UseVisualStyleBackColor = true;
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
            // 
            // btSave
            // 
            this.btSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btSave.Image = global::DicSettingDepCalBonus.Properties.Resources.save_edit;
            this.btSave.Location = new System.Drawing.Point(351, 52);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(32, 32);
            this.btSave.TabIndex = 1;
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // cmbDeps
            // 
            this.cmbDeps.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDeps.FormattingEnabled = true;
            this.cmbDeps.Location = new System.Drawing.Point(225, 11);
            this.cmbDeps.Name = "cmbDeps";
            this.cmbDeps.Size = new System.Drawing.Size(204, 21);
            this.cmbDeps.TabIndex = 2;
            this.cmbDeps.DropDown += new System.EventHandler(this.cmbDeps_DropDown);
            // 
            // lDeps
            // 
            this.lDeps.AutoSize = true;
            this.lDeps.Location = new System.Drawing.Point(12, 15);
            this.lDeps.Name = "lDeps";
            this.lDeps.Size = new System.Drawing.Size(83, 13);
            this.lDeps.TabIndex = 3;
            this.lDeps.Text = "Наименование";
            // 
            // tbMinSumma
            // 
            this.tbMinSumma.Location = new System.Drawing.Point(225, 38);
            this.tbMinSumma.MaxLength = 21;
            this.tbMinSumma.Name = "tbMinSumma";
            this.tbMinSumma.Size = new System.Drawing.Size(100, 20);
            this.tbMinSumma.TabIndex = 4;
            this.tbMinSumma.Text = "0,00";
            this.tbMinSumma.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbMinSumma.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbMinSumma_KeyPress);
            this.tbMinSumma.Leave += new System.EventHandler(this.tbMinSumma_Leave);
            // 
            // lMinSum
            // 
            this.lMinSum.AutoSize = true;
            this.lMinSum.Location = new System.Drawing.Point(12, 42);
            this.lMinSum.Name = "lMinSum";
            this.lMinSum.Size = new System.Drawing.Size(190, 13);
            this.lMinSum.TabIndex = 3;
            this.lMinSum.Text = "Мин. сумма отплаты по нарушению:";
            // 
            // tbPercent
            // 
            this.tbPercent.Location = new System.Drawing.Point(225, 64);
            this.tbPercent.MaxLength = 5;
            this.tbPercent.Name = "tbPercent";
            this.tbPercent.Size = new System.Drawing.Size(100, 20);
            this.tbPercent.TabIndex = 4;
            this.tbPercent.Text = "0,00";
            this.tbPercent.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbPercent.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbMinSumma_KeyPress);
            this.tbPercent.Leave += new System.EventHandler(this.tbPercent_Leave);
            // 
            // lPercent
            // 
            this.lPercent.AutoSize = true;
            this.lPercent.Location = new System.Drawing.Point(12, 68);
            this.lPercent.Name = "lPercent";
            this.lPercent.Size = new System.Drawing.Size(206, 13);
            this.lPercent.TabIndex = 3;
            this.lPercent.Text = "Процент суммы оплаты по нарушению:";
            // 
            // frmAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(433, 102);
            this.ControlBox = false;
            this.Controls.Add(this.tbPercent);
            this.Controls.Add(this.tbMinSumma);
            this.Controls.Add(this.lPercent);
            this.Controls.Add(this.lMinSum);
            this.Controls.Add(this.lDeps);
            this.Controls.Add(this.cmbDeps);
            this.Controls.Add(this.btSave);
            this.Controls.Add(this.btClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAdd";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmAdd";
            this.Load += new System.EventHandler(this.frmAdd_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.ComboBox cmbDeps;
        private System.Windows.Forms.Label lDeps;
        private System.Windows.Forms.TextBox tbMinSumma;
        private System.Windows.Forms.Label lMinSum;
        private System.Windows.Forms.TextBox tbPercent;
        private System.Windows.Forms.Label lPercent;
    }
}