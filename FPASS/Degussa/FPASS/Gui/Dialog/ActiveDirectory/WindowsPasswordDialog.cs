using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Degussa.FPASS.Gui.Dialog.ActiveDirectory
{
    public partial class WindowsPasswordDialog : Form
    {
        private string mPassword;

        internal string Password { get { return mPassword; } }
        internal bool OKPressed { get; set; }

        public WindowsPasswordDialog()
        {
            InitializeComponent();
            this.AcceptButton = btn_OK;
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            mPassword = tb_Password.Text;
            OKPressed = true;
            this.Close();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            mPassword = "";
            OKPressed = false;
            this.Close();
        }
    }
}
