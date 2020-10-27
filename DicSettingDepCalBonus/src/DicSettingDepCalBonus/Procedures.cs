using System.Text;
using System.Collections;
using Nwuram.Framework.Data;
using Nwuram.Framework.Settings.Connection;
using System.Data;
using System;
using Nwuram.Framework.Settings.User;
using System.Threading.Tasks;
using System.Threading;

namespace DicSettingDepCalBonus
{
    class Procedures : SqlProvider
    {
        public Procedures(string server, string database, string username, string password, string appName)
              : base(server, database, username, password, appName)
        {
        }
        ArrayList ap = new ArrayList();

        #region "Справочник производителей"

        /// <summary>
        /// Запись справочника форм собственности
        /// </summary>
        /// <param name="id">Код записи</param>
        /// <param name="cName">Наименование </param>
        /// <param name="Abbreviation">Аббревиатура</param>
        /// <param name="isActive">признак активности записи</param>  
        /// <param name="isDel">Признак удаления записи</param>
        /// <param name="result">Результирующая для проверки</param>
        /// <returns>Таблица с данными</returns>
        /// <param name="id">код созданной записи</param>
        public async Task<DataTable> setDepartmentBonus(int id, int id_deps, decimal MinPayment, decimal PercentPayment, bool isDel)
        {
            ap.Clear();
            ap.Add(id);
            ap.Add(id_deps);
            ap.Add(MinPayment);
            ap.Add(PercentPayment);
            ap.Add(Nwuram.Framework.Settings.User.UserSettings.User.Id);
            ap.Add(isDel);


            DataTable dtResult = executeProcedure("[dbo].[setDepartmentBonus]",
                 new string[6] { "@id", "@id_deps", "@MinPayment", "@PercentPayment", "@id_user", "@isDel" },
                 new DbType[6] { DbType.Int32, DbType.Int32, DbType.Decimal, DbType.Decimal, DbType.Int32, DbType.Boolean }, ap);

            return dtResult;
        }




        /// <summary>
        /// Получение списка отделов для расчёта премий
        /// </summary>
        /// <param name=""></param>
        /// <returns>Таблица с данными</returns>        
        public DataTable getDepartmentBonus()
        {
            ap.Clear();

            return executeProcedure("[dbo].[getDepartmentBonus]",
                 new string[0] { },
                 new DbType[0] { }, ap);            
        }

        public DataTable getDepartments()
        {
            ap.Clear();

            return executeProcedure("[dbo].[getDepartments]",
                 new string[0] { },
                 new DbType[0] { }, ap);
        }


        #endregion

    }
}
