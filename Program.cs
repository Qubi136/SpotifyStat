using System;
using System.Globalization;
using System.Windows.Forms;

namespace SpotifyStat
{

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string savedLanguage = Properties.Settings.Default.Language;
            if (!string.IsNullOrEmpty(savedLanguage))
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(savedLanguage);
                Thread.CurrentThread.CurrentCulture = new CultureInfo(savedLanguage);
            }
            else Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(true);
            Application.Run(new Form1());
        }


    }
}