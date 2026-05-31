namespace PROject.Models;

abstract class User
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }

    public User(string username, string password, string role)
    {
        Username = username;
        Password = password;
        Role = role;
    }

    public abstract void ShowMenu();

    public abstract string ToFileLine();
}
