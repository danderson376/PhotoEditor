﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhotoEditor
{
    public partial class EditPhotoForm : Form
    {
        private Image myImage;
        Bitmap transformedBitmap;
        private string fullPathOfImage;
        private string imageName;

        public delegate void DoLoading();
        private CancellationTokenSource cancellationTokenSource;


        public EditPhotoForm(Image img, string fullPath, string nameOfImage)
        {
            InitializeComponent();
            myImage = img;
            fullPathOfImage = fullPath;
            imageName = nameOfImage;
            //brightnessBar = new TrackBar();
        }
        private void EditPhotoForm_Load(object sender, EventArgs e)
        {
            // myImage = Image.FromFile("C:\\Users\\dalac\\OneDrive\\Pictures\\spidermanlogo.jpg");
            transformedBitmap = new Bitmap(myImage);
            LoadImage(myImage);
            brightnessTrackBar.Maximum = 100;
            brightnessTrackBar.Minimum = 0;
            brightnessTrackBar.Value = 50;
        }

        private void LoadImage(Image img)
        {
            pictureBox.Image = img;
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            myImage = (Image)transformedBitmap;
            myImage.Save(fullPathOfImage + "\\" + "Transformed" + DateTime.Now.Ticks + imageName, ImageFormat.Jpeg);
            Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            //add code to reverse all changes
            Close();
        }
        private async void ColorButton_Click(object sender, EventArgs e)
        {
            ColorDialog colorBox = new ColorDialog();
            if (colorBox.ShowDialog() == DialogResult.OK)
            {
                await ChangeColors(colorBox.Color);
            }
            Image tintedImage = (Image)transformedBitmap;
            LoadImage(tintedImage);
        }
        private async void InvertButton_Click(object sender, EventArgs e) //need to make async, progress bar, 
        {
            await InvertColors();
            Image invertedImage = (Image)transformedBitmap;
           

            if (cancellationTokenSource.IsCancellationRequested)
            {
                LoadImage(myImage);
            }
            else
            {
                LoadImage(invertedImage);
            }
        }

        private async Task InvertColors()
        {
            UseWaitCursor = true;

            cancellationTokenSource = new CancellationTokenSource();
            var token = cancellationTokenSource.Token;
            LoadingForm loadingForm = new LoadingForm();
            
			await Task.Run(async () =>
            {
	            loadingForm.Show();
				int transformedMaxValue = transformedBitmap.Height;
	            for (int y = 0; y < transformedBitmap.Height; y++)
                {
                    for (int x = 0; x < transformedBitmap.Width; x++)
                    {
                        try
                        {
                            Color color = transformedBitmap.GetPixel(x, y);
                            int newRed = Math.Abs(color.R - 255);
                            int newGreen = Math.Abs(color.G - 255);
                            int newBlue = Math.Abs(color.B - 255);
                            Color newColor = Color.FromArgb(newRed, newGreen, newBlue);
                            transformedBitmap.SetPixel(x, y, newColor);
	                        loadingForm.Addprogress(y, transformedMaxValue);
	                        loadingForm.Update();
                        }
                        catch
                        {
                            Console.WriteLine("Could not Invert Colors");
                        }

                        if (loadingForm.CancellationTokenSource!=null)
                            break;
                    }
                    if (loadingForm.CancellationTokenSource != null)
                        break;

                }
				loadingForm.Dispose();
			});

            UseWaitCursor = false;
            //Close();
        }

        private async Task ChangeColors(Color tintColor)
        {
            UseWaitCursor = true;

            await Task.Run(() =>
            {
                for (int y = 0; y < transformedBitmap.Height; y++)
                {
                    for (int x = 0; x < transformedBitmap.Width; x++)
                    {
                        try
                        {
                            Color color = transformedBitmap.GetPixel(x, y);

                            int newRed = Math.Abs((color.R + (1 - color.R / 255) * tintColor.R)); //found this formula at https://stackoverflow.com/questions/4699762/how-do-i-recolor-an-image-see-images                                                                       
                            int newGreen = Math.Abs((color.G + (1 - color.G / 255) * tintColor.G)); //Author Usernames: CodesInChaos and Aliostad 
                            int newBlue = Math.Abs((color.B + (1 - color.B / 255) * tintColor.B));

                            if (newRed > 255)
                            {
                                newRed = 255;
                            }

                            if (newGreen > 255)
                            {
                                newGreen = 255;
                            }

                            if (newBlue > 255)
                            {
                                newBlue = 255;
                            }

                            Color newColor = Color.FromArgb(newRed, newGreen, newBlue);
                            transformedBitmap.SetPixel(x, y, newColor);
                        }
                        catch
                        {
                            Console.WriteLine("Could not Tint Image");
                        }
                    }
                }
            });


            UseWaitCursor = false;
        }

        private async void BrightnessTrackBar_MouseUp(object sender, MouseEventArgs e)
        {
            
            await changeBrightness();
            Image invertedImage = (Image)transformedBitmap;
            if (cancellationTokenSource.IsCancellationRequested)
            {
                LoadImage(myImage);
            }
            else
            {
                LoadImage(invertedImage);
            }
        }

        private async Task changeBrightness()
        {
            int amount = Convert.ToInt32(2 * (50 - brightnessTrackBar.Value) * 0.01 * 255);
            MessageBox.Show(amount.ToString());
            UseWaitCursor = true;

            cancellationTokenSource = new CancellationTokenSource();
            var token = cancellationTokenSource.Token;

            await Task.Run(() =>
            {
                for (int y = 0; y < transformedBitmap.Height; y++)
                {
                    for (int x = 0; x < transformedBitmap.Width; x++)
                    {
                        try
                        {
                            Color color = transformedBitmap.GetPixel(x, y);

                            int newRed = (color.R - amount); //found this formula at https://stackoverflow.com/questions/4699762/how-do-i-recolor-an-image-see-images                                                                       
                            int newGreen = (color.G - amount); //Author Usernames: CodesInChaos and Aliostad 
                            int newBlue = (color.B - amount);

                            if (newRed > 255)
                            {
                                newRed = 255;
                            }
                            else if (newRed < 0)
                            {
                                newRed = 0;
                            }

                            if (newGreen > 255)
                            {
                                newGreen = 255;
                            }
                            else if (newGreen < 0)
                            {
                                newGreen = 0;
                            }

                            if (newBlue > 255)
                            {
                                newBlue = 255;
                            }
                            else if (newBlue < 0)
                            {
                                newBlue = 0;
                            }

                            Color newColor = Color.FromArgb(newRed, newGreen, newBlue);
                            transformedBitmap.SetPixel(x, y, newColor);
                        }
                        catch
                        {
                            Console.WriteLine("Could not Tint Image");
                        }
                    }
                }
            });
            UseWaitCursor = false;
        }
    }
    }