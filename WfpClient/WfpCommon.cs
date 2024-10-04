using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static NativeAPI.WfpNativeAPI;
using Win32Helper;
using System.Reflection;
using static Wfp.WfpClient;


namespace Wfp
{
    public partial class WfpClient
    {


        private class WfpEngineObject
        {
            public IntPtr handle;
            public IntPtr entries;
            public DateTime entries_timestamp;
            public uint numEntriesReturned;
            public IntPtr subscription_changes;
                    
        }

        private class WfpEngineHandles
        {
            //public IntPtr _engineHandle;
            //private IntPtr _sessionEnumHandle;
            //private IntPtr _connectionEnumHandle;
            //private IntPtr _providerEnumHandle;
            //private IntPtr _sublayerEnumHandle;
            //private IntPtr _filterEnumHandle;
            //private IntPtr _calloutEnumHandle;

            //public IntPtr _sessionEnumEntries;
            //public IntPtr _connectionEnumEntries;
            //public IntPtr _providerEnumEntries;
            //public IntPtr _sublayerEnumEntries;
            //public IntPtr _filterEnumEntries;
            //public IntPtr _calloutEnumEntries;

            public IntPtr _engineHandle;
            public WfpEngineObject sessionObj = new WfpEngineObject();
            public WfpEngineObject connectionObj = new WfpEngineObject();
            public WfpEngineObject providerObj = new WfpEngineObject();
            public WfpEngineObject sublayerObj = new WfpEngineObject();
            public WfpEngineObject filterObj = new WfpEngineObject();
            public WfpEngineObject calloutObj = new WfpEngineObject();

            private int expirationMilisecondsThreshold = 10000;

            private bool IsObjectExpired(WfpEngineObject obj)
            {
                DateTime currentTime = DateTime.Now;
                TimeSpan timeDifference = currentTime - obj.entries_timestamp;
                return timeDifference.TotalMilliseconds > expirationMilisecondsThreshold;
            }

            public IntPtr engineHandle
            {
                get => _engineHandle;
                set => _engineHandle = value;
            }

            public void SetEntries<T>(IntPtr value, uint numEntriesReturned)
            {
                if (typeof(T) == typeof(FWPM_PROVIDER0_))
                {
                    if (IsObjectExpired(providerObj))
                    {
                        FwpmFreeMemory0(ref providerObj.entries);
                        providerObj.entries = value;
                        providerObj.entries_timestamp = DateTime.Now;
                        providerObj.numEntriesReturned = numEntriesReturned;
                    }
                }
                else if (typeof(T) == typeof(FWPM_FILTER0_))
                {
                    if (IsObjectExpired(filterObj))
                    {
                        FwpmFreeMemory0(ref filterObj.entries);
                        filterObj.entries = value;
                        filterObj.entries_timestamp = DateTime.Now;
                        filterObj.numEntriesReturned = numEntriesReturned;
                    }
                }
                else if (typeof(T) == typeof(FWPM_SUBLAYER0_))
                {
                    if (IsObjectExpired(sublayerObj))
                    {
                        FwpmFreeMemory0(ref sublayerObj.entries);
                        sublayerObj.entries = value;
                        sublayerObj.entries_timestamp = DateTime.Now;
                        sublayerObj.numEntriesReturned = numEntriesReturned;
                    }
                }
                else if (typeof(T) == typeof(FWPM_CALLOUT0_))
                {
                    if (IsObjectExpired(calloutObj))
                    {
                        FwpmFreeMemory0(ref calloutObj.entries);
                        calloutObj.entries = value;
                        calloutObj.entries_timestamp = DateTime.Now;
                        calloutObj.numEntriesReturned = numEntriesReturned;
                    }
                }
                else if (typeof(T) == typeof(FWPM_SESSION0_))
                {
                    if (IsObjectExpired(sessionObj))
                    {
                        FwpmFreeMemory0(ref sessionObj.entries);
                        sessionObj.entries = value;
                        sessionObj.entries_timestamp = DateTime.Now;
                        sessionObj.numEntriesReturned = numEntriesReturned;
                    }
                }
                else if (typeof(T) == typeof(FWPM_CONNECTION0_))
                {
                    if (IsObjectExpired(connectionObj))
                    {
                        FwpmFreeMemory0(ref connectionObj.entries);
                        connectionObj.entries = value;
                        connectionObj.entries_timestamp = DateTime.Now;
                        connectionObj.numEntriesReturned = numEntriesReturned;
                    }
                }
                else

                    throw new ArgumentException($"Unsupported type: {typeof(T).Name}");

            }

