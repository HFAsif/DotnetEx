using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.Diagnostics.Common;

namespace Microsoft.VisualStudio.Diagnostics.Utilities;

internal static class BatchUtilities
{
	public static IEnumerable<BatchResult> Batch(int totalSize, int batchSize)
	{
		Check.Throw<ArgumentOutOfRangeException>(batchSize > 0);
		if (totalSize <= 0)
		{
			yield break;
		}
		int remainder = totalSize % batchSize;
		bool hasRemainder = remainder > 0;
		int batchCount = totalSize / batchSize;
		if (batchCount > 0)
		{
			for (int i = 0; i < batchCount - 1; i++)
			{
				yield return new BatchResult
				{
					Size = batchSize,
					IsLast = false
				};
			}
			yield return new BatchResult
			{
				Size = batchSize,
				IsLast = !hasRemainder
			};
		}
		if (hasRemainder)
		{
			yield return new BatchResult
			{
				Size = remainder,
				IsLast = true
			};
		}
	}
}
