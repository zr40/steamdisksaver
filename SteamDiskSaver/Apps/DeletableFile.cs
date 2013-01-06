using System.IO;

using SteamDiskSaver.Metadata;

namespace SteamDiskSaver.Apps
{
	internal sealed class DeletableFile
	{
		private readonly string fullPath;
		private readonly PathMatch match;

		internal DeletableFile(string fullPath, PathMatch match)
		{
			this.fullPath = fullPath;
			this.match = match;
		}

		internal void Delete()
		{
			if (match.EmptyFile)
			{
				using (File.Open(fullPath, FileMode.Truncate))
				{
				}
			}
			else
			{
				if (fullPath.EndsWith("\\") && !Directory.Exists(fullPath))
					return;

				if (Directory.Exists(fullPath))
					Directory.Delete(fullPath, true);
				else if (File.Exists(fullPath))
					File.Delete(fullPath);
			}
		}
	}
}