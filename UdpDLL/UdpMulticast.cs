using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UdpDLL
{
    /// <summary>
    /// 
    /// </summary>
    public class UdpMulticast
    {

        /// <summary>
        /// <para>This implementation is used to connect multiple Udp listeners with same port on same machine.</para>
        /// </summary>
        /// <param name="sIPAddress">The range for addresses is from 224.0.0.0 to 239.255.255.255.</param>
        public static void IPV4(string sIPAddress, int multicastPort, ListBox recMsgLstBx, Button ListenBtn)
        {
            try
            {
                IPAddress multicastAddress = IPAddress.Parse(sIPAddress); // "239.0.0.222"
                UdpClient listener = new UdpClient();

                // SocketOptionLevel.Socket is used rather than SocketOptionLevel.Udp because in this context, at this level we are modifying the socket itself for SO_REUSEADDR as they affect the behavior of the socket as a whole.
                // if SocketOptionLevel.Udp is passed in current ReuseAddress context, exception is thrown
                // bool optionValue in SetSocketOption should be passed as true otherwise we cannot Reuse the same port
                listener.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);

                listener.JoinMulticastGroup(multicastAddress); // multicast ipv4 239.0.0.222
                listener.Client.Bind(new IPEndPoint(IPAddress.Any, multicastPort));

                ListenBtn.ForeColor = Color.Red;
                ListenBtn.Text = "Stop Listening";
                // this remoteEndPoint will contain endpoint details of the sender after data has been received from the sender.
                IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);
                while (true)
                {
                    var result = listener.Receive(ref remoteEndPoint);
                    string message = Encoding.UTF8.GetString(result);
                    recMsgLstBx.Items.Add(message);

                    Thread.Sleep(1);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}

