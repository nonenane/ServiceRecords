namespace ServiceRecords
{
    partial class frmOrderMoney
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
            this.tbMoney = new System.Windows.Forms.TextBox();
            this.tbNumberSub = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.Director = new System.Windows.Forms.Label();
            this.cmbDirector = new System.Windows.Forms.ComboBox();
            this.chbChangeDirector = new System.Windows.Forms.CheckBox();
            this.tbValuta = new System.Windows.Forms.TextBox();
            this.lbSumInRub = new System.Windows.Forms.Label();
            this.tbSumInRub = new System.Windows.Forms.TextBox();
            this.tbRUB = new System.Windows.Forms.TextBox();
            this.btSelect = new System.Windows.Forms.Button();
            this.btClose = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.tbCourse = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // tbMoney
            // 
            this.tbMoney.Location = new System.Drawing.Point(143, 31);
            this.tbMoney.MaxLength = 20;
            this.tbMoney.Name = "tbMoney";
            this.tbMoney.Size = new System.Drawing.Size(155, 20);
            this.tbMoney.TabIndex = 31;
            this.tbMoney.Text = "0,00";
            this.tbMoney.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbMoney.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tbMoney_MouseClick);
            this.tbMoney.TextChanged += new System.EventHandler(this.tbMoney_TextChanged);
            this.tbMoney.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbMoney_KeyPress);
            this.tbMoney.Leave += new System.EventHandler(this.tbMoney_Leave);
            // 
            // tbNumberSub
            // 
            this.tbNumberSub.Location = new System.Drawing.Point(145, 6);
            this.tbNumberSub.Name = "tbNumberSub";
            this.tbNumberSub.ReadOnly = true;
            this.tbNumberSub.Size = new System.Drawing.Size(155, 20);
            this.tbNumberSub.TabIndex = 32;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 13);
            this.label3.TabIndex = 28;
            this.label3.Text = "Предполагаемая дата";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(85, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 29;
            this.label2.Text = "Сумма";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(66, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 30;
            this.label1.Text = "Подномер";
            // 
            // dtpDate
            // 
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDate.Location = new System.Drawing.Point(145, 58);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(155, 20);
            this.dtpDate.TabIndex = 27;
            this.dtpDate.ValueChanged += new System.EventHandler(this.dtpDate_ValueChanged);
            // 
            // Director
            // 
            this.Director.AutoSize = true;
            this.Director.Location = new System.Drawing.Point(47, 87);
            this.Director.Name = "Director";
            this.Director.Size = new System.Drawing.Size(78, 13);
            this.Director.TabIndex = 33;
            this.Director.Text = "Руководитель";
            // 
            // cmbDirector
            // 
            this.cmbDirector.FormattingEnabled = true;
            this.cmbDirector.Location = new System.Drawing.Point(145, 85);
            this.cmbDirector.Name = "cmbDirector";
            this.cmbDirector.Size = new System.Drawing.Size(155, 21);
            this.cmbDirector.TabIndex = 34;
            this.cmbDirector.SelectionChangeCommitted += new System.EventHandler(this.cmbDirector_SelectionChangeCommitted);
            // 
            // chbChangeDirector
            // 
            this.chbChangeDirector.AutoSize = true;
            this.chbChangeDirector.Location = new System.Drawing.Point(145, 112);
            this.chbChangeDirector.Name = "chbChangeDirector";
            this.chbChangeDirector.Size = new System.Drawing.Size(143, 17);
            this.chbChangeDirector.TabIndex = 35;
            this.chbChangeDirector.Text = "Сменить руководителя";
            this.chbChangeDirector.UseVisualStyleBackColor = true;
            this.chbChangeDirector.CheckedChanged += new System.EventHandler(this.chbChangeDirector_CheckedChanged);
            // 
            // tbValuta
            // 
            this.tbValuta.Enabled = false;
            this.tbValuta.Location = new System.Drawing.Point(306, 31);
            this.tbValuta.Name = "tbValuta";
            this.tbValuta.Size = new System.Drawing.Size(34, 20);
            this.tbValuta.TabIndex = 36;
            // 
            // lbSumInRub
            // 
            this.lbSumInRub.AutoSize = true;
            this.lbSumInRub.Location = new System.Drawing.Point(140, 147);
            this.lbSumInRub.Name = "lbSumInRub";
            this.lbSumInRub.Size = new System.Drawing.Size(90, 13);
            this.lbSumInRub.TabIndex = 37;
            this.lbSumInRub.Text = "Сумма в рублях:";
            this.lbSumInRub.Visible = false;
            // 
            // tbSumInRub
            // 
            this.tbSumInRub.Enabled = false;
            this.tbSumInRub.Location = new System.Drawing.Point(229, 143);
            this.tbSumInRub.Name = "tbSumInRub";
            this.tbSumInRub.Size = new System.Drawing.Size(71, 20);
            this.tbSumInRub.TabIndex = 38;
            this.tbSumInRub.Text = "0,00";
            this.tbSumInRub.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbSumInRub.Visible = false;
            this.tbSumInRub.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbMoney_KeyPress);
            this.tbSumInRub.Leave += new System.EventHandler(this.tbSumInRub_Leave);
            // 
            // tbRUB
            // 
            this.tbRUB.Enabled = false;
            this.tbRUB.Location = new System.Drawing.Point(306, 143);
            this.tbRUB.Name = "tbRUB";
            this.tbRUB.Size = new System.Drawing.Size(34, 20);
            this.tbRUB.TabIndex = 39;
            this.tbRUB.Text = "RUB";
            this.tbRUB.Visible = false;
            // 
            // btSelect
            // 
            this.btSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btSelect.Image = global::ServiceRecords.Properties.Resources.save_edit;
            this.btSelect.Location = new System.Drawing.Point(268, 169);
            this.btSelect.Name = "btSelect";
            this.btSelect.Size = new System.Drawing.Size(32, 32);
            this.btSelect.TabIndex = 25;
            this.toolTip1.SetToolTip(this.btSelect, "Сохранить");
            this.btSelect.UseVisualStyleBackColor = true;
            this.btSelect.Click += new System.EventHandler(this.btSelect_Click);
            // 
            // btClose
            // 
            this.btClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btClose.Image = global::ServiceRecords.Properties.Resources.exit_8633;
            this.btClose.Location = new System.Drawing.Point(308, 169);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(32, 32);
            this.btClose.TabIndex = 26;
            this.toolTip1.SetToolTip(this.btClose, "Выход");
            this.btClose.UseVisualStyleBackColor = true;
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 147);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 13);
            this.label4.TabIndex = 40;
            this.label4.Text = "Курс валюты:";
            this.label4.Visible = false;
            // 
            // tbCourse
            // 
            this.tbCourse.Location = new System.Drawing.Point(84, 144);
            this.tbCourse.Name = "tbCourse";
            this.tbCourse.Size = new System.Drawing.Size(41, 20);
            this.tbCourse.TabIndex = 41;
            this.tbCourse.Text = "0,00";
            this.tbCourse.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbCourse.Visible = false;
            this.tbCourse.TextChanged += new System.EventHandler(this.tbCourse_TextChanged);
            this.tbCourse.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbMoney_KeyPress);
            this.tbCourse.Leave += new System.EventHandler(this.tbCourse_Leave);
            // 
            // frmOrderMoney
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(348, 207);
            this.ControlBox = false;
            this.Controls.Add(this.tbCourse);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbRUB);
            this.Controls.Add(this.tbSumInRub);
            this.Controls.Add(this.lbSumInRub);
            this.Controls.Add(this.tbValuta);
            this.Controls.Add(this.chbChangeDirector);
            this.Controls.Add(this.cmbDirector);
            this.Controls.Add(this.Director);
            this.Controls.Add(this.tbMoney);
            this.Controls.Add(this.tbNumberSub);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.btSelect);
            this.Controls.Add(this.btClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmOrderMoney";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmOrderMoney";
            this.Load += new System.EventHandler(this.frmOrderMoney_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbMoney;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Button btSelect;
        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.Label Director;
        private System.Windows.Forms.ComboBox cmbDirector;
        private System.Windows.Forms.CheckBox chbChangeDirector;
        private System.Windows.Forms.TextBox tbNumberSub;
        private System.Windows.Forms.TextBox tbValuta;
        private System.Windows.Forms.Label lbSumInRub;
        private System.Windows.Forms.TextBox tbSumInRub;
        private System.Windows.Forms.TextBox tbRUB;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbCourse;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}