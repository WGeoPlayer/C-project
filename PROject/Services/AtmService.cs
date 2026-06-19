using PROject.Contracts;
using PROject.Models;

namespace PROject.Services;

/// <summary>
/// Core driver of the ATM application.
/// Manages the application runtime environment and main menu routing.
/// </summary>
public partial class AtmService : IAtmService
{
    private readonly string filePath = "users.txt";
    private readonly List<User> users = new();

    /// <summary>Starts the main program loop execution.</summary>
    public void StartSystem()
    {
        LoadUsersFromFile();
        bool running = true;
        while (running)
        {
            Console.WriteLine("\n=== ATM ===\n1 - Register\n2 - Login\n3 - Exit");
            Console.Write("Choose: ");
            string? choice = Console.ReadLine();

            if (choice is "1" or "register" or "Register") { RegisterAccount(); }
            else if (choice is "2" or "login" or "Login") { LoginSession(ref running); }
            else if (choice is "3" or "exit" or "Exit") { running = false; }
            else { Console.WriteLine("Wrong option."); }
        }
        Console.WriteLine("Bye!");
    }
}