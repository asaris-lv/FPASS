using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Degussa.FPASS.Gui
{
    public partial class FPASSMessageBox : Form
    {

        public FPASSMessageBox(string pMessage)
        {
            InitializeComponent();
            string message = pMessage;

            message = pMessage.Replace("\n", Environment.NewLine);

            txtMessage.Text = message;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnClipboard_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txtMessage.Text);
        }
    }
}
