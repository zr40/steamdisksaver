using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;

namespace SteamDiskSaver
{
	internal sealed class DirectoryWalker
	{
		private readonly int appId;
		private readonly Queue<string> directoriesToWalk = new Queue<string>();
		private readonly string rootDir;

		internal long TotalSize;
		internal long RedistSize;
		internal long OtherSize;
		internal long IntroSize;
		internal long NonEnglishSize;
		internal readonly List<DeletableFile> RedistFiles = new List<DeletableFile>();
		internal readonly List<DeletableFile> OtherFiles = new List<DeletableFile>();
		internal readonly List<DeletableFile> IntroFiles = new List<DeletableFile>();
		internal readonly List<DeletableFile> NonEnglishFiles = new List<DeletableFile>();

		private DirectoryWalker(string path, int appId)
		{
			this.appId = appId;
			rootDir = path;
			directoriesToWalk.Enqueue(path);

			Walk();
		}

		internal static DirectoryWalker Walk(string path, int appId)
		{
			return new DirectoryWalker(path, appId);
		}

		private void Walk()
		{
			while (directoriesToWalk.Count != 0)
			{
				string directory = directoriesToWalk.Dequeue();

				foreach (NativeMethods.FindData findData in new FindFilesEnumerator(directory + Path.DirectorySeparatorChar + "*"))
				{
					InspectFile(findData, directory);
				}

				InspectDirectory(directory + Path.DirectorySeparatorChar);
			}
		}

		private void InspectFile(NativeMethods.FindData findData, string directory)
		{
			if (findData.fileName == "." || findData.fileName == "..")
				return;

			if (findData.fileAttributes.HasFlag(FileAttributes.ReparsePoint))
				return; // we're not going to deal with reparse points inside a game directory.

			if (findData.fileAttributes.HasFlag(FileAttributes.Directory))
			{
				directoriesToWalk.Enqueue(directory + Path.DirectorySeparatorChar + findData.fileName);
			}
			else
			{
				var fullPath = Path.Combine(directory, findData.fileName);
				long size = 0;
				try
				{
					size = (long) NativeMethods.CompressedFileSize(fullPath);
				}
				catch (Win32Exception e)
				{
					if (e.NativeErrorCode == 2)
					{
						// File not found. Perhaps it was just now deleted.
						Debug.WriteLine("File not found: " + fullPath);
					}
					else
					{
						throw;
					}
				}

				TotalSize += size;

				if (AppMetadata.Known.Contains(appId))
				{
					var path = fullPath.Substring(rootDir.Length + 1);
					CheckFile(AppMetadata.Redist, path, size, fullPath, ref RedistSize, RedistFiles);
					CheckFile(AppMetadata.Other, path, size, fullPath, ref OtherSize, OtherFiles);
					CheckFile(AppMetadata.Intro, path, size, fullPath, ref IntroSize, IntroFiles);
					CheckFile(AppMetadata.NonEnglish, path, size, fullPath, ref NonEnglishSize, NonEnglishFiles);
				}
			}
		}

		private void CheckFile(Dictionary<int, List<PathMatch>> metadata, string path, long size, string fullPath, ref long subtotalSize, List<DeletableFile> deletes)
		{
			var match = metadata[appId].Find(m => m.Match(path));
			if (match == null)
				return;

			subtotalSize += size;
			deletes.Add(new DeletableFile(fullPath, match));
		}

		private void InspectDirectory(string fullPath)
		{
			if (AppMetadata.Known.Contains(appId))
			{
				var path = fullPath.Substring(rootDir.Length + 1);
				CheckFile(AppMetadata.Redist, path, 0, fullPath, ref RedistSize, RedistFiles);
				CheckFile(AppMetadata.Other, path, 0, fullPath, ref OtherSize, OtherFiles);
				CheckFile(AppMetadata.Intro, path, 0, fullPath, ref IntroSize, IntroFiles);
				CheckFile(AppMetadata.NonEnglish, path, 0, fullPath, ref NonEnglishSize, NonEnglishFiles);
			}
		}
	}
}