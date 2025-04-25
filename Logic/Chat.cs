using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

namespace ChatBot_TestTask.Logic;

public class Chat
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ObservableCollection<Message> Messages { get; set; } = [];

    [JsonIgnore]
    private readonly Robot Bot = new();

    public Chat() { } // нужен для десериализации

    public Chat(string title)
    {
        Title = title;
    }

    public Chat(string title, Message message) : this(title)
    {
        Messages.Add(message);
        Update();
    }

    public void SendMessage(string message)
    {
        Messages.Add(new("UserPavel", message));
        Messages.Add(Bot.GetResponse());
        Update();
    }

    public void Update()
    {
        if (Messages.Any())
            Description = Messages.Last().ToString();
    }
}
