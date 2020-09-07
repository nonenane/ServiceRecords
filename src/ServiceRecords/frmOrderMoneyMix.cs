using System;
using Nwuram.Framework.Settings.User;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nwuram.Framework.Logging;

namespace ServiceRecords
{
    public partial class frmOrderMoneyMix : Form
    {
        public int id_ServiceRecords { private get; set; }
        public int type { private get; set; }
        public int status { private get; set; }
        public decimal maxSumma { private get; set; }
        public string valuta { private get; set; }
        public bool isEdit { private get;  set; }

        bool checkSumInRub = false;
        public int idOrder = 0;
        public decimal oldSummaCash = 0;
        public decimal oldSummaNonCash = 0;
        public string oldDirector;
        private int oldIdDirector = 0;
        public int idDirector;
        private bool typeCashNonCash;
        private bool ReturnValuta = false;
        DataTable dtDir, userName;

        public frmOrderMoneyMix()
        {
            InitializeComponent();
            ToolTip tt = new ToolTip();
            tt.SetToolTip(btClose, "Выход");
            tt.SetToolTip(btSelect, "Сохранить");

        }
        public frmOrderMoneyMix(string Summa, string Valuta, int idOrder, string oldDirector, int typeCashNonCash, decimal SummaInValuta)
        {
            InitializeComponent();
            tbValuta.Text = valuta;
            tbMoney.Text = Summa;
            if (typeCashNonCash == 1)
            {
                tbSummaNonCash.Text = Summa;
                tbSummaNonCash.Enabled = tbSummaCash.ReadOnly  =  true;
            }
            else
            {
                tbSummaCash.Text = Summa;
                tbSummaCash.Enabled = tbSummaNonCash.ReadOnly = true;
            }
                
            cmbDirector.Text = oldDirector;
            if (cmbDirector.SelectedValue != null)
                oldIdDirector = int.Parse(cmbDirector.SelectedValue.ToString());

            this.idOrder = idOrder;
            this.oldDirector = oldDirector;
            if (typeCashNonCash == 0)
                this.oldSummaCash = decimal.Parse(Summa);
            else
            this.oldSummaNonCash = decimal.Parse(Summa);

            if (SummaInValuta == 0)
                Valuta = "RUB";

            if (Valuta == "RUB" && typeCashNonCash == 0)
                tbSummaCash.Text = Summa;
            else if (Valuta == "RUB" && typeCashNonCash == 1)
                tbSummaNonCash.Text = Summa;
            else if (Valuta != "RUB" && typeCashNonCash == 0)
            {
                tbSummaCash.Text = SummaInValuta.ToString("######0.00");
                tbSumInRubCash.Text = Summa;
                tbCourse.Text = (decimal.Parse(Summa) / SummaInValuta).ToString("######0.00");
            }
            else if (Valuta != "RUB" && typeCashNonCash == 1)
            {
                tbSummaNonCash.Text = SummaInValuta.ToString("######0.00");
                tbSumInRubNonCash.Text = Summa;
                tbCourse.Text = (decimal.Parse(Summa) / SummaInValuta).ToString("######0.00");
            }
        }

        private void frmOrderMoneyMix_Load(object sender, EventArgs e)
        {
            if (type == 1)
            {
                string valueSettings = Config.hCntMain.GetSettings("овпд");
                DateTime dtMoney = DateTime.Now.Date.AddHours(15).AddMinutes(00);
                if (valueSettings.Length > 0)
                    dtMoney = DateTime.Parse(valueSettings);

                if (DateTime.Now.TimeOfDay < dtMoney.TimeOfDay)
                {
                    dtpDate.MinDate = DateTime.Now.Date;
                }
                else
                {
                    dtpDate.MinDate = DateTime.Now.Date.AddDays(1);
                }
                this.Text = "Редактирование получения денег ";
            }
            else
            {
                dtpDate.MinDate = DateTime.Now.Date;
                this.Text = "Редактирование возврата денег ";
            }
            if (type == 2 && valuta != "RUB")
            {
                ReturnValuta = true;
                valuta = "RUB";
            }
            tbValuta.Text = valuta;

            checkValuta();

        }

