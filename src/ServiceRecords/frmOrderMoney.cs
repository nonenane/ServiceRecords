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
    public partial class frmOrderMoney : Form
    {
        public int id_ServiceRecords { private get; set; }
        public int inType { private get; set; }
        public int type { private get; set; }
        public int status { private get; set; }
        public decimal maxSumma { private get; set; }
        public string valuta { private get; set; }
        public bool isEdit { private get;  set; }        
        public int TypeServiceRecordOnTime { private get; set; }

        bool checkSumInRub = false;
        public int idOrder = 0;
        public decimal oldSumma = 0;
        public string oldDirector;
        public int idDirector;
        private int oldIdDirector = 0;
        private DateTime nowTime;
        public DateTime? DataSumma { set; private get; }

        public frmOrderMoney()
        {
            InitializeComponent();
            nowTime = Config.hCntMain.GetDate();
        }

        public frmOrderMoney(string Summa, string Valuta, int idOrder, string oldDirector, decimal SummaInValuta)
        {
            InitializeComponent();
            nowTime = Config.hCntMain.GetDate();
            this.valuta = Valuta;
            cmbDirector.Text = oldDirector;
            if (cmbDirector.SelectedValue != null)
                oldIdDirector = int.Parse(cmbDirector.SelectedValue.ToString());

            this.idOrder = idOrder;
            this.oldDirector = oldDirector;
            this.oldSumma = decimal.Parse(Summa);
            if (SummaInValuta == 0)
                Valuta = "RUB";
            if (Valuta == "RUB")
                tbMoney.Text = Summa;
            else
            {
                tbMoney.Text = SummaInValuta.ToString("######0.00");
                tbSumInRub.Text = decimal.Parse(Summa).ToString("######0.00"); ;
                tbCourse.Text = (decimal.Parse(Summa) /SummaInValuta ).ToString("######0.00");
            }
        }
     
        private void checkValuta()
        {
            if (!valuta.Equals("RUB") && type!=2)
            {
                tbRUB.Visible = tbSumInRub.Visible = checkSumInRub =
                    lbSumInRub.Visible = tbCourse.Visible = label4.Visible = true;
            }

            if (type == 2)
                tbValuta.Text = "RUB";
            else 
                tbValuta.Text = valuta;
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
            decimal value = 0;
            if (!decimal.TryParse(tbMoney.Text, out value))
                tbMoney.Text = "0,00";

            if (tbMoney.Text.ToString().Length == 0)
                tbMoney.Text = "0,00";
            else
                tbMoney.Text = decimal.Parse(tbMoney.Text.ToString()).ToString("######0.00");
        }
     
        private void tbSumInRub_Leave(object sender, EventArgs e)
        {
            if (tbSumInRub.Text.ToString().Length == 0)
                tbSumInRub.Text = "0,00";
            else
                tbSumInRub.Text = decimal.Parse(tbSumInRub.Text.ToString()).ToString("######0.00");

        }
      
        private void frmOrderMoney_Load(object sender, EventArgs e)
        {
            if (type == 1)
            {
                string valueSettings = Config.hCntMain.GetSettings("овпд");
                DateTime dtMoney = nowTime.Date.AddHours(15).AddMinutes(00);
                if (valueSettings.Length > 0)
                    dtMoney = DateTime.Parse(valueSettings);

                if (nowTime.TimeOfDay < dtMoney.TimeOfDay)
                {
                    dtpDate.MinDate = nowTime.Date;
                }
                else
                {
                    dtpDate.MinDate = nowTime.Date.AddDays(1);
                }
                if (idOrder == 0 )
                    this.Text = "Получение денег ";
                else this.Text = "Редактирование получения денег ";

                if (TypeServiceRecordOnTime == 2)
                {
                    if (nowTime.Year != dtpDate.MinDate.Year || nowTime.Month != dtpDate.MinDate.Month)
                    {
                        MessageBox.Show("Заказ денег не возможен!", "Информарование", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Close();
                        return;
                    }

                    dtpDate.MaxDate = new DateTime(nowTime.Year, nowTime.Month, 1).AddMonths(1).AddDays(-1);
                }
                else if (TypeServiceRecordOnTime == 4)
                { 
                Dictionary<int,List<int>> dicMonthSercher = new Dictionary<int, List<int>>();
                    dicMonthSercher.Add(1, new List<int>() { 1, 2, 3 });
                    dicMonthSercher.Add(2, new List<int>() { 4, 5, 6 });
                    dicMonthSercher.Add(3, new List<int>() { 7, 8, 9 });
                    dicMonthSercher.Add(4, new List<int>() { 10, 11, 12 });


                    //List<int> Values = dicMonthSercher.AsEnumerable().Where(r => r.Value.Contains(nowTime.Month)).First().Value;


                    List<int> Values = dicMonthSercher.First(r => r.Value.Contains(nowTime.Month)).Value;

                    if (nowTime.Year != dtpDate.MinDate.Year || !Values.Contains(dtpDate.MinDate.Month))
                    {
                        MessageBox.Show("Заказ денег не возможен!", "Информарование", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Close();
                        return;
                    }

                    dtpDate.MaxDate = new DateTime(nowTime.Year, Values.AsEnumerable().Max(), 1).AddMonths(1).AddDays(-1);

                }
            }
            else
            {
                dtpDate.MinDate = nowTime.Date;
                tbValuta.Text = valuta;
                if (idOrder == 0)
                    this.Text = "Возврат денег ";
                else this.Text = "Редактирование возврата денег ";
            }

            if (inType == 3)
            {
                tbMoney.ReadOnly = true;
                tbMoney.Text = maxSumma.ToString("0.00");
            }

             checkValuta();

        }

        private void btSelect_Click(object sender, EventArgs e)
        {
            if (decimal.Parse(tbMoney.Text.ToString()) == 0)
            {
                MessageBox.Show("Необходимо ввести сумму!", "Информирование", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            decimal summa = decimal.Parse(tbMoney.Text.Trim());
            
            DataTable dtHistory = Config.hCntMain.getHistoryOrderAndReturn(id_ServiceRecords);
            decimal maxSumma = decimal.Parse(dtHistory.Rows[0]["maxSumma"].ToString());
            decimal balanceGet = decimal.Parse(dtHistory.Rows[0]["balanceGet"].ToString());
            decimal balanceGetInValuta = decimal.Parse(dtHistory.Rows[0]["balanceGetInValuta"].ToString());
            decimal balanceReturn = decimal.Parse(dtHistory.Rows[0]["balanceReturn"].ToString());
            decimal debtReport = decimal.Parse(dtHistory.Rows[0]["debtReport"].ToString());
            //decimal oldDebt = decimal.Parse(dtHistory.Rows[0]["oldDebt"].ToString()); // для ежемесячногй СЗ
            string valuta = dtHistory.Rows[0]["Valuta"].ToString();
            decimal sumGet = decimal.Parse(dtHistory.Rows[0]["sumGet"].ToString());
            decimal sumOrderGetInValuta = decimal.Parse(dtHistory.Rows[0]["sumOrderGetInValuta"].ToString());
            int monthDateCreateReport = DateTime.Parse(dtHistory.Rows[0]["DateCreateReport"].ToString()).Month;
            int yearDateCreateReport = DateTime.Parse(dtHistory.Rows[0]["DateCreateReport"].ToString()).Year;
            bool isTodayMonthAbdYear = nowTime.Month.Equals(monthDateCreateReport) && nowTime.Year.Equals(yearDateCreateReport);
            //int typeSROnTime = (int)dtHistory.Rows[0]["typeSROnTime"];

            if (idOrder != 0)
            {
                balanceGet += oldSumma;
                balanceReturn += oldSumma;
            }

            if (summa>maxSumma && (type == 1 || (type == 2 && valuta == "RUB")))
            {
                MessageBox.Show("Сумма превышает \"Сумму СЗ\"!", "Информирование", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            decimal summaInValuta = 0;
            if (tbSumInRub.Text.Length > 0 ? decimal.Parse(tbSumInRub.Text) > 0 ? true: false: false)
            {
                summa = decimal.Parse(tbSumInRub.Text.Trim());
                summaInValuta = decimal.Parse(tbMoney.Text.Trim());
            }

            if (checkSumInRub && double.Parse(tbCourse.Text) <= 0)
            {
                MessageBox.Show("Заполните курс валюты!", "Информирование", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
           // decimal balanceSumma = type == 1? getHistoryOrder(id_ServiceRecords, maxSumma) : getHistoryReturn(id_ServiceRecords, maxSumma);
            if (type == 1 && (balanceGet - (checkSumInRub ? summaInValuta : summa) < 0) && valuta == "RUB") // - (checkSumInRub ? summaInValuta : summa) < 0)
            {
                MessageBox.Show("Вы можете получить не больше " + balanceGet, "Информирование", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (type == 1 && summaInValuta > balanceGetInValuta && valuta != "RUB") // - (checkSumInRub ? summaInValuta : summa) < 0)
            {
                MessageBox.Show("Вы можете получить не больше " + balanceGetInValuta, "Информирование", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (type == 2 && valuta == "RUB"  && (checkSumInRub ? summaInValuta : summa) > balanceReturn)
            {
                MessageBox.Show("Вы можете вернуть не больше " + balanceReturn, "Информирование", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (type == 2 && valuta != "RUB" && summa > balanceReturn)
            {
                MessageBox.Show("Вы можете вернуть не больше " + balanceReturn, "Информирование", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (type == 2 && (checkSumInRub ? summaInValuta : summa) > balanceReturn && balanceReturn > 0 && !isTodayMonthAbdYear)
            {
                MessageBox.Show(Config.centralText("Вы можете вернуть по старому долгу\nне больше " + balanceReturn + "\n"), "Информирование", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (DialogResult.Yes == MessageBox.Show("Вы заказываете на имя " + cmbDirector.Text, "Заказ", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
            {


               



                int idMoneyRecipient;
                idMoneyRecipient = (int)cmbDirector.SelectedValue;
                DataTable dtTMP = new DataTable();
                DataTable dtTMPUpdate = new DataTable();

                if (idOrder == 0)
                dtTMP = Config.hCntMain.setPayments(id_ServiceRecords, 
                                                              dtpDate.Value, 
                                                              summa, 
                                                              type, 
                                                              UserSettings.User.Id, 
                                                              idMoneyRecipient, 
                                                              status, 
                                                              summaInValuta, 
                                                              valuta//,
                                                              //oldDebt
                                                              );
                else dtTMPUpdate = Config.hCntMain.updatePayments(idOrder,
                                              dtpDate.Value,
                                              summa,
                                              type,
                                              UserSettings.User.Id,
                                              idMoneyRecipient,
                                              status,
                                              summaInValuta);

                if ((dtTMP == null || dtTMP.Rows.Count == 0) && idOrder == 0)
                {
                    MessageBox.Show("Ошибка добавления.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if (dtTMPUpdate != null ? dtTMPUpdate.Rows.Count > 0 ? true: false: false)
                {
                    if (dtTMPUpdate.Rows[0][0].ToString().Equals("ошибка"))
                    {
                        MessageBox.Show("Ошибка редактирования.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    this.DialogResult = DialogResult.OK;
                }

                if (isEdit)
                {

                    DataTable dtTmpData = Config.hCntMain.getServiceRecordsBody(id_ServiceRecords);
                    
                   

                    Logging.StartFirstLevel(1503);
                    Logging.Comment("Произведено редактирование операции по ДС");
                    Logging.Comment("Тип операции:" + (type == 1 ? "Получение" : "Возврат"));
                    //Logging.Comment("ID: " + idOrder);
                    Logging.Comment("Id СЗ: " + id_ServiceRecords);
                    Logging.Comment("Номер СЗ: " + dtTmpData.Rows[0]["Number"].ToString());
                    Logging.Comment("Подномер: " + tbNumberSub.Text);
                    //Logging.VariableChange("Сумма", tbMoney.Text, oldSumma);
                    Logging.Comment("Сумма: "+ tbMoney.Text);
                    Logging.Comment("Валюта: " + tbValuta.Text);

                    Logging.Comment("Тип СЗ по времени: " + ((int)dtTmpData.Rows[0]["TypeServiceRecordOnTime"] == 1 ? "разовая" : ((int)dtTmpData.Rows[0]["TypeServiceRecordOnTime"] == 2 ? "ежемесячная" : "Фонд")));

                    int? idFond = dtTmpData.Rows[0]["id_ServiceRecordsFond"] == DBNull.Value ? null : (int?)dtTmpData.Rows[0]["id_ServiceRecordsFond"];

                    if (idFond != null)
                    {
                        DataTable dtTmpFond = Config.hCntMain.getFondInfo(idFond, id_ServiceRecords);
                        if (dtTmpFond != null && dtTmpFond.Rows.Count > 0)
                        {
                            Logging.Comment($"№{dtTmpFond.Rows[0]["Number"].ToString()} на {dtTmpFond.Rows[0]["sumString"].ToString()} от {((DateTime)dtTmpFond.Rows[0]["DateConfirmationD"]).ToShortDateString()}");
                        }
                    }
                    else
                    {
                        Logging.Comment((int)dtTmpData.Rows[0]["TypeServiceRecordOnTime"] == 3 ? "Доп.фонд не выбран" : "Фонд не выбран");
                    }


                    if (dtTmpData.Rows[0]["inType"] != DBNull.Value)
                    {
                        DataTable dtTypicalWorks = Config.hCntMain.getTypicalWorks(false);
                        if (dtTypicalWorks != null && dtTypicalWorks.Rows.Count > 0)
                        {
                            EnumerableRowCollection<DataRow> rowType = dtTypicalWorks.AsEnumerable().Where(r => r.Field<int>("id") == (int)dtTmpData.Rows[0]["inType"]);
                            if (rowType.Count() > 0)
                            {
                                Logging.Comment($"Тип работ ID:{rowType.First()["id"]}; Наименование:{rowType.First()["cName"]}");
                            }
                        }
                    }


                    Logging.VariableChange("Получатель ID: ", cmbDirector.SelectedValue, oldIdDirector,typeLog._int);
                    Logging.VariableChange("Получатель ФИО: ", cmbDirector.Text, oldDirector == null ? "" : oldDirector);
                    Logging.Comment("Предполагаемая дата: " + dtpDate.Value.ToShortDateString());

                    if (!valuta.Equals("RUB") && type != 2)
                    {
                        Logging.Comment("Курс валюты: " + tbCourse.Text);
                        Logging.Comment("Сумму в рублях: " + tbSumInRub.Text);
                    }

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
                else
                    setLog(id_ServiceRecords, "", 0, type);


                //тут запись в ДО ДЗ

                DataTable dtTmpMemo = Config.hCntMain.getMemorandums(nowTime, nowTime, id_ServiceRecords, false);
                if (dtTmpMemo != null && dtTmpMemo.Rows.Count > 0)
                {
                    foreach (DataRow row in dtTmpMemo.Rows)
                    {                      
                        int id_doc = (int)row["id_doc"];
                        Config.hCntDocumentsDZ.setMoveDocument(id_doc);
                    }
                }
                //if (id_doc != null)
                    //Config.hCntDocumentsDZ.setMoveDocument(id_doc);
                //

                this.DialogResult = DialogResult.OK;
            }
            else return;

        }
 
        private void btClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        DataTable dtDir, userName;

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

        private void cmbDirector_SelectionChangeCommitted(object sender, EventArgs e)
        {

        }

        private void dtpDate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void getDir ()
        {


        }

        private void getDirector_SelectionChangeCommitted (object sender, EventArgs e)
        {
            getDir();
        }

        private void tbCourse_TextChanged(object sender, EventArgs e)
        {
            decimal course = 0, summa = 0;
            if (tbCourse.Text.Length > 0)
                decimal.TryParse(tbCourse.Text, out course);
            if (tbMoney.Text.Length > 0)
                decimal.TryParse(tbMoney.Text, out summa);
            tbSumInRub.Text = roundNumber(course * summa).ToString("######0.00");
        }

        private decimal roundNumber(decimal SumInRubCash)
        {
            return Math.Round(SumInRubCash, MidpointRounding.AwayFromZero);
        }
        private void tbMoney_TextChanged(object sender, EventArgs e)
        {
            tbCourse_TextChanged(sender, e);
        }

        private void tbMoney_MouseClick(object sender, MouseEventArgs e)
        {
            decimal value = 0;
            if (decimal.TryParse(tbMoney.Text, out value))
                return;
            else tbMoney.Text = "0,00";
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

        private void setLog(int id, string comment, int id_status, int isSend)
        {
            DataTable dtTmpData = Config.hCntMain.getServiceRecordsBody(id);
            if (isSend==1)
                Logging.StartFirstLevel(1254);
            else
                Logging.StartFirstLevel(1255);

            Logging.Comment("Id СЗ: " + id_ServiceRecords);
            Logging.Comment("Номер СЗ: " + dtTmpData.Rows[0]["Number"].ToString());
            Logging.Comment("Подномер: " + tbNumberSub.Text);
            Logging.Comment("Сумма: " + tbMoney.Text);
            Logging.Comment("Предполагаемая дата: " + dtpDate.Value.ToShortDateString());

            Logging.Comment(chbChangeDirector.Text + ": " + (chbChangeDirector.Checked ? "Включен" : "Отключен"));
            if (chbChangeDirector.Checked)
                Logging.Comment("Получатель отличается от заказчика");

            Logging.Comment("Руководиель ID: " + (int)dtDir.Rows[cmbDirector.SelectedIndex][0] + "; Наименование:" + cmbDirector.Text.ToString());

            if (!valuta.Equals("RUB") && type != 2)
            {
                Logging.Comment("Курс валюты: " + tbCourse.Text);
                Logging.Comment("Сумму в рублях: " + tbSumInRub.Text);
            }

            Logging.Comment("Тип СЗ: " + ((int)dtTmpData.Rows[0]["TypeServiceRecord"] == 0 ? "стандарт." : "предварит."));
            //Logging.Comment("Тип СЗ по времени: " + ((int)dtTmpData.Rows[0]["TypeServiceRecordOnTime"] == 1 ? "разовая" : "ежемесячная"));
            Logging.Comment("Тип СЗ по времени: " + ((int)dtTmpData.Rows[0]["TypeServiceRecordOnTime"] == 1 ? "разовая" : ((int)dtTmpData.Rows[0]["TypeServiceRecordOnTime"] == 2 ? "ежемесячная" : "Фонд")));

            int? idFond = dtTmpData.Rows[0]["id_ServiceRecordsFond"] == DBNull.Value ? null : (int?)dtTmpData.Rows[0]["id_ServiceRecordsFond"];

            if (idFond != null)
            {
                DataTable dtTmpFond = Config.hCntMain.getFondInfo(idFond, id);
                if (dtTmpFond != null && dtTmpFond.Rows.Count > 0)
                {
                    Logging.Comment($"№{dtTmpFond.Rows[0]["Number"].ToString()} на {dtTmpFond.Rows[0]["sumString"].ToString()} от {((DateTime)dtTmpFond.Rows[0]["DateConfirmationD"]).ToShortDateString()}");
                }
            }
            else
            {
                Logging.Comment((int)dtTmpData.Rows[0]["TypeServiceRecordOnTime"] == 3 ? "Доп.фонд не выбран" : "Фонд не выбран");
            }


            if (dtTmpData.Rows[0]["inType"] != DBNull.Value)
            {
                DataTable dtTypicalWorks = Config.hCntMain.getTypicalWorks(false);
                if (dtTypicalWorks != null && dtTypicalWorks.Rows.Count > 0)
                {
                    EnumerableRowCollection<DataRow> rowType = dtTypicalWorks.AsEnumerable().Where(r => r.Field<int>("id") == (int)dtTmpData.Rows[0]["inType"]);
                    if (rowType.Count() > 0)
                    {
                        Logging.Comment($"Тип работ ID:{rowType.First()["id"]}; Наименование:{rowType.First()["cName"]}");
                    }
                }
            }

            Logging.Comment("Сумма:" + decimal.Parse(dtTmpData.Rows[0]["Summa"].ToString()).ToString("0.00"));
            Logging.Comment("Валюта:" + dtTmpData.Rows[0]["Valuta"].ToString());

            if ((bool)dtTmpData.Rows[0]["Mix"])
            {
                Logging.Comment("Сумма нал:" + dtTmpData.Rows[0]["SummaCash"].ToString());
                Logging.Comment("Сумма безнал:" + dtTmpData.Rows[0]["SummaNonCash"].ToString());
            }

            Logging.Comment("Объект ID: " + dtTmpData.Rows[0]["id_Object"].ToString() + "; Наименование:" + dtTmpData.Rows[0]["name_Object"].ToString());
            Logging.Comment("Описание, комментарий: " + dtTmpData.Rows[0]["Description"].ToString());
            //Logging.Comment("Оплата: " + (!(bool)dtTmpData.Rows[0]["bCashNonCash"] ? "Наличные" : "Карта"));
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

            DataTable dtTmpMemo = Config.hCntMain.getMemorandums(nowTime, nowTime, id, false);
            if (dtTmpMemo != null && dtTmpMemo.Rows.Count > 0)
            {
                if (dtTmpMemo != null && dtTmpMemo.Rows.Count > 0)
                {
                    Logging.Comment("Произведена смена статуса у следующих ДЗ а ДО:");
                    foreach (DataRow row in dtTmpMemo.Rows)
                    {
                        Logging.Comment($"ID записи:{row["id"]}");
                        Logging.Comment($"№ ДЗ:{row["no_doc"]}");
                        Logging.Comment($"Дата:{row["date_create"]}");
                        Logging.Comment($"Отдел нарушителя:{row["depPenalty"]}");
                        Logging.Comment($"Заголовок ДЗ:{row["cname"]}");
                        Logging.Comment($"Тип нарушения:{row["DistrType"]}");
                        Logging.Comment($"Сумма нарушения:{row["sumPenalty"]}");
                        Logging.Comment($"Сумма премии:{row["SumBonus"]}");
                        Logging.Comment($"Сотрудник, обнаружевший нарушение:{row["FIOBonus"]}");
                    }
                }
            }


            Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
               + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
            Logging.StopFirstLevel();
        }

    }
}
