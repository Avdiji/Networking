using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;

class Simple_Server {

    private const string HOSTNAME = "localhost";
    private const int PORT = 80;

    public Simple_Server() { }

    public void startServer() {
        IPHostEntry ipHostInfo = Dns.GetHostEntry(HOSTNAME);
        IPAddress ipAddress = ipHostInfo.AddressList[0];
        IPEndPoint localEP = new IPEndPoint(ipAddress, PORT);

        Socket server = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Unspecified);

        try {
            server.Bind(localEP);
            server.Listen(1);

            Console.WriteLine("Waiting for Connection...");
            Socket client = server.Accept();
            Console.WriteLine("Client Successfully Connected!");

            using (NetworkStream stream = new NetworkStream(client))
            using (StreamReader sr = new StreamReader(stream))
            using (StreamWriter sw = new StreamWriter(stream)) {

                string received = sr.ReadLine();
                Console.WriteLine("Server Received: {0}", received);

                sw.WriteLine("Hello Client!");
                sw.Flush();
                Console.WriteLine("Server Send!");


                //client.Shutdown(SocketShutdown.Both);
                //client.Close();
            }
            //server.Shutdown(SocketShutdown.Both);
            //server.Close();
        }
        catch (Exception e) { Console.WriteLine(e.ToString()); }

        while (true) ;
    }
}
