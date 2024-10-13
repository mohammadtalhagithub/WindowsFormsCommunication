using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SocketDLL
{
    /// <summary>
    /// This class is used to implement socket conection
    /// </summary>
    public class SocketV2
    {
        public static void Server()
        {
            // Create a TCP socket
            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Bind(new IPEndPoint(IPAddress.Any, 8080));// bind to local endpoint
            serverSocket.Listen(5);
            Debug.WriteLine($"Server started at {serverSocket.LocalEndPoint}");

            while (true)
            {
                // Accept a client connection
                Socket clientSocket = serverSocket.Accept();
                Console.WriteLine("Client connected.");

                // Receive data
                byte[] buffer = new byte[1024];
                int bytesRead = clientSocket.Receive(buffer);
                string received = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                Console.WriteLine("Received: " + received);

                // Send a response
                string response = "Echo: " + received;
                byte[] responseData = Encoding.UTF8.GetBytes(response);
                clientSocket.Send(responseData);

                // Close the client socket
                clientSocket.Close();
            }
        }

        public static void Client(string sServerIP, int iServerPort, string sMessage)
        {
            // Create a TCP socket
            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            clientSocket.Connect("127.0.0.1", iServerPort); // OR localhost : 8080

            // Store message in buffer (byte[])
            byte[] data = Encoding.UTF8.GetBytes(sMessage);
            clientSocket.Send(data);
            Console.WriteLine("Sent: " + sMessage);

            // Receive a response
            byte[] buffer = new byte[1024];
            int bytesRead = clientSocket.Receive(buffer);
            string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            Console.WriteLine("Received: " + response);

            // Close the socket
            clientSocket.Close();
        }

        
        /// <summary>
        /// This function will only be called when connecting to a server.
        /// </summary>
        /// <param name="sServerIP"></param>
        /// <param name="iServerPort"></param>
        /// <returns></returns>
        public static Socket CreateClient(string sServerIP, int iServerPort)
        {
            try
            {
                Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                
                clientSocket.Connect(sServerIP, iServerPort);
                return clientSocket;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static void SendToServer(string Message, Socket clientSocket, Form form)
        {
            try
            {
                byte[] data = Encoding.UTF8.GetBytes(Message); // string to byte[] for sending to socket
                clientSocket.Send(data); // send byte[] to the socket.
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        bool IsClientConnected = false;
        /// <summary>
        /// This function is used to get the client socket if its trying to connect to the listening server.
        /// <para>Will be called after a server socket has started listening.</para>
        /// </summary>
        /// <param name="sMessage"></param>
        /// <param name="listeningServerSocket"></param>
        /// <param name="form"></param>
        /// <param name="lstBox"></param>
        /// <returns></returns>
        public static async Task ConnectToClient(string sMessage, Socket listeningServerSocket, Form form, ListBox lstBox, Button ListenBtn)
        {
            try
            {
                await Task.Run(() =>
                {
                    Socket clientSocket = listeningServerSocket.Accept(); // accept the connection, this is blocking operation

                    if (form.InvokeRequired)
                    {
                        form.Invoke(new Action(() =>
                        {
                            MessageBox.Show($"Successfully connected to the Client : {clientSocket.RemoteEndPoint}");
                            ListenBtn.ForeColor = Color.Red;
                            ListenBtn.Text = "Stop Listening";
                        }));
                    }
                    else
                    {
                        MessageBox.Show($"Successfully connected to the Client : {clientSocket.RemoteEndPoint}");
                        ListenBtn.ForeColor = Color.Red;
                        ListenBtn.Text = "Stop Listening";
                    }

                    if (false)
                    {
                        while (clientSocket.Connected == true)
                        {
                            // implementaion if all data is not received in provided buffer size:

                            // this list will grow dynamically untill all data is added from multiple buffers received depending on the buffer size provided.
                            List<byte> listBytes = new List<byte>();
                            int bytesCount;
                            
                            do
                            {
                                // logic pending to exit this loop if no data is received
                                byte[] buffer = new byte[10]; // initialize buffer size
                                bytesCount = clientSocket.Receive(buffer);
                                if (bytesCount > 0)
                                {
                                    // if data is received in buffer, Add to List<byte>
                                    listBytes.AddRange(buffer.Take(bytesCount));
                                }
                                else
                                {
                                    break;
                                }
                            }
                            while (bytesCount > 0);
                            // final processing of the data: 
                            // convert List<byte> to byte[] and then finally to string
                            string message = Encoding.UTF8.GetString(listBytes.ToArray());
                            if (form.InvokeRequired)
                            {
                                form.Invoke(new Action(() =>
                                {
                                    lstBox.Items.Add(message);
                                }));
                            }
                            else
                            {
                                lstBox.Items.Add(message);
                            }
                            Thread.Sleep(1);
                        }
                    }
                    else
                    {
                        // when successfully tested merging buffer implementation in other if conditoin, remove this block
                       // int bytesReadCheck = 0;
                        do
                        {
                            byte[] buffer = new byte[2048]; // initialize buffer size
                            int bytesRead = clientSocket.Receive(buffer); // receive incoming data from sender.
                            if (bytesRead == 0)
                            {
                                break; // this break condition needs to be changed, depending on what special char or delimiter
                                // is passed from the sender.
                            }
                            string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                            if (form.InvokeRequired)
                            {
                                // true if 
                                form.Invoke(new Action(() =>
                                {
                                    lstBox.Items.Add(response);
                                }));
                            }
                            else
                            {
                                lstBox.Items.Add(response);
                            }
                            Thread.Sleep(10);
                        }
                        while (true);
                    }
                });

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Exception when server connecting to the client...!");
                return;
            }
        }

        public static Socket CreateServer(int iServerPort)
        {
            try
            {
                Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp); // using Tcp with socket
                serverSocket.Bind(new IPEndPoint(IPAddress.Any, iServerPort)); // bind the socket to ip and port 
                // the backlog parameter specifies the maximum length of the pending connections queue. This is the number of connections that can be pending acceptance by the server before the system starts rejecting new connections.
                serverSocket.Listen(5);

                return serverSocket;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
