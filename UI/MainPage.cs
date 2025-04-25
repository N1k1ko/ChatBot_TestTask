using ChatBot_TestTask.Logic;
using System.Collections.ObjectModel;

namespace ChatBot_TestTask.UI;

public partial class MainPage : ContentPage
{
    private ListView listView;
    private Button addButton;
    private ObservableCollection<Chat> Chats { get; set; } = [];

    public MainPage()
    {
        Title = "Чаты";

        listView = new ListView
        {
            ItemTemplate = new DataTemplate(() =>
            {
                var titleLabel = new Label { FontAttributes = FontAttributes.Bold };
                titleLabel.SetBinding(Label.TextProperty, "Title");

                var descrLabel = new Label { FontSize = 12 };
                descrLabel.SetBinding(Label.TextProperty, "Description");

                var layout = new StackLayout { Padding = 10 };
                layout.Children.Add(titleLabel);
                layout.Children.Add(descrLabel);

                return new ViewCell { View = layout };
            })
        };

        listView.ItemTapped += async (s, e) =>
        {
            if (e.Item is Chat chat)
                await Navigation.PushAsync(new ChatPage(chat, SaveChats));
        };

        addButton = new Button
        {
            Text = "➕ Новый чат",
            HorizontalOptions = LayoutOptions.Fill,
            Margin = new Thickness(10)
        };

        addButton.Clicked += async (s, e) =>
        {
            Chats.Add(new("Бот", new("BotIvan", "Дратути")));
            await SaveChats();
            listView.ItemsSource = null;
            listView.ItemsSource = Chats;
        };

        Content = new StackLayout
        {
            Margin = new Thickness(15),
            Children = { listView, addButton },
            Spacing = 0
        };
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        Chats = await LocalStorageService.LoadAsync();
        listView.ItemsSource = Chats;
    }

    private async Task SaveChats() => await LocalStorageService.SaveAsync(Chats);
}
