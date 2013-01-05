using System;
using System.Collections.Generic;
using System.IO;
using System.Json;
using System.Net;
using System.Windows.Forms;

namespace SteamDiskSaver
{
	internal sealed class PathMatch
	{
		private readonly string pathMatch;
		internal readonly bool EmptyFile;
		private readonly bool matchContains;

		internal PathMatch(JsonValue rule)
		{
			if (rule.JsonType == JsonType.String)
			{
				pathMatch = (string) rule;
			}
			else
			{
				pathMatch = (string) rule[0];

				for (int i = 1; i < rule.Count; i++)
				{
					var item = rule[i];
					var flag = (string) item;
					switch (flag)
					{
						case "contains":
							matchContains = true;
							break;

						case "empty":
							EmptyFile = true;
							break;

						default:
							MessageBox.Show(string.Format("Unknown flag '{0}' for match '{1}'", flag, pathMatch), "Metadata error", MessageBoxButtons.OK, MessageBoxIcon.Error);
							Environment.Exit(1);
							return;
					}
				}
			}
		}

		internal bool Match(string path)
		{
			if (matchContains)
				return path.Contains(pathMatch);

			return (path == pathMatch || (pathMatch.EndsWith("\\") && path.StartsWith(pathMatch)));
		}
	}

	internal sealed class AppMetadata
	{
		internal readonly HashSet<int> Known = new HashSet<int>();

		internal readonly Dictionary<int, List<PathMatch>> Redist = new Dictionary<int, List<PathMatch>>();
		internal readonly Dictionary<int, List<PathMatch>> Other = new Dictionary<int, List<PathMatch>>();
		internal readonly Dictionary<int, List<PathMatch>> Intro = new Dictionary<int, List<PathMatch>>();
		internal readonly Dictionary<int, List<PathMatch>> NonEnglish = new Dictionary<int, List<PathMatch>>();

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
				MessageBox.Show(e.Message + "\n\nSteam Disk Saver downloads the current version of the metadata directly from GitHub. Please check your internet connectivity.", "Couldn't download metadata", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
				MessageBox.Show(e.Message, "Error in apps.json", MessageBoxButtons.OK, MessageBoxIcon.Error);
				Environment.Exit(1);
				return;
			}

			if ((int) data["version"] != 1)
			{
				MessageBox.Show("This version of Steam Disk Saver is too old. The definition requires format " + data["version"] + ", but this version only supports format 1.", "Outdated version", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				Environment.Exit(0);
			}

			foreach (var item in data["apps"])
			{
				var id = int.Parse(item.Key);
				var app = item.Value;

				Known.Add(id);

				Redist[id] = new List<PathMatch>();
				Other[id] = new List<PathMatch>();
				NonEnglish[id] = new List<PathMatch>();
				Intro[id] = new List<PathMatch>();

				if (app.ContainsKey("redist"))
					foreach (var redist in app["redist"])
					{
						Redist[id].Add(new PathMatch(redist.Value));
					}

				if (app.ContainsKey("other"))
					foreach (var other in app["other"])
					{
						Other[id].Add(new PathMatch(other.Value));
					}

				if (app.ContainsKey("intro"))
					foreach (var intro in app["intro"])
					{
						Intro[id].Add(new PathMatch(intro.Value));
					}

				if (app.ContainsKey("nonenglish"))
					foreach (var nonEnglish in app["nonenglish"])
					{
						NonEnglish[id].Add(new PathMatch(nonEnglish.Value));
					}
			}
		}
	}
}