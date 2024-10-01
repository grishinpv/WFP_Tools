using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace NativeAPI
{

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct MIB_IFROW
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)] // Assuming MAX_INTERFACE_NAME_LEN is 256
        public string wszName; // Interface name

        public uint dwIndex; // Interface index (IF_INDEX)

        public uint dwType; // Interface type (IFTYPE)

        public uint dwMtu; // Maximum Transmission Unit

        public uint dwSpeed; // Speed of the interface

        public uint dwPhysAddrLen; // Physical address length

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)] // Assuming MAXLEN_PHYSADDR is 8
        public byte[] bPhysAddr; // Physical address (MAC address)

        public uint dwAdminStatus; // Administrative status

        public uint dwOperStatus; // Operational status (INTERNAL_IF_OPER_STATUS)

        public uint dwLastChange; // Last change time

        public uint dwInOctets; // Incoming octets

        public uint dwInUcastPkts; // Incoming unicast packets

        public uint dwInNUcastPkts; // Incoming non-unicast packets

        public uint dwInDiscards; // Incoming discards

        public uint dwInErrors; // Incoming errors

        public uint dwInUnknownProtos; // Incoming unknown protocols

        public uint dwOutOctets; // Outgoing octets

        public uint dwOutUcastPkts; // Outgoing unicast packets

        public uint dwOutNUcastPkts; // Outgoing non-unicast packets

        public uint dwOutDiscards; // Outgoing discards

        public uint dwOutErrors; // Outgoing errors

        public uint dwOutQLen; // Output queue length

        public uint dwDescrLen; // Description length

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)] // Assuming MAXLEN_IFDESCR is 256
        public byte[] bDescr; // Interface description
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct IP_ADAPTER_INDEX_MAP
    {
        public uint Index; // Equivalent to ULONG

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)] // Assuming MAX_ADAPTER_NAME is 128
        public string Name; // Adapter name as a string
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct IP_INTERFACE_INFO
    {
        public uint Index; // Interface index
        public uint Type; // Interface type
        public uint Speed; // Interface speed
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public byte[] MacAddress; // MAC address
        public uint Luid; // Locally Unique Identifier
    }

    internal class IpHlpAPI
    {
        //[DllImport("iphlpapi.dll", EntryPoint = "GetInterfaceInfo")]
        //public static extern int GetInterfaceInfo(
        //    ref IntPtr pIfTable, 
        //    ref ulong dwOutBufLen);

        [DllImport("iphlpapi.dll", SetLastError = true)]
        public static extern int GetInterfaceInfo(IntPtr pIfTable, ref uint dwOutBufLen);

        [DllImport("iphlpapi.dll", SetLastError = true)]
        public static extern int GetIfEntry(ref MIB_IFROW pIfRow);
    }

}
