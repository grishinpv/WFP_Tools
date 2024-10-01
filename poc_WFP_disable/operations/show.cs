using Alba.CsConsoleFormat;
using ConsoleTables;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static NativeAPI.WfpNativeAPI;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using static System.ConsoleColor;
using System.Security.Cryptography;
using static System.Collections.Specialized.BitVector32;
using System.Runtime.InteropServices;
using Wfp;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using System.Security.Principal;


namespace poc_WFP_disable
{
    internal partial class Program
    {
        private static readonly LineThickness StrokeHeader = new LineThickness(LineWidth.Single);
        private static readonly LineThickness StrokeRight = new LineThickness(LineWidth.None, LineWidth.None, LineWidth.Single, LineWidth.Single);

        [Obsolete]
        private void PrintAll_Providers()
        {
            WriteLineToConsole("\nWFP Providers:");
            var doc = new Document { Background = Black, Color = Gray }
                .AddChildren(
                    new Grid { Stroke = StrokeHeader, StrokeColor = DarkGray }
                        .AddColumns(
                            new Column { Width = GridLength.Auto },
                            new Column { Width = GridLength.Auto, MaxWidth = 20 },
                            new Column { Width = GridLength.Star(1) },
                            new Column { Width = GridLength.Auto }
                        )
                        .AddChildren(
                            new Cell { Stroke = StrokeHeader, Color = White }
                                .AddChildren("Guid"),
                            new Cell { Stroke = StrokeHeader, Color = White }
                                .AddChildren("Name"),
                            new Cell { Stroke = StrokeHeader, Color = White }
                                .AddChildren("Description"),
                            new Cell { Stroke = StrokeHeader, Color = White }
                                .AddChildren("Service Name"),
                            wfpClient.GetProviders().Select(item => new[] {
                                new Cell { Stroke = StrokeRight }
                                    .AddChildren(item.providerKey),
                                new Cell { Stroke = StrokeRight, Color = Yellow }
                                    .AddChildren(item.displayData.name),
                                new Cell { Stroke = StrokeRight, Color = White}
                                    .AddChildren(item.displayData.description),
                                new Cell { Stroke = StrokeRight}
                                    .AddChildren(item.serviceName),
                            })
                        )
                );

            ConsoleRenderer.RenderDocument(doc);
        }

        [Obsolete]
        private void PrintAll_Sessions()
        {
            WriteLineToConsole("\nWFP Sessions:");
            var doc = new Document { Background = Black, Color = Gray }
                .AddChildren(
                    new Grid { Stroke = StrokeHeader, StrokeColor = DarkGray }
                        .AddColumns(
                            new Column { Width = GridLength.Auto },
                            new Column { Width = GridLength.Auto, MaxWidth = 20 },
                            new Column { Width = GridLength.Auto },
                            new Column { Width = GridLength.Auto },
                            new Column { Width = GridLength.Auto, MaxWidth = 20 },
                            new Column { Width = GridLength.Auto },
                            new Column { Width = GridLength.Auto }
                        )
                        .AddChildren(
                            new Cell { Stroke = StrokeHeader, Color = White }
                                .AddChildren("guid"),
                            new Cell { Stroke = StrokeHeader, Color = White }
                                .AddChildren("Name"),
                            new Cell { Stroke = StrokeHeader, Color = White }
                                .AddChildren("Description"),
                            new Cell { Stroke = StrokeHeader, Color = White }
                                .AddChildren("Process ID"),
                            new Cell { Stroke = StrokeHeader, Color = White }
                                .AddChildren("Process Name"),
                            new Cell { Stroke = StrokeHeader, Color = White }
                                .AddChildren("User Name"),
                            new Cell { Stroke = StrokeHeader, Color = White }
                                .AddChildren("Kernel Mode"),
                            wfpClient.GetSessions().Select(item => new[] {
                                new Cell { Stroke = StrokeRight }
                                    .AddChildren(item.sessionKey),
                                new Cell { Stroke = StrokeRight, Color = Yellow }
                                    .AddChildren(item.displayData.name),
                                new Cell { Stroke = StrokeRight, Color = White}
                                    .AddChildren(item.displayData.description),
                                new Cell { Stroke = StrokeRight}
                                    .AddChildren(item.processId),
                                new Cell { Stroke = StrokeRight}
                                    .AddChildren(Process.GetProcessById(item.processId).ProcessName),
                                new Cell { Stroke = StrokeRight}
                                    .AddChildren(Marshal.PtrToStringUni(item.username)),
                                new Cell { Stroke = StrokeRight}
                                    .AddChildren(item.kernelMode),
                            })
                        )
                );

            ConsoleRenderer.RenderDocument(doc);
        }

