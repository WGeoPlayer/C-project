using PROject.Models;

namespace PROject.Services;

public partial class AtmService
{
    private void ShowAllClients()
    {
        Console.WriteLine("\n--- Clients ---");
        var clients = users.OfType<Client>().ToList();
        if (!clients.Any()) { Console.WriteLine("\nNo clients yet."); return; }
        Console.WriteLine("");
        foreach (var c in clients)
        {
            Console.WriteLine($"{c.Username} | Balance: {c.Balance} {(c.Balance == 0 ? "" : "$")} | Loan: {c.LoanRequested} {(c.LoanRequested == 0 ? "" : "$")}");
        }
            
    }

    private void ApproveLoans()
    {
        var pending = users.OfType<Client>().Where(c => c.LoanRequested > 0).ToList();
        if (!pending.Any()) { Console.WriteLine("\nNo loan requests."); return; }

        for (int i = 0; i < pending.Count; i++)
        {
            Console.WriteLine($"{i + 1} - {pending[i].Username} wants {pending[i].LoanRequested} {(pending[i].LoanRequested == 0 ? "" : "$")}");
        }
        
        Console.Write("\nWhich one to approve (number): ");
        if (int.TryParse(Console.ReadLine(), out int idx) && idx >= 1 && idx <= pending.Count)
        {
            pending[idx - 1].ApproveLoan();
            SaveUsersToFile();
        }
    }

    private void DeleteClient()
    {
        var clients = users.OfType<Client>().ToList();
        if (!clients.Any()) { Console.WriteLine("\nNo clients to delete."); return; }
        Console.WriteLine("");
        for (int i = 0; i < clients.Count; i++)
        {
            Console.WriteLine($"{i + 1} - {clients[i].Username}");
        }
        Console.Write("\nWhich one to delete (number): ");
        if (int.TryParse(Console.ReadLine(), out int idx) && idx >= 1 && idx <= clients.Count)
        {
            users.Remove(clients[idx - 1]);
            SaveUsersToFile();
        }
    }
}