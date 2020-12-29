using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Nwuram.Framework.Settings.Connection;
using Nwuram.Framework.Settings.User;
using System.Collections;
using System.Data.OleDb;
using System.Reflection;
using System.Text.RegularExpressions;
using Nwuram.Framework.ToExcel;
using System.Threading.Tasks;
using Nwuram.Framework.Logging;

namespace ServiceRecords.settings
{
    public partial class frmSettings : Form
    {
        private bool isEdit = false;
        DataTable dtObjects = new DataTable();

        public frmSettings()
        {            
            InitializeComponent();

            dgvRoute.AutoGenerateColumns = false;
            dgvDepVsRoute.AutoGenerateColumns = false;
            dgvBlokVsDeps.AutoGenerateColumns = false;
            dgvDepsSettings.AutoGenerateColumns = false;

            ToolTip tt = new ToolTip();
            tt.SetToolTip(btClose, "Выход");
            tt.SetToolTip(btSelect, "Сохранить");

            tt.SetToolTip(btAdd, "Установить связь маршрута с отделом");
            tt.SetToolTip(btDel, "Разорвать связь маршрута с отделом");


            tt.SetToolTip(btAddBlock, "Добавить");
            tt.SetToolTip(btEditBlock, "Редактировать");
            tt.SetToolTip(btDelBlock, "Удалить");

            tt.SetToolTip(groupBox4, Config.centralText("Для выбранных отделов  будет отображаться раскрывающийся \nсписок «Тип работ» на формах создания, редактирования\n и просмотра СЗ (обязательный для заполнения).\n"));
        }

