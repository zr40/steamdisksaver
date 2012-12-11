using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SteamDiskSaver
{
	internal static class AppManifestParser
	{
		internal static Dictionary<string, AppManifestItem> Parse(Stream stream)
		{
			return ParseObject(stream);
		}

		private static Dictionary<string, AppManifestItem> ParseObject(Stream stream)
		{
			var d = new Dictionary<string, AppManifestItem>();
			while (true)
			{
				SkipWhitespace(stream);

				var b = stream.ReadByte();
				if (b == -1 || (char) b == '}')
					return d;

				stream.Seek(-1, SeekOrigin.Current);

				var o = new AppManifestItem();
				o.Name = ParseString(stream);
				ParseValue(stream, o);
				d[o.Name] = o;
			}
		}

		private static void ParseValue(Stream stream, AppManifestItem o)
		{
			SkipWhitespace(stream);
			var c = (char) stream.ReadByte();
			if (c == '{')
			{
				o.Items = ParseObject(stream);
			}
			else if (c == '"')
			{
				stream.Seek(-1, SeekOrigin.Current);
				o.Value = ParseString(stream);
			}
			else
			{
				throw new InvalidOperationException();
			}
		}

		private static string ParseString(Stream stream)
		{
			SkipWhitespace(stream);
			if (stream.ReadByte() != (byte) '"')
			{
				throw new InvalidOperationException();
			}
			var str = new List<byte>();
			byte b;
			while ((b = (byte)stream.ReadByte()) != (byte) '"')
			{
				str.Add(b);
			}
			return Encoding.UTF8.GetString(str.ToArray());
		}

		private static void SkipWhitespace(Stream stream)
		{
			int b;
			char c;
			do
			{
				b = stream.ReadByte();
				c = (char) b;
			} while (c == '\t' || c == '\n');

			if (b != -1)
				stream.Seek(-1, SeekOrigin.Current);
		}
	}
}