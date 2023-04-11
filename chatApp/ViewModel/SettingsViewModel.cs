using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using chatApp.Services;

namespace chatApp.ViewModel;

public class SettingsViewModel : INotifyPropertyChanged
{
    private MessageService _service;
    private IPEndPoint _ipEndPoint;
    public event PropertyChangedEventHandler PropertyChanged;

    private string _name = "";
    private string _endPoint = "";

    public string Name
    {
        get => _name;
        set
        {
            if (_name != value.Trim())
            {
                _name = value.Trim();
                _service.Name = _name;
                OnPropertyChanged();
            }
        }
    }

    public string EndPoint
    {
        get => _endPoint;
        set
        {
            if (_endPoint != value.Trim())
            {
                _endPoint = value.Trim();
                if (!IPEndPoint.TryParse(_endPoint, out _ipEndPoint))
                {
                    _ipEndPoint = null;
                }

                OnPropertyChanged();
            }
        }
    }

    public ICommand ConnectCommand { get; set; }

    public SettingsViewModel(MessageService service)
    {
        _service = service;
        ConnectCommand = new Command(() => {
                if (_service.Connect(_ipEndPoint, _service.Name))
                {
                    Shell.Current.DisplayAlert("successfully connected", "", "Ok");
                }
                else
                {
                    Shell.Current.DisplayAlert("Attention", "failed to connect", "Ok");
                }
            },
            () => _ipEndPoint is not null);
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        ((Command)ConnectCommand).ChangeCanExecute();
    }
}