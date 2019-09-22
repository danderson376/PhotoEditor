using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhotoEditor
{
    public partial class MainForm : Form
    {
	    private PhotoListing _photoListing;
	    private string _fullPath;
	    public delegate TreeView Add();
	    public delegate TreeView Find();
	    public delegate void AddColumn();
		public delegate ListViewItem AddListItem();
	    public delegate ImageList AddImageList();

	    public delegate View GetView();
		public delegate void Clear();
		public MainForm()
        {
            InitializeComponent();
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            var photoList = new ListView();
            await InitializePhotoList_Async();
            await ListDirectory();
        }

        private async Task InitializePhotoList_Async() //example to get double click to work. Need to change to get photos from disk
        {
	        await Task.Run(() =>
	        {
		        _photoListing=new PhotoListing();
		        _fullPath = _photoListing.PathDirectory.FullName;
	        });
        }

        private async Task GetListView(string PathDirectoryString)
        {
	        var columnsCount = listView1.Columns.Count;
	        var k = Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures));
	        var homeDir = new DirectoryInfo(k + "\\" + PathDirectoryString);
			await Task.Run(() =>
	        {
		        if (columnsCount == 0)
		        {
			        var columns = _photoListing.GetColumnHeader();
			        listView1.Invoke(new AddColumn(() => listView1.Columns.AddRange(columns)));
				}
		        ImageList smallImageList = new ImageList();
		        smallImageList.ImageSize = new Size(64, 64);
		        ImageList largImageList = new ImageList();
		        largImageList.ImageSize = new Size(128, 128);
		        listView1.Invoke(new Clear(() => this.listView1.Items.Clear()));
		        var i = 0;
		        foreach (var file in homeDir.GetFiles("*.jpg"))
		        {
			        var itemPerFile = _photoListing.GetFilesAndImages(file,ref smallImageList,ref largImageList,ref i);
			        listView1.Invoke(new AddListItem(() => this.listView1.Items.Add(itemPerFile)));
				}
		        listView1.Invoke(new AddImageList(() => listView1.SmallImageList = smallImageList));
		        listView1.Invoke(new AddImageList(() => listView1.LargeImageList = largImageList));
		        listView1.Invoke(new GetView(() => this.listView1.View = View.Details));
			});
        }

        public async Task ListDirectory()
		{
			await Task.Run(() =>
			{
				var p = _photoListing.ListDirectory();
				Action showTreeView = () => treeView1.Nodes.Add(p);
				this.Invoke(showTreeView);
				int i = 0;
			});
		}

		private void PhotoList_MouseDoubleClick(object sender, MouseEventArgs e) //found this function at https://stackoverflow.com/questions/12872740/doubleclick-on-a-row-in-listview 
        {                                                                        //Username of Author: XIVSolutions
            ListViewHitTestInfo info = listView1.HitTest(e.X, e.Y);
            ListViewItem item = info.Item;

//            if (item != null)
//            {
//                //MessageBox.Show("The selected Item Name is: " + item.Text);
//                EditPhotoForm editBox = new EditPhotoForm();  //creates new editBox Every time an item is double clicked
//                editBox.ShowDialog(this);
//            }
//            else
//            {
//                this.listView1.SelectedItems.Clear();
//                MessageBox.Show("No Item is selected");
//            }
        }

		private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm aboutBox = new AboutForm();  //creates new editBox Every time an item is double clicked
            aboutBox.ShowDialog(this);
		}

		private void LocateOnDiskToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var k = Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures));
			DirectoryInfo homeDir;
			if (_fullPath.Contains("c:")||_fullPath.Contains("C:"))
			{
				homeDir = new DirectoryInfo(_fullPath);
			}
			else
			{
				homeDir = new DirectoryInfo(k + "\\" + _fullPath);
			}
			Process.Start(homeDir.FullName);
		}

		private void SelectRootFolderToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var t = new System.Windows.Forms.FolderBrowserDialog();
			t.ShowDialog();
		}

		private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private async void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
		{
			_fullPath = treeView1.SelectedNode.FullPath;
			await GetListView(_fullPath);
		}


		private void PhotoList_SelectedIndexChanged(object sender, EventArgs e)
		{

		}

		private void DetailToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.listView1.View = View.Details;
		}

		private void SmallToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.listView1.View = View.SmallIcon;
		}

		private void LargeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.listView1.View = View.LargeIcon;
		}

		//		if (cancellationTokenSource != null)
		//        {
		//	        cancellationTokenSource.Cancel();
		//	        // Wait until the task has been cancelled
		//	        while (cancellationTokenSource != null)
		//	        {
		//		        Application.DoEvents();
		//	        }
		//        }

	}
}
