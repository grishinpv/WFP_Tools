using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static NativeAPI.WfpNativeAPI;
using static Win32Helper.Win32Helper;

namespace Wfp
{
    public partial class WfpClient
    {
        public IEnumerable<FWPM_SESSION0_> GetSessions(string nameFilter = "")
        {
            return GetItems<FWPM_SESSION0_>(nameFilter);
        }


        
    }
}
