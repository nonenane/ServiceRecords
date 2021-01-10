using Nwuram.Framework.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ServiceRecords.docmoverDZ
{
    public partial class frmEdit : Form
    {
        private bool isEditData = false;
        private decimal _sumBonus = 0;

        public DataRowView row { set; get; }
        public frmEdit()
        {
            InitializeComponent();
        }

        private void tbSumBonus_KeyPress(object sender, KeyPressEventArgs e)
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

        private void frmEdit_Load(object sender, EventArgs e)
        {
            if (row != null)
            {
                tbDate.Text = ((DateTime)row["date_create"]).ToShortDateString();
                tbType.Text = (string)row["DistrType"];
                tbDep.Text = (string)row["depPenalty"];
                tbSumPenalty.Text = ((decimal)row["sumPenalty"]).ToString("0.00");
                tbSumBonus.Text = ((decimal)row["SumBonus"]).ToString("0.00");
                tbFIO.Text = (string)row["FIOBonus"];
                _sumBonus = (decimal)row["SumBonus"];
            }

            isEditData = false;
        }

        private void tbSumBonus_Leave(object sender, EventArgs e)
        {
            if (tbSumBonus.Text.Trim().Length == 0) { tbSumBonus.Text = "0,00"; return; }
            decimal tmpValue;
            if (!decimal.TryParse(tbSumBonus.Text, out tmpValue)) { tbSumBonus.Text = "0,00"; return; }
            if (tmpValue > 100000) tmpValue = 99999.99M;
            //if (tmpValue > 100) { tbMinSumma.Text = "100,00"; return; }
            tbSumBonus.Text = tmpValue.ToString("0.00");
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            decimal tmpValue;
            if (!decimal.TryParse(tbSumBonus.Text, out tmpValue)) return;

            int? id_ListServiceRecords = null;
            if (row["id_ListServiceRecords"] != DBNull.Value)
                id_ListServiceRecords = (int)row["id_ListServiceRecords"];

            int id = (int)row["id"];

            //if (tmpValue == _sumBonus)
            //{
            //    MessageBox.Show("Начальное и конечно значение сумму премии должно различатся!","Информирование",MessageBoxButtons.OK,MessageBoxIcon.Information);
            //    return; }

            Logging.StartFirstLevel(3497);

            Logging.Comment($"ID записи:{row["id"]}");
            Logging.Comment($"№ ДЗ:{row["no_doc"]}");
            Logging.Comment($"Дата:{row["date_create"]}");
            Logging.Comment($"Отдел нарушителя:{row["depPenalty"]}");
            Logging.Comment($"Заголовок ДЗ:{row["cname"]}");
            Logging.Comment($"Тип нарушения:{row["DistrType"]}");
            Logging.Comment($"Сумма нарушения:{row["sumPenalty"]}");
            Logging.VariableChange($"Сумма премии:",tmpValue, row["SumBonus"],typeLog._decimal);
            Logging.Comment($"Сотрудник, обнаружевший нарушение:{row["FIOBonus"]}");

            Logging.StopFirstLevel();

            Config.hCntMain.setMemorandums(id, id_ListServiceRecords, tmpValue, true, false, true);

            row["SumBonus"] = tmpValue;
            row["isEdit"] = 1;

            isEditData = false;
            MessageBox.Show("Данные сохранены.", "Сохранение данных", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.DialogResult = DialogResult.OK;
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void frmEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = isEditData && DialogResult.No == MessageBox.Show("На форме есть не сохранённые данные.\nЗакрыть форму без сохранения данных?\n", "Закрытие формы", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

        }

        private void tbSumBonus_TextChanged(object sender, EventArgs e)
        {           
            isEditData = true;
            try
            {
                decimal tmpValue;
                if (!decimal.TryParse(tbSumBonus.Text, out tmpValue)) { btSave.Enabled = false; return; }

                btSave.Enabled = _sumBonus != tmpValue;
            }
            catch
            {
                btSave.Enabled = false;
            }
        }
    }
}
