using System.Diagnostics;
using System.Json;

namespace SteamDiskSaver.Metadata
{
	internal sealed class PathMatch
	{
		private readonly string pathMatch;
		internal readonly bool EmptyFile;
		private readonly bool matchContains;
		private readonly bool matchStartsWith;
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

						case "startswith":
							matchStartsWith = true;
							break;

						default:
							ignore = true;
							Debug.WriteLine("Unknown flag '{0}' for match '{1}'", flag, pathMatch);
							return;
					}
				}
			}
		}

		internal PathMatch(JsonValue rule, JsonValue replacements) : this(rule)
		{
			foreach (var replacement in replacements)
			{
				pathMatch = pathMatch.Replace('<' + replacement.Key + '>', (string) replacement.Value);
			}

			ignore |= pathMatch.Contains("<") || pathMatch.Contains(">"); // unreplaced argument. Ignore this match
		}

		internal bool Match(string path)
		{
			if (matchStartsWith || pathMatch.EndsWith("\\"))
				return path.StartsWith(pathMatch);

			if (matchContains)
				return path.Contains(pathMatch);

			return path == pathMatch;
		}
	}
}