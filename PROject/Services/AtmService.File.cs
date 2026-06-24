using PROject.Models;
using System.Text.Json;

namespace PROject.Services;

/// <summary>
/// Data access submodule of AtmService.
/// Demonstrates Compile-time Polymorphism via Method Overloading.
/// </summary>
public partial class AtmService
{

    private string GetFilePath()
    {
        string folder = "data";
        if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
        return Path.Combine(folder, "users.json");
    }
    private void SaveUsersToFile() => DataHandler.Save(users);

    private void LoadUsersFromFile()
    {
        users = DataHandler.Load();
    }
}