        private void frmSettings_Load(object sender, EventArgs e)
        {
            getData();
            isEdit = false;
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            var differences =
                dtDepsSettings.AsEnumerable().Except(dtDepsSettings_old.AsEnumerable(),
                                            DataRowComparer.Default);
            isEdit = differences.Count() > 0;

            //this.DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btSelect_Click(object sender, EventArgs e)
        {
            #region "Общие"

            string valueSettings = dtpLimit.Value.ToShortTimeString();
            Config.hCntMain.SetSettings("овпд", valueSettings);

            valueSettings = nudTimeSafe.Value.ToString();
            Config.hCntMain.SetSettings("срха", valueSettings);

            if (rbNeed.Checked) valueSettings = "1"; else if (rbNotNeed.Checked) valueSettings = "0";
            Config.hCntMain.SetSettings("опрф", valueSettings);

            foreach (DataRow row in dtDepsSettings.AsEnumerable().Where(r => r.Field<bool>("isSelect")))
                Config.hCntMain.SetSettingsMulti("otna", row["id_Department"].ToString(), "отображение элемента «тип работ» на форме создания СЗ", false);

            foreach (DataRow row in dtDepsSettings_old.AsEnumerable().Where(r => r.Field<bool>("isSelect")))
            {
                EnumerableRowCollection<DataRow> rowCollect = dtDepsSettings.AsEnumerable().Where(r => r.Field<int>("id_Department") == (int)row["id_Department"] && !r.Field<bool>("isSelect"));
                if(rowCollect.Count()>0)
                    Config.hCntMain.SetSettingsMulti("otna", row["id_Department"].ToString(), "отображение элемента «тип работ» на форме создания СЗ", true);
            }

            Logging.StartFirstLevel(353);

            Logging.Comment("Ограничение по времени на получение ДС: " + dtpLimit.Value.ToShortTimeString());
            Logging.Comment("Срок хранения обработанных СЗ: " + nudTimeSafe.Value.ToString());
            Logging.Comment("Прикрепление файлов «Отчет по ДС к СЗ»: " + (rbNeed.Checked ? rbNeed.Text : rbNotNeed.Text));

            Logging.Comment("Список отделов у которых будет отображаться «Тип работ» на форме создания СЗ");
            foreach (DataRow row in dtDepsSettings.AsEnumerable().Where(r => r.Field<bool>("isSelect")))
                Logging.Comment($"Отдел ID:{row["id_Department"]}; Наименование:{row["cName"]}");

            Logging.Comment("Список отделов у которых не будет отображаться «Тип работ» на форме создания СЗ");
            foreach (DataRow row in dtDepsSettings.AsEnumerable().Where(r => !r.Field<bool>("isSelect")))
                Logging.Comment($"Отдел ID:{row["id_Department"]}; Наименование:{row["cName"]}");


            Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
            Logging.StopFirstLevel();

            #endregion
            dtDepsSettings_old = dtDepsSettings.Copy();
            isEdit = false;
            MessageBox.Show("Данные сохранены!", "Информирование", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.DialogResult = DialogResult.OK;
        }

        DataTable dtRoute, dtRouteToCMB, dtDeps, dtRouteVsDeps, dtDepsSettings, dtDepsSettings_old;

        private void getData()
        {
            #region "Общие"

            string valueSettings = Config.hCntMain.GetSettings("овпд");

            if (valueSettings.Length > 0)
                dtpLimit.Value = DateTime.Parse(valueSettings);

            valueSettings = Config.hCntMain.GetSettings("срха");
            if (valueSettings.Length > 0)
                nudTimeSafe.Value = decimal.Parse(valueSettings);

            valueSettings = Config.hCntMain.GetSettings("опрф");
            if (valueSettings.Length > 0)
            {
                rbNeed.Checked = valueSettings.Equals("1");
                rbNotNeed.Checked = valueSettings.Equals("0");
            }

            DataTable dtListDeps = Config.hCntMain.GetSettingsTable("otna");
            DataTable dtTmp = Config.hCntMain.getBlockVsDepartment();
            dtDepsSettings = new DataTable();
            dtDepsSettings.Columns.Add("id_Department", typeof(int));
            dtDepsSettings.Columns.Add("cName", typeof(string));
            dtDepsSettings.Columns.Add("isSelect", typeof(bool));
            dtDepsSettings.AcceptChanges();

            var result = (from table in dtTmp.AsEnumerable()
                          group table by new
                          {
                              id_Department = table["id_Department"],
                              nameDeps = table["nameDeps"]
                          }
                into g
                          select new
                          {
                              g.Key.id_Department,
                              g.Key.nameDeps

                          }).OrderBy(r => r.nameDeps);

            foreach (var d in result)
            {
                DataRow row = dtDepsSettings.NewRow();
                row["id_Department"] = d.id_Department;
                row["cName"] = d.nameDeps;
                row["isSelect"] = dtListDeps.AsEnumerable().Where(r => r.Field<string>("value").Equals(d.id_Department.ToString())).Count() > 0;
                dtDepsSettings.Rows.Add(row);
            }
            dtDepsSettings_old = dtDepsSettings.Copy();
            dgvDepsSettings.DataSource = dtDepsSettings;

            #endregion

            #region "Маршруты"

            dtRoute = Config.hCntMain.getRoute();

            dgvRoute.DataSource = dtRoute;

            dtRouteToCMB = dtRoute.Copy();
            if (dtRouteToCMB != null && dtRouteToCMB.Rows.Count > 0)
            {
                if (dtRouteToCMB.Select("id = 0 ").Count() == 0)
                {
                    dtRouteToCMB.Rows.Add(0, "Все маршруты");
                    dtRouteToCMB.DefaultView.Sort = "id asc";
                    dtRouteToCMB = dtRouteToCMB.DefaultView.ToTable();
                }
            }

            cmbRoute.DataSource = dtRouteToCMB;
            cmbRoute.DisplayMember = "cName";
            cmbRoute.ValueMember = "id";

            dtDeps = Config.hCntMain.getDeps();

            cmbDeps.DataSource = dtDeps;
            cmbDeps.DisplayMember = "name";
            cmbDeps.ValueMember = "id";

            //dtRouteVsDeps = Config.hCntMain.getRouteVsDepartment();
            //dgvDepVsRoute.DataSource = dtRouteVsDeps;
            getRouteVsDeps();

            #endregion

            #region "Блоки"

            getBlock();

            #endregion

            #region Справочник объектов
            Grid_Load();
            #endregion
        }

        private void getRouteVsDeps()
        {
            dtRouteVsDeps = Config.hCntMain.getRouteVsDepartment();
            setFilter();
            dgvDepVsRoute.DataSource = dtRouteVsDeps;
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            if (dtRoute == null || dtRoute.DefaultView.Count == 0 || dgvRoute.CurrentRow==null || dgvRoute.CurrentRow.Index==-1) return;

            int id_deps = (Int16)cmbDeps.SelectedValue;
            int id_route = (int)dtRoute.DefaultView[dgvRoute.CurrentRow.Index]["id"];

            DataTable dtTMP = Config.hCntMain.setRouteVsDepartment(id_route, id_deps, -1);

            if (dtTMP == null || dtTMP.Rows.Count == 0)
            {
                MessageBox.Show("Ошибока добавления");
                return;
            }

            if(int.Parse(dtTMP.Rows[0]["id"].ToString()) == -1)
            {
                MessageBox.Show("Такая комбинация уже существует");
                return;
            }

            Logging.StartFirstLevel(1257);
            Logging.Comment("Назначить маршрут");
            Logging.Comment("Отдел ID: " + (Int16)cmbDeps.SelectedValue + ", Наименование: " + (string)cmbDeps.Text);
            Logging.Comment("Маршрут ID: " + (int)dtRoute.DefaultView[dgvRoute.CurrentRow.Index]["id"] + ", Наименование: " + (string)dtRoute.DefaultView[dgvRoute.CurrentRow.Index]["cName"]);


            Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
            Logging.StopFirstLevel();

            MessageBox.Show("Маршрут успешно назначен отделу!", "Информирование", MessageBoxButtons.OK, MessageBoxIcon.Information);
            getRouteVsDeps(); 
        }

        private void btDel_Click(object sender, EventArgs e)
        {
            if (dtRouteVsDeps == null || dtRouteVsDeps.DefaultView.Count == 0 || dgvDepVsRoute.CurrentRow == null || dgvDepVsRoute.CurrentRow.Index == -1) return;
            int id = (int)dtRouteVsDeps.DefaultView[dgvDepVsRoute.CurrentRow.Index]["id"];

            Config.hCntMain.setRouteVsDepartment(0, 0, id);

            Logging.StartFirstLevel(1257);
            Logging.Comment("Разорвать маршрут");
            Logging.Comment("Отдел ID: " + (Int16)dtRouteVsDeps.DefaultView[dgvDepVsRoute.CurrentRow.Index]["idDeps"] + ", Наименование: " + (string)dtRouteVsDeps.DefaultView[dgvDepVsRoute.CurrentRow.Index]["name"]);
            Logging.Comment("Маршрут ID: " + (int)dtRouteVsDeps.DefaultView[dgvDepVsRoute.CurrentRow.Index]["id_Route"] + ", Наименование: " + (string)dtRouteVsDeps.DefaultView[dgvDepVsRoute.CurrentRow.Index]["cName"]);


            Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
            Logging.StopFirstLevel();

            MessageBox.Show("Маршрут успешно удален от отдела!", "Информирование", MessageBoxButtons.OK, MessageBoxIcon.Information);
            getRouteVsDeps();
        }

        private void cmbRoute_SelectionChangeCommitted(object sender, EventArgs e)
        {
            setFilter();
        }

        private void setFilter()
        {
            try
            {
                string filter = "";

                filter += (tbDeps.Text.Length != 0 ?
                (filter.Length == 0 ? "" : " AND ") + "CONVERT(name, 'System.String') LIKE '%" + tbDeps.Text + "%'" : "");

                if (cmbRoute.SelectedValue != null)
                {
                    filter += (int.Parse(cmbRoute.SelectedValue.ToString()) != 0 ?
                        (filter.Length == 0 ? "" : " AND ") + "id_Route = " + cmbRoute.SelectedValue.ToString() : "");
                }

                dtRouteVsDeps.DefaultView.RowFilter = filter;
            }
            catch (Exception)
            {
                MessageBox.Show("Некорректное значение фильтра!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

        }

        private void tbDeps_TextChanged(object sender, EventArgs e)
        {
            setFilter();
        }

        DataTable dtBlock;
        private void getBlock()
        {
            dtBlock = Config.hCntMain.getBlockVsDepartment();
            dgvBlokVsDeps.DataSource = dtBlock;
        }

        private void btAddBlock_Click(object sender, EventArgs e)
        {
            frmBlockVsDeps frmBvD = new frmBlockVsDeps();
            frmBvD.Text = "Добавить связь блок-отдел";
            if (DialogResult.OK == frmBvD.ShowDialog())
            {
                getBlock();
            }
        }

        private void btEditBlock_Click(object sender, EventArgs e)
        {
            if (dtBlock == null || dtBlock.DefaultView.Count == 0 || dgvBlokVsDeps.CurrentRow == null || dgvBlokVsDeps.CurrentRow.Index == -1) return;

            DataRowView rowView = dtBlock.DefaultView[dgvBlokVsDeps.CurrentRow.Index];

            frmBlockVsDeps frmBvD = new frmBlockVsDeps();
            frmBvD.Text = "Редактировать связь блок-отдел";
            frmBvD.rowView = rowView;
            if (DialogResult.OK == frmBvD.ShowDialog())
            {
                getBlock();
            }
        }

        private void btDelBlock_Click(object sender, EventArgs e)
        {
            if (dtBlock == null || dtBlock.DefaultView.Count == 0 || dgvBlokVsDeps.CurrentRow == null || dgvBlokVsDeps.CurrentRow.Index == -1) return;
            if (DialogResult.Yes == MessageBox.Show("Удалить выбранную запись?", "Запрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
            {

                int id = (int)dtBlock.DefaultView[dgvBlokVsDeps.CurrentRow.Index]["id"];
                int id_block = (int)dtBlock.DefaultView[dgvBlokVsDeps.CurrentRow.Index]["id_Block"];
                int id_deps = (int)dtBlock.DefaultView[dgvBlokVsDeps.CurrentRow.Index]["id_Block"];
                DataTable dtTMP = Config.hCntMain.setBlockVsDepartment(id_block, id_deps, id, true);

                Logging.StartFirstLevel(1260);
                Logging.Comment("Блок ID: " + (int)dtBlock.DefaultView[dgvBlokVsDeps.CurrentRow.Index]["id_Block"] + "; Наименование:" + (string)dtBlock.DefaultView[dgvBlokVsDeps.CurrentRow.Index]["nameBlock"]);
                Logging.Comment("Отдел ID: " + (int)dtBlock.DefaultView[dgvBlokVsDeps.CurrentRow.Index]["id_Department"] + "; Наименование:" + (string)dtBlock.DefaultView[dgvBlokVsDeps.CurrentRow.Index]["nameDeps"]);
                Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                    + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
                Logging.StopFirstLevel();

                getBlock();
            }
        }

        private void frmSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = isEdit && DialogResult.No == MessageBox.Show(Config.centralText("На форме есть несохраненные данные.\nЗакрыть форму без сохранения?\n"), "Выход из программы", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
        }

        private void dtpLimit_ValueChanged(object sender, EventArgs e)
        {
            isEdit = true;
        }

        private void nudTimeSafe_ValueChanged(object sender, EventArgs e)
        {
            isEdit = true;
        }

        private void rbNotNeed_Click(object sender, EventArgs e)
        {
            isEdit = true;
        }

        private void rbNeed_Click(object sender, EventArgs e)
        {
            isEdit = true;
        }

        private void nudTimeSafe_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        //Справочник
        #region Справочник объектов
        private void Grid_Load()
        {
            dtObjects = Config.hCntMain.getObjects();
            if (dtObjects != null)
                if (dtObjects.Rows.Count > 0)
            dgvObjectsHandbook.DataSource = dtObjects;
        }

        private void btnAddObject_Click(object sender, EventArgs e)
        {
            AddNewObject();
            Refresh();
        }

        private void btnEditObject_Click(object sender, EventArgs e)
        {
            if (dtObjects != null)
                EditObject();
            Refresh();
        }

        private void btnDeleteObject_Click(object sender, EventArgs e)
        {
            if (dtObjects != null ) 
                DeleteObject();
            Refresh();
            EnabledButton();
        }

        private void btnCloseForm_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void tbSearchObject_TextChanged(object sender, EventArgs e)
        {
            Filter();
        }
        private void cbVievWorkObjects_CheckedChanged(object sender, EventArgs e)
        {
            ViewRows();
            EnabledButton();
            tbSearchObject_TextChanged_1(sender, e);
        }
        private void dgvObjectsHandbook_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (dgvObjectsHandbook == null) return;
            if (tbSearchObject.Text.Length < 1)
                Filter();

            if (dgvObjectsHandbook.CurrentRow != null)
                 if (!((bool)dtObjects.DefaultView[e.RowIndex]["is_Active"]))
                     dgvObjectsHandbook.Rows[e.RowIndex].DefaultCellStyle.BackColor = picbDontWork.BackColor;

        }
        private void EnabledButton()
        {
             btnEditObject.Enabled = btnDeleteObject.Enabled = dgvObjectsHandbook.CurrentRow == null ? false : true;
        }

        private void AddNewObject()
        {
            workDoc.frmAddEditObject frm = new workDoc.frmAddEditObject();
            if (DialogResult.OK == frm.ShowDialog())
                Refresh();
        }

        private void DeleteObject()
        {
            int count = (int)dgvObjectsHandbook.CurrentRow.Cells["count"].Value;
            bool is_Active = (bool)dgvObjectsHandbook.CurrentRow.Cells["is_Active"].Value;

            if (count == 0 && MessageBox.Show("Удалить выбранную запись?", "Запрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Logging.StartFirstLevel(1500);
                Logging.Comment("ID: " + dgvObjectsHandbook.CurrentRow.Cells["id_Object"].Value.ToString());
                Logging.Comment("Наименование: " + dgvObjectsHandbook.CurrentRow.Cells[1].Value.ToString());
                Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                                + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
                Logging.StopFirstLevel();

                Config.hCntMain.addEditObject(dgvObjectsHandbook.CurrentRow.Cells[1].Value.ToString(), 2, int.Parse(dgvObjectsHandbook.CurrentRow.Cells["id_Object"].Value.ToString()));
            }
            else if (count > 0 && is_Active && MessageBox.Show("Выбранная для удаления запись используется в программе. Сделать запись недействующей?", "Запрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Logging.StartFirstLevel(1501);
                Logging.Comment("ID: " + dgvObjectsHandbook.CurrentRow.Cells["id_Object"].Value.ToString());
                Logging.Comment("Наименование: " + dgvObjectsHandbook.CurrentRow.Cells[1].Value.ToString());
                Logging.Comment("Произведена смена статуса объекта на \"недействующий\"");
                Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                                + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
                Logging.StopFirstLevel();

                Config.hCntMain.addEditObject(dgvObjectsHandbook.CurrentRow.Cells[1].Value.ToString(), 1, int.Parse(dgvObjectsHandbook.CurrentRow.Cells["id_Object"].Value.ToString()));
            }
            else if (count > 0 && !is_Active && MessageBox.Show("Выбранная для удаления запись используется в программе. Сделать запись действующей?", "Запрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Logging.StartFirstLevel(1501);
                Logging.Comment("ID: " + dgvObjectsHandbook.CurrentRow.Cells["id_Object"].Value.ToString());
                Logging.Comment("Наименование: " + dgvObjectsHandbook.CurrentRow.Cells[1].Value.ToString());
                Logging.Comment("Произведена смена статуса объекта на \"действующий\"");
                Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                                + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
                Logging.StopFirstLevel();

                Config.hCntMain.addEditObject(dgvObjectsHandbook.CurrentRow.Cells[1].Value.ToString(), 1, int.Parse(dgvObjectsHandbook.CurrentRow.Cells["id_Object"].Value.ToString()));
            }
        }

        private void EditObject()
        {
            if (dgvObjectsHandbook.CurrentRow != null)
            {
                workDoc.frmAddEditObject frm = new workDoc.frmAddEditObject(int.Parse(dgvObjectsHandbook.CurrentRow.Cells["id_Object"].Value.ToString()), dgvObjectsHandbook.CurrentRow.Cells[1].Value.ToString(), 3);
                if (DialogResult.OK == frm.ShowDialog())
                    Refresh();
            }
        }

        private void tbSearchObject_TextChanged_1(object sender, EventArgs e)
        {
            Search();
            EnabledButton();
        }
        private void Search()
        {
            try
            {
                if (cbVievWorkObjects.Checked == false)
                    dtObjects.DefaultView.RowFilter = string.Format("CONVERT(is_Active, 'System.Int32')  = 1 AND name_Object LIKE '%{0}%' AND name_Object  not LIKE \'БЕЗ ОБЪЕКТА\'", tbSearchObject.Text);
                else
                    dtObjects.DefaultView.RowFilter = string.Format("name_Object LIKE '%{0}%' AND name_Object  not LIKE \'БЕЗ ОБЪЕКТА\'", tbSearchObject.Text);
            }
            catch { }
        }

        private void Filter()
        {
            string filter = "";
            if (cbVievWorkObjects.Checked == false)
                filter += "" + "CONVERT(is_Active, 'System.Int32')  = 1 AND name_Object  not LIKE \'БЕЗ ОБЪЕКТА\'  ";

            else
                filter += "" + "name_Object  not LIKE \'БЕЗ ОБЪЕКТА\' ";

            dtObjects.DefaultView.RowFilter = filter;
            EnabledButton();
        }

        private void Refresh()
        {
            Grid_Load();
            Search();
        }

        private void ViewRows()
        {
            Filter();
            
        }
        #endregion
    }
}
