using System;
using System.Windows.Forms;

namespace mynumpad
{
    /// <summary>
    /// main entry point for the application
    /// </summary>
    static class Program
    {
        /// <summary>
        /// the main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // enable visual styles for modern ui appearance
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // ensure only one instance of the application runs at a time
            bool createdNew;
            using (var mutex = new System.Threading.Mutex(true, "MyNumpadKeyboardMapper", out createdNew))
            {
                if (createdNew)
                {
                    // run the main form
                    Application.Run(new MainForm());
                }
                else
                {
                    // another instance is already running
                    MessageBox.Show(
                        "MyNumpad Keyboard Mapper is already running.",
                        "Already Running",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
            }
        }
    }
}
