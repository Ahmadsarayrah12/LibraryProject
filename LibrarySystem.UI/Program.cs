using System;
using System.Windows.Forms;

namespace LibrarySystem.UI
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// Implements a login loop so sign-out returns cleanly to the login screen
        /// without restarting the process.
        /// </summary >
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            while (true)
            {
                using (frmLogin login = new frmLogin())
                {
                    if (login.ShowDialog() != DialogResult.OK)
                        break;
                }

                Application.Run(new frmMain());

                // If CurrentUser is still set, the user closed the window (not signed out)
                if (clsGlobal.CurrentUser != null)
                    break;
            }
        }
    }
}
