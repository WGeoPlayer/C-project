using PROject.Models;

class Program
{
    static string filePath = "users.txt";
    static List<User> users = new List<User>();

    static void Main()
    {
        LoadUsers();

        bool running = true;
        while (running)
        {
            Console.WriteLine();
            Console.WriteLine("=== ATM ===");
            Console.WriteLine("1 - Register");
            Console.WriteLine("2 - Login");
            Console.WriteLine("3 - Exit");
            Console.Write("Choose: ");
            string? choice = Console.ReadLine();

            if (choice == "1" || choice == "register" || choice == "Register")
            {
                Register();
            }
            else if (choice == "2" || choice == "login" || choice == "Login")
            {
                Login();
            }
            else if (choice == "3" || choice == "exit" || choice == "Exit")
            {
                running = false;
            }
            else
            {
                Console.WriteLine("Wrong option.");
            }
        }

        Console.WriteLine("Bye!");
    }

    static void LoadUsers()
    {
        if (!File.Exists(filePath))
        {
            File.Create(filePath).Close();
            return;
        }

        string[] lines = File.ReadAllLines(filePath);
        foreach (string line in lines)
        {
            if (line.Trim() == "")
            {
                continue;
            }

            string[] parts = line.Split(';');
            if (parts[0] == "client")
            {
                string username = parts[1];
                string password = parts[2];
                double balance = double.Parse(parts[3]);
                double loan = double.Parse(parts[4]);
                users.Add(new Client(username, password, balance, loan));
            }
            else if (parts[0] == "admin")
            {
                users.Add(new Admin(parts[1], parts[2]));
            }
        }
    }

    static void SaveUsers()
    {
        List<string> lines = new List<string>();
        foreach (User u in users)
        {
            lines.Add(u.ToFileLine());
        }
        File.WriteAllLines(filePath, lines);
    }

    static User? FindUser(string username)
    {
        foreach (User u in users)
        {
            if (u.Username == username)
            {
                return u;
            }
        }
        return null;
    }

    static void Register()
    {
        string secretAdminCode = "7777";

        Console.WriteLine("-----------------------------");
        Console.Write("New username: ");
        string? username = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(username) || username.Contains(";"))
        {
            Console.WriteLine("Invalid username.");
            return;
        }

        if (FindUser(username) != null)
        {
            Console.WriteLine("This username is already taken.");
            return;
        }

