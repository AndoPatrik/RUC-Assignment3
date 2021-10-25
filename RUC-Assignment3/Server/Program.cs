using Server;
using System.Net;
using System.Net.Sockets;

TcpListener listener = null;
try
{
	listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 5000);
	listener.Start();
	Console.WriteLine("MultiThreadedEchoServer started...");
	while (true)
	{
		Console.WriteLine("Waiting for incoming client connections...");
		TcpClient client = listener.AcceptTcpClient();
		Console.WriteLine("Accepted new client connection...");
		Thread t = new Thread(Functions.ProcessClientRequests);
		t.Start(client);
	}
}
catch (Exception e)
{
	Console.WriteLine(e);
}
finally
{
	if (listener != null)
	{
		listener.Stop();
	}
}