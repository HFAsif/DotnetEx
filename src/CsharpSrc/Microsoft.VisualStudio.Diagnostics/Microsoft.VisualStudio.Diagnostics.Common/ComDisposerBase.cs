using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Microsoft.VisualStudio.Diagnostics.Common;

internal abstract class ComDisposerBase : IDisposable
{
	protected List<object> ObjectList { get; set; }

	protected ComDisposerBase()
	{
		ObjectList = new List<object>();
	}

	public void Add(object o)
	{
		ObjectList.Add(o);
	}

	public void AddRange(IEnumerable<object> collection)
	{
		ObjectList.AddRange(collection);
	}

	public void Remove(object o)
	{
		ObjectList.Remove(o);
	}

	void IDisposable.Dispose()
	{
		Dispose(disposing: true);
		GC.SuppressFinalize(this);
	}

	protected abstract void Dispose(bool disposing);

	protected abstract void DisposeObject(object o);

	public static void ReleaseComObject(object o)
	{
		if (o != null && Marshal.IsComObject(o))
		{
			Marshal.ReleaseComObject(o);
		}
	}

	public static void ReleaseComReference(IntPtr ptr)
	{
		if (ptr != IntPtr.Zero)
		{
			Marshal.Release(ptr);
		}
	}
}
