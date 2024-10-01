using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Wfp;

namespace poc_WFP_disable
{
    internal partial class Program : IDisposable
    {
        public WfpClient wfpClient = new WfpClient();
        private static readonly object ConsoleLock = new object();

        public void Dispose()
        {
            wfpClient.Dispose();
        }

        static void Main(string[] args)
        {

            if (! Helper.IsRunningAsAdministrator())
            {
                Helper.RestartWithElevatedPrivileges(args);
                return;
            }



            Program p = new Program();

            if (args.Length > 0)
            {
                //p.ExecuteCommand(args);
            }
            else
            {
                p.ShowMainMenu();
            }

            p.Dispose();
        }


        static void WriteLineToConsole(string message = "")
        {
            lock (ConsoleLock) // Locking to ensure thread safety
            {
                Console.WriteLine(message);
            }
        }

    }
}
