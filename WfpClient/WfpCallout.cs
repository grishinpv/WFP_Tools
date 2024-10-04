using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static NativeAPI.WfpNativeAPI;

namespace Wfp
{
    public partial class WfpClient
    {

        public IEnumerable<FWPM_CALLOUT0_> GetCallouts(string nameFilter = "")
        {
            return GetItems<FWPM_CALLOUT0_>(nameFilter);
        }

        public FWPM_CALLOUT0_ GetCalloutByKey(Guid item_guid)
        {
            return GetItemByKey<FWPM_CALLOUT0_>(item_guid);
        }

        public FWPM_CALLOUT0_ GetCalloutById(uint item_id)
        {
            return GetItemById<FWPM_CALLOUT0_>(item_id);
        }

        public void DeleteCallout(Guid key)
        {
            DeleteItem<FWPM_CALLOUT0_>(key);
        }

        public FirewallObjectSecurityInfo GetCalloutSecurityInfo(Guid item_guid)
        {
            return GetSecurityInfoByKey<FWPM_CALLOUT0_>(item_guid);
        }

        public void SubscribeCalloutChanges(FWPM_CALLOUT_CHANGE_CALLBACK0_ callback)
        {
            uint code;
            IntPtr subscriptionHandle = IntPtr.Zero;

            FWPM_CALLOUT_ENUM_TEMPLATE0_ enumTemplate = new FWPM_CALLOUT_ENUM_TEMPLATE0_();
            FWPM_CALLOUT_SUBSCRIPTION0_ subscription = new FWPM_CALLOUT_SUBSCRIPTION0_
            {
                enumTemplate = IntPtr.Zero, //enumTemplate,
                sessionKey = session_key != IntPtr.Zero ? Marshal.PtrToStructure<Guid>(session_key) : Guid.Empty,
                flags = FirewallSubscriptionFlags.FWPM_SUBSCRIPTION_FLAG_NOTIFY_ON_DELETE | FirewallSubscriptionFlags.FWPM_SUBSCRIPTION_FLAG_NOTIFY_ON_ADD
            };

            code = FwpmCalloutSubscribeChanges0(
                handleManager.engineHandle, //GetEngineHandle(),
                ref subscription,
                callback,
                IntPtr.Zero, // No context data in this case
                out subscriptionHandle);

            if (code != 0)
            {
                Console.WriteLine($"Error subscribing CALLOUT change events: {code}");
                return;
            }

            Console.WriteLine("Successfully subscribed to CALLOUT change events");
            handleManager.calloutObj.subscription_changes = subscriptionHandle;

        }

        public void UnsubscribeCalloutChanges()
        {
            Unsibscribe<FWPM_CALLOUT0_>(handleManager.calloutObj.subscription_changes);
        }

        public IEnumerable<FWPM_SESSION0_> GetCalloutSubscribtions()
        {
            return GetSubscribtions<FWPM_CALLOUT0_>();
        }
    }
}
