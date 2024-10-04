using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using WfpClient;
using Win32Helper;
using static NativeAPI.WfpNativeAPI;

namespace Wfp
{
    public partial class WfpClient
    {
        
        public IEnumerable<FWPM_FILTER0_> GetFilters(string nameFilter = "")
        {
            return GetItems<FWPM_FILTER0_>(nameFilter);
        }

        public FWPM_FILTER0_ GetFilterByKey(Guid item_guid)
        {
            return GetItemByKey<FWPM_FILTER0_>(item_guid);
        }

        public FWPM_FILTER0_ GetFilterById(uint item_id)
        {
            return GetItemById<FWPM_FILTER0_>(item_id);
        }

        public void DeleteFilter(Guid key)
        {
            DeleteItem<FWPM_FILTER0_>(key);
        }

        public void DeleteFilters(IEnumerable<FWPM_FILTER0_> items) 
        {
            DeleteItem(items);
        }

        public FirewallObjectSecurityInfo GetFilterSecurityInfo(Guid item_guid)
        {
            return GetSecurityInfoByKey<FWPM_FILTER0_>(item_guid);
        }

        public void SubscribeFilterChanges(FWPM_FILTER_CHANGE_CALLBACK0_ callback)
        {
            uint code;
            IntPtr subscriptionHandle = IntPtr.Zero;

            FWPM_FILTER_ENUM_TEMPLATE0_ enumTemplate = new FWPM_FILTER_ENUM_TEMPLATE0_();
            FWPM_FILTER_SUBSCRIPTION0_ subscription = new FWPM_FILTER_SUBSCRIPTION0_
            {
                enumTemplate = IntPtr.Zero, // enumTemplate,
                sessionKey = session_key != IntPtr.Zero ? Marshal.PtrToStructure<Guid>(session_key) : Guid.Empty,
                flags = FirewallSubscriptionFlags.FWPM_SUBSCRIPTION_FLAG_NOTIFY_ON_DELETE |
                        FirewallSubscriptionFlags.FWPM_SUBSCRIPTION_FLAG_NOTIFY_ON_ADD
            };

            code = FwpmFilterSubscribeChanges0(
                handleManager.engineHandle, //GetEngineHandle(),
                ref subscription,
                callback,
                IntPtr.Zero, // No context data in this case
                out subscriptionHandle);

            if (code != 0)
            {
                Console.WriteLine($"Error subscribing FILTER change events: {code}");
                return;
            }

            Console.WriteLine("Successfully subscribed to FILTER change events");
            handleManager.filterObj.subscription_changes = subscriptionHandle;

        }

        public void UnsubscribeFilterChanges()
        {
            Unsibscribe<FWPM_FILTER0_>(handleManager.filterObj.subscription_changes);
        }

        public IEnumerable<FWPM_SESSION0_> GetFilterSubscribtions()
        {
            return GetSubscribtions<FWPM_FILTER0_>();
        }


        private static T[] MarshalArray<T>(IntPtr pointer, int count)
        {
            if (pointer == IntPtr.Zero)
                return null;

            var offset = pointer;
            var data = new T[count];
            var type = typeof(T);
            if (type.IsEnum)
                type = type.GetEnumUnderlyingType();

            for (var i = 0; i < count; i++)
            {
                data[i] = (T)Marshal.PtrToStructure(offset, type);
                offset += Marshal.SizeOf(type);
            }

            return data;
        }


        public IEnumerable<FWPM_FILTER_CONDITION0_> GetFilterConditions(FWPM_FILTER0_ filter)
        {
            for (uint i = 0; i < filter.numFilterConditions; i++)
            {
                var itmeSize = Marshal.SizeOf<FWPM_FILTER_CONDITION0_>();

                var ptr = new IntPtr(filter.filterCondition.ToInt64() + i * itmeSize);
                var item = Marshal.PtrToStructure<FWPM_FILTER_CONDITION0_>(ptr);
                yield return item;
            }
        }

        public uint BlockOutgoingToRemoteHost(string name, string description, int dstAddressV4, Guid sublayer)
        {
            // TODO make FilterBuilder

            uint filterId = 0;
            uint code = 0;


            if (sublayer.Equals(Guid.Empty))
                throw new Exception("Sublayer can not be emty guid");


            WfpConditionBuilder condition = new WfpConditionBuilder();
            condition.IPRule(dstAddressV4, WfpConditionBuilder.TARGET.LOCAL);

            FWPM_FILTER0_ fwpFilter = new FWPM_FILTER0_();
            fwpFilter.layerKey = FWPM_LAYER_ALE_AUTH_CONNECT_V4;    //FWPM_LAYER_ALE_AUTH_RECV_ACCEPT_V4 - inbound
            fwpFilter.action.type = FWP_ACTION_TYPE_.FWP_ACTION_PERMIT;
            fwpFilter.subLayerKey = sublayer;
            fwpFilter.weight.type = FWP_DATA_TYPE_.FWP_EMPTY; // auto-weight.
            fwpFilter.numFilterConditions = condition.Length; // this applies to all application traffic
            fwpFilter.filterCondition = condition.ToPointer();
            fwpFilter.displayData.name = name;
            fwpFilter.displayData.description = description;


            code = FwpmFilterAdd0(handleManager.engineHandle, ref fwpFilter, IntPtr.Zero, ref filterId);
            if (code != 0)
            {
                throw new NativeException(nameof(FwpmFilterAdd0), code);
            }

            return filterId;
        }

    }
}
