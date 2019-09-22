using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhotoEditor
{
    public partial class LoadingForm : Form
    {
        //private Image myImage;
        //public Bitmap invertedBitmap;
        public LoadingForm()
        {
            InitializeComponent();
            //invertedBitmap = new Bitmap(img);
            // myImage = img;
            //startButton.Enabled = false;
            progressBar1.Minimum = 0;
            progressBar1.Maximum = 100;
            progressBar1.Visible = true;
            cancelButton.Enabled = true;
            // pauseButton.Enabled = true;
            //startNumTextBox.Enabled = false;
            //endNumTextBox.Enabled = false;
            //pauseButton.Text = "Pause";
            this.DialogResult = DialogResult.OK;
        }
        private void CancelButton_Click(object sender, EventArgs e)
        {
            //this.invertedBitmap = new Bitmap(myImage);
            //this.DialogResult = DialogResult.Cancel;
            Close();
        }

        private async void LoadingForm_Load(object sender, EventArgs e)
        {
            //await InvertColors();

        }


    }
}
