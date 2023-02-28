using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;

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
//https://docs.microsoft.com/de-de/dotnet/framework/network-programming/synchronous-client-socket-example

class Simple_TCPClient {

    public const string HOSTNAME = "localhost";
    public const int PORT = 43;

    private Socket sender;

    public Simple_TCPClient() { }

    public void startClient() {
        // Establish the remote endpoint for the socket
        // this example uses port 43 on the local computer
        IPHostEntry ipHostInfo = Dns.GetHostEntry(HOSTNAME);
        IPAddress ipAddress = ipHostInfo.AddressList[0];  
        IPEndPoint remoteEP = new IPEndPoint(ipAddress, PORT);

        // create TCP/IP socket
        sender = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

        try {
            sender.Connect(remoteEP);
            Console.WriteLine("Socket connected to {0}", sender.RemoteEndPoint.ToString());

            sendData("Hello Server");
            receiveData();

            sender.Shutdown(SocketShutdown.Both);
            sender.Close();
        }
        catch (SocketException se) {
            Console.WriteLine("SocketException: {0}", se.ToString());
        }
        catch (ArgumentNullException ane) {
            Console.WriteLine("ArgumentNullException: {0}", ane.ToString());
        }

    }

    // Method to send data
    public void sendData(string msg) {
        byte[] send = Encoding.ASCII.GetBytes(msg);
        sender.Send(send);
        Console.WriteLine("Message Sent from Client to Server!");
    }

    // Method to receive data
    public void receiveData() {
        // Data buffer for incoming data
        byte[] bytes = new byte[1024];

        int bytesRec = sender.Receive(bytes);
        Console.WriteLine("Received Message from Server to Client: {0}", Encoding.ASCII.GetString(bytes, 0, bytesRec));
    }

}
