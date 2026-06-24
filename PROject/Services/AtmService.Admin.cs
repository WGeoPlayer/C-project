using PROject.Models;

namespace PROject.Services;

/// <summary>
/// Session manager containing administrative command consoles and data cleanup flows.
/// </summary>
public partial class AtmService
{
    /// <summary>Hosts administrative execution loops for data control and approval flows.</summary>
    private void RunAdminMenu(Admin admin, ref bool systemRunning)
    {
        bool active = true;
        while (active)
        {
            admin.ShowMenu();
            Console.Write("Choose: ");
            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1": ShowAllClients(); break;
                case "2": ApproveLoans(); break;
                case "3": DeleteClient(); break;
                case "4":
                    Console.Write("\nThis deletes ALL data. Are you sure? (y/n): ");
                    if (Console.ReadLine()?.ToLower() == "y")
                    {
                        users.Clear();
                        DataHandler.Save(users);

                        systemRunning = false;
                        active = false;
                    }
                    break;
                case "5": active = false; break;
                default: Console.WriteLine("Wrong option."); break;
            }
        }
    }
}