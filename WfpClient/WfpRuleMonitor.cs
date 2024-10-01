using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static NativeAPI.WfpNativeAPI;

namespace Wfp
{
    public partial class WfpClient
    {

        public class LogFilter
        {
            public List<int> id_filter = new List<int>();
            public List<string> name_filter = new List<string>();
            public Guid providerKey = Guid.Empty;
            public List<IPAddress> hosts = new List<IPAddress>();
            public List<ushort> ports = new List<ushort>();                

            public bool isSet()
            {
                return id_filter.Count > 0 || name_filter.Count > 0 || providerKey != Guid.Empty;
            }
        }

        public void AddMonitorFilter<T>(T item)
        {
            if (typeof(T) == typeof(int))
                log_filter.id_filter.Add(Convert.ToInt32(item));
            if (typeof(T) == typeof(string))
                log_filter.name_filter.Add(Convert.ToString(item));
            if (typeof(T) == typeof(Guid))
                log_filter.providerKey = new Guid(Convert.ToString(item));
            if (typeof(T) == typeof(IPAddress))
                log_filter.hosts.Add(IPAddress.Parse(Convert.ToString(item)));
            if (typeof(T) == typeof(ushort))
                log_filter.ports.Add((ushort)Convert.ToInt16(item));
        }

        public void RemoveMonitorFilter<T>(T filterId)
        {
            if (typeof(T) == typeof(int))
                log_filter.id_filter.Remove(Convert.ToInt32(filterId));
            if (typeof(T) == typeof(string))
                log_filter.name_filter.Remove(Convert.ToString(filterId));
        }

        public void ClearMonitorFilter()
        {
            log_filter.id_filter.Clear();
            log_filter.name_filter.Clear(); 
            log_filter.providerKey = Guid.Empty;   
            log_filter.hosts.Clear();
            log_filter.ports.Clear();
        }

        public IntPtr StartMonitor()
        {
            FWPM_NET_EVENT_ENUM_TEMPLATE0_ enumTemplate = new FWPM_NET_EVENT_ENUM_TEMPLATE0_();
            FWPM_NET_EVENT_SUBSCRIPTION0_ subscription = new FWPM_NET_EVENT_SUBSCRIPTION0_();
            subscription.enumTemplate = enumTemplate;
            subscription.sessionKey = session_key; // GetSessionKey();

            IntPtr subscriptionHandle = IntPtr.Zero;

            monitor_callback = new FWPM_NET_EVENT_CALLBACK4_(EventCallback);

            // Subscribe to network events
            uint result = FwpmNetEventSubscribe4(
                handleManager.engineHandle, //GetEngineHandle(),
                ref subscription,
                monitor_callback,
                IntPtr.Zero, // No context data in this case
                ref subscriptionHandle);

            if (result != 0)
            {
                Console.WriteLine($"Error subscribing to network events: {result}");
                return IntPtr.Zero;
            }

            Console.WriteLine("Successfully subscribed to network events.");
            return subscriptionHandle;
        }


        public void StopMonitor(IntPtr subscriptionHandle)
        {
            uint result = FwpmNetEventUnsubscribe0(
                handleManager.engineHandle, //GetEngineHandle(),
                subscriptionHandle);

            if (result != 0)
            {
                Console.WriteLine($"Error unsubscribing from network events: {result}");
                return;
            }

            Console.WriteLine("Successfully unsubscribed from network events.");
        }

