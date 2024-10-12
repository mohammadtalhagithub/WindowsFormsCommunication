using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace UdpDLL
{
    public class ListenerModel
    {
        public UdpClient UdpReceiver { get; set; }
        public TextBox ReceiverPortTxtBx { get; set; }
        public Button ListenBtn { get; set; }
        public ListBox ReceiveMsgLstBx { get; set; }
        public ListBox ExceptionListBox { get; set; }
    }

    public class UDPReceiver
    {
        public static void StartReceivingV2__(ListenerModel listenerModel, Form form)
        {
            try
            {
                int iPort = Convert.ToInt32(listenerModel.ReceiverPortTxtBx.Text);
                try
                {
                    listenerModel.UdpReceiver = new UdpClient(iPort);
                }
                catch (SocketException ex)
                {
                    if (form.InvokeRequired)
                    {
                        form.Invoke(new Action(() =>
                        {
                            MessageBox.Show("Endpoint already in use.");
                            listenerModel.ExceptionListBox.Items.Add(ex.Message);
                        }));
                    }
                    return;
                }

                IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);

                if (form.InvokeRequired)
                {
                    form.Invoke(new Action(() =>
                    {
                        MessageBox.Show($"Listening on {remoteEndPoint.Address}:{iPort}");
                        listenerModel.ListenBtn.ForeColor = Color.Red;
                        listenerModel.ListenBtn.Text = "Stop Listening";
                        ControlsValidation.DisableIfListening(listenerModel.ReceiverPortTxtBx);
                    }));
                }
                string sMessage = string.Empty;
                while (listenerModel.UdpReceiver != null)
                {
                    try
                    {
                        if (listenerModel.UdpReceiver != null)
                        {
                            byte[] recBytes = listenerModel.UdpReceiver.Receive(ref remoteEndPoint);
                            sMessage = Encoding.ASCII.GetString(recBytes);
                            // InvokeRequired checks if the calling thread is different from the thread that created the control. 
                            if (form.InvokeRequired)
                            {
                                form.Invoke(new Action(() =>
                                {
                                    listenerModel.ReceiveMsgLstBx.Items.Add(sMessage);
                                }));
                            }
                            else
                            {
                                listenerModel.ReceiveMsgLstBx.Items.Add(sMessage);

                                // System.InvalidOperationException: 'Cross-thread operation not valid: Control '_recMsgLstBx' accessed from a thread other than the thread it was created on.'
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    catch (Exception ex)
                    {
                        listenerModel.ExceptionListBox.Items.Add(ex.Message);
                        // A blocking operation was interrupted by a call to WSACancelBlockingCall :
                        // possible reason might be when the receiver is still stuck on Receive() function which is a blocking operation, and then the receiver is either Closed, Disposed off or pointing to a null reference.
                        //MessageBox.Show(ex.Message);
                    }
                    Thread.Sleep(1);
                }
            }
            catch (Exception ex)
            {
                listenerModel.ExceptionListBox.Items.Add(ex.Message);
                // System.Net.Sockets.SocketException: 'Only one usage of each socket address (protocol/network address/port) is normally permitted'
                // this occurs when we try to listen on a port thats already occupied by another Udp listner
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sReceiverIpAddress">To be inserted into the textbox.</param>
        /// <param name="iReceiverPort">To be inserted into the textbox.</param>
        public static void ReceiveMessage(string sReceiverIpAddress, int iReceiverPort)
        {
            try
            {
                MessageBox.Show($"Listening on: IP = {sReceiverIpAddress}, Port = {iReceiverPort}");

                UdpClient receiver = new UdpClient(iReceiverPort);
                IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);
                string message = string.Empty;
                do
                {
                    byte[] recBytes = receiver.Receive(ref remoteEndPoint);
                    message = Encoding.ASCII.GetString(recBytes);
                    recBytes = null;
                    Console.WriteLine(">> " + message);
                }
                while (message.ToUpper() != "CLOSE");

                message = null;
                Console.WriteLine("Closing Udp client...");
                receiver.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{nameof(ex.Message)} = {ex.Message} | {nameof(ex.StackTrace)} = {ex.StackTrace}");
            }
        }


    }
}
