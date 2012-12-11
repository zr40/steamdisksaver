using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Windows.Forms;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SteamDiskSaver
{
	internal sealed class PathMatch
	{
		private readonly string pathMatch;
		internal readonly bool EmptyFile;
		private readonly bool matchContains;

		internal PathMatch(JToken rule)
		{
			if (rule.Type == JTokenType.String)
			{
				pathMatch = rule.Value<string>();
			}
			else
			{
				pathMatch = rule[0].Value<string>();

				var item = rule[0];
				while ((item = item.Next) != null)
				{
					var flag = item.Value<string>();
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

	internal static class AppMetadata
	{
		internal static readonly HashSet<int> Known = new HashSet<int>();

		internal static readonly Dictionary<int, List<PathMatch>> Redist = new Dictionary<int, List<PathMatch>>();
		internal static readonly Dictionary<int, List<PathMatch>> Other = new Dictionary<int, List<PathMatch>>();
		internal static readonly Dictionary<int, List<PathMatch>> Intro = new Dictionary<int, List<PathMatch>>();
		internal static readonly Dictionary<int, List<PathMatch>> NonEnglish = new Dictionary<int, List<PathMatch>>();

		internal static void GetDefinition()
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
				return;
			}

			ParseJson(json);
		}

		private static void ParseJson(string json)
		{
			JObject data;
			try
			{
				data = JObject.Parse(json);
			}
			catch (JsonReaderException e)
			{
				MessageBox.Show(e.Message, "Error in apps.json", MessageBoxButtons.OK, MessageBoxIcon.Error);
				Environment.Exit(1);
				return;
			}

			if (data["version"].Value<int>() != 1)
			{
				MessageBox.Show("This version of Steam Disk Saver is too old. The definition requires format " + data["version"] + ", but this version only supports format 1.", "Outdated version", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				Environment.Exit(0);
			}

			foreach (JProperty app in data["apps"])
			{
				var id = int.Parse((app).Name);
				Known.Add(id);

				Redist[id] = new List<PathMatch>();
				Other[id] = new List<PathMatch>();
				NonEnglish[id] = new List<PathMatch>();
				Intro[id] = new List<PathMatch>();

				if (app.Value["redist"] != null)
					foreach (var redist in app.Value["redist"])
					{
						Redist[id].Add(new PathMatch(redist));
					}

				if (app.Value["other"] != null)
					foreach (var other in app.Value["other"])
					{
						Other[id].Add(new PathMatch(other));
					}

				if (app.Value["intro"] != null)
					foreach (var intro in app.Value["intro"])
					{
						Intro[id].Add(new PathMatch(intro));
					}

				if (app.Value["nonenglish"] != null)
					foreach (var nonEnglish in app.Value["nonenglish"])
					{
						NonEnglish[id].Add(new PathMatch(nonEnglish));
					}
			}
		}
	}
}