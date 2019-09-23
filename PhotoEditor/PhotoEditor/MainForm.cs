using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace PhotoEditor
{
    public partial class MainForm : Form
    {
        private CancellationTokenSource cancellationTokenSource { get; set; }
        private int lengthOfFiles { get; set; }
        private PhotoListing _photoListing { get; set; }
        private string _fullPath { get; set; }
        private View mainView { get; set; }
        public delegate void HideProgress();
        public delegate void SetTotalAmount();
        public delegate void AddToProgressBar();
        public delegate void AddColumn();
        public delegate ListViewItem AddListItem();
        public delegate ImageList AddImageList();
        public delegate View GetView();
        public delegate void Clear();

        public MainForm()
        {
            InitializeComponent();
            listView1.MouseDoubleClick += new MouseEventHandler(listView1_MouseDoubleClick);
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            await InitializePhotoList_Async();
            await ListDirectory();
            await GetListView(_fullPath);
        }

        private async Task InitializePhotoList_Async() //example to get double click to work. Need to change to get photos from disk
        {
            await Task.Run(() =>
            {
                _photoListing = new PhotoListing();
                _fullPath = _photoListing.PathDirectory.FullName;
            });
        }

        private DirectoryInfo getDirectoryInfo(string PathDirectoryString)
        {
	        var systemDirectoryInfo = Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures));
	        var homeDir = (_fullPath.Contains("c:") || _fullPath.Contains("C:"))
		        ? new DirectoryInfo(PathDirectoryString)
		        : new DirectoryInfo(systemDirectoryInfo + "\\" + PathDirectoryString);
	        return homeDir;
        }

        private async Task GetListView(string PathDirectoryString)
        {
            var columnsCount = listView1.Columns.Count;
            cancellationTokenSource = new CancellationTokenSource();
            var homeDir = getDirectoryInfo(PathDirectoryString);

            await Task.Run(() =>
            {
	            ImageList smallImageList = new ImageList { ImageSize = new Size(64, 64) };
	            ImageList largImageList = new ImageList { ImageSize = new Size(128, 128) };
	            lengthOfFiles = homeDir.GetFiles("*.jpg").GetLength(0);
	            var i = 0;

				if (columnsCount == 0)
                {
                    var columns = _photoListing.GetColumnHeader();
                    listView1.Invoke(new AddColumn(() => listView1.Columns.AddRange(columns)));
                }

				listView1.Invoke(new Clear(() => this.listView1.Items.Clear()));
				this.Invoke(new SetTotalAmount(() => this.progressBar1.Value = 0));
                this.Invoke(new SetTotalAmount(() => this.progressBar1.Maximum = lengthOfFiles));
                progressBar1.Invoke(new HideProgress(() => this.progressBar1.Visible = true));

                foreach (var file in homeDir.GetFiles("*.jpg"))
                {
                    if (cancellationTokenSource.IsCancellationRequested != true)
                    {
                        var itemPerFile = _photoListing.GetFilesAndImages(file, ref smallImageList, ref largImageList, ref i);
                        listView1.Invoke(new AddListItem(() => this.listView1.Items.Add(itemPerFile)));
                        Thread.Sleep(50);
                        if (i <= lengthOfFiles && i > 0)
                        {
                            progressBar1.Invoke(new AddToProgressBar(() => this.progressBar1.Value = i));
                        }
                        else
                        {
                            cancellationTokenSource.Cancel();
                        }
                    }
                }
                listView1.Invoke(new AddImageList(() => listView1.SmallImageList = smallImageList));
                listView1.Invoke(new AddImageList(() => listView1.LargeImageList = largImageList));
                listView1.Invoke(new GetView(() => this.listView1.View = mainView));
                progressBar1.Invoke(new HideProgress(() => this.progressBar1.Visible = false));

                if (cancellationTokenSource.IsCancellationRequested == true)
                {
                    GetListView(_fullPath);
                }
            });

        }

        public async Task ListDirectory()
        {
            await Task.Run(() =>
            {
                var treeNodeHolder = _photoListing.ListDirectory();
                mainView = View.Details;
                Action showTreeView = () => treeView1.Nodes.Add(treeNodeHolder);
                Invoke(showTreeView);
            });
        }

        private async void listView1_MouseDoubleClick(object sender, MouseEventArgs e) //found these few lines at https://stackoverflow.com/questions/12872740/doubleclick-on-a-row-in-listview 
        {                                                                        //Username of Author: XIVSolutions
            ListViewHitTestInfo info = listView1.HitTest(e.X, e.Y);
            ListViewItem item = info.Item;
            var homedir = getDirectoryInfo(_fullPath);
            var holderString = homedir.FullName +"\\" +item.Text;
            Image image = Image.FromFile(holderString);

            if (image != null)
            {
                EditPhotoForm editBox = new EditPhotoForm(image, _fullPath, item.Text);  //creates new editBox Every time an item is double clicked
                editBox.ShowDialog(this);
            }
            else
            {
                this.listView1.SelectedItems.Clear();
                MessageBox.Show("No Item is selected");
            }
            await GetListView(_fullPath);
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm aboutBox = new AboutForm();
            aboutBox.ShowDialog(this);
        }

        private void LocateOnDiskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var systemDirectoryInfo = Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures));
            DirectoryInfo homeDir;
            if (_fullPath.Contains("c:") || _fullPath.Contains("C:"))
            {
                homeDir = new DirectoryInfo(_fullPath);
            }
            else
            {
                homeDir = new DirectoryInfo(systemDirectoryInfo + "\\" + _fullPath);
            }
            Process.Start(homeDir.FullName);
        }

        private void SelectRootFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var rootFolder = new System.Windows.Forms.FolderBrowserDialog();
            rootFolder.ShowDialog();
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

        private void DetailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mainView = View.Details;
            this.listView1.View = View.Details;
        }

        private void SmallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mainView = View.SmallIcon;
            this.listView1.View = View.SmallIcon;
        }

        private void LargeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mainView = View.LargeIcon;
            this.listView1.View = View.LargeIcon;
        }
    }
}