            public (IntPtr, uint) GetEntries<T>()
            {
                IntPtr intPtr = IntPtr.Zero;
                uint numEntriesReturned = 0;
                if (typeof(T) == typeof(FWPM_PROVIDER0_) && (!IsObjectExpired(providerObj)))
                {
                    intPtr = providerObj.entries;
                    numEntriesReturned = providerObj.numEntriesReturned;
                }
                else if (typeof(T) == typeof(FWPM_FILTER0_) && (!IsObjectExpired(filterObj)))
                {
                    intPtr = filterObj.entries;
                    numEntriesReturned = filterObj.numEntriesReturned;
                }
                else if (typeof(T) == typeof(FWPM_SUBLAYER0_) && (!IsObjectExpired(sublayerObj)))
                {
                    intPtr = sublayerObj.entries;
                    numEntriesReturned = sublayerObj.numEntriesReturned;
                }
                else if (typeof(T) == typeof(FWPM_CALLOUT0_) && (!IsObjectExpired(calloutObj)))
                {
                    intPtr = calloutObj.entries;
                    numEntriesReturned = calloutObj.numEntriesReturned;
                }
                else if (typeof(T) == typeof(FWPM_SESSION0_) & (!IsObjectExpired(sessionObj)))
                {
                    intPtr = sessionObj.entries;
                    numEntriesReturned = sessionObj.numEntriesReturned;
                }
                else if (typeof(T) == typeof(FWPM_CONNECTION0_) && (!IsObjectExpired(connectionObj)))
                {
                    intPtr = connectionObj.entries;
                    numEntriesReturned = connectionObj.numEntriesReturned;
                }
                //else
                //    throw new ArgumentException($"Unsupported type: {typeof(T).Name}");

                return (intPtr, numEntriesReturned);
            }

            public IntPtr GetHandle<T>()
            {
                IntPtr intPtr = IntPtr.Zero;
                if (typeof(T) == typeof(FWPM_PROVIDER0_))

                    intPtr = providerObj.handle;

                else if (typeof(T) == typeof(FWPM_FILTER0_))

                    intPtr = filterObj.handle;

                else if (typeof(T) == typeof(FWPM_SUBLAYER0_))

                    intPtr = sublayerObj.handle;

                else if (typeof(T) == typeof(FWPM_CALLOUT0_))

                    intPtr = calloutObj.handle;

                else if (typeof(T) == typeof(FWPM_SESSION0_))

                    intPtr = sessionObj.handle;

                else if (typeof(T) == typeof(FWPM_CONNECTION0_))

                    intPtr = connectionObj.handle;
                else

                    throw new ArgumentException($"Unsupported type: {typeof(T).Name}");

                if (intPtr == IntPtr.Zero)
                    CreateEnumHandle<T>(_engineHandle, IntPtr.Zero, ref intPtr);

                return intPtr;
            }
        }


        private void DeleteItem<T>(Guid key)
        {
            uint code;
            IntPtr guidPtr = Marshal.AllocHGlobal(Marshal.SizeOf(key));
            Marshal.Copy(key.ToByteArray(), 0, guidPtr, 16);

            if (typeof(T) == typeof(FWPM_PROVIDER0_))
            {
                code = FwpmProviderDeleteByKey0(handleManager.engineHandle, guidPtr);
                if (code != 0 && code != (uint)FWP_E.PROVIDER_NOT_FOUND)
                    throw new NativeException(nameof(FwpmProviderDeleteByKey0), code);
            }
            else if (typeof(T) == typeof(FWPM_FILTER0_))
            {
                code = FwpmFilterDeleteByKey0(handleManager.engineHandle, guidPtr);
                if (code != 0 && code != (uint)FWP_E.FILTER_NOT_FOUND)
                    throw new NativeException(nameof(FwpmFilterDeleteByKey0), code);
            }
            else if (typeof(T) == typeof(FWPM_SUBLAYER0_))
            {
                code = FwpmSubLayerDeleteByKey0(handleManager.engineHandle,  guidPtr);
                if (code != 0 && code != (uint)FWP_E.SUBLAYER_NOT_FOUND)
                    throw new NativeException(nameof(FwpmSubLayerDeleteByKey0), code);
            }
            else if (typeof(T) == typeof(FWPM_CALLOUT0_))
            {
                code = FwpmCalloutDeleteByKey0(handleManager.engineHandle, guidPtr);
                if (code != 0 && code != (uint)FWP_E.CALLOUT_NOT_FOUND)
                    throw new NativeException(nameof(FwpmCalloutDeleteByKey0), code);
            }
            else
            {
                throw new ArgumentException($"Unsupported type: {typeof(T).Name}");
            }

        }


