using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

using Microsoft.Win32;

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

		public MainForm()
		{
			InitializeComponent();
			ReadSteamApps();

			listView1.ListViewItemSorter = itemSorter;

			comparisons[0] = (l, r) => l.Name.CompareTo(r.Name);
			comparisons[1] = (l, r) => l.Id.CompareTo(r.Id);
			comparisons[2] = (l, r) => r.TotalSize.CompareTo(l.TotalSize);
			comparisons[3] = (l, r) => r.RedistSize.CompareTo(l.RedistSize);
			comparisons[4] = (l, r) => r.IntroSize.CompareTo(l.IntroSize);
			comparisons[5] = (l, r) => r.NonEnglishSize.CompareTo(l.NonEnglishSize);
			comparisons[6] = (l, r) => r.OtherSize.CompareTo(l.OtherSize);

			itemSorter.Comparison = comparisons[0];
		}

		private ListViewItem totalsItem;

		private void ReadSteamApps()
		{
			var steamPath = (string) Registry.GetValue(@"HKEY_CURRENT_USER\Software\Valve\Steam", "SteamPath", null);
			if (steamPath == null)
			{
				MessageBox.Show("Could not find Steam installation path in the Registry.", "Steam not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
				Environment.Exit(1);
			}
			var path = Path.Combine(steamPath, "steamapps");

			List<App> apps;

			using (var loading = new LoadingForm())
			{
				try
				{
					// this needs to be refactored into a worker thread
					var files = Directory.GetFiles(path, "*.acf");
					loading.ProgressBar.Maximum = files.Length + 1;
					loading.CurrentGame.Text = "Downloading definition...";
					loading.Show();

					Application.DoEvents();
					AppMetadata.GetDefinition();

					apps = files.Select(f =>
					                    {
						                    AppManifestItem appManifestItem;
						                    using (var s = File.OpenRead(f))
							                    appManifestItem = AppManifestParser.Parse(s)["AppState"];

						                    loading.ProgressBar.Value++;
						                    if (appManifestItem["UserConfig"].Items.ContainsKey("name"))
							                    loading.CurrentGame.Text = appManifestItem["UserConfig"]["name"];
						                    else
							                    loading.CurrentGame.Text = appManifestItem["installdir"].Value.Split('\\').Last();
						                    Application.DoEvents();

						                    try
						                    {
							                    return new App(appManifestItem, path);
						                    }
						                    catch (IgnoreAppException)
						                    {
							                    return (App) null;
						                    }
					                    }).Where(n => n != null).ToList();
				}
				catch (DirectoryNotFoundException)
				{
					MessageBox.Show(string.Format("'steamapps' directory wasn't found in '{0}'. Please reinstall Steam", steamPath), "steamapps directory not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
					Environment.Exit(1);

					return;
				}
			}

			foreach (var item in apps)
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
			return ((size - 1) / 1024 / 1024 / 1024 / 1024 / +1) + " TiB";
		}

		private void ItemSelected(object sender, EventArgs e)
		{
			flowLayoutPanel2.Enabled = listView1.SelectedItems.Count == 1;
			button6.Enabled = listView1.SelectedItems.Count == 1 && listView1.SelectedItems[0].Tag != null;
		}

		private void DeleteRedistsClicked(object sender, EventArgs e)
		{
			var selected = listView1.SelectedItems[0];
			IEnumerable<ListViewItem> items = selected.Tag == null ? listView1.Items.Cast<ListViewItem>().Where(i => i.Tag != null) : new[] {selected};
			foreach (var item in items)
				DeleteRedists(item);
		}

		private void DeleteRedists(ListViewItem listItem)
		{
			var item = (App) listItem.Tag;

			if (!item.Known)
				return;

			foreach (var file in item.Redists)
			{
				file.Delete();
			}

			item.TotalSize -= item.RedistSize;
			item.RedistSize = 0;
			item.Redists.Clear();
			UpdateItem(listItem);
		}

		private void DeleteOthers(ListViewItem listItem)
		{
			var item = (App) listItem.Tag;

			if (!item.Known)
				return;

			foreach (var file in item.Others)
			{
				file.Delete();
			}

			item.TotalSize -= item.OtherSize;
			item.OtherSize = 0;
			item.Others.Clear();
			UpdateItem(listItem);
		}

		private void DeleteNonEnglish(ListViewItem listItem)
		{
			var item = (App) listItem.Tag;

			if (!item.Known)
				return;

			foreach (var file in item.NonEnglish)
			{
				file.Delete();
			}

			item.TotalSize -= item.NonEnglishSize;
			item.NonEnglishSize = 0;
			item.NonEnglish.Clear();
			UpdateItem(listItem);
		}

		private void DeleteIntros(ListViewItem listItem)
		{
			var item = (App) listItem.Tag;

			if (!item.Known)
				return;

			foreach (var file in item.Intros)
			{
				file.Delete();
			}

			item.TotalSize -= item.IntroSize;
			item.IntroSize = 0;
			item.Intros.Clear();
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
				i.SubItems.Add(SizeFormat(apps.Sum(a => a.RedistSize)));
				i.SubItems.Add(SizeFormat(apps.Sum(a => a.IntroSize)));
				i.SubItems.Add(SizeFormat(apps.Sum(a => a.NonEnglishSize)));
				i.SubItems.Add(SizeFormat(apps.Sum(a => a.OtherSize)));
			}
			else
			{
				i.SubItems.Add("Unknown game");
			}
		}

		private void DeleteIntrosClicked(object sender, EventArgs e)
		{
			var selected = listView1.SelectedItems[0];
			IEnumerable<ListViewItem> items = selected.Tag == null ? listView1.Items.Cast<ListViewItem>().Where(i => i.Tag != null) : new[] {selected};
			foreach (var item in items)
				DeleteIntros(item);
		}

		private void DeleteOthersClicked(object sender, EventArgs e)
		{
			var selected = listView1.SelectedItems[0];
			IEnumerable<ListViewItem> items = selected.Tag == null ? listView1.Items.Cast<ListViewItem>().Where(i => i.Tag != null) : new[] {selected};
			foreach (var item in items)
				DeleteOthers(item);
		}

		private void DeleteNonEnglishClicked(object sender, EventArgs e)
		{
			var selected = listView1.SelectedItems[0];
			IEnumerable<ListViewItem> items = selected.Tag == null ? listView1.Items.Cast<ListViewItem>().Where(i => i.Tag != null) : new[] {selected};
			foreach (var item in items)
				DeleteNonEnglish(item);
		}

		private void DeleteAllClicked(object sender, EventArgs e)
		{
			var selected = listView1.SelectedItems[0];
			IEnumerable<ListViewItem> items = selected.Tag == null ? listView1.Items.Cast<ListViewItem>().Where(i => i.Tag != null) : new[] {selected};
			foreach (var item in items)
			{
				DeleteRedists(item);
				DeleteOthers(item);
				DeleteIntros(item);
				DeleteNonEnglish(item);
			}
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
	}
}