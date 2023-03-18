using System.Net;
using System.Net.Sockets;
using System.Text;



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

    Console.WriteLine($"server: {server}. Trying start listening...");
    try
    {
        socket.Bind(server);
        socket.Listen();
        Console.WriteLine("Listening started");
        isConnected = true;
    }
    catch (SocketException)
    {
        Console.WriteLine("start failed. Try again?(y/other)");
        if (Console.ReadLine() != "y")
        {
            return;
        }
    }
}

while (true)
{
    using Socket clientSocket = socket.Accept();
    Console.WriteLine("Connection successfully");
    
    StringBuilder recieveMessage = new StringBuilder();
    int bytesRead = 0;
    byte[] buffer = new byte[256];
    
    do
    {
        bytesRead = clientSocket.Receive(buffer);
        string text = Encoding.UTF8.GetString(buffer, 0, bytesRead);
        recieveMessage.Append(text);
    } while (bytesRead > 0);

    Console.WriteLine($"{DateTime.Now.ToShortTimeString()}: {recieveMessage}");
    
    byte[] outputMessage = Encoding.UTF8.GetBytes("message received");
    clientSocket.Send(outputMessage);
        
    clientSocket.Shutdown(SocketShutdown.Both);
}