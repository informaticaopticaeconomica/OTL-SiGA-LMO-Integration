using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Integration_OTL_SIGA_LMO
{
    public partial class frmLayoutShowMessage : Form
    {

        public string StringMessageFromForm { get; set; }

        public frmLayoutShowMessage()
        {
            InitializeComponent();
        }

        private void frmLayoutShowMessage_Load(object sender, EventArgs e)
        {
            this.lblMessage.Text = StringMessageFromForm;
        }
    }
}
