using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

using SteamDiskSaver.Apps;
using SteamDiskSaver.Metadata;

namespace SteamDiskSaver
{
	internal sealed partial class MainForm : Form
	{
		private sealed class ListViewSorter : IComparer
		{
			internal Comparison<App> Comparison;

			public int Compare(object x, object y)
			{
				var l = (ListViewItem) x;
				var r = (ListViewItem) y;

				if (l == r)
					return 0;

				if (l.Tag == null)
					return 1;

				if (r.Tag == null)
					return -1;

				return Comparison((App) l.Tag, (App) r.Tag);
			}
		}

		private readonly ListViewSorter itemSorter = new ListViewSorter();
		private readonly Comparison<App>[] comparisons = new Comparison<App>[7];

		private string errors;
		internal List<string> Errors
		{
			set
			{
				if (value.Count != 0)
				{
					errors = string.Join("\n\n", value);
					ErrorsLabel.Visible = true;
					ErrorsButton.Visible = true;
				}
			}
		}

		public MainForm()
		{
			InitializeComponent();
		}

		internal List<App> Apps;

		private ListViewItem totalsItem;
		internal AppMetadata Metadata;

		private void ReadSteamApps()
		{
			foreach (var item in Apps)
			{
				var i = new ListViewItem();
				i.Tag = item;
				UpdateItem(i, false);
				listView1.Items.Add(i);
			}

			totalsItem = new ListViewItem();
			UpdateItem(totalsItem, false);
			listView1.Items.Add(totalsItem);
		}

		private static string SizeFormat(long size)
		{
			// Windows uses binary prefixes, so we do that as well

			if (size < 0)
			{
				return "-" + SizeFormat(-size);
			}

			if (size == 0)
				return "";
			if (size < 10000)
				return size + " B";
			if (size < 10000000)
				return ((size - 1) / 1024 + 1) + " KiB";
			if (size < 10000000000)
				return ((size - 1) / 1024 / 1024 + 1) + " MiB";
			if (size < 10000000000000)
				return ((size - 1) / 1024 / 1024 / 1024 + 1) + " GiB";
			return ((size - 1) / 1024 / 1024 / 1024 / 1024 / + 1) + " TiB";
		}

		private void ItemSelected(object sender, EventArgs e)
		{
			button1.Enabled = listView1.SelectedItems.Count == 1;
			button6.Enabled = listView1.SelectedItems.Count == 1 && listView1.SelectedItems[0].Tag != null;
		}

		private void DeleteFilesClicked(object sender, EventArgs e)
		{
			var selected = listView1.SelectedItems[0];
			IEnumerable<ListViewItem> items = selected.Tag == null ? listView1.Items.Cast<ListViewItem>().Where(i => i.Tag != null) : new[] {selected};
			foreach (var item in items)
				DeleteFiles(item);
		}

		private void DeleteFiles(ListViewItem listItem)
		{
			var item = (App) listItem.Tag;

			if (!item.Known)
				return;

			foreach (var file in item.DeletableFiles)
			{
				file.Delete();
			}

			item.TotalSize -= item.DeletableSize;
			item.DeletableSize = 0;
			item.DeletableFiles.Clear();
			UpdateItem(listItem);
		}

		private void UpdateItem(ListViewItem i, bool updateTotal = true)
		{
			IEnumerable<App> apps;
			if (i == totalsItem)
			{
				apps = listView1.Items.Cast<ListViewItem>().Where(a => a != totalsItem).Select(a => (App) a.Tag).ToList();
			}
			else
			{
				if (updateTotal)
					UpdateItem(totalsItem);

				apps = new[] {(App) i.Tag};
			}
			var first = apps.First();

			i.SubItems.Clear();
			if (i.Tag == null)
			{
				i.Text = "All installed games";
				i.SubItems.Add("");
			}
			else
			{
				i.Text = first.Name;
				i.SubItems.Add(first.Id.ToString());
			}
			i.SubItems.Add(SizeFormat(apps.Sum(a => a.TotalSize)));
			if (first.Known || i.Tag == null)
			{
				i.SubItems.Add(SizeFormat(apps.Sum(a => a.DeletableSize)));
				i.SubItems.Add(SizeFormat(apps.Sum(a => a.NotSelectedSize)));
			}
			else
			{
				i.SubItems.Add("Unknown game");
				i.SubItems.Add("");
			}

			i.SubItems.Add(SizeFormat(apps.Sum((a => a.SavedSize))));
		}

		private void ValidateClicked(object sender, EventArgs e)
		{
			var item = (App) listView1.SelectedItems[0].Tag;

			Process.Start("steam://validate/" + item.Id);
		}

		private void HeaderClicked(object sender, ColumnClickEventArgs e)
		{
			itemSorter.Comparison = comparisons[e.Column];
			listView1.Sort();
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			Program.Context.FormOpened();

			ReadSteamApps();

			comparisons[0] = (l, r) => l.Name.CompareTo(r.Name);
			comparisons[1] = (l, r) => l.Id.CompareTo(r.Id);
			comparisons[2] = (l, r) => r.TotalSize.CompareTo(l.TotalSize);
			comparisons[3] = (l, r) => r.DeletableSize.CompareTo(l.DeletableSize);
			comparisons[4] = (l, r) => r.NotSelectedSize.CompareTo(l.NotSelectedSize);
			comparisons[5] = (l, r) => r.SavedSize.CompareTo(l.SavedSize);

			itemSorter.Comparison = comparisons[0];
			listView1.ListViewItemSorter = itemSorter;
		}

		private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			Program.Context.FormClosed();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			var form = new CategoriesForm();
			form.Metadata = Metadata;
			if (form.ShowDialog(this) == DialogResult.OK)
				Close();
		}

		private void ErrorsButton_Click(object sender, EventArgs e)
		{
			MessageBox.Show(errors, "Errors", MessageBoxButtons.OK);
		}
	}
}