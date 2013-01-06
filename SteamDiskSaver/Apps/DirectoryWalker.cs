using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;

using SteamDiskSaver.Metadata;
using SteamDiskSaver.Win32;

namespace SteamDiskSaver.Apps
{
	internal sealed class DirectoryWalker
	{
		private readonly AppMetadata metadata;
		private readonly int appId;
		private readonly Queue<string> directoriesToWalk = new Queue<string>();
		private readonly string rootDir;

		internal long TotalSize;
		internal long DeletableSize;
		internal long NotSelectedSize;
		internal readonly List<DeletableFile> DeletableFiles = new List<DeletableFile>();

		private DirectoryWalker(AppMetadata metadata, string path, int appId)
		{
			this.metadata = metadata;
			this.appId = appId;
			rootDir = path;
			directoriesToWalk.Enqueue(path);

			Walk();
		}

		internal static DirectoryWalker Walk(AppMetadata metadata, string path, int appId)
		{
			return new DirectoryWalker(metadata, path, appId);
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

				if (metadata.Known.Contains(appId))
				{
					var path = fullPath.Substring(rootDir.Length + 1);
					CheckFile(path, size, fullPath);
				}
			}
		}

		private void CheckFile(string path, long size, string fullPath)
		{
			foreach (var category in metadata.Categories)
			{
				if (category.Apps.ContainsKey(appId))
				{
					var match = category.Apps[appId].Find(m => m.Match(path));
					if (match == null)
						continue;

					if (category.Selected)
					{
						DeletableSize += size;
						DeletableFiles.Add(new DeletableFile(fullPath, match));
					}
					else
					{
						NotSelectedSize += size;
					}
				}
			}
		}

		private void InspectDirectory(string fullPath)
		{
			if (metadata.Known.Contains(appId))
			{
				var path = fullPath.Substring(rootDir.Length + 1);
				CheckFile(path, 0, fullPath);
			}
		}
	}
}