        [Obsolete]
        private void PrintAll_Sublayers()
        {
            WriteLineToConsole("\nWFP Syblayers:");
            var doc = new Document { Background = Black, Color = Gray }
                .AddChildren(
                    new Grid { Stroke = StrokeHeader, StrokeColor = DarkGray }
                        .AddColumns(
                            new Column { Width = GridLength.Auto },
                            new Column { Width = GridLength.Auto, MaxWidth = 20 },
                            new Column { Width = GridLength.Auto, MaxWidth = 30 },
                            new Column { Width = GridLength.Auto },
                            new Column { Width = GridLength.Auto }
                        )
                        .AddChildren(
                            new Cell { Stroke = StrokeHeader, Color = White }
                                .AddChildren("guid"),
                            new Cell { Stroke = StrokeHeader, Color = White }
                                .AddChildren("Name"),
                            new Cell { Stroke = StrokeHeader, Color = White }
                                .AddChildren("Description"),
                            new Cell { Stroke = StrokeHeader, Color = White }
                                .AddChildren("Weight"),
                            new Cell { Stroke = StrokeHeader, Color = White }
                                .AddChildren("Provider guid"),
                            wfpClient.GetSubLayers().Select(item => new[] {
                                new Cell { Stroke = StrokeRight }
                                    .AddChildren(item.subLayerKey),
                                new Cell { Stroke = StrokeRight, Color = Yellow }
                                    .AddChildren(item.displayData.name),
                                new Cell { Stroke = StrokeRight, Color = White}
                                    .AddChildren(item.displayData.description),
                                new Cell { Stroke = StrokeRight }
                                    .AddChildren(item.weight),
                                new Cell { Stroke = StrokeRight }
                                    .AddChildren(item.providerKey == IntPtr.Zero ? null : Marshal.PtrToStructure<Guid>(item.providerKey)),
                            })
                        )
                );

            ConsoleRenderer.RenderDocument(doc);
        }

        [Obsolete]
        private void PrintAll_Filters()
        {
            WriteLineToConsole("\nWFP Filters:");
            var doc = new Document { Background = Black, Color = Gray }
                .AddChildren(
                    new Grid { Stroke = StrokeHeader, StrokeColor = DarkGray }
                        .AddColumns(
                            new Column { Width = GridLength.Auto, MaxWidth = 37 },
                            new Column { Width = GridLength.Auto },
                            new Column { Width = GridLength.Star(1) },
                            new Column { Width = GridLength.Star(1) },
                            new Column { Width = GridLength.Star(1), MaxWidth = 15 },
                            //new Column { Width = GridLength.Auto },
                            new Column { Width = GridLength.Auto, MaxWidth = 37 }
                        )
                        .AddChildren(
                            new Cell { Stroke = StrokeHeader, Color = White }
                                .AddChildren("Key"),
                            new Cell { Stroke = StrokeHeader, Color = White }
                                .AddChildren("filterId"),
                            new Cell { Stroke = StrokeHeader, Color = White }
                                .AddChildren("Name"),
                            new Cell { Stroke = StrokeHeader, Color = White }
                                .AddChildren("Description"),
                            new Cell { Stroke = StrokeHeader, Color = White }
                                .AddChildren("Action"),
                            //new Cell { Stroke = StrokeHeader, Color = White }
                            //    .AddChildren("Weight"),
                            new Cell { Stroke = StrokeHeader, Color = White }
                                .AddChildren("Provider guid"),
                            wfpClient.GetFilters().Select(item => new[] {
                                new Cell { Stroke = StrokeRight }
                                    .AddChildren(item.filterKey),
                                new Cell { Stroke = StrokeRight }
                                    .AddChildren(item.filterId),
                                new Cell { Stroke = StrokeRight, Color = Yellow }
                                    .AddChildren(item.displayData.name),
                                new Cell { Stroke = StrokeRight, Color = White}
                                    .AddChildren(item.displayData.description),
                                new Cell { Stroke = StrokeRight }
                                    .AddChildren(item.action.type),
                                //new Cell { Stroke = StrokeRight }
                                //    .AddChildren(item.weight.type),
                                new Cell { Stroke = StrokeRight }
                                    .AddChildren(item.providerKey == IntPtr.Zero ? null : Marshal.PtrToStructure<Guid>(item.providerKey)),
                            })
                        )
                );

            ConsoleRenderer.RenderDocument(doc);
        }

