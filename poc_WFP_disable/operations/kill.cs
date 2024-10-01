using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poc_WFP_disable
{
    internal partial class Program
    {
        public void KillByFilter()
        {
            //Unregister order
            //  filter
            //  callout
            //  sublayer
            //  provider

            if (! wfpClient.log_filter.isSet())
            {
                Console.WriteLine($"The Following objects will be unregistered:");
                Print_Statistics_Detailed();
                
                if (! PromptForYesNo()) 
                    return;
                

            }
        }

        public void KillById(Guid key) 
        {
        
        }

    }
}
