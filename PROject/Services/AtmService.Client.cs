using PROject.Models;

namespace PROject.Services;

public partial class AtmService
{
    /// <summary>Hosts execution cycles for verified banking transactions.</summary>
    private void RunClientMenu(Client client)
    {
        bool active = true;
        while (active)
        {
            client.ShowMenu();
            Console.Write("Choose: ");
            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.WriteLine($"\nBalance: {client.Balance} {(client.Balance == 0 ? "" : "$")}");
                    Console.WriteLine("----------------------------");
                    Console.WriteLine($"Pending loan: {client.LoanRequested} {(client.LoanRequested == 0 ? "" : "$")}");
                    break;
                case "2": HandleDeposit(client); break;
                case "3": HandleWithdraw(client); break;
                case "4": HandleLoan(client); break;
                case "5":
                    Console.Write("Are you sure? (y/n): ");
                    if (Console.ReadLine()?.ToLower() == "y")
                    {
                        users.Remove(client);
                        SaveUsersToFile();
                        active = false;
                    }
                    break;
                case "6": active = false; break;
                default: Console.WriteLine("Wrong option."); break;
            }
        }
    }
}