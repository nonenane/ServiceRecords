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

    public partial class frmAddUsers : Form
    {
        public frmAddUsers()
        {
            InitializeComponent();
        }

        private void frmAddUsers_Load(object sender, EventArgs e)
        {
            tbFIO.Text = Nwuram.Framework.Settings.User.UserSettings.User.FullUsername;
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            //if (tbFIO.Text.Trim().Length == 0 && tbPassword.Text.Trim().Length == 0) return;

            //if (tbFIO.Text.Trim().Length == 0) { MessageBox.Show("Введите имя пользователя", "Информирование", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (tbPassword.Text.Trim().Length == 0) { MessageBox.Show("Введите пароль", "Информирование", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (tbPassword2.Text.Trim().Length == 0) { MessageBox.Show("Введите пароль 2", "Информирование", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }


            if (!tbPassword.Text.Trim().Equals(tbPassword2.Text.Trim()))
            {
                MessageBox.Show("Пароли не совпадают", "Информирование", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


            Guid guiPassword = Config.GetHashString(tbPassword.Text.Trim());
            string strFIO = tbFIO.Text.Trim();

            DataTable dtTmp = Config.hCntMain.setUsers(guiPassword);

            if (dtTmp == null || dtTmp.Rows.Count == 0)
                return;

            if (dtTmp.Rows[0]["id"].ToString().Equals("-1"))
            {
                MessageBox.Show(dtTmp.Rows[0]["msg"].ToString(), "Информирование", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            MessageBox.Show("Данные сохранены", "Информирование", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.DialogResult = DialogResult.OK;
        }

    }
}
