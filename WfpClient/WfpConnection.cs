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
        public IEnumerable<FWPM_CONNECTION0_> GetConnections(string nameFilter = "")
        {
            return GetItems<FWPM_CONNECTION0_>(nameFilter);
        }

        public FirewallObjectSecurityInfo GetConnectionSecurityInfo(Guid item_guid)
        {
            return GetSecurityInfoByKey<FWPM_CONNECTION0_>(item_guid);
        }


        public void SubscribeConnectionChanges(FWPM_CONNECTION_CALLBACK0_ callback)
        {
            uint code;
            IntPtr subscriptionHandle = IntPtr.Zero;

            FWPM_CONNECTION_ENUM_TEMPLATE0_ enumTemplate = new FWPM_CONNECTION_ENUM_TEMPLATE0_();
            FWPM_CONNECTION_SUBSCRIPTION0_ subscription = new FWPM_CONNECTION_SUBSCRIPTION0_()
            {
                enumTemplate = enumTemplate,
                sessionKey = session_key != IntPtr.Zero ? Marshal.PtrToStructure<Guid>(session_key) : Guid.Empty,
            };

            code = FwpmConnectionSubscribe0(
                handleManager.engineHandle, //GetEngineHandle(),
                ref subscription,
                callback,
                IntPtr.Zero, // No context data in this case
                out subscriptionHandle);

            if (code != 0)
            {
                Console.WriteLine($"Error subscribing CONNECTION change events: {code}");
                return;
            }

            Console.WriteLine("Successfully subscribed to CONNECTION change events");
            handleManager.connectionObj.subscription_changes = subscriptionHandle;

        }

        public void UnsubscribeConnectionChanges()
        {
            Unsibscribe<FWPM_CONNECTION0_>(handleManager.connectionObj.subscription_changes);
        }

        public IEnumerable<FWPM_SESSION0_> GetConnectionSubscribtions()
        {
            return GetSubscribtions<FWPM_CONNECTION0_>();
        }

    }
}
