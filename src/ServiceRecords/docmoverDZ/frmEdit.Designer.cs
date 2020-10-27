namespace ServiceRecords.docmoverDZ
{
    partial class frmEdit
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
            this.label1 = new System.Windows.Forms.Label();
            this.tbDate = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbType = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbDep = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbSumPenalty = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbSumBonus = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbFIO = new System.Windows.Forms.TextBox();
            this.btSave = new System.Windows.Forms.Button();
            this.btClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 13);
            this.label1.TabIndex = 38;
            this.label1.Text = "Дата составления ДЗ:";
            // 
            // tbDate
            // 
            this.tbDate.Location = new System.Drawing.Point(257, 16);
            this.tbDate.Name = "tbDate";
            this.tbDate.ReadOnly = true;
            this.tbDate.Size = new System.Drawing.Size(173, 20);
            this.tbDate.TabIndex = 39;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 13);
            this.label2.TabIndex = 38;
            this.label2.Text = "Тип нарушения:";
            // 
            // tbType
            // 
            this.tbType.Location = new System.Drawing.Point(257, 42);
            this.tbType.Name = "tbType";
            this.tbType.ReadOnly = true;
            this.tbType.Size = new System.Drawing.Size(173, 20);
            this.tbType.TabIndex = 39;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 13);
            this.label3.TabIndex = 38;
            this.label3.Text = "Отдел нарушителя:";
            // 
            // tbDep
            // 
            this.tbDep.Location = new System.Drawing.Point(257, 68);
            this.tbDep.Name = "tbDep";
            this.tbDep.ReadOnly = true;
            this.tbDep.Size = new System.Drawing.Size(173, 20);
            this.tbDep.TabIndex = 39;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 98);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 13);
            this.label4.TabIndex = 38;
            this.label4.Text = "Сумма нарушения:";
            // 
            // tbSumPenalty
            // 
            this.tbSumPenalty.Location = new System.Drawing.Point(257, 94);
            this.tbSumPenalty.Name = "tbSumPenalty";
            this.tbSumPenalty.ReadOnly = true;
            this.tbSumPenalty.Size = new System.Drawing.Size(173, 20);
            this.tbSumPenalty.TabIndex = 39;
            this.tbSumPenalty.Text = "0,00";
            this.tbSumPenalty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 124);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(85, 13);
            this.label5.TabIndex = 38;
            this.label5.Text = "Сумма премии:";
            // 
            // tbSumBonus
            // 
            this.tbSumBonus.Location = new System.Drawing.Point(257, 120);
            this.tbSumBonus.MaxLength = 19;
            this.tbSumBonus.Name = "tbSumBonus";
            this.tbSumBonus.Size = new System.Drawing.Size(173, 20);
            this.tbSumBonus.TabIndex = 39;
            this.tbSumBonus.Text = "0,00";
            this.tbSumBonus.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbSumBonus.TextChanged += new System.EventHandler(this.tbSumBonus_TextChanged);
            this.tbSumBonus.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbSumBonus_KeyPress);
            this.tbSumBonus.Leave += new System.EventHandler(this.tbSumBonus_Leave);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 150);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(239, 13);
            this.label6.TabIndex = 38;
            this.label6.Text = "ФИО сотрудника обнаружевшего нарушение:";
            // 
            // tbFIO
            // 
            this.tbFIO.Location = new System.Drawing.Point(257, 146);
            this.tbFIO.Name = "tbFIO";
            this.tbFIO.ReadOnly = true;
            this.tbFIO.Size = new System.Drawing.Size(173, 20);
            this.tbFIO.TabIndex = 39;
            // 
            // btSave
            // 
            this.btSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btSave.Image = global::ServiceRecords.Properties.Resources.save_edit;
            this.btSave.Location = new System.Drawing.Point(358, 183);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(32, 32);
            this.btSave.TabIndex = 36;
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // btClose
            // 
            this.btClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btClose.Image = global::ServiceRecords.Properties.Resources.exit_8633;
            this.btClose.Location = new System.Drawing.Point(396, 183);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(32, 32);
            this.btClose.TabIndex = 37;
            this.btClose.UseVisualStyleBackColor = true;
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
            // 
            // frmEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(440, 227);
            this.ControlBox = false;
            this.Controls.Add(this.tbFIO);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tbSumBonus);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbSumPenalty);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbDep);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbType);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbDate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btSave);
            this.Controls.Add(this.btClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmEdit";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Редактирование ДЗ";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmEdit_FormClosing);
            this.Load += new System.EventHandler(this.frmEdit_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbDep;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbSumPenalty;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbSumBonus;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbFIO;
    }
}