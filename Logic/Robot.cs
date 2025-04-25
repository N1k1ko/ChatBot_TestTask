namespace ChatBot_TestTask.Logic;

class Robot
{
    private static readonly string[] Responses =
    [
        "Это круто! :D",
        "Это ужасно! D:",
        "Я — бот, а ты человек!"
    ];

    private Random _rand = new();

    public Message GetResponse() => new("BotIvan", Responses[_rand.Next(Responses.Length)]);
}