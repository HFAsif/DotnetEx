using System.Collections.Generic;
using System.Linq;

namespace Microsoft.VisualStudio.Diagnostics.Utilities;

internal static class EnumerableUtilities
{
	public static bool AreEnumerableEqual<T>(IEnumerable<T> left, IEnumerable<T> right)
	{
		if (left == null)
		{
			if (right == null)
			{
				return true;
			}
			return false;
		}
		if (right == null)
		{
			return false;
		}
		if (left.Count() != right.Count())
		{
			return false;
		}
		return new HashSet<T>(left).SetEquals(right);
	}
}