        private void DeleteItem<T>(IEnumerable<T> items)
        {
            foreach (var item in items)
            {

                var providerKeyField = typeof(T).GetProperty("providerKey");
                if (providerKeyField != null)
                {
                    try
                    {
                        DeleteItem<T>((Guid)providerKeyField.GetValue(item));
                    }
                    catch (Exception ex)
                    {
                        // pass
                    }
                }

            }

        }

        private static void CreateEnumHandle<T>(
            IntPtr engineHandle,
            IntPtr enumTemplate,
            ref System.IntPtr enumHandle)
        {
            uint code;
            var callable = FwpmProviderCreateEnumHandle0; // default

            if (typeof(T) == typeof(FWPM_PROVIDER0_))
            {
                callable = FwpmProviderCreateEnumHandle0;
            }
            else if (typeof(T) == typeof(FWPM_FILTER0_))
            {
                callable = FwpmFilterCreateEnumHandle0;
            }
            else if (typeof(T) == typeof(FWPM_SUBLAYER0_))
            {
                callable = FwpmSubLayerCreateEnumHandle0;
            }
            else if (typeof(T) == typeof(FWPM_CALLOUT0_))
            {
                callable = FwpmCalloutCreateEnumHandle0;
            }
            else if (typeof(T) == typeof(FWPM_SESSION0_))
            {
                callable = FwpmSessionCreateEnumHandle0;
            }
            else if (typeof(T) == typeof(FWPM_CONNECTION0_))
            {
                callable = FwpmConnectionCreateEnumHandle0;
            }
            //else if (typeof(T) == typeof(FWPM_CONNECTION0_))
            //    callable = FwpmConnectionCreateEnumHandle0;
            else
            {
                throw new ArgumentException($"Unsupported type: {typeof(T).Name}");
            }

            code = callable(engineHandle, IntPtr.Zero, ref enumHandle);
            if (code != 0)
                throw new NativeException(callable.Method.Name, code);
        }


        private void FwpmEnumItem<T>(IntPtr engineHandle,
            IntPtr enumHandle,
            uint numEntriesRequested,
            ref IntPtr entries,
            ref uint numEntriesReturned)
        {
            uint code;
            var callable = FwpmProviderEnum0; // default

            if (typeof(T) == typeof(FWPM_PROVIDER0_))

                callable = FwpmProviderEnum0;

            else if (typeof(T) == typeof(FWPM_FILTER0_))

                callable = FwpmFilterEnum0;

            else if (typeof(T) == typeof(FWPM_SUBLAYER0_))

                callable = FwpmSubLayerEnum0;

            else if (typeof(T) == typeof(FWPM_CALLOUT0_))

                callable = FwpmCalloutEnum0;

            else if (typeof(T) == typeof(FWPM_SESSION0_))

                callable = FwpmSessionEnum0;

            else if (typeof(T) == typeof(FWPM_CONNECTION0_))

                callable = FwpmConnectionEnum0;

            else

                throw new ArgumentException($"Unsupported type: {typeof(T).Name}");


            code = callable(engineHandle, enumHandle, numEntriesRequested, ref entries, ref numEntriesReturned);

            if (code != 0)
                throw new NativeException(callable.Method.Name, code);
        }


