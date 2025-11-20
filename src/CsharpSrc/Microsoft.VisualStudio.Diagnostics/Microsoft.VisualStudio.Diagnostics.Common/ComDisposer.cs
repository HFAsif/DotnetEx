using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace Microsoft.VisualStudio.Diagnostics.Common;

internal class ComDisposer : ComDisposerBase
{
	private bool _isDisposed;

	public void AddReference(IntPtr ptr)
	{
		if (_isDisposed)
		{
			throw new ObjectDisposedException("ComDisposer");
		}
		Check.Throw<ArgumentException>(ptr != IntPtr.Zero, "ptr");
		Add(ptr);
	}

	public void AddComObject(object o)
	{
		if (_isDisposed)
		{
			throw new ObjectDisposedException("ComDisposer");
		}
		Check.ThrowIfNull(o, "o");
		Check.Throw<ArgumentException>(Marshal.IsComObject(o), "o");
		Add(o);
	}

	public static ComDisposer GetComDisposer(params object[] comObjects)
	{
		ComDisposer comDisposer = new ComDisposer();
		comDisposer.AddRange(comObjects);
		return comDisposer;
	}

	public static void DisposeComObjects(params object[] comObjects)
	{
		using (GetComDisposer(comObjects.Where((object o) => o != null)))
		{
		}
	}

	protected sealed override void Dispose(bool disposing)
	{
		if (!disposing || base.ObjectList == null || _isDisposed)
		{
			return;
		}
		foreach (object @object in base.ObjectList)
		{
			DisposeObject(@object);
		}
		base.ObjectList = null;
	}

	protected override void DisposeObject(object o)
	{
		try
		{
			if (o is IntPtr)
			{
				ComDisposerBase.ReleaseComReference((IntPtr)o);
			}
			ComDisposerBase.ReleaseComObject(o);
		}
		catch
		{
		}
	}
}
