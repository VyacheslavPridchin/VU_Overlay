using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Overlay_Client_DSTU
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        /// 

        public static Loading loading;

        [STAThread]
        static void Main()
        {
            //Подгрузка ресурсов
            Painter.Fonts.LoadFonts();
            //Подключение базы

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.SetUnhandledExceptionMode(UnhandledExceptionMode.ThrowException);

            SQL.User.id = "";
            SQL.User.login = "";
            SQL.User.nickname = "";
            SQL.User.type = "";
            SQL.Lesson.id = "";
            SQL.Lesson.name = "";
            SQL.Lesson.password = "";
            SQL.Lesson.UniqueKey = "";


            UnusedTestForm unusedTestForm = new UnusedTestForm();

            loading = new Loading();
            Application.Run(loading);

            

        }
    }
}
