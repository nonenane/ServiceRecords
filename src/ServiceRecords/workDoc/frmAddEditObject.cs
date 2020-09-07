using Nwuram.Framework.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ServiceRecords.workDoc
{
    public partial class frmAddEditObject : Form
    {
        public int id { get; set; }
        public string str {get; set;}
        public int typework { get; set;}

        public frmAddEditObject(int _id = 0, string _str = "", int _typework = 0)
        {
            InitializeComponent();
            id = _id;
            str = _str;
            typework = _typework;
            if (str != null)
                tbStr.Text = str;
        }

        private void btnCloseForm_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (tbStr.Text.Length < 1)
            {
                MessageBox.Show("Необходимо ввести имя объекта");
                return;
            }

            DataTable dtResult = Config.hCntMain.addEditObject(tbStr.Text, typework, id);

            if (id == 0)
            {
                if (dtResult != null && dtResult.Rows.Count > 0)
                {
                    if (dtResult.Columns.Contains("id"))
                        id = (int)dtResult.Rows[0]["id"];
                    else if (dtResult.Columns.Contains("id_Object"))
                        id = (int)dtResult.Rows[0]["id_Object"];
                }

                Logging.StartFirstLevel(1498);
                Logging.Comment("ID: "+id);
                Logging.Comment("Наименование: "+tbStr.Text);                
                Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                                + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
                Logging.StopFirstLevel();
            }
            else
            {
                Logging.StartFirstLevel(1499);
                Logging.Comment("ID: " + id);
                Logging.VariableChange("Наименование: ", tbStr.Text, str);
                Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                                + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
                Logging.StopFirstLevel();
            }



           
            this.DialogResult = DialogResult.OK;
        }
    }
}