        [Obsolete]
        private void PrintAll_Callouts()
        {
            WriteLineToConsole("\nWFP Callouts");
            var doc = new Document { Background = Black, Color = Gray }
                .AddChildren(
                    new Grid { Stroke = StrokeHeader, StrokeColor = DarkGray }
                        .AddColumns(
                            new Column { Width = GridLength.Auto, MaxWidth = 37 },
                            //new Column { Width = GridLength.Auto },
                            new Column { Width = GridLength.Star(1) },
                            new Column { Width = GridLength.Star(1) },
                            new Column { Width = GridLength.Auto, MaxWidth = 37 }
                        )
                        .AddChildren(
                            new Cell { Stroke = StrokeHeader, Color = White }
                                .AddChildren("Key"),
                            //new Cell { Stroke = StrokeHeader, Color = White }
                            //    .AddChildren("calloutId"),
                            new Cell { Stroke = StrokeHeader, Color = White }
                                .AddChildren("Name"),
                            new Cell { Stroke = StrokeHeader, Color = White }
                                .AddChildren("Description"),
                            new Cell { Stroke = StrokeHeader, Color = White }
                                .AddChildren("Provider guid"),
                            wfpClient.GetCallouts().Select(item => new[] {
                                new Cell { Stroke = StrokeRight }
                                    .AddChildren(item.calloutKey),
                                //new Cell { Stroke = StrokeRight }
                                //    .AddChildren(item.filterId),
                                new Cell { Stroke = StrokeRight, Color = Yellow }
                                    .AddChildren(item.displayData.name),
                                new Cell { Stroke = StrokeRight, Color = White}
                                    .AddChildren(item.displayData.description),
                                new Cell { Stroke = StrokeRight }
                                    .AddChildren(item.providerKey == IntPtr.Zero ? null : Marshal.PtrToStructure<Guid>(item.providerKey)),
                            })
                        )
                );

            ConsoleRenderer.RenderDocument(doc);
        }

