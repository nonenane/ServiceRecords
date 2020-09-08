using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Nwuram.Framework.Project;
using Nwuram.Framework.Settings.User;
using Nwuram.Framework.Logging;
using Nwuram.Framework.Settings.Connection;
using System.IO;
using System.Data;

namespace ServiceRecords
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (args.Length != 0)
                if (Project.FillSettings(args))
                {
                    Logging.Init(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);
                    Logging.StartFirstLevel(1);
                    Logging.Comment("Вход в программу");
                    Logging.StopFirstLevel();

                    Config.hCntMain = new Procedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);
                    Config.hCntSecond = new Procedures(ConnectionSettings.GetServer("2"), ConnectionSettings.GetDatabase("2"), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);
                    Config.CodeUser = Nwuram.Framework.Settings.User.UserSettings.User.StatusCode.ToUpper();

                    DataTable dtPassword = Config.hCntMain.getrUsers();

                    if (dtPassword == null || dtPassword.Rows.Count == 0)
                    {
                        globalForm.frmAddUsers frmAdd = new globalForm.frmAddUsers();
                        if (DialogResult.Cancel == frmAdd.ShowDialog())
                        {
                            return;
                        }
                    }

                    //Application.Run(new HardWare.frmListHardware() { id_ServiceRecod = 8305 });
                    //return;
                    Console.WriteLine(UserSettings.User.Id);
                    if (Config.CodeUser.Equals("АДМ"))
                        Application.Run(new settings.frmSettings());
                    else
                        Application.Run(new frmMain());

                    Logging.StartFirstLevel(2);
                    Logging.Comment("Выход из программы");
                    Logging.StopFirstLevel();
                    Project.clearBufferFiles();
                }
        }
    }
}
