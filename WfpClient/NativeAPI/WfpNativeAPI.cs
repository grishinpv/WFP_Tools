using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using static NativeAPI.WfpNativeAPI;
using System.Security.AccessControl;

namespace NativeAPI
{
    public partial class WfpNativeAPI
    {

        [Flags]
        public enum FWP_ACTION_TYPE_
        {
            FWP_ACTION_FLAG_TERMINATING = 0x00001000,
            FWP_ACTION_FLAG_NON_TERMINATING = 0x00002000,
            FWP_ACTION_FLAG_CALLOUT = 0x00004000,
            FWP_ACTION_BLOCK = 0x00000001 | FWP_ACTION_FLAG_TERMINATING,
            FWP_ACTION_PERMIT = 0x00000002 | FWP_ACTION_FLAG_TERMINATING,
            FWP_ACTION_CALLOUT_TERMINATING = 0x00000003 | FWP_ACTION_FLAG_CALLOUT | FWP_ACTION_FLAG_TERMINATING,
            FWP_ACTION_CALLOUT_INSPECTION = 0x00000004 | FWP_ACTION_FLAG_CALLOUT | FWP_ACTION_FLAG_NON_TERMINATING,
            FWP_ACTION_CALLOUT_UNKNOWN = 0x00000005 | FWP_ACTION_FLAG_CALLOUT

        }
        public enum IPSEC_TRAFFIC_TYPE_
        {
            IPSEC_TRAFFIC_TYPE_TRANSPORT = 0,
            IPSEC_TRAFFIC_TYPE_TUNNEL,
            IPSEC_TRAFFIC_TYPE_MAX
        }

        public enum IKEEXT_AUTHENTICATION_METHOD_TYPE_
        {
            IKEEXT_PRESHARED_KEY = 0,
            IKEEXT_CERTIFICATE,
            IKEEXT_KERBEROS,
            IKEEXT_ANONYMOUS,
            IKEEXT_SSL,
            IKEEXT_NTLM_V2,
            IKEEXT_IPV6_CGA,
            IKEEXT_CERTIFICATE_ECDSA_P256,
            IKEEXT_CERTIFICATE_ECDSA_P384,
            IKEEXT_SSL_ECDSA_P256,
            IKEEXT_SSL_ECDSA_P384,
            IKEEXT_EAP,
            IKEEXT_RESERVED,
            IKEEXT_AUTHENTICATION_METHOD_TYPE_MAX
        }

        public enum IKEEXT_AUTHENTICATION_IMPERSONATION_TYPE_
        {
            IKEEXT_IMPERSONATION_NONE = 0,
            IKEEXT_IMPERSONATION_SOCKET_PRINCIPAL,
            IKEEXT_IMPERSONATION_MAX
        }

        public enum IKEEXT_INTEGRITY_TYPE_
        {
            IKEEXT_INTEGRITY_MD5 = 0,
            IKEEXT_INTEGRITY_SHA1,
            IKEEXT_INTEGRITY_SHA_256,
            IKEEXT_INTEGRITY_SHA_384,
            IKEEXT_INTEGRITY_TYPE_MAX
        }

        public enum IKEEXT_KEY_MODULE_TYPE_
        {
            IKEEXT_KEY_MODULE_IKE = 0,
            IKEEXT_KEY_MODULE_AUTHIP,
            IKEEXT_KEY_MODULE_IKEV2,
            IKEEXT_KEY_MODULE_MAX
        }

