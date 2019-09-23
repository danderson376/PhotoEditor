using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhotoEditor
{
    public partial class LoadingForm : Form
    {
        //private Image myImage;
        //public Bitmap invertedBitmap;
        public delegate void DoWorkLoad();
        public delegate void DoWorkLoad2();

        public CancellationTokenSource CancellationTokenSource;
		public LoadingForm()
        {
            InitializeComponent();
            progressBar1.Minimum = 0;
            progressBar1.Maximum = 100;
            progressBar1.Visible = true;
            cancelButton.Enabled = true;
            this.DialogResult = DialogResult.OK;
        }
        private async void CancelButton_Click(object sender, EventArgs e)
        {
	        CancellationTokenSource =new CancellationTokenSource();
        }

        public void LoadingForm_Load(object sender, EventArgs e)
        {
	        
        }

        public async Task Addprogress(int i, int maximum)
        {
	        progressBar1.Maximum = maximum;
	        progressBar1.Value = i;
//	        Invoke(new DoWorkLoad(() => progressBar1.Maximum = maximum));
//	        Invoke(new DoWorkLoad2(() => progressBar1.Value = i));
        }
		//        await Task.Run(() =>
		//
		//        {
		//	        Invoke(new DoWorkLoad(() => progressBar1.Maximum = maximum));
		//	        Invoke(new DoWorkLoad(() => progressBar1.Value = i));
		//        });
	}
}