        private void DestroyEnumHandle<T>(IntPtr engineHandle, IntPtr enumHandle)
        {
            uint code;
            var callable = FwpmProviderDestroyEnumHandle0; // default

            if (typeof(T) == typeof(FWPM_PROVIDER0_))

                callable = FwpmProviderDestroyEnumHandle0;

            else if (typeof(T) == typeof(FWPM_FILTER0_))

                callable = FwpmFilterDestroyEnumHandle0;

            else if (typeof(T) == typeof(FWPM_SUBLAYER0_))

                callable = FwpmSubLayerDestroyEnumHandle0;

            else if (typeof(T) == typeof(FWPM_CALLOUT0_))

                callable = FwpmCalloutDestroyEnumHandle0;

            else if (typeof(T) == typeof(FWPM_SESSION0_))

                callable = FwpmSessionDestroyEnumHandle0;

            else if (typeof(T) == typeof(FWPM_CONNECTION0_))

                callable = FwpmConnectionDestroyEnumHandle0;
            else

                throw new ArgumentException($"Unsupported type: {typeof(T).Name}");


            code = callable(engineHandle, enumHandle);
            if (code.Equals((uint)FWP_EXCEPTION_.FWP_E_SESSION_ABORTED))
            {
                // reconnect if suddenly lost connection (works for FilterEnum)
                code = this.Connect();
            }
            if (code != 0)
                throw new NativeException(callable.Method.Name, code);
        }

        private T GetItemById<T>(uint id)
        {
            uint code;
            IntPtr out_struct;
            var callable = FwpmFilterGetById0; // default

            if (typeof(T) == typeof(FWPM_FILTER0_))

                callable = FwpmFilterGetById0;

            else if (typeof(T) == typeof(FWPM_CALLOUT0_))

                callable = FwpmCalloutGetById0;

            else

                throw new ArgumentException($"Unsupported type: {typeof(T).Name}");

            code = callable(handleManager.engineHandle, id, out out_struct);

            if (code != 0) // Check for error
            {
                throw new ArgumentException($"Failed to get item by key: {typeof(T).Name}");
            }
            return Marshal.PtrToStructure<T>(out_struct);
        }

        private T GetItemByKey<T>(Guid item_guid)
        {
            uint code;
            IntPtr out_struct;
            var callable = FwpmProviderGetByKey0; // default

            if (typeof(T) == typeof(FWPM_PROVIDER0_))

                callable = FwpmProviderGetByKey0;

            else if (typeof(T) == typeof(FWPM_FILTER0_))

                callable = FwpmFilterGetByKey0;

            else if (typeof(T) == typeof(FWPM_SUBLAYER0_))

                callable = FwpmSubLayerGetByKey0;

            else if (typeof(T) == typeof(FWPM_CALLOUT0_))

                callable = FwpmCalloutGetByKey0;

            else

                throw new ArgumentException($"Unsupported type: {typeof(T).Name}");
            
            code = callable(handleManager.engineHandle, ref item_guid, out out_struct);

            if (code != 0) // Check for error
            {
                throw new ArgumentException($"Failed to get item by key: {typeof(T).Name}");
            }
            return Marshal.PtrToStructure<T>(out_struct);
        }


