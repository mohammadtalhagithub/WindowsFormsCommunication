using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UdpDLL
{
    /// <summary>
    /// This class handles state change of form controls on specific conditions.
    /// </summary>
    public class ControlsValidation
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="receiverPortTextBox"></param>
        public static void DisableIfListening(TextBox receiverPortTextBox)
        {
            receiverPortTextBox.Enabled = false;
        }


    }
}

