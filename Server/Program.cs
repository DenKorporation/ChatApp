using System.Net;
using System.Net.Sockets;
using ServerLib;


IPEndPoint serverIp;

bool isConnected = false;

ServerObject server = new ServerObject(); // создаем сервер

while (!isConnected)
{
    Console.WriteLine("Choose EndPoint: ");
    while (!IPEndPoint.TryParse(Console.ReadLine(), out serverIp))
    {
        Console.WriteLine("Try again");
    }

    Console.WriteLine($"server: {serverIp}. Trying start listening...");
    try
    {
        server.tcpListener = new TcpListener(serverIp);
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

await server.ListenAsync(); // запускаем сервер
 