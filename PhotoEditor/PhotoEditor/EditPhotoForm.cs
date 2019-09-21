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
    public partial class EditPhotoForm : Form
    {
        private Image myImage;
        Bitmap transformedBitmap;

        public EditPhotoForm()
        {
            InitializeComponent();
        }
        private void EditPhotoForm_Load(object sender, EventArgs e)
        {
            myImage = Image.FromFile("C:\\Users\\dalac\\OneDrive\\Pictures\\spidermanlogo.jpg");
            transformedBitmap = new Bitmap(myImage);
            LoadImage(myImage);
        }

        private void LoadImage(Image img)
        {
            pictureBox.Image = img;
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            myImage = (Image)transformedBitmap;
           // myImage.Save("myphoto.jpg", ImageFormat.Jpeg); 
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            //add code to reverse all changes
            Close();
        }
        private void ColorButton_Click(object sender, EventArgs e)
        {
            ColorDialog colorBox = new ColorDialog();
            if (colorBox.ShowDialog() == DialogResult.OK)
            { 
               ChangeColors(colorBox.Color);
            }
            Image tintedImage = (Image)transformedBitmap;
            LoadImage(tintedImage);
        }
        private void InvertButton_Click(object sender, EventArgs e) //need to make async, progress bar, 
        {
            InvertColors();
            Image invertedImage = (Image)transformedBitmap;
            LoadImage(invertedImage);
        }

        private void InvertColors()
        {
            for (int y = 0; y < transformedBitmap.Height; y++)
            {
                for (int x = 0; x < transformedBitmap.Width; x++)
                {
                    Color color = transformedBitmap.GetPixel(x, y);
                    int newRed = Math.Abs(color.R - 255);
                    int newGreen = Math.Abs(color.G - 255);
                    int newBlue = Math.Abs(color.B - 255);
                    Color newColor = Color.FromArgb(newRed, newGreen, newBlue);
                    transformedBitmap.SetPixel(x, y, newColor);
                }
            }
        }

        private void ChangeColors(Color tintColor)
        {
            for (int y = 0; y < transformedBitmap.Height; y++)
            {
                for (int x = 0; x < transformedBitmap.Width; x++)
                {
                    Color color = transformedBitmap.GetPixel(x, y);

                    int newRed = Math.Abs((color.R + (1 - color.R/255) * tintColor.R)); //found this formula at https://stackoverflow.com/questions/4699762/how-do-i-recolor-an-image-see-images                                                                       
                    int newGreen = Math.Abs((color.G + (1 - color.G/255) * tintColor.G)); //Author Usernames: CodesInChaos and Aliostad 
                    int newBlue = Math.Abs((color.B + (1 - color.B/255) * tintColor.B));

                    if(newRed > 255)
                    {
                        newRed = 255;
                    }

                    if(newGreen > 255)
                    {
                        newGreen = 255;
                    }

                    if(newBlue > 255)
                    {
                        newBlue = 255;
                    }

                    Color newColor = Color.FromArgb(newRed, newGreen, newBlue);
                    transformedBitmap.SetPixel(x, y, newColor);
                }
            }
        }

    }
}
