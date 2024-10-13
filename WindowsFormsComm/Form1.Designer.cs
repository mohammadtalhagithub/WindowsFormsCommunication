using System.Web.UI.WebControls;

namespace WindowsFormsComm
{
    partial class SPAForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnConnect = new System.Windows.Forms.Button();
            this.txtBxRemoteIp = new System.Windows.Forms.TextBox();
            this.lblRemoteIP = new System.Windows.Forms.Label();
            this.lblRemotePort = new System.Windows.Forms.Label();
            this.txtBxRemotePort = new System.Windows.Forms.TextBox();
            this.txtBxSendMessage = new System.Windows.Forms.TextBox();
            this.lblTextMsg = new System.Windows.Forms.Label();
            this.btnSend = new System.Windows.Forms.Button();
            this.grpBxSender = new System.Windows.Forms.GroupBox();
            this.chckBxException = new System.Windows.Forms.CheckBox();
            this.lstBxException = new System.Windows.Forms.ListBox();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.grpBxReceiver = new System.Windows.Forms.GroupBox();
            this.btnListen = new System.Windows.Forms.Button();
            this.lstBxMsgReceive = new System.Windows.Forms.ListBox();
            this.txtBxReceiverPort = new System.Windows.Forms.TextBox();
            this.lblRecieverPort = new System.Windows.Forms.Label();
            this.rdbSockets = new System.Windows.Forms.RadioButton();
            this.rdbUDP = new System.Windows.Forms.RadioButton();
            this.grpBxSender.SuspendLayout();
            this.grpBxReceiver.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(213, 22);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(106, 44);
            this.btnConnect.TabIndex = 0;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.ConnectBtn_Click);
            // 
            // txtBxRemoteIp
            // 
            this.txtBxRemoteIp.Location = new System.Drawing.Point(107, 24);
            this.txtBxRemoteIp.Name = "txtBxRemoteIp";
            this.txtBxRemoteIp.Size = new System.Drawing.Size(100, 20);
            this.txtBxRemoteIp.TabIndex = 1;
            // 
            // lblRemoteIP
            // 
            this.lblRemoteIP.AutoSize = true;
            this.lblRemoteIP.Location = new System.Drawing.Point(6, 27);
            this.lblRemoteIP.Name = "lblRemoteIP";
            this.lblRemoteIP.Size = new System.Drawing.Size(98, 13);
            this.lblRemoteIP.TabIndex = 2;
            this.lblRemoteIP.Text = "Remote IP Address";
            // 
            // lblRemotePort
            // 
            this.lblRemotePort.AutoSize = true;
            this.lblRemotePort.Location = new System.Drawing.Point(6, 53);
            this.lblRemotePort.Name = "lblRemotePort";
            this.lblRemotePort.Size = new System.Drawing.Size(66, 13);
            this.lblRemotePort.TabIndex = 2;
            this.lblRemotePort.Text = "Remote Port";
            // 
            // txtBxRemotePort
            // 
            this.txtBxRemotePort.Location = new System.Drawing.Point(107, 46);
            this.txtBxRemotePort.Name = "txtBxRemotePort";
            this.txtBxRemotePort.Size = new System.Drawing.Size(100, 20);
            this.txtBxRemotePort.TabIndex = 1;
            this.txtBxRemotePort.Text = "11000";
            // 
            // txtBxSendMessage
            // 
            this.txtBxSendMessage.Location = new System.Drawing.Point(15, 116);
            this.txtBxSendMessage.Name = "txtBxSendMessage";
            this.txtBxSendMessage.Size = new System.Drawing.Size(304, 20);
            this.txtBxSendMessage.TabIndex = 3;
            // 
            // lblTextMsg
            // 
            this.lblTextMsg.AutoSize = true;
            this.lblTextMsg.Location = new System.Drawing.Point(12, 91);
            this.lblTextMsg.Name = "lblTextMsg";
            this.lblTextMsg.Size = new System.Drawing.Size(77, 13);
            this.lblTextMsg.TabIndex = 4;
            this.lblTextMsg.Text = "Type Message";
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(324, 111);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(110, 30);
            this.btnSend.TabIndex = 5;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.SendBtn_Click);
            // 
            // grpBxSender
            // 
            this.grpBxSender.Controls.Add(this.chckBxException);
            this.grpBxSender.Controls.Add(this.lstBxException);
            this.grpBxSender.Controls.Add(this.btnDisconnect);
            this.grpBxSender.Controls.Add(this.btnConnect);
            this.grpBxSender.Controls.Add(this.btnSend);
            this.grpBxSender.Controls.Add(this.txtBxRemoteIp);
            this.grpBxSender.Controls.Add(this.lblTextMsg);
            this.grpBxSender.Controls.Add(this.txtBxRemotePort);
            this.grpBxSender.Controls.Add(this.txtBxSendMessage);
            this.grpBxSender.Controls.Add(this.lblRemoteIP);
            this.grpBxSender.Controls.Add(this.lblRemotePort);
            this.grpBxSender.Location = new System.Drawing.Point(12, 63);
            this.grpBxSender.Name = "grpBxSender";
            this.grpBxSender.Size = new System.Drawing.Size(440, 430);
            this.grpBxSender.TabIndex = 6;
            this.grpBxSender.TabStop = false;
            this.grpBxSender.Text = "Sender";
            // 
            // chckBxException
            // 
            this.chckBxException.AutoSize = true;
            this.chckBxException.Location = new System.Drawing.Point(9, 175);
            this.chckBxException.Name = "chckBxException";
            this.chckBxException.Size = new System.Drawing.Size(108, 17);
            this.chckBxException.TabIndex = 8;
            this.chckBxException.Text = "Show Exceptions";
            this.chckBxException.UseVisualStyleBackColor = true;
            this.chckBxException.CheckedChanged += new System.EventHandler(this.exceptionCheckBox_CheckedChanged);
            // 
            // lstBxException
            // 
            this.lstBxException.FormattingEnabled = true;
            this.lstBxException.Location = new System.Drawing.Point(9, 199);
            this.lstBxException.MultiColumn = true;
            this.lstBxException.Name = "lstBxException";
            this.lstBxException.Size = new System.Drawing.Size(421, 199);
            this.lstBxException.TabIndex = 7;
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Location = new System.Drawing.Point(324, 22);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(106, 44);
            this.btnDisconnect.TabIndex = 6;
            this.btnDisconnect.Text = "Disconnect";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new System.EventHandler(this.DisconnectBtn_Click);
            // 
            // grpBxReceiver
            // 
            this.grpBxReceiver.Controls.Add(this.btnListen);
            this.grpBxReceiver.Controls.Add(this.lstBxMsgReceive);
            this.grpBxReceiver.Controls.Add(this.txtBxReceiverPort);
            this.grpBxReceiver.Controls.Add(this.lblRecieverPort);
            this.grpBxReceiver.Location = new System.Drawing.Point(467, 63);
            this.grpBxReceiver.Name = "grpBxReceiver";
            this.grpBxReceiver.Size = new System.Drawing.Size(440, 430);
            this.grpBxReceiver.TabIndex = 7;
            this.grpBxReceiver.TabStop = false;
            this.grpBxReceiver.Text = "Receiver";
            // 
            // btnListen
            // 
            this.btnListen.Location = new System.Drawing.Point(271, 27);
            this.btnListen.Name = "btnListen";
            this.btnListen.Size = new System.Drawing.Size(106, 44);
            this.btnListen.TabIndex = 4;
            this.btnListen.Text = "Start Listening";
            this.btnListen.UseVisualStyleBackColor = true;
            this.btnListen.Click += new System.EventHandler(this.ListenBtn_Click);
            // 
            // lstBxMsgReceive
            // 
            this.lstBxMsgReceive.FormattingEnabled = true;
            this.lstBxMsgReceive.Location = new System.Drawing.Point(24, 108);
            this.lstBxMsgReceive.Name = "lstBxMsgReceive";
            this.lstBxMsgReceive.Size = new System.Drawing.Size(388, 290);
            this.lstBxMsgReceive.TabIndex = 3;
            // 
            // txtBxReceiverPort
            // 
            this.txtBxReceiverPort.Location = new System.Drawing.Point(130, 42);
            this.txtBxReceiverPort.Name = "txtBxReceiverPort";
            this.txtBxReceiverPort.Size = new System.Drawing.Size(100, 20);
            this.txtBxReceiverPort.TabIndex = 1;
            this.txtBxReceiverPort.Text = "11000";
            // 
            // lblRecieverPort
            // 
            this.lblRecieverPort.AutoSize = true;
            this.lblRecieverPort.Location = new System.Drawing.Point(21, 44);
            this.lblRecieverPort.Name = "lblRecieverPort";
            this.lblRecieverPort.Size = new System.Drawing.Size(72, 13);
            this.lblRecieverPort.TabIndex = 2;
            this.lblRecieverPort.Text = "Receiver Port";
            // 
            // rdbSockets
            // 
            this.rdbSockets.AutoSize = true;
            this.rdbSockets.Location = new System.Drawing.Point(163, 25);
            this.rdbSockets.Name = "rdbSockets";
            this.rdbSockets.Size = new System.Drawing.Size(86, 17);
            this.rdbSockets.TabIndex = 10;
            this.rdbSockets.TabStop = true;
            this.rdbSockets.Text = "Use Sockets";
            this.rdbSockets.UseVisualStyleBackColor = true;
            // 
            // rdbUDP
            // 
            this.rdbUDP.AutoSize = true;
            this.rdbUDP.Location = new System.Drawing.Point(21, 25);
            this.rdbUDP.Name = "rdbUDP";
            this.rdbUDP.Size = new System.Drawing.Size(70, 17);
            this.rdbUDP.TabIndex = 9;
            this.rdbUDP.TabStop = true;
            this.rdbUDP.Text = "Use UDP";
            this.rdbUDP.UseVisualStyleBackColor = true;
            // 
            // SPAForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(932, 516);
            this.Controls.Add(this.rdbSockets);
            this.Controls.Add(this.rdbUDP);
            this.Controls.Add(this.grpBxReceiver);
            this.Controls.Add(this.grpBxSender);
            this.Name = "SPAForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sockets Communication";
            this.grpBxSender.ResumeLayout(false);
            this.grpBxSender.PerformLayout();
            this.grpBxReceiver.ResumeLayout(false);
            this.grpBxReceiver.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.Button btnListen;
        private System.Windows.Forms.CheckBox chckBxException;


        private System.Windows.Forms.GroupBox grpBxSender;
        private System.Windows.Forms.GroupBox grpBxReceiver;

        private System.Windows.Forms.Label lblRemoteIP;
        private System.Windows.Forms.Label lblRemotePort;
        private System.Windows.Forms.Label lblTextMsg;
        private System.Windows.Forms.Label lblRecieverPort;

        private System.Windows.Forms.ListBox lstBxMsgReceive;
        private System.Windows.Forms.ListBox lstBxException;

        private System.Windows.Forms.RadioButton rdbSockets;
        private System.Windows.Forms.RadioButton rdbUDP;
        /// <summary>
        /// 224.0.0.0 to 239.255.255.255
        /// </summary>
        private System.Windows.Forms.TextBox txtBxRemoteIp;
        private System.Windows.Forms.TextBox txtBxRemotePort;
        private System.Windows.Forms.TextBox txtBxSendMessage;
        private System.Windows.Forms.TextBox txtBxReceiverPort;
    }
}


