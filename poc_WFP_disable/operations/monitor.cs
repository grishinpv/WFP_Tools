using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Wfp;

namespace poc_WFP_disable
{
    internal partial class Program
    {


        private void AddProviderFilter()
        {
            Console.Write("Input provider guid: ");
            string input = Console.ReadLine();
            try
            {
                wfpClient.AddMonitorFilter(new Guid(input));
            } catch (Exception ex) { 
                Console.WriteLine(ex.Message);  
            }
        }

        private void AddMonitorFilter()
        {
            int filer_id = 0;
            Console.Write("Input filter_id (negative value if exclude): ");
            string input = Console.ReadLine();
            if (int.TryParse(input, out filer_id))
                wfpClient.AddMonitorFilter(filer_id);
            else if (input != String.Empty)
            {
                wfpClient.AddMonitorFilter(input);
            }
        }

        private void AddHostFilter()
        {
            Console.Write("Input ip addrss: ");
            string input = Console.ReadLine();
            try
            {
                wfpClient.AddMonitorFilter<IPAddress>(IPAddress.Parse(input));
            }
            catch
            {
                Console.WriteLine("Incorrect  ");
            }
        }

        private void AddPortFilter()
        {
            ushort imp = 0;
            Console.Write("Input ip port number: ");
            string input = Console.ReadLine();
            if (ushort.TryParse(input, out imp))
                wfpClient.AddMonitorFilter(imp);
        }


        private void RemoveMonitorFilter()
        {
            int filer_id = 0;
            Console.Write("Input filter_id to remove: ");
            string input = Console.ReadLine();
            if (int.TryParse(input, out filer_id))
                wfpClient.RemoveMonitorFilter(filer_id);
            else
                wfpClient.RemoveMonitorFilter(input);
        }


        private void RuleMonotor()
        {
            var subscriptionHandle = wfpClient.StartMonitor();
            PauseBeforeContinuing();
            wfpClient.StopMonitor(subscriptionHandle);
        }
    }
}
