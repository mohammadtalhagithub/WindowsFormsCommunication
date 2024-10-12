using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UdpDLL
{
    /// <summary>
    /// This class is created in order to listen to same port with 2 separate upd listeners
    /// </summary>
    public class MultipleListnerUsingThread
    {
        private UdpClient _udpClient;
        private object _lock = new object();

        public MultipleListnerUsingThread(int port)
        {
            _udpClient = new UdpClient(port);
        }

        public void Start()
        {
            // Create a thread to receive incoming messages
            Thread receiveThread = new Thread(ReceiveMessages);
            receiveThread.Start();
        }

        private void ReceiveMessages()
        {
            while (true)
            {
                IPEndPoint remoteEndPoint = null;
                byte[] data = _udpClient.Receive(ref remoteEndPoint);
                string message = Encoding.ASCII.GetString(data);

                // Process the message in a separate thread
                ProcessMessage(message, remoteEndPoint);
            }
        }

        private void ProcessMessage(string message, IPEndPoint remoteEndPoint)
        {
            // Create a new thread to process the message
            Thread processThread = new Thread(() => HandleMessage(message, remoteEndPoint));
            processThread.Start();
        }

        private void HandleMessage(string message, IPEndPoint remoteEndPoint)
        {
            // Simulate some processing time
            Thread.Sleep(1000);

            Console.WriteLine($"Processed message from {remoteEndPoint}: {message}");
        }
    }
}
