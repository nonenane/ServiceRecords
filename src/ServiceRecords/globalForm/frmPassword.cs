using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ServiceRecords.globalForm
{
    public partial class frmPassword : Form
    {
       // public string getPassword { private set; get; }
        public int idUser { set; private get; }
        public int idCreator { set; private get; }
        public frmPassword()
        {
            InitializeComponent();
            ToolTip tt = new ToolTip();
            tt.SetToolTip(btClose, "Выход");
            tt.SetToolTip(btSelect, "Сохранить");
        }

        private void chbShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (chbShowPassword.Checked)
            {
                tbPassword.PasswordChar = '\0';
            }
            else
            {
                tbPassword.PasswordChar = '*';
            }
        }

        private void btSelect_Click(object sender, EventArgs e)
        {
            if (tbPassword.Text.Trim().Length == 0)
            {
                MessageBox.Show("Необходимо ввести пароль!", "Информирование", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string getPassword = tbPassword.Text.Trim();
            Guid guiPassword = Config.GetHashString(getPassword);

            DataTable dtTmp = Config.hCntMain.verificationPasswordrUsers(idUser, guiPassword);
            DataTable dtTmp2 = Config.hCntMain.verificationPasswordrUsers(idCreator, guiPassword);

            if (dtTmp == null && dtTmp2 == null ? true : dtTmp.Rows.Count == 0 && dtTmp2.Rows.Count == 0 ? true : false )
                return;

            if (dtTmp.Rows[0]["id"].ToString().Equals("-1") && dtTmp2.Rows[0]["id"].ToString().Equals("-1"))
            {
                MessageBox.Show(dtTmp.Rows[0]["msg"].ToString(), "Информирование", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            this.DialogResult = DialogResult.OK;
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
