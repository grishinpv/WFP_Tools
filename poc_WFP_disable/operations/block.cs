using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Wfp;

namespace poc_WFP_disable
{
    internal partial class Program
    {
        public void BlockToRemoteIp(IPAddress address)
        {
            string name = $"WFP TOOL block ip";
            string description = $"Filter to block all outgoing connections to ip";

            uint filterId = wfpClient.BlockOutgoingToRemoteHost(
                                name,
                                description,
                                Helper.ConvertIPv4AddressToInt(address), 
                                sublayer);
            if (filterId == 0)
            {
                WriteLineToConsole($"Failed to filter remote host {address}");
                return;
            }
            WriteLineToConsole($"New filter added: ");
            PrintFilterDitailedInfo(wfpClient.GetFilterById(filterId));
        }
    }
}
