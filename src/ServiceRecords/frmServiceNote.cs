﻿using Nwuram.Framework.Logging;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Nwuram.Framework.Settings.User;

namespace ServiceRecords
{
    public partial class frmServiceNote : Form
    {
        public bool isScan = false;

        private int id_ServiceRecords = 0;
        private bool isCreate = true;
        private bool isView = false;
        private int id_status = -1;
        private int TypeServiceRecord = -1;
        private int TypeServiceRecordOnTime = 1;
        private bool isEdit = false;
        private bool isDopFond = false;
        private DataTable dtTmpData;
        private decimal Allsumma;
        private string resultScaner = "";        
        private int? idFond = null;
        private readonly string NameFileFinger = @"C:\AppAuth\ServiceRecords\fingers.txt";
        private readonly string NameFileResult = @"C:\AppAuth\ServiceRecords\result.txt";
        private readonly string NameProgramm = @"C:\AppAuth\ServiceRecords\Demo.exe";
        private DataTable dtListDeps;

        public void setIsView()
        {
            this.isView = true;
        }

        public int id { set; private get; }

        public frmServiceNote()
        {
            InitializeComponent();
            dgvNote.AutoGenerateColumns = false;
            ToolTip tt = new ToolTip();
            tt.SetToolTip(btClose, "Выход");
            tt.SetToolTip(btSelect, "Сохранить");
            tt.SetToolTip(btnSaveComment, "Добавить комментарий");
            tt.SetToolTip(btAddFond, "Выбрать фонд");
            tt.SetToolTip(btDelFond, "Удалить фонд");
            tt.SetToolTip(btFondPrintSZ, "Сформировать отчёт по фонду");
            tt.SetToolTip(btFondViewSZ, "Посмотреть прикреплённую СЗ");
            dgvFond.AutoGenerateColumns = false;

            chbMoreNote.Visible = false;
            dgvNote.Visible = false;
            btAddBlock.Visible = btEditBlock.Visible = btDelBlock.Visible = false;
        }

        public void frmServiceNote_Load(object sender, EventArgs e)
        {
            dtStatus = Config.hCntMain.getStatus();
            Config.bufferDataTable = null;
            dtpNextDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            getBlock();
            getDeps();
            //createBlock();
            createObjects();
            //createDeps();
            createValuta();
            getMultipleReceivingMone();
            getTypicalWorks();
            dtListDeps = Config.hCntMain.GetSettingsTable("otna");
            cmbDeps_SelectionChangeCommitted(null, null);

            if (id != -1)
            {
                DataTable dtTmp = Config.hCntMain.getServiceRecordsBody(id);
                dtTmpData = dtTmp.Copy();

                if (dtTmp == null || dtTmp.Rows.Count == 0)
                {
                    return;
                }
                tbSummaCash.Text = dtTmp.Rows[0]["SummaCash"].ToString();
                tbSummaNonCash.Text = dtTmp.Rows[0]["SummaNonCash"].ToString();
                tbCommentNote.Text = dtTmp.Rows[0]["Description"].ToString();
                dtpDateNote.Value = (DateTime)dtTmp.Rows[0]["CreateServiceRecord"];

                if ((int)dtTmp.Rows[0]["TypeServiceRecord"] == 0) rbStandart.Checked = true; else rbTemplate.Checked = true;

                cmbBlock.SelectedValue = (int)dtTmp.Rows[0]["id_Block"];
                createDeps();
                cmbDeps.SelectedValue = (int)dtTmp.Rows[0]["id_Department"];


                tbSumma.Text = decimal.Parse(dtTmp.Rows[0]["Summa"].ToString()).ToString("0.00");


                if ((bool)dtTmp.Rows[0]["bCashNonCash"])
                    rbCard.Checked = true;
                else if ((bool)dtTmp.Rows[0]["Mix"])
                    rbMix.Checked = true;
                else rbMoney.Checked = true;

                if (dtTmp.Rows[0]["DataSumma"] == DBNull.Value && dtTmp.Rows[0]["bDataSumma"] != DBNull.Value && (bool)dtTmp.Rows[0]["bDataSumma"])
                {
                    chbMoreNote.Checked = true;
                }
                else if (dtTmp.Rows[0]["bDataSumma"] != DBNull.Value && !(bool)dtTmp.Rows[0]["bDataSumma"] && dtTmp.Rows[0]["DataSumma"] != DBNull.Value)
                {
                    chbSingleNote.Checked = true;
                    dtpSingleNote.Value = (DateTime)dtTmp.Rows[0]["DataSumma"];
                }

                dtpNextDate.Value = (DateTime)dtTmp.Rows[0]["MonthB"];

                tbNumberNote.Text = dtTmp.Rows[0]["Number"].ToString();
                tbCommentGlobal.Text = dtTmp.Rows[0]["Comments"].ToString().Replace(@"\r", "\r\n");

                btView.Visible = true;
                btAddFile.Visible = true;

                id_status = (int)dtTmp.Rows[0]["id_Status"];
                TypeServiceRecord = (int)dtTmp.Rows[0]["TypeServiceRecord"];
                TypeServiceRecordOnTime = (int)dtTmp.Rows[0]["TypeServiceRecordOnTime"];
                rbOneTime.Checked = TypeServiceRecordOnTime == 1 ? true : false;
                rbMonthly.Checked = TypeServiceRecordOnTime == 2 ? true : false;
                rbFond.Checked = TypeServiceRecordOnTime == 3 ? true : false;

                idFond = dtTmp.Rows[0]["id_ServiceRecordsFond"] == DBNull.Value ? null : (int?)dtTmp.Rows[0]["id_ServiceRecordsFond"];

                if (idFond != null)
                {
                    DataTable dtTmpFond = Config.hCntMain.getFondInfo(idFond, id);
                    if (dtTmpFond != null && dtTmpFond.Rows.Count > 0)
                    {
                        tbFond.Text = $"№{dtTmpFond.Rows[0]["Number"].ToString()} на {dtTmpFond.Rows[0]["sumString"].ToString()} от {((DateTime)dtTmpFond.Rows[0]["DateConfirmationD"]).ToShortDateString()}";
                    }

                    cmbValuta.SelectedValue = "RUB";
                    cmbValuta.Enabled = false;
                    // rbMonthly.Enabled = false;
                }
                //доработка от Люды
                if (TypeServiceRecordOnTime == 3 || TypeServiceRecordOnTime == 1)
                {
                    //idFond== null ? id: idFond
                    DataTable dtTmpFond = Config.hCntMain.getFondInfo(idFond == null ? id : idFond, id);
                    if (dtTmpFond != null && dtTmpFond.Rows.Count > 0)
                    {
                        isDopFond = (bool)dtTmpFond.Rows[0]["isDopFond"];
                        sumDopFond = (decimal)dtTmpFond.Rows[0]["Summa"];
                        fullSumm = (decimal)dtTmpFond.Rows[0]["fullSumm"];
                        btDelFond.Visible =
                            btAddFond.Visible =
                            //tbFond.Visible =
                            //lFond.Visible = 
                            !isDopFond;
                        gbFond.Visible = isDopFond;
                    }

                    btAddFond.Visible = btDelFond.Visible = new List<int> { 1, 2, 3, 4, 6, 9, 12 }.Contains((int)dtTmp.Rows[0]["id_status"]);

                    DataTable dtListRecordFond = Config.hCntMain.getListRecordFond(TypeServiceRecordOnTime == 3 ? id : (idFond == null ? id : (int) idFond));
                    if (dtListRecordFond != null && dtListRecordFond.Rows.Count > 0)
                    {
                        dgvFond.DataSource = dtListRecordFond;
                        reSummFond();
                        //Object objSumFond = dtListRecordFond.Compute("SUM(Summa)", "TypeServiceRecordOnTime <> 3");
                        //decimal resFond;
                        //if (decimal.TryParse(objSumFond.ToString(), out resFond))
                        //{
                        //    tbSumSZFond.Text = resFond.ToString("0.00");
                        //    tbSumFond.Text = (decimal.Parse(tbSumma.Text) + sumDopFond).ToString("0.00");
                        //    tbFondResult.Text = (decimal.Parse(tbSumma.Text) + sumDopFond - resFond).ToString("0.00");
                        //}
                        //else
                        //{
                        //    tbSumSZFond.Text = "0.00";
                        //    tbSumFond.Text = (decimal.Parse(tbSumma.Text) + sumDopFond).ToString("0.00");
                        //    tbFondResult.Text = (decimal.Parse(tbSumma.Text) + sumDopFond).ToString("0.00");
                        //}
                        typeSRonTime.Enabled = false;
                    }
                    if (TypeServiceRecordOnTime == 1)
                    {
                       
                        if (!isView)
                            gbFond.Visible = false;
                    }
                    tbFond.Visible =
                           lFond.Visible = true;

                }
                else
                {
                    btAddFond.Visible = btDelFond.Visible = new List<int> { 1, 2, 3, 4, 6, 9, 12 }.Contains((int)dtTmp.Rows[0]["id_status"]);
                }






                createObjects();
                if (dtTmp.Rows[0]["id_Object"].ToString().Equals(""))
                    cmbObjects.SelectedValue = 1;
                else
                    cmbObjects.SelectedValue = (int)dtTmp.Rows[0]["id_Object"];

                cmbValuta.SelectedValue = dtTmp.Rows[0]["Valuta"].ToString();

                if (Config.CodeUser.Equals("КД"))
                {
                    tbComment.Enabled = btnSaveComment.Enabled = true;
                    tbCommentNote.Text = tbCommentNote.Text.Contains("Разовая.") ? tbCommentNote.Text.Replace("Разовая.", "") : tbCommentNote.Text;
                }


                List<int> allowedStatus = new List<int> { 1, 2, 3, 4, 6, 8, 9, 10, 12 };

                if (allowedStatus.Contains(id_status) || (Config.CodeUser.Equals("КНТ") && id_status == 2))
                {

                }
                else
                {
                    tbSumma.ReadOnly = tbSummaCash.ReadOnly = tbSummaNonCash.ReadOnly = true;
                    tbCommentNote.ReadOnly = true;
                    dtpSingleNote.Enabled = false;
                    chbSingleNote.Enabled = false;
                    chbMoreNote.Enabled = false;
                    grbTypeOplata.Enabled = false;
                    dtpNextDate.Enabled = false;
                    cmbBlock.Enabled = false;
                    cmbDeps.Enabled = false;
                    tbCommentGlobal.ReadOnly = true;
                    TypeSR.Enabled = typeSRonTime.Enabled = cmbValuta.Enabled = false;
                }

                if ((id_status == 4 || id_status == 10) && Config.CodeUser.Equals("КД"))
                {
                    btAccept.Visible = btRefuse.Visible = isView;
                    tbSumma.ReadOnly = tbSummaCash.ReadOnly = tbSummaNonCash.ReadOnly = false;
                    tbCommentNote.ReadOnly = false;
                    TypeSR.Enabled = typeSRonTime.Enabled = cmbObjects.Enabled =
                        cmbBlock.Enabled = cmbDeps.Enabled = dtpDateNote.Enabled =
                        dtpSingleNote.Enabled = btAddBlock.Enabled = tbCommentGlobal.Enabled =
                        btEditBlock.Enabled = btDelBlock.Enabled = dtpNextDate.Enabled =
                        chbSingleNote.Enabled = chbMoreNote.Enabled = grbTypeOplata.Enabled = cmbValuta.Enabled = false;
                }

                if (dtTmp.Rows[0]["PreviosConfirmationD"] != DBNull.Value)
                {
                    lTPreviosConfirmationD.Visible = lPreviosConfirmationD.Visible = picBoxPreviosConfirmationD.Visible = true;
                }

                if (dtTmp.Rows[0]["ConfirmationD"] != DBNull.Value)
                {
                    lTConfirmationD.Visible = lConfirmationD.Visible = picBoxConfirmationD.Visible = true;
                }
                if (isView) initIsView();

                if (cmbObjects.Enabled = cmbDeps.Enabled)
                {
                    if (dtTmp.Rows[0]["id_Object"].ToString().Equals(""))
                    {
                        dtObjects.DefaultView.RowFilter = "is_Active = 1";
                        cmbObjects.SelectedValue = 1;
                    }
                    else
                    {
                        dtObjects.DefaultView.RowFilter = "is_Active = 1 or id_Object = " + (int)dtTmp.Rows[0]["id_Object"];
                        cmbObjects.SelectedValue = (int)dtTmp.Rows[0]["id_Object"];
                    }
                }

                getMultipleReceivingMone();

                cmbTypicalWorks.SelectedValue = dtTmp.Rows[0]["inType"];

                cmbTypicalWorks.Enabled = 
                    (new List<string>(new string[] { "ОП", "РКВ" }).Contains(Config.CodeUser) && new List<int>(new int[] { 1 }).Contains((int)dtTmp.Rows[0]["id_Status"])) ||
                    (new List<string>(new string[] { "КНТ" }).Contains(Config.CodeUser) && new List<int>(new int[] { 2, 8 }).Contains((int)dtTmp.Rows[0]["id_Status"]));


                isCreate = false;
            }
            //else tbComment.Enabled = btnSaveComment.Enabled = false;
            isEdit = false;

            if (UserSettings.User.StatusCode.ToLower() == "кд")
            {
                btAddFond.Enabled = btDelFond.Enabled = false;
            }
        }

