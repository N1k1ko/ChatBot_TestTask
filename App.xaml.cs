using ChatBot_TestTask.UI;

namespace ChatBot_TestTask;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        MainPage = new NavigationPage(new MainPage());
    }
}
