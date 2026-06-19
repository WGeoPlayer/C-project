using PROject.Models;

namespace PROject.Services;

public partial class AtmService
{
    private void HandleDeposit(Client client)
    {
        Console.Write("Amount to deposit: ");
        if (double.TryParse(Console.ReadLine(), out double amt) && amt > 0)
        {
            client.Deposit(amt);
            SaveUsersToFile(client);
            Console.WriteLine($"Done. New balance:{client.Balance} {(client.Balance == 0 ? "" : "$")}");
        }
        else Console.WriteLine("Invalid amount.");
    }

    private void HandleWithdraw(Client client)
    {
        Console.Write("Amount to withdraw: ");
        if (double.TryParse(Console.ReadLine(), out double amt) && amt > 0)
        {
            if (client.Withdraw(amt))
            {
                SaveUsersToFile(client);
                Console.WriteLine($"Done. New balance:{client.Balance} {(client.Balance == 0 ? "" : "$")}");
            }
            else Console.WriteLine("Insufficient funds.");
        }
        else Console.WriteLine("Invalid amount.");
    }

    private void HandleLoan(Client client)
    {
        if (client.LoanRequested > 0)
        {
            Console.WriteLine("You already have a pending loan request.");
        }
        else
        {
            Console.Write("Loan amount: ");
            if (double.TryParse(Console.ReadLine(), out double amt) && amt > 0)
            {
                client.LoanRequested = amt;
                SaveUsersToFile(client);
                Console.WriteLine("Request sent.");
            }
            else Console.WriteLine("Invalid amount.");
        }
    }
}