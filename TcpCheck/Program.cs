using System;
using System.Net.Sockets;
using System.Text;

class TcpClientProgram
{
    static void Main(string[] args)
    {
        string serverIP = "192.168.2.150"; // Replace with your server's IP
        int port = 9001; // Replace with your server's port
        try
        {
            // Create a TCP client
            TcpClient client = new TcpClient();

            Console.WriteLine("Connecting to server...");
            client.Connect(serverIP, port);

            Console.WriteLine("Connected to server.");
            NetworkStream stream = client.GetStream();

            // Continuous loop for sending and receiving messages
            while (true)
            {
                Console.Write("Enter a message to send (or type 'exit' to quit): ");
                string messageToSend = Console.ReadLine();

                // Exit the loop if the user types "exit"
                if (messageToSend.ToLower() == "exit")
                {
                    break;
                }

                // Send the message to the server
                byte[] dataToSend = Encoding.ASCII.GetBytes(messageToSend + "\r\n"); // Adding CRLF as per protocol
                stream.Write(dataToSend, 0, dataToSend.Length);
                Console.WriteLine("Message sent to server.");

                // Receive response from the server
                byte[] buffer = new byte[1024];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                string response = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                Console.WriteLine("Received from server: " + response);
            }

            // Close the connection after the loop ends
            stream.Close();
            client.Close();
            Console.WriteLine("Connection closed.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }
}
