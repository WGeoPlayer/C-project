using System.Text.Json;
using PROject.Models;

namespace PROject.Services;

public static class DataHandler
{
    private static readonly string Folder = Path.Combine(AppContext.BaseDirectory, "../../../data");
    private static readonly string FilePath = Path.Combine(Folder, "users.json");

    public static void Save(List<User> users)
    {
        if (!Directory.Exists(Folder)) Directory.CreateDirectory(Folder);
        string json = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(FilePath, json);
    }

    public static List<User> Load()
    {
        if (!File.Exists(FilePath)) return new List<User>();
        string json = File.ReadAllText(FilePath);
        return JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
    }
}