using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace poc_WFP_disable
{
    internal partial class Program
    {
        private void ShowMainMenu()
        {
            bool showMenu = true;
            while (showMenu)
            {
                Console.Clear();
                WriteLineToConsole("Main Menu:");
                WriteLineToConsole("1) Sessions");
                WriteLineToConsole("2) Inspect");
                WriteLineToConsole("3) Add Rule");
                WriteLineToConsole("4) Remove");
                WriteLineToConsole("5) Rule Monitor");
                WriteLineToConsole("6) Subscribe for changes");
                WriteLineToConsole("7) Exit");
                WriteLineToConsole("\nSelect an option: ");


                switch (Console.ReadLine())
                {
                    case "1":
                        ShowSessions();
                        break;
                    case "2":
                        ShowInspectMenu();
                        break;
                    case "3":
                        ShowAddFilter();
                        PauseBeforeContinuing();
                        break;
                    case "4":
                        ShowRemoveMenu();
                        break;
                    case "5":
                        ShowMonotorMenu();
                        break;
                    case "6":
                        ShowSubscriptionMenu();
                        break;
                    case "7":
                        showMenu = false;
                        break;
                    default:
                        WriteLineToConsole("Invalid option. Please try again.");
                        PauseBeforeContinuing();
                        break;
                }
            }

        }


        private void ShowRemoveMenu()
        {
            Console.Clear();
            ShowProviderFilters();
            WriteLineToConsole("Remove Menu:");
            WriteLineToConsole("1) Remove by filters");
            WriteLineToConsole("2) Set Provider GUID filter");
            WriteLineToConsole("3) Set Name filter");
            WriteLineToConsole("4) Clear filters");
            WriteLineToConsole("5) Back to Main Menu");
            Console.Write("\nSelect an option: ");
            

            switch (Console.ReadLine())
            {
                case "1":
                    KillByFilter();
                    PauseBeforeContinuing();
                    ShowRemoveMenu();
                    break;
                case "2":
                    AddProviderFilter();
                    PauseBeforeContinuing();
                    ShowRemoveMenu();
                    break;
                case "3":
                    AddMonitorFilter();
                    PauseBeforeContinuing();
                    ShowRemoveMenu();
                    break;
                case "4":
                    wfpClient.ClearMonitorFilter();
                    PauseBeforeContinuing();
                    ShowRemoveMenu();
                    break;
                case "5":
                    return;
                default:
                    WriteLineToConsole("Invalid option. Please try again.");
                    ShowRemoveMenu();
                    break;
            }
        }


        private void ShowAddFilter()
        {
            IPAddress address;

            WriteLineToConsole("Input ip addrss: ");
            string input = Console.ReadLine();
            try
            {
                address = IPAddress.Parse(input);
            }
            catch
            {
                Console.WriteLine($"Incorrect ip address: {input}");
                return;
            }

            BlockToRemoteIp(address);


        }

        private void ShowSubscriptionMenu()
        {
            Console.Clear();
            WriteLineToConsole("Subscription Menu:");
            WriteLineToConsole("1) Show events");
            WriteLineToConsole("2) Get subscribtions");
            WriteLineToConsole("3) Back to Main Menu");
            Console.Write("\nSelect an option: ");

            switch (Console.ReadLine())
            {
                case "1":
                    SubscribeAllEvents();
                    PauseBeforeContinuing();
                    ShowSubscriptionMenu();
                    break;
                case "2":
                    PrintSubscriptions();
                    PauseBeforeContinuing();
                    ShowSubscriptionMenu();
                    break;
                case "3":
                    return; // Go back to Main Menu
                default:
                    WriteLineToConsole("Invalid option. Please try again.");
                    PauseBeforeContinuing();
                    ShowSubscriptionMenu();
                    break;

            }
        }

        private void ShowSessions()
        {
            Console.Clear();
            PrintAll_Sessions();
            PauseBeforeContinuing();
            ShowMainMenu();
        }

        private void ShowProviderFilters()
        {
            WriteLineToConsole(new String('*', 20));
            WriteLineToConsole($"Available filter: ");
            if (wfpClient.GetLogFilters().providerKey != Guid.Empty)
            {
                WriteLineToConsole($"\t provider guid: {wfpClient.GetLogFilters().providerKey}");
            }
            else
            {
                WriteLineToConsole($"\t provider guid: NONE");
            }
            WriteLineToConsole($"\t name: {String.Join(", ", wfpClient.GetLogFilters().name_filter)}");


            WriteLineToConsole(new String('*', 20));
            WriteLineToConsole();
        }


        private void Print_Detailed_menu()
        {
            
            Console.Write("Input some object guid: ");
            string input = Console.ReadLine();
            WriteLineToConsole();
            WriteLineToConsole(new String('*', 20));
            try
            {
                Print_Detailed(new Guid(input));
            }
            catch (Exception ex)
            {
                WriteLineToConsole(ex.Message);
            }
            WriteLineToConsole(new String('*', 20));
        }


        private void ShowInspectMenu()
        {
            Console.Clear();
            WriteLineToConsole("Inspect Menu:");
            ShowProviderFilters();
            WriteLineToConsole("1) Show Providers");
            WriteLineToConsole("2) Show Sublayes");
            WriteLineToConsole("3) Show Filters");
            WriteLineToConsole("4) Show Callouts");
            WriteLineToConsole("5) Show Connections");
            WriteLineToConsole("6) Print Statistics");
            WriteLineToConsole("7) Detailed info");
            WriteLineToConsole("8) Set Provider GUID filter");
            WriteLineToConsole("9) Set Name filter");
            WriteLineToConsole("10) Clear filters");
            WriteLineToConsole("11) Back to Main Menu");
            Console.Write("\nSelect an option: ");

            switch (Console.ReadLine())
            {
                case "1":
                    PrintAll_Providers();
                    PauseBeforeContinuing();
                    ShowInspectMenu();
                    break;
                case "2":
                    PrintAll_Sublayers();
                    PauseBeforeContinuing();
                    ShowInspectMenu();
                    break;
                case "3":
                    PrintAll_Filters();
                    PauseBeforeContinuing();
                    ShowInspectMenu();
                    break;
                case "4":
                    PrintAll_Callouts();
                    PauseBeforeContinuing();
                    ShowInspectMenu();
                    break;
                case "5":
                    PrintAll_Connections();
                    PauseBeforeContinuing();
                    ShowInspectMenu();
                    break;
                case "6":
                    Print_Statistics();
                    PauseBeforeContinuing();
                    ShowInspectMenu();
                    break;
                case "7":
                    Print_Detailed_menu();
                    PauseBeforeContinuing();
                    ShowInspectMenu();
                    break;
                case "8":
                    AddProviderFilter();
                    PauseBeforeContinuing();
                    ShowInspectMenu();
                    break;
                case "9":
                    AddMonitorFilter();
                    ShowInspectMenu();
                    break;
                case "10":
                    wfpClient.ClearMonitorFilter();
                    ShowInspectMenu();
                    break;
                case "11":
                    return; // Go back to Main Menu
                default:
                    WriteLineToConsole("Invalid option. Please try again.");
                    PauseBeforeContinuing();
                    ShowInspectMenu();
                    break;
            }
        }


        private void ShowFilters()
        {
            WriteLineToConsole(new String('*', 20));
            WriteLineToConsole($"Available filters: ");
            WriteLineToConsole($"\t filter_id: {String.Join(", ", wfpClient.GetLogFilters().id_filter)}");
            WriteLineToConsole($"\t filter_name: {String.Join(", ", wfpClient.GetLogFilters().name_filter)}");
            WriteLineToConsole($"\t hosts: {String.Join(", ", wfpClient.GetLogFilters().hosts)}");
            WriteLineToConsole($"\t ports: {String.Join(", ", wfpClient.GetLogFilters().ports)}");
            WriteLineToConsole(new String('*', 20));
            WriteLineToConsole();
        }

        private void ShowMonotorMenu()
        {
            Console.Clear();
            WriteLineToConsole("Monitor Menu:");
            ShowFilters();
            WriteLineToConsole("1) Start Monitor");
            WriteLineToConsole("2) Add filter (filterId)");
            WriteLineToConsole("3) Remove filter (filterId)");
            WriteLineToConsole("4) Add host to filter");
            WriteLineToConsole("5) Add port to filter");
            WriteLineToConsole("6) Clear filter");
            WriteLineToConsole("7) Back to Main Menu");
            Console.Write("\nSelect an option: ");

            switch (Console.ReadLine())
            {
                case "1":
                    RuleMonotor();
                    PauseBeforeContinuing();
                    ShowMonotorMenu();
                    break;
                case "2":
                    AddMonitorFilter();
                    ShowMonotorMenu();
                    break;
                case "3":
                    RemoveMonitorFilter();
                    ShowMonotorMenu();
                    break;
                case "4":
                    AddHostFilter();
                    ShowMonotorMenu();
                    break;
                case "5":
                    AddPortFilter();
                    ShowMonotorMenu();
                    break;
                case "6":
                    wfpClient.ClearMonitorFilter();
                    ShowMonotorMenu();
                    break;
                case "7":
                    return; // Go back to Main Menu
                default:
                    WriteLineToConsole("Invalid option. Please try again.");
                    PauseBeforeContinuing();
                    ShowMonotorMenu();
                    break;
            }
        }


        static void PauseBeforeContinuing()
        {
            WriteLineToConsole("Press any key to continue...");
            Console.ReadKey();
        }


        static bool PromptForYesNo()
        {
            string userInput = string.Empty;

            while (true)
            {
                WriteLineToConsole("Do you want to continue? (yes/no)");
                userInput = Console.ReadLine();

                // Process the input
                if (!string.IsNullOrEmpty(userInput))
                {
                    // Convert input to lower case for case-insensitive comparison
                    userInput = userInput.ToLower();

                    if (userInput == "yes" || userInput == "y")
                    {
                        return true;
                    }
                    else if (userInput == "no" || userInput == "n")
                    {
                        return false;
                    }
                    else
                    {
                        WriteLineToConsole("Invalid input. Please enter 'yes' or 'no'.");
                    }
                }
                else
                {
                    WriteLineToConsole("No input received. Please enter 'yes' or 'no'.");
                }
            }
        }
    }
}
