using System.ComponentModel;
using chatApp.ViewModel;

namespace chatApp;

public partial class MainView : ContentPage
{
    public MainView(MainViewModel viewModel)
    {
        BindingContext = viewModel;
        InitializeComponent();
    }
    private void Entry_OnCompleted(object sender, EventArgs e)
    {
        MainViewModel viewModel = BindingContext as MainViewModel;
        viewModel.SendMessage();
    }

    private void ChatView_OnPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        MainViewModel viewModel = BindingContext as MainViewModel;
        if (viewModel.ChatHistory.Count > 0)
        {
            ChatView.ScrollTo(viewModel.ChatHistory.LastOrDefault(), position: ScrollToPosition.End);
        }
    }
}