using System;
using System.Text;
using System.Windows.Forms;

using SteamDiskSaver.Metadata;

namespace SteamDiskSaver
{
	internal sealed partial class CategoryControl : UserControl
	{
		public CategoryControl()
		{
			InitializeComponent();
		}

		internal Category Category;

		private void CategoryControl_Load(object sender, EventArgs e)
		{
			NameLabel.Text = char.ToUpper(Category.Name[0]) + Category.Name.Substring(1);

			var sb = new StringBuilder();
			sb.Append(Category.Description);

			if (Category.Benefit != null)
			{
				sb.Append("\nDeleting these will also ");
				sb.Append(Category.Benefit);
			}

			if (Category.KeepIf != null)
			{
				sb.Append("\nDo not delete these if ");
				sb.Append(Category.KeepIf);
			}

			DescriptionLabel.Text = sb.ToString();

			checkBox1.Checked = Category.Selected;
		}

		internal void Save()
		{
			Category.Selected = checkBox1.Checked;
			Category.SavePreference();
		}
	}
}