using NativeAPI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using static NativeAPI.WfpNativeAPI;

namespace poc_WFP_disable
{
    internal static class Helper
    {

        public enum WFP_OBJECTS
        {
            SESSION = 0,
            PROVIDER,
            CALLOUT,
            CONNECTION,
            FILTER,
            SUBLAYER
        }

        public static int ConvertIPv4AddressToInt(IPAddress address)
        {
            byte[] ip_bytes = address.GetAddressBytes();
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(ip_bytes);
            }
            return (int)(BitConverter.ToUInt32(ip_bytes, 0));
        }

        public static List<Dictionary<string, string>> GetDacl(IntPtr dacl)
        {
            List<Dictionary<string, string>> DaclValues = new List<Dictionary<string, string>>();

            if (dacl != IntPtr.Zero)
            {
                ACL_REVISION_2_HEADER daclHeader = (ACL_REVISION_2_HEADER)Marshal.PtrToStructure(dacl, typeof(ACL_REVISION_2_HEADER));

                SecurityIdentifier sid;


                if (daclHeader.AclRevision == 2)
                {
                    int aceOffset = Marshal.SizeOf(typeof(ACL_REVISION_2_HEADER));

                    for (int i = 0; i < daclHeader.AceCount; i++)
                    {
                        Dictionary<string, string> ace_item = new Dictionary<string, string>();

                        byte aceType = Marshal.ReadByte(dacl, aceOffset);

                        switch ((System.Security.AccessControl.AceType)aceType)
                        {
                            case AceType.AccessAllowed:
                                ACCESS_ALLOWED_ACE allowedAce = (ACCESS_ALLOWED_ACE)Marshal.PtrToStructure(new IntPtr(dacl.ToInt64() + aceOffset), typeof(ACCESS_ALLOWED_ACE));

                                ace_item.Add("ACE Type", "ACCESS_ALLOWED_ACE");
                                ace_item.Add("AceFlags", allowedAce.Header.AceFlags.ToString());
                                ace_item.Add("Mask", Helper.AccessMaskConverter.AccessMaskToString(allowedAce.Mask));
                                sid = new SecurityIdentifier(IntPtr.Add(new IntPtr(dacl.ToInt64() + aceOffset), Marshal.SizeOf(typeof(ACE_HEADER)) + Marshal.SizeOf(typeof(uint))));
                                ace_item.Add("SID", sid.ToString());
                                try
                                {
                                    ace_item.Add("User", sid.Translate(typeof(System.Security.Principal.NTAccount)).ToString());
                                }
                                catch (Exception ex)
                                {
                                    ace_item.Add("User", $"< Error>: {ex.Message}");
                                }
                                DaclValues.Add(ace_item);


                                //WriteLineToConsole($"  AceFlags: {allowedAce.Header.AceFlags}");
                                //WriteLineToConsole($"  Mask: {Helper.AccessMaskConverter.AccessMaskToString(allowedAce.Mask)}");

                                //WriteLineToConsole($"  Sid: {sid}");
                                //try
                                //{
                                //    WriteLineToConsole($"  User: {sid.Translate(typeof(System.Security.Principal.NTAccount))}");
                                //}
                                //catch (Exception ex)
                                //{
                                //    WriteLineToConsole($"  User: <Error>: {ex.Message}");
                                //}


                                aceOffset += allowedAce.Header.AceSize;
                                break;

                            case AceType.AccessDenied:
                                ACCESS_DENIED_ACE deniedAce = (ACCESS_DENIED_ACE)Marshal.PtrToStructure(new IntPtr(dacl.ToInt64() + aceOffset), typeof(ACCESS_DENIED_ACE));

                                ace_item.Add("ACE Type", "ACCESS_ALLOWED_ACE");
                                ace_item.Add("AceFlags", deniedAce.Header.AceFlags.ToString());
                                ace_item.Add("Mask", Helper.AccessMaskConverter.AccessMaskToString(deniedAce.Mask));
                                sid = new SecurityIdentifier(IntPtr.Add(new IntPtr(dacl.ToInt64() + aceOffset), Marshal.SizeOf(typeof(ACE_HEADER)) + Marshal.SizeOf(typeof(uint))));
                                ace_item.Add("SID", sid.ToString());
                                try
                                {
                                    ace_item.Add("User", sid.Translate(typeof(System.Security.Principal.NTAccount)).ToString());
                                }
                                catch (Exception ex)
                                {
                                    ace_item.Add("User", $"< Error>: {ex.Message}");
                                }
                                DaclValues.Add(ace_item);

                                //WriteLineToConsole("ACCESS_ALLOWED_ACE");
                                //WriteLineToConsole($"  AceFlags: {deniedAce.Header.AceFlags}");
                                //WriteLineToConsole($"  Mask: {Helper.AccessMaskConverter.AccessMaskToString(deniedAce.Mask)}");
                                //sid = new SecurityIdentifier(IntPtr.Add(new IntPtr(dacl.ToInt64() + aceOffset), Marshal.SizeOf(typeof(ACE_HEADER)) + Marshal.SizeOf(typeof(uint))));
                                //WriteLineToConsole($"  Sid: {sid}");
                                //try
                                //{
                                //    WriteLineToConsole($"  User: {sid.Translate(typeof(System.Security.Principal.NTAccount))}");
                                //}
                                //catch (Exception ex)
                                //{
                                //    WriteLineToConsole($"  User: <Error>: {ex.Message}");
                                //}


                                aceOffset += deniedAce.Header.AceSize;
                                break;

                            case AceType.AccessAllowedObject:
                                ACCESS_ALLOWED_OBJECT_ACE allowedObjectAce = (ACCESS_ALLOWED_OBJECT_ACE)Marshal.PtrToStructure(new IntPtr(dacl.ToInt64() + aceOffset), typeof(ACCESS_ALLOWED_OBJECT_ACE));

                                ace_item.Add("ACE Type", "ACCESS_ALLOWED_ACE");
                                ace_item.Add("AceFlags", allowedObjectAce.Header.AceFlags.ToString());
                                ace_item.Add("Mask", Helper.AccessMaskConverter.AccessMaskToString(allowedObjectAce.Mask));
                                ace_item.Add("ObjectType", allowedObjectAce.ObjectType.ToString());
                                ace_item.Add("InheritedObjectType", allowedObjectAce.InheritedObjectType.ToString());
                                sid = new SecurityIdentifier(IntPtr.Add(new IntPtr(dacl.ToInt64() + aceOffset), Marshal.SizeOf(typeof(ACE_HEADER)) + Marshal.SizeOf(typeof(uint))));
                                ace_item.Add("SID", sid.ToString());
                                try
                                {
                                    ace_item.Add("User", sid.Translate(typeof(System.Security.Principal.NTAccount)).ToString());
                                }
                                catch (Exception ex)
                                {
                                    ace_item.Add("User", $"< Error>: {ex.Message}");
                                }
                                DaclValues.Add(ace_item);

                                //WriteLineToConsole("ACCESS_ALLOWED_OBJECT_ACE");
                                //WriteLineToConsole($"  AceFlags: {allowedObjectAce.Header.AceFlags}");
                                //WriteLineToConsole($"  Mask: {Helper.AccessMaskConverter.AccessMaskToString(allowedObjectAce.Mask)}");
                                //WriteLineToConsole($"  ObjectType: {allowedObjectAce.ObjectType}");
                                //WriteLineToConsole($"  InheritedObjectType: {allowedObjectAce.InheritedObjectType}");
                                //sid = new SecurityIdentifier(IntPtr.Add(new IntPtr(dacl.ToInt64() + aceOffset), Marshal.SizeOf(typeof(ACE_HEADER)) + Marshal.SizeOf(typeof(uint))));
                                //WriteLineToConsole($"  Sid: {sid}");
                                //try
                                //{
                                //    WriteLineToConsole($"  User: {sid.Translate(typeof(System.Security.Principal.NTAccount))}");
                                //}
                                //catch (Exception ex)
                                //{
                                //    WriteLineToConsole($"  User: <Error>: {ex.Message}");
                                //}

                                aceOffset += allowedObjectAce.Header.AceSize;
                                break;

                            // Handle other ACE types as necessary
                            default:
                                DaclValues.Add(new Dictionary<string, string> { { "ACE Type", $"UNKNOWN {aceType}" } });
                                aceOffset += Marshal.ReadInt16(dacl, aceOffset + 2); // Move to the next ACE based on the size of the current ACE
                                break;
                        }
                    }
                }


            }
            return DaclValues;
        }

