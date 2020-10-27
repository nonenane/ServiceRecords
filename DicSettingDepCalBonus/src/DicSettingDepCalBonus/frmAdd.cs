using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DicSettingDepCalBonus
{
    public partial class frmAdd : Form
    {
        public DataRowView row { set;private get; }

        private int id = -1;
        public frmAdd()
        {
            InitializeComponent();
        }

        private void tbMinSumma_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == '.')
            {
                e.KeyChar = ',';
            }

            if ((e.KeyChar == ',') && ((sender as TextBox).Text.ToString().Contains(e.KeyChar) || (sender as TextBox).Text.ToString().Length == 0))
            {
                e.Handled = true;
            }
            else
                if ((!Char.IsNumber(e.KeyChar) && (e.KeyChar != ',')))
            {
                if (e.KeyChar != '\b')
                { e.Handled = true; }
            }

        }

        private void tbPercent_Leave(object sender, EventArgs e)
        {
            if (tbPercent.Text.Trim().Length == 0) { tbPercent.Text = "0,00"; return; }
            decimal tmpValue;
            if (!decimal.TryParse(tbPercent.Text, out tmpValue)) { tbPercent.Text = "0,00"; return; }
            if (tmpValue > 100) { tbPercent.Text = "100,00"; return; }
            tbPercent.Text = tmpValue.ToString("0.00");
        }

        private void tbMinSumma_Leave(object sender, EventArgs e)
        {
            if (tbMinSumma.Text.Trim().Length == 0) { tbMinSumma.Text = "0,00"; return; }
            decimal tmpValue;
            if (!decimal.TryParse(tbMinSumma.Text, out tmpValue)) { tbMinSumma.Text = "0,00"; return; }
            //if (tmpValue > 100) { tbMinSumma.Text = "100,00"; return; }
            tbMinSumma.Text = tmpValue.ToString("0.00");
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            if (cmbDeps.SelectedValue == null || cmbDeps.SelectedIndex == -1)
            {
                MessageBox.Show(Config.centralText($"Необходимо выбрать\n \"{lDeps.Text}\"\n"), "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbDeps.Focus();
                return;
            }

            if (tbMinSumma.Text.Trim().Length == 0)
            {
                MessageBox.Show(Config.centralText($"Необходимо заполнить\n \"{lMinSum.Text}\"\n"), "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbMinSumma.Focus();
                return;
            }

            if (tbPercent.Text.Trim().Length == 0)
            {
                MessageBox.Show(Config.centralText($"Необходимо заполнить\n \"{lPercent.Text}\"\n"), "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbPercent.Focus();
                return;
            }

            int id_otdel = (int)cmbDeps.SelectedValue;

            decimal MinPayment = decimal.Parse(tbMinSumma.Text);
            decimal PercentPayment = decimal.Parse(tbPercent.Text);

            Task<DataTable> task = Config.hCntMain.setDepartmentBonus(id, id_otdel, MinPayment, PercentPayment, false);
            task.Wait();

            DataTable dtResult = task.Result;

            if (dtResult == null || dtResult.Rows.Count == 0)
            {
                MessageBox.Show("Не удалось сохранить данные", "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if ((int)dtResult.Rows[0]["id"] == -1)
            {
                MessageBox.Show(Config.centralText($"{dtResult.Rows[0]["msg"].ToString().Replace("\\n","\n")}"), "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if ((int)dtResult.Rows[0]["id"] == -9999)
            {
                MessageBox.Show($"{dtResult.Rows[0]["msg"]}", "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            MessageBox.Show("Данные сохранены.", "Сохранение данных", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.DialogResult = DialogResult.OK;
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void frmAdd_Load(object sender, EventArgs e)
        {
            cmbDeps.DataSource = Config.hCntMain.getDepartments();
            cmbDeps.ValueMember = "id";
            cmbDeps.DisplayMember = "cName";
            cmbDeps.SelectedIndex = -1;

            if (row != null)
            {
                id = (int)row["id"];
                cmbDeps.SelectedValue = (int)row["id_departments"];
                tbMinSumma.Text = ((decimal)row["MinPayment"]).ToString("0.00");
                tbPercent.Text = ((decimal)row["PercentPayment"]).ToString("0.00");
                cmbDeps.Enabled = false;
            }
        }

        private void cmbDeps_DropDown(object sender, EventArgs e)
        {
            var senderComboBox = (ComboBox)sender;
            int width = senderComboBox.DropDownWidth;
            Graphics g = senderComboBox.CreateGraphics();
            Font font = senderComboBox.Font;

            int vertScrollBarWidth = (senderComboBox.Items.Count > senderComboBox.MaxDropDownItems)
                    ? SystemInformation.VerticalScrollBarWidth : 0;

            DataTable dtList = (DataTable)senderComboBox.DataSource;
            
            foreach (DataRow r in dtList.Rows)
            {
                string s = "";
                if (dtList.Columns.Contains("cname"))
                    s = (string)r["cname"];
                else
                    if (dtList.Columns.Contains("name"))
                    s = (string)r["name"];
                else
                    if (dtList.Columns.Contains("cName"))
                    s = (string)r["cName"];

                int newWidth = (int)g.MeasureString(s, font).Width + vertScrollBarWidth;

                if (width < newWidth)
                {
                    width = newWidth;
                }
            }

            senderComboBox.DropDownWidth = width;
        }
    }
}
