using PROject.Models;

namespace PROject.Services;

/// <summary>
/// Data access submodule of AtmService.
/// Demonstrates Compile-time Polymorphism via Method Overloading.
/// </summary>
public partial class AtmService
{
    /// <summary>Overload 1: Persists all runtime user records to the text file.</summary>
    private void SaveUsersToFile()
    {
        List<string> lines = users.Select(u => u.ToFileLine()).ToList();
        File.WriteAllLines(filePath, lines);
    }

    /// <summary>Overload 2: Persists data and triggers a specific system logging notification.</summary>
    private void SaveUsersToFile(User specificUser)
    {
        SaveUsersToFile();
        Console.WriteLine($"[System Log]: Database updated for user {specificUser.Username}");
    }

    /// <summary>Parses the flat-file database and populates user collections into memory.</summary>
    private void LoadUsersFromFile()
    {
        if (!File.Exists(filePath)) { return; }
        foreach (string line in File.ReadAllLines(filePath))
        {
            if (string.IsNullOrWhiteSpace(line)) { continue; }
            string[] parts = line.Split(';');
            if (parts[0] == "client"){users.Add(new Client(parts[1], parts[2], double.Parse(parts[3]), double.Parse(parts[4])));}
            else if (parts[0] == "admin") { users.Add(new Admin(parts[1], parts[2])); }
        }
    }
}