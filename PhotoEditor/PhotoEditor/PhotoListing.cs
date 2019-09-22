using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace PhotoEditor
{
	public class PhotoListing
	{
		public DirectoryInfo PathDirectory { get; }
		public PhotoListing()
		{
			var k = Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures));
			var homeDir = new DirectoryInfo(k + "\\Pictures");
			PathDirectory = homeDir;
		}

		public TreeNode ListDirectory()
		{
			var p = CreateDirectoryNode(PathDirectory);
			return p;
		}

		private static TreeNode CreateDirectoryNode(DirectoryInfo directoryInfo)
		{
			var directoryNode = new TreeNode(directoryInfo.Name);
			foreach (var directory in directoryInfo.GetDirectories())
				directoryNode.Nodes.Add(CreateDirectoryNode(directory));
			return directoryNode;
		}

		public ColumnHeader[] GetColumnHeader()
		{
			
			ColumnHeader columnHeader1 = new ColumnHeader();
			ColumnHeader columnHeader2 = new ColumnHeader();
			ColumnHeader columnHeader3 = new ColumnHeader();
			columnHeader1.Text = "Name";
			columnHeader1.Width = 250;
			columnHeader2.Text = "Date";
			columnHeader2.Width = 200;
			columnHeader3.Text = "Size(bytes)";
			columnHeader3.Width = 100;
			var returnColumnHeader = new ColumnHeader[]{columnHeader1,columnHeader2,columnHeader3};
			return returnColumnHeader;
		}

		public ListViewItem GetFilesAndImages(FileInfo file,ref ImageList smallImageList,ref ImageList largeImageList,ref int i)
		{
			var holder = new ListViewItem();
			try
			{
				i++;
				byte[] bytes = System.IO.File.ReadAllBytes(file.FullName);
				MemoryStream ms = new MemoryStream(bytes);
				ListViewItem itemPerFile = new ListViewItem(file.Name, i - 1);
				Image img = Image.FromStream(ms); // Use this instead of Image.FromFile()
				smallImageList.Images.Add("i", img);
				largeImageList.Images.Add("i", img);
				itemPerFile.SubItems.Add(file.LastWriteTime.ToString());
				itemPerFile.SubItems.Add(file.Length.ToString());
				holder = itemPerFile;
			}
			catch
			{
				Console.WriteLine("This is not an image file");
			}
			return holder;
		}
	}
}
