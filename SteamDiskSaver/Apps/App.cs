using System.Collections.Generic;
using System.IO;
using System.Linq;

using SteamDiskSaver.Metadata;

namespace SteamDiskSaver.Apps
{
	internal sealed class App
	{
		private readonly AppManifestItem manifest;

		internal long TotalSize;
		internal long DeletableSize;
		internal readonly long NotSelectedSize;
		internal readonly List<DeletableFile> DeletableFiles;
		internal readonly long SteamSize;

		internal readonly bool Known;

		internal App(AppMetadata metadata, AppManifestItem manifest, string steamDir)
		{
			this.manifest = manifest;

			string baseDir;
			try
			{
				baseDir = manifest["UserConfig"].Items.ContainsKey("appinstalldir") ? this.manifest["UserConfig"]["appinstalldir"] : Path.Combine(steamDir, "common", this.manifest["installdir"]);
			}
			catch (KeyNotFoundException)
			{
				throw new IgnoreAppException("No installdir present in manifest");
			}

			SteamSize = long.Parse(manifest["SizeOnDisk"]);

			var walker = DirectoryWalker.Walk(metadata, baseDir, Id);
			Known = metadata.Known.Contains(Id);

			TotalSize = walker.TotalSize;
			if (!Known)
				return;

			DeletableSize = walker.DeletableSize;
			NotSelectedSize = walker.NotSelectedSize;

			DeletableFiles = walker.DeletableFiles;
		}

		public string Name
		{
			get
			{
				if (manifest["UserConfig"].Items.ContainsKey("name"))
					return manifest["UserConfig"]["name"];

				return manifest["installdir"].Value.Split('\\').Last();
			}
		}

		internal int Id
		{
			get
			{
				if (manifest["UserConfig"].Items.ContainsKey("GameID"))
					return int.Parse(manifest["UserConfig"]["GameID"]);
				return int.Parse(manifest["appID"]);
			}
		}

		public long SavedSize
		{
			get
			{
				if (TotalSize > SteamSize)
					return 0;

				return SteamSize - TotalSize;
			}
		}
	}
}