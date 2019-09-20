using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhotoEditor
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            await InitializePhotoList_Async();
        }

        private async Task InitializePhotoList_Async() //example to get double click to work. Need to change to get photos from disk
        {

            await Task.Run(() =>
            {
                DirectoryInfo homeDir = new DirectoryInfo("C:\\Pictures");
                foreach (FileInfo file in homeDir.GetFiles("*.jpg"))
                {
                    try
                    {
                        byte[] bytes = System.IO.File.ReadAllBytes(file.FullName);
                        MemoryStream ms = new MemoryStream(bytes);
                        Image img = Image.FromStream(ms); // Use this instead of Image.FromFile()
                        Console.WriteLine("Filename: " + file.Name);
                        Console.WriteLine("Last mod: " + file.LastWriteTime.ToString());
                        Console.WriteLine("File size: " + file.Length);

                    }
                    catch
                    {
                        Console.WriteLine("This is not an image file");
                    }
                }
            });
        }

        private void PhotoList_MouseDoubleClick(object sender, MouseEventArgs e) //found this function at https://stackoverflow.com/questions/12872740/doubleclick-on-a-row-in-listview 
        {                                                                        //Username of Author: XIVSolutions
            ListViewHitTestInfo info = PhotoList.HitTest(e.X, e.Y);
            ListViewItem item = info.Item;

            if (item != null)
            {
                //MessageBox.Show("The selected Item Name is: " + item.Text);
                EditPhotoForm editBox = new EditPhotoForm();  //creates new editBox Every time an item is double clicked
                editBox.ShowDialog(this);
            }
            else
            {
                this.PhotoList.SelectedItems.Clear();
                MessageBox.Show("No Item is selected");
            }
        }
    }
}
