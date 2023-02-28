using System;
using System.Text;
using System.Net;
using System.Net.Sockets;


// Advantage of Synchronous vs asynchronous in TCP socket connection
//
//Async IO saves threads. A thread consumes (usually) 1MB of stack memory.
//This is the main reason to use async IO when the number of concurrent outstanding IO operations becomes big.
//According to my measurements OS scalability is not a concern until you get into the thousands of threads.
//The main disadvantage is that it requires more development effort to make the same application work at the same level of reliability.
//
//
// -> use Synchronous TCP Sockets for own projects
//
//
//https://docs.microsoft.com/de-de/dotnet/framework/network-programming/synchronous-server-socket-example

class Simple_TCPServer {

    private const string HOSTNAME = "localhost";
    private const int PORT = 43;

    public Simple_TCPServer() { }

    public void startServer() {
        IPHostEntry ipHostInfo = Dns.GetHostEntry(HOSTNAME);
        IPAddress ipAddress = ipHostInfo.AddressList[0];
        IPEndPoint localEP = new IPEndPoint(ipAddress, PORT);

        Socket listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

        try {
            listener.Bind(localEP);
            listener.Listen(1);


            // listen for connections
            while (true) {
                Console.WriteLine("Waiting for a Connection");

                Socket handler = listener.Accept();

                receiveData(handler);
                sendData(handler, "Hello Client!");

                handler.Shutdown(SocketShutdown.Both);
                handler.Close();
            }
        }
        catch (Exception e) {
            Console.WriteLine(e.ToString());
        }
    }

    public void receiveData(Socket handler) {
        byte[] bytes = new byte[1024];
        int bytesRec = handler.Receive(bytes);
        Console.WriteLine("Received Message from Client to Server: {0}", Encoding.ASCII.GetString(bytes, 0, bytesRec));
    }

    public void sendData(Socket handler, string msg) {
        byte[] send = Encoding.ASCII.GetBytes(msg);
        handler.Send(send);
        Console.WriteLine("Message Sent from Client to Server!");
    }

}

