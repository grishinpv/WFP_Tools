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
        public IEnumerable<FWPM_PROVIDER0_> GetProviders(string nameFilter = "")
        {
            return GetItems<FWPM_PROVIDER0_>(nameFilter);
        }

        public FWPM_PROVIDER0_ GetProviderByKey(Guid item_guid)
        {
            return GetItemByKey<FWPM_PROVIDER0_>(item_guid);
        }

        public void DeleteProvider(Guid key)
        {
            DeleteItem<FWPM_PROVIDER0_>(key);
        }

        public FirewallObjectSecurityInfo GetProviderSecurityInfo(Guid item_guid)
        {
            return GetSecurityInfoByKey<FWPM_PROVIDER0_>(item_guid);
        }

        public void SubscribeProviderChanges(FWPM_PROVIDER_CHANGE_CALLBACK0_ callback)
        {
            uint code;
            IntPtr subscriptionHandle = IntPtr.Zero;

            FWPM_PROVIDER_ENUM_TEMPLATE0_ enumTemplate = new FWPM_PROVIDER_ENUM_TEMPLATE0_();
            FWPM_PROVIDER_SUBSCRIPTION0_ subscription = new FWPM_PROVIDER_SUBSCRIPTION0_
            {
                enumTemplate = enumTemplate,
                sessionKey = session_key != IntPtr.Zero ? Marshal.PtrToStructure<Guid>(session_key) : Guid.Empty,
                flags = FirewallSubscriptionFlags.FWPM_SUBSCRIPTION_FLAG_NOTIFY_ON_ADD | FirewallSubscriptionFlags.FWPM_SUBSCRIPTION_FLAG_NOTIFY_ON_DELETE
            };

            code = FwpmProviderSubscribeChanges0(
                handleManager.engineHandle, //GetEngineHandle(),
                ref subscription,
                callback,
                IntPtr.Zero, // No context data in this case
                out subscriptionHandle);

            if (code != 0)
            {
                Console.WriteLine($"Error subscribing PROVIDER change events: {code}");
                return;
            }

            Console.WriteLine("Successfully subscribed to PROVIDER change events");
            handleManager.providerObj.subscription_changes = subscriptionHandle;

        }

        public void UnsubscribeProviderChanges()
        {
            Unsibscribe<FWPM_PROVIDER0_>(handleManager.providerObj.subscription_changes);
        }

        public IEnumerable<FWPM_SESSION0_> GetProviderSubscribtions()
        {
            return GetSubscribtions<FWPM_PROVIDER0_>();
        }

    }
}
