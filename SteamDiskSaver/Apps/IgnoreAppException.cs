using System;

namespace SteamDiskSaver.Apps
{
	internal sealed class IgnoreAppException : Exception
	{
		internal IgnoreAppException(string reason) : base(reason)
		{
		}
	}
}