using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SteamDiskSaver
{
	internal sealed class App
	{
		internal readonly AppManifestItem Manifest;

		internal long TotalSize;
		internal long RedistSize;
		internal long OtherSize;
		internal long IntroSize;
		internal long NonEnglishSize;
		internal readonly List<DeletableFile> Redists;
		internal readonly List<DeletableFile> Others;
		internal readonly List<DeletableFile> Intros;
		internal readonly List<DeletableFile> NonEnglish;

		internal readonly bool Known;

		internal App(AppManifestItem manifest, string steamDir)
		{
			Manifest = manifest;

			var baseDir = Manifest["UserConfig"].Items.ContainsKey("appinstalldir") ? Manifest["UserConfig"]["appinstalldir"] : Path.Combine(steamDir, "common", Manifest["installdir"]);

			var walker = DirectoryWalker.Walk(baseDir, Id);
			Known = AppMetadata.Known.Contains(Id);

			TotalSize = walker.TotalSize;
			if (!Known)
				return;

			RedistSize = walker.RedistSize;
			OtherSize = walker.OtherSize;
			IntroSize = walker.IntroSize;
			NonEnglishSize = walker.NonEnglishSize;

			Redists = walker.RedistFiles;
			Others = walker.OtherFiles;
			Intros = walker.IntroFiles;
			NonEnglish = walker.NonEnglishFiles;
		}

		public string Name
		{
			get
			{
				if (Manifest["UserConfig"].Items.ContainsKey("name"))
					return Manifest["UserConfig"]["name"];
				else
					return Manifest["installdir"].Value.Split('\\').Last();
			}
		}

		internal int Id
		{
			get
			{
				if (Manifest["UserConfig"].Items.ContainsKey("GameID"))
					return int.Parse(Manifest["UserConfig"]["GameID"]);
				return int.Parse(Manifest["appID"]);
			}
		}
	}
}