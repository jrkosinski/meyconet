using System;
using System.Threading;
using System.Windows.Forms;

namespace BusinessProcessing
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            WSGUtilitieslib.Telemetry.Telemetry.StartSession();
            Application.Run(new FrmMenu());
        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            WSGUtilitieslib.Telemetry.Telemetry.AddErrorEvent(e.Exception.ToString());

            //here, force write/upload telemetry
            System.Threading.Tasks.Task.Run(async () => await WSGUtilitieslib.Telemetry.Telemetry.StoreData());
        }

        private static void CurrentDomain_UnhandledException(object sender, System.UnhandledExceptionEventArgs e)
        {
            WSGUtilitieslib.Telemetry.Telemetry.AddErrorEvent(e.ExceptionObject.ToString());

            //here, force write/upload telemetry
            System.Threading.Tasks.Task.Run(async () => await WSGUtilitieslib.Telemetry.Telemetry.StoreData());
        }
    }
}