using System;
using System.Collections.Generic;
using System.Json;
using System.Linq;

using Microsoft.Win32;

namespace SteamDiskSaver.Metadata
{
	internal sealed class Category
	{
		private readonly string key;
		internal readonly string Name;
		internal readonly string Description;
		private readonly bool @default;
		internal readonly string KeepIf;
		internal readonly string Benefit;

		internal readonly Dictionary<int, List<PathMatch>> Apps = new Dictionary<int, List<PathMatch>>();

		internal bool Selected;

		internal Category(string key, JsonValue data)
		{
			this.key = key;
			Name = (string) data["name"];
			Description = (string) data["description"];
			@default = (bool) data["default"];
			KeepIf = (string) data.ValueOrDefault("keep_if");
			Benefit = (string) data.ValueOrDefault("benefit");

			Selected = Convert.ToBoolean(Registry.GetValue(@"HKEY_CURRENT_USER\Software\zr40.nl\Steam Disk Saver\Categories", key, Convert.ToInt32(@default)));
		}

		internal void AddApp(int appId, JsonValue app, JsonValue engines)
		{
			var matches = new List<PathMatch>();

			if (app.ContainsKey(key))
			{
				matches.AddRange(app[key].Select(match => new PathMatch(match.Value)));
			}

			if (app.ContainsKey("engine"))
			{
				var args = app["engine"];
				var engine = engines[(string) args["name"]];
				if (engine.ContainsKey(key))
				{
					matches.AddRange(engine[key].Select(match => new PathMatch(match.Value, args)));
				}
			}

			Apps[appId] = matches;
		}

		internal void SavePreference()
		{
			Registry.SetValue(@"HKEY_CURRENT_USER\Software\zr40.nl\Steam Disk Saver\Categories", key, Convert.ToInt32(Selected));
		}
	}
}