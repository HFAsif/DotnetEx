using System.Globalization;

namespace Microsoft.VisualStudio.Diagnostics.Common;

internal static class StringFormat
{
	public static string FormatUI(string format, params object[] parameters)
	{
		return string.Format(CultureInfo.CurrentUICulture, format, parameters);
	}
}
