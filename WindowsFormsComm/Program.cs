using SocketDLL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsComm
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// <para>STAThread: This attribute specifies that the COM (Component Object Model) threading model for the application is Single-Threaded Apartment (STA).</para>
        /// <para>STA ensures that certain objects, such as Windows Forms controls and certain COM components, are accessed by only one thread at a time, which is important for UI elements that are not thread-safe.</para>
        /// </summary>
        [STAThread]
        static void Main()
        {
            TestFunction();


            // Visual styles give your application the current Windows theme appearance, such as modern buttons, controls, etc., instead of the older Windows Classic look. This makes the app more consistent with the operating system’s appearance.
            Application.EnableVisualStyles();

            // This specifies which text rendering engine the application should use for its controls.
            // Setting this to false makes the application use GDI+ for rendering text, which is the standard rendering engine for modern Windows Forms controls.
            // If set to true, it uses GDI for rendering text, which was used in older applications. Modern apps typically use false to ensure better text rendering and Unicode support.
            Application.SetCompatibleTextRenderingDefault(false);

            // This starts the application and opens the main form, here, it is SPAForm.
            // Initiates the Windows Forms message loop, which listens for user input and other events like mouse clicks, keyboard input, and system messages.
            // The argument new SPAForm() creates an instance of your SPAForm class (which is presumably your main form) and shows it as the starting window of the application.
            Application.Run(new SPAForm());
        }

        static void TestFunction()
        {
            try
            {
                MultiThreading_CLS.Using_Thread();
                // Create a UdpClient for sending data.
                UdpClient sendClient = new UdpClient();
                string sReceiverIpAddress = "127.0.0.1";
                int iReceiverPort = 11001;
                IPAddress receiverIPAd = IPAddress.Parse(sReceiverIpAddress);
                IPEndPoint receiverEndPoint = new IPEndPoint(receiverIPAd, iReceiverPort);
                sendClient.Connect(receiverEndPoint);
                // Send a message to the specified endpoint.
                string message = "Hello, UDP!";
                byte[] data = Encoding.UTF8.GetBytes(message);
                sendClient.Send(data, data.Length, "127.0.0.1", 11000);

                // Create a UdpClient for reading incoming data.
                UdpClient receiveClient = new UdpClient(11000);
                
                Console.WriteLine($"Sent: {message}");

                // Receive a message from any IP address.
                IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);
                byte[] receivedData = receiveClient.Receive(ref remoteEndPoint);
                string receivedMessage = Encoding.UTF8.GetString(receivedData);
                Console.WriteLine($"Received: {receivedMessage}");

                // Close the clients
                sendClient.Close();
                receiveClient.Close();

            }
            catch (Exception ex)
            {

            }
        }
    }
}