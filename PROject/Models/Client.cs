namespace PROject.Models;

class Client : User
{
    public double Balance { get; private set; }
    public double LoanRequested { get; set; }

    public Client(string username, string password, double balance, double loanRequested)
        : base(username, password, "client")
    {
        Balance = balance;
        LoanRequested = loanRequested;
    }

    public void Deposit(double amount)
    {
        Balance += amount;
    }

    public bool Withdraw(double amount)
    {
        if (amount > Balance)
        {
            return false;
        }
        Balance -= amount;
        return true;
    }

    public void ApproveLoan()
    {
        Balance += LoanRequested;
        LoanRequested = 0;
    }

    public override void ShowMenu()
    {
        Console.WriteLine();
        Console.WriteLine("1 - Check balance");
        Console.WriteLine("2 - Deposit");
        Console.WriteLine("3 - Withdraw");
        Console.WriteLine("4 - Request loan");
        Console.WriteLine("5 - Delete my account");
        Console.WriteLine("6 - Log out");
    }

    public override string ToFileLine()
    {
        return "client;" + Username + ";" + Password + ";" + Balance + ";" + LoanRequested;
    }
}
