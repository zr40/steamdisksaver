using System.Collections.Generic;

namespace SteamDiskSaver
{
	internal sealed class AppManifestItem
	{
		internal string Name;
		internal string Value;
		internal Dictionary<string, AppManifestItem> Items;

		internal AppManifestItem this[string key]
		{
			get
			{
				return Items[key];
			}
		}

		public static implicit operator string(AppManifestItem i)
		{
			return i.Value;
		}
	}
}