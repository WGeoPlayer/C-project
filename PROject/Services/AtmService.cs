using PROject.Contracts;
using PROject.Models;

namespace PROject.Services;

/// <summary>
/// Core driver of the ATM application.
/// Manages the application runtime environment and main menu routing.
/// </summary>
public partial class AtmService : IAtmService
{
    private List<User> users = new();

    /// <summary>Starts the main program loop execution.</summary>
    public void StartSystem()
    {
        LoadUsersFromFile();
        bool running = true;
        while (running)
        {
            Console.WriteLine("\n|>---<| ATM |>---<| \n1 - Register\n2 - Login\n3 - Exit");
            Console.Write("Choose: ");
            string? choice = Console.ReadLine();

            if (choice == "1" || choice?.ToLower() == "register") { RegisterAccount(); }
            else if (choice == "2" || choice?.ToLower() == "login") { LoginSession(ref running); }
            else if (choice == "3" || choice?.ToLower() == "exit") { running = false; }
            else { Console.WriteLine("Wrong option."); }
        }
        Console.WriteLine("Bye!");
    }
}