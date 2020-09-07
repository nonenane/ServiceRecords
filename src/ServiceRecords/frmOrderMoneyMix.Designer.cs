namespace ServiceRecords
{
    partial class frmOrderMoneyMix
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
            this.tbCourse = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbRUB = new System.Windows.Forms.TextBox();
            this.tbSumInRubCash = new System.Windows.Forms.TextBox();
            this.lbSumInRub = new System.Windows.Forms.Label();
            this.tbValuta = new System.Windows.Forms.TextBox();
            this.chbChangeDirector = new System.Windows.Forms.CheckBox();
            this.cmbDirector = new System.Windows.Forms.ComboBox();
            this.Director = new System.Windows.Forms.Label();
            this.tbMoney = new System.Windows.Forms.TextBox();
            this.tbNumberSub = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.btSelect = new System.Windows.Forms.Button();
            this.btClose = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.tbSummaCash = new System.Windows.Forms.TextBox();
            this.tbSummaNonCash = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tbSumInRubNonCash = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // tbCourse
            // 
            this.tbCourse.Location = new System.Drawing.Point(96, 184);
            this.tbCourse.Name = "tbCourse";
            this.tbCourse.Size = new System.Drawing.Size(41, 20);
            this.tbCourse.TabIndex = 58;
            this.tbCourse.Text = "0,00";
            this.tbCourse.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbCourse.Visible = false;
            this.tbCourse.TextChanged += new System.EventHandler(this.tbCourse_TextChanged);
            this.tbCourse.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbCourse_KeyPress);
            this.tbCourse.Leave += new System.EventHandler(this.tbCourse_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 187);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 13);
            this.label4.TabIndex = 57;
            this.label4.Text = "Курс валюты:";
            this.label4.Visible = false;
            // 
            // tbRUB
            // 
            this.tbRUB.Enabled = false;
            this.tbRUB.Location = new System.Drawing.Point(334, 183);
            this.tbRUB.Name = "tbRUB";
            this.tbRUB.Size = new System.Drawing.Size(34, 20);
            this.tbRUB.TabIndex = 56;
            this.tbRUB.Text = "RUB";
            this.tbRUB.Visible = false;
            // 
            // tbSumInRubCash
            // 
            this.tbSumInRubCash.Enabled = false;
            this.tbSumInRubCash.Location = new System.Drawing.Point(257, 183);
            this.tbSumInRubCash.Name = "tbSumInRubCash";
            this.tbSumInRubCash.Size = new System.Drawing.Size(71, 20);
            this.tbSumInRubCash.TabIndex = 55;
            this.tbSumInRubCash.Text = "0,00";
            this.tbSumInRubCash.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbSumInRubCash.Visible = false;
            // 
            // lbSumInRub
            // 
            this.lbSumInRub.AutoSize = true;
            this.lbSumInRub.Location = new System.Drawing.Point(144, 187);
            this.lbSumInRub.Name = "lbSumInRub";
            this.lbSumInRub.Size = new System.Drawing.Size(111, 13);
            this.lbSumInRub.TabIndex = 54;
            this.lbSumInRub.Text = "Сумма в рублях нал:";
            this.lbSumInRub.Visible = false;
            // 
            // tbValuta
            // 
            this.tbValuta.Enabled = false;
            this.tbValuta.Location = new System.Drawing.Point(334, 42);
            this.tbValuta.Name = "tbValuta";
            this.tbValuta.Size = new System.Drawing.Size(34, 20);
            this.tbValuta.TabIndex = 53;
            // 
            // chbChangeDirector
            // 
            this.chbChangeDirector.AutoSize = true;
            this.chbChangeDirector.Location = new System.Drawing.Point(161, 156);
            this.chbChangeDirector.Name = "chbChangeDirector";
            this.chbChangeDirector.Size = new System.Drawing.Size(143, 17);
            this.chbChangeDirector.TabIndex = 52;
            this.chbChangeDirector.Text = "Сменить руководителя";
            this.chbChangeDirector.UseVisualStyleBackColor = true;
            this.chbChangeDirector.CheckedChanged += new System.EventHandler(this.chbChangeDirector_CheckedChanged);
            this.chbChangeDirector.TextChanged += new System.EventHandler(this.chbChangeDirector_TextChanged);
            // 
            // cmbDirector
            // 
            this.cmbDirector.FormattingEnabled = true;
            this.cmbDirector.Location = new System.Drawing.Point(149, 128);
            this.cmbDirector.Name = "cmbDirector";
            this.cmbDirector.Size = new System.Drawing.Size(155, 21);
            this.cmbDirector.TabIndex = 51;
            // 
            // Director
            // 
            this.Director.AutoSize = true;
            this.Director.Location = new System.Drawing.Point(63, 131);
            this.Director.Name = "Director";
            this.Director.Size = new System.Drawing.Size(78, 13);
            this.Director.TabIndex = 50;
            this.Director.Text = "Руководитель";
            // 
            // tbMoney
            // 
            this.tbMoney.Location = new System.Drawing.Point(159, 42);
            this.tbMoney.MaxLength = 20;
            this.tbMoney.Name = "tbMoney";
            this.tbMoney.ReadOnly = true;
            this.tbMoney.Size = new System.Drawing.Size(155, 20);
            this.tbMoney.TabIndex = 48;
            this.tbMoney.Text = "0,00";
            this.tbMoney.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbMoney.TextChanged += new System.EventHandler(this.tbMoney_TextChanged);
            this.tbMoney.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbMoney_KeyPress);
            this.tbMoney.Leave += new System.EventHandler(this.tbMoney_Leave);
            // 
            // tbNumberSub
            // 
            this.tbNumberSub.Location = new System.Drawing.Point(159, 17);
            this.tbNumberSub.Name = "tbNumberSub";
            this.tbNumberSub.ReadOnly = true;
            this.tbNumberSub.Size = new System.Drawing.Size(155, 20);
            this.tbNumberSub.TabIndex = 49;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 106);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 13);
            this.label3.TabIndex = 45;
            this.label3.Text = "Предполагаемая дата";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(80, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 47;
            this.label1.Text = "Подномер";
            // 
            // dtpDate
            // 
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDate.Location = new System.Drawing.Point(149, 99);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(155, 20);
            this.dtpDate.TabIndex = 44;
            // 
            // btSelect
            // 
            this.btSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btSelect.Image = global::ServiceRecords.Properties.Resources.save_edit;
            this.btSelect.Location = new System.Drawing.Point(296, 246);
            this.btSelect.Name = "btSelect";
            this.btSelect.Size = new System.Drawing.Size(32, 32);
            this.btSelect.TabIndex = 42;
            this.toolTip1.SetToolTip(this.btSelect, "Сохранить");
            this.btSelect.UseVisualStyleBackColor = true;
            this.btSelect.Click += new System.EventHandler(this.btSelect_Click);
            // 
            // btClose
            // 
            this.btClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btClose.Image = global::ServiceRecords.Properties.Resources.exit_8633;
            this.btClose.Location = new System.Drawing.Point(334, 246);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(32, 32);
            this.btClose.TabIndex = 43;
            this.toolTip1.SetToolTip(this.btClose, "Выход");
            this.btClose.UseVisualStyleBackColor = true;
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(52, 77);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 13);
            this.label5.TabIndex = 59;
            this.label5.Text = "Сумма в нал.";
            // 
            // tbSummaCash
            // 
            this.tbSummaCash.Location = new System.Drawing.Point(132, 73);
            this.tbSummaCash.MaxLength = 20;
            this.tbSummaCash.Name = "tbSummaCash";
            this.tbSummaCash.Size = new System.Drawing.Size(66, 20);
            this.tbSummaCash.TabIndex = 60;
            this.tbSummaCash.Text = "0,00";
            this.tbSummaCash.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbSummaCash.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tbSummaCash_MouseClick);
            this.tbSummaCash.TextChanged += new System.EventHandler(this.tbSummaCash_TextChanged);
            this.tbSummaCash.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbMoney_KeyPress);
            this.tbSummaCash.Leave += new System.EventHandler(this.tbSummaCash_Leave);
            // 
            // tbSummaNonCash
            // 
            this.tbSummaNonCash.Location = new System.Drawing.Point(302, 73);
            this.tbSummaNonCash.MaxLength = 20;
            this.tbSummaNonCash.Name = "tbSummaNonCash";
            this.tbSummaNonCash.Size = new System.Drawing.Size(66, 20);
            this.tbSummaNonCash.TabIndex = 61;
            this.tbSummaNonCash.Text = "0,00";
            this.tbSummaNonCash.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbSummaNonCash.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tbSummaCash_MouseClick);
            this.tbSummaNonCash.TextChanged += new System.EventHandler(this.tbSummaNonCash_TextChanged);
            this.tbSummaNonCash.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbMoney_KeyPress);
            this.tbSummaNonCash.Leave += new System.EventHandler(this.tbSummaNonCash_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(99, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 46;
            this.label2.Text = "Сумма";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(204, 76);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(92, 13);
            this.label6.TabIndex = 62;
            this.label6.Text = "Сумма в безнал.";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(129, 217);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(129, 13);
            this.label7.TabIndex = 63;
            this.label7.Text = "Сумма в рублях безнал:";
            this.label7.Visible = false;
            // 
            // tbSumInRubNonCash
            // 
            this.tbSumInRubNonCash.Enabled = false;
            this.tbSumInRubNonCash.Location = new System.Drawing.Point(257, 214);
            this.tbSumInRubNonCash.Name = "tbSumInRubNonCash";
            this.tbSumInRubNonCash.Size = new System.Drawing.Size(71, 20);
            this.tbSumInRubNonCash.TabIndex = 64;
            this.tbSumInRubNonCash.Text = "0,00";
            this.tbSumInRubNonCash.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbSumInRubNonCash.Visible = false;
            // 
            // textBox2
            // 
            this.textBox2.Enabled = false;
            this.textBox2.Location = new System.Drawing.Point(334, 214);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(34, 20);
            this.textBox2.TabIndex = 65;
            this.textBox2.Text = "RUB";
            this.textBox2.Visible = false;
            // 
            // frmOrderMoneyMix
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 290);
            this.ControlBox = false;
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.tbSumInRubNonCash);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tbSummaNonCash);
            this.Controls.Add(this.tbSummaCash);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbCourse);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbRUB);
            this.Controls.Add(this.tbSumInRubCash);
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
            this.MaximumSize = new System.Drawing.Size(400, 330);
            this.MinimumSize = new System.Drawing.Size(400, 272);
            this.Name = "frmOrderMoneyMix";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmOrderMoneyMix";
            this.Load += new System.EventHandler(this.frmOrderMoneyMix_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbCourse;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbRUB;
        private System.Windows.Forms.TextBox tbSumInRubCash;
        private System.Windows.Forms.Label lbSumInRub;
        private System.Windows.Forms.TextBox tbValuta;
        private System.Windows.Forms.CheckBox chbChangeDirector;
        private System.Windows.Forms.ComboBox cmbDirector;
        private System.Windows.Forms.Label Director;
        private System.Windows.Forms.TextBox tbMoney;
        private System.Windows.Forms.TextBox tbNumberSub;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Button btSelect;
        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbSummaCash;
        private System.Windows.Forms.TextBox tbSummaNonCash;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbSumInRubNonCash;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}