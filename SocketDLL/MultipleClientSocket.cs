using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;

namespace SocketDLL
{
    public class MultipleClientSocket
    {
        public static async void CreateListner(int port)
        {
            Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // Bind to the local IP address and port
            IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Any, port);
            listener.Bind(localEndPoint);

            // the backlog parameter specifies the maximum length of the pending connections queue. This is the number of connections that can be pending acceptance by the server before the system starts rejecting new connections.
            listener.Listen(10);
            // the backlog parameter specifies the

            MessageBox.Show($"Server is listening on port {port}...");

            // Infinite loop to keep the server running
            while (true)
            {
                // Accept a new client connection
                Socket clientSocket = listener.Accept();
                MessageBox.Show("Client connected!");
                // Create a new thread to handle this client's communication
                Thread clientThread = new Thread(() => HandleClient(clientSocket));
                clientThread.Start(); // Start the thread
                Thread.Sleep(1);

            }

        }


        /// <summary>
        /// This method is used to handle communication with a specific client
        /// </summary>
        /// <param name="clientSocket"></param>
        static void HandleClient(Socket clientSocket)
        {
            byte[] buffer = new byte[1024];
            int bytesReceived;

            try
            {
                // Continuously receive data from the client
                // only continue if byte[] length is > 0
                while ((bytesReceived = clientSocket.Receive(buffer)) > 0)
                {
                    string data = Encoding.ASCII.GetString(buffer, 0, bytesReceived);
                    Console.WriteLine("Received from client: " + data);

                    // Send a response back to the client
                    byte[] msg = Encoding.ASCII.GetBytes("Data received successfully");
                    clientSocket.Send(msg);
                }
            }
            catch (SocketException se)
            {
                Console.WriteLine("SocketException: {0}", se.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e.Message);
            }
            finally
            {
                // Close the client socket after communication is done
                clientSocket.Shutdown(SocketShutdown.Both);
                clientSocket.Close();
                Console.WriteLine("Client disconnected.");
            }
        }

    }
}
