using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using NativeAPI;

namespace Win32Helper
{
    internal class Win32Helper
    {
        public static (int ErrorCode, string ErrorMessage) GetLastError(string operationName = "")
        {
            int errorCode = Marshal.GetLastWin32Error();
            string errorMessage = new Win32Exception(errorCode).Message;

            Console.WriteLine($"Win32 Error in {operationName}:");
            Console.WriteLine($"Error Code: {errorCode}");
            Console.WriteLine($"Error Message: {errorMessage}");

            return (errorCode, errorMessage);
        }

        
    }

    public class NativeException : Exception
    {
        public NativeException(string method, uint code) : base($"Method {method} returned error code 0x{code:X8}")
        {

        }
    }

    
}