        public enum IKEEXT_CIPHER_TYPE_
        {
            IKEEXT_CIPHER_DES = 0,
            IKEEXT_CIPHER_3DES,
            IKEEXT_CIPHER_AES_128,
            IKEEXT_CIPHER_AES_192,
            IKEEXT_CIPHER_AES_256,
            IKEEXT_CIPHER_AES_GCM_128_16ICV,
            IKEEXT_CIPHER_AES_GCM_256_16ICV,
            IKEEXT_CIPHER_TYPE_MAX
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct IKEEXT_INTEGRITY_ALGORITHM0_
        {
            public IKEEXT_INTEGRITY_TYPE_ algoIdentifier;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct IKEEXT_CIPHER_ALGORITHM0_
        {
            public IKEEXT_CIPHER_TYPE_ algoIdentifier;
            public uint keyLen;
            public uint rounds;
        }

        public enum IKEEXT_DH_GROUP_
        {
            IKEEXT_DH_GROUP_NONE = 0,
            IKEEXT_DH_GROUP_1,
            IKEEXT_DH_GROUP_2,
            IKEEXT_DH_GROUP_14,
            IKEEXT_DH_GROUP_2048,
            IKEEXT_DH_ECP_256,
            IKEEXT_DH_ECP_384,
            IKEEXT_DH_GROUP_24,
            IKEEXT_DH_GROUP_MAX
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct IKEEXT_PROPOSAL0_
        {
            public IKEEXT_CIPHER_ALGORITHM0_ cipherAlgorithm;
            public IKEEXT_INTEGRITY_ALGORITHM0_ integrityAlgorithm;
            public uint maxLifetimeSeconds;
            public IKEEXT_DH_GROUP_ dhGroup;
            public uint quickModeLimit;
        }

        public enum FWP_E : uint
        {
            SUCCESS = 0x00000000,
            CALLOUT_NOT_FOUND = 0x80320001,
            CONDITION_NOT_FOUND = 0x80320002,
            FILTER_NOT_FOUND = 0x80320003,
            LAYER_NOT_FOUND = 0x80320004,
            PROVIDER_NOT_FOUND = 0x80320005,
            PROVIDER_CONTEXT_NOT_FOUND = 0x80320006,
            SUBLAYER_NOT_FOUND = 0x80320007,
            NOT_FOUND = 0x80320008,
            ALREADY_EXISTS = 0x80320009,
            IN_USE = 0x8032000A,
            DYNAMIC_SESSION_IN_PROGRESS = 0x8032000B,
            WRONG_SESSION = 0x8032000C,
            NO_TXN_IN_PROGRESS = 0x8032000D,
            TXN_IN_PROGRESS = 0x8032000E,
            TXN_ABORTED = 0x8032000F,
            SESSION_ABORTED = 0x80320010,
            INCOMPATIBLE_TXN = 0x80320011,
            TIMEOUT = 0x80320012,
            NET_EVENTS_DISABLED = 0x80320013,
            INCOMPATIBLE_LAYER = 0x80320014,
            KM_CLIENTS_ONLY = 0x80320015,
            LIFETIME_MISMATCH = 0x80320016,
            BUILTIN_OBJECT = 0x80320017,
            TOO_MANY_CALLOUTS = 0x80320018,
            NOTIFICATION_DROPPED = 0x80320019,
            TRAFFIC_MISMATCH = 0x8032001A,
            INCOMPATIBLE_SA_STATE = 0x8032001B,
            NULL_POINTER = 0x8032001C,
            INVALID_ENUMERATOR = 0x8032001D,
            INVALID_FLAGS = 0x8032001E,
            INVALID_NET_MASK = 0x8032001F,
            INVALID_RANGE = 0x80320020,
            INVALID_INTERVAL = 0x80320021,
            ZERO_LENGTH_ARRAY = 0x80320022,
            NULL_DISPLAY_NAME = 0x80320023,
            INVALID_ACTION_TYPE = 0x80320024,
            INVALID_WEIGHT = 0x80320025,
            MATCH_TYPE_MISMATCH = 0x80320026,
            TYPE_MISMATCH = 0x80320027,
            OUT_OF_BOUNDS = 0x80320028,
            RESERVED = 0x80320029,
            DUPLICATE_CONDITION = 0x8032002A,
            DUPLICATE_KEYMOD = 0x8032002B,
            ACTION_INCOMPATIBLE_WITH_LAYER = 0x8032002C,
            ACTION_INCOMPATIBLE_WITH_SUBLAYER = 0x8032002D,
            CONTEXT_INCOMPATIBLE_WITH_LAYER = 0x8032002E,
            CONTEXT_INCOMPATIBLE_WITH_CALLOUT = 0x8032002F,
            INCOMPATIBLE_AUTH_METHOD = 0x80320030,
            INCOMPATIBLE_DH_GROUP = 0x80320031,
            EM_NOT_SUPPORTED = 0x80320032,
            NEVER_MATCH = 0x80320033,
            PROVIDER_CONTEXT_MISMATCH = 0x80320034,
            INVALID_PARAMETER = 0x80320035,
            TOO_MANY_SUBLAYERS = 0x80320036,
            CALLOUT_NOTIFICATION_FAILED = 0x80320037,
            INVALID_AUTH_TRANSFORM = 0x80320038,
            INVALID_CIPHER_TRANSFORM = 0x80320039,
            INCOMPATIBLE_CIPHER_TRANSFORM = 0x8032003A,
            INVALID_TRANSFORM_COMBINATION = 0x8032003B,
            DUPLICATE_AUTH_METHOD = 0x8032003C,
            INVALID_TUNNEL_ENDPOINT = 0x8032003D,
            L2_DRIVER_NOT_READY = 0x8032003E,
            KEY_DICTATOR_ALREADY_REGISTERED = 0x8032003F,
            KEY_DICTATION_INVALID_KEYING_MATERIAL = 0x80320040,
            CONNECTIONS_DISABLED = 0x80320041,
            INVALID_DNS_NAME = 0x80320042,
            STILL_ON = 0x80320043,
            IKEEXT_NOT_RUNNING = 0x80320044,
            DROP_NOICMP = 0x80320104,
        }

        public enum FWPM_PROVIDER_FLAG_ : uint
        {
            PERSISTENT = 0x00000001,
            DISABLED = 0x00000010
        }

        public enum FWPM_SUBLAYER_FLAG_ : uint
        {
            NONE = 0x00000000,
            PERSISTENT = 0x00000001,
        }

        public enum FWPM_FILTER_FLAG_ : uint
        {
            NONE = 0x00000000,
            PERSISTENT = 0x00000001,
            BOOTTIME = 0x00000002,
            HAS_PROVIDER_CONTEXT = 0x00000004,
            CLEAR_ACTION_RIGHT = 0x00000008,
            PERMIT_IF_CALLOUT_UNREGISTERED = 0x00000010,
            DISABLED = 0x00000020,
            INDEXED = 0x00000040,
        }

        public enum RPC_C_AUTHN_
        {
            /// RPC_C_AUTHN_WINNT  -> 10
            RPC_C_AUTHN_WINNT = 10,

            /// RPC_C_AUTHN_DEFAULT -> 0xFFFFFFFF

        }

        public enum FWP_DIRECTION_
        {
            /// FWP_DIRECTION_OUTBOUND -> 0
            FWP_DIRECTION_OUTBOUND = 0,

            /// FWP_DIRECTION_INBOUND -> 1
            FWP_DIRECTION_INBOUND = 1,

            /// FWP_DIRECTION_MAX -> 2
            FWP_DIRECTION_MAX = 2,
        }

        public enum FWP_IP_VERSION_
        {
            /// FWP_IP_VERSION_V4 -> 0
            FWP_IP_VERSION_V4 = 0,

            /// FWP_IP_VERSION_V6 -> 1
            FWP_IP_VERSION_V6 = 1,

            /// FWP_IP_VERSION_NONE -> 2
            FWP_IP_VERSION_NONE = 2,

            /// FWP_IP_VERSION_MAX -> 3
            FWP_IP_VERSION_MAX = 3,
        }

        public enum FWP_NE_FAMILY_
        {
            /// FWP_AF_INET -> FWP_IP_VERSION_V4
            FWP_AF_INET = FWP_IP_VERSION_.FWP_IP_VERSION_V4,

            /// FWP_AF_INET6 -> FWP_IP_VERSION_V6
            FWP_AF_INET6 = FWP_IP_VERSION_.FWP_IP_VERSION_V6,

            /// FWP_AF_ETHER -> FWP_IP_VERSION_NONE
            FWP_AF_ETHER = FWP_IP_VERSION_.FWP_IP_VERSION_NONE,

            /// FWP_AF_NONE -> 3
            FWP_AF_NONE = 3,
        }

        public enum FWP_ETHER_ENCAP_METHOD_
        {
            /// FWP_ETHER_ENCAP_METHOD_ETHER_V2 -> 0
            FWP_ETHER_ENCAP_METHOD_ETHER_V2 = 0,

            /// FWP_ETHER_ENCAP_METHOD_SNAP -> 1
            FWP_ETHER_ENCAP_METHOD_SNAP = 1,

            /// FWP_ETHER_ENCAP_METHOD_SNAP_W_OUI_ZERO -> 3
            FWP_ETHER_ENCAP_METHOD_SNAP_W_OUI_ZERO = 3,
        }

        public enum FWP_DATA_TYPE_
        {
            /// FWP_EMPTY -> 0
            FWP_EMPTY = 0,

            /// FWP_UINT8 -> 1
            FWP_UINT8 = 1,

            /// FWP_UINT16 -> 2
            FWP_UINT16 = 2,

            /// FWP_UINT32 -> 3
            FWP_UINT32 = 3,

            /// FWP_UINT64 -> 4
            FWP_UINT64 = 4,

            /// FWP_INT8 -> 5
            FWP_INT8 = 5,

            /// FWP_INT16 -> 6
            FWP_INT16 = 6,

            /// FWP_INT32 -> 7
            FWP_INT32 = 7,

            /// FWP_INT64 -> 8
            FWP_INT64 = 8,

            /// FWP_FLOAT -> 9
            FWP_FLOAT = 9,

            /// FWP_DOUBLE -> 10
            FWP_DOUBLE = 10,

            /// FWP_BYTE_ARRAY16_TYPE -> 11
            FWP_BYTE_ARRAY16_TYPE = 11,

            /// FWP_BYTE_BLOB_TYPE -> 12
            FWP_BYTE_BLOB_TYPE = 12,

            /// FWP_SID -> 13
            FWP_SID = 13,

            /// FWP_SECURITY_DESCRIPTOR_TYPE -> 14
            FWP_SECURITY_DESCRIPTOR_TYPE = 14,

            /// FWP_TOKEN_INFORMATION_TYPE -> 15
            FWP_TOKEN_INFORMATION_TYPE = 15,

            /// FWP_TOKEN_ACCESS_INFORMATION_TYPE -> 16
            FWP_TOKEN_ACCESS_INFORMATION_TYPE = 16,

            /// FWP_UNICODE_STRING_TYPE -> 17
            FWP_UNICODE_STRING_TYPE = 17,

            /// FWP_BYTE_ARRAY6_TYPE -> 18
            FWP_BYTE_ARRAY6_TYPE = 18,

            /// FWP_SINGLE_DATA_TYPE_MAX -> 0xff
            FWP_SINGLE_DATA_TYPE_MAX = 255,

            /// FWP_V4_ADDR_MASK -> 0x100
            FWP_V4_ADDR_MASK = 256,

            /// FWP_V6_ADDR_MASK -> 0x101
            FWP_V6_ADDR_MASK = 257,

            /// FWP_RANGE_TYPE -> 0x102
            FWP_RANGE_TYPE = 258,

            /// FWP_DATA_TYPE_MAX -> 0x103
            FWP_DATA_TYPE_MAX = 259,
        }

        public enum FWP_MATCH_TYPE_
        {
            /// FWP_MATCH_EQUAL -> 0
            FWP_MATCH_EQUAL = 0,

            /// FWP_MATCH_GREATER -> 1
            FWP_MATCH_GREATER = 1,

            /// FWP_MATCH_LESS -> 2
            FWP_MATCH_LESS = 2,

            /// FWP_MATCH_GREATER_OR_EQUAL -> 3
            FWP_MATCH_GREATER_OR_EQUAL = 3,

            /// FWP_MATCH_LESS_OR_EQUAL -> 4
            FWP_MATCH_LESS_OR_EQUAL = 4,

            /// FWP_MATCH_RANGE -> 5
            FWP_MATCH_RANGE = 5,

            /// FWP_MATCH_FLAGS_ALL_SET -> 6
            FWP_MATCH_FLAGS_ALL_SET = 6,

            /// FWP_MATCH_FLAGS_ANY_SET -> 7
            FWP_MATCH_FLAGS_ANY_SET = 7,

            /// FWP_MATCH_FLAGS_NONE_SET -> 8
            FWP_MATCH_FLAGS_NONE_SET = 8,

            /// FWP_MATCH_EQUAL_CASE_INSENSITIVE -> 9
            FWP_MATCH_EQUAL_CASE_INSENSITIVE = 9,

            /// FWP_MATCH_NOT_EQUAL -> 10
            FWP_MATCH_NOT_EQUAL = 10,

            /// FWP_MATCH_TYPE_MAX -> 11
            FWP_MATCH_TYPE_MAX = 11,
        }

        public enum FWP_VSWITCH_NETWORK_TYPE_
        {
            /// FWP_VSWITCH_NETWORK_TYPE_UNKNOWN -> 0
            FWP_VSWITCH_NETWORK_TYPE_UNKNOWN = 0,

            /// FWP_VSWITCH_NETWORK_TYPE_PRIVATE -> 1
            FWP_VSWITCH_NETWORK_TYPE_PRIVATE = 1,

            /// FWP_VSWITCH_NETWORK_TYPE_INTERNAL -> 2
            FWP_VSWITCH_NETWORK_TYPE_INTERNAL = 2,

            /// FWP_VSWITCH_NETWORK_TYPE_EXTERNAL -> 3
            FWP_VSWITCH_NETWORK_TYPE_EXTERNAL = 3,
        }

        public enum FWP_CLASSIFY_OPTION_TYPE_
        {
            /// FWP_CLASSIFY_OPTION_MULTICAST_STATE -> 0
            FWP_CLASSIFY_OPTION_MULTICAST_STATE = 0,

            /// FWP_CLASSIFY_OPTION_LOOSE_SOURCE_MAPPING -> 1
            FWP_CLASSIFY_OPTION_LOOSE_SOURCE_MAPPING = 1,

            /// FWP_CLASSIFY_OPTION_UNICAST_LIFETIME -> 2
            FWP_CLASSIFY_OPTION_UNICAST_LIFETIME = 2,

            /// FWP_CLASSIFY_OPTION_MCAST_BCAST_LIFETIME -> 3
            FWP_CLASSIFY_OPTION_MCAST_BCAST_LIFETIME = 3,

            /// FWP_CLASSIFY_OPTION_SECURE_SOCKET_SECURITY_FLAGS -> 4
            FWP_CLASSIFY_OPTION_SECURE_SOCKET_SECURITY_FLAGS = 4,

            /// FWP_CLASSIFY_OPTION_SECURE_SOCKET_AUTHIP_MM_POLICY_KEY -> 5
            FWP_CLASSIFY_OPTION_SECURE_SOCKET_AUTHIP_MM_POLICY_KEY = 5,

            /// FWP_CLASSIFY_OPTION_SECURE_SOCKET_AUTHIP_QM_POLICY_KEY -> 6
            FWP_CLASSIFY_OPTION_SECURE_SOCKET_AUTHIP_QM_POLICY_KEY = 6,

            /// FWP_CLASSIFY_OPTION_LOCAL_ONLY_MAPPING -> 7
            FWP_CLASSIFY_OPTION_LOCAL_ONLY_MAPPING = 7,

            /// FWP_CLASSIFY_OPTION_MAX -> 8
            FWP_CLASSIFY_OPTION_MAX = 8,
        }

        public enum FWP_FILTER_ENUM_TYPE_
        {
            /// FWP_FILTER_ENUM_FULLY_CONTAINED -> 0
            FWP_FILTER_ENUM_FULLY_CONTAINED = 0,

            /// FWP_FILTER_ENUM_OVERLAPPING -> 1
            FWP_FILTER_ENUM_OVERLAPPING = 1,

            /// FWP_FILTER_ENUM_TYPE_MAX -> 2
            FWP_FILTER_ENUM_TYPE_MAX = 2,
        }


        [StructLayout(LayoutKind.Sequential)]
        public struct FWPM_DISPLAY_DATA0_
        {
            /// wchar_t*
            [MarshalAs(UnmanagedType.LPWStr)]
            public string name;

            /// wchar_t*
            [MarshalAs(UnmanagedType.LPWStr)]
            public string description;
        }

        //[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
        //public struct FWP_BYTE_BLOB_
        //{
        //    /// UINT32->int
        //    public int size;

        //    /// UINT8*
        //    [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.LPStr)]
        //    public string data;
        //}

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct FWP_BYTE_ARRAY6_
        {
            /// UINT8[6]
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 6)]
            public string byteArray6;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct FWP_BYTE_ARRAY16_
        {
            /// UINT8[16]
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string byteArray16;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct FWP_TOKEN_INFORMATION
        {
            /// ULONG->int
            public int sidCount;

            /// PSID_AND_ATTRIBUTES->_SID_AND_ATTRIBUTES*
            public IntPtr sids;

            /// ULONG->int
            public int restrictedSidCount;

            /// PSID_AND_ATTRIBUTES->_SID_AND_ATTRIBUTES*
            public IntPtr restrictedSids;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct FWP_V4_ADDR_AND_MASK_
        {
            /// UINT32->int
            public int addr;

            /// UINT32->int
            public int mask;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct FWP_V6_ADDR_AND_MASK_
        {
            /// UINT8[0]
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0)]
            public string addr;

            /// UINT8->char
            public byte prefixLength;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct Anonymous_750df4da_d850_4fa1_a0b8_aefa57daeefd
        {
            /// UINT8->char
            [FieldOffset(0)]
            public byte uint8;

            /// UINT16->short
            [FieldOffset(0)]
            public short uint16;

            /// UINT32->int
            [FieldOffset(0)]
            public int uint32;

            /// UINT64*
            [FieldOffset(0)]
            public IntPtr uint64;

            /// INT8->char
            [FieldOffset(0)]
            public byte int8;

            /// INT16->short
            [FieldOffset(0)]
            public short int16;

            /// INT32->int
            [FieldOffset(0)]
            public int int32;

            /// INT64*
            [FieldOffset(0)]
            public IntPtr int64;

            /// float
            [FieldOffset(0)]
            public float float32;

            /// double*
            [FieldOffset(0)]
            public IntPtr double64;

            /// FWP_BYTE_ARRAY16*
            [FieldOffset(0)]
            public IntPtr byteArray16;

            /// FWP_BYTE_BLOB*
            [FieldOffset(0)]
            public IntPtr byteBlob;

            /// SID*
            [FieldOffset(0)]
            public IntPtr sid;

            /// FWP_BYTE_BLOB*
            [FieldOffset(0)]
            public IntPtr sd;

            /// FWP_TOKEN_INFORMATION*
            [FieldOffset(0)]
            public IntPtr tokenInformation;

            /// FWP_BYTE_BLOB*
            [FieldOffset(0)]
            public IntPtr tokenAccessInformation;

            /// LPWSTR->WCHAR*
            [FieldOffset(0)]
            public IntPtr unicodeString;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct FWP_VALUE0_
        {
            /// FWP_DATA_TYPE->FWP_DATA_TYPE_
            public FWP_DATA_TYPE_ type;

            /// Anonymous_750df4da_d850_4fa1_a0b8_aefa57daeefd
            public Anonymous_750df4da_d850_4fa1_a0b8_aefa57daeefd Union1;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct FWP_RANGE0_
        {
            /// FWP_VALUE0->FWP_VALUE0_
            public FWP_VALUE0_ valueLow;

            /// FWP_VALUE0->FWP_VALUE0_
            public FWP_VALUE0_ valueHigh;
        }


        [StructLayout(LayoutKind.Sequential)]
        public struct FWPM_ACTION0_
        {
            /// FWP_ACTION_TYPE->FWP_ACTION_TYPE
            public FWP_ACTION_TYPE_ type;

            /// 
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            private byte[] guidData;

            public Guid FilterType
            {
                get => new Guid(guidData);
                set => guidData = value.ToByteArray();
            }

            public Guid CalloutKey
            {
                get => new Guid(guidData);
                set => guidData = value.ToByteArray();
            }
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct Anonymous_99707a60_90d9_473c_951b_97fea591b664
        {
            /// UINT8->char
            [FieldOffset(0)]
            public byte uint8;

            /// UINT16->short
            [FieldOffset(0)]
            public short uint16;

            /// UINT32->int
            [FieldOffset(0)]
            public int uint32;

            /// UINT64*
            [FieldOffset(0)]
            public IntPtr uint64;

            /// INT8->char
            [FieldOffset(0)]
            public byte int8;

            /// INT16->short
            [FieldOffset(0)]
            public short int16;

            /// INT32->int
            [FieldOffset(0)]
            public int int32;

            /// INT64*
            [FieldOffset(0)]
            public IntPtr int64;

            /// float
            [FieldOffset(0)]
            public float float32;

            /// double*
            [FieldOffset(0)]
            public IntPtr double64;

            /// FWP_BYTE_ARRAY16*
            [FieldOffset(0)]
            public IntPtr byteArray16;

            /// FWP_BYTE_BLOB*
            [FieldOffset(0)]
            public IntPtr byteBlob;

            /// SID*
            [FieldOffset(0)]
            public IntPtr sid;

            /// FWP_BYTE_BLOB*
            [FieldOffset(0)]
            public IntPtr sd;

            /// FWP_BYTE_BLOB*
            [FieldOffset(0)]
            public IntPtr tokenInformation;

            /// FWP_BYTE_BLOB*
            [FieldOffset(0)]
            public IntPtr tokenAccessInformation;

            /// LPWSTR->WCHAR*
            [FieldOffset(0)]
            public IntPtr unicodeString;

            /// FWP_BYTE_ARRAY6*
            [FieldOffset(0)]
            public IntPtr byteArray6;

            /// FWP_V4_ADDR_AND_MASK*
            [FieldOffset(0)]
            public IntPtr v4AddrMask;

            /// FWP_V6_ADDR_AND_MASK*
            [FieldOffset(0)]
            public IntPtr v6AddrMask;

            /// FWP_RANGE0*
            [FieldOffset(0)]
            public IntPtr rangeValue;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct FWP_CONDITION_VALUE0_
        {
            /// FWP_DATA_TYPE->FWP_DATA_TYPE_
            public FWP_DATA_TYPE_ type;

            /// Anonymous_99707a60_90d9_473c_951b_97fea591b664
            public Anonymous_99707a60_90d9_473c_951b_97fea591b664 Union1;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct FWPM_FILTER_CONDITION0_
        {
            /// GUID->_GUID
            public Guid fieldKey;

            /// FWP_MATCH_TYPE->FWP_MATCH_TYPE_
            public FWP_MATCH_TYPE_ matchType;

            /// FWP_CONDITION_VALUE0->FWP_CONDITION_VALUE0_
            public FWP_CONDITION_VALUE0_ conditionValue;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct Anonymous_cb34be57_1743_4fbd_bd83_ab14b1cf7ace
        {
            /// UINT64->__int64
            [FieldOffset(0)]
            public long rawContext;

            /// GUID->_GUID
            [FieldOffset(0)]
            public Guid providerContextKey;
        }




        [StructLayout(LayoutKind.Sequential)]
        public struct FWPM_PROVIDER0_
        {
            /// GUID->_GUID
            public Guid providerKey;

            /// FWPM_DISPLAY_DATA0_->FWPM_DISPLAY_DATA0_
            public FWPM_DISPLAY_DATA0_ displayData;

            /// FWPM_PROVIDER_FLAG_->FWPM_PROVIDER_FLAG_
            public FWPM_PROVIDER_FLAG_ flags;

            /// GUID*
            public FWP_BYTE_BLOB providerData;

            /// serviceName
            [MarshalAs(UnmanagedType.LPWStr)]
            public string serviceName;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct FWPM_FILTER0_UNION_
        {
            [FieldOffset(0)]
            public ulong rawContext;
            [FieldOffset(0)]
            public Guid providerContextKey;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct FWPM_FILTER0_
        {
            /// GUID->_GUID
            public Guid filterKey;

            /// FWPM_DISPLAY_DATA0->FWPM_DISPLAY_DATA0_
            public FWPM_DISPLAY_DATA0_ displayData;

            /// UINT32->int
            public FWPM_FILTER_FLAG_ flags;

            /// GUID*
            public IntPtr providerKey;

            /// FWP_BYTE_BLOB->FWP_BYTE_BLOB_
            public FWP_BYTE_BLOB providerData;

            /// GUID->_GUID
            public Guid layerKey;

            /// GUID->_GUID
            public Guid subLayerKey;

            /// FWP_VALUE0->FWP_VALUE0_
            public FWP_VALUE0_ weight;

            /// UINT32->int
            public int numFilterConditions;

            /// FWPM_FILTER_CONDITION0*
            public unsafe FWPM_FILTER_CONDITION0_* filterCondition;

            /// FWPM_ACTION0->FWPM_ACTION0_
            public FWPM_ACTION0_ action;

            /// UINT64 RawContext or GUID ProviderContextKey 
            public FWPM_FILTER0_UNION_ context;

            /// GUID*
            public IntPtr reserved;

            /// UINT64->__int64
            public ulong filterId;

            /// FWP_VALUE0->FWP_VALUE0_
            public FWP_VALUE0_ effectiveWeight;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct FWPM_SUBLAYER0_
        {
            /// GUID->_GUID
            public Guid subLayerKey;

            /// FWPM_DISPLAY_DATA0->FWPM_DISPLAY_DATA0_
            public FWPM_DISPLAY_DATA0_ displayData;

            /// UINT32->int
            public FWPM_SUBLAYER_FLAG_ flags;

            /// GUID*
            public IntPtr providerKey;

            /// FWP_BYTE_BLOB->FWP_BYTE_BLOB_
            public FWP_BYTE_BLOB providerData;

            /// UINT16->short
            public short weight;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SECURITY_DESCRIPTOR_
        {
            /// BYTE->char
            public byte Revision;

            /// BYTE->char
            public byte Sbz1;

            /// SECURITY_DESCRIPTOR_CONTROL->WORD->short
            public short Control;

            /// PSID->PVOID->void*
            public IntPtr Owner;

            /// PSID->PVOID->void*
            public IntPtr Group;

            /// PACL->ACL*
            public IntPtr Sacl;

            /// PACL->ACL*
            public IntPtr Dacl;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SID_
        {

            /// BYTE->unsigned char
            public byte Revision;

            /// BYTE->unsigned char
            public byte SubAuthorityCount;

            /// SID_IDENTIFIER_AUTHORITY->_SID_IDENTIFIER_AUTHORITY
            public SID_IDENTIFIER_AUTHORITY IdentifierAuthority;

            /// DWORD[1]
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1, ArraySubType = UnmanagedType.U4)]
            public uint[] SubAuthority;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SID_IDENTIFIER_AUTHORITY
        {

            /// BYTE[6]
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6, ArraySubType = UnmanagedType.I1)]
            public byte[] Value;
        }


        [Flags]
        public enum FirewallSessionFlags
        {
            None = 0,
            FWPM_SESSION_FLAG_DYNAMIC = 0x00000001,
            FWPM_SESSION_FLAG_RESERVED = 0x10000000
        }


        [StructLayout(LayoutKind.Sequential)]
        public struct FWPM_SESSION0_
        {
            public Guid sessionKey;
            public FWPM_DISPLAY_DATA0_ displayData;
            public FirewallSessionFlags flags;
            public uint txnWaitTimeoutInMSec;
            public int processId;
            /// SID->SID*
            public IntPtr sid;
            /// wchar_t->wchar_t*
            public IntPtr username;
            public bool kernelMode;
        }

        public struct FWP_BYTE_BLOB
        {
            public uint size;
            public IntPtr data;
        }

        [Flags]
        public enum FirewallCalloutFlags : uint
        {
            None = 0,
            FWP_CALLOUT_FLAG_CONDITIONAL_ON_FLOW = 0x00000001,
            FWP_CALLOUT_FLAG_ALLOW_OFFLOAD = 0x00000002,
            FWP_CALLOUT_FLAG_ENABLE_COMMIT_ADD_NOTIFY = 0x00000004,
            FWP_CALLOUT_FLAG_ALLOW_MID_STREAM_INSPECTION = 0x00000008,
            FWP_CALLOUT_FLAG_ALLOW_RECLASSIFY = 0x00000010,
            FWP_CALLOUT_FLAG_RESERVED1 = 0x00000020,
            FWP_CALLOUT_FLAG_ALLOW_RSC = 0x00000040,
            FWP_CALLOUT_FLAG_ALLOW_L2_BATCH_CLASSIFY = 0x00000080,
            FWP_CALLOUT_FLAG_ALLOW_USO = 0x00000100,
            FWP_CALLOUT_FLAG_ALLOW_URO = 0x00000200,
            FWPM_CALLOUT_FLAG_PERSISTENT = 0x00010000,
            FWPM_CALLOUT_FLAG_USES_PROVIDER_CONTEXT = 0x00020000,
            FWPM_CALLOUT_FLAG_REGISTERED = 0x00040000
        }


        [StructLayout(LayoutKind.Sequential)]
        public struct FWPM_CALLOUT0_
        {
            public Guid calloutKey;
            public FWPM_DISPLAY_DATA0_ displayData;
            public FirewallCalloutFlags flags;
            public IntPtr providerKey; // _GUID
            public FWP_BYTE_BLOB providerData;
            public Guid applicableLayer;
            public uint calloutId;
        }

        // authnservice = 10 or uint.MaxValue, pass null for the other inputs
        [DllImport("FWPUClnt.dll", EntryPoint = "FwpmEngineOpen0")]
        public static extern uint FwpmEngineOpen0([In()][MarshalAs(UnmanagedType.LPWStr)]
                        string serverName,
                        uint authnService,
                        IntPtr authIdentity,
                        IntPtr session,
                        ref IntPtr engineHandle);

        [DllImport("FWPUClnt.dll", EntryPoint = "FwpmEngineClose0")]
        public static extern uint FwpmEngineClose0(IntPtr handle);

        // flags = 1 means read only transaction, flags of 0 is read/write transaction
        [DllImport("FWPUClnt.dll", EntryPoint = "FwpmTransactionBegin0")]
        public static extern uint FwpmTransactionBegin0(IntPtr engineHandle, uint flags);

        [DllImport("FWPUClnt.dll", EntryPoint = "FwpmTransactionCommit0")]
        public static extern uint FwpmTransactionCommit0(IntPtr engineHandle);

        [DllImport("FWPUClnt.dll", EntryPoint = "FwpmFilterAdd0")]
        public static extern uint FwpmFilterAdd0(IntPtr engineHandle,
            ref FWPM_FILTER0_ filter,
            ref SECURITY_DESCRIPTOR_ sd,
            ref IntPtr id);

        [DllImport("FWPUClnt.dll", EntryPoint = "FwpmFilterDeleteById0")]
        public static extern uint FwpmFilterDeleteById0(IntPtr engineHandle, IntPtr id);

        [DllImport("FWPUClnt.dll", EntryPoint = "FwpmFilterDeleteByKey0")]
        public static extern uint FwpmFilterDeleteByKey0(IntPtr engineHandle, ref Guid key);

        [DllImport("FWPUClnt.dll", EntryPoint = "FwpmSubLayerAdd0")]
        public static extern uint FwpmSubLayerAdd0([In()] IntPtr engineHandle,
            [In()] ref FWPM_SUBLAYER0_ subLayer,
            [In()] IntPtr sd);

        [DllImport("FWPUClnt.dll", EntryPoint = "FwpmSubLayerDeleteByKey0")]
        public static extern uint FwpmSubLayerDeleteByKey0(IntPtr engineHandle, ref Guid key);

        [DllImport("FWPUClnt.dll", EntryPoint = "FwpmCalloutAdd0")]
        public static extern uint FwpmCalloutAdd0(IntPtr engineHandle,
            ref FWPM_CALLOUT0_ callout,
            ref SECURITY_DESCRIPTOR_ sd,
            ref IntPtr id);

        [DllImport("FWPUClnt.dll", EntryPoint = "FwpmCalloutDeleteById0")]
        public static extern uint FwpmCalloutDeleteById0(IntPtr engineHandle, uint id);


        [DllImport("FWPUClnt.dll", EntryPoint = "FwpmSubLayerCreateEnumHandle0")]
        public static extern uint FwpmSubLayerCreateEnumHandle0(
            IntPtr engineHandle,
            IntPtr enumTemplate,
            ref IntPtr enumHandle);

        [DllImport("FWPUClnt.dll", EntryPoint = "FwpmSubLayerEnum0")]
        public static extern uint FwpmSubLayerEnum0(IntPtr engineHandle,
            IntPtr enumHandle,
            uint numEntriesRequested,
            ref IntPtr entries,
            ref uint numEntriesReturned);


        [DllImport("FWPUClnt.dll", EntryPoint = "FwpmSubLayerDestroyEnumHandle0")]
        public static extern uint FwpmSubLayerDestroyEnumHandle0(
            IntPtr engineHandle,
            IntPtr enumHandle);

        [DllImport("FWPUClnt.dll", EntryPoint = "FwpmFreeMemory0")]
        public static extern void FwpmFreeMemory0(ref IntPtr p);

        [DllImport("FWPUClnt.dll", EntryPoint = "FwpmProviderDeleteByKey0")]
        public static extern uint FwpmProviderDeleteByKey0(IntPtr engineHandle, ref Guid key);


        [DllImport("FWPUClnt.dll", EntryPoint = "FwpmProviderEnum0")]
        public static extern uint FwpmProviderEnum0(IntPtr engineHandle,
            IntPtr enumHandle,
            uint numEntriesRequested,
            ref IntPtr entries,
            ref uint numEntriesReturned);


        [DllImport("FWPUClnt.dll", EntryPoint = "FwpmProviderCreateEnumHandle0")]
        public static extern uint FwpmProviderCreateEnumHandle0(
            IntPtr engineHandle,
            IntPtr enumTemplate,
            ref IntPtr enumHandle);

        [DllImport("FWPUClnt.dll", EntryPoint = "FwpmProviderDestroyEnumHandle0")]
        public static extern uint FwpmProviderDestroyEnumHandle0(
            IntPtr engineHandle,
            IntPtr enumHandle);

        [DllImport("FWPUClnt.dll", EntryPoint = "FwpmFilterCreateEnumHandle0")]
        public static extern uint FwpmFilterCreateEnumHandle0(
            IntPtr engineHandle,
            IntPtr enumTemplate,
            ref IntPtr enumHandle);


        [DllImport("FWPUClnt.dll", EntryPoint = "FwpmFilterEnum0")]
        public static extern uint FwpmFilterEnum0(
            IntPtr engineHandle,
            IntPtr enumHandle,
            uint numEntriesRequested,
            ref IntPtr entries,
            ref uint numEntriesReturned);


        [DllImport("FWPUClnt.dll", EntryPoint = "FwpmProviderDestroyEnumHandle0")]
        public static extern uint FwpmFilterDestroyEnumHandle0(
           IntPtr engineHandle,
           IntPtr enumHandle);


        [DllImport("FWPUClnt.dll", EntryPoint = "FwpmCalloutCreateEnumHandle0")]
        public static extern uint FwpmCalloutCreateEnumHandle0(
            IntPtr engineHandle,
            IntPtr enumTemplate,
            ref IntPtr enumHandle);


        [DllImport("FWPUClnt.dll", EntryPoint = "FwpmCalloutEnum0")]
        public static extern uint FwpmCalloutEnum0(
            IntPtr engineHandle,
            IntPtr enumHandle,
            uint numEntriesRequested,
            ref IntPtr entries,
            ref uint numEntriesReturned);


        [DllImport("FWPUClnt.dll", EntryPoint = "FwpmCalloutDestroyEnumHandle0")]
        public static extern uint FwpmCalloutDestroyEnumHandle0(
           IntPtr engineHandle,
           IntPtr enumHandle);

        [DllImport("FWPUClnt.dll", EntryPoint = "FwpmCalloutDeleteByKey0")]
        public static extern uint FwpmCalloutDeleteByKey0(IntPtr engineHandle, ref Guid key);


        [DllImport("FWPUClnt.dll", EntryPoint = "FwpmSessionEnum0")]
        public static extern uint FwpmSessionEnum0(
            IntPtr engineHandle,
            IntPtr enumHandle,
            uint numEntriesRequested,
            ref IntPtr entries,
            ref uint numEntriesReturned);


        [DllImport("FWPUClnt.dll", EntryPoint = "FwpmSessionCreateEnumHandle0")]
        public static extern uint FwpmSessionCreateEnumHandle0(
            IntPtr engineHandle,
            IntPtr enumTemplate,
            ref IntPtr enumHandle);


        [DllImport("FWPUClnt.dll", EntryPoint = "FwpmSessionDestroyEnumHandle0")]
        public static extern uint FwpmSessionDestroyEnumHandle0(
           IntPtr engineHandle,
           IntPtr enumHandle);


        [DllImport("FWPUClnt.dll", EntryPoint = "FwpmConnectionCreateEnumHandle0")]
        public static extern uint FwpmConnectionCreateEnumHandle0(
            IntPtr engineHandle,
            IntPtr enumTemplate,
            ref IntPtr enumHandle);

        [DllImport("FWPUClnt.dll", EntryPoint = "FwpmConnectionEnum0")]
        public static extern uint FwpmConnectionEnum0(
            IntPtr engineHandle,
            IntPtr enumHandle,
            uint numEntriesRequested,
            ref IntPtr entries,
            ref uint numEntriesReturned);

        [DllImport("FWPUClnt.dll", EntryPoint = "FwpmConnectionDestroyEnumHandle0")]
        public static extern uint FwpmConnectionDestroyEnumHandle0(
           IntPtr engineHandle,
           IntPtr enumHandle);

        [StructLayout(LayoutKind.Sequential)]
        public struct IKEEXT_CREDENTIAL2_
        {
            public IKEEXT_AUTHENTICATION_METHOD_TYPE_ authenticationMethodType; // IKEEXT_AUTHENTICATION_METHOD_TYPE
            public IKEEXT_AUTHENTICATION_IMPERSONATION_TYPE_ impersonationType; // IKEEXT_AUTHENTICATION_IMPERSONATION_TYPE

            // Union for different authentication methods
            [StructLayout(LayoutKind.Explicit)]
            public struct CredentialUnion
            {
                [FieldOffset(0)]
                public IntPtr presharedKey; // Pointer to IKEEXT_PRESHARED_KEY_AUTHENTICATION1

                [FieldOffset(8)]
                public IntPtr certificate; // Pointer to IKEEXT_CERTIFICATE_CREDENTIAL1

                [FieldOffset(16)]
                public IntPtr name; // Pointer to IKEEXT_NAME_CREDENTIAL0
            }

            public CredentialUnion credentials; // Union member
        };

        [StructLayout(LayoutKind.Sequential)]
        public struct IKEEXT_PRESHARED_KEY_AUTHENTICATION1_
        {
            public FWP_BYTE_BLOB presharedKey; // FWP_BYTE_BLOB
            public uint flags; // UINT32
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct FWPM_CONNECTION0_
        {
            public uint connectionId;
            public FWP_IP_VERSION_ ipVersion;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public byte[] localV6Address; // Union for IPv6 address (UINT8[16])
            public uint localV4Address; // Union for IPv4 address (UINT32)

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public byte[] remoteV6Address; // Union for IPv6 address (UINT8[16])
            public uint remoteV4Address; // Union for IPv4 address (UINT32)


            public IntPtr providerKey; // GUID*
            public IPSEC_TRAFFIC_TYPE_ ipsecTrafficModeType; // IPSEC_TRAFFIC_TYPE
            public IKEEXT_KEY_MODULE_TYPE_ keyModuleType; // IKEEXT_KEY_MODULE_TYPE
            public IKEEXT_PROPOSAL0_ mmCrypto; // IKEEXT_PROPOSAL0
            public IKEEXT_CREDENTIAL2_ mmPeer; // IKEEXT_CREDENTIAL2
            public IKEEXT_CREDENTIAL2_ emPeer; // IKEEXT_CREDENTIAL2

            public ulong bytesTransferredIn; // UINT64
            public ulong bytesTransferredOut; // UINT64
            public ulong bytesTransferredTotal; // UINT64

            public long startSysTime; // FILETIME (represented as long)
        }

        public delegate void FWPM_NET_EVENT_CALLBACK4_(IntPtr context, FWPM_NET_EVENT5_ eventData);

        [DllImport("FWPUClnt.dll", EntryPoint = "FwpmNetEventUnsubscribe0")]
        public static extern uint FwpmNetEventUnsubscribe0(
            IntPtr engineHandle,
            IntPtr eventsHandle);

        [DllImport("FWPUClnt.dll", EntryPoint = "FwpmNetEventSubscribe4")]
        public static extern uint FwpmNetEventSubscribe4(
            IntPtr engineHandle,
            ref FWPM_NET_EVENT_SUBSCRIPTION0_ subscription,
            FWPM_NET_EVENT_CALLBACK4_ callback,
            IntPtr context,
            ref IntPtr eventsHandle);

        [StructLayout(LayoutKind.Sequential)]
        public struct FWPM_NET_EVENT_SUBSCRIPTION0_
        {
            public FWPM_NET_EVENT_ENUM_TEMPLATE0_ enumTemplate; // Pointer to FWPM_NET_EVENT_ENUM_TEMPLATE0 structure (use IntPtr)
            public uint flags; // UINT32
            public IntPtr sessionKey; // GUID
        }


        [StructLayout(LayoutKind.Sequential)]
        public struct FWP_BYTE_ARRAY16
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public byte[] byteArray; // Array of bytes (IPv6 address)
        }

        public enum ipProtocol : byte
        {
            ICMP = 0x01,
            IGMP = 0x02,
            RFCOMM = 0x03,
            TCP = 0x06,
            UDP = 0x11,
            ICMPV6 = 0x3A,
            RM = 0x71
        }

        [Flags]
        public enum FirewallNetEventFlags : uint
        {
            FWPM_NET_EVENT_FLAG_IP_PROTOCOL_SET = 0x00000001,
            FWPM_NET_EVENT_FLAG_LOCAL_ADDR_SET = 0x00000002,
            FWPM_NET_EVENT_FLAG_REMOTE_ADDR_SET = 0x00000004,
            FWPM_NET_EVENT_FLAG_LOCAL_PORT_SET = 0x00000008,
            FWPM_NET_EVENT_FLAG_REMOTE_PORT_SET = 0x00000010,
            FWPM_NET_EVENT_FLAG_APP_ID_SET = 0x00000020,
            FWPM_NET_EVENT_FLAG_USER_ID_SET = 0x00000040,
            FWPM_NET_EVENT_FLAG_SCOPE_ID_SET = 0x00000080,
            FWPM_NET_EVENT_FLAG_IP_VERSION_SET = 0x00000100,
            FWPM_NET_EVENT_FLAG_REAUTH_REASON_SET = 0x00000200,
            FWPM_NET_EVENT_FLAG_PACKAGE_ID_SET = 0x00000400,
        }


        [StructLayout(LayoutKind.Sequential)]
        public struct FWPM_NET_EVENT_HEADER3
        {
            public FILETIME timeStamp; // Timestamp of the event
            public FirewallNetEventFlags flags; // Flags for the event
            public FWP_IP_VERSION_ ipVersion; // IP version (IPv4 or IPv6)
            public ipProtocol ipProtocol; // IP protocol number

            // Union representation for local address
            private uint localAddrV4; // Local IPv4 address
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            private byte[] localAddrV6; // Local IPv6 address

            // Union representation for remote address
            private uint remoteAddrV4; // Remote IPv4 address
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            private byte[] remoteAddrV6; // Remote IPv6 address


            public ushort localPort; // Local port number
            public ushort remotePort; // Remote port number
            public uint scopeId; // Scope ID for the event
            public FWP_BYTE_BLOB appId; // Application ID associated with the event
            public IntPtr userId; // Pointer to user SID (Security Identifier)

            public uint addressFamily; // Address family (e.g., AF_INET or AF_INET6)
            public IntPtr packageSid; // Pointer to package SID
            public IntPtr enterpriseId; // Pointer to enterprise ID (wchar_t*)
            public ulong policyFlags; // Policy flags associated with the event
            public FWP_BYTE_BLOB effectiveName; // Effective name associated with the event

            public IPAddress GetLocalAddr()
            {
                try
                {
                    if (ipVersion == FWP_IP_VERSION_.FWP_IP_VERSION_V4)
                        return new IPAddress(BitConverter.GetBytes(localAddrV4).Reverse().ToArray());
                    return new IPAddress(localAddrV6);
                }
                catch
                {
                    return null;
                }
            }

            //public void SetLocalAddrV4(uint addr)
            //{
            //    if (!(ipVersion == FWP_IP_VERSION_.FWP_IP_VERSION_V4))
            //        throw new InvalidOperationException("Local address is not IPv4.");
            //    localAddrV4 = addr;
            //}

            //public void SetLocalAddrV6(FWP_BYTE_ARRAY16 addr)
            //{
            //    if (ipVersion == FWP_IP_VERSION_.FWP_IP_VERSION_V4)
            //        throw new InvalidOperationException("Local address is not IPv6.");
            //    localAddrV6 = addr;
            //}

            public IPAddress GetRemoteAddr()
            {
                try
                {
                    if (ipVersion == FWP_IP_VERSION_.FWP_IP_VERSION_V4)
                        return new IPAddress(BitConverter.GetBytes(remoteAddrV4).Reverse().ToArray());
                    return new IPAddress(remoteAddrV6);
                }
                catch
                {
                    return null;
                }
                //throw new InvalidOperationException("Remote address is not IPv4.");                
            }

            //public void SetRemoteAddrV4(uint addr)
            //{
            //    if (!(ipVersion == FWP_IP_VERSION_.FWP_IP_VERSION_V4))
            //        throw new InvalidOperationException("Remote address is not IPv4.");
            //    remoteAddrV4 = addr;
            //}

            //public void SetRemoteAddrV6(FWP_BYTE_ARRAY16 addr)
            //{
            //    if (ipVersion == FWP_IP_VERSION_.FWP_IP_VERSION_V4)
            //        throw new InvalidOperationException("Remote address is not IPv6.");
            //    remoteAddrV6 = addr;
            //}

        }

        //[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        //struct FWPM_NET_EVENT_HEADER2_
        //{
        //    public FILETIME timeStamp;
        //    public FirewallNetEventFlags flags;
        //    public FWP_IP_VERSION_ ipVersion;
        //    public ipProtocol ipProtocol;
        //    public uint localAddrV4;
        //    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
        //    public byte[] localAddrV6;
        //    public uint remoteAddrV4;
        //    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
        //    public byte[] remoteAddrV6;
        //    public ushort localPort;
        //    public ushort remotePort;
        //    public uint scopeId;
        //    public FWP_BYTE_BLOB appId;
        //    public IntPtr userId;
        //    public FirewallAddressFamily addressFamily;
        //    public IntPtr packageSid;
        //}

        //[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        //struct FWPM_NET_EVENT_HEADER3_
        //{
        //    public FWPM_NET_EVENT_HEADER2_ Header2;
        //    [MarshalAs(UnmanagedType.LPWStr)]
        //    public string enterpriseId;
        //    public ulong policyFlags;
        //    public FWP_BYTE_BLOB effectiveName;
        //}

        public enum FWPM_NET_EVENT_TYPE
        {
            IKEEXT_MM_FAILURE = 0,
            IKEEXT_QM_FAILURE,
            IKEEXT_EM_FAILURE,
            CLASSIFY_DROP,
            IPSEC_KERNEL_DROP,
            IPSEC_DOSP_DROP,
            CLASSIFY_ALLOW,
            CAPABILITY_DROP,
            CAPABILITY_ALLOW,
            CLASSIFY_DROP_MAC,
            LPM_PACKET_ARRIVAL,
            MAX // This can be used for validation or limits
        }




        //public delegate void FWPM_NET_EVENT_CALLBACK4_(IntPtr context, ref FWPM_NET_EVENT5_ event)


        [StructLayout(LayoutKind.Sequential)]
        public struct FILETIME
        {
            public uint dwLowDateTime;
            public uint dwHighDateTime;

            public static FILETIME FromDateTime(DateTime dateTime)
            {
                long fileTime = dateTime.ToFileTime();
                return new FILETIME
                {
                    dwLowDateTime = (uint)(fileTime & 0xFFFFFFFF),
                    dwHighDateTime = (uint)(fileTime >> 32)
                };
            }

            public DateTime ToDateTime()
            {
                long fileTime = (long)dwHighDateTime << 32 | dwLowDateTime;
                return DateTime.FromFileTime(fileTime);
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct FWPM_NET_EVENT_ENUM_TEMPLATE0_
        {
            public FILETIME startTime;
            public FILETIME endTime;
            public uint numFilterConditions;
            public IntPtr filterCondition; // Pointer to FWPM_FILTER_CONDITION0
        }


        public enum FWP_DIRECTION
        {
            FWP_DIRECTION_OUTBOUND = 0,
            FWP_DIRECTION_INBOUND,
            FWP_DIRECTION_MAX
        }


        [StructLayout(LayoutKind.Sequential)]
        public struct FWPM_NET_EVENT_IPSEC_DOSP_DROP0
        {
            public FWP_IP_VERSION_ IpVersion;

            // Using an object to represent the union for publicHost
            private object _publicHost;

            public uint PublicHostV4Addr
            {
                get => _publicHost is uint ? (uint)_publicHost : 0;
                set => _publicHost = value;
            }

            public byte[] PublicHostV6Addr
            {
                get => _publicHost is byte[]? (byte[])_publicHost : new byte[16];
                set => _publicHost = value;
            }

            // Using an object to represent the union for internalHost
            private object _internalHost;

            public uint InternalHostV4Addr
            {
                get => _internalHost is uint ? (uint)_internalHost : 0;
                set => _internalHost = value;
            }

            public byte[] InternalHostV6Addr
            {
                get => _internalHost is byte[]? (byte[])_internalHost : new byte[16];
                set => _internalHost = value;
            }

            public int FailureStatus;
            public FWP_DIRECTION Direction;
        }

        
        public enum FirewallNetEventDirectionType
        {
            FWP_DIRECTION_IN = 0x00003900,
            FWP_DIRECTION_OUT = 0x00003901,
            FWP_DIRECTION_FORWARD = 0x00003902,
            Loopback = 0x00003903,
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct FWPM_NET_EVENT_CLASSIFY_DROP2
        {
            public ulong FilterId;              // UINT64
            public ushort LayerId;              // UINT16
            public uint ReauthReason;           // UINT32
            public uint OriginalProfile;        // UINT32
            public uint CurrentProfile;         // UINT32
            public FirewallNetEventDirectionType MsFwpDirection;         // UINT32
            [MarshalAs(UnmanagedType.I1)]       // BOOL is represented as a byte (true/false)
            public bool IsLoopback;             // BOOL
            public FWP_BYTE_BLOB VSwitchId;     // Custom struct for FWP_BYTE_BLOB
            public uint VSwitchSourcePort;      // UINT32
            public uint VSwitchDestinationPort; // UINT32
        }


        [StructLayout(LayoutKind.Sequential)]
        public struct FWPM_NET_EVENT_IPSEC_KERNEL_DROP0
        {
            public int FailureStatus;          // INT32
            public FWP_DIRECTION Direction;    // FWP_DIRECTION (enum)
            public uint Spi;                 // IPSEC_SA_SPI (custom struct or type)
            public ulong FilterId;             // UINT64
            public ushort LayerId;             // UINT16
        }


        [StructLayout(LayoutKind.Sequential)]
        public struct FWPM_NET_EVENT_CLASSIFY_ALLOW0
        {
            public ulong FilterId;            // UINT64
            public ushort LayerId;            // UINT16
            public uint ReauthReason;         // UINT32
            public uint OriginalProfile;      // UINT32
            public uint CurrentProfile;       // UINT32
            public FirewallNetEventDirectionType MsFwpDirection;       // UINT32
            [MarshalAs(UnmanagedType.I1)]     // BOOL is represented as a byte (true/false)
            public bool IsLoopback;           // BOOL
        }


        public enum FWPM_APPC_NETWORK_CAPABILITY_TYPE
        {
            // Define enum values based on your requirements
            CapabilityType1 = 0,
            CapabilityType2,
            CapabilityTypeMax
        }


        [StructLayout(LayoutKind.Sequential)]
        public struct FWPM_NET_EVENT_CAPABILITY_DROP0
        {
            public FWPM_APPC_NETWORK_CAPABILITY_TYPE NetworkCapabilityId; // Enum type
            public ulong FilterId;                                      // UINT64
            [MarshalAs(UnmanagedType.I1)]                              // BOOL is represented as a byte (true/false)
            public bool IsLoopback;                                    // BOOL
        }


        [StructLayout(LayoutKind.Sequential)]
        public struct FWPM_NET_EVENT_CAPABILITY_ALLOW0
        {
            public FWPM_APPC_NETWORK_CAPABILITY_TYPE NetworkCapabilityId; // Enum type
            public ulong FilterId;                                      // UINT64
            [MarshalAs(UnmanagedType.I1)]                              // BOOL is represented as a byte (true/false)
            public bool IsLoopback;                                    // BOOL
        }


        [StructLayout(LayoutKind.Sequential)]
        public struct FWPM_NET_EVENT_CLASSIFY_DROP_MAC0
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] LocalMacAddr;                // FWP_BYTE_ARRAY6
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] RemoteMacAddr;               // FWP_BYTE_ARRAY6
            public uint MediaType;                      // UINT32
            public uint IfType;                         // UINT32
            public ushort EtherType;                    // UINT16
            public uint NdisPortNumber;                 // UINT32
            public uint Reserved;                       // UINT32
            public ushort VlanTag;                      // UINT16
            public ulong IfLuid;                        // UINT64
            public ulong FilterId;                      // UINT64
            public ushort LayerId;                      // UINT16
            public uint ReauthReason;                   // UINT32
            public uint OriginalProfile;                // UINT32
            public uint CurrentProfile;                 // UINT32
            public FirewallNetEventDirectionType MsFwpDirection;                 // UINT32
            [MarshalAs(UnmanagedType.I1)]              // BOOL is represented as a byte (true/false)
            public bool IsLoopback;                     // BOOL
            public FWP_BYTE_BLOB VSwitchId;            // Custom struct for FWP_BYTE_BLOB
            public uint VSwitchSourcePort;              // UINT32
            public uint VSwitchDestinationPort;         // UINT32

        }

        [StructLayout(LayoutKind.Sequential)]
        public struct FWPM_NET_EVENT_LPM_PACKET_ARRIVAL0
        {
            public uint Spi; // Assuming IPSEC_SA_SPI is defined elsewhere
        }

        public enum IPSEC_FAILURE_POINT
        {
            IPSEC_FAILURE_NONE = 0,
            IPSEC_FAILURE_ME,
            IPSEC_FAILURE_PEER,
            IPSEC_FAILURE_POINT_MAX
        }

        public enum IKEEXT_EM_SA_STATE
        {
            IKEEXT_EM_SA_STATE_NONE = 0,
            IKEEXT_EM_SA_STATE_SENT_ATTS,
            IKEEXT_EM_SA_STATE_SSPI_SENT,
            IKEEXT_EM_SA_STATE_AUTH_COMPLETE,
            IKEEXT_EM_SA_STATE_FINAL,
            IKEEXT_EM_SA_STATE_COMPLETE,
            IKEEXT_EM_SA_STATE_MAX
        }

        public enum IKEEXT_SA_ROLE
        {
            IKEEXT_SA_ROLE_INITIATOR = 0,
            IKEEXT_SA_ROLE_RESPONDER,
            IKEEXT_SA_ROLE_MAX
        }

        public enum IPSEC_TRAFFIC_TYPE
        {
            IPSEC_TRAFFIC_TYPE_TRANSPORT = 0,
            IPSEC_TRAFFIC_TYPE_TUNNEL,
            IPSEC_TRAFFIC_TYPE_MAX
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct FWPM_NET_EVENT_IKEEXT_EM_FAILURE1
        {
            public uint FailureErrorCode;                              // UINT32
            public IPSEC_FAILURE_POINT FailurePoint;                   // IPSEC_FAILURE_POINT (enum or struct)
            public uint Flags;                                         // UINT32
            public IKEEXT_EM_SA_STATE EmState;                        // IKEEXT_EM_SA_STATE (enum or struct)
            public IKEEXT_SA_ROLE SaRole;                             // IKEEXT_SA_ROLE (enum)
            public IKEEXT_AUTHENTICATION_METHOD_TYPE_ EmAuthMethod;   // IKEEXT_AUTHENTICATION_METHOD_TYPE (enum)

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EndCertHash;                                // UINT8 endCertHash[20]

            public ulong MmId;                                        // UINT64
            public ulong QmFilterId;                                  // UINT64
            [MarshalAs(UnmanagedType.LPWStr)]                         // wchar_t* localPrincipalNameForAuth
            public string LocalPrincipalNameForAuth;

            [MarshalAs(UnmanagedType.LPWStr)]                         // wchar_t* remotePrincipalNameForAuth
            public string RemotePrincipalNameForAuth;

            public uint NumLocalPrincipalGroupSids;                  // UINT32
            public IntPtr LocalPrincipalGroupSids;                    // LPWSTR* localPrincipalGroupSids (pointer to array of strings)

            public uint NumRemotePrincipalGroupSids;                 // UINT32
            public IntPtr RemotePrincipalGroupSids;                   // LPWSTR* remotePrincipalGroupSids (pointer to array of strings)

            public IPSEC_TRAFFIC_TYPE SaTrafficType;                  // IPSEC_TRAFFIC_TYPE (enum or struct)
        }

        public enum IKEEXT_QM_SA_STATE
        {
            IKEEXT_QM_SA_STATE_NONE = 0,
            IKEEXT_QM_SA_STATE_INITIAL,
            IKEEXT_QM_SA_STATE_FINAL,
            IKEEXT_QM_SA_STATE_COMPLETE,
            IKEEXT_QM_SA_STATE_MAX
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct FWP_CONDITION_VALUE0
        {
            public FWP_DATA_TYPE_ Type; // FWP_DATA_TYPE (enum or struct)

            // Union simulation using explicit layout
            [StructLayout(LayoutKind.Explicit)]
            public struct ValueUnion
            {
                [FieldOffset(0)]
                public byte UInt8;                          // UINT8

                [FieldOffset(1)]
                public ushort UInt16;                       // UINT16

                [FieldOffset(2)]
                public uint UInt32;                         // UINT32

                [FieldOffset(4)]
                public IntPtr UInt64;                       // UINT64* (pointer)

                [FieldOffset(12)]
                public sbyte Int8;                          // INT8

                [FieldOffset(13)]
                public short Int16;                         // INT16

                [FieldOffset(15)]
                public int Int32;                           // INT32

                [FieldOffset(19)]
                public IntPtr Int64;                        // INT64* (pointer)

                [FieldOffset(27)]
                public float Float32;                       // float

                [FieldOffset(31)]
                public IntPtr Double64;                     // double* (pointer)

                [FieldOffset(39)]
                public IntPtr ByteArray16;                  // FWP_BYTE_ARRAY16* (pointer)

                [FieldOffset(43)]
                public IntPtr ByteBlob;                     // FWP_BYTE_BLOB* (pointer)

                [FieldOffset(47)]
                public IntPtr Sid;                          // SID* (pointer)

                [FieldOffset(51)]
                public IntPtr Sd;                           // FWP_BYTE_BLOB* (pointer)

                [FieldOffset(55)]
                public IntPtr TokenInformation;             // FWP_TOKEN_INFORMATION* (pointer)

                [FieldOffset(59)]
                public IntPtr TokenAccessInformation;       // FWP_BYTE_BLOB* (pointer)

                [FieldOffset(63)]
                public IntPtr UnicodeString;                // LPWSTR (pointer)

                [FieldOffset(67)]
                public IntPtr ByteArray6;                   // FWP_BYTE_ARRAY6* (pointer)

                [FieldOffset(71)]
                public IntPtr V4AddrMask;                   // FWP_V4_ADDR_AND_MASK* (pointer)

                [FieldOffset(75)]
                public IntPtr V6AddrMask;                   // FWP_V6_ADDR_AND_MASK* (pointer)

                [FieldOffset(79)]
                public IntPtr RangeValue;                   // FWP_RANGE0* (pointer)
            }

            public ValueUnion Value;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct FWPM_NET_EVENT_IKEEXT_QM_FAILURE1
        {
            public uint FailureErrorCode;                          // UINT32
            public IPSEC_FAILURE_POINT FailurePoint;               // IPSEC_FAILURE_POINT (enum or struct)
            public IKEEXT_KEY_MODULE_TYPE_ KeyingModuleType;       // IKEEXT_KEY_MODULE_TYPE (enum or struct)
            public IKEEXT_QM_SA_STATE QmState;                     // IKEEXT_QM_SA_STATE (enum or struct)
            public IKEEXT_SA_ROLE SaRole;                          // IKEEXT_SA_ROLE (enum)
            public IPSEC_TRAFFIC_TYPE SaTrafficType;               // IPSEC_TRAFFIC_TYPE (enum)

            // Simulating union for localSubNet
            [StructLayout(LayoutKind.Explicit)]
            public struct LocalSubNetUnion
            {
                [FieldOffset(0)]
                public FWP_CONDITION_VALUE0 LocalSubNet;            // FWP_CONDITION_VALUE0 (struct)
            }

            public LocalSubNetUnion LocalSubNet;

            // Simulating union for remoteSubNet
            [StructLayout(LayoutKind.Explicit)]
            public struct RemoteSubNetUnion
            {
                [FieldOffset(0)]
                public FWP_CONDITION_VALUE0 RemoteSubNet;           // FWP_CONDITION_VALUE0 (struct)
            }

            public RemoteSubNetUnion RemoteSubNet;

            public ulong QmFilterId;                               // UINT64
            public ulong MmSaLuid;                                 // UINT64
            public Guid MmProviderContextKey;                      // GUID
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct FWPM_NET_EVENT5_
        {
            public FWPM_NET_EVENT_HEADER3 header; // The header of the event
            public FWPM_NET_EVENT_TYPE type;       // The type of the event

            // Union representation using an object for flexibility
            private IntPtr unionPointer; // Pointer to the union data

            public object GetEventData()
            {
                switch (type)
                {
                    //case FWPM_NET_EVENT_TYPE.IKEEXT_MM_FAILURE:
                    //    return Marshal.PtrToStructure(unionPointer, typeof(FWPM_NET_EVENT_IKEEXT_MM_FAILURE2));
                    case FWPM_NET_EVENT_TYPE.IKEEXT_QM_FAILURE:
                        return Marshal.PtrToStructure(unionPointer, typeof(FWPM_NET_EVENT_IKEEXT_QM_FAILURE1));
                    case FWPM_NET_EVENT_TYPE.IKEEXT_EM_FAILURE:
                        return Marshal.PtrToStructure(unionPointer, typeof(FWPM_NET_EVENT_IKEEXT_EM_FAILURE1));
                    case FWPM_NET_EVENT_TYPE.CLASSIFY_DROP:
                        return Marshal.PtrToStructure(unionPointer, typeof(FWPM_NET_EVENT_CLASSIFY_DROP2));
                    case FWPM_NET_EVENT_TYPE.IPSEC_KERNEL_DROP:
                        return Marshal.PtrToStructure(unionPointer, typeof(FWPM_NET_EVENT_IPSEC_KERNEL_DROP0));
                    case FWPM_NET_EVENT_TYPE.IPSEC_DOSP_DROP:
                        return Marshal.PtrToStructure(unionPointer, typeof(FWPM_NET_EVENT_IPSEC_DOSP_DROP0));
                    case FWPM_NET_EVENT_TYPE.CLASSIFY_ALLOW:
                        return Marshal.PtrToStructure(unionPointer, typeof(FWPM_NET_EVENT_CLASSIFY_ALLOW0));
                    case FWPM_NET_EVENT_TYPE.CAPABILITY_DROP:
                        return Marshal.PtrToStructure(unionPointer, typeof(FWPM_NET_EVENT_CAPABILITY_DROP0));
                    case FWPM_NET_EVENT_TYPE.CAPABILITY_ALLOW:
                        return Marshal.PtrToStructure(unionPointer, typeof(FWPM_NET_EVENT_CAPABILITY_ALLOW0));
                    case FWPM_NET_EVENT_TYPE.CLASSIFY_DROP_MAC:
                        return Marshal.PtrToStructure(unionPointer, typeof(FWPM_NET_EVENT_CLASSIFY_DROP_MAC0));
                    case FWPM_NET_EVENT_TYPE.LPM_PACKET_ARRIVAL:
                        return Marshal.PtrToStructure(unionPointer, typeof(FWPM_NET_EVENT_LPM_PACKET_ARRIVAL0));
                    default:
                        throw new InvalidOperationException("Unknown event type.");
                }
            }
        }


        [DllImport("FWPUClnt.dll")]
        public static extern uint FwpmProviderGetByKey0(
           IntPtr engineHandle,
           ref Guid key,
           out IntPtr provider);

        [DllImport("FWPUClnt.dll")]
        public static extern uint FwpmFilterGetByKey0(
           IntPtr engineHandle,
           ref Guid key,
           out IntPtr filter);

        [DllImport("FWPUClnt.dll")]
        public static extern uint FwpmSubLayerGetByKey0(
           IntPtr engineHandle,
           ref Guid key,
           out IntPtr subLayer);

        [DllImport("FWPUClnt.dll")]
        public static extern uint FwpmCalloutGetByKey0(
           IntPtr engineHandle,
           ref Guid key,
           out IntPtr callout);


        [DllImport("FWPUClnt.dll")]
        public static extern uint FwpmFilterGetById0(
           IntPtr engineHandle,
           uint id,
           out IntPtr filter);

        [DllImport("FWPUClnt.dll")]
        public static extern uint FwpmCalloutGetById0(
           IntPtr engineHandle,
           uint id,
           out IntPtr callout);


        public delegate void FWPM_CONNECTION_CALLBACK0_(IntPtr context, FWPM_CONNECTION_EVENT_TYPE_ eventType, in FWPM_CONNECTION0_ connection);

        [DllImport("fwpuclnt.dll", SetLastError = true)]
        public static extern uint FwpmConnectionSubscribe0(
            IntPtr engineHandle,
            ref FWPM_CONNECTION_SUBSCRIPTION0_ subscription,
            FWPM_CONNECTION_CALLBACK0_ callback,
            IntPtr context,
            out IntPtr eventsHandle);

        [StructLayout(LayoutKind.Sequential)]
        public struct FWPM_CONNECTION_ENUM_TEMPLATE0_
        {
            public ulong connectionId; // UINT64 corresponds to ulong in C#
            public uint flags;         // UINT32 corresponds to uint in C#
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct FWPM_CONNECTION_SUBSCRIPTION0_
        {
            public FWPM_CONNECTION_ENUM_TEMPLATE0_ enumTemplate; // Pointer to FWPM_CONNECTION_ENUM_TEMPLATE0, use IntPtr for pointers
            public uint flags;          // UINT32 corresponds to uint in C#
            public IntPtr sessionKey;     // GUID corresponds to Guid in C#
        }

        public enum FWPM_CONNECTION_EVENT_TYPE_
        {
            FWPM_CONNECTION_EVENT_ADD = 0,
            FWPM_CONNECTION_EVENT_DELETE,
            FWPM_CONNECTION_EVENT_MAX
        }

        public delegate void FWPM_CALLOUT_CHANGE_CALLBACK0_(IntPtr context, FWPM_CALLOUT_CHANGE0_ eventData);

        [DllImport("FWPUClnt.dll", SetLastError = true)]
        public static extern uint FwpmCalloutSubscribeChanges0(
            IntPtr engineHandle,
            ref FWPM_CALLOUT_SUBSCRIPTION0_ subscription,
            FWPM_CALLOUT_CHANGE_CALLBACK0_ callback,
            IntPtr context,
            out IntPtr changeHandle);

        [StructLayout(LayoutKind.Sequential)]
        public struct FWPM_CALLOUT_ENUM_TEMPLATE0_
        {
            public IntPtr providerKey; // Pointer to GUID, use IntPtr for pointers
            public Guid layerKey;       // GUID corresponds to Guid in C#
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct FWPM_CALLOUT_SUBSCRIPTION0_
        {
            public IntPtr enumTemplate; // Pointer to FWPM_CALLOUT_ENUM_TEMPLATE0, use IntPtr for pointers
            public FirewallSubscriptionFlags flags;          // UINT32 corresponds to uint in C#
            public IntPtr sessionKey;     // GUID corresponds to Guid in C#
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct FWPM_CALLOUT_CHANGE0_
        {
            public FWPM_CHANGE_TYPE_ changeType; 
            public Guid calloutKey;
            public uint calloutId; // UINT32 
        }


        public delegate void FWPM_SUBLAYER_CHANGE_CALLBACK0_(IntPtr context, FWPM_SUBLAYER_CHANGE0_ eventData);

        [DllImport("FWPUClnt.dll", SetLastError = true)]
        public static extern uint FwpmSubLayerSubscribeChanges0(
            IntPtr engineHandle,
            ref FWPM_SUBLAYER_SUBSCRIPTION0_ subscription,
            FWPM_SUBLAYER_CHANGE_CALLBACK0_ callback,
            IntPtr context, // Optional: can be IntPtr.Zero if not used
            out IntPtr changeHandle
        );

        [StructLayout(LayoutKind.Sequential)]
        public struct FWPM_SUBLAYER_CHANGE0_
        {
            public FWPM_CHANGE_TYPE_ changeType;
            public Guid subLayerKey;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct FWPM_SUBLAYER_SUBSCRIPTION0_
        {
            public FWPM_SUBLAYER_ENUM_TEMPLATE0_ enumTemplate; // Pointer to FWPM_SUBLAYER_ENUM_TEMPLATE0
            public FirewallSubscriptionFlags flags;          // UINT32
            public IntPtr sessionKey;     // GUID
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct FWPM_SUBLAYER_ENUM_TEMPLATE0_
        {
            public IntPtr providerKey; // Pointer to GUID
        }

        public delegate void FWPM_PROVIDER_CHANGE_CALLBACK0_ (IntPtr context, FWPM_PROVIDER_CHANGE0_ eventData);

        [DllImport("FWPUClnt.dll", SetLastError = true)]
        public static extern uint FwpmProviderSubscribeChanges0(
            IntPtr engineHandle,
            ref FWPM_PROVIDER_SUBSCRIPTION0_ subscription, // Optional: can be IntPtr.Zero if not used
            FWPM_PROVIDER_CHANGE_CALLBACK0_ callback,
            IntPtr context, // Optional: can be IntPtr.Zero if not used
            out IntPtr changeHandle
        );

        [StructLayout(LayoutKind.Sequential)]
        public struct FWPM_PROVIDER_ENUM_TEMPLATE0_
        {
            public ulong reserved; // UINT64
        }

        public enum FWPM_CHANGE_TYPE_ : uint
        {
            FWPM_CHANGE_ADD = 1,
            FWPM_CHANGE_DELETE,
            FWPM_CHANGE_TYPE_MAX
        }


        [StructLayout(LayoutKind.Sequential)]
        public struct FWPM_PROVIDER_CHANGE0_
        {
            public FWPM_CHANGE_TYPE_ changeType; // Enum type for change type
            public Guid providerKey;            // GUID for provider key
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct FWPM_PROVIDER_SUBSCRIPTION0_
        {
            public FWPM_PROVIDER_ENUM_TEMPLATE0_ enumTemplate; // Pointer to FWPM_PROVIDER_ENUM_TEMPLATE0
            public FirewallSubscriptionFlags flags;          // UINT32
            public IntPtr sessionKey;     // GUID
        }

        public delegate void FWPM_FILTER_CHANGE_CALLBACK0_ (IntPtr context, FWPM_FILTER_CHANGE0_ eventData);

        [DllImport("FWPUClnt.dll", SetLastError = true)]
        public static extern uint FwpmFilterSubscribeChanges0(
            IntPtr engineHandle,
            ref FWPM_FILTER_SUBSCRIPTION0_ subscription,
            FWPM_FILTER_CHANGE_CALLBACK0_ callback,
            IntPtr context,
            out IntPtr changeHandle
        );

        [Flags]
        public enum FirewallFilterEnumFlags
        {
            None = 0,
            FWP_FILTER_ENUM_FLAG_BEST_TERMINATING_MATCH = 0x00000001,
            FWP_FILTER_ENUM_FLAG_SORTED = 0x00000002,
            FWP_FILTER_ENUM_FLAG_BOOTTIME_ONLY = 0x00000004,
            FWP_FILTER_ENUM_FLAG_INCLUDE_BOOTTIME = 0x00000008,
            FWP_FILTER_ENUM_FLAG_INCLUDE_DISABLED = 0x00000010,
            ReseFWP_FILTER_ENUM_FLAG_RESERVED1rved1 = 0x00000020,
        }

        public enum FWPM_PROVIDER_CONTEXT_TYPE_ : uint
        {
            FWPM_IPSEC_KEYING_CONTEXT = 0,
            FWPM_IPSEC_IKE_QM_TRANSPORT_CONTEXT,
            FWPM_IPSEC_IKE_QM_TUNNEL_CONTEXT,
            FWPM_IPSEC_AUTHIP_QM_TRANSPORT_CONTEXT,
            FWPM_IPSEC_AUTHIP_QM_TUNNEL_CONTEXT,
            FWPM_IPSEC_IKE_MM_CONTEXT,
            FWPM_IPSEC_AUTHIP_MM_CONTEXT,
            FWPM_CLASSIFY_OPTIONS_CONTEXT,
            FWPM_GENERAL_CONTEXT,
            FWPM_IPSEC_IKEV2_QM_TUNNEL_CONTEXT,
            FWPM_IPSEC_IKEV2_MM_CONTEXT,
            FWPM_IPSEC_DOSP_CONTEXT,
            FWPM_IPSEC_IKEV2_QM_TRANSPORT_CONTEXT,
            FWPM_NETWORK_CONNECTION_POLICY_CONTEXT,
            FWPM_PROVIDER_CONTEXT_TYPE_MAX
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct FWPM_PROVIDER_CONTEXT_ENUM_TEMPLATE0_
        {
            public IntPtr providerKey; // Pointer to GUID
            public FWPM_PROVIDER_CONTEXT_TYPE_ providerContextType; // Enum type
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct FWPM_FILTER_ENUM_TEMPLATE0_
        {
            public IntPtr providerKey; // Pointer to GUID
            public Guid layerKey;      // GUID
            public FWP_FILTER_ENUM_TYPE_ enumType; // Enum type
            public FirewallFilterEnumFlags flags;         // UINT32
            public FWPM_PROVIDER_CONTEXT_ENUM_TEMPLATE0_ providerContextTemplate; // Pointer to FWPM_PROVIDER_CONTEXT_ENUM_TEMPLATE0
            public uint numFilterConditions; // UINT32
            public FWPM_FILTER_CONDITION0_ filterCondition; // Pointer to FWPM_FILTER_CONDITION0
            public uint actionMask;     // UINT32 FWP_ACTION
        }


        [StructLayout(LayoutKind.Sequential)]
        public struct FWPM_FILTER_SUBSCRIPTION0_
        {
            public IntPtr enumTemplate; // Pointer to FWPM_FILTER_ENUM_TEMPLATE0
            public FirewallSubscriptionFlags flags;          // UINT32
            public IntPtr sessionKey;     // GUID
        }



        [StructLayout(LayoutKind.Sequential)]
        public struct FWPM_FILTER_CHANGE0_
        {
            public FWPM_CHANGE_TYPE_ changeType;
            public Guid filterKey;
            public ulong filterId;
        }

        public struct FirewallObjectSecurityInfo
        {
            public Guid key;
            public IntPtr sidOwner;
            public IntPtr sidGroup;
            public IntPtr dacl;
            public IntPtr sacl;
            public IntPtr securityDescriptor;

        }

        public const uint OWNER_SECURITY_INFORMATION = 0x00000001;
        public const uint GROUP_SECURITY_INFORMATION = 0x00000002;
        public const uint DACL_SECURITY_INFORMATION = 0x00000004;
        public const uint SACL_SECURITY_INFORMATION = 0x00000008;

        // Define the SECURITY_INFORMATION enum
        [Flags]
        public enum SECURITY_INFORMATION : uint
        {
            Owner = OWNER_SECURITY_INFORMATION,
            Group = GROUP_SECURITY_INFORMATION,
            Dacl = DACL_SECURITY_INFORMATION,
            Sacl = SACL_SECURITY_INFORMATION
        }


        [DllImport("FWPUClnt.dll", SetLastError = true)]
        public static extern uint FwpmFilterGetSecurityInfoByKey0(
            IntPtr engineHandle,
            ref Guid key,
            SECURITY_INFORMATION securityInfo,
            out IntPtr sidOwner,
            out IntPtr sidGroup,
            out IntPtr dacl,
            out IntPtr sacl,
            out IntPtr securityDescriptor);


        [DllImport("FWPUClnt.dll", SetLastError = true)]
        public static extern uint FwpmSubLayerGetSecurityInfoByKey0(
            IntPtr engineHandle,
            ref Guid key,
            SECURITY_INFORMATION securityInfo,
            out IntPtr sidOwner,
            out IntPtr sidGroup,
            out IntPtr dacl,
            out IntPtr sacl,
            out IntPtr securityDescriptor);


        [DllImport("FWPUClnt.dll", SetLastError = true)]
        public static extern uint FwpmProviderGetSecurityInfoByKey0(
            IntPtr engineHandle,
            ref Guid key,
            SECURITY_INFORMATION securityInfo,
            out IntPtr sidOwner,
            out IntPtr sidGroup,
            out IntPtr dacl,
            out IntPtr sacl,
            out IntPtr securityDescriptor);


        [DllImport("FWPUClnt.dll", SetLastError = true)]
        public static extern uint FwpmCalloutGetSecurityInfoByKey0(
            IntPtr engineHandle,
            ref Guid key,
            SECURITY_INFORMATION securityInfo,
            out IntPtr sidOwner,
            out IntPtr sidGroup,
            out IntPtr dacl,
            out IntPtr sacl,
            out IntPtr securityDescriptor);


        [DllImport("FWPUClnt.dll", SetLastError = true)]
        public static extern uint FwpmConnectionGetSecurityInfo0(
            IntPtr engineHandle,
            SECURITY_INFORMATION securityInfo,
            out IntPtr sidOwner,
            out IntPtr sidGroup,
            out IntPtr dacl,
            out IntPtr sacl,
            out IntPtr securityDescriptor);


        [StructLayout(LayoutKind.Sequential)]
        public struct ACL
        {
            public byte AclRevision;
            public byte Sbz1;
            public ushort AclSize;
            public ushort AceCount;
            public ushort Sbz2;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct ACE_HEADER    
        {
            public byte AceType;       // Type of ACE
            public AceFlags AceFlags;      // Flags that control inheritance
            public ushort AceSize;     // Size of the ACE in bytes
        }



        [StructLayout(LayoutKind.Sequential)]
        public struct ACL_REVISION_2_HEADER
        {
            public byte AclRevision;
            public byte Sbz1;
            public ushort AclSize;
            public ushort AceCount;
            public ushort Sbz2;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct ACCESS_ALLOWED_ACE
        {
            public ACE_HEADER Header;   // ACE header
            public uint Mask;           // Access rights granted by this ACE
            public uint SidStart;       // Start of the SID (first DWORD)
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct ACCESS_DENIED_ACE
        {
            public ACE_HEADER Header;   // ACE header
            public uint Mask;           // Access rights granted by this ACE
            public uint SidStart;       // Start of the SID (first DWORD)
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct ACCESS_ALLOWED_OBJECT_ACE
        {
            public ACE_HEADER Header;
            public uint Mask;
            public ushort Flags;
            public Guid ObjectType;
            public Guid InheritedObjectType;
            public uint SidStart;
        }


        //[Flags]
        //public enum AceFlags : byte
        //{
        //    OBJECT_INHERIT_ACE = 0x01,
        //    CONTAINER_INHERIT_ACE = 0x02,
        //    NO_PROPAGATE_INHERIT_ACE = 0x04,
        //    INHERIT_ONLY_ACE = 0x08,
        //    INHERITED_ACE = 0x10,
        //    SUCCESSFUL_ACCESS_ACE_FLAG = 0x40,
        //    FAILED_ACCESS_ACE_FLAG = 0x80,
        //}

        [Flags]
        public enum AccessMask : uint
        {
            DELETE = 0x00010000,
            READ_CONTROL = 0x00020000,
            WRITE_DAC = 0x00040000,
            WRITE_OWNER = 0x00080000,
            SYNCHRONIZE = 0x00100000,
            STANDARD_RIGHTS_REQUIRED = 0x000F0000,
            STANDARD_RIGHTS_READ = 0x00020000,
            STANDARD_RIGHTS_WRITE = 0x00020000,
            STANDARD_RIGHTS_EXECUTE = 0x00020000,
            STANDARD_RIGHTS_ALL = 0x001F0000,
            SPECIFIC_RIGHTS_ALL = 0x0000FFFF,
            ACCESS_SYSTEM_SECURITY = 0x01000000,
            MAXIMUM_ALLOWED = 0x02000000,
            GENERIC_READ = 0x80000000,
            GENERIC_WRITE = 0x40000000,
            GENERIC_EXECUTE = 0x20000000,
            GENERIC_ALL = 0x10000000,
        }

        [DllImport("FWPUClnt.dll", SetLastError = true)]
        public static extern uint FwpmProviderUnsubscribeChanges0(
            IntPtr engineHandle,
            IntPtr changeHandle);

        [DllImport("FWPUClnt.dll", SetLastError = true)]
        public static extern uint FwpmCalloutUnsubscribeChanges0(
            IntPtr engineHandle,
            IntPtr changeHandle);

        [DllImport("FWPUClnt.dll", SetLastError = true)]
        public static extern uint FwpmConnectionUnsubscribe0(
            IntPtr engineHandle,
            IntPtr changeHandle);

        [DllImport("FWPUClnt.dll", SetLastError = true)]
        public static extern uint FwpmFilterUnsubscribeChanges0(
            IntPtr engineHandle,
            IntPtr changeHandle);

        [DllImport("FWPUClnt.dll", SetLastError = true)]
        public static extern uint FwpmSubLayerUnsubscribeChanges0(
            IntPtr engineHandle,
            IntPtr changeHandle);

        [Flags]
        public enum FirewallSubscriptionFlags
        {
            None = 0,
            FWPM_SUBSCRIPTION_FLAG_NOTIFY_ON_ADD = 0x00000001,
            FWPM_SUBSCRIPTION_FLAG_NOTIFY_ON_DELETE = 0x00000002
        }

    }
}
