using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static NativeAPI.WfpNativeAPI;

namespace poc_WFP_disable
{
    internal partial class Program
    {

        public void FilterChanged_Callback(IntPtr context, FWPM_FILTER_CHANGE0_ change)
        {
            WriteLineToConsole($"[Filter changed] op: {change.changeType}, filterKey: {change.filterKey}");
            if (change.changeType == FWPM_CHANGE_TYPE_.FWPM_CHANGE_ADD)
                PrintFilterInfo(wfpClient.GetFilterByKey(change.filterKey));

        }

        public void ProviderChanged_Callback(IntPtr context, FWPM_PROVIDER_CHANGE0_ change)
        {
            WriteLineToConsole($"[Provider changed] op: {change.changeType}, providerKey: {change.providerKey}");
            if (change.changeType == FWPM_CHANGE_TYPE_.FWPM_CHANGE_ADD)
                PrintProviderInfo(wfpClient.GetProviderByKey(change.providerKey));
        }

        public void CalloutChanged_Callback(IntPtr context, FWPM_CALLOUT_CHANGE0_ change)
        {
            WriteLineToConsole($"[Callout changed] op: {change.changeType}, calloutKey: {change.calloutKey}");
            if (change.changeType == FWPM_CHANGE_TYPE_.FWPM_CHANGE_ADD)
                PrintCalloutInfo(wfpClient.GetCalloutByKey(change.calloutKey)); 
        }

        public void SyblayerChanged_Callback(IntPtr context, FWPM_SUBLAYER_CHANGE0_ change)
        {
            WriteLineToConsole($"[Sublayer changed] op: {change.changeType}, sublayerKey: {change.subLayerKey}");
            if (change.changeType == FWPM_CHANGE_TYPE_.FWPM_CHANGE_ADD)
                PrintSublayerInfo(wfpClient.GetSubLayerByKey(change.subLayerKey));
        }

        public void ConnectionChanged_Callack(IntPtr context, FWPM_CONNECTION_EVENT_TYPE_ eventType, in FWPM_CONNECTION0_ connection)
        {
            WriteLineToConsole($"[Connection changed] op: {eventType}");
            if (eventType == FWPM_CONNECTION_EVENT_TYPE_.FWPM_CONNECTION_EVENT_ADD) 
                PrintConnectionInfo(connection);
        }

        public void SubscribeAllEvents()
        {
            wfpClient.SubscribeProviderChanges(ProviderChanged_Callback);
            wfpClient.SubscribeFilterChanges(FilterChanged_Callback);
            wfpClient.SubscribeCalloutChanges(CalloutChanged_Callback);
            wfpClient.SubscribeSublayerChanges(SyblayerChanged_Callback);
            wfpClient.SubscribeConnectionChanges(ConnectionChanged_Callack);

            PauseBeforeContinuing();


            wfpClient.UnsubscribeAllChanges();
        }

    }
}
