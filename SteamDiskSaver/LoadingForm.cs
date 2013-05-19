using System;
using System.Threading;
using System.Windows.Forms;

namespace SteamDiskSaver
{
	internal sealed partial class LoadingForm : Form
	{
		internal LoadingForm()
		{
			InitializeComponent();
			var loader = new Loader();
			loader.Done = (apps, metadata) => BeginInvoke(new Action(() =>
			                                                         {
				                                                         var f = new MainForm();
				                                                         f.Apps = apps;
				                                                         f.Metadata = metadata;
				                                                         f.Errors = loader.Errors;
																		 f.Show();
				                                                         Close();
			                                                         }));
			loader.Maximum = max => BeginInvoke(new Action(() => { ProgressBar.Maximum = max; }));
			loader.Progress = val => BeginInvoke(new Action(() => { ProgressBar.Value = val; }));
			loader.Status = text => BeginInvoke(new Action(() => { CurrentGame.Text = text; }));
			new Thread(loader.Load).Start();
		}

		private void LoadingForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			Program.Context.FormClosed();
		}

		private void LoadingForm_Load(object sender, EventArgs e)
		{
			Program.Context.FormOpened();
		}
	}
}