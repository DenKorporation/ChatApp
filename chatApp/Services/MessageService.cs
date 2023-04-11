using System.Net;
using ClientLib;

namespace chatApp.Services;

public class MessageService
{
    private string _name;
    private Client client = new();

    public string Name {
        get
        {
            if (String.IsNullOrEmpty(_name))
            {
                return "anonymous";
            }
            return _name;
        }
        set
        {
            _name = value;
        }
    }

    public bool Connected
    {
        get => client.Connected;
    }

    public bool Connect(IPEndPoint endPoint, string username)
    {
        client.Connect(endPoint, username);
        return client.Connected;
    }

    public async Task<string?> ReceiveMessageAsync()
    {
        return await client.ReceiveMessageAsync();
    }

    public async void SendMessageAsync(string message)
    {
        await client.SendMessageAsync(message);
    }
}