        Console.Write("Password: ");
        string? password = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(password) || password.Contains(";"))
        {
            Console.WriteLine("Invalid password.");
            return;
        }

        string[] roles = { "client", "admin" };
        Console.Write("Role (client/admin): ");
        string? role = Console.ReadLine();

        bool roleOk = false;
        foreach (string r in roles)
        {
            if (r == role)
            {
                roleOk = true;
            }
        }

        if (!roleOk)
        {
            Console.WriteLine("Role must be client or admin.");
            return;
        }

        if (role == "client")
        {
            users.Add(new Client(username, password, 0, 0));
        }
        else
        {
            Console.Write("Enter 4-digit Admin Security Code: ");
            string? adminInput = Console.ReadLine();

            if (adminInput != secretAdminCode)
            {
                Console.WriteLine("Wrong admin code! Registration canceled.");
                return;
            }

            users.Add(new Admin(username, password));
        }

        SaveUsers();
        Console.WriteLine("Registration done.");
    }

    static void Login()
    {
        Console.WriteLine("-----------------------------");
        Console.Write("Username: ");
        string? username = Console.ReadLine();
        Console.Write("Password: ");
        string? password = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            Console.WriteLine("Invalid username or password.");
            return;
        }

        User? user = FindUser(username);
        if (user == null || user.Password != password)
        {
            Console.WriteLine("Wrong username or password.");
            return;
        }

        Console.WriteLine("Welcome " + user.Username + "!");

        if (user is Client)
        {
            ClientSession((Client)user);
        }
        else
        {
            AdminSession((Admin)user);
        }
    }

    static void ClientSession(Client client)
    {
        bool loggedIn = true;
        while (loggedIn)
        {
            client.ShowMenu();
            Console.Write("Choose: ");
            string? choice = Console.ReadLine();

            if (choice == "1")
            {
                Console.WriteLine("-----------------------------");
                Console.WriteLine("Your balance: " + client.Balance);
                if (client.LoanRequested > 0)
                {
                    Console.WriteLine("Pending loan request: " + client.LoanRequested);
                }
            }
            else if (choice == "2")
            {
                Console.WriteLine("-----------------------------");
                Console.Write("Amount to deposit: ");
                double amount;
                if (double.TryParse(Console.ReadLine(), out amount) && amount > 0)
                {
                    client.Deposit(amount);
                    SaveUsers();
                    Console.WriteLine("Done. New balance: " + client.Balance);
                }
                else
                {
                    Console.WriteLine("Invalid amount.");
                }
            }
            else if (choice == "3")
            {
                Console.WriteLine("-----------------------------");
                Console.Write("Amount to withdraw: ");
                double amount;
                if (double.TryParse(Console.ReadLine(), out amount) && amount > 0)
                {
                    if (client.Withdraw(amount))
                    {
                        SaveUsers();
                        Console.WriteLine("Done. New balance: " + client.Balance);
                    }
                    else
                    {
                        Console.WriteLine("Not enough money.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid amount.");
                }
            }
            else if (choice == "4")
            {
                Console.WriteLine("-----------------------------");
                if (client.LoanRequested > 0)
                {
                    Console.WriteLine("You already have a pending loan request.");
                }
                else
                {
                    Console.Write("Loan amount: ");
                    double amount;
                    if (double.TryParse(Console.ReadLine(), out amount) && amount > 0)
                    {
                        client.LoanRequested = amount;
                        SaveUsers();
                        Console.WriteLine("Request sent, waiting for admin.");
                    }
                    else
                    {
                        Console.WriteLine("Invalid amount.");
                    }
                }
            }
            else if (choice == "5")
            {
                Console.WriteLine("-----------------------------");
                Console.Write("Are you sure? (y/n): ");
                string? answer = Console.ReadLine();
                if (answer == "y" || answer == "Y" || answer == "yes" || answer == "Yes" || answer == "YES")
                {
                    users.Remove(client);
                    SaveUsers();
                    Console.WriteLine("Account deleted.");
                    loggedIn = false;
                }
            }
            else if (choice == "6")
            {
                loggedIn = false;
            }
            else
            {
                Console.WriteLine("Wrong option.");
            }
        }
    }

    static void AdminSession(Admin admin)
    {
        bool loggedIn = true;
        while (loggedIn)
        {
            admin.ShowMenu();
            Console.Write("Choose: ");
            string? choice = Console.ReadLine();

            if (choice == "1")
            {
                ShowAllClients();
            }
            else if (choice == "2")
            {
                ApproveLoans();
            }
            else if (choice == "3")
            {
                DeleteClient();
            }
            else if (choice == "4")
            {
                Console.Write("This deletes ALL data. Are you sure? (y/n): ");
                string? answer = Console.ReadLine();
                if (answer == "y" || answer == "Y" || answer == "yes" || answer == "Yes" || answer == "YES")
                {
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                    users.Clear();
                    Console.WriteLine("-----------------------------");
                    Console.WriteLine("All data deleted.");
                    loggedIn = false;
                }
            }
            else if (choice == "5")
            {
                loggedIn = false;
            }
            else
            {
                Console.WriteLine("Wrong option.");
            }
        }
    }

    static void ShowAllClients()
    {
        Console.WriteLine();
        Console.WriteLine("--- Clients ---");
        bool any = false;
        foreach (User u in users)
        {
            if (u is Client)
            {
                Client c = (Client)u;
                Console.WriteLine(c.Username + " | balance: " + c.Balance + " | loan request: " + c.LoanRequested);
                any = true;
            }
        }
        if (!any)
        {
            Console.WriteLine("No clients yet.");
        }
    }

    static void ApproveLoans()
    {
        List<Client> pending = new List<Client>();
        foreach (User u in users)
        {
            if (u is Client)
            {
                Client c = (Client)u;
                if (c.LoanRequested > 0)
                {
                    pending.Add(c);
                }
            }
        }

        if (pending.Count == 0)
        {
            Console.WriteLine("-----------------------------");
            Console.WriteLine("No loan requests.");
            return;
        }

        for (int i = 0; i < pending.Count; i++)
        {
            Console.WriteLine("-----------------------------");
            Console.WriteLine((i + 1) + " - " + pending[i].Username + " wants " + pending[i].LoanRequested);
        }
        Console.WriteLine("-----------------------------");
        Console.Write("Which one to approve (number): ");
        int index;
        if (int.TryParse(Console.ReadLine(), out index) && index >= 1 && index <= pending.Count)
        {
            Client chosen = pending[index - 1];
            chosen.ApproveLoan();
            SaveUsers();
            Console.WriteLine("-----------------------------");
            Console.WriteLine("Loan approved for " + chosen.Username + ".");
        }
        else
        {
            Console.WriteLine("-----------------------------");
            Console.WriteLine("Wrong number.");
        }
    }

    static void DeleteClient()
    {
        List<Client> clients = new List<Client>();
        foreach (User u in users)
        {
            if (u is Client)
            {
                clients.Add((Client)u);
            }
        }

        if (clients.Count == 0)
        {
            Console.WriteLine("-----------------------------");
            Console.WriteLine("No clients to delete.");
            return;
        }

        for (int i = 0; i < clients.Count; i++)
        {
            Console.WriteLine("-----------------------------");
            Console.WriteLine((i + 1) + " - " + clients[i].Username);
        }
        Console.WriteLine("-----------------------------");
        Console.Write("Which one to delete (number): ");
        int index;
        if (int.TryParse(Console.ReadLine(), out index) && index >= 1 && index <= clients.Count)
        {
            Client chosen = clients[index - 1];
            users.Remove(chosen);
            SaveUsers();
            Console.WriteLine("-----------------------------");
            Console.WriteLine(chosen.Username + " deleted.");
        }
        else
        {
            Console.WriteLine("-----------------------------");
            Console.WriteLine("Wrong number.");
        }
    }
}