        [Obsolete]
        private void PrintAll_Connections()
        {
            WriteLineToConsole("\nWFP Connections");
            var doc = new Document { Background = Black, Color = Gray }
                .AddChildren(
                    new Grid { Stroke = StrokeHeader, StrokeColor = DarkGray }
                        .AddColumns(
                            new Column { Width = GridLength.Auto, MaxWidth = 37 },
                            //new Column { Width = GridLength.Auto },
                            new Column { Width = GridLength.Star(1) },
                            new Column { Width = GridLength.Star(1) },
                            new Column { Width = GridLength.Auto, MaxWidth = 37 }
                        )
                        .AddChildren(
                            new Cell { Stroke = StrokeHeader, Color = White }
                                .AddChildren("ID"),
                            //new Cell { Stroke = StrokeHeader, Color = White }
                            //    .AddChildren("calloutId"),
                            new Cell { Stroke = StrokeHeader, Color = White }
                                .AddChildren("Bytes In"),
                            new Cell { Stroke = StrokeHeader, Color = White }
                                .AddChildren("Bytes Out"),
                            new Cell { Stroke = StrokeHeader, Color = White }
                                .AddChildren("Provider guid"),
                            wfpClient.GetConnections().Select(item => new[] {
                                new Cell { Stroke = StrokeRight }
                                    .AddChildren(item.connectionId),
                                //new Cell { Stroke = StrokeRight }
                                //    .AddChildren(item.filterId),
                                new Cell { Stroke = StrokeRight, Color = Yellow }
                                    .AddChildren(item.bytesTransferredIn),
                                new Cell { Stroke = StrokeRight, Color = White}
                                    .AddChildren(item.bytesTransferredOut),
                                new Cell { Stroke = StrokeRight }
                                    .AddChildren(item.providerKey == IntPtr.Zero ? null : Marshal.PtrToStructure<Guid>(item.providerKey)),
                            })
                        )
                );

            ConsoleRenderer.RenderDocument(doc);
        }


        private void Print_Statistics()
        {
            WriteLineToConsole("\nWFP objects:");
            WriteLineToConsole(new String('-', 40));
            WriteLineToConsole($"Sessions: {wfpClient.GetSessions().Count()}");
            WriteLineToConsole($"Providers: {wfpClient.GetProviders().Count()}");
            WriteLineToConsole($"Sublayers: {wfpClient.GetSubLayers().Count()}");
            WriteLineToConsole($"Filters: {wfpClient.GetFilters().Count()}");
            WriteLineToConsole($"Callouts: {wfpClient.GetCallouts().Count()}");
            WriteLineToConsole($"Connections: {wfpClient.GetConnections().Count()}");
            WriteLineToConsole(new String('-', 40));
        }

        private void Print_Statistics_Detailed()
        {
            var sessions = wfpClient.GetSessions();
            var providers = wfpClient.GetProviders();
            var filters = wfpClient.GetFilters();
            var sublayers = wfpClient.GetSubLayers();
            var callouts = wfpClient.GetCallouts();
            var connection = wfpClient.GetConnections();

            WriteLineToConsole("\nWFP objects:");
            WriteLineToConsole(new String('-', 40));

            WriteLineToConsole($"Sessions: ");
            if (sessions.Count() == 0)
            {
                WriteLineToConsole("\t None");
            }
            else
            {
                foreach (var item in sessions)
                {
                    WriteLineToConsole($"\tname: {item.displayData.name}");
                }
            }

            WriteLineToConsole($"Providers: ");
            if (providers.Count() == 0)
            {
                WriteLineToConsole("\t None");
            }
            else
            {
                foreach (var item in providers)
                {
                    WriteLineToConsole($"\tname: {item.displayData.name}");
                }
            }

            WriteLineToConsole($"Sublayers: ");
            if (sublayers.Count() == 0)
            {
                WriteLineToConsole("\t None");
            }
            else
            {
                foreach (var item in sublayers)
                {
                    WriteLineToConsole($"\tname: {item.displayData.name}");
                }
            }

            WriteLineToConsole($"Filters: ");
            if (filters.Count() == 0)
            {
                WriteLineToConsole("\t None");
            }
            else
            {
                foreach (var item in filters)
                {
                    WriteLineToConsole($"\tname: {item.displayData.name}");
                }
            }

            WriteLineToConsole($"Callouts: ");
            if (callouts.Count() == 0)
            {
                WriteLineToConsole("\t None");
            }
            else
            {
                foreach (var item in callouts)
                {
                    WriteLineToConsole($"\tname: {item.displayData.name}");
                }
            }

            WriteLineToConsole($"Connections: ");
            if (connection.Count() == 0)
            {
                WriteLineToConsole("\t None");
            }
            else
            {
                foreach (var item in connection)
                {
                    WriteLineToConsole($"\tconnectionId: {item.connectionId}");
                }
            }

            WriteLineToConsole(new String('-', 40));
        }



