using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;

namespace SteamDiskSaver
{
	internal static class NativeMethods
	{
		internal const int ErrorNoMoreFiles = 18;
		private const uint InvalidFileSize = uint.MaxValue;

		[DllImport("kernel32", SetLastError = true, CharSet = CharSet.Unicode)]
		internal static extern FindFilesHandle FindFirstFile(string fileName, out FindData findFileData);

		[DllImport("kernel32", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool FindNextFile(FindFilesHandle findFileHandle, out FindData findFileData);

		[DllImport("kernel32", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool FindClose(IntPtr findFileHandle);

		[DllImport("kernel32", SetLastError = true, CharSet = CharSet.Unicode)]
		private static extern uint GetCompressedFileSize(string fileName, out uint fileSizeHigh);

		internal static ulong CompressedFileSize(string fileName)
		{
			uint high;
			uint low = GetCompressedFileSize(fileName, out high);
			if (low == InvalidFileSize && Marshal.GetLastWin32Error() != 0)
				throw new Win32Exception();

			return ((ulong) high << 32) | low;
		}

		[StructLayout(LayoutKind.Sequential)]
		private struct FileTime
		{
			private readonly uint lowDateTime;
			private readonly uint highDateTime;
		}

		[StructLayout(LayoutKind.Sequential)]
		internal struct FindData
		{
			internal readonly FileAttributes fileAttributes;
			private readonly FileTime creationTime;
			private readonly FileTime lastAccessTime;
			private readonly FileTime lastWriteTime;
			private readonly uint fileSizeHigh;
			private readonly uint fileSizeLow;
			private readonly uint reserved0;
			private readonly uint reserved1;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
			internal readonly string fileName;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 14)]
			private readonly string alternateFileName;
		}
	}
}