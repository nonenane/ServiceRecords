using Nwuram.Framework.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ServiceRecords.settings
{
    public partial class frmBlockVsDeps : Form
    {
        public DataRowView rowView { set; private get; }
        //private bool isEdit = false;
        private int id = -1;

        public frmBlockVsDeps()
        {
            InitializeComponent();
            ToolTip tt = new ToolTip();
            tt.SetToolTip(btClose, "Выход");
            tt.SetToolTip(btSelect, "Сохранить");
        }

        private void frmBlockVsDeps_Load(object sender, EventArgs e)
        {
            getBlock();
            getDeps();
            if (rowView != null)
            {
                id = (int)rowView["id"];
                cmbBlock.SelectedValue = (int)rowView["id_Block"];
                cmbDeps.SelectedValue = (int)rowView["id_Department"];
                //isEdit = true;
            }
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btSelect_Click(object sender, EventArgs e)
        {

            int id_block = (Int16)cmbBlock.SelectedValue; 
            int id_deps = (Int16)cmbDeps.SelectedValue; 

            //setBlockVsDepartment



            DataTable dtTMP = Config.hCntMain.setBlockVsDepartment(id_block, id_deps, id, false);

            if (dtTMP == null || dtTMP.Rows.Count == 0)
            {
                MessageBox.Show("Ошибока добавления");
                return;
            }

            if (int.Parse(dtTMP.Rows[0]["id"].ToString()) == -1)
            {
                MessageBox.Show("Такая комбинация уже существует");
                return;
            }

            if (id == -1)
            {
                Logging.StartFirstLevel(1258);
                Logging.Comment("Блок ID: " + (Int16)cmbBlock.SelectedValue + "; Наименование:" + (string)cmbBlock.Text);
                Logging.Comment("Отдел ID: " + (Int16)cmbDeps.SelectedValue + "; Наименование:" + (string)cmbDeps.Text);
                Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                    + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
                Logging.StopFirstLevel();
            }
            else
            {
                Logging.StartFirstLevel(1259);
                Logging.VariableChange("Блок ID: ", (Int16)cmbBlock.SelectedValue, (int)rowView["id_Block"], typeLog._int);
                Logging.VariableChange("Блок Наименование:", (string)cmbBlock.Text, (string)rowView["nameBlock"], typeLog._string);

                Logging.VariableChange("Отдел ID: ", (Int16)cmbDeps.SelectedValue, (int)rowView["id_Department"], typeLog._int);
                Logging.VariableChange("Отдел Наименование:", (string)rowView["nameBlock"], (string)rowView["nameDeps"], typeLog._string);

                Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                    + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
                Logging.StopFirstLevel();

            }

            MessageBox.Show("Данные сохранены!", "Информирование", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.DialogResult = DialogResult.OK;
        }

        DataTable dtBlock, dtDeps;
        private void getBlock()
        {
            dtBlock = Config.hCntMain.getDeps();
            cmbBlock.DataSource = dtBlock;
            cmbBlock.DisplayMember = "name";
            cmbBlock.ValueMember = "id";
        }

        private void getDeps()
        {
            if (cmbBlock.SelectedValue == null)
            {
                cmbDeps.DataSource = null;
            }

            int id_block = (Int16)cmbBlock.SelectedValue;
            //DataRow[] row = dtBlock.Select("id <> " + id_block);
            DataRow[] row = dtBlock.Select();
            dtDeps = row.CopyToDataTable();

            cmbDeps.DataSource = dtDeps;
            cmbDeps.DisplayMember = "name";
            cmbDeps.ValueMember = "id";
        }

        private void cmbBlock_SelectionChangeCommitted(object sender, EventArgs e)
        {
            getDeps();
        }
    }
}