        [Obsolete]
        public void PrintEvents()
        {
            IntPtr subscriptionHandle = wfpClient.StartMonitor();
            PauseBeforeContinuing();
            wfpClient.StopMonitor(subscriptionHandle);
        }


        public void Print_Detailed(Guid input)
        {
            var sessions = wfpClient.GetSessions();
            var providers = wfpClient.GetProviders();
            var filters = wfpClient.GetFilters();
            var sublayers = wfpClient.GetSubLayers();
            var callouts = wfpClient.GetCallouts();

            WriteLineToConsole($"Detailed info for '{input}'");

            if (sessions.Count() > 0 && sessions.Where(item => input.Equals(item.sessionKey)).Count() > 0)
            {
                PrintSessionInfo(sessions.Where(item => input.Equals(item.sessionKey)).First());
                return;
            }

            if (providers.Count() > 0 && providers.Where(item => input.Equals(item.providerKey)).Count() > 0)
            {
                PrintProviderInfo(providers.Where(item => input.Equals(item.providerKey)).First());
                return;
            }

            if (filters.Count() > 0 && filters.Where(item => input.Equals(item.filterKey)).Count() > 0)
            {
                PrintFilterInfo(filters.Where(item => input.Equals(item.filterKey)).First());
                return;
            }

            if (sublayers.Count() > 0 && sublayers.Where(item => input.Equals(item.subLayerKey)).Count() > 0)
            {
                PrintSublayerInfo(sublayers.Where(item => input.Equals(item.subLayerKey)).First());
                return;
            }

            if (callouts.Count() > 0 && callouts.Where(item => input.Equals(item.calloutKey)).Count() > 0)
            {
                PrintCalloutInfo(callouts.Where(item => input.Equals(item.calloutKey)).First());
                return;
            }

            WriteLineToConsole($"Guid `{input}` not found");

        }


        public void PrintFilterSecurityInfo(FWPM_FILTER0_ item)
        {
            PrintSecurityInfo(wfpClient.GetFilterSecurityInfo(item.filterKey));
        }


        public void PrintCalloutSecurityInfo(FWPM_CALLOUT0_ item)
        {
            PrintSecurityInfo(wfpClient.GetCalloutSecurityInfo(item.calloutKey));
        }

        public void PrintProviderSecurityInfo(FWPM_PROVIDER0_ item)
        {
            PrintSecurityInfo(wfpClient.GetProviderSecurityInfo(item.providerKey));
        }

        public void PrintSublayerSecurityInfo(FWPM_SUBLAYER0_ item)
        {
            PrintSecurityInfo(wfpClient.GetSublayerSecurityInfo(item.subLayerKey));
        }

        public void PrintConnectionSecurityInfo(FWPM_CONNECTION0_ item)
        {
            PrintSecurityInfo(wfpClient.GetConnectionSecurityInfo(Guid.Empty));
        }

        public void PrintSecurityInfo(FirewallObjectSecurityInfo sec_info)
        {

            // print Owner
            if (sec_info.sidOwner != IntPtr.Zero)
            {
                WriteLineToConsole($"Owner: ");
                List<Dictionary<string, string>> owner_params = new List<Dictionary<string, string>>();

                var owner = new SecurityIdentifier(sec_info.sidOwner);

                owner_params.Add(new Dictionary<string, string> { { "SID", owner.ToString() } });
                try
                {
                    owner_params.Add(new Dictionary<string, string> { { "Name", owner.Translate(typeof(System.Security.Principal.NTAccount)).ToString() } });
                }
                catch (Exception ex)
                {
                    owner_params.Add(new Dictionary<string, string> { { "Name:", $"<Error>: {ex.Message}" } });
                }

                Print2ColumnTable(owner_params);
            }

            // print Group
            if (sec_info.sidGroup != IntPtr.Zero) {

                WriteLineToConsole($"Group: ");
                List<Dictionary<string, string>> group_params = new List<Dictionary<string, string>>();

                var group = new SecurityIdentifier(sec_info.sidGroup);

                group_params.Add(new Dictionary<string, string> { { "SID", group.ToString() } });
                try
                {
                    group_params.Add(new Dictionary<string, string> { { "Name", group.Translate(typeof(System.Security.Principal.NTAccount)).ToString() } });
                }
                catch (Exception ex)
                {
                    group_params.Add(new Dictionary<string, string> { { "Name:", $"<Error>: {ex.Message}" } });
                }

                Print2ColumnTable(group_params);
            }

            // print DACL
            if (sec_info.dacl != IntPtr.Zero)
            {
                WriteLineToConsole($"DACL: ");
                Print2ColumnTableSeparated(Helper.GetDacl(sec_info.dacl));
            }

        }