        private IEnumerable<T> GetItems<T>(string nameFilter) where T : struct
        {
            IntPtr enumHandle = IntPtr.Zero;
            IntPtr entries = IntPtr.Zero;
            uint numEntriesReturned = 0;

            try
            {
                // Create an enumeration handle for the specific type
                //CreateEnumHandle<T>(engineHandle, IntPtr.Zero, ref enumHandle);
                enumHandle = handleManager.GetHandle<T>();

                // Enumerate items of specific type
                (entries, numEntriesReturned) = handleManager.GetEntries<T>();
                if (entries == IntPtr.Zero)
                {
                    FwpmEnumItem<T>(handleManager.engineHandle, enumHandle, uint.MaxValue, ref entries, ref numEntriesReturned);
                    handleManager.SetEntries<T>(entries, numEntriesReturned);
                }

                var itmeSize = Marshal.SizeOf<IntPtr>();//Marshal.SizeOf<T>();
                for (uint i = 0; i < numEntriesReturned; i++)
                {
                    var ptr = new IntPtr(entries.ToInt64() + i * itmeSize);
                    var ptr2 = Marshal.PtrToStructure<IntPtr>(ptr);
                    var item = Marshal.PtrToStructure<T>(ptr2);

                    // Implement filtering ?

                    if (typeof(T) == typeof(FWPM_PROVIDER0_))
                    {
                        if (log_filter.providerKey != Guid.Empty && log_filter.providerKey != (Guid)item.GetType().GetField("providerKey").GetValue(item))
                            continue;


                        //var displayData = item.GetType().GetField("displayData").GetValue(item);
                        //string name = (string)displayData.GetType().GetField("name").GetValue(displayData);
                        //if (log_filter.name_filter.Count > 0 && log_filter.name_filter.Where(item => name.Contains(item)).Count() == 0)
                        //{
                        //    continue;
                        //}
                    }
                    else if (typeof(T) == typeof(FWPM_SESSION0_))
                    {
                        //Do Nothing
                    }
                    else
                    {
                        if ((IntPtr)item.GetType().GetField("providerKey").GetValue(item) != IntPtr.Zero && log_filter.providerKey != Guid.Empty)
                        {
                            if (log_filter.providerKey != Marshal.PtrToStructure<Guid>((IntPtr)item.GetType().GetField("providerKey").GetValue(item)))
                            {
                                continue;
                            }

                        }
                    }
                    
                        var displayData = item.GetType().GetField("displayData").GetValue(item);
                        string object_name = (string)displayData.GetType().GetField("name").GetValue(displayData);
                         
                        if (object_name != null && log_filter.name_filter.Count > 0 && log_filter.name_filter.Where(item => object_name.Contains(item)).Count() == 0)
                        {
                            continue;
                        }
                    

                    //FieldInfo itemField = item.GetType().GetField("providerKey");
                    //if (itemField == null)
                    //    throw new InvalidOperationException($"Type {typeof(T).Name} does not have a 'providerKey' field.");

                    //if (typeof(T) == typeof(FWPM_PROVIDER0_))
                    //{
                    //    var filterProviderKey = (Guid)itemField.GetValue(item);
                    //    if (filterProviderKey == Guid.Empty)
                    //        continue;
                    //} else
                    //{
                    //    var filterProviderKey = (IntPtr)itemField.GetValue(item);
                    //    if (filterProviderKey == IntPtr.Zero) 
                    //        continue;

                    //}

                    // filter by nameFilter
                    //filterProviderKey != this.providerKey

                    yield return item;
                }
            }
            finally
            {
                //if (entries != IntPtr.Zero)
                //    FwpmFreeMemory0(ref entries);

                //if (enumHandle != IntPtr.Zero)
                //{
                //    DestroyEnumHandle<T>(engineHandle, enumHandle);

                //}
            }
        }


        private IEnumerable<T> Filter<T>(IEnumerable<T> items, Func<T, bool> predicate)
        {
            return items.Where(predicate);
        }


        private FirewallObjectSecurityInfo GetSecurityInfoByKey<T>(Guid item_guid)
        
        {
            FirewallObjectSecurityInfo sec_info = new FirewallObjectSecurityInfo();

            uint code = 0;
            IntPtr out_struct;
            var callable = FwpmProviderGetSecurityInfoByKey0; // default

            if (typeof(T) == typeof(FWPM_PROVIDER0_))

                callable = FwpmProviderGetSecurityInfoByKey0;

            else if (typeof(T) == typeof(FWPM_FILTER0_))

                callable = FwpmFilterGetSecurityInfoByKey0;

            else if (typeof(T) == typeof(FWPM_SUBLAYER0_))

                callable = FwpmSubLayerGetSecurityInfoByKey0;

            else if (typeof(T) == typeof(FWPM_CALLOUT0_))

                callable = FwpmCalloutGetSecurityInfoByKey0;

            else if (typeof(T) == typeof(FWPM_CONNECTION0_))

                code = FwpmConnectionGetSecurityInfo0(handleManager.engineHandle,
                    SECURITY_INFORMATION.Dacl | SECURITY_INFORMATION.Owner | SECURITY_INFORMATION.Group, // SECURITY_INFORMATION.Sacl gives error ERROR_PRIVILEGE_NOT_HELD 1314 (0x522)
                    out sec_info.sidOwner,
                    out sec_info.sidGroup,
                    out sec_info.dacl,
                    out sec_info.sacl,
                    out sec_info.securityDescriptor);

            else

                throw new ArgumentException($"Unsupported type: {typeof(T).Name}");

            if (typeof(T) != typeof(FWPM_CONNECTION0_))
                code = callable(handleManager.engineHandle,
                    ref item_guid,
                    SECURITY_INFORMATION.Dacl | SECURITY_INFORMATION.Owner  | SECURITY_INFORMATION.Group, // SECURITY_INFORMATION.Sacl gives error ERROR_PRIVILEGE_NOT_HELD 1314 (0x522)
                    out sec_info.sidOwner,
                    out sec_info.sidGroup,
                    out sec_info.dacl,
                    out sec_info.sacl,
                    out sec_info.securityDescriptor);

            if (code != 0) // Check for error
            {
                throw new ArgumentException($"Failed to get item by key: {typeof(T).Name}");
            }
            
            return sec_info;
        }


