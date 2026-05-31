namespace PROject.Models;

class Admin : User
{
    public Admin(string username, string password)
        : base(username, password, "admin")
    {
    }

    public override void ShowMenu()
    {
        Console.WriteLine();
        Console.WriteLine("1 - Show all clients");
        Console.WriteLine("2 - Approve loans");
        Console.WriteLine("3 - Delete a client");
        Console.WriteLine("4 - Reset all data");
        Console.WriteLine("5 - Log out");
    }

    public override string ToFileLine()
    {
        return "admin;" + Username + ";" + Password;
    }
}
