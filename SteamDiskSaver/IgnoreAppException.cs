using System;

namespace SteamDiskSaver
{
	internal sealed class IgnoreAppException : Exception
	{
		internal IgnoreAppException(string reason) : base(reason)
		{
		}
	}
}