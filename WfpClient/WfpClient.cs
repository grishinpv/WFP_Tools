using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static NativeAPI.WfpNativeAPI;
using static NativeAPI.IpHlpAPI;
using NativeAPI;

namespace Wfp
{
    public partial class WfpClient : IDisposable
    {
        // WfpEngine handle
        private WfpEngineHandles handleManager = new WfpEngineHandles();
        //private IntPtr engineHandle = IntPtr.Zero;
        private IntPtr session_key = IntPtr.Zero;
        
        public LogFilter log_filter = new LogFilter();
        private FWPM_NET_EVENT_CALLBACK4_ monitor_callback;

        // Last WinAPI error
        private static (int, string) lasterror;


        public LogFilter GetLogFilters()
        {
             return log_filter;
        }

        public IntPtr GetSessionKey()
        {
            return session_key;
        }

        public static T Win32CallWrapper<T>(Func<T> action, string operationName = "")
        {
            try
            {
                T status = action();

                // Check if the result indicates an error condition
                if ((status is int intResult && intResult != 0) ||
                (status is uint uintResult && uintResult != 0))
                {
                    lasterror = Win32Helper.Win32Helper.GetLastError(operationName);
                    int errorCode = Marshal.GetLastWin32Error();
                    if (errorCode != 0)
                    {
                        throw new Win32Exception(errorCode);
                    }
                    //throw new Exception($"{operationName} status = {status}");
                }

                return status;
            }
            catch (Win32Exception ex)
            {
                Console.WriteLine($"WfpCallWrapper exception: {ex.Message} ");
                throw; // Re-throw the exception after handling
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"{operationName} status != ERROR_SUCCESS");
                throw; // Re-throw the exception after handling
            }
        }

       
        public WfpClient()
        {
            uint status;

            // Open connect to WFP engine
            status = Connect();
            
        }

        private uint Connect()
        {
            return Win32CallWrapper(() =>
            {
                return FwpmEngineOpen0(null,
                                (uint)RPC_C_AUTHN_.RPC_C_AUTHN_WINNT,
                                IntPtr.Zero,
                                session_key,
                                ref handleManager._engineHandle);
            }, "FwpmEngineOpen0");
        }


        public void Dispose()
        {
            if (handleManager.engineHandle != IntPtr.Zero)
            {
                uint status = Win32CallWrapper(() =>
                {
                    return FwpmEngineClose0(handleManager.engineHandle);
                }, "FwpmEngineClose0");

            }
        }


        public void GetNetworkInterfaceByLuid(ulong luid)
        {
            uint outBufLen = int.MaxValue;
            IntPtr pIfTable = IntPtr.Zero;

            try
            {
                // First call to get the size of the buffer needed
                int result = GetInterfaceInfo(IntPtr.Zero, ref outBufLen);
                if (result != 0x7a)
                {
                    throw new Exception("Failed to get interface information.");
                }

                // Allocate memory for the interface information
                pIfTable = Marshal.AllocHGlobal((int)outBufLen);

                // Call again to get the actual interface information
                result = GetInterfaceInfo(pIfTable, ref outBufLen);
                if (result != 0)
                {
                    throw new Exception("Failed to get interface information.");
                }

                // Process the interface information here...
                IP_INTERFACE_INFO[] interfaces = new IP_INTERFACE_INFO[outBufLen / Marshal.SizeOf(typeof(IP_INTERFACE_INFO))];

                for (int i = 0; i < interfaces.Length; i++)
                {
                    IP_INTERFACE_INFO iface = (IP_INTERFACE_INFO)Marshal.PtrToStructure(
                        IntPtr.Add(pIfTable, i * Marshal.SizeOf(typeof(IP_INTERFACE_INFO))),
                        typeof(IP_INTERFACE_INFO));

                    // Check if this interface matches the given LUID
                    if (iface.Luid == luid)
                    {
                        Console.WriteLine($"Found Interface: Index={iface.Index}, MAC={BitConverter.ToString(iface.MacAddress)}");
                        return; // Exit after finding the interface
                    }
                }

                Console.WriteLine("No interface found with the specified LUID.");
            }
            finally
            {
                if (pIfTable != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(pIfTable);
                }
            }
        }

    }
}