        private void checkValuta()
        {
            if (!valuta.Equals("RUB") && type!=2)
            {
                tbRUB.Visible = tbSumInRubCash.Visible = checkSumInRub =
                    lbSumInRub.Visible = tbCourse.Visible = label4.Visible = tbSumInRubNonCash.Visible =
                    textBox2.Visible = label7.Visible = true;
            }
            else
            {
                btSelect.Location = new Point(btSelect.Location.X, btClose.Location.Y - 10);
                btClose.Location = new Point(btClose.Location.X, btClose.Location.Y - 10);
                this.Width = 400;
                this.Height = 272;
            }
            
            tbValuta.Text = valuta;

            if (type == 2)
                tbValuta.Text = "RUB";
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

        private void tbMoney_Leave(object sender, EventArgs e)
        {
            if (tbMoney.Text.ToString().Length == 0)
                tbMoney.Text = "0,00";
            else
                tbMoney.Text = decimal.Parse(tbMoney.Text.ToString()).ToString("######0.00");
        }

        private void tbMoney_TextChanged(object sender, EventArgs e)
        {
            tbCourse_TextChanged(sender, e);
            if (!typeCashNonCash && tbSummaCash.Enabled == false)
                tbSummaCash.Text = tbMoney.Text;
            else if (typeCashNonCash && tbSummaCash.Enabled == false) tbSummaNonCash.Text = tbMoney.Text;
        }

        private void tbCourse_TextChanged(object sender, EventArgs e)
        {
            decimal course = 0, summaCash = 0, summaNonCash = 0;
            if (tbCourse.Text.Length > 0)
                decimal.TryParse(tbCourse.Text, out course);
            if (tbSummaCash.Text.Length > 0)
                decimal.TryParse(tbSummaCash.Text, out summaCash);
            if (tbSummaNonCash.Text.Length > 0)
                decimal.TryParse(tbSummaNonCash.Text, out summaNonCash);
            tbMoney.Text = (summaCash + summaNonCash).ToString("######0.00");

            tbSumInRubCash.Text = roundNumber(course * summaCash).ToString("######0.00");
            tbSumInRubNonCash.Text = roundNumber(course * summaNonCash).ToString("######0.00");
        }

        private decimal roundNumber(decimal SumInRubCash)
        {
            return Math.Round(SumInRubCash, MidpointRounding.AwayFromZero);
        }

    public void setDirector()
        {
            dtDir = Config.hCntMain.getDirectors();
            userName = Config.hCntMain.getUserName();
            dtDir.DefaultView.Sort = "id_users ASC";
            dtDir.Rows.Add(0, userName.Rows[0][0].ToString());
            cmbDirector.DataSource = dtDir;
            cmbDirector.DisplayMember = "FIO";
            cmbDirector.ValueMember = "id_users";
            cmbDirector.SelectedIndex = 0;
            cmbDirector.Enabled = false;
            cmbDirector.SelectedValue = UserSettings.User.Id;

            if (idOrder != 0)
            {
                cmbDirector.SelectedValue = idDirector;
            }
        }
        public void getDirectors()
        {
            dtDir = Config.hCntMain.getDirectors();
            dtDir.DefaultView.Sort = "id_users ASC";

            cmbDirector.DataSource = dtDir;
            cmbDirector.DisplayMember = "FIO";
            cmbDirector.ValueMember = "id_users";

        }

        private void chbChangeDirector_TextChanged(object sender, EventArgs e)
        {
            if (chbChangeDirector.Checked == true)
            {
                cmbDirector.Enabled = true;
                getDirectors();
            }
            else
            {
                cmbDirector.Enabled = false;
                setDirector();
            }
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void tbCourse_KeyPress(object sender, KeyPressEventArgs e)
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

        private void btSelect_Click(object sender, EventArgs e)
        {
            decimal summa = decimal.Parse(tbMoney.Text.Trim());
            decimal summaCash = decimal.Parse(tbSummaCash.Text.Trim());
            decimal summaNonCash = decimal.Parse(tbSummaNonCash.Text.Trim());

            DataTable dtHistory = Config.hCntMain.getHistoryOrderAndReturnMix(id_ServiceRecords);
            decimal maxSumma = decimal.Parse(dtHistory.Rows[0]["maxSumma"].ToString());

            decimal balanceGetCash = decimal.Parse(dtHistory.Rows[0]["balanceGetCash"].ToString());
            decimal balanceReturnCash = decimal.Parse(dtHistory.Rows[0]["balanceReturnCash"].ToString());
            decimal balanceGetInValutaCash = decimal.Parse(dtHistory.Rows[0]["balanceGetInValutaCash"].ToString());
            decimal balanceGetInValutaNonCash = decimal.Parse(dtHistory.Rows[0]["balanceGetInValutaNonCash"].ToString());

            decimal balanceGetNonCash = decimal.Parse(dtHistory.Rows[0]["balanceGetNonCash"].ToString());
            decimal balanceReturnNonCash = decimal.Parse(dtHistory.Rows[0]["balanceReturnNonCash"].ToString());

            decimal debtCash = decimal.Parse(dtHistory.Rows[0]["debtReportCash"].ToString()); // для ежемесячногй СЗ
            decimal debtNonCash = decimal.Parse(dtHistory.Rows[0]["debtReportNonCash"].ToString()); // для ежемесячногй СЗ

            int monthDateCreateReportCash = DateTime.Parse(dtHistory.Rows[0]["DateCreateReportCash"].ToString()).Month;
            int monthDateCreateReportNonCash = DateTime.Parse(dtHistory.Rows[0]["DateCreateReportNonCash"].ToString()).Month;

            int yearDateCreateReportCash = DateTime.Parse(dtHistory.Rows[0]["DateCreateReportCash"].ToString()).Year;
            int yearDateCreateReportNonCash = DateTime.Parse(dtHistory.Rows[0]["DateCreateReportNonCash"].ToString()).Year;
            bool isTodayMonthAbdYearCash = DateTime.Now.Month.Equals(monthDateCreateReportCash) && DateTime.Now.Year.Equals(yearDateCreateReportCash);
            bool isTodayMonthAbdYearNonCash = DateTime.Now.Month.Equals(monthDateCreateReportNonCash) && DateTime.Now.Year.Equals(yearDateCreateReportNonCash);

            if (decimal.Parse(tbMoney.Text.ToString()) == 0)
            {
                MessageBox.Show("Необходимо ввести сумму!", "Информирование", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (summa > maxSumma && ReturnValuta == false)
            {
                MessageBox.Show("Сумма превышает \"Сумму СЗ\"!", "Информирование", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (summa > 0  && decimal.Parse(tbSummaCash.Text) == 0 && decimal.Parse(tbSummaNonCash.Text) == 0)
            {
                MessageBox.Show("Заполните сумму в нал. или безнал.!", "Информирование", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (summa > 0 && decimal.Parse(tbSummaCash.Text) + decimal.Parse(tbSummaNonCash.Text) != summa)
            {
                MessageBox.Show(Config.centralText("Сумма нал. и безнал.\n не равна общей сумме \n"), "Информирование", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (checkSumInRub && double.Parse(tbCourse.Text) <= 0)
            {
                MessageBox.Show("Заполните курс валюты!", "Информирование", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (idOrder != 0)
            {
                balanceGetCash += oldSummaCash;
                balanceReturnCash += oldSummaCash;
                balanceGetNonCash += oldSummaNonCash;
                balanceReturnNonCash += oldSummaNonCash;
            }
            
            decimal summaInValutaCash = 0, summaInValutaNonCash = 0;

            if (tbSumInRubCash.Text.Length > 0 ? decimal.Parse(tbSumInRubCash.Text) > 0 ? true : false : false)
            {
                //summaCash = decimal.Parse(tbSummaCash.Text.Trim());
                summaInValutaCash = summaCash;
                if (valuta != "RUB")
                    summaCash = decimal.Parse(tbSumInRubCash.Text.Trim());
            }

            if (tbSumInRubNonCash.Text.Length > 0 ? decimal.Parse(tbSumInRubNonCash.Text) > 0 ? true : false : false)
            {
               //summaNonCash = decimal.Parse(tbSummaNonCash.Text.Trim());
                summaInValutaNonCash = summaNonCash;
                if (valuta != "RUB")
                    summaNonCash = decimal.Parse(tbSumInRubNonCash.Text.Trim());
            }

            if (type == 1 && (balanceGetCash - (checkSumInRub ? summaInValutaCash : summaCash) < 0) && valuta == "RUB") // - (checkSumInRub ? summaInValuta : summa) < 0)
            {
                MessageBox.Show("Вы можете получить нал. не больше " + balanceGetCash, "Информирование", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (type == 1 && (balanceGetNonCash - (checkSumInRub ? summaInValutaNonCash : summaNonCash) < 0) && valuta == "RUB")
            {
                MessageBox.Show("Вы можете получить безнал. не больше " + balanceGetNonCash, "Информирование", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (type == 1 && summaInValutaCash > balanceGetInValutaCash && valuta != "RUB") // - (checkSumInRub ? summaInValuta : summa) < 0)
            {
                MessageBox.Show("Вы можете получить не больше " + balanceGetInValutaCash, "Информирование", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (type == 1 && summaInValutaNonCash > balanceGetInValutaNonCash && valuta != "RUB") // - (checkSumInRub ? summaInValuta : summa) < 0)
            {
                MessageBox.Show("Вы можете получить не больше " + balanceGetInValutaNonCash, "Информирование", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (type == 2 && (checkSumInRub ? summaInValutaCash : summaCash) > balanceReturnCash && valuta == "RUB")
            {
                MessageBox.Show("Вы можете вернуть нал. не больше " + balanceReturnCash, "Информирование", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (type == 2 && (checkSumInRub ? summaInValutaNonCash : summaNonCash) > balanceReturnNonCash && valuta == "RUB")
            {
                MessageBox.Show("Вы можете вернуть безнал. не больше " + balanceReturnNonCash, "Информирование", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (type == 2 && summaCash > balanceReturnCash && valuta != "RUB")
            {
                MessageBox.Show("Вы можете вернуть нал. не больше " + balanceReturnCash, "Информирование", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (type == 2 &&  summaNonCash > balanceReturnNonCash && valuta != "RUB")
            {
                MessageBox.Show("Вы можете вернуть безнал. не больше " + balanceReturnNonCash, "Информирование", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (type == 2 && (checkSumInRub ? summaInValutaCash : summaCash) > debtCash && !isTodayMonthAbdYearCash)
            {
                MessageBox.Show(Config.centralText("Вы можете вернуть по старому долгу\nнал. не больше " + debtCash + "\n"), "Информирование", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (type == 2 && (checkSumInRub ? summaInValutaNonCash : summaNonCash) > debtNonCash && !isTodayMonthAbdYearNonCash)
            {
                MessageBox.Show(Config.centralText("Вы можете вернуть по старому долгу\nбезнал. не больше " + debtNonCash + "\n"), "Информирование", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (DialogResult.Yes == MessageBox.Show("Вы заказываете на имя " + cmbDirector.Text, "Заказ", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
            {

                if (isEdit)
                {
                    DataTable dtTmpData = Config.hCntMain.getServiceRecordsBody(id_ServiceRecords);

                    Logging.StartFirstLevel(1503);
                    Logging.Comment("Произведено редактирование операции по ДС");
                    Logging.Comment("Тип операции:" + (type == 1 ? "Получение" : "Возврат"));
                    //Logging.Comment("ID: " + idOrder);
                    Logging.Comment("Id СЗ: " + id_ServiceRecords);
                    Logging.Comment("Номер СЗ: " + dtTmpData.Rows[0]["Number"].ToString());
//                    Logging.Comment("Номер СЗ: " + dtTmpData.Rows[0]["Number"].ToString());
                    Logging.Comment("Подномер: " + tbNumberSub.Text);
                    //Logging.VariableChange("Сумма", tbMoney.Text, oldSummaNonCash);
                    Logging.Comment("Сумма: " + tbMoney.Text);
                    Logging.Comment("Валюта: " + tbValuta.Text);
                    Logging.Comment("Сумма в нал: " + tbSummaCash.Text);
                    Logging.Comment("Сумма в безнал: " + tbSummaNonCash.Text);

                    Logging.VariableChange("Получатель ID: ", cmbDirector.SelectedValue, oldIdDirector, typeLog._int);
                    Logging.VariableChange("Получатель ФИО: ", cmbDirector.Text, oldDirector == null ? "" : oldDirector);
                    //Logging.VariableChange("Руководитель: ", cmbDirector.Text, oldDirector == null ? "" : oldDirector);
                    Logging.Comment("Предполагаемая дата: " + dtpDate.Value.ToShortDateString());

                    if (!valuta.Equals("RUB") && type != 2)
                    {
                        Logging.Comment("Курс валюты: " + tbCourse.Text);
                        Logging.Comment("Сумма в рублях нал: " + tbSumInRubCash.Text);
                        Logging.Comment("Сумма в рублях безнал: " + tbSumInRubNonCash.Text);
                    }
                }
             


                int idMoneyRecipient;
                idMoneyRecipient = (int)cmbDirector.SelectedValue;
                DataTable dtTMP = new DataTable();
                DataTable dtTMPUpdate = new DataTable();

                if (idOrder == 0 && summaCash > 0)
                    dtTMP = Config.hCntMain.setPayments(id_ServiceRecords,
                                                                  dtpDate.Value,
                                                                  summaCash,
                                                                  type,
                                                                  UserSettings.User.Id,
                                                                  idMoneyRecipient,
                                                                  status,
                                                                  summaInValutaCash,
                                                                  valuta,
                                                                  //oldDebtCash,
                                                                  0); // нал
                else if (idOrder != 0 && summaCash > 0)
                    dtTMPUpdate = Config.hCntMain.updatePayments(idOrder,
                                              dtpDate.Value,
                                              summaCash,
                                              type,
                                              UserSettings.User.Id,
                                              idMoneyRecipient,
                                              status,
                                              summaInValutaCash);

                if (isEdit)
                {

                    if (dtTMPUpdate != null && dtTMPUpdate.Rows.Count > 0 && !dtTMPUpdate.Columns.Contains("error"))
                    {
                        Logging.Comment("ФИО автора СЗ ID: " + dtTMPUpdate.Rows[0]["id_Creator"].ToString() + "; ФИО: " + dtTMPUpdate.Rows[0]["FIO"].ToString());
                    }

                    if (dtTMP != null && dtTMP.Rows.Count > 0 && !dtTMP.Columns.Contains("error"))
                    {
                        Logging.Comment("Статус ДО ID: " + dtTMP.Rows[0]["id_prev"].ToString() + "; Наименование: " + dtTMP.Rows[0]["cName_prev"].ToString());
                        Logging.Comment("Статус После ID: " + dtTMP.Rows[0]["id"].ToString() + "; Наименование: " + dtTMP.Rows[0]["cName"].ToString());
                    }
                }

                if (idOrder == 0 && summaNonCash > 0)
                    dtTMP = Config.hCntMain.setPayments(id_ServiceRecords,
                                                                  dtpDate.Value,
                                                                  summaNonCash,
                                                                  type,
                                                                  UserSettings.User.Id,
                                                                  idMoneyRecipient,
                                                                  status,
                                                                  summaInValutaNonCash,
                                                                  valuta,
                                                                 // oldDebtNonCash,
                                                                  1); //безнал
                else if (idOrder != 0 && summaNonCash > 0)
                    dtTMPUpdate = Config.hCntMain.updatePayments(idOrder,
                                              dtpDate.Value,
                                              summaNonCash,
                                              type,
                                              UserSettings.User.Id,
                                              idMoneyRecipient,
                                              status,
                                              summaInValutaNonCash);
                if (isEdit)
                {
                    if (dtTMPUpdate != null && dtTMPUpdate.Rows.Count > 0 && !dtTMPUpdate.Columns.Contains("error"))
                    {
                        Logging.Comment("ФИО автора СЗ ID: " + dtTMPUpdate.Rows[0]["id_Creator"].ToString() + "; ФИО: " + dtTMPUpdate.Rows[0]["FIO"].ToString());
                    }

                    if (dtTMP != null && dtTMP.Rows.Count > 0 && !dtTMP.Columns.Contains("error"))
                    {
                        Logging.Comment("Статус ДО ID: " + dtTMP.Rows[0]["id_prev"].ToString() + "; Наименование: " + dtTMP.Rows[0]["cName_prev"].ToString());
                        Logging.Comment("Статус После ID: " + dtTMP.Rows[0]["id"].ToString() + "; Наименование: " + dtTMP.Rows[0]["cName"].ToString());
                    }

                    Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                        + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
                    Logging.StopFirstLevel();
                }

                if ((dtTMP == null || dtTMP.Rows.Count == 0) && idOrder == 0)
                {
                    MessageBox.Show("Ошибка добавления.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if (dtTMPUpdate != null ? dtTMPUpdate.Rows.Count > 0 ? true : false : false)
                {
                    if (dtTMPUpdate.Rows[0][0].ToString().Equals("ошибка"))
                        MessageBox.Show("Ошибка редактирования.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.DialogResult = DialogResult.OK;
                }
                if (!isEdit)
                    setLog(id_ServiceRecords, "", 0, type);

                this.DialogResult = DialogResult.OK;
            }
            else return;

        }

        private void chbChangeDirector_CheckedChanged(object sender, EventArgs e)
        {
            if (chbChangeDirector.Checked == true)
            {
                cmbDirector.Enabled = true;
                getDirectors();
            }
            else
            {
                cmbDirector.Enabled = false;
                setDirector();
            }
        }

        private void setLog(int id, string comment, int id_status, int isSend)
        {
            DataTable dtTmpData = Config.hCntMain.getServiceRecordsBody(id);
            if (isSend == 1)
                Logging.StartFirstLevel(1254);
            else
                Logging.StartFirstLevel(1255);

            Logging.Comment("Id СЗ: " + id_ServiceRecords);
            Logging.Comment("Номер СЗ: " + dtTmpData.Rows[0]["Number"].ToString());
            Logging.Comment("Подномер: " + tbNumberSub.Text);
            Logging.Comment("Сумма: " + tbMoney.Text);
            Logging.Comment("Сумма нал:" + tbSummaCash.Text.ToString());
            Logging.Comment("Сумма безнал:" + tbSummaNonCash.Text.ToString());


            Logging.Comment("Предполагаемая дата: " + dtpDate.Value.ToShortDateString());
             
            Logging.Comment(chbChangeDirector.Text + ": " + (chbChangeDirector.Checked ? "Включен" : "Отключен"));
            if (chbChangeDirector.Checked)
                Logging.Comment("Получатель отличается от заказчика");

            Logging.Comment("Руководиель ID: " + (int)dtDir.Rows[cmbDirector.SelectedIndex][0] + "; Наименование:" + cmbDirector.Text.ToString());
            if (!valuta.Equals("RUB") && type != 2)
            {
                Logging.Comment("Курс валюты: " + tbCourse.Text);
                Logging.Comment("Сумма в рублях нал: " + tbSumInRubCash.Text);
                Logging.Comment("Сумма в рублях безнал: " + tbSumInRubNonCash.Text);
            }

            Logging.Comment("Тип СЗ: " + ((int)dtTmpData.Rows[0]["TypeServiceRecord"] == 0 ? "стандарт." : "предварит."));
            Logging.Comment("Тип СЗ по времени: " + ((int)dtTmpData.Rows[0]["TypeServiceRecordOnTime"] == 1 ? "разовая" : "ежемесячная"));
            Logging.Comment("Валюта:" + dtTmpData.Rows[0]["Valuta"].ToString());
            if ((bool)dtTmpData.Rows[0]["Mix"])
            {
                Logging.Comment("Сумма нал:" + dtTmpData.Rows[0]["SummaCash"].ToString());
                Logging.Comment("Сумма безнал:" + dtTmpData.Rows[0]["SummaNonCash"].ToString());
            }

            Logging.Comment("Сумма:" + decimal.Parse(dtTmpData.Rows[0]["Summa"].ToString()).ToString("0.00"));

            Logging.Comment("Объект ID: " + dtTmpData.Rows[0]["id_Object"].ToString() + "; Наименование:" + dtTmpData.Rows[0]["name_Object"].ToString());
            Logging.Comment("Описание, комментарий: " + dtTmpData.Rows[0]["Description"].ToString());
            Logging.Comment("Оплата: " + ((bool)dtTmpData.Rows[0]["Mix"] ? "Смешанный" : (!(bool)dtTmpData.Rows[0]["bCashNonCash"] ? "Наличные" : "Карта")));

            Logging.Comment("Дата создания СЗ: " + ((DateTime)dtTmpData.Rows[0]["CreateServiceRecord"]).ToShortDateString());

            Logging.Comment("Блок ID: " + dtTmpData.Rows[0]["id_Block"].ToString() + "; Наименование:" + dtTmpData.Rows[0]["nameBlock"].ToString());
            Logging.Comment("Отдел ID: " + dtTmpData.Rows[0]["id_Department"].ToString() + "; Наименование:" + dtTmpData.Rows[0]["nameDeps"].ToString());

            Logging.Comment("На рассмотрении РП. Прошу включить в Б: " + ((DateTime)dtTmpData.Rows[0]["MonthB"]).ToShortDateString());

            if (dtTmpData.Rows[0]["bDataSumma"] != DBNull.Value && !(bool)dtTmpData.Rows[0]["bDataSumma"] && dtTmpData.Rows[0]["DataSumma"] != DBNull.Value)
            {
                Logging.Comment("Дата получения ДС (при единовременном получении ДС): " + ((DateTime)dtTmpData.Rows[0]["DataSumma"]).ToShortDateString());

            }
            if (dtTmpData.Rows[0]["DataSumma"] == DBNull.Value && dtTmpData.Rows[0]["bDataSumma"] != DBNull.Value && (bool)dtTmpData.Rows[0]["bDataSumma"])
            {
                DataTable dtMultipleReceivingMone = Config.hCntMain.getMultipleReceivingMone(id);
                Logging.Comment("Дата получения ДС (при распределенном ДС)");

                foreach (DataRow r in dtMultipleReceivingMone.Rows)
                {
                    Logging.Comment("Подномер:" + r["SubNumber"].ToString() + ";Сумма:" + r["Summa"].ToString() + ";Предполагаемая дата:" + r["DataSumma"].ToString());
                }
            }
            Logging.Comment("Комментарий:" + dtTmpData.Rows[0]["Comments"].ToString().Replace(@"\r", "\r\n"));

            if (comment.Length != 0)
                Logging.Comment("Комментрий с формы:" + comment);

            Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
               + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
            Logging.StopFirstLevel();
        }

        private void tbSummaCash_TextChanged(object sender, EventArgs e)
        {
            decimal SummaCash = 0;
            decimal.TryParse(tbSummaCash.Text, out SummaCash);
            tbMoney.Text = (decimal.Parse(tbMoney.Text) + SummaCash).ToString("######0.00");
            if (tbCourse.Text.Length == 0) return;
            tbCourse_TextChanged(sender, e);
        }

        private void tbSummaCash_MouseClick(object sender, MouseEventArgs e)
        {
            decimal value = 0;
            if (!decimal.TryParse(tbSummaCash.Text, out value))
                tbSummaCash.Text = "0,00";
            if (!decimal.TryParse(tbSummaNonCash.Text, out value))
                tbSummaNonCash.Text = "0,00";
        }

        private void tbCourse_Leave(object sender, EventArgs e)
        {
            decimal value = 0;
            if (!decimal.TryParse(tbCourse.Text, out value))
                tbCourse.Text = "0,00";

            if (tbCourse.Text.ToString().Length == 0)
                tbCourse.Text = "0,00";
            else
                tbCourse.Text = decimal.Parse(tbCourse.Text.ToString()).ToString("######0.00");
        }

        private void tbSummaCash_Leave(object sender, EventArgs e)
        {
             decimal value = 0;
            if (!decimal.TryParse(tbSummaCash.Text, out value))
                tbSummaCash.Text = "0,00";

            if (tbSummaCash.Text.ToString().Length == 0)
                tbSummaCash.Text = "0,00";
            else
                tbSummaCash.Text = decimal.Parse(tbSummaCash.Text.ToString()).ToString("######0.00");
        }

        private void tbSummaNonCash_Leave(object sender, EventArgs e)
        {

            decimal value = 0;
            if (!decimal.TryParse(tbSummaNonCash.Text, out value))
                tbSummaNonCash.Text = "0,00";

            if (tbSummaNonCash.Text.ToString().Length == 0)
                tbSummaNonCash.Text = "0,00";
            else
                tbSummaNonCash.Text = decimal.Parse(tbSummaNonCash.Text.ToString()).ToString("######0.00");
        }

        private void tbSummaNonCash_TextChanged(object sender, EventArgs e)
        {
            decimal SummaNonCash = 0;
            decimal.TryParse(tbSummaNonCash.Text, out SummaNonCash);
            tbMoney.Text = (decimal.Parse(tbMoney.Text) + SummaNonCash).ToString("######0.00");
            if (tbCourse.Text.Length == 0) return;
            tbCourse_TextChanged(sender,e);
        }
    }
}
