namespace ChatBot_TestTask.Logic;

public class Message
{
    public string Sender { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }

    public Message() { }

    public Message(string sender, string text)
    {
        Sender = sender;
        Text = text;
        Timestamp = DateTime.Now;
    }

    public Message(string sender, string text, DateTime time)
    {
        Sender = sender;
        Text = text;
        Timestamp = time;
    }

    public override string ToString() => $"{Timestamp} {Sender} {Text}";
}