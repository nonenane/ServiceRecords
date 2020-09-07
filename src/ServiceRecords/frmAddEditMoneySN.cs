using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ServiceRecords
{
    public partial class frmAddEditMoneySN : Form
    {
        public int id_ServiceRecords { private get; set; }
        public DataRowView  row { private get; set; }
        public DataTable dtMultipleReceivingMone { set; get; }
        public decimal summaDC { set; private get; }
        decimal Allsumma;
        private int id = -1;

        public frmAddEditMoneySN()
        {
            InitializeComponent();
            //System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");       
            ToolTip tt = new ToolTip();
            tt.SetToolTip(btClose, "Выход");
            tt.SetToolTip(btSelect, "Сохранить");
        }

        private void frmAddEditMoneySN_Load(object sender, EventArgs e)
        {
            dtpDate.MinDate = DateTime.Now.Date;
            if (row != null)
            {
                id = (int)row["id"];
                tbNumberSub.Text = row["SubNumber"].ToString();
                tbMoney.Text = decimal.Parse(row["Summa"].ToString()).ToString("0.00");
                try
                {
                    dtpDate.Value = DateTime.Parse(row["DataSumma"].ToString());
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Data summa  " + ex.Message);
                }
            }
        }

        private void tbMoney_KeyPress(object sender, KeyPressEventArgs e)
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

        private void btClose_Click(object sender, EventArgs e)
        {
            
            this.DialogResult = DialogResult.Cancel;
        }

        private void btSelect_Click(object sender, EventArgs e)
        {
            if (decimal.Parse(tbMoney.Text.ToString()) == 0)
            {
                MessageBox.Show("Необходимо ввести сумму!", "Информирование", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            decimal summa = decimal.Parse(tbMoney.Text.Trim());

            object tSumma = dtMultipleReceivingMone.Compute("SUM(Summa)", "id <> " + id);

            if (tSumma == DBNull.Value)
            {
                tSumma = 0;
            }
            tSumma = decimal.Parse(tSumma.ToString()) + summa;
            //foreach (DataRow r in dtMultipleReceivingMone.Rows)
            //{
            //    Allsumma += decimal.Parse(r["Summa"].ToString());
            //}

            //Allsumma += decimal.Parse(tbMoney.Text.Trim());
            if (decimal.Parse(tSumma.ToString()) > summaDC )//|| Allsumma > summaDC)
            {
                MessageBox.Show("Сумма выплат больше суммы ДС!", "Информирование", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Allsumma -= decimal.Parse(tbMoney.Text.Trim());
                return;
            }

            if (id_ServiceRecords == -1)
            {
                if (row == null)
                {
                    dtMultipleReceivingMone.Rows.Add(tbNumberSub.Text.Trim(), summa, dtpDate.Value, -1);
                    dtMultipleReceivingMone.AcceptChanges();
                }
                else
                {
                    row["SubNumber"] = tbNumberSub.Text.Trim();
                    row["Summa"] = summa;
                    row["DataSumma"] = dtpDate.Value;
                    dtMultipleReceivingMone.AcceptChanges();
                }
            }
            else
            {
                DataTable dtTMP = Config.hCntMain.setMultipleReceivingMoney(id_ServiceRecords, tbNumberSub.Text.Trim(), summa, dtpDate.Value, id, false);

                if (dtTMP == null || dtTMP.Rows.Count == 0)
                {
                    MessageBox.Show("Ошибка добавления");
                    return;
                }
            }
            this.DialogResult = DialogResult.OK;
        }

        private void tbMoney_Leave(object sender, EventArgs e)
        {
            if (tbMoney.Text.ToString().Length == 0)
                tbMoney.Text = "0,00";
            else             
                tbMoney.Text = decimal.Parse(tbMoney.Text.ToString()).ToString("######0.00");
        }

        
    }
}
