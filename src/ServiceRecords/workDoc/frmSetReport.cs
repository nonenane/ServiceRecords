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
    public partial class frmSetReport : Form
    {
        public int id_ServiceRecords { get; set; }
        public int numberSR { get; set; }
        public int typeSZ { get; set; }
        public decimal Summa { get; set; }
        public string Valuta { get; set; }

        public int? inType { set; private get; }

        public bool Mix;
        private DataTable dtHistory = new DataTable();

        public frmSetReport()
        {
            InitializeComponent();
            ToolTip tp = new ToolTip();
            tp.SetToolTip(btViewHardwareList, "Просмотр компьютерного оборудования");
        }

        private void frmSetReport_Load(object sender, EventArgs e)
        {
            Config.bufferDataTable = null;
            btViewHardwareList.Visible = (inType != null && inType == 1);
            create_cbTypeOrderMoney();
            update();
        }

        private void update()
        {
            ServiceRecordsInf SZ = new ServiceRecordsInf();
            getValue(SZ);
            setValue(SZ);
        }

        private void getValue(ServiceRecordsInf SZ)
        {
            SZ.id = id_ServiceRecords;
            SZ.Valuta = Valuta;
            SZ.typeCashNonCash = typeSZ;

            if (Mix)
            {
                dtHistory = Config.hCntMain.getHistoryOrderAndReturnMix(id_ServiceRecords);
                if (dtHistory != null ? dtHistory.Rows.Count > 0 ? false : true : true) return;

                if (decimal.Parse(dtHistory.Rows[0]["sumGetCash"].ToString()) != 0)
                    SZ.sumGetCashMinusReturn = decimal.Parse(dtHistory.Rows[0]["sumGetCash"].ToString()) - decimal.Parse(dtHistory.Rows[0]["sumReturnCash"].ToString());
                else SZ.sumGetCashMinusReturn = decimal.Parse(dtHistory.Rows[0]["sumGetLastMonthCash"].ToString()) - decimal.Parse(dtHistory.Rows[0]["sumReturnCash"].ToString());
                if (decimal.Parse(dtHistory.Rows[0]["sumGetNonCash"].ToString()) != 0)
                    SZ.sumGetNonCashMinusReturn = decimal.Parse(dtHistory.Rows[0]["sumGetNonCash"].ToString()) - decimal.Parse(dtHistory.Rows[0]["sumReturnNonCash"].ToString());
                else SZ.sumGetNonCashMinusReturn = decimal.Parse(dtHistory.Rows[0]["sumGetLastMonthNonCash"].ToString()) - decimal.Parse(dtHistory.Rows[0]["sumReturnNonCash"].ToString());
                if (cbTypeOrderMoney.SelectedValue.ToString() == "0")
                    SZ.oldSummaReportCash = decimal.Parse(dtHistory.Rows[0]["sumReportCash"].ToString());
                else
                    SZ.oldSummaReportNonCash = decimal.Parse(dtHistory.Rows[0]["sumReportNonCash"].ToString());
            }
            else
            {
                dtHistory = Config.hCntMain.getHistoryOrderAndReturn(id_ServiceRecords);
                if (dtHistory != null ? dtHistory.Rows.Count > 0 ? false : true : true) return;

                if (decimal.Parse(dtHistory.Rows[0]["sumGet"].ToString()) != 0)
                    SZ.sumGetMinusReturn = decimal.Parse(dtHistory.Rows[0]["sumGet"].ToString()) - decimal.Parse(dtHistory.Rows[0]["sumReturn"].ToString());
                else SZ.sumGetMinusReturn = decimal.Parse(dtHistory.Rows[0]["sumGetLastMonth"].ToString()) - decimal.Parse(dtHistory.Rows[0]["sumReturn"].ToString());
                SZ.oldSummaReport = decimal.Parse(dtHistory.Rows[0]["sumReport"].ToString());

            }
        }

        private void setValue(ServiceRecordsInf SZ)
        {
            txNumberSR.Text = numberSR.ToString();
            tbSumma.Text = Summa.ToString();
            if (Mix)
            {
                cbTypeOrderMoney.Enabled = true;
                if (cbTypeOrderMoney.SelectedValue.ToString() == "0")
                {
                    tbSummaAmountReceived.Text = SZ.sumGetCashMinusReturn.ToString("### ### ##0.00");
                    tbSummaInReport.Text = SZ.oldSummaReportCash.ToString("### ### ##0.00");
                }
                else
                {
                    tbSummaAmountReceived.Text = SZ.sumGetNonCashMinusReturn.ToString("### ### ##0.00");
                    tbSummaInReport.Text = SZ.oldSummaReportNonCash.ToString("### ### ##0.00");
                }
            }

            else
            {
                cbTypeOrderMoney.SelectedValue = typeSZ;
                tbSummaAmountReceived.Text = SZ.sumGetMinusReturn.ToString("### ### ##0.00");
                tbSummaInReport.Text = SZ.oldSummaReport.ToString("### ### ##0.00");
            }

            if (decimal.Parse(tbSummaAmountReceived.Text.Replace(" ", "")) >= decimal.Parse(tbSummaInReport.Text.Replace(" ", "")))
                tbDebt.Text = (decimal.Parse(tbSummaAmountReceived.Text) - decimal.Parse(tbSummaInReport.Text)).ToString("### ### ##0.00");
            lbSummaInValuta.Text = Valuta;
        }


        private void create_cbTypeOrderMoney()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id", typeof(int));
            dt.Columns.Add("name", typeof(string));
            dt.Rows.Add(0, "Нал.");
            dt.Rows.Add(1, "Безнал.");

            cbTypeOrderMoney.ValueMember = "id";
            cbTypeOrderMoney.DisplayMember = "name";
            cbTypeOrderMoney.DataSource = dt;
        }
        private void tsmiAddDoc_Click(object sender, EventArgs e)
        {
            workDoc.frmDocument frmD = new workDoc.frmDocument(false);
            frmD.id_ServiceRecords = id_ServiceRecords;
            frmD.typeDoc = 3;
            frmD.ShowDialog();
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            decimal SummaReport = 0, Debt = 0;
            decimal.TryParse(tbSummaInReport.Text, out SummaReport);
            decimal.TryParse(tbDebt.Text.Replace(" ", ""), out Debt);

            if (tbSummaInReport.Text.Length < 1 || SummaReport < 0 || Debt < 0)
            {
                MessageBox.Show("Введите корректную сумму отчета.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DataTable dtScan = Config.hCntMain.getScan(id_ServiceRecords, -1);
            if (dtScan.Select("TypeScan = 3").ToList().Count < 1)
            {
                MessageBox.Show(Config.centralText("Вы не прикрепили к отчету\nподтверждающую документацию.\nСохранение отчета невозможно!\n"), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DataTable dtResult = null;
            if (inType != null && inType == 1)
            {
                dtResult = Config.hCntMain.getListHardwareForServiceRecord(id_ServiceRecords);
                if (dtResult == null || dtResult.Rows.Count == 0)
                {
                    MessageBox.Show(Config.centralText("У выбранной СЗ есть признак \"Закупка компьютерного оборудования\".\nДля сохранения отчёта требуется ввести данные по оборудованию\n в учёте компьютерного оборудования.\n"), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }


            //Сохранение отчета
            Config.hCntMain.updateReport(id_ServiceRecords, SummaReport, decimal.Parse(tbDebt.Text), (int)cbTypeOrderMoney.SelectedValue);

            dtResult = null;

            //Сохранение документов
            if (Config.bufferDataTable != null && Config.bufferDataTable.Rows.Count > 0)
                foreach (DataRow row in Config.bufferDataTable.Rows)
                {
                    string fileName = row["cName"].ToString();
                    byte[] byteFile = (byte[])row["img"];
                    int TypeScan = (int)row["TypeScan"];
                    string @Extension = (string)row["Extension"];
                    Config.hCntMain.setScan(id_ServiceRecords, byteFile, fileName, TypeScan, @Extension);
                }

            decimal debtReportCash = 0, debtReportNonCash = 0;
            if (Mix)
            {
                debtReportCash = (decimal)Config.hCntMain.getHistoryOrderAndReturnMix(id_ServiceRecords).Rows[0]["debtReportCash"];
                debtReportNonCash = (decimal)Config.hCntMain.getHistoryOrderAndReturnMix(id_ServiceRecords).Rows[0]["debtReportNonCash"];
            }

            // Вывод сообщения и обновление статуса
            if (double.Parse(tbDebt.Text) == 0 && ((Mix && debtReportCash == 0
                    && debtReportNonCash == 0) || !Mix))
            {
                dtResult = Config.hCntMain.updateStatus(id_ServiceRecords, 15); // 15 - отчет предоставлен
                MessageBox.Show(Config.centralText("Отчет предоставлен.\n Ожидайте проверки отчета оператором.\n"), "Предоставление отчета", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                // Обновление статуса
                if ((double.Parse(tbDebt.Text) != 0 && !Mix) || Mix)
                    dtResult = Config.hCntMain.updateStatus(id_ServiceRecords, 14);

                //Вывод сообщений
                if (Mix && (int)cbTypeOrderMoney.SelectedValue == 0)
                {
                    string message = double.Parse(tbDebt.Text) > 0 ? "Данные по нал. сохранены\n Ваш долг по нал. составляет " + double.Parse(tbDebt.Text) + " р. \nНеобходимо произвести возврат ДС, \nлибо предоставить повторный отчет\n" : "Данные по нал. сохранены.\n";
                    MessageBox.Show(Config.centralText(message), "Предоставление отчета", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (debtReportNonCash > 0)
                    {
                        MessageBox.Show(Config.centralText("Ваш долг по безнал.: " + debtReportNonCash + " р. \nНеобходимо произвести возврат ДС, \nлибо предоставить отчет.\n"), "Предоставление отчета", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        inserLog(dtResult);
                        return;
                    }
                }
                else if (Mix && (int)cbTypeOrderMoney.SelectedValue == 1)
                {
                    string message = double.Parse(tbDebt.Text) > 0 ? "Данные по безнал. сохранены.\nВаш долг по безнал. составляет " + double.Parse(tbDebt.Text) + " р. \nНеобходимо произвести возврат ДС, \nлибо предоставить повторный отчет.\n" : "Данные по безнал. сохранены.\n";
                    MessageBox.Show(Config.centralText(message), "Предоставление отчета", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (debtReportCash > 0)
                    {
                        MessageBox.Show(Config.centralText("Ваш долг по нал.: " + debtReportCash + " р. \nНеобходимо произвести возврат ДС, \nлибо предоставить отчет.\n"), "Предоставление отчета", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        inserLog(dtResult);
                        return;
                    }
                }
                else MessageBox.Show(Config.centralText("Данные сохранены.\nВаш долг составляет " + double.Parse(tbDebt.Text) + " р. \nНеобходимо произвести возврат ДС,\nлибо предоставить повторный отчет.\n"), "Отчет", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
            inserLog(dtResult);

            /*
            Logging.StartFirstLevel(1502);
            Logging.Comment("Произведено предоставление отчета по СЗ");
            Logging.Comment("ID: " + id_ServiceRecords);
            Logging.Comment("№ СЗ: " + numberSR);
            Logging.Comment("Тип оплаты: " + cbTypeOrderMoney.Text);
            Logging.Comment("Общая сумма по СЗ: " + tbSumma.Text + " "+ lbSummaInValuta.Text);
            Logging.Comment("Взято: " + tbSummaAmountReceived.Text + " RUB");
            Logging.Comment("Сумма отчета: " + tbSummaInReport.Text + " RUB");
            Logging.Comment("Долг: " + tbDebt.Text + " RUB");

            if (Config.bufferDataTable != null && Config.bufferDataTable.Rows.Count > 0)
                foreach (DataRow row in Config.bufferDataTable.Rows)
                {
                    int TypeScan = (int)row["TypeScan"];
                    string tas = TypeScan == 1 ? "к описанию СЗ (при создании и редактировании)" : (TypeScan == 2 ? "при оплате безналом (обязательное добавление)" : "отчет по тратам ДС по СЗ");

                    Logging.Comment("Добавление документа: Тип документа: " + tas + "; Наименование: " + row["cName"].ToString());                    
                }

            if (dtResult != null && dtResult.Rows.Count>0 && !dtResult.Columns.Contains("error"))
            {
                //Logging.Comment("Статус ДО ID: " + dtResult.Rows[0]["id_prev"].ToString() + "; Наименование: " + dtResult.Rows[0]["cName_prev"].ToString());
                //Logging.Comment("Статус После ID: " + dtResult.Rows[0]["id"].ToString() + "; Наименование: " + d);

                Logging.VariableChange("Статус ID: ", dtResult.Rows[0]["id"].ToString(), dtResult.Rows[0]["id_prev"].ToString());
                Logging.VariableChange("Статус Наименование: ", dtResult.Rows[0]["cName"].ToString(), dtResult.Rows[0]["cName_prev"].ToString());
            }

            Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
            Logging.StopFirstLevel();*/

            this.DialogResult = DialogResult.OK;
        }

        private void inserLog(DataTable dtResult)
        {
            Logging.StartFirstLevel(1502);
            Logging.Comment("Произведено предоставление отчета по СЗ");
            Logging.Comment("ID: " + id_ServiceRecords);
            Logging.Comment("№ СЗ: " + numberSR);
            Logging.Comment("Тип оплаты: " + cbTypeOrderMoney.Text);
            Logging.Comment("Общая сумма по СЗ: " + tbSumma.Text + " " + lbSummaInValuta.Text);
            Logging.Comment("Взято: " + tbSummaAmountReceived.Text + " RUB");
            Logging.Comment("Сумма отчета: " + tbSummaInReport.Text + " RUB");
            Logging.Comment("Долг: " + tbDebt.Text + " RUB");

            if (Config.bufferDataTable != null && Config.bufferDataTable.Rows.Count > 0)
                foreach (DataRow row in Config.bufferDataTable.Rows)
                {
                    int TypeScan = (int)row["TypeScan"];
                    string tas = TypeScan == 1 ? "к описанию СЗ (при создании и редактировании)" : (TypeScan == 2 ? "при оплате безналом (обязательное добавление)" : "отчет по тратам ДС по СЗ");

                    Logging.Comment("Добавление документа: Тип документа: " + tas + "; Наименование: " + row["cName"].ToString());
                }

            if (dtResult != null && dtResult.Rows.Count > 0 && !dtResult.Columns.Contains("error"))
            {
                //Logging.Comment("Статус ДО ID: " + dtResult.Rows[0]["id_prev"].ToString() + "; Наименование: " + dtResult.Rows[0]["cName_prev"].ToString());
                //Logging.Comment("Статус После ID: " + dtResult.Rows[0]["id"].ToString() + "; Наименование: " + d);

                Logging.VariableChange("Статус ID: ", dtResult.Rows[0]["id"].ToString(), dtResult.Rows[0]["id_prev"].ToString());
                Logging.VariableChange("Статус Наименование: ", dtResult.Rows[0]["cName"].ToString(), dtResult.Rows[0]["cName_prev"].ToString());
            }

            Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
            Logging.StopFirstLevel();
        }


        private void tbSummaInReport_TextChanged(object sender, EventArgs e)
        {
            decimal SummaReport = 0, SummaReceived = 0;
            decimal.TryParse(tbSummaInReport.Text, out SummaReport);
            decimal.TryParse(tbSummaAmountReceived.Text, out SummaReceived);
            tbDebt.Text = (SummaReceived - SummaReport).ToString("### ### ##0.00");
        }

        private void tbSummaInReport_KeyPress(object sender, KeyPressEventArgs e)
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

        private void cbTypeOrderMoney_SelectedIndexChanged(object sender, EventArgs e)
        {
            update();
        }

        private void tbSummaInReport_Leave(object sender, EventArgs e)
        {
            decimal value = 0;
            if (decimal.TryParse(tbSummaInReport.Text, out value))
                return;
            else tbSummaInReport.Text = "0,00";

            if (tbSummaInReport.Text.ToString().Length == 0)
                tbSummaInReport.Text = "0,00";
            else
                tbSummaInReport.Text = decimal.Parse(tbSummaInReport.Text.ToString()).ToString("######0.00");
        }

        private void btViewHardwareList_Click(object sender, EventArgs e)
        {            
            new HardWare.frmListHardware() { id_ServiceRecod = id_ServiceRecords }.ShowDialog();
        }
    }
}
