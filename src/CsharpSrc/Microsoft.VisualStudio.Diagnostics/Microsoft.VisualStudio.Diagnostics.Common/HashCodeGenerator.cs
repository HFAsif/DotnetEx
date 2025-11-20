namespace Microsoft.VisualStudio.Diagnostics.Common;

internal static class HashCodeGenerator
{
	public static int GenerateHashCode(params object[] fields)
	{
		int num = 17;
		for (int i = 0; i < fields.Length; i++)
		{
			num = num * 23 + (fields[i]?.GetHashCode() ?? 0);
		}
		return num;
	}
}
