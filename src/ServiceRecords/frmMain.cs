using Nwuram.Framework.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nwuram.Framework.Settings.User;


namespace ServiceRecords
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            dgvMain.AutoGenerateColumns = false;

            this.Text = "\"" + Nwuram.Framework.Settings.Connection.ConnectionSettings.ProgramName + "\", \"" + Nwuram.Framework.Settings.User.UserSettings.User.Status + "\", " + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername + "";
            ToolTip tt = new ToolTip();
            tt.SetToolTip(btClose, "Выход");
            tt.SetToolTip(btAccept, "Подтвердить");
            tt.SetToolTip(btRefuse, "Отклонить");
            tt.SetToolTip(btAddBlock, "Добавить СЗ");
            tt.SetToolTip(btEditBlock, "Редактировать СЗ");
            tt.SetToolTip(btDelBlock, "Удалить СЗ");
            tt.SetToolTip(btView, "Просмотреть СЗ");
            tt.SetToolTip(btUpdate, "Обновить");
            tt.SetToolTip(btnAccept, "Подтвердить");
            tt.SetToolTip(btnRefuse, "Отклонить");
            tt.SetToolTip(btnCheckReport, "Проверка отчетов");
            if (Config.CodeUser.Equals("РКВ") || Config.CodeUser.Equals("ОП"))
                tt.SetToolTip(btViewPayment, "Журнал СЗ");

            else
                tt.SetToolTip(btViewPayment, "Журнал выплат по СЗ");
            tt.SetToolTip(btViewHistoryStatus, "Журнал статусов СЗ");
            tt.SetToolTip(btChangeStatus, "Сменить статус СЗ");
            //tt.SetToolTip(btListNotePeriod, "Список СЗ за период");
            tt.SetToolTip(btListNotePeriod, "Отчёт");
            tt.SetToolTip(btReportNoteSingleDeps, "Отчет по СЗ по отделу");
            //tt.SetToolTip(btReportNoteMoreDeps, "Отчет по СЗ по отделам");
            tt.SetToolTip(btReportNoteMoreDeps, "Отчёт по СЗ");
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            dtpStart.Value = DateTime.Now.AddDays(-7);
            getStatus();
            getBlock();
            getDeps();
            createObjects();
            visibleElements();
            getData();
            timUpdate.Interval = 1000 * 60 * 10;
            chbUpdate.Checked = Config.hCntMain.getSettingUpdateButton() == 1;


        }

        #region "Выпадающие списки"
        
        DataTable dtBlock, dtDeps,dtStatus, dtObjects, userDepartmentName, idDepartament, idBlock, userStatus;

        private void createObjects()
        {
            dtObjects = Config.hCntMain.getObjects(); 
            dtObjects.Rows.Add(0, "Все объекты", 1);
            dtObjects.DefaultView.Sort = "id_Object ASC";
            dtObjects.DefaultView.RowFilter = "is_Active = 1";
            cmbObjects.DataSource = dtObjects;
            cmbObjects.DisplayMember = "name_Object";
            cmbObjects.ValueMember = "id_Object";
            
        }

        private void createBlock()
        {
            dtBlock = Config.hCntMain.getBlock(-1);
            cmbBlock.DataSource = dtBlock;
            dtBlock.Rows.Add(0, "Все блоки");
            dtBlock.DefaultView.Sort = "id_Block ASC";
            cmbBlock.DisplayMember = "name";
            cmbBlock.ValueMember = "id_Block";
        }

        private void createDepartment()
        {
            if (cmbBlock.SelectedValue == null)
            {
                cmbDeps.DataSource = null;
                return;
            }

            int id_block = (int)cmbBlock.SelectedValue;
            dtDeps = Config.hCntMain.getBlock(id_block);
            dtDeps.Rows.Add(0, "Все отделы");
            dtDeps.DefaultView.Sort = "id_Department ASC";
            cmbDeps.DataSource = dtDeps;
            cmbDeps.DisplayMember = "name";
            cmbDeps.ValueMember = "id_Department";

        }
        private void getBlock()
        {
            userStatus = Config.hCntMain.getUserProgramsStatus();
            userDepartmentName = Config.hCntMain.getUserDepartment();
            idDepartament = Config.hCntMain.getUserDepartmentId();
            try
            {
                if (/*userStatus != null && userStatus.Rows.Count > 0 &&*/ (Config.CodeUser.Equals("РКВ") || Config.CodeUser.Equals("КНТ")))
                {
                    dtBlock = Config.hCntMain.getBlock(-1);
                    cmbBlock.DataSource = dtBlock;
                    dtBlock.DefaultView.Sort = "id_Block ASC";
                    cmbBlock.DisplayMember = "name";
                    cmbBlock.ValueMember = "id_Block";
                    //УСЛОВИЕ 

                    //if (userDepartmentName.Rows[0][0].ToString().StartsWith("Блок") || userDepartmentName.Rows[0][0].ToString().StartsWith("Отдел 1"))
                    if (Config.hCntMain.checkBlock(int.Parse(idDepartament.Rows[0][0].ToString())).Rows.Count>0)
                    {
                        int i = 0;
                        while ((i < dtBlock.Rows.Count))
                        {
                            if (dtBlock.Rows[i][0].ToString().Equals(idDepartament.Rows[0][0].ToString()) == true)
                                break;
                            i++;
                        }
                        cmbBlock.SelectedIndex = i;
                        this.cmbBlock.Enabled = false;
                    }
                    else
                    {
                        int der = (int)idDepartament.Rows[0][0];
                        idBlock = Config.hCntMain.getUsersBlockId(der);
                       
                        
                        
                        DataRow[] f = dtBlock.Select("id_Block = " + (int)idBlock.Rows[0][0]);


                        //int i = 0;
                        //while ((i < dtBlock.Rows.Count))
                        //{
                        //    if (dtBlock.Rows[i][0].ToString().Equals(idBlock.Rows[0][0].ToString()) == true)
                        //        break;
                        //    i++;
                        //}
                        //cmbBlock.SelectedIndex = i;


                        cmbBlock.SelectedValue = f[0][0];
                        this.cmbBlock.Enabled = false;


                    }

                }
                else createBlock();
            }
            catch (Exception e) { MessageBox.Show("Ваш отдел не занесен в программу. Обратитесь в ОЭЭС!","Ошибка",MessageBoxButtons.OK, MessageBoxIcon.Error); }

        }



        private void getDeps()
        {
            userStatus = Config.hCntMain.getUserProgramsStatus();
            userDepartmentName = Config.hCntMain.getUserDepartment();
            try
            {
                if (/*userStatus != null && userStatus.Rows.Count > 0 && */ (Config.CodeUser.Equals("РКВ") || Config.CodeUser.Equals("КНТ")))
                {
                    if (cmbBlock.SelectedValue == null)
                    {
                        cmbDeps.DataSource = null;
                        return;
                    }

                    int id_block = (int)cmbBlock.SelectedValue;
                    dtDeps = Config.hCntMain.getBlock(id_block);
                    cmbDeps.DataSource = dtDeps;
                    cmbDeps.DisplayMember = "name";
                    cmbDeps.ValueMember = "id_Department";

                    //if (!(userDepartmentName.Rows[0][0].ToString().StartsWith("Блок") || userDepartmentName.Rows[0][0].ToString().StartsWith("Отдел 1")))

                        if (Config.hCntMain.checkBlock(int.Parse(idDepartament.Rows[0][0].ToString())).Rows.Count == 0)
                            findDepartment();
                    else createDepartment();
                }
                else createDepartment();
            }
            catch (Exception e) { Process.GetCurrentProcess().Kill(); }
        }

        private void findDepartment()
        {
            userDepartmentName = Config.hCntMain.getUserDepartment();
            idDepartament = Config.hCntMain.getUserDepartmentId();
            int m = 0;
            while ((m < dtDeps.Rows.Count))
            {
                var row = dtDeps.Rows[m][0];
                if (row.ToString().Equals(idDepartament.Rows[0][0].ToString()) == true)
                {
                    int index = cmbDeps.FindString(userDepartmentName.Rows[0][0].ToString());
                    cmbDeps.SelectedIndex = index;
                    break;
                }
                m++;
            }
            cmbDeps.SelectedIndex = m;
            cmbDeps.Enabled = false;

        }
        private void getStatus()
        {
            dtStatus = Config.hCntMain.getStatus();

            DataRow[] DrArrCheck = dtStatus.Select("id = 21 or id = 13");
            foreach (DataRow DrCheck in DrArrCheck)
            {
                dtStatus.Rows.Remove(DrCheck);
            }

            dtStatus.Rows.Add(0, " Все статусы", true);
            dtStatus.DefaultView.Sort = "cName ASC";

            cmbStatus.DataSource = dtStatus;
            cmbStatus.ValueMember = "id";
            cmbStatus.DisplayMember = "cName";
            

            if (Config.CodeUser.Equals("КД"))
            {
                dtStatus.DefaultView.Sort = "isKD DESC, id ASC";
                cmbStatus.SelectedValue = 4;
                isKD();
            }
            if (Config.CodeUser.Equals("КНТ"))
            {
                cmbStatus.SelectedValue = 2;
            }
        }

        private void isKD()
        {
            if (Config.CodeUser.Equals("КД") && chbKD.Checked)
            {
                cmbStatus.SelectedValue = 0;
                cmbStatus.Enabled = false;
            }
            else
            {
                cmbStatus.SelectedValue = 0;
                cmbStatus.Enabled = true;
            }
        }
        private void cmbBlock_SelectionChangeCommitted(object sender, EventArgs e)
        {
            getDeps();
            setFilter();
        }



        #endregion

        #region "Выборка данных"

        DataTable dtMain;

        private void getData()
        {

            DateTime dateStart, dateEnd;
            dateStart = dtpStart.Value.Date;
            dateEnd = dtpEnd.Value.Date;

            dtMain = Config.hCntMain.getServiceRecords(dateStart, dateEnd);
            if (Config.CodeUser == "КД")
            {
                foreach (DataRow row in dtMain.Rows)
                    row["Description"] = row["Description"].ToString().Replace("Разовая.", "");
            }
            setFilter();
            dgvMain.DataSource = dtMain;
            if (sortCol != null)
            {
                dgvMain.Sort(sortCol, direction);
            }

            foreach (DataGridViewRow row in dgvMain.Rows)
            {
              row.Cells["Enter"].Value = false;
            }
            btnAccept.Enabled = btnRefuse.Enabled = false;
        }

        private void setFilter()
        {
            string filter = "";

            filter += (tbNumber.Text.Length != 0 ?
                (filter.Length == 0 ? "" : " AND ") + "CONVERT(Number, 'System.String') LIKE '%" + tbNumber.Text + "%'" : "");

            filter += (tbInfo.Text.Length != 0 ?
              (filter.Length == 0 ? "" : " AND ") + "CONVERT(Description, 'System.String') LIKE '%" + tbInfo.Text + "%'" : "");

            //filter += (tbIP.Text.Length != 0 ?
            //  (filter.Length == 0 ? "" : " AND ") + "CONVERT(IP, 'System.String') LIKE '%" + tbIP.Text + "%'" : "");

            //filter += (tbName.Text.Length != 0 ?
            //  (filter.Length == 0 ? "" : " AND ") + "CONVERT(PCName, 'System.String') LIKE '%" + tbName.Text + "%'" : "");

            //filter += (tbUser.Text.Length != 0 ?
            //  (filter.Length == 0 ? "" : " AND ") + "CONVERT(cUser, 'System.String') LIKE '%" + tbUser.Text + "%'" : "");

            //filter += (tbProg.Text.Length != 0 ?
            //  (filter.Length == 0 ? "" : " AND ") + "CONVERT(cProgram, 'System.String') LIKE '%" + tbProg.Text + "%'" : "");

            //filter += (tbMsg.Text.Length != 0 ?
            //  (filter.Length == 0 ? "" : " AND ") + "CONVERT(Log, 'System.String') LIKE '%" + tbMsg.Text + "%'" : "");

            //filter += (filter.Length == 0 ? "" : " AND ") + (chbErrorFix.Checked ?
            //     "" : "Solved = 0");

            if (Config.CodeUser != "КНТ" && Config.CodeUser != "РКВ")
                filter += ((int)cmbBlock.SelectedValue != 0 ?
                     (filter.Length == 0 ? "" : " AND ") + "CONVERT(id_Block, 'System.Int32')  = " + cmbBlock.SelectedValue.ToString() + "" : "");

            if ((Config.CodeUser == "КНТ" || Config.CodeUser == "РКВ") && (int)cmbDeps.SelectedValue == 0)
            {
                filter += filter.Length == 0 ? "(" : " AND (";
                for (int i = 0; i < cmbDeps.Items.Count; i++)
                {
                    if ((int)dtDeps.Rows[i]["id_Department"] != 0)
                    {
                        filter += (filter.EndsWith("(") ? "" : " OR ");
                        filter += "CONVERT(id_Department, 'System.Int32')  = " + dtDeps.Rows[i]["id_Department"].ToString();
                    }
                }
                filter += ")";
            }


            filter += ((int)cmbDeps.SelectedValue != 0 ?
                (filter.Length == 0 ? "" : " AND ") + "CONVERT(id_Department, 'System.Int32')  = " + cmbDeps.SelectedValue.ToString() + "" : "");

            if (cmbStatus.SelectedValue != null)
            filter += ((int)cmbStatus.SelectedValue != 0 ?
               (filter.Length == 0 ? "" : " AND ") + "CONVERT(id_Status, 'System.Int32')  = " + cmbStatus.SelectedValue.ToString() + "" : "");

           filter += ((int)cmbObjects.SelectedValue != 0 ?
               (filter.Length == 0 ? "" : " AND ") + "CONVERT(id_Object, 'System.Int32')  = " + cmbObjects.SelectedValue.ToString() + "" : "");

            if (rbWork.Checked)
                filter += (filter.Length == 0 ? "" : " AND ") + "id_Status <>13 AND id_Status <> 21";
            else if(rbArhiv.Checked)
                filter += (filter.Length == 0 ? "" : " AND ") + "id_Status not in (1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12,  14, 15, 17, 18, 19, 20)";

            filter += Config.CodeUser.Equals("КД") && chbKD.Checked ? " AND (id_Status = 4 OR id_Status = 10)" : ""; // На согласовании с КД и На повторном согласовании с КД

            try
            {
                dtMain.DefaultView.RowFilter = filter;
                //dtMain.DefaultView.Sort = "DateCreate ASC, Server ASC, DB ASC";
            }
            catch (Exception)
            {
                MessageBox.Show("Некорректное значение фильтра!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }


        #endregion
      
        private void btAddBlock_Click(object sender, EventArgs e)
        {
            frmServiceNote frmS = new frmServiceNote();
            frmS.id = -1;
            frmS.Text = "Добавить СЗ";
            if (DialogResult.OK == frmS.ShowDialog())
            {
                int currentRow = dgvMain.CurrentCell.RowIndex;
                getData();
                if (dgvMain.Rows.Count > currentRow)
                {
                    DataGridViewCell cell = dgvMain.Rows[currentRow].Cells[1];
                    dgvMain.CurrentCell = cell;
                    dgvMain.CurrentCell.Selected = true;
                }
            }
        }

        private void btEditBlock_Click(object sender, EventArgs e)
        {
            if (dtMain == null || dtMain.DefaultView.Count == 0 || dgvMain.CurrentRow == null || dgvMain.CurrentRow.Index == -1) return;
            int id = (int)dtMain.DefaultView[dgvMain.CurrentRow.Index]["id"];

            frmServiceNote frmS = new frmServiceNote();
            frmS.id = id;
            frmS.Text = "Редактировать СЗ";
            if (DialogResult.OK == frmS.ShowDialog())
            {
                int currentRow = dgvMain.CurrentCell.RowIndex;
                getData();
                if (dgvMain.Rows.Count > currentRow)
                {
                    DataGridViewCell cell = dgvMain.Rows[currentRow].Cells[1];
                    dgvMain.CurrentCell = cell;
                    dgvMain.CurrentCell.Selected = true;
                }
            }
        }

        private void btDelBlock_Click(object sender, EventArgs e)
        {
            if (dtMain == null || dtMain.DefaultView.Count == 0 || dgvMain.CurrentRow == null || dgvMain.CurrentRow.Index == -1) return;

            if (DialogResult.Yes == MessageBox.Show(Config.centralText("Удалить выбранную\nзапись?\n"), "Удаление записи", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
            {
                int id = (int)dtMain.DefaultView[dgvMain.CurrentRow.Index]["id"];

                DataTable dtTmpData = Config.hCntMain.getServiceRecordsBody(id);

                Logging.StartFirstLevel(1252);

                Logging.Comment("Id СЗ: " + id);
                Logging.Comment("Номер СЗ: " + dtTmpData.Rows[0]["Number"].ToString());

                Logging.Comment("Тип СЗ: "+ ((int)dtTmpData.Rows[0]["TypeServiceRecord"] == 0 ? "стандарт." :"предварит."));
                Logging.Comment("Тип СЗ по времени: " + ((int)dtTmpData.Rows[0]["TypeServiceRecordOnTime"] == 1 ? "разовая" : "ежемесячная"));


                Logging.Comment("Сумма:" + decimal.Parse(dtTmpData.Rows[0]["Summa"].ToString()).ToString("0.00"));
                Logging.Comment("Валюта:"+ dtTmpData.Rows[0]["Valuta"].ToString());
                if ((bool)dtTmpData.Rows[0]["Mix"])
                {
                    Logging.Comment("Сумма нал:"+ dtTmpData.Rows[0]["SummaCash"].ToString());
                    Logging.Comment("Сумма безнал:"+ dtTmpData.Rows[0]["SummaNonCash"].ToString());
                }
                Logging.Comment("Объект ID: " + dtTmpData.Rows[0]["id_Object"].ToString() + "; Наименование:" + dtTmpData.Rows[0]["name_Object"].ToString());

                Logging.Comment("Описание, комментарий: " + dtTmpData.Rows[0]["Description"].ToString());
                Logging.Comment("Оплата: "+(!(bool)dtTmpData.Rows[0]["bCashNonCash"] ? "Наличные": "Карта"));

                Logging.Comment("Дата создания СЗ: "+((DateTime)dtTmpData.Rows[0]["CreateServiceRecord"]).ToShortDateString());

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


                Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                   + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
                Logging.StopFirstLevel();


                Config.hCntMain.delServiceRecords(id);
                getData();
            }
        }

        private void btClose_Click(object sender, EventArgs e)
        {

            Close();
        }

        private void btUpdate_Click(object sender, EventArgs e)
        {
            getData();
        }

        private void btView_Click(object sender, EventArgs e)
        {
            if (dtMain == null || dtMain.DefaultView.Count == 0 || dgvMain.CurrentRow == null || dgvMain.CurrentRow.Index == -1) return;
            int id = (int)dtMain.DefaultView[dgvMain.CurrentRow.Index]["id"];

            frmServiceNote frmS = new frmServiceNote();
            frmS.id = id;
            frmS.Text = "Просмотр СЗ";
            frmS.setIsView();
            if (DialogResult.OK == frmS.ShowDialog())
            {
                int currentRow = dgvMain.CurrentCell.RowIndex;
                getData();
                if (dgvMain.Rows.Count > currentRow)
                {
                    DataGridViewCell cell = dgvMain.Rows[currentRow].Cells[0];
                    dgvMain.CurrentCell = cell;
                    dgvMain.CurrentCell.Selected = true;
                }
            }
        }


        int id_Status;
        int id_Creator;
        bool AddPayment;
        bool isAddReportMoney;
        bool isClosedDoc;

        private void dgvMain_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex != -1)
            {
                dgvMain.CurrentCell = dgvMain[e.ColumnIndex, e.RowIndex];

                if (!Config.CodeUser.Equals("РКВ") && !Config.CodeUser.Equals("ОП") && !Config.CodeUser.Equals("КД")) return;

                if (dtMain == null || dtMain.DefaultView.Count == 0 || dgvMain.CurrentRow == null || dgvMain.CurrentRow.Index == -1) return;

                id_Status = (int)dtMain.DefaultView[dgvMain.CurrentRow.Index]["id_Status"];

                id_Creator = (int)dtMain.DefaultView[dgvMain.CurrentRow.Index]["id_Creator"];

                AddPayment = (bool)dtMain.DefaultView[dgvMain.CurrentRow.Index]["AddPayment"];

                isAddReportMoney = (bool)dtMain.DefaultView[dgvMain.CurrentRow.Index]["isAddReportMoney"];

                isClosedDoc = (bool)dtMain.DefaultView[dgvMain.CurrentRow.Index]["isClosedDoc"];

                //if (!AddPayment || (id_Status != 5 && id_Status != 11)) return;

                if (AddPayment && (id_Status == 5 || id_Status == 11 || id_Status == 14 || id_Status == 15) 
                    && id_Creator == Nwuram.Framework.Settings.User.UserSettings.User.Id)
                {
                    cmsWorking.Show(MousePosition);
                    return;
                }

                if (isAddReportMoney && id_Creator == Nwuram.Framework.Settings.User.UserSettings.User.Id)
                {
                    cmsWorking.Show(MousePosition);
                    return;
                }

                if (isClosedDoc && Config.CodeUser.Equals("ОП") /*&& id_Creator == Nwuram.Framework.Settings.User.UserSettings.User.Id*/)
                {
                    cmsWorking.Show(MousePosition);
                    return;
                }

                if (Config.CodeUser.Equals("КД"))
                {
                    cmsWorking.Show(MousePosition);
                    return;
                }

            }
        }

        private void setLog(int id, string comment, int id_status, bool isSend)
        {
            DataTable dtTmpData = Config.hCntMain.getServiceRecordsBody(id);
            if (isSend)
                Logging.StartFirstLevel(1253);
            else
                Logging.StartFirstLevel(1256);

            Logging.Comment("Id СЗ: " + id);
            Logging.Comment("Номер СЗ: " + dtTmpData.Rows[0]["Number"].ToString());
            Logging.Comment("Произведено изменение статуса СЗ:");
            Logging.Comment("Статус до ID: " + dtTmpData.Rows[0]["id_Status"].ToString() + "; Наименование:" + dtTmpData.Rows[0]["nameStatus"].ToString());
            try
            {
                Logging.Comment("Статус после ID: " + id_status + "; Наименование:" + dtStatus.Select("id = " + id_status)[0]["cName"].ToString());
            }
            catch
            { };

            Logging.Comment("Тип СЗ: " + ((int)dtTmpData.Rows[0]["TypeServiceRecord"] == 0 ? "стандарт." : "предварит."));
            Logging.Comment("Тип СЗ по времени: " + ((int)dtTmpData.Rows[0]["TypeServiceRecordOnTime"] == 1 ? "разовая" : "ежемесячная"));

            Logging.Comment("Сумма:" + decimal.Parse(dtTmpData.Rows[0]["Summa"].ToString()).ToString("0.00"));
            Logging.Comment("Валюта:" + dtTmpData.Rows[0]["Valuta"].ToString());
            if ((bool)dtTmpData.Rows[0]["Mix"])
            {
                Logging.Comment("Сумма нал:" + dtTmpData.Rows[0]["SummaCash"].ToString());
                Logging.Comment("Сумма безнал:" + dtTmpData.Rows[0]["SummaNonCash"].ToString());
            }
            Logging.Comment("Объект ID: " + dtTmpData.Rows[0]["id_Object"].ToString() + "; Наименование:" + dtTmpData.Rows[0]["name_Object"].ToString());
            Logging.Comment("Описание, комментарий: " + dtTmpData.Rows[0]["Description"].ToString());
            Logging.Comment("Оплата: " + (!(bool)dtTmpData.Rows[0]["bCashNonCash"] ? "Наличные" : "Карта"));

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

        private void btAccept_Click(object sender, EventArgs e)
        {
            if (dtMain == null || dtMain.DefaultView.Count == 0 || dgvMain.CurrentRow == null || dgvMain.CurrentRow.Index == -1) return;

            if (DialogResult.Yes == MessageBox.Show(Config.centralText("Вы хотите подтвердить СЗ\nи передать её дальше по маршруту?\n"), "Подтвердить СЗ", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
            {
                DataTable dt = dtMain;
                int id_Status = (int)dtMain.DefaultView[dgvMain.CurrentRow.Index]["id_Status"];
                int id_ServiceRecords = (int)dtMain.DefaultView[dgvMain.CurrentRow.Index]["id"];
                int TypeServiceRecord = (int)dtMain.DefaultView[dgvMain.CurrentRow.Index]["TypeServiceRecord"];

                if (Config.CodeUser.Equals("ОП"))
                {
                    if (id_Status == 1 || id_Status == 6)
                    {
                        setLog(id_ServiceRecords, "", 4, true);
                        Config.hCntMain.updateServiceRecordsStatus(id_ServiceRecords, 4);
                    }
                    else if ( TypeServiceRecord == 1 && (id_Status == 12 || id_Status ==7))
                    {
                        setLog(id_ServiceRecords, "", 10, true);
                        Config.hCntMain.updateServiceRecordsStatus(id_ServiceRecords, 10);
                    }
                    getData();
                    return;
                }

                string strComment = "";
                if (Config.CodeUser.Equals("КНТ") || Config.CodeUser.Equals("КД") || Config.CodeUser.Equals("РКВ"))
                {                  
                    globalForm.frmComment frmCom = new globalForm.frmComment();
                    if (DialogResult.OK == frmCom.ShowDialog())
                    {
                        strComment = frmCom.getComment;
                    }
                }

                if (TypeServiceRecord == 0)
                {
                    //setLog(id_ServiceRecords, "", 4, true);
                    switch (id_Status)
                    {
                        //РКВ
                        case 1: //Config.hCntMain.updateServiceRecordsStatus(id_ServiceRecords, 2); ; break;
                        case 3: //Config.hCntMain.updateServiceRecordsStatus(id_ServiceRecords, 2); ; break;
                        case 9: // = 3
                        case 12: // = 6
                        case 6: setLog(id_ServiceRecords, strComment, 2, true); Config.hCntMain.updateServiceRecordsStatus(id_ServiceRecords, 2, strComment); ; break;
                        //КНТ
                        case 8:
                        case 2: setLog(id_ServiceRecords, strComment, 4, true); Config.hCntMain.updateServiceRecordsStatus(id_ServiceRecords, 4, strComment); ; break;
                        //КД
                        case 10:
                        case 4: setLog(id_ServiceRecords, strComment, 5, true); Config.hCntMain.updateServiceRecordsStatus(id_ServiceRecords, 5, strComment); ; break;
                        
                    }
                }else
                    if (TypeServiceRecord == 1)
                    {
                        switch (id_Status)
                        {
                            //РКВ
                            case 1: //Config.hCntMain.updateServiceRecordsStatus(id_ServiceRecords, 2); ; break;
                            case 3: //Config.hCntMain.updateServiceRecordsStatus(id_ServiceRecords, 2); ; break;
                            case 6: setLog(id_ServiceRecords, strComment, 2, true); Config.hCntMain.updateServiceRecordsStatus(id_ServiceRecords, 2, strComment); ; break;
                            case 7: //Config.hCntMain.updateServiceRecordsStatus(id_ServiceRecords, 2); ; break;
                            case 9: //Config.hCntMain.updateServiceRecordsStatus(id_ServiceRecords, 2); ; break;
                            case 12: setLog(id_ServiceRecords, strComment, 8, true); Config.hCntMain.updateServiceRecordsStatus(id_ServiceRecords, 8, strComment); ; break;
                            //КНТ
                            case 2: setLog(id_ServiceRecords, strComment, 4, true); Config.hCntMain.updateServiceRecordsStatus(id_ServiceRecords, 4, strComment); ; break;
                            case 8: setLog(id_ServiceRecords, strComment, 10, true); Config.hCntMain.updateServiceRecordsStatus(id_ServiceRecords, 10, strComment); ; break;
                            //КД
                            case 4: setLog(id_ServiceRecords, strComment, 7, true); Config.hCntMain.updateServiceRecordsStatus(id_ServiceRecords, 7, strComment); ; break;
                            case 10: setLog(id_ServiceRecords, strComment, 11, true); Config.hCntMain.updateServiceRecordsStatus(id_ServiceRecords, 11, strComment); ; break;
                        }
                    }
                //if (id_Status == 1) Config.hCntMain.updateServiceRecordsStatus(id_ServiceRecords, 2);



                getData();
            }
        }

        private void btRefuse_Click(object sender, EventArgs e)
        {
            if (dtMain == null || dtMain.DefaultView.Count == 0 || dgvMain.CurrentRow == null || dgvMain.CurrentRow.Index == -1) return;
            if (DialogResult.Yes == MessageBox.Show(Config.centralText("Вы хотите отклонить СЗ\nи вернуть её назад по маршруту?\n"), "Отклонене СЗ", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
            {
                string strComment = "";
                globalForm.frmComment frmCom = new globalForm.frmComment();
                if (DialogResult.OK == frmCom.ShowDialog())
                {
                    strComment = frmCom.getComment;
                }

                int id_Status = (int)dtMain.DefaultView[dgvMain.CurrentRow.Index]["id_Status"];
                int id_ServiceRecords = (int)dtMain.DefaultView[dgvMain.CurrentRow.Index]["id"];
                int TypeServiceRecord = (int)dtMain.DefaultView[dgvMain.CurrentRow.Index]["TypeServiceRecord"];
                if (TypeServiceRecord == 0)
                {
                    switch (id_Status)
                    {
                        //РКВ
                        //case 1: //Config.hCntMain.updateServiceRecordsStatus(id_ServiceRecords, 2); ; break;
                        //case 3: //Config.hCntMain.updateServiceRecordsStatus(id_ServiceRecords, 2); ; break;
                        //case 6: Config.hCntMain.updateServiceRecordsStatus(id_ServiceRecords, 2); ; break;
                        //КНТ
                        case 8: 
                        case 2: setLog(id_ServiceRecords, strComment, 3, false); Config.hCntMain.updateServiceRecordsStatus(id_ServiceRecords, 3, strComment); ; break;
                        //КД
                        case 10:
                        case 4: setLog(id_ServiceRecords, strComment, 6, false); Config.hCntMain.updateServiceRecordsStatus(id_ServiceRecords, 6, strComment); ; break;
                    }
                }
                else
                    if (TypeServiceRecord == 1)
                    {
                        switch (id_Status)
                        {
                            //РКВ
                            //case 1: //Config.hCntMain.updateServiceRecordsStatus(id_ServiceRecords, 2); ; break;
                            //case 3: //Config.hCntMain.updateServiceRecordsStatus(id_ServiceRecords, 2); ; break;
                            //case 6: Config.hCntMain.updateServiceRecordsStatus(id_ServiceRecords, 2); ; break;
                            //КНТ
                            case 2: setLog(id_ServiceRecords, strComment, 3, false); Config.hCntMain.updateServiceRecordsStatus(id_ServiceRecords, 3, strComment); ; break;
                            case 8: setLog(id_ServiceRecords, strComment, 9, false); Config.hCntMain.updateServiceRecordsStatus(id_ServiceRecords, 9, strComment); ; break;
                            //КД
                            case 4: setLog(id_ServiceRecords, strComment, 6, false); Config.hCntMain.updateServiceRecordsStatus(id_ServiceRecords, 6); ; break;
                            case 10: setLog(id_ServiceRecords, strComment, 12, false); Config.hCntMain.updateServiceRecordsStatus(id_ServiceRecords, 12); ; break;//12
                        }
                    }

                //if (id_Status == 1) Config.hCntMain.updateServiceRecordsStatus(id_ServiceRecords, 2);



                getData();
            }
        }

        private void dgvMain_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dtMain == null || dtMain.DefaultView.Count == 0 || dgvMain.CurrentRow == null || dgvMain.CurrentRow.Index == -1) return;

            int id_Status = (int)dtMain.DefaultView[dgvMain.CurrentRow.Index]["id_Status"];
            int id_Creator = (int)dtMain.DefaultView[dgvMain.CurrentRow.Index]["id_Creator"];

            btAccept.Enabled = (Config.CodeUser.Equals("РКВ") && (id_Status == 1 || id_Status == 3 || id_Status == 6 || id_Status == 7 || id_Status == 9 || id_Status == 12)) ||
                (Config.CodeUser.Equals("КНТ") && (id_Status == 2 || id_Status == 8)) ||
                (Config.CodeUser.Equals("КД") && (id_Status == 4 || id_Status == 10))  ||
                (Config.CodeUser.Equals("ОП") && (id_Status == 1 || id_Status == 6 || id_Status == 12 || id_Status == 7));


            btRefuse.Enabled = (Config.CodeUser.Equals("КНТ") && (id_Status == 2 || id_Status == 8)) ||
                (Config.CodeUser.Equals("КД") && (id_Status == 4 || id_Status == 10));

            //btEditBlock.Enabled = (Config.CodeUser.Equals("РКВ")  && (id_Status == 1 || id_Status == 3 || id_Status == 6) && id_Creator == Nwuram.Framework.Settings.User.UserSettings.User.Id)
            //    ||(Config.CodeUser.Equals("ОП") && (id_Status == 1 || id_Status == 3 || id_Status == 6))
            //    || (Config.CodeUser.Equals("КНТ") && (id_Status != 5 || id_Status != 11));

            btDelBlock.Enabled = (Config.CodeUser.Equals("РКВ") && (id_Status == 1 || id_Status == 3 || id_Status == 6) && id_Creator == Nwuram.Framework.Settings.User.UserSettings.User.Id)
                || (Config.CodeUser.Equals("ОП") && (id_Status == 1 || id_Status == 6));


            btnPrintFond.Enabled = ((int)dtMain.DefaultView[dgvMain.CurrentRow.Index]["TypeServiceRecordOnTime"] == 3 || 
                (dtMain.DefaultView[dgvMain.CurrentRow.Index]["id_ServiceRecords"] != DBNull.Value && 
                (int)dtMain.DefaultView[dgvMain.CurrentRow.Index]["TypeServiceRecordOnTime"] != 3));

        }


        private void cmsiTakeMoney_Click(object sender, EventArgs e)
        {
            if (dtMain == null || dtMain.DefaultView.Count == 0 || dgvMain.CurrentRow == null || dgvMain.CurrentRow.Index == -1) return;
            
            int id_ServiceRecords = (int)dtMain.DefaultView[dgvMain.CurrentRow.Index]["id"];
            decimal maxSumma = (decimal)dtMain.DefaultView[dgvMain.CurrentRow.Index]["Summa"];
            string valuta = dtMain.DefaultView[dgvMain.CurrentRow.Index]["Valuta"].ToString();

            frmOrderMoney frmO = new frmOrderMoney() {type = 1, status = 16,
                                                        id_ServiceRecords = id_ServiceRecords,
                maxSumma = maxSumma,
                valuta = valuta,
                isEdit = false
            };

            frmOrderMoneyMix frmO2 = new frmOrderMoneyMix() {
                type = 1,
                status = 16,
                id_ServiceRecords = id_ServiceRecords,
                maxSumma = maxSumma,
                valuta = valuta,
                isEdit = false
            };

            if ((bool)dtMain.DefaultView[dgvMain.CurrentRow.Index]["Mix"])
            {
                frmO2.Text = "Получение денег";
                frmO2.setDirector();
                if (frmO2.ShowDialog() == DialogResult.OK)
                {
                    Config.hCntMain.updateStatus(id_ServiceRecords, 16);
                    getData();
                }
            }
            else
            {
                frmO.Text = "Получение денег";
                frmO.setDirector();
                if (frmO.ShowDialog() == DialogResult.OK)
                {
                    Config.hCntMain.updateStatus(id_ServiceRecords, 16);
                    getData();
                }
            }
        }

        private void cmsiDropeMoney_Click(object sender, EventArgs e)
        {
            if (dtMain == null || dtMain.DefaultView.Count == 0 || dgvMain.CurrentRow == null || dgvMain.CurrentRow.Index == -1) return;

            int id_ServiceRecords = (int)dtMain.DefaultView[dgvMain.CurrentRow.Index]["id"];
            decimal maxSumma = (decimal)dtMain.DefaultView[dgvMain.CurrentRow.Index]["Summa"];
            string valuta = dtMain.DefaultView[dgvMain.CurrentRow.Index]["Valuta"].ToString();

            frmOrderMoney frmO = new frmOrderMoney()
            {
                type = 2,
                status = 17,
                id_ServiceRecords = id_ServiceRecords,
                maxSumma = maxSumma,
                valuta = valuta,
                isEdit = false
            };
            frmOrderMoneyMix frmO2 = new frmOrderMoneyMix()
            {
                type = 2,
                status = 17,
                id_ServiceRecords = id_ServiceRecords,
                maxSumma = maxSumma,
                valuta = valuta,
                isEdit=false
            };

            if ((bool)dtMain.DefaultView[dgvMain.CurrentRow.Index]["Mix"])
            {

                frmO2.Text = "Возврат денег";
                frmO2.setDirector();
                if (frmO2.ShowDialog() == DialogResult.OK)
                {
                    Config.hCntMain.updateStatus(id_ServiceRecords, 17);
                    getData();
                }
            }
            else
            {
                frmO.Text = "Возврат денег";
                frmO.setDirector();
                if (frmO.ShowDialog() == DialogResult.OK)
                {
                    Config.hCntMain.updateStatus(id_ServiceRecords, 17);
                    getData();
                }
            }
        }

        private void visibleElements()
        {
            btRefuse.Visible = (/*Config.CodeUser.Equals("КД") ||*/ Config.CodeUser.Equals("КНТ"));
            btAccept.Visible = (/*Config.CodeUser.Equals("КД") ||*/ Config.CodeUser.Equals("КНТ") || Config.CodeUser.Equals("РКВ") || Config.CodeUser.Equals("ОП"));
            btAddBlock.Visible = Config.CodeUser.Equals("РКВ") || Config.CodeUser.Equals("ОП");
            btEditBlock.Visible = Config.CodeUser.Equals("РКВ") || Config.CodeUser.Equals("ОП") || Config.CodeUser.Equals("КНТ") || Config.CodeUser.Equals("КД");
            btDelBlock.Visible = Config.CodeUser.Equals("РКВ") || Config.CodeUser.Equals("ОП");
            btViewPayment.Visible = Config.CodeUser.Equals("ОП") || Config.CodeUser.Equals("РКВ");
            btnAccept.Visible = btnRefuse.Visible = Config.CodeUser.Equals("КД");
            btnCheckReport.Visible = Config.CodeUser.Equals("ОП");
            Enter.Visible = Config.CodeUser.Equals("КД");
            chbKD.Visible = Config.CodeUser.Equals("КД");
            label9.Visible = panel5.Visible = Config.CodeUser == "КД";
        }

        private void enabledElements()
        {
            //if (Config.CodeUser.Equals("КНТ") || Config.CodeUser.Equals("ОП"))
            //    if (Config.CodeUser.Equals("РКВ"))
            //        btEditBlock.Enabled = (int)dgvMain.CurrentRow.Cells["Creator"].Value == UserSettings.User.Id &&
            //             (dgvMain.CurrentRow.Cells["cNameStatus"].Value.ToString().Equals("Черновик")
            //            || dgvMain.CurrentRow.Cells["cNameStatus"].Value.ToString().Equals("Отклонена КНТ")
            //            || dgvMain.CurrentRow.Cells["cNameStatus"].Value.ToString().Equals("Отклонена КД"));
            //    else if (Config.CodeUser.Equals("КНТ"))
            //        btEditBlock.Enabled =
            //             dgvMain.CurrentRow.Cells["cNameStatus"].Value.ToString().Equals("Передано КНТ");
            //    else if (Config.CodeUser.Equals("ОП"))
            //        btEditBlock.Enabled = (int)dgvMain.CurrentRow.Cells["Creator"].Value == UserSettings.User.Id &&
            //            (dgvMain.CurrentRow.Cells["cNameStatus"].Value.ToString().Equals("Черновик") ||
            //            dgvMain.CurrentRow.Cells["cNameStatus"].Value.ToString().Equals("Отклонена КД"));

        }

        private void btViewPayment_Click(object sender, EventArgs e)
        {
            if (Config.CodeUser.Equals("РКВ") || Config.CodeUser.Equals("ОП"))
            {
                frmViewPaymentOP frmP = new frmViewPaymentOP(UserSettings.User.Id);
                frmP.Text = "Журнал выплат/возвратов";
                frmP.ShowDialog();
            }
            else
            {
                frmViewPayment frmP = new frmViewPayment();
                frmP.ShowDialog();
            }
            getData();
        }

        private void cmbDeps_SelectionChangeCommitted(object sender, EventArgs e)
        {
            setFilter();
        }

        private void cmbStatus_SelectionChangeCommitted(object sender, EventArgs e)
        {
            setFilter();
        }

        private void tbNumber_TextChanged(object sender, EventArgs e)
        {
            setFilter();
        }

        private void dtpStart_ValueChanged(object sender, EventArgs e)
        {
            if (dtpStart.Value > dtpEnd.Value)
            {
                dtpEnd.Value = dtpStart.Value;
            }
        }

        private void dtpEnd_ValueChanged(object sender, EventArgs e)
        {
            if (dtpStart.Value > dtpEnd.Value)
            {
                dtpStart.Value = dtpEnd.Value;
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = DialogResult.No == MessageBox.Show("Закрыть программу?", "Выход из программы", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
        }

        private void btViewHistoryStatus_Click(object sender, EventArgs e)
        {
            if (dtMain == null || dtMain.DefaultView.Count == 0 || dgvMain.CurrentRow == null || dgvMain.CurrentRow.Index == -1) return;
            int id_ServiceRecords = (int)dtMain.DefaultView[dgvMain.CurrentRow.Index]["id"];

            globalForm.frmViewHistoryStatus frmVS = new globalForm.frmViewHistoryStatus();
            frmVS.id_ServiceRecords = id_ServiceRecords;
            frmVS.ShowDialog();
        }

        private void btChangeStatus_Click(object sender, EventArgs e)
        {
            if (dtMain == null || dtMain.DefaultView.Count == 0 || dgvMain.CurrentRow == null || dgvMain.CurrentRow.Index == -1) return;
            int id_ServiceRecords = (int)dtMain.DefaultView[dgvMain.CurrentRow.Index]["id"];
            globalForm.frmChangeStatus frmCS = new globalForm.frmChangeStatus();
            frmCS.id_ServiceRecords = id_ServiceRecords;
            if (DialogResult.OK == frmCS.ShowDialog())
            {
                getData();
            }
        }

        private void DoOnUIThread(MethodInvoker d)
        {
            if (this.InvokeRequired) { this.Invoke(d); } else { d(); }
        }

        private void btListNotePeriod_Click(object sender, EventArgs e)
        {
            Nwuram.Framework.ToExcelNew.ExcelUnLoad report = new Nwuram.Framework.ToExcelNew.ExcelUnLoad("Лист - 1");

            int indexRow = 1;

            report.Merge(indexRow, 1, indexRow, 9);
            report.AddSingleValue("Список СЗ за период", indexRow, 1);
            report.SetCellAlignmentToJustify(indexRow, 1, indexRow, 1);
            report.SetCellAlignmentToCenter(indexRow, 1, indexRow, 1);
            report.SetFontSize(indexRow, 1, indexRow, 1, 16);
            report.SetFontBold(indexRow, 1, indexRow, 1);
            indexRow++;
            indexRow++;
            DoOnUIThread(delegate()
            {
                report.Merge(indexRow, 1, indexRow, 9);
                report.AddSingleValue("Период сравнения с : " + dtpStart.Value.Date.ToShortDateString() + " по " + dtpEnd.Value.Date.ToShortDateString(), indexRow, 1);
                indexRow++;
            });

            report.Merge(indexRow, 1, indexRow, 9);
            report.AddSingleValue("Выгрузил: " + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername, indexRow, 1);
            indexRow++;

            report.Merge(indexRow, 1, indexRow, 9);
            report.AddSingleValue("Дата выгрузки: " + DateTime.Now.ToString(), indexRow, 1);
            indexRow++;
            indexRow++;
            indexRow++;

            report.AddSingleValue("Дата и время создания", indexRow, 1);
            report.AddSingleValue("№ СЗ", indexRow, 2);
            report.AddSingleValue("Блок/Отдел", indexRow, 3);
            report.AddSingleValue("Описание", indexRow, 4);            
            report.AddSingleValue("Сумма СЗ, руб.", indexRow, 5);
            report.AddSingleValue("Дата получения", indexRow, 6);
            report.AddSingleValue("Статус", indexRow, 7);
            report.AddSingleValue("Смена статуса", indexRow, 8);
            report.AddSingleValue("ФИО изм. статус", indexRow, 9);

            report.SetCellAlignmentToCenter(indexRow, 1, indexRow, 9);
            report.SetBorders(indexRow, 1, indexRow, 9);
            indexRow++;


            foreach (DataRowView r in dtMain.DefaultView)
            {

                report.AddSingleValue(r["DateCreate"].ToString(), indexRow, 1);
                report.AddSingleValue(r["Number"].ToString(), indexRow, 2);
                report.AddSingleValue(r["nameBlock"].ToString(), indexRow, 3);
                report.AddSingleValue(r["Description"].ToString(), indexRow, 4);
                report.AddSingleValue(r["Summa"].ToString(), indexRow, 5);
                //report.AddSingleValue(r["DataSumma"].ToString(), indexRow, 6);
                report.AddSingleValue(r["DataSummaPay"].ToString(), indexRow, 6);
                report.AddSingleValue(r["nameStatus"].ToString(), indexRow, 7);
                report.AddSingleValue(r["DateStatusChange"].ToString(), indexRow, 8);
                report.AddSingleValue(r["FIO"].ToString(), indexRow, 9);

                report.SetCellAlignmentToCenter(indexRow, 1, indexRow, 9);
                report.SetBorders(indexRow, 1, indexRow, 9);
                indexRow++;
            }

            report.SetColumnAutoSize(8, 1, indexRow - 1, 9);
            report.Show();
        }

        private void btReportNoteSingleDeps_Click(object sender, EventArgs e)
        {
            Nwuram.Framework.ToExcelNew.ExcelUnLoad report = new Nwuram.Framework.ToExcelNew.ExcelUnLoad("Лист - 1");

            int indexRow = 1;

            report.Merge(indexRow, 1, indexRow, 7);
            report.AddSingleValue("Отчет по СЗ по отделу", indexRow, 1);
            report.SetCellAlignmentToJustify(indexRow, 1, indexRow, 1);
            report.SetCellAlignmentToCenter(indexRow, 1, indexRow, 1);
            report.SetFontSize(indexRow, 1, indexRow, 1, 16);
            report.SetFontBold(indexRow, 1, indexRow, 1);
            indexRow++;
            indexRow++;
            DoOnUIThread(delegate()
            {
                report.Merge(indexRow, 1, indexRow, 7);
                report.AddSingleValue("Период сравнения с : " + dtpStart.Value.Date.ToShortDateString() + " по " + dtpEnd.Value.Date.ToShortDateString(), indexRow, 1);
                indexRow++;

                report.Merge(indexRow, 1, indexRow, 7);
                report.AddSingleValue("Блок: " + cmbBlock.Text, indexRow, 1);
                indexRow++;

                report.Merge(indexRow, 1, indexRow, 7);
                report.AddSingleValue("Отдел: " + cmbDeps.Text, indexRow, 1);
                indexRow++;
            });

            report.Merge(indexRow, 1, indexRow, 7);
            report.AddSingleValue("Выгрузил: " + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername, indexRow, 1);
            indexRow++;

            report.Merge(indexRow, 1, indexRow, 7);
            report.AddSingleValue("Дата выгрузки: " + DateTime.Now.ToString(), indexRow, 1);
            indexRow++;
            indexRow++;
            indexRow++;

            report.AddSingleValue("Дата и время создания", indexRow, 1);
            report.AddSingleValue("№ СЗ", indexRow, 2);
            report.AddSingleValue("№№ СЗ", indexRow, 3);
            report.AddSingleValue("Описание", indexRow, 4);
            report.AddSingleValue("Статус", indexRow, 5);
            report.AddSingleValue("Сумма СЗ", indexRow, 6);
            report.AddSingleValue("Дата получения ДС", indexRow, 7);
            
            //report.AddSingleValue("Смена статуса", indexRow, 8);
            //report.AddSingleValue("ФИО изм. статус", indexRow, 9);

            report.SetCellAlignmentToCenter(indexRow, 1, indexRow, 7);
            report.SetBorders(indexRow, 1, indexRow, 7);
            indexRow++;

            DataTable dtTmp = dtMain.DefaultView.ToTable().Copy();

            var groupedData
                    = from b in dtTmp.AsEnumerable()
                      group b by new { id_Block = b.Field<Int32>("id_Block"), id_Department = b.Field<Int32>("id_Department") } into g
                      select new
                      {
                          id_Block = g.Key.id_Block,
                          id_Department = g.Key.id_Department
                        //  netto = g.Sum(x => x.Field<decimal>("netto"))
                      };


            foreach (var grp in groupedData)
            {
                DataRow[] row = dtTmp.Select(string.Format("id_Block = {0} AND id_Department = {1}", grp.id_Block, grp.id_Department));
                foreach (DataRow r in row)
                {
                    //report.AddSingleValue(r["DateCreate"].ToString(), indexRow, 1);
                    //report.AddSingleValue(r["Number"].ToString(), indexRow, 2);
                    ////report.AddSingleValue(r["nameBlock"].ToString(), indexRow, 3);
                    //report.AddSingleValue(r["Description"].ToString(), indexRow, 4);
                    //report.AddSingleValue(r["nameStatus"].ToString(), indexRow, 5);
                    //report.AddSingleValue(r["Summa"].ToString(), indexRow, 6);
                    //report.AddSingleValue(r["DataSumma"].ToString(), indexRow, 7);                    

                    //report.SetCellAlignmentToCenter(indexRow, 1, indexRow, 9);
                    //report.SetBorders(indexRow, 1, indexRow, 9);
                    //indexRow++;
                    DataTable dtMultim = Config.hCntMain.getMultipleReceivingMone((int)r["id"]);
                    if (dtMultim != null && dtMultim.Rows.Count > 0)
                    {
                        report.Merge(indexRow, 1, indexRow + dtMultim.Rows.Count - 1, 1);
                        report.AddSingleValue(r["DateCreate"].ToString(), indexRow, 1);

                        report.Merge(indexRow, 2, indexRow + dtMultim.Rows.Count - 1, 2);
                        report.AddSingleValue(r["Number"].ToString(), indexRow, 2);

                        report.Merge(indexRow, 4, indexRow + dtMultim.Rows.Count - 1, 4);
                        report.AddSingleValue(r["Description"].ToString(), indexRow, 4);

                        report.Merge(indexRow, 5, indexRow + dtMultim.Rows.Count - 1, 5);
                        report.AddSingleValue(r["nameStatus"].ToString(), indexRow, 5);

                        foreach (DataRow rM in dtMultim.Rows)
                        {
                            report.AddSingleValue(rM["SubNumber"].ToString(), indexRow, 3);
                            //report.AddSingleValue(r["Description"].ToString(), indexRow, 4);
                            //report.AddSingleValue(r["nameStatus"].ToString(), indexRow, 5);
                            report.AddSingleValue(rM["Summa"].ToString(), indexRow, 6);
                            //report.AddSingleValue(rM["DataSumma"].ToString(), indexRow, 7);
                            report.AddSingleValue(r["DataSummaPay"].ToString(), indexRow, 7);

                            report.SetCellAlignmentToCenter(indexRow, 1, indexRow, 7);
                            report.SetCellAlignmentToJustify(indexRow, 1, indexRow, 7);
                            report.SetBorders(indexRow, 1, indexRow, 7);
                            indexRow++;
                        }

                    }
                    else
                    {
                        report.AddSingleValue(r["DateCreate"].ToString(), indexRow, 1);
                        report.AddSingleValue(r["Number"].ToString(), indexRow, 2);
                        //report.AddSingleValue(r["nameBlock"].ToString(), indexRow, 3);
                        report.AddSingleValue(r["Description"].ToString(), indexRow, 4);
                        report.AddSingleValue(r["nameStatus"].ToString(), indexRow, 5);
                        report.AddSingleValue(r["Summa"].ToString(), indexRow, 6);
                        //report.AddSingleValue(r["DataSumma"].ToString(), indexRow, 7);
                        report.AddSingleValue(r["DataSummaPay"].ToString(), indexRow, 7);

                        report.SetCellAlignmentToCenter(indexRow, 1, indexRow, 7);
                        report.SetBorders(indexRow, 1, indexRow, 7);
                        indexRow++;
                    }
                }
            }


            report.SetColumnAutoSize(8, 1, indexRow - 1, 7);
            report.Show();
        }

        private void btReportNoteMoreDeps_Click(object sender, EventArgs e)
        {

            Logging.StartFirstLevel(79);
            Logging.Comment("Выгружен в Excel отчет по СЗ со следующими параметрами");

            Logging.Comment("Период с: " + dtpStart.Value.ToShortDateString());
            Logging.Comment("Период по: " + dtpEnd.Value.ToShortDateString());
            Logging.Comment("Блок ID: " + cmbBlock.SelectedValue.ToString() + "; Наименование:" + cmbBlock.Text);
            Logging.Comment("Отдел ID: " + cmbDeps.SelectedValue.ToString() + "; Наименование:" + cmbDeps.Text);
            Logging.Comment("Статус ID: " + cmbStatus.SelectedValue.ToString() + "; Наименование:" + cmbStatus.Text);
            Logging.Comment("Объект ID: " + cmbObjects.SelectedValue.ToString() + "; Наименование:" + cmbObjects.Text);

            if (tbNumber.Text.Trim().Length != 0)
                Logging.Comment("Поиск по № СЗ: " + tbNumber.Text);
            if (tbInfo.Text.Trim().Length != 0)
                Logging.Comment("Поиск по описанию: " + tbInfo.Text);

            Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
            Logging.StopFirstLevel();

            Nwuram.Framework.ToExcelNew.ExcelUnLoad report = new Nwuram.Framework.ToExcelNew.ExcelUnLoad("Лист - 1");

            int indexRow = 1;

            report.Merge(indexRow, 1, indexRow, 7);
            //report.AddSingleValue("Отчет по СЗ по отделу", indexRow, 1);
            report.AddSingleValue("Отчет по СЗ", indexRow, 1);
            report.SetCellAlignmentToJustify(indexRow, 1, indexRow, 1);
            report.SetCellAlignmentToCenter(indexRow, 1, indexRow, 1);
            report.SetFontSize(indexRow, 1, indexRow, 1, 16);
            report.SetFontBold(indexRow, 1, indexRow, 1);
            indexRow++;
            indexRow++;
            DoOnUIThread(delegate()
            {
                report.Merge(indexRow, 1, indexRow, 7);
                report.AddSingleValue("Период сравнения с : " + dtpStart.Value.Date.ToShortDateString() + " по " + dtpEnd.Value.Date.ToShortDateString(), indexRow, 1);
                indexRow++;

                //report.Merge(indexRow, 1, indexRow, 7);
                //report.AddSingleValue("Блок: " + cmbBlock.Text, indexRow, 1);
                //indexRow++;

                //report.Merge(indexRow, 1, indexRow, 7);
                //report.AddSingleValue("Отдел: " + cmbDeps.Text, indexRow, 1);
                //indexRow++;
            });

            report.Merge(indexRow, 1, indexRow, 7);
            report.AddSingleValue("Выгрузил: " + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername, indexRow, 1);
            indexRow++;

            report.Merge(indexRow, 1, indexRow, 7);
            report.AddSingleValue("Дата выгрузки: " + DateTime.Now.ToString(), indexRow, 1);
            indexRow++;
            indexRow++;
            indexRow++;

            report.AddSingleValue("Дата и время создания", indexRow, 1);
            report.AddSingleValue("№ СЗ", indexRow, 2);
            report.AddSingleValue("№№ СЗ", indexRow, 3);
            report.AddSingleValue("Описание", indexRow, 4);
            report.AddSingleValue("Статус", indexRow, 5);
            report.AddSingleValue("Сумма СЗ", indexRow, 6);
            report.AddSingleValue("Дата получения ДС", indexRow, 7);

            //report.AddSingleValue("Смена статуса", indexRow, 8);
            //report.AddSingleValue("ФИО изм. статус", indexRow, 9);

            report.SetCellAlignmentToCenter(indexRow, 1, indexRow, 7);
            report.SetBorders(indexRow, 1, indexRow, 7);
            indexRow++;

            DataTable dtTmp = dtMain.DefaultView.ToTable().Copy();

            var groupedData
                    = from b in dtTmp.AsEnumerable()
                      group b by new { id_Block = b.Field<Int32>("id_Block"), id_Department = b.Field<Int32>("id_Department") } into g
                      select new
                      {
                          id_Block = g.Key.id_Block,
                          id_Department = g.Key.id_Department
                          //  netto = g.Sum(x => x.Field<decimal>("netto"))
                      };


            foreach (var grp in groupedData)
            {
                DataRow[] row = dtTmp.Select(string.Format("id_Block = {0} AND id_Department = {1}", grp.id_Block, grp.id_Department));
                report.Merge(indexRow, 1, indexRow, 7);
                report.AddSingleValue(row[0]["nameBlock"].ToString(), indexRow, 1);
                report.SetCellAlignmentToCenter(indexRow, 1, indexRow, 7);
                report.SetBorders(indexRow, 1, indexRow, 7);
                indexRow++;

                foreach (DataRow r in row)
                {
                    DataTable dtMultim = Config.hCntMain.getMultipleReceivingMone((int)r["id"]);
                    if (dtMultim != null && dtMultim.Rows.Count > 0)
                    {
                        report.Merge(indexRow, 1, indexRow + dtMultim.Rows.Count-1, 1);
                        report.AddSingleValue(r["DateCreate"].ToString(), indexRow, 1);

                        report.Merge(indexRow, 2, indexRow + dtMultim.Rows.Count-1, 2);
                        report.AddSingleValue(r["Number"].ToString(), indexRow, 2);

                        report.Merge(indexRow, 4, indexRow + dtMultim.Rows.Count - 1, 4);
                        report.AddSingleValue(r["Description"].ToString(), indexRow, 4);

                        report.Merge(indexRow, 5, indexRow + dtMultim.Rows.Count - 1, 5);
                        report.AddSingleValue(r["nameStatus"].ToString(), indexRow, 5);

                        foreach (DataRow rM in dtMultim.Rows)
                        {
                            report.AddSingleValue(rM["SubNumber"].ToString(), indexRow, 3);
                            //report.AddSingleValue(r["Description"].ToString(), indexRow, 4);
                            //report.AddSingleValue(r["nameStatus"].ToString(), indexRow, 5);
                            report.AddSingleValue(rM["Summa"].ToString(), indexRow, 6);
                            //report.AddSingleValue(rM["DataSumma"].ToString(), indexRow, 7);
                            report.AddSingleValue(r["DataSummaPay"].ToString(), indexRow, 7);

                            report.SetCellAlignmentToCenter(indexRow, 1, indexRow, 7);
                            report.SetCellAlignmentToJustify(indexRow, 1, indexRow, 7);
                            report.SetBorders(indexRow, 1, indexRow, 7);
                            indexRow++;
                        }

                    }
                    else
                    {
                        report.AddSingleValue(r["DateCreate"].ToString(), indexRow, 1);
                        report.AddSingleValue(r["Number"].ToString(), indexRow, 2);
                        //report.AddSingleValue(r["nameBlock"].ToString(), indexRow, 3);
                        report.AddSingleValue(r["Description"].ToString(), indexRow, 4);
                        report.AddSingleValue(r["nameStatus"].ToString(), indexRow, 5);
                        report.AddSingleValue(r["Summa"].ToString(), indexRow, 6);
                        //report.AddSingleValue(r["DataSumma"].ToString(), indexRow, 7);
                        report.AddSingleValue(r["DataSummaPay"].ToString(), indexRow, 7);

                        report.SetCellAlignmentToCenter(indexRow, 1, indexRow, 7);
                        report.SetBorders(indexRow, 1, indexRow, 7);
                        indexRow++;
                    }
                }
            }


            report.SetColumnAutoSize(8, 1, indexRow - 1, 7);
            report.Show();
        }

        private void tsmiAddDoc_Click(object sender, EventArgs e)
        {
            int id = (int)dtMain.DefaultView[dgvMain.CurrentRow.Index]["id"];
            workDoc.frmDocument frmD = new workDoc.frmDocument(false);
            frmD.id_ServiceRecords = id;
            frmD.ShowDialog();
        }

        private void tsmiSetReport_Click(object sender, EventArgs e)
        {
            workDoc.frmSetReport frm = new workDoc.frmSetReport()
            { id_ServiceRecords = (int)dtMain.DefaultView[dgvMain.CurrentRow.Index]["id"],
                numberSR = (int)dtMain.DefaultView[dgvMain.CurrentRow.Index]["Number"],
                typeSZ = (int)dtMain.DefaultView[dgvMain.CurrentRow.Index]["TypeServiceRecord"],
                Summa = (decimal)dtMain.DefaultView[dgvMain.CurrentRow.Index]["Summa"],
                Valuta = dtMain.DefaultView[dgvMain.CurrentRow.Index]["Valuta"].ToString(),
                Mix = (bool)dtMain.DefaultView[dgvMain.CurrentRow.Index]["Mix"]
            };

            if (DialogResult.OK == frm.ShowDialog())
                getData();
        }
        private void cmsWorking_Opening(object sender, CancelEventArgs e)
        {

            int typeSZonTime = (int)dtMain.DefaultView[dgvMain.CurrentRow.Index]["TypeServiceRecordOnTime"];
            DataTable dtHistory = Config.hCntMain.getHistoryOrderAndReturn((int)dtMain.DefaultView[dgvMain.CurrentRow.Index]["id"]);
            double maxSumma = double.Parse(dtHistory.Rows[0]["maxSumma"].ToString());
            double sumGet = double.Parse(dtHistory.Rows[0]["sumGet"].ToString());
            double sumGetInValuta = double.Parse(dtHistory.Rows[0]["sumGetInValuta"].ToString());
            double sumReturn = double.Parse(dtHistory.Rows[0]["sumReturn"].ToString());
            double balanceGetInValuta = double.Parse(dtHistory.Rows[0]["balanceGetInValuta"].ToString());
            double balanceGet = double.Parse(dtHistory.Rows[0]["balanceGet"].ToString());
            double balanceReturn = double.Parse(dtHistory.Rows[0]["balanceReturn"].ToString());
            double debtReport = double.Parse(dtHistory.Rows[0]["debtReport"].ToString());
            string valuta = dtHistory.Rows[0]["Valuta"].ToString();

            int monthDateCreateReport = DateTime.Parse(dtHistory.Rows[0]["DateCreateReport"].ToString()).Month;
            int yearDateCreateReport = DateTime.Parse(dtHistory.Rows[0]["DateCreateReport"].ToString()).Year;
            bool isTodayMonthCreateReport = DateTime.Now.Month.Equals(monthDateCreateReport) && DateTime.Now.Year.Equals(yearDateCreateReport);
           
            //string valueSettings = Config.hCntMain.GetSettings("овпд");

            List<int> takeMoneylist = new List<int> {5, 11, 14, 15, 19, 20 };
            List<int> dropeMoneylist = new List<int> { 14, 15, 19 };
            List<int> SetReportlist = new List<int> { 14, 19 };

            if (typeSZonTime == 3 && id_Status == 5 && (Config.CodeUser.Equals("ОП") || Config.CodeUser.Equals("КД")))
            {
                tsmiClose.Visible = true;
                cmsiTakeMoney.Visible =
                cmsiDropeMoney.Visible =
                tsmiAddDoc.Visible =
                tsmiSetReport.Visible =
                tsmiAnylSZ.Visible = false;
                return;
            }

            cmsiTakeMoney.Visible = ((typeSZonTime == 2 && (isTodayMonthCreateReport || (!isTodayMonthCreateReport && sumGet == 0 && (id_Status == 20 || id_Status == 5 || id_Status == 7))) 
                                        && ((balanceGet > 0 && valuta == "RUB")
                                            || (valuta != "RUB"))) ||
                                    (typeSZonTime == 1 && ((balanceGet > 0 && valuta == "RUB") || (valuta != "RUB"))))
                                        && takeMoneylist.Contains(id_Status)
                                        && (Config.CodeUser.Equals("РКВ") || Config.CodeUser.Equals("ОП"))
                                        && id_Creator == Nwuram.Framework.Settings.User.UserSettings.User.Id;


            cmsiDropeMoney.Visible = (Config.CodeUser.Equals("РКВ") || Config.CodeUser.Equals("ОП")) 
                                    && id_Creator == Nwuram.Framework.Settings.User.UserSettings.User.Id 
                                    && (debtReport > 0 || balanceReturn > 0) && dropeMoneylist.Contains(id_Status);

            tsmiSetReport.Visible = SetReportlist.Contains(id_Status) && (debtReport >= 0)
                                    && (Config.CodeUser.Equals("РКВ") || Config.CodeUser.Equals("ОП")) 
                                    && id_Creator == Nwuram.Framework.Settings.User.UserSettings.User.Id;

            if (tsmiSetReport.Visible == false) tsmiAddDoc.Visible = false;

            tsmiClose.Visible = isClosedDoc && Config.CodeUser.Equals("ОП") && id_Status == 20;


            tsmiAnylSZ.Visible = Config.CodeUser.Equals("КД");

            //tsmiAddDoc.Visible = (Config.CodeUser.Equals("РКВ") || Config.CodeUser.Equals("ОП")) && id_Creator == Nwuram.Framework.Settings.User.UserSettings.User.Id;

            if (typeSZonTime == 2 && (id_Status == 14 || id_Status == 15 || id_Status == 19) /* && debtReport > 0*/ && !isTodayMonthCreateReport && (Config.CodeUser.Equals("РКВ") || Config.CodeUser.Equals("ОП")) && id_Creator == Nwuram.Framework.Settings.User.UserSettings.User.Id)
            {
                MessageBox.Show(Config.centralText("Для заказа по ежемесячной СЗ\nотчет должен быть подтвержден оператором.\n"), "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (debtReport > 0)
                tsmiSetReport.Visible = cmsiDropeMoney.Visible = true;
            }

            if (typeSZonTime == 2 && ((valuta == "RUB" && (sumGet - sumReturn) == maxSumma) || (valuta != "RUB" && (sumGetInValuta - sumReturn) == maxSumma)) && debtReport == 0 && isTodayMonthCreateReport && (Config.CodeUser.Equals("РКВ") || Config.CodeUser.Equals("ОП")) && id_Creator == Nwuram.Framework.Settings.User.UserSettings.User.Id)
            {
                MessageBox.Show(Config.centralText("Вы сможете заказать ДС только в следующем месяце.\n"), "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void tsmiClose_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show(Config.centralText("Вы уверены, что хотите закрыть СЗ?\n"), "Подтвердить СЗ", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
            {
                int id_Status = (int)dtMain.DefaultView[dgvMain.CurrentRow.Index]["id_Status"];
                int id_ServiceRecords = (int)dtMain.DefaultView[dgvMain.CurrentRow.Index]["id"];
                int TypeServiceRecord = (int)dtMain.DefaultView[dgvMain.CurrentRow.Index]["TypeServiceRecord"];


                DataTable dtResult = null;

                dtResult = Config.hCntMain.updateServiceRecordsStatus(id_ServiceRecords, 13);



                Logging.StartFirstLevel(540);
                Logging.Comment("СЗ Id: " + dtMain.DefaultView[dgvMain.CurrentRow.Index]["id"].ToString() + " ;Номер: " + dtMain.DefaultView[dgvMain.CurrentRow.Index]["Number"].ToString());
                Logging.Comment("Объект ID: " + dtMain.DefaultView[dgvMain.CurrentRow.Index]["id_Object"].ToString() + "; наименование: ");// + dtMain.DefaultView[dgvMain.CurrentRow.Index]["id"].ToString());
                Logging.Comment("Блок ID: " + dtMain.DefaultView[dgvMain.CurrentRow.Index]["id_Block"].ToString() + "; наименование: "+ dtMain.DefaultView[dgvMain.CurrentRow.Index]["nameBlock"].ToString());
                Logging.Comment("Отдел ID: " + dtMain.DefaultView[dgvMain.CurrentRow.Index]["id_Department"].ToString() + "; наименование: " + dtMain.DefaultView[dgvMain.CurrentRow.Index]["nameDep"].ToString());
                Logging.Comment("Сумма СЗ: " + dtMain.DefaultView[dgvMain.CurrentRow.Index]["Summa"].ToString());
                Logging.Comment("Валюта: " + dtMain.DefaultView[dgvMain.CurrentRow.Index]["Valuta"].ToString());
                Logging.Comment("Описание: " + dtMain.DefaultView[dgvMain.CurrentRow.Index]["Description"].ToString());
                Logging.Comment("Предполагаемая дата получения: " + dtMain.DefaultView[dgvMain.CurrentRow.Index]["DataSumma"].ToString());

                if (dtResult != null && dtResult.Rows.Count > 0 && !dtResult.Columns.Contains("error"))
                {
                    Logging.Comment("Статус ДО ID: " + dtResult.Rows[0]["id_prev"].ToString() + "; Наименование: " + dtResult.Rows[0]["cName_prev"].ToString());
                    Logging.Comment("Статус После ID: " + dtResult.Rows[0]["id"].ToString() + "; Наименование: " + dtResult.Rows[0]["cName"].ToString());
                }

                Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                                + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
                Logging.StopFirstLevel();


                //setLog(id_ServiceRecords, "", 13, true);

                
                getData();
            }
        }

        private void rbWork_Click(object sender, EventArgs e)
        {
            dtStatus = Config.hCntMain.getStatus();
            if (!rbWork.Checked)
            {
                chbKD.Checked = rbWork.Checked = false;
                DataTable table = new DataTable();
                table = dtStatus.Clone();
                var r = dtStatus.Select("id = 13 or id = 21");
                foreach (DataRow dr in r)
                {
                    object[] row = dr.ItemArray;
                    table.Rows.Add(row);
                }
                table.DefaultView.Sort = "cName ASC";
                cmbStatus.DataSource = table;
                cmbStatus.ValueMember = "id";
                cmbStatus.DisplayMember = "cName";
                cmbStatus.SelectedValue = 13;

                cmbStatus.Enabled = true;
            }
            else
            {
                getStatus();
            }
            setFilter();
        }

        private void помощьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String code = Nwuram.Framework.Settings.User.UserSettings.User.StatusCode;
            //MessageBox.Show("помощьToolStripMenuItem_Click UserStatusCode " + code);
            String fullCode = Nwuram.Framework.Settings.User.UserSettings.User.Status;
            //MessageBox.Show("помощьToolStripMenuItem_Click UserStatus " + code);
            if (Directory.Exists(Application.StartupPath + @"\HelpDoc"))
            {
                DirectoryInfo dir = new DirectoryInfo(Application.StartupPath + @"\HelpDoc");
                String[] fileList = Directory.GetFiles(Application.StartupPath + @"\HelpDoc");
                FileInfo[] files = dir.GetFiles("*" + fullCode + "*");
                if (files.Count() > 0)
                {
                    Process.Start(files[0].FullName);
                }
                else
                {
                    MessageBox.Show("Нет руководство пользователя", "Нет руководства", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
        }

        private void dgvMain_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           if (dgvMain.CurrentRow.Cells["Enter"].Value == null) dgvMain.CurrentRow.Cells["Enter"].Value = false;

            if (dgvMain.CurrentRow.Cells["Enter"].Value != null && e.ColumnIndex == 0)

                if ((dgvMain.CurrentRow.Cells["cNameStatus"].Value.Equals("На согласовании с КД") ||
                  dgvMain.CurrentRow.Cells["cNameStatus"].Value.Equals("На повторном согласовании с КД"))
                    && (bool)(dgvMain.CurrentRow.Cells["Enter"].Value) == false)
                {
                    dgvMain.CurrentRow.Cells["Enter"].Value = true;
                    Logging.StartFirstLevel(1333);
                    Logging.Comment("СЗ id = " + dgvMain.CurrentRow.Cells["id"].Value.ToString() + " была выбрана\n(поставлен чек-бокс)");
                    Logging.Comment("Сумма: " + dgvMain.CurrentRow.Cells["cSumma"].Value.ToString());
                    Logging.Comment("Описание: " + dgvMain.CurrentRow.Cells["cDescription"].Value.ToString());
                    Logging.StopFirstLevel();
                }

                else
                {
                    dgvMain.CurrentRow.Cells["Enter"].Value = false;
                    Logging.StartFirstLevel(1334);
                    Logging.Comment("СЗ id = " + dgvMain.CurrentRow.Cells["id"].Value.ToString() + " был снять чек-бокс");
                    Logging.Comment("Сумма: " + dgvMain.CurrentRow.Cells["cSumma"].Value.ToString());
                    Logging.Comment("Описание: " + dgvMain.CurrentRow.Cells["cDescription"].Value.ToString());
                    Logging.StopFirstLevel();
                }
            enabledElements();
            CheckBtnAcceptrefuse();

        }

        private void CheckBtnAcceptrefuse()
        {
          try 
          {
            foreach (DataGridViewRow row in dgvMain.Rows)
            {
                    if (row.Cells["Enter"].Value == null)
                    {
                        row.Cells["Enter"].Value = false;
                        
                    }
                    else if ((bool)row.Cells["Enter"].Value == true)
                    {
                        btnAccept.Enabled = btnRefuse.Enabled = true;
                        return;
                    }
                    else
                        btnAccept.Enabled = btnRefuse.Enabled = false;
            }

          }
          catch { }

        }
        private void dgvMain_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            if (dtMain == null || dtMain.DefaultView.Count == 0 || dgvMain.CurrentRow == null ||  e.RowIndex == -1) return;            
            int id = (int)dtMain.DefaultView[e.RowIndex]["id"];
          
            frmServiceNote frmS = new frmServiceNote();
            frmS.id = id;
            frmS.Text = "Просмотр СЗ";
            frmS.setIsView();
            if (DialogResult.OK == frmS.ShowDialog())
            {
                getData();
            }
        }

        private void chbUpdate_CheckedChanged(object sender, EventArgs e)
        {
            Config.hCntMain.setSettingUpdateButton(chbUpdate.Checked ? "1" : "0");
            timUpdate.Enabled = chbUpdate.Checked;
        }

        private void dgvMain_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            Color rColor = Color.White;

            rColor = double.Parse(dtMain.DefaultView[e.RowIndex]["DebtReport"].ToString()) > 0 ? panel4.BackColor : Color.White;

            if (Config.CodeUser == "КД")
                rColor = (int)dtMain.DefaultView[e.RowIndex]["TypeServiceRecord"] == 1 ? panel5.BackColor : Color.White;

            dgvMain.Rows[e.RowIndex].DefaultCellStyle.BackColor
            = dgvMain.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = rColor;

            dgvMain.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.Black;


            if ((int)dtMain.DefaultView[e.RowIndex]["TypeServiceRecordOnTime"] == 2)
                dgvMain.Rows[e.RowIndex].Cells["cDateCreate"].Style.BackColor =
                     dgvMain.Rows[e.RowIndex].Cells["cDateCreate"].Style.SelectionBackColor = panel2.BackColor;
            else if ((int)dtMain.DefaultView[e.RowIndex]["TypeServiceRecordOnTime"] == 1) dgvMain.Rows[e.RowIndex].Cells["cDateCreate"].Style.BackColor =
                    dgvMain.Rows[e.RowIndex].Cells["cDateCreate"].Style.SelectionBackColor = panel1.BackColor;
            else if ((int)dtMain.DefaultView[e.RowIndex]["TypeServiceRecordOnTime"] == 3) dgvMain.Rows[e.RowIndex].Cells["cDescription"].Style.BackColor =
                    dgvMain.Rows[e.RowIndex].Cells["cDescription"].Style.SelectionBackColor = panel6.BackColor;

            if (dtMain.Columns.Contains("id_ServiceRecords"))
            {
                if (dtMain.DefaultView[e.RowIndex]["id_ServiceRecords"] != DBNull.Value && (int)dtMain.DefaultView[e.RowIndex]["TypeServiceRecordOnTime"] != 3) dgvMain.Rows[e.RowIndex].Cells["cDescription"].Style.BackColor =
                         dgvMain.Rows[e.RowIndex].Cells["cDescription"].Style.SelectionBackColor = panel6.BackColor;
            }

            
        }

        private void btnCheckReport_Click(object sender, EventArgs e)
        {
            frmCheckReport frm = new frmCheckReport();
            if (DialogResult.Cancel == frm.ShowDialog())
                getData();

        }


        private void chbKD_CheckedChanged(object sender, EventArgs e)
        {
            isKD();
            getData();

            rbWork.Checked = chbKD.Checked;
            rbWork_Click(sender, e);
        }

        private void анулироватьСЗToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            if (DialogResult.Yes == MessageBox.Show("Аннулировать СЗ ?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                dt = Config.hCntMain.updateStatus((int)dtMain.DefaultView[dgvMain.CurrentRow.Index]["id"], 21);
                if (dt != null ? dt.Rows.Count > 0 && dt.Columns.Contains("error") ? true : false : false)
                    MessageBox.Show("По СЗ №" + dtMain.DefaultView[dgvMain.CurrentRow.Index]["Number"] + " были получены ДС.\nАннулирование невозможно.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                else MessageBox.Show("Аннулирование прошло успешно.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);

                int id = (int)dtMain.DefaultView[dgvMain.CurrentRow.Index]["id"];

                DataTable dtTmpData = Config.hCntMain.getServiceRecordsBody(id);

                Logging.StartFirstLevel(1504);

                Logging.Comment("Id СЗ: " + id);
                Logging.Comment("Номер СЗ: " + dtTmpData.Rows[0]["Number"].ToString());

                Logging.Comment("Тип СЗ: " + ((int)dtTmpData.Rows[0]["TypeServiceRecord"] == 0 ? "стандарт." : "предварит."));
                Logging.Comment("Тип СЗ по времени: " + ((int)dtTmpData.Rows[0]["TypeServiceRecordOnTime"] == 1 ? "разовая" : "ежемесячная"));


                Logging.Comment("Сумма:" + decimal.Parse(dtTmpData.Rows[0]["Summa"].ToString()).ToString("0.00"));
                Logging.Comment("Валюта:" + dtTmpData.Rows[0]["Valuta"].ToString());
                if ((bool)dtTmpData.Rows[0]["Mix"])
                {
                    Logging.Comment("Сумма нал:" + dtTmpData.Rows[0]["SummaCash"].ToString());
                    Logging.Comment("Сумма безнал:" + dtTmpData.Rows[0]["SummaNonCash"].ToString());
                }
                Logging.Comment("Объект ID: " + dtTmpData.Rows[0]["id_Object"].ToString() + "; Наименование:" + "");

                Logging.Comment("Описание, комментарий: " + dtTmpData.Rows[0]["Description"].ToString());
                Logging.Comment("Оплата: " + (!(bool)dtTmpData.Rows[0]["bCashNonCash"] ? "Наличные" : "Карта"));

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

                if (dt != null && dt.Rows.Count > 0 && !dt.Columns.Contains("error"))
                {
                    Logging.Comment("Статус ДО ID: " + dt.Rows[0]["id_prev"].ToString() + "; Наименование: " + dt.Rows[0]["cName_prev"].ToString());
                    Logging.Comment("Статус После ID: " + dt.Rows[0]["id"].ToString() + "; Наименование: " + dt.Rows[0]["cName"].ToString());
                }



                Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                   + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
                Logging.StopFirstLevel();

            }
             getData();
        }

        private void frmMain_SizeChanged(object sender, EventArgs e)
        {
            //this.cDescription.Width = 200;
        }

        private void chbKD_Click(object sender, EventArgs e)
        {
            isKD();
            getData();

            if (chbKD.Checked)
            {
                rbWork.Checked = true;
                rbWork_Click(sender, e);
            }
        }

        private void dgvMain_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            //Рисуем рамку для выделеной строки
            if (dgv.Rows[e.RowIndex].Selected)
            {
                int width = dgv.Width;
                Rectangle r = dgv.GetRowDisplayRectangle(e.RowIndex, false);
                Rectangle rect = new Rectangle(r.X, r.Y, width - 1, r.Height - 1);

                ControlPaint.DrawBorder(e.Graphics, rect,
                    SystemColors.Highlight, 4, ButtonBorderStyle.Solid,
                    SystemColors.Highlight, 4, ButtonBorderStyle.Solid,
                    SystemColors.Highlight, 4, ButtonBorderStyle.Solid,
                    SystemColors.Highlight, 4, ButtonBorderStyle.Solid);
            }
        }

        private void dtpStart_CloseUp(object sender, EventArgs e)
        {
            getData();
        }

        private void dtpEnd_CloseUp(object sender, EventArgs e)
        {
            getData();
        }

        private void dtpStart_Leave(object sender, EventArgs e)
        {
            getData();
        }

        private void dtpEnd_Leave(object sender, EventArgs e)
        {
            getData();
        }

        private void btnPrintFond_Click(object sender, EventArgs e)
        {
            
            int id = int.Parse(dtMain.DefaultView[dgvMain.CurrentRow.Index]["id"].ToString());
            DataTable dtTmp = Config.hCntMain.getServiceRecordsBody(id);

            if (dtTmp==null && dtTmp.Rows.Count == 0)
            {
                return;
            }
            int? idFond = dtTmp.Rows[0]["id_ServiceRecordsFond"] == DBNull.Value ? null : (int?)dtTmp.Rows[0]["id_ServiceRecordsFond"];
            DataTable dtDatReport = Config.hCntMain.getDataReportFond(idFond != null ? (int)idFond : id);
            if (dtDatReport == null || dtDatReport.Rows.Count == 0) { MessageBox.Show("Нет Данных для отчёта", "Выгрузка отчёта", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }


            Nwuram.Framework.ToExcelNew.ExcelUnLoad report = new Nwuram.Framework.ToExcelNew.ExcelUnLoad();
            int indexRow = 1;
            int maxColumn = 6;

            report.Merge(indexRow, 1, indexRow, maxColumn);
            report.AddSingleValue("Содержание фонда", indexRow, 1);
            report.SetCellAlignmentToCenter(indexRow, 1, indexRow, 1);
            report.SetFontBold(indexRow, 1, indexRow, 1);
            report.SetFontSize(indexRow, 1, indexRow, 1, 16);
            indexRow++;

            EnumerableRowCollection<DataRow> rowCollect = dtDatReport.AsEnumerable()
                .Where(r => r.Field<int>("g") == 1);

            #region "Header"
            report.Merge(indexRow, 1, indexRow, 3);
            report.AddSingleValue($"№ СЗ: {rowCollect.First()["Number"]}", indexRow, 1);
            report.Merge(indexRow, 4, indexRow, 6);
            report.AddSingleValue($"Выгрузил: {UserSettings.User.FullUsername}", indexRow, 4);
            indexRow++;

            report.Merge(indexRow, 1, indexRow, 3);
            report.AddSingleValue($"Дата подтверждения: {rowCollect.First()["DateConfirmationD"]}", indexRow, 1);
            report.Merge(indexRow, 4, indexRow, 6);
            report.AddSingleValue($"Дата выгрузки: {DateTime.Now.ToShortDateString()}", indexRow, 4);
            indexRow++;

            report.Merge(indexRow, 1, indexRow, maxColumn);
            report.AddSingleValue($"Сумма: {rowCollect.First()["summaWithValute"]}", indexRow, 1);
            indexRow++;


            object _Number = rowCollect.First()["Number"];
            object _Summa = rowCollect.First()["Summa"];
            object _DateConfirmationD = rowCollect.First()["DateConfirmationD"];

            //report.Merge(indexRow, 1, indexRow, maxColumn);
            //report.AddSingleValue($"Основание: Фонд №{rowCollect.First()["Number"]} в размере {rowCollect.First()["Summa"]} от {rowCollect.First()["DateConfirmationD"]}", indexRow, 1);
            //indexRow++;

            report.Merge(indexRow, 1, indexRow, maxColumn);
            report.AddSingleValue($"Описание: {rowCollect.First()["Description"]}", indexRow, 1);
            indexRow++;
            indexRow++;
            #endregion

            #region "body"
            rowCollect = dtDatReport.AsEnumerable()
               .Where(r => r.Field<int>("g") == 3);

            report.AddSingleValue("№", indexRow, 1);
            report.AddSingleValue("Дата создания", indexRow, 2);
            report.AddSingleValue("Сумма", indexRow, 3);
            report.AddSingleValue("Автор", indexRow, 4);
            report.AddSingleValue("Дата подтв.", indexRow, 5);
            report.AddSingleValue("Описание", indexRow, 6);
            report.SetBorders(indexRow, 1, indexRow, maxColumn);
            report.SetCellAlignmentToCenter(indexRow, 1, indexRow, maxColumn);
            report.SetFontBold(indexRow, 1, indexRow, maxColumn);
            indexRow++;

            foreach (DataRow row in rowCollect)
            {
                report.AddSingleValue($"{row["Number"]} {((int)row["id_Status"] == 21 ? "(Аннул.)" : "")}", indexRow, 1);
                report.AddSingleValue($"{row["CreateServiceRecord"]}", indexRow, 2);
                report.AddSingleValue($"{row["Summa"]}", indexRow, 3);
                report.AddSingleValue($"{row["FIO"]}", indexRow, 4);
                report.AddSingleValue($"{row["DateConfirmationD"]}", indexRow, 5);
                report.AddSingleValue($"{row["Description"]}", indexRow, 6);
                report.SetBorders(indexRow, 1, indexRow, maxColumn);
                report.SetCellAlignmentToCenter(indexRow, 1, indexRow, maxColumn);
                indexRow++;
            }
            indexRow++;
            indexRow++;
            #endregion

            #region "Dop"
            rowCollect = dtDatReport.AsEnumerable()
              .Where(r => r.Field<int>("g") == 2);

            if (rowCollect.Count() > 0)
            {
                int nppDopFond = 1;
                foreach (DataRow row in rowCollect)
                {
                    report.Merge(indexRow, 1, indexRow, maxColumn);
                    report.AddSingleValue($"Дополнительный фонд №{nppDopFond}", indexRow, 1);
                    report.SetCellAlignmentToLeft(indexRow, 1, indexRow, 1);
                    report.SetFontBold(indexRow, 1, indexRow, 1);
                    report.SetFontSize(indexRow, 1, indexRow, 1, 14);
                    indexRow++;

                    report.Merge(indexRow, 1, indexRow, 6);
                    report.AddSingleValue($"№ СЗ: {row["Number"]}", indexRow, 1);
                    indexRow++;

                    report.Merge(indexRow, 1, indexRow, 6);
                    report.AddSingleValue($"Дата подтверждения: {row["DateConfirmationD"]}", indexRow, 1);
                    indexRow++;

                    report.Merge(indexRow, 1, indexRow, maxColumn);
                    report.AddSingleValue($"Сумма: {row["summaWithValute"]}", indexRow, 1);
                    indexRow++;

                    report.Merge(indexRow, 1, indexRow, maxColumn);
                    report.AddSingleValue($"Основание: Фонд №{_Number} в размере {_Summa} от {_DateConfirmationD}", indexRow, 1);
                    indexRow++;

                    report.Merge(indexRow, 1, indexRow, maxColumn);
                    report.AddSingleValue($"Описание: {row["Description"]}", indexRow, 1);
                    indexRow++;
                    indexRow++;
                    nppDopFond++;
                }
            }
            indexRow++;
            #endregion

            //Result
            object tmpSum;
            decimal sumFond;
            decimal sumSZ;
            try
            {
                tmpSum = dtDatReport.Compute("SUM(Summa)", "g in (1,2) and id_Status <> 21");
                decimal.TryParse(tmpSum.ToString(), out sumFond);
            }
            catch
            {
                sumFond = 0;
            }

            try
            {
                tmpSum = dtDatReport.Compute("SUM(Summa)", "g  = 3  and id_Status <> 21");
                decimal.TryParse(tmpSum.ToString(), out sumSZ);
            }
            catch
            {
                sumSZ = 0;
            }


            report.Merge(indexRow, 1, indexRow, 4);
            report.Merge(indexRow, 5, indexRow, 6);
            report.AddSingleValue($"Итого выделено по фонду:", indexRow, 1);
            report.AddSingleValue($"{sumFond.ToString("0.00")}", indexRow, 5);
            report.SetCellAlignmentToRight(indexRow, 1, indexRow, maxColumn);
            report.SetFontBold(indexRow, 1, indexRow, maxColumn);
            indexRow++;

            report.Merge(indexRow, 1, indexRow, 4);
            report.Merge(indexRow, 5, indexRow, 6);
            report.AddSingleValue($"Итого выделено по СЗ:", indexRow, 1);
            report.AddSingleValue($"{sumSZ.ToString("0.00")}", indexRow, 5);
            report.SetCellAlignmentToRight(indexRow, 1, indexRow, maxColumn);
            report.SetFontBold(indexRow, 1, indexRow, maxColumn);
            indexRow++;


            report.Merge(indexRow, 1, indexRow, 4);
            report.Merge(indexRow, 5, indexRow, 6);
            report.AddSingleValue($"Итого осталось:", indexRow, 1);
            report.AddSingleValue($"{(sumFond - sumSZ).ToString("0.00")}", indexRow, 5);
            report.SetCellAlignmentToRight(indexRow, 1, indexRow, maxColumn);
            report.SetFontBold(indexRow, 1, indexRow, maxColumn);
            indexRow++;


            report.SetColumnAutoSize(1, 1, indexRow, maxColumn);
            report.SetPageSetup(1, 999, true);
            report.Show();
        }

        private void timUpdate_Tick(object sender, EventArgs e)
        {
            //Console.WriteLine("Тик-Так");
           getData();
        }
        DataGridViewColumn sortCol;
        ListSortDirection direction;

        private void dgvMain_Sorted(object sender, EventArgs e)
        {
            sortCol = dgvMain.SortedColumn;
            direction = (dgvMain.SortOrder == SortOrder.Ascending ? ListSortDirection.Ascending : ListSortDirection.Descending);
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            string strLog = "";
            bool startScan = true, isOk = false; ;
            for (int i = 0; i < dgvMain.Rows.Count; i++)
            {
              if (dgvMain.Rows[i].Cells["Enter"].Value == null) return;

              if ((bool)dgvMain.Rows[i].Cells["Enter"].Value == true)
                {
                  int id = (int)dtMain.DefaultView[dgvMain.Rows[i].Index]["id"];
                  frmServiceNote frmS = new frmServiceNote();
                  frmS.id = id;
                  frmS.frmServiceNote_Load(sender, e);
                  frmS.setIsView();

                    if (startScan)
                    {
                        frmS.btAccept_Click(sender, e);
                        isOk = frmS.isScan;
                        strLog += (strLog.Length < 1 ? "id: " : ", id: ") + dgvMain.Rows[i].Cells["id"].Value.ToString();
                    }
                    if (isOk && startScan == false)
                        frmS.ChangeStatusAccept();

               startScan = false;
                    Logging.StartFirstLevel(1253);
                    Logging.Comment("Подтверждены СЗ " + strLog);
                    Logging.StopFirstLevel();
                }
            }
            getData();
                 
         }

        private void btnRefuse_Click(object sender, EventArgs e)
        {
            string strLog = "";
            bool startScan = true, isOk = false;
            for (int i = 0; i < dgvMain.Rows.Count; i++)
            {
                bool isChecked = (bool)dgvMain.Rows[i].Cells["Enter"].Value;
                if (isChecked)
                {
                    int id = (int)dtMain.DefaultView[dgvMain.Rows[i].Index]["id"];
                    frmServiceNote frmS = new frmServiceNote();
                    frmS.id = id;
                    frmS.frmServiceNote_Load(sender, e);
                    frmS.setIsView();

                    if (startScan)
                    {
                        frmS.btRefuse_Click(sender, e);
                        isOk = frmS.isScan;
                        strLog += (strLog.Length < 1 ? "id: " : ", id: ") + dgvMain.Rows[i].Cells["id"].Value.ToString();
                    }
                    if (isOk && startScan == false)
                        frmS.ChangeStatusRefuse();

                    startScan = false;

                }
            }
            Logging.StartFirstLevel(1256);
            Logging.Comment("Отклонены СЗ " + strLog);
            Logging.StopFirstLevel();

            getData();
        }

        private void cmbObjects_SelectionChangeCommitted(object sender, EventArgs e)
        {
            setFilter();
        }
        
    }
}
