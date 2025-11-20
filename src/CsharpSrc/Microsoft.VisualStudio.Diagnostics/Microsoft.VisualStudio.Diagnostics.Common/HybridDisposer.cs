using System;

namespace Microsoft.VisualStudio.Diagnostics.Common;

internal sealed class HybridDisposer : ComDisposer
{
	protected override void DisposeObject(object o)
	{
		if (o is IDisposable disposable)
		{
			disposable.Dispose();
		}
		else
		{
			base.DisposeObject(o);
		}
	}
}
