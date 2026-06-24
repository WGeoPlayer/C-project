namespace PROject.Models;

/// <summary>
/// Represents a standard bank customer.
/// </summary>
public class Client : User
{
    /// <summary>Gets the current account funds.</summary>
    public double Balance { get; private set; }

    /// <summary>Gets or sets the requested loan amount.</summary>
    public double LoanRequested { get; set; }

    /// <summary>Initializes a new client with financial data.</summary>
    public Client(string username, string password, double balance, double loanRequested)
    : base(username, password, "client")
    {
        Balance = balance;
        LoanRequested = loanRequested;
    }

    /// <summary>Adds funds to the account.</summary>
    public void Deposit(double amount)
    {
        Balance += amount;
    }

    /// <summary>Removes funds if the balance is sufficient.</summary>
    public bool Withdraw(double amount)
    {
        if (amount > Balance) return false;
        Balance -= amount;
        return true;
    }

    /// <summary>Moves requested loan funds into the main balance.</summary>
    public void ApproveLoan()
    {
        Balance += LoanRequested;
        LoanRequested = 0;
    }

    /// <summary>Prints the banking operations menu.</summary>
    public override void ShowMenu()
    {
        Console.WriteLine("\n1 - Check balance\n2 - Deposit\n3 - Withdraw\n4 - Request loan\n5 - Delete my account\n6 - Log out");
    }

    /// <summary>Formats client data for text file storage.</summary>
    public override string ToFileLine()
    {
        return "client;" + Username + ";" + Password + ";" + Balance + ";" + LoanRequested;
    }
}