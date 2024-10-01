using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static NativeAPI.WfpNativeAPI;

namespace Wfp
{
    public partial class WfpClient
    {

        public IEnumerable<FWPM_SUBLAYER0_> GetSubLayers(string nameFilter = "")
        {
            return GetItems<FWPM_SUBLAYER0_>(nameFilter);
        }

        public FWPM_SUBLAYER0_ GetSubLayerByKey(Guid item_guid)
        {
            return GetItemByKey<FWPM_SUBLAYER0_>(item_guid);
        }

        public void DeleteSubLayer(Guid key)
        {
            DeleteItem<FWPM_SUBLAYER0_>(key);
        }

        public FirewallObjectSecurityInfo GetSublayerSecurityInfo(Guid item_guid)
        {
            return GetSecurityInfoByKey<FWPM_SUBLAYER0_>(item_guid);
        }

        public void SubscribeSublayerChanges(FWPM_SUBLAYER_CHANGE_CALLBACK0_ callback)
        {
            uint code;
            IntPtr subscriptionHandle = IntPtr.Zero;

            FWPM_SUBLAYER_ENUM_TEMPLATE0_ enumTemplate = new FWPM_SUBLAYER_ENUM_TEMPLATE0_();
            FWPM_SUBLAYER_SUBSCRIPTION0_ subscription = new FWPM_SUBLAYER_SUBSCRIPTION0_
            {
                enumTemplate = enumTemplate,
                sessionKey = session_key,
                flags = FirewallSubscriptionFlags.FWPM_SUBSCRIPTION_FLAG_NOTIFY_ON_DELETE | FirewallSubscriptionFlags.FWPM_SUBSCRIPTION_FLAG_NOTIFY_ON_ADD
            };

            code = FwpmSubLayerSubscribeChanges0(
                handleManager.engineHandle, //GetEngineHandle(),
                ref subscription,
                callback,
                IntPtr.Zero, // No context data in this case
                out subscriptionHandle);

            if (code != 0)
            {
                Console.WriteLine($"Error subscribing SUBLAYER change events: {code}");
                return;
            }

            Console.WriteLine("Successfully subscribed to SUBLAYER change events");
            handleManager.sublayerObj.subscription_changes = subscriptionHandle;
        }

        public void UnsubscribeSublayerChanges()
        {
            Unsibscribe<FWPM_SUBLAYER0_>(handleManager.sublayerObj.subscription_changes);
        }


    }
}
