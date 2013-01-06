using System;
using System.Threading;
using System.Windows.Forms;

namespace SteamDiskSaver
{
	internal sealed partial class LoadingForm : Form
	{
		internal ProgressBar ProgressBar;
		internal Label CurrentGame;

		#region Windows Form Designer generated code

		private void InitializeComponent()
		{
			this.ProgressBar = new System.Windows.Forms.ProgressBar();
			this.CurrentGame = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// ProgressBar
			// 
			this.ProgressBar.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.ProgressBar.Location = new System.Drawing.Point(0, 32);
			this.ProgressBar.Name = "ProgressBar";
			this.ProgressBar.Size = new System.Drawing.Size(626, 23);
			this.ProgressBar.TabIndex = 0;
			// 
			// CurrentGame
			// 
			this.CurrentGame.Dock = System.Windows.Forms.DockStyle.Fill;
			this.CurrentGame.Location = new System.Drawing.Point(0, 0);
			this.CurrentGame.Name = "CurrentGame";
			this.CurrentGame.Padding = new System.Windows.Forms.Padding(2);
			this.CurrentGame.Size = new System.Drawing.Size(626, 32);
			this.CurrentGame.TabIndex = 1;
			// 
			// LoadingForm
			// 
			this.ClientSize = new System.Drawing.Size(626, 55);
			this.ControlBox = false;
			this.Controls.Add(this.CurrentGame);
			this.Controls.Add(this.ProgressBar);
			this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "LoadingForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Loading games...";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.LoadingForm_FormClosed);
			this.Load += new System.EventHandler(this.LoadingForm_Load);
			this.ResumeLayout(false);

		}

		#endregion
	}
}