using Microsoft.Win32.SafeHandles;

namespace SteamDiskSaver
{
	internal sealed class FindFilesHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		public FindFilesHandle() : base(true)
		{
		}

		protected override bool ReleaseHandle()
		{
			return NativeMethods.FindClose(handle);
		}
	}
}