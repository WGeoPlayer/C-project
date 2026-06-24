using PROject.Models;

namespace PROject.Services;

/// <summary>
/// Authentication and registration processing engine for the ATM application.
/// </summary>
public partial class AtmService
{
    /// <summary>Handles interactive user registration procedures and constraint enforcement.</summary>
    private void RegisterAccount()
    {
        Console.Write("-----------------------------\nNew username: ");
        string? name = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(name) || name.Contains(';'))
        {
            Console.WriteLine("Invalid username.");
            return;
        }

        Console.Write("Password: ");
        string? pass = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(pass) || pass.Contains(';'))
        {
            Console.WriteLine("Invalid password.");
            return;
        }

        Console.Write("Role (client/admin): ");
        string? role = Console.ReadLine();
        if (role != "client" && role != "admin") { return; }

        if (role == "client") { users.Add(new Client(name, pass, 0, 0)); }
        else
        {
            Console.Write("Admin Code: ");
            if (Console.ReadLine() != "7777") return;
            users.Add(new Admin(name, pass));
        }
        SaveUsersToFile();
        Console.WriteLine("Registration done.");
    }

    /// <summary>Validates user identity credentials and initialises targeted runtime sessions.</summary>
    private void LoginSession(ref bool systemRunning)
    {
        Console.Write("-----------------------------\nUsername: ");
        string? name = Console.ReadLine();
        Console.Write("Password: ");
        string? pass = Console.ReadLine();

        User? user = users.FirstOrDefault(u => u.Username == name && u.Password == pass);

        if (user == null)
        {
            Console.WriteLine("Error: Username or password incorrect.");
            return;
        }
        Console.WriteLine("---------------------------------");
        Console.WriteLine($"Welcome {user.Username}!");
        if (user is Client c) { RunClientMenu(c); }
        else if (user is Admin a) { RunAdminMenu(a, ref systemRunning); }
    }
}