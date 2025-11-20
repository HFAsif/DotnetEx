using System;
using System.IO;
using Microsoft.VisualStudio.Diagnostics.Common;

namespace Microsoft.VisualStudio.Diagnostics.Utilities;

internal static class PathUtilities
{
	public static string NormalizePath(string path)
	{
		Check.ThrowIfNull(path, "path");
		return Path.GetFullPath(new Uri(path).LocalPath).TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar).ToLowerInvariant();
	}
}