        [Obsolete]
        private void Print2ColumnTable(List<Dictionary<string, string>> items)
        {
            var doc = new Document { Background = Black, Color = Gray }
            .AddChildren(
                new Grid { Stroke = StrokeHeader, StrokeColor = DarkGray }
                    .AddColumns(
                        new Column { Width = GridLength.Auto, MaxWidth = 15 },
                        new Column { Width = GridLength.Auto, MaxWidth = 40 }
                    )
                    .AddChildren(
                        new Cell { Stroke = StrokeHeader, Color = White }
                            .AddChildren("Parameter"),
                        new Cell { Stroke = StrokeHeader, Color = White }
                            .AddChildren("Value"),

                        items.SelectMany(dict =>
                            dict.Select(kvp =>
                                new[] {
                                    new Cell { Stroke = StrokeRight }
                                        .AddChildren(kvp.Key), // Key cell
                                    new Cell { Stroke = StrokeRight, Color = Yellow }
                                        .AddChildren(kvp.Value) // Value cell
                                })
                        )
                    )
            );


            ConsoleRenderer.RenderDocument(doc);
        }

        [Obsolete]
        private void Print2ColumnTableSeparated(List<Dictionary<string, string>> items)
        {
            var doc = new Document { Background = Black, Color = Gray }
            .AddChildren(
                new Grid { Stroke = StrokeHeader, StrokeColor = DarkGray }
                    .AddColumns(
                        new Column { Width = GridLength.Auto, MaxWidth = 15 },
                        new Column { Width = GridLength.Auto, MaxWidth = 40 }
                    )
                    .AddChildren(
                        new Cell { Stroke = StrokeHeader, Color = White }
                            .AddChildren("Parameter"),
                        new Cell { Stroke = StrokeHeader, Color = White }
                            .AddChildren("Value"),

                        items.SelectMany(dict =>
                            dict.Select(kvp =>
                                new[] {
                                    new Cell { Stroke = StrokeRight }
                                        .AddChildren(kvp.Key), // Key cell
                                    new Cell { Stroke = StrokeRight, Color = Yellow }
                                        .AddChildren(kvp.Value) // Value cell
                                })
                            .Concat(new[] {
                                new[] { new Cell().AddChildren(new String('█', 15)), new Cell().AddChildren(new String('█', 40)) }
                                })
                        )
                    )
            );


            ConsoleRenderer.RenderDocument(doc);
        }



