using System.Collections.ObjectModel;
using System.Text.Json;

namespace ChatBot_TestTask.Logic;

public static class LocalStorageService
{
    private static string FilePath => Path.Combine(FileSystem.AppDataDirectory, "chats.json");

    public static async Task SaveAsync(ObservableCollection<Chat> chats)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        var json = JsonSerializer.Serialize(chats, options);
        await File.WriteAllTextAsync(FilePath, json);
    }

    public static async Task<ObservableCollection<Chat>> LoadAsync()
    {
        if (!File.Exists(FilePath))
            return new ObservableCollection<Chat>(
            [
                new Chat("Бот 1", new Message("BotIvan", "Привет!")),
                new Chat("Бот 2", new Message("BotIvan", "Хей!", DateTime.Now.AddMinutes(-5))),
            ]);

        var json = await File.ReadAllTextAsync(FilePath);
        return JsonSerializer.Deserialize<ObservableCollection<Chat>>(json)
            ?? throw new InvalidDataException("Данные чатов пусты или повреждены.");
    }
}