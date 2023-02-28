package Random;

import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.OutputStreamWriter;
import java.net.Socket;

public class SimpleClient {

    private static String HOSTNAME = "localhost";
    private static int PORT = 80;

    public static void startClient() throws IOException {

        // using ARM (Automated resource Manager, so that I don't have to manually close the socket and BufferedReader/Writer)
        try (Socket socket = new Socket(HOSTNAME, PORT);
             BufferedReader br = new BufferedReader(new InputStreamReader(socket.getInputStream()));
             BufferedWriter bw = new BufferedWriter(new OutputStreamWriter(socket.getOutputStream()));) {

            // important to send a newline (\r\n) with each message
            bw.write("Hello Server!\r\n");
            bw.flush();
            System.out.println("Client Send!");

            String received = br.readLine();
            System.out.printf("Client Received: %s", received);
        }
    }

    public static void main(String... args) throws IOException {
        SimpleClient.startClient();
    }
}
