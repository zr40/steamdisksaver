using System;
using System.Json;
using System.Windows.Forms;

namespace SteamDiskSaver.Metadata
{
	internal sealed class PathMatch
	{
		private readonly string pathMatch;
		internal readonly bool EmptyFile;
		private readonly bool matchContains;
		private readonly bool ignore;

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

		internal PathMatch(JsonValue rule, JsonValue replacements) : this(rule)
		{
			foreach (var replacement in replacements)
			{
				pathMatch = pathMatch.Replace(':' + replacement.Key + ':', (string)replacement.Value);
			}

			ignore = pathMatch.Contains(":"); // unreplaced argument. Ignore this match
		}

		internal bool Match(string path)
		{
			if (matchContains)
				return path.Contains(pathMatch);

			return (path == pathMatch || (pathMatch.EndsWith("\\") && path.StartsWith(pathMatch)));
		}
	}
}