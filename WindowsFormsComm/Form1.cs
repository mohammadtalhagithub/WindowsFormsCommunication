using SocketDLL;
using System;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using UdpDLL;

namespace WindowsFormsComm
{
    public partial class SPAForm : Form
    {
        #region Variables
        UdpClient _udpSender;
        UdpClient _udpReceiver;

        Socket _socketClient;
        Socket _socketServer;

        // will be used in case of Reuse address for Udp
        //string _sListenerMulticastIP = ConfigurationManager.AppSettings["ListenerMulticastIP"];

        #endregion

        /// <summary>
        /// The constructor of the Main form of the application. This class should be inhereted from System.Windows.Forms.
        /// </summary>
        public SPAForm()
        {
            InitializeComponent();

            SetDefaultData();
        }

        /// <summary>
        /// Function to set default value of controls or variables if needed.
        /// </summary>
        private void SetDefaultData()
        {
            txtBxSendMessage.Text = "Test Data To Send."; // only for testing
            lstBxException.Visible = false; // hide initially
            chckBxException.Checked = false; // unchecked initially

            rdbUDP.Checked = true;
            //rdbSockets.Checked = true;
            txtBxRemoteIp.Text = "127.0.0.1";

            // following will be uncommented when successfully implemented Reuse Address functionality in Udp 
            /*
            if (rdbUDP.Checked)
            {
                txtBxRemoteIp.Text = "239.0.0.222";
            }
            else if (rdbSockets.Checked)
            {
                txtBxRemoteIp.Text = "127.0.0.1";
            }
             */
        }

        #region Event Handlers

