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

            uint code = 0;

            if (!wfpClient.log_filter.isSet())
            {
                WriteLineToConsole($"No filters provided...");
                return;
            }

            WriteLineToConsole($"The Following objects will be unregistered:");
            Print_Statistics_Detailed();
                
            if (! PromptForYesNo()) 
                return;

            //code = wfpClient.
            var filters = wfpClient.GetFilters();
            var callouts = wfpClient.GetCallouts();
            var sublayers = wfpClient.GetSubLayers();
            var providers = wfpClient.GetProviders();

            //delete filters
            foreach (var item in filters)
            {
                wfpClient.DeleteFilter(item.filterKey);
            }
            WriteLineToConsole($"Remove '{filters.Count()}' filters: done");

            //delete callouts
            foreach (var item in callouts)
            {
                wfpClient.DeleteCallout(item.calloutKey);
            }
            WriteLineToConsole($"Remove '{callouts.Count()}' callouts: done");

            //delete sublayers
            foreach (var item in sublayers)
            {
                wfpClient.DeleteSubLayer(item.subLayerKey);
            }
            WriteLineToConsole($"Remove '{sublayers.Count()}' sublayers: done");

            //delete providers
            foreach (var item in providers)
            {
                wfpClient.DeleteProvider(item.providerKey);
            }
            WriteLineToConsole($"Remove '{sublayers.Count()}' providers: done");


        }

        public void KillById(Guid key) 
        {
        
        }

    }
}
