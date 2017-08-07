using System;
using System.Windows.Forms;

namespace QuadraticSurfaces
{
    static class Program
    {
        public static MainForm formMain;
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            formMain = new MainForm();
            Application.Run(formMain);
        }
    }
}