        public void PrintFilterInfo(FWPM_FILTER0_ item)
        {
            WriteLineToConsole($"Type: 'Filter'");

            List<Dictionary<string, string>> item_params = new List<Dictionary<string, string>>();

            item_params.Add(new Dictionary<string, string> { { "filterKey", item.filterKey.ToString() } });
            item_params.Add(new Dictionary<string, string> { { "filterId", item.filterId.ToString() } });
            item_params.Add(new Dictionary<string, string> { { "name", item.displayData.name.ToString() } } );
            item_params.Add(new Dictionary<string, string> { { "description", item.displayData.description } } );
            item_params.Add(new Dictionary<string, string> { { "flags", item.flags.ToString() } } );
            item_params.Add(new Dictionary<string, string> { { "providerKey", (item.providerKey != IntPtr.Zero ? Marshal.PtrToStructure<Guid>(item.providerKey).ToString() : "NONE") } });
            item_params.Add(new Dictionary<string, string> { { "providerData", "..." } });
            item_params.Add(new Dictionary<string, string> { { "layerKey", item.layerKey.ToString() } } );
            item_params.Add(new Dictionary<string, string> { { "subLayerKey", item.subLayerKey.ToString() } });
            item_params.Add(new Dictionary<string, string> { { "weight", item.weight.ToString() } });
            item_params.Add(new Dictionary<string, string> { { "effectiveWeight", item.effectiveWeight.ToString() } });
            item_params.Add(new Dictionary<string, string> { { "numFilterConditions", item.numFilterConditions.ToString() } });
            item_params.Add(new Dictionary<string, string> { { "action", item.action.type.ToString() } });
            item_params.Add(new Dictionary<string, string> { { "context", item.context.ToString() } });
            item_params.Add(new Dictionary<string, string> { { "filterCondition", "UNDERCONSTRUCTION" } });
            Print2ColumnTable(item_params);
            PrintFilterSecurityInfo(item);


        }

        public void PrintCalloutInfo(FWPM_CALLOUT0_ item)
        {
            WriteLineToConsole($"Type: 'Callout'");

            List<Dictionary<string, string>> item_params = new List<Dictionary<string, string>>();

            item_params.Add(new Dictionary<string, string> { { "calloutKey", item.calloutKey.ToString() } });
            item_params.Add(new Dictionary<string, string> { { "calloutId", item.calloutId.ToString() } });
            item_params.Add(new Dictionary<string, string> { { "name", item.displayData.name } });
            item_params.Add(new Dictionary<string, string> { { "description", item.displayData.description } });
            item_params.Add(new Dictionary<string, string> { { "flags", item.flags.ToString() } });
            item_params.Add(new Dictionary<string, string> { { "providerKey", (item.providerKey != IntPtr.Zero ? Marshal.PtrToStructure<Guid>(item.providerKey).ToString() : "NONE") } });
            item_params.Add(new Dictionary<string, string> { { "providerData", "..." } });
            item_params.Add(new Dictionary<string, string> { { "applicableLayer", item.applicableLayer.ToString() } });
            Print2ColumnTable(item_params);
            PrintCalloutSecurityInfo(item);
        }

        public void PrintConnectionInfo(FWPM_CONNECTION0_ item)
        {
            WriteLineToConsole($"Type: 'Connecton'");

            List<Dictionary<string, string>> item_params = new List<Dictionary<string, string>>();

            item_params.Add(new Dictionary<string, string> { { "connectionId", item.connectionId.ToString() } });
            item_params.Add(new Dictionary<string, string> { { "ipVersion", item.ipVersion.ToString() } });
            item_params.Add(new Dictionary<string, string> { { "localIPv4", item.localV4Address.ToString() } });
            item_params.Add(new Dictionary<string, string> { { "localIPv6", item.localV6Address.ToString() } });
            item_params.Add(new Dictionary<string, string> { { "remoteIPv4", item.remoteV4Address.ToString() } });
            item_params.Add(new Dictionary<string, string> { { "remoteIPv6", item.remoteV6Address.ToString() } });
            item_params.Add(new Dictionary<string, string> { { "providerKey", (item.providerKey != IntPtr.Zero ? Marshal.PtrToStructure<Guid>(item.providerKey).ToString() : "NONE") } });
            item_params.Add(new Dictionary<string, string> { { "ipsecTrafficModeType", item.ipsecTrafficModeType.ToString() } });
            item_params.Add(new Dictionary<string, string> { { "keyModuleType", item.keyModuleType.ToString() } });
            //item_params.Add(new Dictionary<string, string> { { "mmCrypto", item.mmCrypto.ToString() } });
            //item_params.Add(new Dictionary<string, string> { { "mmPeer", item.mmPeer.ToString() } });
            //item_params.Add(new Dictionary<string, string> { { "emPeer", item.emPeer.ToString() } });
            item_params.Add(new Dictionary<string, string> { { "mmCrypto", item.bytesTransferredIn.ToString() } });
            item_params.Add(new Dictionary<string, string> { { "mmPeer", item.bytesTransferredOut.ToString() } });
            item_params.Add(new Dictionary<string, string> { { "emPeer", item.bytesTransferredTotal.ToString() } });
            item_params.Add(new Dictionary<string, string> { { "emPeer", item.startSysTime.ToString() } });
            Print2ColumnTable(item_params);
            PrintConnectionSecurityInfo(item);
        }