        private void initIsView()
        {
            TypeSR.Enabled = false;
            groupBox2.Enabled = false;
            groupBox3.Enabled = false;
            tbCommentGlobal.ReadOnly = true;
            //btAddFile.Visible = false;
            btSelect.Visible = false;
            rbMonthly.Enabled = rbOneTime.Enabled = TypeSR.Enabled = rbFond.Enabled = false;
            cmbValuta.Enabled = false;
        }
        DataTable dtBlock, dtDeps, dtObjects, userDepartmentName, idDepartament, idBlock, userStatus;
        public void createValuta()
        {
            DataTable dtValuta = new DataTable();
            dtValuta.Columns.Add("Valuta", typeof(string));
            dtValuta.Rows.Add("RUB");
            dtValuta.Rows.Add("EUR");
            dtValuta.Rows.Add("USD");
            cmbValuta.DataSource = dtValuta;
            cmbValuta.ValueMember = "Valuta";
        }

        private void createObjects()
        {
            dtObjects = Config.hCntMain.getObjects();
            dtObjects.Rows.Add(0, "Не выбрано", 1);
            cmbObjects.DataSource = dtObjects;
            dtObjects.DefaultView.Sort = "id_Object ASC";
            cmbObjects.DisplayMember = "name_Object";
            cmbObjects.ValueMember = "id_Object";

            string filter = "" + "CONVERT(is_Active, 'System.Int32')  = 1";
            dtObjects.DefaultView.RowFilter = filter;

        }
        private void createBlock()
        {
            dtBlock = Config.hCntMain.getBlock(-1);
            cmbBlock.DataSource = dtBlock;
            cmbBlock.DisplayMember = "name";
            cmbBlock.ValueMember = "id_Block";

        }
        private void createDeps()
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
            cmbDeps_SelectionChangeCommitted(null, null);
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
                    //if (userDepartmentName.Rows[0][0].ToString().StartsWith("Блок") || userDepartmentName.Rows[0][0].ToString().StartsWith("Отдел 1"))
                    if (Config.hCntMain.checkBlock(int.Parse(idDepartament.Rows[0][0].ToString())).Rows.Count > 0)
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
                        var der = idDepartament.Rows[0][0];
                        int e = (int)der;
                        idBlock = Config.hCntMain.getUsersBlockId(e);
                        int i = 0;
                        while ((i < dtBlock.Rows.Count))
                        {
                            if (dtBlock.Rows[i][0].ToString().Equals(idBlock.Rows[0][0].ToString()) == true)
                                break;
                            i++;
                        }
                        cmbBlock.SelectedIndex = i;
                        this.cmbBlock.Enabled = false;
                    }

                }
                else createBlock();
            }
            catch (Exception e) { MessageBox.Show("Ошибка"); }

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

                    // if (!(userDepartmentName.Rows[0][0].ToString().StartsWith("Блок") || userDepartmentName.Rows[0][0].ToString().StartsWith("Отдел 1")))
                    if (Config.hCntMain.checkBlock(int.Parse(idDepartament.Rows[0][0].ToString())).Rows.Count == 0)
                        findDepartment();
                    else createDeps();
                }
                else createDeps();
            }
            catch (Exception e) { Process.GetCurrentProcess().Kill(); }
        }

        private void getTypicalWorks()
        {
            DataTable dtTypicalWorks = Config.hCntMain.getTypicalWorks(false);
            cmbTypicalWorks.DataSource = dtTypicalWorks;
            cmbTypicalWorks.DisplayMember = "cName";
            cmbTypicalWorks.ValueMember = "id";
            cmbTypicalWorks.SelectedIndex = -1;
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
        //DataTable dtBlock, dtDeps;
        //private void getBlock()
        //{
        //  dtBlock = Config.hCntMain.getBlock(-1);
        //  cmbBlock.DataSource = dtBlock;
        //  cmbBlock.DisplayMember = "name";
        //  cmbBlock.ValueMember = "id_Block";

        //}

        //private void getDeps()
        //{
        //  if (cmbBlock.SelectedValue == null)
        //  {
        //    cmbDeps.DataSource = null;
        //    return;
        //  }

        //  int id_block = (int)cmbBlock.SelectedValue;

        //  dtDeps = Config.hCntMain.getBlock(id_block);

        //  cmbDeps.DataSource = dtDeps;
        //  cmbDeps.DisplayMember = "name";
        //  cmbDeps.ValueMember = "id_Department";
        //}




        private void tbSumma_KeyPress(object sender, KeyPressEventArgs e)
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

        private void tbSumma_Leave(object sender, EventArgs e)
        {
            if (tbSumma.Text.ToString().Length == 0)
                tbSumma.Text = "0,00";
            else
                tbSumma.Text = decimal.Parse(tbSumma.Text.ToString()).ToString("######0.00");

            isEdit = true;
            reSummFond();
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btSelect_Click(object sender, EventArgs e)
        {
            #region "проверки"

            if (decimal.Parse(tbSumma.Text.ToString()) == 0)
            {
                MessageBox.Show("Необходимо ввести сумму!", "Информирование", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }



            DataTable dtTmpFond;

            if (TypeServiceRecordOnTime == 3)
            {
                dtTmpFond = Config.hCntMain.validateFondSumma(id, null);

                if (dtTmpFond != null && dtTmpFond.Rows.Count > 0)
                {
                    decimal resultSum = (decimal)dtTmpFond.Rows[0]["Summa"];
                    decimal _sumSZ = decimal.Parse(tbSumma.Text);

                    if (_sumSZ + resultSum + sumDopFond < 0)
                    {
                        MessageBox.Show(Config.centralText("Сумма сохраняемой СЗ превышает\nразмер фонда с учётом СЗ,\nсозданных на основе фонда.\nСохранение невозможно.\n"), "Сохранить СЗ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

            }
            else if (idFond != null)
            {
                dtTmpFond = Config.hCntMain.validateFondSumma(idFond, id);

                if (dtTmpFond != null && dtTmpFond.Rows.Count > 0)
                {
                    decimal resultSum = (decimal)dtTmpFond.Rows[0]["Summa"];
                    decimal _sumSZ = decimal.Parse(tbSumma.Text);

                    if (resultSum - _sumSZ < 0)
                    {
                        MessageBox.Show(Config.centralText("Сумма сохраняемой СЗ превышает\nразмер фонда с учётом СЗ,\nсозданных на основе фонда.\nСохранение невозможно.\n"), "Сохранить СЗ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }


            if (isCreate)
            {
                if (rbCard.Checked || rbMix.Checked)
                {
                    if (Config.bufferDataTable != null && Config.bufferDataTable.Rows.Count > 0)
                    {
                        DataRow[] row = Config.bufferDataTable.Select("TypeScan = 2");
                        if (row.Count() == 0)
                        {
                            MessageBox.Show("При выборе типа оплаты \"Безнал\"требуется\nприкрепить к СЗ основополагающий\nдокумент для оплаты  по безналичному расчёту.", "Информирование", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("При выборе типа оплаты \"Безнал\"требуется\nприкрепить к СЗ основополагающий\nдокумент для оплаты  по безналичному расчёту.", "Информирование", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                if (cmbObjects.SelectedValue.ToString().Equals("0"))
                {
                    MessageBox.Show("Необходимо выбрать объект", "Информирование", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if ((rbMix.Checked) && (double.Parse(tbSumma.Text) != double.Parse(tbSummaCash.Text) + double.Parse(tbSummaNonCash.Text)))
                {
                    MessageBox.Show(Config.centralText("Несовпадение суммы нал. и безнал.\nс главной суммой \n"), "Информирование", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }               
            }
            else
            {
                if (rbCard.Checked || rbMix.Checked)
                {
                    DataTable dtScan = Config.hCntMain.getScan(id, -1);
                    if (dtScan != null && dtScan.Rows.Count > 0)
                    {
                        DataRow[] row = dtScan.Select("TypeScan = 2");
                        if (row.Count() == 0)
                        {
                            MessageBox.Show("При выборе типа оплаты \"Безнал\"требуется\nприкрепить к СЗ основополагающий\nдокумент для оплаты  по безналичному расчёту.", "Информирование", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("При выборе типа оплаты \"Безнал\"требуется\nприкрепить к СЗ основополагающий\nдокумент для оплаты  по безналичному расчёту.", "Информирование", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                if (cmbObjects.SelectedValue.ToString().Equals("0"))
                {
                    MessageBox.Show("Необходимо выбрать объект", "Информирование", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if ((rbMix.Checked) && (double.Parse(tbSumma.Text) != double.Parse(tbSummaCash.Text) + double.Parse(tbSummaNonCash.Text)))
                {
                    MessageBox.Show(Config.centralText("Несовпадение суммы нал. и безнал.\nс главной суммой \n"), "Информирование", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (cmbTypicalWorks.Visible && cmbTypicalWorks.SelectedIndex == -1)
            {
                MessageBox.Show($"Необходимо выбрать: \"{lTypicalWorks.Text}\"", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbTypicalWorks.Focus();
                return;
            }

            #endregion

            #region "Сохранение"


            string Description = tbCommentNote.Text.Trim();
            DateTime CreateServiceRecord = dtpDateNote.Value;
            int TypeServiceRecord = (rbStandart.Checked ? 0 : 1);

            int id_Block = (int)cmbBlock.SelectedValue;
            int id_Department = (int)cmbDeps.SelectedValue;

            decimal Summa = decimal.Parse(tbSumma.Text.ToString());

            bool bCashNonCash = rbCard.Checked;
            bool mix = rbMix.Checked;
            object DataSumma = null;
            if (chbSingleNote.Checked)
                DataSumma = dtpSingleNote.Value;

            object bDataSumma = null;
            if (chbMoreNote.Checked) bDataSumma = true;
            if (chbSingleNote.Checked) bDataSumma = false;

            DateTime MonthB = dtpNextDate.Value;

            string Comments = tbCommentGlobal.Text.Trim() + "\r" + "\n";
            foreach (DataRow r in dtMultipleReceivingMone.Rows)
            {
                Allsumma += decimal.Parse(r["Summa"].ToString());
            }
            if (Allsumma > decimal.Parse(tbSumma.Text.Trim()))
            {
                MessageBox.Show("Сумма выплат больше суммы ДС!", "Информирование", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Allsumma = 0;
                return;
            }

            if (!(Description.Contains("Разовая") || Description.Contains("Ежемесячная") || Description.Contains("Фонд") || Description.Contains("Доп.Фонд")))
            {
                //Description = (rbOneTime.Checked ? "Разовая. " : (rbMonthly.Checked ? "Ежемесячная. " : "Фонд. ")) + Description;

                if (rbOneTime.Checked)
                {
                    Description = (idFond == null ? "Разовая.  " : "Разовая из фонда.  ") + Description;
                }
                else
            if (rbMonthly.Checked)
                {
                    Description = (idFond == null ? "Ежемесячная. " : "Ежемесячная из фонда.  ") + Description;
                }
                else
                    if (rbFond.Checked)
                {
                    Description = (idFond == null ? "Фонд.  " : "Доп.Фонд.  ") + Description;
                }
            }

            int? inType = null;
            if (cmbTypicalWorks.SelectedIndex != -1 && cmbTypicalWorks.Visible)
                inType = (int)cmbTypicalWorks.SelectedValue;


            if (isCreate && Allsumma <= decimal.Parse(tbSumma.Text.Trim()))
            {
                DataTable dtTmp = Config.hCntMain.setServiceRecords(Description,
                    CreateServiceRecord,
                    TypeServiceRecord,
                    id_Block,
                    id_Department,
                    Summa,
                    bCashNonCash,
                    DataSumma,
                    bDataSumma,
                    MonthB,
                    Comments,
                    (int)cmbObjects.SelectedValue,
                    TypeServiceRecordOnTime,
                    cmbValuta.SelectedValue.ToString(),
                    decimal.Parse(tbSummaCash.Text),
                    decimal.Parse(tbSummaNonCash.Text),
                    mix,
                    idFond,
                    inType);

                id_ServiceRecords = int.Parse(dtTmp.Rows[0]["id"].ToString());
                if (Config.bufferDataTable != null && Config.bufferDataTable.Rows.Count > 0)
                    foreach (DataRow row in Config.bufferDataTable.Rows)
                    {
                        string fileName = row["cName"].ToString();
                        byte[] byteFile = (byte[])row["img"];
                        int TypeScan = (int)row["TypeScan"];
                        string @Extension = (string)row["Extension"];
                        Config.hCntMain.setScan(id_ServiceRecords, byteFile, fileName, TypeScan, @Extension);
                    }

                if (dtMultipleReceivingMone != null && dtMultipleReceivingMone.Rows.Count > 0)
                {
                    foreach (DataRow r in dtMultipleReceivingMone.Rows)
                    {
                        Config.hCntMain.setMultipleReceivingMoney(id_ServiceRecords, r["SubNumber"].ToString(), (decimal)r["Summa"], (DateTime)r["DataSumma"], -1, false);
                    }
                }
            }
            else
            {
                Config.hCntMain.updateServiceRecords(Description,
                                                    CreateServiceRecord,
                                                    TypeServiceRecord,
                                                    id_Block,
                                                    id_Department,
                                                    Summa,
                                                    bCashNonCash,
                                                    DataSumma,
                                                    bDataSumma,
                                                    MonthB,
                                                    Comments,
                                                    id,
                                                    (int)cmbObjects.SelectedValue,
                                                    TypeServiceRecordOnTime,
                                                    cmbValuta.SelectedValue.ToString(),
                                                    decimal.Parse(tbSummaCash.Text),
                                                    decimal.Parse(tbSummaNonCash.Text),
                                                    rbMix.Checked,
                                                    idFond,
                                                    inType);
            }

            #endregion

            MessageBox.Show("Данные сохранены!", "Информирование", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (isCreate)
            {
                Logging.StartFirstLevel(1262);//1250
                Logging.Comment("Id СЗ: " + id_ServiceRecords);
                Logging.Comment("Номер СЗ: " + tbNumberNote.Text);
                Logging.Comment("Тип СЗ: " + (rbStandart.Checked ? rbStandart.Text : rbTemplate.Text));
                Logging.Comment("Тип СЗ по времени: " + (rbOneTime.Checked ? rbOneTime.Text : rbMonthly.Text));
                Logging.Comment("Сумма:" + tbSumma.Text);
                Logging.Comment("Валюта:" + cmbValuta.Text);
                if (rbMix.Checked)
                {
                    Logging.Comment("Сумма нал:" + tbSummaCash.Text);
                    Logging.Comment("Сумма безнал:" + tbSummaNonCash.Text);
                }
                Logging.Comment("Описание, комментарий: " + tbCommentNote.Text);
                Logging.Comment("Оплата:" + (rbMix.Checked ? rbMix.Text : (rbMoney.Checked ? rbMoney.Text : rbCard.Text)));
                Logging.Comment("Объект ID: " + cmbObjects.SelectedValue.ToString() + "; Наименование:" + cmbObjects.Text);
                Logging.Comment("Дата создания СЗ: " + dtpDateNote.Value.ToShortDateString());
                Logging.Comment("Блок ID: " + cmbBlock.SelectedValue.ToString() + "; Наименование:" + cmbBlock.Text);
                Logging.Comment("Отдел ID: " + cmbDeps.SelectedValue.ToString() + "; Наименование:" + cmbDeps.Text);
                Logging.Comment("На рассмотрении РП. Прошу включить в Б: " + dtpNextDate.Value.ToShortDateString());
                if (chbSingleNote.Checked)
                {
                    Logging.Comment(chbSingleNote.Text + ": " + dtpSingleNote.Value.ToShortDateString());
                }
                if (chbMoreNote.Checked)
                {
                    Logging.Comment(chbMoreNote.Text);

                    foreach (DataRow r in dtMultipleReceivingMone.Rows)
                    {
                        Logging.Comment("Подномер:" + r["SubNumber"].ToString() + ";Сумма:" + r["Summa"].ToString() + ";Предполагаемая дата:" + r["DataSumma"].ToString());
                    }
                }

                Logging.Comment("Комментарий:" + tbCommentGlobal.Text);

                if (Config.bufferDataTable != null && Config.bufferDataTable.Rows.Count > 0)
                    foreach (DataRow row in Config.bufferDataTable.Rows)
                    {
                        string fileName = row["cName"].ToString();
                        int TypeScan = (int)row["TypeScan"];
                        Logging.Comment("Добавленный файл: Тип:" + (TypeScan == 1 ? "к описанию СЗ" : (TypeScan == 2 ? "при оплате безналом " : "отчет по тратам ДС по СЗ")) + ";Наименование: " + fileName);
                    }


                Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                    + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
                Logging.StopFirstLevel();
            }
            else
            {

                Logging.StartFirstLevel(1251);
                Logging.Comment("Id СЗ: " + id);
                Logging.Comment("Номер СЗ: " + dtTmpData.Rows[0]["Number"].ToString());
                Logging.VariableChange("Тип СЗ: ",
                       (rbStandart.Checked ? rbStandart.Text : rbTemplate.Text)
                    , ((int)dtTmpData.Rows[0]["TypeServiceRecord"] == 0 ? rbStandart.Text : rbTemplate.Text), typeLog._string);

                Logging.VariableChange("Тип СЗ по времени: ", (rbOneTime.Checked ? rbOneTime.Text : rbMonthly.Text), ((int)dtTmpData.Rows[0]["TypeServiceRecordOnTime"] == 1 ? rbOneTime.Text : rbMonthly.Text));

                Logging.VariableChange("Сумма:", tbSumma.Text, decimal.Parse(dtTmpData.Rows[0]["Summa"].ToString()).ToString("0.00"), typeLog._string);
                Logging.VariableChange("Валюта:", cmbValuta.Text, dtTmpData.Rows[0]["Valuta"].ToString());
                if (rbMix.Checked)
                {
                    Logging.VariableChange("Сумма нал:", tbSummaCash.Text, dtTmpData.Rows[0]["SummaCash"].ToString());
                    Logging.VariableChange("Сумма безнал:", tbSummaNonCash.Text, dtTmpData.Rows[0]["SummaNonCash"].ToString());
                }


                Logging.VariableChange("Описание, комментарий: ", tbCommentNote.Text, dtTmpData.Rows[0]["Description"].ToString());
                Logging.VariableChange("Оплата:"
                    , (rbMix.Checked ? rbMix.Text : (rbMoney.Checked ? rbMoney.Text : rbCard.Text))
                    , ((bool)dtTmpData.Rows[0]["Mix"] ? rbMix.Text : (!(bool)dtTmpData.Rows[0]["bCashNonCash"] ? rbMoney.Text : rbCard.Text)));

                Logging.VariableChange("Объект ID: ", cmbObjects.SelectedValue.ToString(), dtTmpData.Rows[0]["id_Object"].ToString());
                Logging.VariableChange("Наименование ID: ", cmbObjects.Text, dtTmpData.Rows[0]["name_Object"].ToString());


                Logging.VariableChange("Дата создания СЗ: ", dtpDateNote.Value.ToShortDateString(), ((DateTime)dtTmpData.Rows[0]["CreateServiceRecord"]).ToShortDateString());

                Logging.VariableChange("Блок ID: ", cmbBlock.SelectedValue.ToString(), ((int)dtTmpData.Rows[0]["id_Block"]).ToString(), typeLog._int);
                Logging.VariableChange("Блок Наименование:", cmbBlock.Text.Trim(), ((string)dtTmpData.Rows[0]["nameBlock"]).Trim());

                Logging.VariableChange("Отдел ID: ", cmbDeps.SelectedValue.ToString(), (int)dtTmpData.Rows[0]["id_Department"], typeLog._int);
                Logging.VariableChange("Отдел Наименование:", cmbDeps.Text.Trim(), ((string)dtTmpData.Rows[0]["nameDeps"]).Trim());

                Logging.VariableChange("На рассмотрении РП. Прошу включить в Б: ", dtpNextDate.Value.ToShortDateString(), ((DateTime)dtTmpData.Rows[0]["MonthB"]).ToShortDateString());


                if (dtTmpData.Rows[0]["bDataSumma"] != DBNull.Value && !(bool)dtTmpData.Rows[0]["bDataSumma"] && dtTmpData.Rows[0]["DataSumma"] != DBNull.Value && !chbSingleNote.Checked)
                {
                    Logging.Comment(chbSingleNote.Text + ": Отключен");
                }

                if (chbSingleNote.Checked)
                {
                    Logging.Comment(chbSingleNote.Text + ": Включен");
                    Logging.VariableChange(chbSingleNote.Text + ": ", dtpSingleNote.Value.ToShortDateString(), (dtTmpData.Rows[0]["DataSumma"] == DBNull.Value ? "" : ((DateTime)dtTmpData.Rows[0]["DataSumma"]).ToShortDateString()));
                }

                if (dtTmpData.Rows[0]["DataSumma"] == DBNull.Value && dtTmpData.Rows[0]["bDataSumma"] != DBNull.Value && (bool)dtTmpData.Rows[0]["bDataSumma"] && !chbMoreNote.Checked)
                {
                    Logging.Comment(chbMoreNote.Text + ": Отключен");
                }

                if (chbMoreNote.Checked)
                {
                    Logging.Comment(chbMoreNote.Text + ": Включен");

                    foreach (DataRow r in dtMultipleReceivingMone.Rows)
                    {
                        Logging.Comment("Подномер:" + r["SubNumber"].ToString() + ";Сумма:" + r["Summa"].ToString() + ";Предполагаемая дата:" + r["DataSumma"].ToString());
                    }
                }

                Logging.VariableChange("Комментарий:", tbCommentGlobal.Text, dtTmpData.Rows[0]["Comments"].ToString().Replace(@"\r", "\r\n"));

                if (Config.CodeUser.Equals("КД"))
                    Logging.Comment("Комментарий КД: " + tbComment.Text);

                Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                   + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
                Logging.StopFirstLevel();
                /*
                 dtTmpData = dtTmp.Copy();

                if (dtTmp == null || dtTmp.Rows.Count == 0)
                {
                    return;
                }

                tbCommentNote.Text = dtTmp.Rows[0]["Description"].ToString();
                dtpDateNote.Value = (DateTime)dtTmp.Rows[0]["CreateServiceRecord"];

                if ((int)dtTmp.Rows[0]["TypeServiceRecord"] == 0) rbStandart.Checked = true; else rbTemplate.Checked = true;

                cmbBlock.SelectedValue = (int)dtTmp.Rows[0]["id_Block"];
                getDeps();
                cmbDeps.SelectedValue = (int)dtTmp.Rows[0]["id_Department"];


                tbSumma.Text = decimal.Parse(dtTmp.Rows[0]["Summa"].ToString()).ToString("0.00");


                if ((bool)dtTmp.Rows[0]["bCashNonCash"]) rbCard.Checked = true; else rbMoney.Checked = true;

                if (dtTmp.Rows[0]["DataSumma"] == DBNull.Value && dtTmp.Rows[0]["bDataSumma"] != DBNull.Value && (bool)dtTmp.Rows[0]["bDataSumma"])
                {
                    chbMoreNote.Checked = true;
                }
                else if (dtTmp.Rows[0]["bDataSumma"] != DBNull.Value && !(bool)dtTmp.Rows[0]["bDataSumma"] && dtTmp.Rows[0]["DataSumma"] != DBNull.Value)
                {
                    chbSingleNote.Checked = true;
                    dtpSingleNote.Value = (DateTime)dtTmp.Rows[0]["DataSumma"];
                }

                dtpNextDate.Value = (DateTime)dtTmp.Rows[0]["MonthB"];
                

                tbCommentGlobal.Text = dtTmp.Rows[0]["Comments"].ToString().Replace(@"\r", "\r\n");

                btView.Visible = true;
                btAddFile.Visible = true;

                id_status = (int)dtTmp.Rows[0]["id_Status"];
                TypeServiceRecord = (int)dtTmp.Rows[0]["TypeServiceRecord"];
                 */
            }


            isEdit = false;
            this.DialogResult = DialogResult.OK;
        }

        private void chbSingleNote_CheckedChanged(object sender, EventArgs e)
        {
            chbMoreNote.Enabled = !chbSingleNote.Checked;
            dtpSingleNote.Enabled = chbSingleNote.Checked;
            isEdit = true;
        }

        private void chbMoreNote_CheckedChanged(object sender, EventArgs e)
        {
            chbSingleNote.Enabled = !chbMoreNote.Checked;

            btAddBlock.Enabled = btEditBlock.Enabled = btDelBlock.Enabled = chbMoreNote.Checked;

            isEdit = true;
        }

        private void cmbBlock_SelectionChangeCommitted(object sender, EventArgs e)
        {
            createDeps();
            isEdit = true;
        }

        private void btAddFile_Click(object sender, EventArgs e)
        {
            workDoc.frmDocument frmD = new workDoc.frmDocument(isView);
            frmD.id_ServiceRecords = id;
            frmD.ShowDialog();
        }

        private void btView_Click(object sender, EventArgs e)
        {
            globalForm.frmViewHistoryStatus frmVS = new globalForm.frmViewHistoryStatus();
            frmVS.id_ServiceRecords = id;
            frmVS.ShowDialog();
        }

        private void btAddBlock_Click(object sender, EventArgs e)
        {
            frmAddEditMoneySN frmM = new frmAddEditMoneySN();
            frmM.Text = "Добавить дату/сумму в СЗ";
            frmM.id_ServiceRecords = id;
            frmM.dtMultipleReceivingMone = dtMultipleReceivingMone;
            frmM.summaDC = decimal.Parse(tbSumma.Text);
            if (DialogResult.OK == frmM.ShowDialog())
            {
                getMultipleReceivingMone();
                isEdit = true;
            }
        }

        private void btEditBlock_Click(object sender, EventArgs e)
        {

            if (dtMultipleReceivingMone == null || dtMultipleReceivingMone.DefaultView.Count == 0 || dgvNote.CurrentRow == null || dgvNote.CurrentRow.Index == -1) return;

            DataRowView row = dtMultipleReceivingMone.DefaultView[dgvNote.CurrentRow.Index];
            frmAddEditMoneySN frmM = new frmAddEditMoneySN();
            frmM.Text = "Редактировать дату/сумму в СЗ";
            frmM.id_ServiceRecords = id;
            frmM.row = row;
            frmM.summaDC = decimal.Parse(tbSumma.Text);
            frmM.dtMultipleReceivingMone = dtMultipleReceivingMone;
            if (DialogResult.OK == frmM.ShowDialog())
                getMultipleReceivingMone();
            isEdit = true;
        }

        private void btDelBlock_Click(object sender, EventArgs e)
        {

            if (dtMultipleReceivingMone == null || dtMultipleReceivingMone.DefaultView.Count == 0 || dgvNote.CurrentRow == null || dgvNote.CurrentRow.Index == -1) return;

            if (DialogResult.Yes == MessageBox.Show("Удалить выбранную запись?", "Запрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
            {

                DataRowView row = dtMultipleReceivingMone.DefaultView[dgvNote.CurrentRow.Index];
                int idMP = (int)row["id"];

                if (idMP != -1)
                {
                    Config.hCntMain.setMultipleReceivingMoney(id, "", 0, DateTime.Now, idMP, true);
                }

                row.Delete();
                dtMultipleReceivingMone.AcceptChanges();
                isEdit = true;
            }

            /*
            if (dtBlock == null || dtBlock.DefaultView.Count == 0 || dgvBlokVsDeps.CurrentRow == null || dgvBlokVsDeps.CurrentRow.Index == -1) return;

            int id = (int)dtBlock.DefaultView[dgvBlokVsDeps.CurrentRow.Index]["id"];
            int id_block = (int)dtBlock.DefaultView[dgvBlokVsDeps.CurrentRow.Index]["id_Block"];
            int id_deps = (int)dtBlock.DefaultView[dgvBlokVsDeps.CurrentRow.Index]["id_Block"];
            DataTable dtTMP = Config.hCntMain.setBlockVsDepartment(id_block, id_deps, id, true);
            getBlock();
             */
        }

        private DataTable dtMultipleReceivingMone;
        private void getMultipleReceivingMone()
        {
            if (id == -1 && dtMultipleReceivingMone != null) return;

            dtMultipleReceivingMone = Config.hCntMain.getMultipleReceivingMone(id);
            dgvNote.DataSource = dtMultipleReceivingMone;
        }

        private void dgvNote_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {
                //TODO - Button Clicked - Execute Code Here
                int id_multi = (int)dtMultipleReceivingMone.DefaultView[dgvNote.CurrentRow.Index]["id"];
                //MessageBox.Show(id_multi.ToString());
            }
        }

        #region "Режимы работы формы"
        /*

        private void RKV()
        { }

        private void CNT()
        { }

        private void KD()
        { }

        private void OP()
        { }
        */
        #endregion
        public void btAccept_Click(object sender, EventArgs e)
        {
            decimal summa = decimal.Parse(tbSumma.Text.Trim());
            foreach (DataRow r in dtMultipleReceivingMone.Rows)
            {
                Allsumma += decimal.Parse(r["Summa"].ToString());
            }
            if (Allsumma > summa)
            {
                MessageBox.Show("Сумма выплат больше суммы ДС!", "Информирование", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            ////Потом удалить
            //ChangeStatusAccept();
            //isScan = true;
            //resultScaner = "";
            //return;

            bool setScanOK = getScan();
            try { FingerScan(sender, e); }
            catch
            {
                MessageBox.Show("Сканер не обнаружен");
                Logging.StartFirstLevel(1336);
                Logging.Comment("Сканер не обнаружен;");
                Logging.Comment("Время: " + DateTime.Now.ToString());
                Logging.StopFirstLevel();
                return;
                //resultScaner = "Сканер не подключен\r\n";
                //enterPasword();
            }

            if (resultScaner.Contains("Сканер не подключен") || setScanOK == false)
            {
                MessageBox.Show("Сканер не обнаружен");
                Logging.StartFirstLevel(1336);
                Logging.Comment("Сканер не обнаружен;");
                Logging.Comment("Время: " + DateTime.Now.ToString());
                Logging.StopFirstLevel();
                return;             //enterPasword();
            }

            else if (resultScaner.Equals("Non"))
                return;

            else if (resultScaner.Contains("Скан не определен. Попробуйте еще раз") || resultScaner.Equals(""))
            {
                NotFoundScan(sender, e, "Accept");
                Logging.StartFirstLevel(1335);
                Logging.Comment("Скан пальца не определен;");
                Logging.Comment("Время: " + DateTime.Now.ToString());
                Logging.StopFirstLevel();
            }

            else
            {
                ChangeStatusAccept();
                isScan = true;
            }


            resultScaner = "";
        }


        private DataTable dtStatus;
        private void setLog(int id, string comment, int id_status, bool isSend)
        {
            DataTable dtTmpData = Config.hCntMain.getServiceRecordsBody(id);
            if (isSend)
                Logging.StartFirstLevel(1253);
            else
                Logging.StartFirstLevel(1256);


            Logging.Comment("Произведено изменение статуса СЗ:");
            Logging.Comment("Статус до ID: " + dtTmpData.Rows[0]["id_Status"].ToString() + "; Наименование:" + dtTmpData.Rows[0]["nameStatus"].ToString());
            Logging.Comment("Статус после ID: " + id_status + "; Наименование:" + dtStatus.Select("id = " + id_status)[0]["cName"].ToString());

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


        public void btRefuse_Click(object sender, EventArgs e)
        {
            ////Потом удалить
            //ChangeStatusAccept();
            //isScan = true;
            //resultScaner = "";
            //return;

            bool setScanOK = getScan();
            try { FingerScan(sender, e); }
            catch
            {
                MessageBox.Show("Сканер не обнаружен");
                Logging.StartFirstLevel(1336);
                Logging.Comment("Сканер не обнаружен; ");
                Logging.Comment("Время: " + DateTime.Now.ToString());
                Logging.StopFirstLevel();
                return;
                //resultScaner = "Сканер не подключен\r\n";
                //Refuse();
            }

            if (resultScaner.Equals("Сканер не подключен\r\n") || setScanOK == false)
            {
                MessageBox.Show("Сканер не обнаружен");
                Logging.StartFirstLevel(1336);
                Logging.Comment("Сканер не обнаружен; ");
                Logging.Comment("Время: " + DateTime.Now.ToString());
                Logging.StopFirstLevel();
                return;   //Refuse();
            }


            else if (resultScaner.Equals("Non"))
                return;

            else if (resultScaner.Contains("Скан не определен. Попробуйте еще раз\r\n") || resultScaner.Equals(""))
            {
                NotFoundScan(sender, e, "Refuse");
                Logging.StartFirstLevel(1335);
                Logging.Comment("Скан пальца не определен;");
                Logging.Comment("Время: " + DateTime.Now.ToString());
                Logging.StopFirstLevel();
            }

            else
            {
                ChangeStatusRefuse();
                isScan = true;
            }


            resultScaner = "";

        }
        private void Refuse()
        {
            globalForm.frmPassword frmP = new globalForm.frmPassword();
            frmP.idUser = Nwuram.Framework.Settings.User.UserSettings.User.Id;
            if (DialogResult.OK == frmP.ShowDialog())
            {
                ChangeStatusRefuse();
                //this.DialogResult = DialogResult.OK;
            }
        }

        public void ChangeStatusRefuse()
        {
            string comments = "";

            if (TypeServiceRecord == 0 && id_status == 4)
            {
                setLog(id, comments, 6, false);
                Config.hCntMain.updateServiceRecordsStatus(id, 6, comments);
            }
            else if (TypeServiceRecord == 1 && (id_status == 4 || id_status == 10))
            {
                switch (id_status)
                {
                    case 4: setLog(id, comments, 6, false); Config.hCntMain.updateServiceRecordsStatus(id, 6, comments); break;
                    case 10: setLog(id, comments, 12, false); Config.hCntMain.updateServiceRecordsStatus(id, 12, comments); break;//12
                }
            }
            this.DialogResult = DialogResult.OK;
        }


        private void frmServiceNote_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = isEdit && DialogResult.No == MessageBox.Show(Config.centralText("На форме есть несохраненные данные.\nЗакрыть форму без сохранения?\n"), "Выход из программы", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
        }

        private void rbMoney_Click(object sender, EventArgs e)
        {
            isEdit = true;
            tbSummaNonCash.Text = tbSummaCash.Text = "0,00";
        }

        private void dtpDateNote_ValueChanged(object sender, EventArgs e)
        {
            isEdit = true;
        }

        private void cmbDeps_SelectionChangeCommitted(object sender, EventArgs e)
        {
            isEdit = true;
            if (cmbDeps.SelectedIndex==-1 || cmbDeps.SelectedValue == null)
            {
                lTypicalWorks.Visible = cmbTypicalWorks.Visible = false;
                return;
            }
            int _id_deps = (int)cmbDeps.SelectedValue;
            EnumerableRowCollection<DataRow> rowCollect = dtListDeps.AsEnumerable().Where(r => r.Field<string>("value").Equals(_id_deps.ToString()));
            lTypicalWorks.Visible = cmbTypicalWorks.Visible = rowCollect.Count() > 0;
        }

        private void dtpNextDate_ValueChanged(object sender, EventArgs e)
        {
            isEdit = true;
        }

        private void rbStandart_Click(object sender, EventArgs e)
        {
            isEdit = true;
        }
        #region Сканер пальца
        private bool getScan()
        {
            DataTable tb = Config.hCntMain.getFingerScan();
            try
            {
                if (tb.Rows[0]["fingerScanOne"].ToString().Length > 1)
                    writeScanerToFile(tb, "fingerScanOne");
                if (tb.Rows[0]["fingerScanTwo"].ToString().Length > 1)
                    writeScanerToFile(tb, "fingerScanTwo");
                if (tb.Rows[0]["fingerScanThree"].ToString().Length > 1)
                    writeScanerToFile(tb, "fingerScanThree");

                return true;
            }
            catch { return false; }

        }

        private void writeScanerToFile(DataTable tb, string finger)
        {
            string scan = "";
            scan = Config.Decode(tb.Rows[0][finger].ToString(), tb.Rows[0]["password"].ToString().Substring(0, 4));
            File.AppendAllText(NameFileFinger, (scan.Length - 2 + scan).Replace("\n", "").Replace("\r", "")); //"C:\\Users\\fingers.txt"
        }

        private void btnSaveComment_Click(object sender, EventArgs e)
        {
            if (tbComment.Text.Length > 0)
            {

                Logging.StartFirstLevel(1251);
                Logging.Comment("Id СЗ: " + id);
                Logging.Comment("Номер СЗ: " + dtTmpData.Rows[0]["Number"].ToString());
                Logging.Comment("Комментарий КД: " + tbComment.Text);
                Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                                + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
                Logging.StopFirstLevel();

                Config.hCntMain.updateServiceRecordsStatus(id, id_status, tbComment.Text);
                frmServiceNote_Load(sender, e);
                tbComment.Text = "";
            }
        }

        private void rbOneTime_CheckedChanged(object sender, EventArgs e)
        {
            if (rbOneTime.Checked)
            {
                rbMonthly.Checked = false;
                TypeServiceRecordOnTime = 1;
                isEdit = true;
                gbFond.Visible = false;
                fondEnableelement();
            }
        }

        private void rbMonthly_CheckedChanged(object sender, EventArgs e)
        {
            if (rbMonthly.Checked)
            {
                rbOneTime.Checked = false;
                TypeServiceRecordOnTime = 2;
                isEdit = true;
                gbFond.Visible = false;
                fondEnableelement();
            }
        }

        private void rbMix_CheckedChanged(object sender, EventArgs e)
        {
            tbSummaCash.Enabled = tbSummaNonCash.Enabled = rbMix.Checked;
        }

        private void tbSummaNonCash_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void tbSummaCash_Leave(object sender, EventArgs e)
        {
            if (tbSummaCash.Text.ToString().Length == 0)
                tbSummaCash.Text = "0,00";
            else
                tbSummaCash.Text = decimal.Parse(tbSummaCash.Text.ToString()).ToString("######0.00");

            isEdit = true;
        }

        private void tbSummaNonCash_Leave(object sender, EventArgs e)
        {
            if (tbSummaNonCash.Text.ToString().Length == 0)
                tbSummaNonCash.Text = "0,00";
            else
                tbSummaNonCash.Text = decimal.Parse(tbSummaNonCash.Text.ToString()).ToString("######0.00");

            isEdit = true;
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void dgvFond_Paint(object sender, PaintEventArgs e)
        {
            tbFondNumber.Location = new Point(dgvFond.Location.X, tbFondNumber.Location.Y);
            tbFondNumber.Size = new Size(cFNumber.Width, tbFondNumber.Size.Height);

            tbFondDiscript.Location = new Point(dgvFond.Location.X + cFNumber.Width + cFSumma.Width + 1, tbFondNumber.Location.Y);
            tbFondDiscript.Size = new Size(cFDescript.Width, tbFondNumber.Size.Height);
        }

        private void btAddFond_Click(object sender, EventArgs e)
        {
            fond.frmFond fFond = new fond.frmFond() { idSZ = id, TypeServiceRecordOnTime = TypeServiceRecordOnTime };
            if (DialogResult.OK == fFond.ShowDialog())
            {
                tbFond.Text = fFond.selectSrtData;
                idFond = fFond.selectId;
                sumDopFond = fFond.Summa;
                cmbValuta.SelectedValue = "RUB";
                cmbValuta.Enabled = false;
                //rbMonthly.Enabled = false;
                reSummFond();
                setElementFondSelect();
            }
        }

        private void btDelFond_Click(object sender, EventArgs e)
        {
            idFond = null;
            tbFond.Clear();
            sumDopFond = 0;
            //rbMonthly.Enabled = true;
            reSummFond();
            setElementFondSelect();
        }

        private void btFondViewSZ_Click(object sender, EventArgs e)
        {


            int sId = (int)(dgvFond.DataSource as DataTable).DefaultView[dgvFond.CurrentRow.Index]["id_ServiceRecords"];
            frmServiceNote frmS = new frmServiceNote();
            frmS.id = sId;
            frmS.Text = "Просмотр СЗ";
            frmS.setIsView();
            frmS.ShowDialog();

        }

        private void rbFond_CheckedChanged(object sender, EventArgs e)
        {
            if (rbFond.Checked)
            {
                TypeServiceRecordOnTime = 3;

                cmbValuta.SelectedValue = "RUB";
                rbStandart.Checked = true;
                rbMoney.Checked = true;
                gbFond.Visible = true;
                fondEnableelement();
            }
        }

        private void FingerScan(object sender, EventArgs e)
        {
            resultScaner = "";
            startFingerScan(sender, e);
            while (resultScaner.Equals(""))
                System.Threading.Thread.Sleep(1000);
        }
        private void startFingerScan(object sender, EventArgs e)
        {
            Process p = new Process();
            p.Exited += new EventHandler(p_Exited);
            p.StartInfo.FileName = NameProgramm;
            p.EnableRaisingEvents = true;
            p.Start();
        }





        // Проверка на завершенный процесс
        void p_Exited(object sender, EventArgs e)
        {
            FileStream fstream = File.OpenRead(NameFileResult);
            byte[] array = new byte[fstream.Length];
            if (array != null)
            {
                fstream.Read(array, 0, array.Length);
                string textFromFile = System.Text.Encoding.Default.GetString(array);
                resultScaner = textFromFile;
                fstream.Close();
                File.WriteAllText(NameFileResult, "");
                File.WriteAllText(NameFileFinger, "");
            }
            else resultScaner = "";
        }

        private void NotFoundScan(object sender, EventArgs e, string str)
        {
            DialogResult result = MessageBox.Show("Скан не определен. Попробуете еще раз?", "Сообщение",
                                            MessageBoxButtons.YesNo,
                                            MessageBoxIcon.Information,
                                            MessageBoxDefaultButton.Button1,
                                            MessageBoxOptions.DefaultDesktopOnly);
            if (result == DialogResult.Yes)
            {
                if (str.Equals("Refuse"))
                    btRefuse_Click(sender, e);
                else if (str.Equals("Accept"))
                    btAccept_Click(sender, e);
            }
            else return; //enterPasword(); 
        }

        private void fondEnableelement()
        {
            btAddBlock.Enabled =
            btEditBlock.Enabled =
            btDelBlock.Enabled =
            chbSingleNote.Enabled =
            chbMoreNote.Enabled =
            tbSummaCash.Enabled =
            tbSummaNonCash.Enabled =
            grbTypeOplata.Enabled =
            cmbValuta.Enabled =
            TypeSR.Enabled = !rbFond.Checked;

            lFond.Visible = btAddFond.Visible = btDelFond.Visible = tbFond.Visible = !rbMonthly.Checked;

            idFond = null;
            tbFond.Clear();
        }

        private void setElementFondSelect()
        {
            if (idFond != null || rbFond.Checked)
            {
                cmbValuta.SelectedValue = "RUB";
                rbStandart.Checked = true;
                rbMoney.Checked = true;

                TypeSR.Enabled = false;
                cmbValuta.Enabled = false;
                grbTypeOplata.Enabled = false;
            }
            else
            {
                cmbValuta.Enabled = true;
                cmbValuta.Enabled = true;
                grbTypeOplata.Enabled = true;
            }
        }

        private void tbFondNumber_TextChanged(object sender, EventArgs e)
        {
            setFilterFond();
        }

        private void btFondPrintSZ_Click(object sender, EventArgs e)
        {
            DataTable dtDatReport = Config.hCntMain.getDataReportFond(idFond!=null ? (int) idFond : id);
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

        private void setFilterFond()
        {

            if (dgvFond.DataSource == null) { btFondViewSZ.Enabled = false; btFondPrintSZ.Enabled = false; return; }
            if ((dgvFond.DataSource as DataTable).Rows.Count == 0) { btFondViewSZ.Enabled = false; btFondPrintSZ.Enabled = false; return; }

            try
            {
                string filter = "";
                if (tbFondDiscript.Text.Trim().Length > 0)
                    filter += (filter.Length > 0 ? " and " : "") + $"Description like '%{tbFondDiscript.Text.Trim()}%'";

                if (tbFondNumber.Text.Trim().Length > 0)
                    filter += (filter.Length > 0 ? " and " : "") + $"Convert(Number,'System.String') like '%{tbFondNumber.Text.Trim()}%'";

                (dgvFond.DataSource as DataTable).DefaultView.RowFilter = filter;
            }
            catch
            {
                (dgvFond.DataSource as DataTable).DefaultView.RowFilter = "id_ServiceRecords = -1";
            }
            finally
            {
                dgvFond_SelectionChanged(null, null);
            }
        }

        private void dgvFond_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvFond.DataSource == null) { btFondViewSZ.Enabled = false; btFondPrintSZ.Enabled = false; return; }
            if ((dgvFond.DataSource as DataTable).Rows.Count == 0) { btFondViewSZ.Enabled = false; btFondPrintSZ.Enabled = false; return; }
            if ((dgvFond.DataSource as DataTable).DefaultView.Count == 0) { btFondViewSZ.Enabled = false; btFondPrintSZ.Enabled = true; return; }

            btFondViewSZ.Enabled = true;
            btFondPrintSZ.Enabled = true;
        }

        private decimal sumDopFond = 0;
        private decimal fullSumm = 0;
        private void reSummFond()
        {
            //убрано kav
           // if (TypeServiceRecordOnTime != 3) return;

            DataTable dtListRecordFond = (dgvFond.DataSource as DataTable);
            if (dtListRecordFond == null) return;

            Object objSumFond = dtListRecordFond.Compute("SUM(Summa)", "");
            Object objSumAccept = dtListRecordFond.Compute("SUM(Summa)", "Number is not null");
            decimal resFond;
            decimal resAccepted;
            if (decimal.TryParse(objSumAccept.ToString(), out resAccepted))
            {
                tbRealSum.Text = resAccepted.ToString("0.00");
            }
            else
            {
                tbRealSum.Text = "0,00";
            }
            if (decimal.TryParse(objSumFond.ToString(), out resFond))
            {              
                tbSumSZFond.Text = resFond.ToString("0.00");        
               // tbSumSZFond.Text = resFond.ToString("0.00");
               // tbSumFond.Text = (decimal.Parse(tbSumma.Text) + sumDopFond).ToString("0.00");
               // tbFondResult.Text = (decimal.Parse(tbSumma.Text) + sumDopFond - resFond).ToString("0.00");
            }
            else
            {
                // tbSumSZFond.Text = "0.00";
                // tbSumFond.Text = (decimal.Parse(tbSumma.Text) + sumDopFond).ToString("0.00");
                // tbFondResult.Text = (decimal.Parse(tbSumma.Text) + sumDopFond).ToString("0.00");
                tbSumSZFond.Text = "0,00";
               
            }
            tbSumFond.Text = fullSumm.ToString("0.00");
           // tbRealSum.Text = decimal.Parse(objSumAccept.ToString()).ToString("0.00");
            tbFondResult.Text = (TypeServiceRecordOnTime == 1 ? (decimal.Parse(tbSumFond.Text) - decimal.Parse(tbRealSum.Text)).ToString("0.00")
                : (decimal.Parse(tbSumFond.Text) - decimal.Parse(tbSumSZFond.Text)).ToString("0.00"));
            Console.WriteLine("ffff");
        }

        #endregion

        #region Ручной ввод пароля
        private void enterPasword()
        {
            globalForm.frmPassword frmP = new globalForm.frmPassword();
            frmP.idUser = Nwuram.Framework.Settings.User.UserSettings.User.Id;
            if (DialogResult.OK == frmP.ShowDialog())
            {
                ChangeStatusAccept();
                //this.DialogResult = DialogResult.OK;
            }

        }
        public void ChangeStatusAccept()
        {
            string strComment = "";
            //if (Config.CodeUser.Equals("КНТ") || Config.CodeUser.Equals("КД"))
            //{
            //  globalForm.frmComment frmCom = new globalForm.frmComment();
            //  if (DialogResult.OK == frmCom.ShowDialog())
            //  {
            //    strComment = frmCom.getComment;
            //  }
            //}

            if (TypeServiceRecord == 0 && id_status == 4)
            {
                setLog(id, strComment, 5, true);
                Config.hCntMain.updateServiceRecordsStatus(id, 5, strComment);
            }
            else if (TypeServiceRecord == 1 && (id_status == 4 || id_status == 10))
            {
                switch (id_status)
                {
                    case 4: setLog(id, strComment, 7, true); Config.hCntMain.updateServiceRecordsStatus(id, 7, strComment); break;
                    case 10: setLog(id, strComment, 11, true); Config.hCntMain.updateServiceRecordsStatus(id, 11, strComment); break;//11
                }
            }
            this.DialogResult = DialogResult.OK;
        }
        #endregion



    }


}