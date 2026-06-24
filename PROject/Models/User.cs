using System.Text.Json.Serialization;

namespace PROject.Models;

[JsonDerivedType(typeof(Client), "client")]
[JsonDerivedType(typeof(Admin), "admin")]

/// <summary>
/// Represents a generic system user.
/// </summary>
public abstract class User
{
    public Guid Id { get; set; }
    /// <summary>Gets or sets the account username.</summary>
    public string Username { get; set; }

    /// <summary>Gets or sets the account password.</summary>
    public string Password { get; set; }
    
    /// <summary>Gets or sets the email address.</summary>
    public string Email { get; set; }

    /// <summary>Gets or sets the access level role.</summary>
    public string Role { get; set; }

    /// <summary>Initializes a new user account.</summary>
    public User(string username, string password, string email, string role)
    {
        Username = username;
        Password = password;
        Email = email;
        Role = role;
    }

    /// <summary>Displays the available actions for the user.</summary>
    public abstract void ShowMenu();
}