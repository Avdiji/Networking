using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;

class Simple_Client {

    private const string HOSTNAME = "localhost";
    private const int PORT = 80;

    public Simple_Client() { }

    public void startClient() {
        IPHostEntry ipHostInfo = Dns.GetHostEntry(HOSTNAME);
        IPAddress ipAddress = ipHostInfo.AddressList[0];
        IPEndPoint remoteEP = new IPEndPoint(ipAddress, PORT);

        Socket client = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Unspecified);

        try {
            client.Connect(remoteEP);

            using (NetworkStream stream = new NetworkStream(client))
            using (StreamReader sr = new StreamReader(stream))
            using (StreamWriter sw = new StreamWriter(stream)) {

                sw.WriteLine("Hello Server!\n");
                sw.Flush();
                Console.WriteLine("Client Send!");

                string received = sr.ReadLine();
                Console.WriteLine("Client Received: {0}", received);

                //client.Shutdown(SocketShutdown.Both);
                //client.Close();
            }
        }
        catch (Exception e) { Console.WriteLine(e.ToString()); }
    }

}