        //public void SubscribeAllChanges()
        //{
        //    SubscribeProviderChanges();
        //    SubscribeFilterChanges();
        //    SubscribeCalloutChanges();
        //    SubscribeConnectionChanges();
        //    SubscribeSublayerChanges();                    
        //}

        public void UnsubscribeAllChanges()
        {
            UnsubscribeProviderChanges();
            UnsubscribeFilterChanges();
            UnsubscribeCalloutChanges();
            UnsubscribeConnectionChanges();
            UnsubscribeSublayerChanges();
        }

        public void Unsibscribe<T>(IntPtr subscriptionHandle)
        {
            uint code;
            IntPtr out_struct;
            var callable = FwpmProviderUnsubscribeChanges0; // default

            if (typeof(T) == typeof(FWPM_PROVIDER0_))

                callable = FwpmProviderUnsubscribeChanges0;

            else if (typeof(T) == typeof(FWPM_FILTER0_))

                callable = FwpmFilterUnsubscribeChanges0;

            else if (typeof(T) == typeof(FWPM_SUBLAYER0_))

                callable = FwpmSubLayerUnsubscribeChanges0;

            else if (typeof(T) == typeof(FWPM_CALLOUT0_))

                callable = FwpmCalloutUnsubscribeChanges0;

            else if (typeof(T) == typeof(FWPM_CONNECTION0_))

                callable = FwpmConnectionUnsubscribe0;

            else

                throw new ArgumentException($"Unsupported type: {typeof(T).Name}");

            code = callable(handleManager.engineHandle, subscriptionHandle);

            if (code != 0) // Check for error
            {
                throw new ArgumentException($"Failed to unsubscribe change events: {typeof(T).Name}");
            }

            Console.WriteLine($"Unsubscribed changes successfully {typeof(T).Name}");
            
        }

        private IEnumerable<FWPM_SESSION0_> GetSubscribtions<T>()
        {
            IntPtr entries = IntPtr.Zero;
            uint numEntries = 0;
            try
            {
                var callable = FwpmProviderSubscriptionsGet0; // default

                if (typeof(T) == typeof(FWPM_PROVIDER0_))

                    callable = FwpmProviderSubscriptionsGet0;

                else if (typeof(T) == typeof(FWPM_FILTER0_))

                    callable = FwpmFilterSubscriptionsGet0;

                else if (typeof(T) == typeof(FWPM_SUBLAYER0_))

                    callable = FwpmSubLayerSubscriptionsGet0;

                else if (typeof(T) == typeof(FWPM_CALLOUT0_))

                    callable = FwpmCalloutSubscriptionsGet0;

                //else if (typeof(T) == typeof(FWPM_CONNECTION0_))

                //    callable = FwpmConnectionSubscriptionsGet0;

                else

                    throw new ArgumentException($"Unsupported type: {typeof(T).Name}");

                uint code = callable(
                    handleManager.engineHandle,
                    out entries,
                    out numEntries);

                var itmeSize = Marshal.SizeOf<IntPtr>();//Marshal.SizeOf<T>();
                for (uint i = 0; i < numEntries; i++)
                {
                    var ptr = new IntPtr(entries.ToInt64() + i * itmeSize);
                    var ptr2 = Marshal.PtrToStructure<IntPtr>(ptr);
                    var item = Marshal.PtrToStructure<FWPM_FILTER_SUBSCRIPTION0_>(ptr2);

                    var session = item.sessionKey;
                    //var enumTemplate = Marshal.PtrToStructure<FWPM_FILTER_ENUM_TEMPLATE0_>((IntPtr)item.GetType().GetField("enumTemplate").GetValue(item));

                    if (session == Guid.Empty || GetSessions().Where(item => item.sessionKey == session).Count() == 0)
                    {
                        Console.WriteLine($"Got subscriber '{session}' for type `{typeof(T).Name}' but not found in sessions");
                        continue;
                    }

                    yield return GetSessions().Where(item => item.sessionKey == session).First();

                }
            }
            finally
            {
                if (entries != IntPtr.Zero)
                {
                    FwpmFreeMemory0(ref entries);
                }
            }

        }

    }
}
