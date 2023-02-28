package Random;

import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.OutputStreamWriter;
import java.net.ServerSocket;
import java.net.Socket;

public class SimpleServer {

    private static final String HOSTNAME = "localhost";
    private static final int PORT = 80;

    public static void startServer() throws IOException {

        // using ARM (Automated resource Manager, so that I don't have to manually close the socket and BufferedReader/Writer)
        try (ServerSocket serverSocket = new ServerSocket(PORT);) {
            System.out.println("Started Server");
            System.out.println("Waiting for Client to connect");

            // infinite loop, so that infinite clients are able to connect to the server
            while (true) {
                // using ARM (Automated resource Manager, so that I don't have to manually close the socket and BufferedReader/Writer)
                try (Socket socket = serverSocket.accept();
                     BufferedReader br = new BufferedReader(new InputStreamReader(socket.getInputStream()));
                     BufferedWriter bw = new BufferedWriter(new OutputStreamWriter(socket.getOutputStream()));) {

                    System.out.println("Client Successfully Connected");

                    String received = br.readLine();
                    System.out.println(received);

                    // important to send a newline (\r\n) with each message
                    bw.write("Hello Client\r\n");
                    bw.flush();
                    System.out.println("Server Send!");
                }
            }
        }
    }

    public static void main(String... args) throws IOException {
        SimpleServer.startServer();
    }

}