        static string ConvertDaclToString(IntPtr daclPtr, ushort size)
        {
            if (daclPtr == IntPtr.Zero)
                return "No DACL present.";

            try
            {
                // Create a RawSecurityDescriptor from the DACL pointer
                RawSecurityDescriptor rawDescriptor;

                // Create a managed byte array to hold the data
                byte[] managedArray = new byte[size];

                // Copy data from IntPtr to managed byte array
                Marshal.Copy(daclPtr, managedArray, 0, size);


                rawDescriptor = new RawSecurityDescriptor(managedArray, 0);
                return rawDescriptor.GetSddlForm(AccessControlSections.Access);
            }
            catch (Exception ex)
            {
                return $"Error converting DACL: {ex.Message}";
            }
            //finally
            //{
            //    // Free unmanaged memory if necessary (depends on how it's allocated)
            //    Marshal.FreeHGlobal(daclPtr);
            //}
        }

        public class AccessMaskConverter
        {
            // Define constants for common access rights
            public const uint READ_DATA = 0x00000001;       // Read Data (or List)
            public const uint WRITE_DATA = 0x00000002;      // Write Data (or Add File)
            public const uint APPEND_DATA = 0x00000004;     // Append Data (or Add Subdirectory)
            public const uint READ_ACL = 0x00000008;        // Read Permissions
            public const uint WRITE_ACL = 0x00000010;       // Change Permissions
            public const uint DELETE = 0x00000020;           // Delete
            public const uint EXECUTE = 0x00000040;         // Execute
            public const uint FULL_CONTROL = 0xFFFFFFFF;     // Full Control