        /// <summary>
        /// When clicked, connects to Udp/Tcp Socket depending upon the radio button checked.
        /// </summary>
        private async void ConnectBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (rdbUDP.Checked) // if Udp is used
                {
                    //check for existing connection
                    if (_udpSender != null)
                    {
                        MessageBox.Show("Already connected."); // show this message if already connected.
                        return;
                    }
                    string sIpAddress = txtBxRemoteIp.Text.Trim(); // ip address from UI
                    int iPort = Convert.ToInt32(txtBxRemotePort.Text.Trim()); // port number from UI

                    _udpSender = await UDPSender.Connect(sIpAddress, iPort);
                    if (_udpSender == null)//check for existing connection
                    {
                        throw new Exception("Connection not established.");
                    }
                    MessageBox.Show($"Connection Established at {sIpAddress}:{iPort}");
                    txtBxRemoteIp.Enabled = false;
                    txtBxRemotePort.Enabled = false;
                }
                else if (rdbSockets.Checked) // is socket is used
                {
                    if (_socketClient != null) // check socket conection
                    {
                        MessageBox.Show("Already connected.");
                        return;
                    }
                    string sServerIP = txtBxRemoteIp.Text; // ip address to send data to.

                    int iServerPort = Convert.ToInt32(txtBxReceiverPort.Text); // port no. to send data to.

                    _socketClient = SocketV2.CreateClient(sServerIP, iServerPort); // after connection, socket client is returned.
                }
            }
            catch (Exception ex)
            {
                lstBxException.Items.Add(ex.Message);
                MessageBox.Show(ex.Message);
            }
        }


        /// <summary>
        /// If the sender client is connected to a remote endpoint, this event will close the connection and releases all resources held by any managed objects that this UdpClient references.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void DisconnectBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (rdbUDP.Checked)
                {
                    if (_udpSender == null)
                    {
                        MessageBox.Show("Already Disconnected.");
                        return;
                    }

                    // Close() is used to disable the underlying socket and release all managed and unmanaged resources associated with the UdpClient
                    // Close() method is typically used when you want to close the connection but still keep the UdpClient object alive.
                    _udpSender.Close();

                    // Dispose() is used to release all managed and unmanaged resources used by the UdpClient.
                    // This method is typically used when you want to completely dispose of the UdpClient object and release all its resources.
                    // This means that the object is no longer usable, but it still occupies memory until it is garbage collected.
                    _udpSender.Dispose();
                    _udpSender = null;

                    MessageBox.Show("Sender closed Successfully.");
                    txtBxRemoteIp.Enabled = true;
                    txtBxRemotePort.Enabled = true;
                }
                else if (rdbSockets.Checked)
                {
                    if (_socketClient != null)
                    {
                        _socketClient.Close();
                        _socketClient.Dispose();
                        _socketClient = null;
                    }
                    else
                    {
                        MessageBox.Show("Not connected...!");
                    }
                }
            }
            catch (Exception ex)
            {
                lstBxException.Items.Add(ex.Message);
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Click event to send data data to remote end point.
        /// </summary>
        private async void SendBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (rdbUDP.Checked) // check connection type
                {
                    if (_udpSender == null)
                    {
                        MessageBox.Show("Not connected."); // if not connected, show message.
                        return;
                    }
                    string sMessage = txtBxSendMessage.Text; // mesage to be sent


                    // asyncronously send 
                    Task.Run(() =>
                    {
                        UDPSender.SendMessage(_udpSender, sMessage);
                    });
                }
                else if (rdbSockets.Checked)
                {
                    SocketV2.SendToServer(txtBxSendMessage.Text, _socketClient, this); // 
                }
            }
            catch (Exception ex)
            {
                lstBxException.Items.Add(ex.Message);
                MessageBox.Show(ex.Message);
            }
        }
        
        /// <summary>
        /// Used for starting and stopping the server for both Udp and socket
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void ListenBtn_Click(object sender, EventArgs e)
        {
            try
            {
                // if UDP is selected
                if (rdbUDP.Checked)
                {
                    if (_udpReceiver == null)
                    {
                        // this is blocking operation, so listen in a separate thread
                        await Task.Run(() =>
                        {
                            // Reuse Address functionality not fully tested, so commenting the following 
                            //int iPort = Convert.ToInt32(txtBxReceiverPort.Text);
                            //UdpMulticast.IPV4(_sListenerMulticastIP, iPort, lstBxMsgReceive, btnListen);

                            // Hence, using this function for single listener for single port
                            StartReceivingUDP();
                        });
                    }
                    else
                    {
                        // to close the udp receiver when button is clicked again
                        _udpReceiver.Close(); // close the udp client
                        _udpReceiver.Dispose();// release the resources
                        _udpReceiver = null;
                        MessageBox.Show("Listener stopped.");
                        btnListen.ForeColor = Color.Black;
                        btnListen.Text = "Start Listening";

                        // to change the states of related controls when listener has stopped.

                        txtBxReceiverPort.Enabled = true;

                    }
                }
                else if (rdbSockets.Checked)
                {
                    if (_socketServer != null)
                    {
                        _socketServer.Close();
                        _socketServer.Dispose();
                        _socketServer = null;

                        MessageBox.Show("Server closed");
                        btnListen.Text = "Start Listening";
                        btnListen.ForeColor = Color.Black;
                        return;
                    }
                    if (_socketClient != null)
                    {
                        _socketClient.Close();
                        _socketClient.Dispose();
                        _socketClient = null;

                        MessageBox.Show("Server closed");
                        btnListen.Text = "Start Listening";
                        btnListen.ForeColor = Color.Black;
                        return;
                    }
                    string sMessage = string.Empty;
                    string sServerIP = IPAddress.Loopback.ToString();
                    int iServerPort = Convert.ToInt32(txtBxReceiverPort.Text);

                    // this expression is for testing multiple client with same server
                    //MultipleClientSocket.CreateListner(iServerPort);

                    // this is for single client with server
                    _socketServer = SocketV2.CreateServer(iServerPort);

                    MessageBox.Show($"Server started on : {sServerIP}:{iServerPort}");
                    await SocketV2.ConnectToClient(sMessage, _socketServer, this, lstBxMsgReceive, btnListen);
                }
            }
            catch (Exception ex)
            {
                lstBxException.Items.Add(ex.Message);
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Hides/ unhides the exception listbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exceptionCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (chckBxException.Checked)
            {
                lstBxException.Visible = true;
            }
            else
            {
                lstBxException.Visible = false;
            }
        }

        #endregion


        #region Private Methods


        /// <summary>
        /// Creates an instance of UdpClient for receiving data and listens for incomming data from UdpClient sender.
        /// <para>This implementation is not suitable to connect multiple Udp listeners with same port on same machine.</para>
        /// </summary>
        private void StartReceivingUDP()
        {
            try
            {
                int iPort = Convert.ToInt32(txtBxReceiverPort.Text);
                try
                {
                    _udpReceiver = new UdpClient(iPort);// create listener at the specif port number
                }
                catch (Exception ex)
                {
                    // when a listner is already listening on a port, another listner is unable to listen on same port
                    if (this.InvokeRequired)
                    {
                        this.Invoke(new Action(() =>
                        {
                            MessageBox.Show("Endpoint already in use.");
                            lstBxException.Items.Add(ex.Message);
                        }));
                    }
                    return;
                }

                // this remoteEndPoint will contain endpoint details of the sender after data has been received from the sender.
                IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);

                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(() =>
                    {
                        // Loopback address is the localhost/ 127.0.0.1
                        MessageBox.Show($"Listening on {IPAddress.Loopback}:{iPort}");
                        btnListen.ForeColor = Color.Red;
                        btnListen.Text = "Stop Listening";
                        ControlsValidation.DisableIfListening(txtBxReceiverPort);
                    }));
                }
                string sMessage = string.Empty;
                while (_udpReceiver != null)
                {
                    try
                    {
                        if (_udpReceiver != null)
                        {
                            byte[] recBytes = _udpReceiver.Receive(ref remoteEndPoint);
                            sMessage = Encoding.ASCII.GetString(recBytes);
                            // InvokeRequired checks if the calling thread is different from the thread that created the control. 
                            if (this.InvokeRequired)
                            {
                                this.Invoke(new Action(() =>
                                {
                                    lstBxMsgReceive.Items.Add(sMessage); // add received data to listbox
                                }));
                            }
                            else
                            {
                                lstBxMsgReceive.Items.Add(sMessage);

                                // System.InvalidOperationException: 'Cross-thread operation not valid: Control '_recMsgLstBx' accessed from a thread other than the thread it was created on.'
                            }
                        }
                        else
                        {
                            break; // exit while loop when udpReceiver is null and stop receiving
                        }
                    }
                    catch (Exception ex)
                    {
                        lstBxException.Items.Add(ex.Message);
                        // A blocking operation was interrupted by a call to WSACancelBlockingCall :
                        // possible reason might be when the receiver is still stuck on Receive() function which is a blocking operation, and then the receiver is either Closed, Disposed off or pointing to a null reference.
                    }
                    Thread.Sleep(1);
                }
            }
            catch (Exception ex)
            {
                lstBxException.Items.Add(ex.Message);
                // System.Net.Sockets.SocketException: 'Only one usage of each socket address (protocol/network address/port) is normally permitted'
                // this occurs when we try to listen on a port thats already occupied by another Udp listner
            }
        }

        private static void EnableControl(Control control)
        {

            control.Enabled = false;
        }



        #endregion


    }
}

