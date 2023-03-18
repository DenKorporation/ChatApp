using System.Net;
using System.Net.Sockets;
using System.Text;



Console.WriteLine("Enter your message:");
var outputMessage = Console.ReadLine();

IPEndPoint server;
using Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

bool isConnected = false;

while (!isConnected)
{
    Console.WriteLine("Choose EndPoint: ");
    while (!IPEndPoint.TryParse(Console.ReadLine(), out server))
    {
        Console.WriteLine("Try again");
    }

    Console.WriteLine($"server: {server}. Trying connect...");
    try
    {
        socket.Connect(server);
        Console.WriteLine("Connection successfully");
        isConnected = true;
    }
    catch (SocketException)
    {
        Console.WriteLine("Connect failed. Try again?(y/other)");
        if (Console.ReadLine() != "y")
        {
            return;
        }
    }
}

socket.Send(Encoding.UTF8.GetBytes(outputMessage));

StringBuilder recieveMessage = new StringBuilder();
int bytesRead = 0;
byte[] buffer = new byte[256];
    
do
{
    bytesRead = socket.Receive(buffer);
    string text = Encoding.UTF8.GetString(buffer, 0, bytesRead);
    recieveMessage.Append(text);
} while (bytesRead > 0);

Console.WriteLine($"Reply: {recieveMessage}");