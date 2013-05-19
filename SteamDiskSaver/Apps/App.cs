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
		private readonly long SteamSize;

		internal readonly bool Known;

		internal App(AppMetadata metadata, AppManifestItem manifest, string steamDir)
		{
			this.manifest = manifest;

			string gameName = manifest["UserConfig"].Items.ContainsKey("name") ? manifest["UserConfig"]["name"] : manifest["appID"];
			
			string baseDir;
			try
			{
				baseDir = manifest["UserConfig"].Items.ContainsKey("appinstalldir") ? manifest["UserConfig"]["appinstalldir"] : Path.Combine(steamDir, "common", manifest["installdir"]);
			}
			catch (KeyNotFoundException)
			{
				throw new IgnoreAppException("Game " + gameName + ": No installdir present in manifest");
			}

			if (baseDir.Length == 2 && baseDir[1] == ':')
			{
				throw new IgnoreAppException("Game " + gameName + ": invalid appinstalldir " + baseDir);
			}

			SteamSize = long.Parse(manifest["SizeOnDisk"]);

			DirectoryWalker walker;
			try
			{
				walker = DirectoryWalker.Walk(metadata, baseDir, Id);
			}
			catch (IgnoreAppException e)
			{
				throw new IgnoreAppException("Game " + gameName + ": " + e.Message);
			}
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