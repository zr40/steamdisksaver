using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using SteamDiskSaver.Metadata;

namespace SteamDiskSaver
{
	public partial class CategoriesForm : Form
	{
		public CategoriesForm()
		{
			InitializeComponent();
			Height += flowLayoutPanel1.PreferredSize.Height - flowLayoutPanel1.Height;
		}

		internal AppMetadata Metadata;
		private List<CategoryControl> CategoryControls = new List<CategoryControl>();

		private void CategoriesForm_Load(object sender, System.EventArgs e)
		{
			foreach (var category in Metadata.Categories)
			{
				var control = new CategoryControl();
				control.Category = category;
				flowLayoutPanel1.Controls.Add(control);
				flowLayoutPanel1.SetFlowBreak(control, true);
				CategoryControls.Add(control);

				// work around the flow layout panel flow break spacing bug
				var workaround = new Control();
				workaround.Size = new Size();
				workaround.Margin = new Padding();
				flowLayoutPanel1.Controls.Add(workaround);
			}

			flowLayoutPanel1.PerformLayout();
			

			Left = Owner.Left + Owner.Width / 2 - Width / 2;
			Top = Owner.Top + Owner.Height / 2 - Height / 2;
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			foreach (var control in CategoryControls)
			{
				control.Save();
			}
			new LoadingForm().Show();
		}
	}
}