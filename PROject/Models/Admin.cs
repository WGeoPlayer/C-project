namespace PROject.Models;

/// <summary>
/// Represents a system administrator.
/// </summary>
public class Admin : User
{
    /// <summary>Initializes a new administrator account.</summary>
    public Admin(string username, string password) : base(username, password, "admin")
    {
    }

    /// <summary>Prints the administrative control menu.</summary>
    public override void ShowMenu()
    {
        Console.WriteLine("\n1 - Show all clients\n2 - Approve loans\n3 - Delete a client\n4 - Reset all data\n5 - Log out");
    }

    /// <summary>Formats admin data for text file storage.</summary>
    public override string ToFileLine()
    {
        return "admin;" + Username + ";" + Password;
    }
}