            public static string AccessMaskToString(uint accessMask)
            {
                if (accessMask == 0)
                    return "No Access";

                var rights = new System.Text.StringBuilder();

                if ((accessMask & READ_DATA) != 0) rights.Append("READ_DATA ");
                if ((accessMask & WRITE_DATA) != 0) rights.Append("WRITE_DATA ");
                if ((accessMask & APPEND_DATA) != 0) rights.Append("APPEND_DATA ");
                if ((accessMask & READ_ACL) != 0) rights.Append("READ_ACL ");
                if ((accessMask & WRITE_ACL) != 0) rights.Append("WRITE_ACL ");
                if ((accessMask & DELETE) != 0) rights.Append("DELETE ");
                if ((accessMask & EXECUTE) != 0) rights.Append("EXECUTE ");
                if (accessMask == FULL_CONTROL) rights.Append("FULL_CONTROL ");

                return rights.ToString().Trim();
            }
        }

        public static string ConvertSidToString(IntPtr sidPointer)
        {
            if (WinApi.ConvertSidToStringSid(sidPointer, out IntPtr stringSid))
            {
                string sidString = Marshal.PtrToStringAuto(stringSid);
                WinApi.LocalFree(stringSid);
                return sidString;
            }
            else
            {
                return null;
            }
        }

        public static bool IsRunningAsAdministrator()
        {
            var identity = WindowsIdentity.GetCurrent();
            var principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        public static void RestartWithElevatedPrivileges(string[] args)
        {
            // Get the path of the executable
            string programPath = Environment.GetCommandLineArgs()[0];

            // Create a new process start info
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = programPath,
                UseShellExecute = true,
                Verb = "runas", // This will prompt for UAC elevation
                Arguments = string.Join(" ", args) // Pass any arguments to the new process
            };

            try
            {
                Process.Start(startInfo);
                Environment.Exit(0); // Exit the current instance of the application
            }
            catch (System.ComponentModel.Win32Exception ex)
            {
                // Handle the case where the user cancels the UAC prompt
                if (ex.NativeErrorCode == 1223) // ERROR_CANCELLED
                {
                    Console.WriteLine("UAC prompt was canceled.");
                }
                else
                {
                    Console.WriteLine($"Failed to restart with elevated privileges: {ex.Message}");
                    throw;
                }
            }
        }
    }

    
}
