using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Win32Helper;
using static NativeAPI.WfpNativeAPI;

namespace WfpClient
{
    internal class WfpConditionBuilder
    {
        // Conditions correspons to layes. Condition should be picked up according to layer
        // Not all conditions work with each layer
        // see for ditails:
        // https://learn.microsoft.com/en-us/windows/win32/fwp/filtering-conditions-available-at-each-filtering-layer

        private List<FWPM_FILTER_CONDITION0_> conditions = new List<FWPM_FILTER_CONDITION0_>();
        public int Length
        {
            get => conditions.Count();
        }


        public enum TARGET
        {
            LOCAL,
            REMOTE
        }

        public IntPtr ToPointer()
        {
            var cond = conditions.ToArray<FWPM_FILTER_CONDITION0_>();
            IntPtr pConditionArray = Marshal.AllocHGlobal(cond.Length * Marshal.SizeOf<FWPM_FILTER_CONDITION0_>());
            for (int i = 0; i < cond.Length; i++)
            {
                Marshal.StructureToPtr(cond[i],
                    pConditionArray + (i * Marshal.SizeOf<FWPM_FILTER_CONDITION0_>()), false);
            }
            return pConditionArray;
        }

        public void IPRule(int ipAddress, TARGET target)
        {
            var condition = new FWPM_FILTER_CONDITION0_();
            if (target == TARGET.LOCAL)
                condition.fieldKey = FWPM_CONDITION_IP_REMOTE_ADDRESS;
            else
                condition.fieldKey = FWPM_CONDITION_IP_LOCAL_ADDRESS;
            condition.matchType = FWP_MATCH_TYPE_.FWP_MATCH_EQUAL; 
            condition.conditionValue.type = FWP_DATA_TYPE_.FWP_UINT32; 
            condition.conditionValue.value.uint32 = (uint)ipAddress; 

            conditions.Add(condition);
        }

        public void PortRule(short portNumber, TARGET target)
        {
            var condition = new FWPM_FILTER_CONDITION0_();
            if (target == TARGET.LOCAL)
                condition.fieldKey = FWPM_CONDITION_IP_REMOTE_PORT;
            else
                condition.fieldKey = FWPM_CONDITION_IP_LOCAL_PORT;
            condition.matchType = FWP_MATCH_TYPE_.FWP_MATCH_EQUAL;
            condition.conditionValue.type = FWP_DATA_TYPE_.FWP_UINT32;
            condition.conditionValue.value.uint32 = (uint)portNumber;

            conditions.Add(condition);
        }

        public void ApplicationRule(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException(filePath);

            IntPtr appIdPtr;

            uint code = FwpmGetAppIdFromFileName0(filePath, out appIdPtr);
            if (code != 0)
            {
                throw new NativeException("FwpmGetAppIdFromFileName0", code);
            }

            // appidPtr to struct
            FWP_BYTE_BLOB appId = Marshal.PtrToStructure<FWP_BYTE_BLOB>(appIdPtr);
            

            var condition = new FWPM_FILTER_CONDITION0_();
            condition.fieldKey = FWPM_CONDITION_ALE_APP_ID;
            condition.matchType = FWP_MATCH_TYPE_.FWP_MATCH_EQUAL;
            condition.conditionValue.type = FWP_DATA_TYPE_.FWP_BYTE_BLOB_TYPE;
            condition.conditionValue.value.byteBlob = appIdPtr;

            conditions.Add(condition);
        }

    }
}
