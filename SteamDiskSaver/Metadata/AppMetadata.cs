using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Json;
using System.Net;
using System.Windows.Forms;

namespace SteamDiskSaver.Metadata
{
	internal sealed class AppMetadata
	{
		internal readonly HashSet<int> Known = new HashSet<int>();
		internal readonly List<Category> Categories = new List<Category>();

		internal static AppMetadata GetMetadata()
		{
			string json;

			try
			{
				var request = WebRequest.Create("https://raw.github.com/zr40/steamdisksaver/data/apps.json");
				using (var response = request.GetResponse())
				using (var stream = response.GetResponseStream())
				using (var reader = new StreamReader(stream))
				{
					json = reader.ReadToEnd();
				}
			}
			catch (WebException e)
			{
				// github down? Let's find out if the internet connection is working
				try
				{
					using (WebRequest.Create("https://www.google.com/").GetResponse())
					{
					}
				}
				catch (WebException)
				{
					// google also failed. Assume not connected
					MessageBox.Show("Metadata download failed with the error '" + e.Message + "'. It appears that you aren't connected to the internet.", "Couldn't download metadata", MessageBoxButtons.OK, MessageBoxIcon.Error);
					Environment.Exit(1);
					return null;
				}

				// github down, but google isn't. Assume github maintenance
				MessageBox.Show("Metadata download failed with the error '" + e.Message + "'. It appears that GitHub is currently unavailable. Please try again later.", "Couldn't download metadata", MessageBoxButtons.OK, MessageBoxIcon.Error);
				Environment.Exit(1);
				return null;
			}
			return new AppMetadata(json);
		}

		private AppMetadata(string json)
		{
			JsonValue data;
			try
			{
				data = JsonValue.Parse(json);
			}
			catch (FormatException e)
			{
				MessageBox.Show("Apparently the metadata contains an error. Sorry about that! Please try again in a minute.\n\nThe error is: " + e.Message, "Error in apps.json", MessageBoxButtons.OK, MessageBoxIcon.Error);
				Environment.Exit(1);
				return;
			}

			if ((int) data["version"] != 1)
			{
				MessageBox.Show("This version of Steam Disk Saver is too old. The metadata uses format " + data["version"] + ", but this version only supports format 1. Please download the latest version.", "Outdated version", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				Environment.Exit(0);
			}

			foreach (var item in data["categories"])
			{
				Categories.Add(new Category(item.Key, item.Value));
			}

			var engines = data["engines"];


			foreach (var item in data["apps"])
			{
				var id = int.Parse(item.Key);
				var app = item.Value;

				Known.Add(id);

				var categoriesInApp = new HashSet<string>();
				foreach (var cat in app)
				{
					categoriesInApp.Add(cat.Key);
				}

				categoriesInApp.Remove("engine");

				foreach (var cat in Categories)
				{
					categoriesInApp.Remove(cat.Key);
					cat.AddApp(id, app, engines);
				}

				foreach (var cat in categoriesInApp)
				{
					Debug.WriteLine("Unknown category {0} in {1}", cat, id);
				}
			}
		}
	}
}