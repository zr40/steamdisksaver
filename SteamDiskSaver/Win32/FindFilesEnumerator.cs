using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace SteamDiskSaver.Win32
{
	internal sealed class FindFilesEnumerator : IEnumerator<NativeMethods.FindData>, IEnumerable<NativeMethods.FindData>
	{
		private readonly FindFilesHandle handle;
		private NativeMethods.FindData findData;
		private bool first = true;

		internal FindFilesEnumerator(string directory)
		{
			handle = NativeMethods.FindFirstFile(directory, out findData);
			if (handle.IsInvalid)
			{
				throw new Win32Exception();
			}
		}

		public IEnumerator<NativeMethods.FindData> GetEnumerator()
		{
			return this;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public void Dispose()
		{
			handle.Dispose();
		}

		public bool MoveNext()
		{
			if (first)
			{
				first = false;
				return true;
			}

			if (!NativeMethods.FindNextFile(handle, out findData))
			{
				if (Marshal.GetLastWin32Error() == NativeMethods.ErrorNoMoreFiles)
					return false;
				throw new Win32Exception();
			}
			return true;
		}

		public void Reset()
		{
			throw new NotSupportedException();
		}

		public NativeMethods.FindData Current
		{
			get
			{
				return findData;
			}
		}

		object IEnumerator.Current
		{
			get
			{
				return Current;
			}
		}
	}
}