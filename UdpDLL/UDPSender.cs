using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace UdpDLL
{

    public class UDPSender
    {
        /// <summary>
        /// This function binds UdpClient to specific ip and port to send data to and returns the sender instance.
        /// </summary>
        public static async Task<UdpClient> Connect(string sReceiverIpAddress, int iReceiverPort)
        {
            try
            {
                IPAddress receiverIPAd = IPAddress.Parse(sReceiverIpAddress); // ip to send the data to
                IPEndPoint receiverEndPoint = new IPEndPoint(receiverIPAd, iReceiverPort); // endpoint (ip + port) of the receiver
                UdpClient sender = new UdpClient();

                sender.Connect(receiverEndPoint);
                return sender;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Used to send data to remote endpoint.
        /// </summary>
        /// <param name="udpClient">Udp client to be passed thats already binded with remote endpoint.</param>
        /// <param name="sMessage">Message to send to udp listener</param>
        public static void SendMessage(UdpClient udpClient, string sMessage)
        {
            try
            {
                byte[] msgBytes = Encoding.ASCII.GetBytes(sMessage);// convert string to corresponding byte[] for sending

                udpClient.Send(msgBytes, msgBytes.Length); // Sends the converted byte[] to the remote end point.
            }
            catch (Exception ex)
            {
                throw;// handled in caller
            }
        }
    }
}
