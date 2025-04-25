using ChatBot_TestTask.Logic;
using System.Collections.ObjectModel;

namespace ChatBot_TestTask.UI;

public partial class ChatPage : ContentPage
{
    private Func<Task> _saveCallback;
    private ObservableCollection<Message> _messages;
    private Entry _entry;
    private Chat _chat;

    public ChatPage(Chat chat, Func<Task> saveCallback)
    {
        _saveCallback = saveCallback;
        _chat = chat;
        _messages = chat.Messages;

        Title = chat.Title;
        #region Content
        Grid grid = new()
        {
            Margin = new Thickness(15),
            RowDefinitions =
            {
                new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                new RowDefinition { Height = GridLength.Auto },
            },
            ColumnDefinitions =
            {
                new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                new ColumnDefinition { Width = GridLength.Auto },
            }
        };

        var listView = new ListView
        {
            ItemsSource = _messages,
            HasUnevenRows = true,
            ItemTemplate = new DataTemplate(() =>
            {
                Label msgLabel = new();
                msgLabel.SetBinding(Label.TextProperty, "Text");

                Label senderLabel = new() { FontSize = 10, TextColor = Colors.Gray };
                senderLabel.SetBinding(Label.TextProperty, "Sender");

                Label timeLabel = new() { FontSize = 10, TextColor = Colors.Gray };
                timeLabel.SetBinding(Label.TextProperty, new Binding("Timestamp", stringFormat: "{0:HH:mm}"));

                StackLayout layout = new() { Padding = 5 };
                layout.Children.Add(senderLabel);
                layout.Children.Add(msgLabel);
                layout.Children.Add(timeLabel);

                return new ViewCell { View = layout };
            })
        };
        grid.SetRow(listView, 0);
        grid.SetColumnSpan(listView, 2);
        grid.Add(listView);

        _entry = new() { Placeholder = "Введите сообщение..." };
        grid.Add(_entry, 0, 1);

        Button sendButton = new() { Text = "Отправить", WidthRequest = 100 };
        sendButton.Clicked += OnSendClicked;
        grid.Add(sendButton, 1, 1);

        Content = grid;
        #endregion
    }

    private async void OnSendClicked(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(_entry.Text)) return;

        _chat.SendMessage(_entry.Text);
        _entry.Text = string.Empty;

        await _saveCallback();
    }
}