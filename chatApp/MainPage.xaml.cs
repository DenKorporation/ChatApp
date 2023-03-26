using System.Collections.ObjectModel;

namespace chatApp;

public partial class MainPage : ContentPage
{
    public ObservableCollection<Message> ChatHistory = new();

    public MainPage()
    {
        InitializeComponent();

        ChatView.ItemsSource = ChatHistory;
    }

    private void EntryField_OnCompleted(object sender, EventArgs e)
    {
        if ((EntryField.Text ?? "") != "")
        {
            ChatHistory.Add(new Message(EntryField.Text, "anonymous"));
            // ChatHistory.Add(EntryField.Text);
            ChatView.ScrollTo(ChatHistory.Last(x => x.Text == EntryField.Text), position: ScrollToPosition.End);
            EntryField.Text = "";
        }
    }
}