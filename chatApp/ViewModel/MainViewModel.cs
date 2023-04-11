using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json;
using chatApp.Services;

namespace chatApp.ViewModel;

public class MainViewModel : INotifyPropertyChanged
{
    private string _entryText;
    private MessageService _service;

    public ObservableCollection<Message> ChatHistory { get; } = new();
    public event PropertyChangedEventHandler PropertyChanged;
    
    public string EntryText
    {
        get => _entryText;
        set
        {
            if (_entryText != value.Trim())
            {
                _entryText = value.Trim();

                OnPropertyChanged();
            }
        }
    }

    public MainViewModel(MessageService service)
    {
        _service = service;
        Task.Run(() => ListenMessages());
    }
    
    private async void ListenMessages()
    {
        while (true)
        {
            if (_service.Connected)
            {
                string? messageStr = await _service.ReceiveMessageAsync();
                if (!String.IsNullOrEmpty(messageStr))
                {
                    Message receivedMessage = JsonSerializer.Deserialize<Message>(messageStr);
                    ChatHistory.Add(receivedMessage);
                }
            }
        }
    }

    public void SendMessage()
    {
        if (!String.IsNullOrEmpty(EntryText))
        {
            Message curMessage = new Message(_service.Name, EntryText);
            ChatHistory.Add(curMessage);
            EntryText = "";
            Task.Run(() => _service.SendMessageAsync(JsonSerializer.Serialize(curMessage)));
        }
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}