        public void EventCallback(IntPtr context, FWPM_NET_EVENT5_ eventData)
        {
            object data = eventData.GetEventData();
            //var filters = GetFilters().ToList();
            //{ NativeAPI.WfpNativeAPI.FWPM_NET_EVENT_CLASSIFY_DROP2}
            var filter = GetFilterById((uint)Convert.ToInt32(data.GetType().GetField("FilterId").GetValue(data)));//filters.Where(item => (int)item.filterId == Convert.ToInt32(data.GetType().GetField("FilterId").GetValue(data))).First();
            //var providers = GetProviders().ToList();
            //var sublayers = GetSubLayers().ToList();
            //var callouts = GetCallouts().ToList();


            //try 
            //{
            //    var ir = data.GetType().GetField("IfLuid").GetValue(data);
            //    wfpClient.GetNetworkInterfaceByLuid((ulong)ir);
            //}
            //catch { }
            try
            {
                if ((log_filter.id_filter.Count == 0 && log_filter.name_filter.Count == 0 && log_filter.hosts.Count == 0 && log_filter.ports.Count == 0)
                    || log_filter.id_filter.Contains(Convert.ToInt32(data.GetType().GetField("FilterId").GetValue(data)))
                    || (log_filter.name_filter.Count > 0 && (log_filter.name_filter.Where(item => filter.displayData.name.Contains(item)).Count() == 0))
                    || (log_filter.id_filter.Count != 0 && !(log_filter.id_filter.Contains(-1 * Convert.ToInt32(data.GetType().GetField("FilterId").GetValue(data)))))
                    || (log_filter.hosts.Count != 0 && (log_filter.hosts.Where(item => item.Equals(eventData.header.GetLocalAddr()) || item.Equals(eventData.header.GetRemoteAddr())).Count() != 0))
                    || (log_filter.ports.Count != 0 && (log_filter.ports.Where(item => item == eventData.header.localPort || item == eventData.header.remotePort).Count() != 0))
                    )
                {



                    Console.WriteLine($"{eventData.header.timeStamp.ToDateTime()} action {eventData.type} direction {data.GetType().GetField("MsFwpDirection").GetValue(data)} local.ip {eventData.header.GetLocalAddr()} local.port {eventData.header.localPort} remote.ip {eventData.header.GetRemoteAddr()} remote.port {eventData.header.remotePort}");
                    Console.WriteLine($"\taction: {filter.action.type}");
                    Console.WriteLine($"\tfilter:");
                    Console.WriteLine($"\t\tguid: {filter.filterKey}\n\t\tid: {Convert.ToInt32(data.GetType().GetField("FilterId").GetValue(data))}\n\t\tname: {filter.displayData.name}");
                    if (filter.providerKey != IntPtr.Zero) // && (providers.Where(item => item.providerKey == Marshal.PtrToStructure<Guid>(filter.providerKey)).Count() > 0))
                    {
                        var provider = GetProviderByKey(Marshal.PtrToStructure<Guid>(filter.providerKey)); // providers.Where(item => item.providerKey == Marshal.PtrToStructure<Guid>(filter.providerKey)).First();
                        Console.WriteLine($"\tprovider");
                        Console.WriteLine($"\t\tguid: {provider.providerKey}\n\t\tname: {provider.displayData.name}");
                    }

                    if (filter.subLayerKey != Guid.Empty ) //&& sublayers.Where(item => item.subLayerKey == filter.subLayerKey).Count() > 0)
                    {
                        var sublayer = GetSubLayerByKey(filter.subLayerKey);//sublayers.Where(item => item.subLayerKey == filter.subLayerKey).First();
                        Console.WriteLine($"\tsublayer");
                        Console.WriteLine($"\t\tguid: {sublayer.subLayerKey}\n\t\tname: {sublayer.displayData.name}");
                    }

                    // if filterd by callout
                    if ((filter.action.type & FWP_ACTION_TYPE_.FWP_ACTION_FLAG_CALLOUT) == FWP_ACTION_TYPE_.FWP_ACTION_FLAG_CALLOUT)
                    {
                        //if (callouts.Where(item => item.calloutKey == filter.action.CalloutKey).Count() > 0)
                        //{
                        var callout = GetCalloutByKey(filter.action.CalloutKey); //callouts.Where(item => item.calloutKey == filter.action.CalloutKey).First();
                            Console.WriteLine($"\tcallout");
                            Console.WriteLine($"\t\tguid: {callout.calloutKey}\n\t\tname: {callout.displayData.name}");
                        //}
                    }



                }

                

            //if (log_id.Contains(-1 * Convert.ToInt32(data.GetType().GetField("FilterId").GetValue(data))))
            //{
            //    // negative filter
            //    IntPtr ptr = handleManager.GetEntries<FWPM_FILTER0_>();
            //    FwpmFreeMemory0(ref ptr);
            //    ptr = handleManager.GetEntries<FWPM_PROVIDER0_>();
            //    FwpmFreeMemory0(ref ptr);
            //    return;
            //}
            //else
            //{
            //    Console.WriteLine($"{eventData.header.timeStamp.ToDateTime()} action {eventData.type} local.ip {eventData.header.GetLocalAddr()} local.port {eventData.header.localPort} remote.ip {eventData.header.GetRemoteAddr()} remote.port {eventData.header.remotePort}");
            //    Console.WriteLine($"\trule_id: {Convert.ToInt32(data.GetType().GetField("FilterId").GetValue(data))}\n\trule_name: {filter.displayData.name}\n\trule_action: {filter.action.type}");
            //    Console.WriteLine($"\tprovider_id: {provider.providerKey}\n\tprovider_name: {provider.displayData.name}");
            //}

                    // Close Enum handles
                    //DestroyEnumHandle<FWPM_FILTER0_>(handleManager.engineHandle, handleManager.GetHandle<FWPM_FILTER0_>());
                    //DestroyEnumHandle<FWPM_PROVIDER0_>(handleManager.engineHandle, handleManager.GetHandle<FWPM_PROVIDER0_>());

                    //Console.WriteLine("\n");
                    // Process the event data as needed
                    //IntPtr ptr1 = handleManager.GetEntries<FWPM_FILTER0_>();
                    //FwpmFreeMemory0(ref ptr1);
                    //ptr1 = handleManager.GetEntries<FWPM_PROVIDER0_>();
                    //FwpmFreeMemory0(ref ptr1);
            }
            catch
            {
                Console.WriteLine("ERROR");
            }
        }
    }
}
