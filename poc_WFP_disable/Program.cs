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
        public Wfp.WfpClient wfpClient = new Wfp.WfpClient();
        private static readonly object ConsoleLock = new object();
        private Guid sublayer = new Guid("b19a6c3d-1e54-47ad-b0cf-fa14eef66274");

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
