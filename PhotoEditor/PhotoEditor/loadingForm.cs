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
        private CancellationTokenSource cancellationTokenSource;
        private Image myImage;
        public Bitmap invertedBitmap;
        public LoadingForm(Image img)
        {
            InitializeComponent();
            invertedBitmap = new Bitmap(img);
            myImage = img;
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
            invertedBitmap = new Bitmap(myImage);
            this.DialogResult = DialogResult.Cancel;
            Close();
        }

        private async void LoadingForm_Load(object sender, EventArgs e)
        {
            await InvertColors();

        }

        private async Task InvertColors()
        {
            UseWaitCursor = true;

            cancellationTokenSource = new CancellationTokenSource();
            var token = cancellationTokenSource.Token;

            //LoadingForm loadingForm = new LoadingForm();

            await Task.Run(() =>
            {
                for (int y = 0; y < invertedBitmap.Height; y++)
                {
                    for (int x = 0; x < invertedBitmap.Width; x++)
                    {
                        try
                        {
                            Color color = invertedBitmap.GetPixel(x, y);
                            int newRed = Math.Abs(color.R - 255);
                            int newGreen = Math.Abs(color.G - 255);
                            int newBlue = Math.Abs(color.B - 255);
                            Color newColor = Color.FromArgb(newRed, newGreen, newBlue);
                            invertedBitmap.SetPixel(x, y, newColor);
                        }
                        catch
                        {
                            Console.WriteLine("Could not Invert Colors");
                        }

                        if (token.IsCancellationRequested)
                            break;
                    }
                    if (token.IsCancellationRequested)
                        break;

                }
            });
            UseWaitCursor = false;
            Close();
        }
    }
}
