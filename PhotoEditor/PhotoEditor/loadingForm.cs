using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhotoEditor
{
    public partial class LoadingForm : Form
    {
        public LoadingForm()
        {
            InitializeComponent();
            //startButton.Enabled = false;
            progressBar1.Minimum = 0;
            progressBar1.Maximum = 100;
            progressBar1.Visible = true;
            cancelButton.Enabled = true;
           // pauseButton.Enabled = true;
            //startNumTextBox.Enabled = false;
            //endNumTextBox.Enabled = false;
            //pauseButton.Text = "Pause";
        }
        private void CancelButton_Click(object sender, EventArgs e)
        {

        }
    }
}
