using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
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
                sessionKey = session_key,
                flags = FirewallSubscriptionFlags.FWPM_SUBSCRIPTION_FLAG_NOTIFY_ON_DELETE | FirewallSubscriptionFlags.FWPM_SUBSCRIPTION_FLAG_NOTIFY_ON_ADD
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


    }
}