        public void PrintProviderInfo(FWPM_PROVIDER0_ item)
        {

            WriteLineToConsole($"Type: 'Provider'");

            List<Dictionary<string, string>> item_params = new List<Dictionary<string, string>>();

            item_params.Add(new Dictionary<string, string> { { "providerkey", item.providerKey.ToString() } });
            item_params.Add(new Dictionary<string, string> { { "name", item.displayData.name } });
            item_params.Add(new Dictionary<string, string> { { "description", item.displayData.description } });
            item_params.Add(new Dictionary<string, string> { { "flags", item.flags.ToString() } });
            item_params.Add(new Dictionary<string, string> { { "providerdata", "...".ToString() } });
            item_params.Add(new Dictionary<string, string> { { "serviceName", item.serviceName.ToString() } });
            Print2ColumnTable(item_params);
            PrintProviderSecurityInfo(item);
        }

        public void PrintSessionInfo(FWPM_SESSION0_ item)
        {
            WriteLineToConsole($"Type: 'Session'");

            List<Dictionary<string, string>> item_params = new List<Dictionary<string, string>>();

            item_params.Add(new Dictionary<string, string> { { "sessionKey", item.sessionKey.ToString() } });
            item_params.Add(new Dictionary<string, string> { { "name", item.displayData.name } });
            item_params.Add(new Dictionary<string, string> { { "description", item.displayData.description } });
            item_params.Add(new Dictionary<string, string> { { "flags", item.flags.ToString() } });
            item_params.Add(new Dictionary<string, string> { { "txnWaitTimeoutInMSec", item.txnWaitTimeoutInMSec.ToString() } });
            item_params.Add(new Dictionary<string, string> { { "processId", Process.GetProcessById(item.processId).ProcessName.ToString() } });
            item_params.Add(new Dictionary<string, string> { { "sid", Helper.ConvertSidToString(item.sid).ToString() } });
            item_params.Add(new Dictionary<string, string> { { "username", Marshal.PtrToStringUni(item.username).ToString() } });
            item_params.Add(new Dictionary<string, string> { { "kernelMode", item.kernelMode.ToString() } });
            // session does not contain SecurityDescriptor
            Print2ColumnTable(item_params);
        }

        public void PrintSublayerInfo(FWPM_SUBLAYER0_ item)
        {
            WriteLineToConsole($"Type: 'SubLayer'");

            List<Dictionary<string, string>> item_params = new List<Dictionary<string, string>>();

            item_params.Add(new Dictionary<string, string> { { "subLayerKey", item.subLayerKey.ToString() } });
            item_params.Add(new Dictionary<string, string> { { "name", item.displayData.name } });
            item_params.Add(new Dictionary<string, string> { { "description", item.displayData.description } });
            item_params.Add(new Dictionary<string, string> { { "flags", item.flags.ToString() } });
            item_params.Add(new Dictionary<string, string> { { "providerKey", (item.providerKey != IntPtr.Zero ? Marshal.PtrToStructure<Guid>(item.providerKey).ToString() : "NONE") } });
            item_params.Add(new Dictionary<string, string> { { "providerData", "..." } });
            item_params.Add(new Dictionary<string, string> { { "weight", item.weight.ToString() } });
            Print2ColumnTable(item_params);
            PrintSublayerSecurityInfo(item);
        }

    }
}
