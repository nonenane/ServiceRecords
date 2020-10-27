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
    public partial class frmList : Form
    {
        private DataTable dtData;

        public frmList()
        {
            InitializeComponent();

            dgvData.AutoGenerateColumns = false;

            ToolTip tp = new ToolTip();
            tp.SetToolTip(btClose, "Выход");
            tp.SetToolTip(btAdd, "Добавить");
            tp.SetToolTip(btEdit, "Редактировать");
            tp.SetToolTip(btDel, "Удалить");
        }

        private void frmList_Load(object sender, EventArgs e)
        {
            getData();
        }

        private void dgvData_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            int width = 0;
            foreach (DataGridViewColumn col in dgvData.Columns)
            {
                if (!col.Visible) continue;

                if (col.Name.Equals(cDep.Name))
                {
                    tbNameDeps.Location = new Point(dgvData.Location.X + 1 + width, tbNameDeps.Location.Y);
                    tbNameDeps.Size = new Size(cDep.Width, tbNameDeps.Size.Height);
                }

                width += col.Width;
            }
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == new frmAdd() { Text = "Добавить отдел" }.ShowDialog())
                getData();
        }

        private void btEdit_Click(object sender, EventArgs e)
        {
            if (dgvData.CurrentRow != null && dgvData.CurrentRow.Index != -1 && dtData != null && dtData.DefaultView.Count != 0)
            {
                DataRowView row = dtData.DefaultView[dgvData.CurrentRow.Index];
                if (DialogResult.OK == new frmAdd() { Text = "Редактировать отдел", row = row }.ShowDialog())
                    getData();
            }
        }

        private void btDel_Click(object sender, EventArgs e)
        {
            DataRowView row = dtData.DefaultView[dgvData.CurrentRow.Index];
            int id = (int)row["id"];
            int id_otdel = (int)row["id_departments"];
            decimal MinPayment = (decimal)row["MinPayment"];
            decimal PercentPayment = (decimal)row["PercentPayment"];

            if (DialogResult.Yes == MessageBox.Show("Удалить выбранную запись?", "Удаление записи", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
            {
                Task<DataTable> task = Config.hCntMain.setDepartmentBonus(id, id_otdel, MinPayment, PercentPayment, true);                
                task.Wait();
                if (task.Result == null)
                {
                    MessageBox.Show(Config.centralText("При сохранение данных возникли ошибки записи.\nОбратитесь в ОЭЭС\n"), "Сохранение данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                getData();
                return;
            }
        }

        private void tbNameDeps_TextChanged(object sender, EventArgs e)
        {
            setFilter();
        }

        private void getData()
        {
            dtData = Config.hCntMain.getDepartmentBonus();
            setFilter();
            dgvData.DataSource = dtData;
        }

        private void setFilter()
        {
            if (dtData == null || dtData.Rows.Count == 0)
            {
                btEdit.Enabled = btDel.Enabled = false;
                return;
            }

            try
            {
                string filter = "";

                if (tbNameDeps.Text.Trim().Length != 0)
                    filter += (filter.Length == 0 ? "" : " and ") + $"nameDep like '%{tbNameDeps.Text.Trim()}%'";

                dtData.DefaultView.RowFilter = filter;
            }
            catch
            {
                dtData.DefaultView.RowFilter = "id = -1";
            }
            finally
            {
                btEdit.Enabled = btDel.Enabled =
                dtData.DefaultView.Count != 0;
                dgvData_SelectionChanged(null, null);
            }
        }

        private void dgvData_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvData.CurrentRow == null || dgvData.CurrentRow.Index == -1 || dtData == null || dtData.DefaultView.Count == 0 || dgvData.CurrentRow.Index >= dtData.DefaultView.Count)
            {
                btDel.Enabled = false;
                btEdit.Enabled = false;
                return;
            }

            btDel.Enabled = true;
            btEdit.Enabled = true;
            //btEdit.Enabled = (bool)dtData.DefaultView[dgvData.CurrentRow.Index]["isActive"];
        }
    }
}
