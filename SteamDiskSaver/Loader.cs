using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

using Microsoft.Win32;

namespace SteamDiskSaver
{
	internal sealed class Loader
	{
		internal Action<List<App>> Done;
		internal Action<int> Maximum;
		internal Action<int> Progress;
		internal Action<string> Status;

		private int progress;

		internal void Load()
		{
			var steamPath = (string) Registry.GetValue(@"HKEY_CURRENT_USER\Software\Valve\Steam", "SteamPath", null);
			if (steamPath == null)
			{
				MessageBox.Show("Could not find Steam installation path in the Registry.", "Steam not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
				Environment.Exit(1);
			}
			var path = Path.Combine(steamPath, "steamapps");

			try
			{
				var files = Directory.GetFiles(path, "*.acf");
				Maximum(files.Length + 1);
				Status("Downloading definition...");

				var metadata = AppMetadata.GetMetadata();

				Done(files.Select(f =>
				                    {
					                    AppManifestItem appManifestItem;
					                    using (var s = File.OpenRead(f))
						                    appManifestItem = AppManifestParser.Parse(s)["AppState"];

					                    Progress(++progress);
					                    if (appManifestItem["UserConfig"].Items.ContainsKey("name"))
						                    Status(appManifestItem["UserConfig"]["name"]);
					                    else
						                    Status(appManifestItem["installdir"].Value.Split('\\').Last());
					                    Application.DoEvents();

					                    try
					                    {
						                    return new App(metadata, appManifestItem, path);
					                    }
					                    catch (IgnoreAppException)
					                    {
						                    return (App) null;
					                    }
				                    }).Where(n => n != null).ToList());
			}
			catch (DirectoryNotFoundException)
			{
				MessageBox.Show(string.Format("'steamapps' directory wasn't found in '{0}'. Please reinstall Steam", steamPath), "steamapps directory not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
				Environment.Exit(1);

				return;
			}
		}
	}
}