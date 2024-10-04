using System;
using System.Collections.Generic;
using System.Diagnostics;
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
                sessionKey = session_key != IntPtr.Zero ? Marshal.PtrToStructure<Guid>(session_key) : Guid.Empty,
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

        public IEnumerable<FWPM_SESSION0_> GetSublayerSubscribtions()
        {
            return GetSubscribtions<FWPM_SUBLAYER0_>();
        }


        public Guid CreateSublayer(
                Guid guid,
                string name = "WFP Tool Sublayer",
                string description = "WFP Toll Sublayer for test purpuses")
        {
            uint code;

            FWPM_SUBLAYER0_ fwpFilterSubLayer = new FWPM_SUBLAYER0_
            {
                subLayerKey = guid,  // my guid
                displayData = new FWPM_DISPLAY_DATA0_
                {
                    name = name,
                    description = description
                },
                flags = FWPM_SUBLAYER_FLAG_.NONE,
                weight = 0
            };

            if (GetSubLayers().Where(item => item.subLayerKey.Equals(fwpFilterSubLayer.subLayerKey)).Count() == 0)
            {
                code = FwpmSubLayerAdd0(handleManager.engineHandle, ref fwpFilterSubLayer, IntPtr.Zero);
                if (code != 0)
                {
                    throw new NativeException(nameof(FwpmFilterAdd0), code);
                }
            }

            return guid;
        }
        

    }
}
