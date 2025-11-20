using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

namespace Microsoft.VisualStudio.Diagnostics.Common;

internal static class Check
{
	private static bool _suppressAllAsserts;

	public static bool SuppressAllAsserts
	{
		get
		{
			return _suppressAllAsserts;
		}
		set
		{
			_suppressAllAsserts = value;
		}
	}

	[DebuggerHidden]
	public static void HRThrow(int hr)
	{
		if (HResult.Failed(hr))
		{
			AssertInternal(null, Marshal.GetExceptionForHR(hr));
		}
	}

	[DebuggerHidden]
	public static bool Verify(bool condition)
	{
		if (!condition)
		{
			AssertInternal(null, null);
		}
		return condition;
	}

	[DebuggerHidden]
	public static bool Verify(bool condition, string message)
	{
		if (!condition)
		{
			AssertInternal(message, null);
		}
		return condition;
	}

	[DebuggerHidden]
	[Conditional("DEBUG")]
	public static void Assert(bool condition)
	{
		if (!condition)
		{
			AssertInternal(null, null);
		}
	}

	[DebuggerHidden]
	[Conditional("DEBUG")]
	public static void Assert(bool condition, string message)
	{
		if (!condition)
		{
			AssertInternal(message, null);
		}
	}

	[DebuggerHidden]
	[Conditional("DEBUG")]
	public static void Assert(string message)
	{
		AssertInternal(message, null);
	}

	[DebuggerHidden]
	public static void Throw(bool condition)
	{
		if (!condition)
		{
			AssertInternal(null, GetException<InvariantException>(new object[0]));
		}
	}

	[DebuggerHidden]
	public static void Throw<XT>(bool condition = false) where XT : Exception
	{
		if (!condition)
		{
			AssertInternal(null, GetException<XT>(new object[0]));
		}
	}

	[DebuggerHidden]
	public static void ThrowIfNull(object param, string paramName)
	{
		if (param == null)
		{
			AssertInternal(null, new ArgumentNullException(paramName));
		}
	}

	[DebuggerHidden]
	public static void Throw(bool condition, string message)
	{
		if (!condition)
		{
			AssertInternal(message, GetException<InvariantException>(new object[1] { message }));
		}
	}

	[DebuggerHidden]
	public static void Throw(string message)
	{
		AssertInternal(message, GetException<InvariantException>(new object[1] { message }));
	}

	[DebuggerHidden]
	public static void Throw<XT>(bool condition, string message, Func<Exception> innerEx) where XT : Exception
	{
		if (!condition)
		{
			AssertInternal(message, GetException<XT>(message, innerEx()));
		}
	}

	[DebuggerHidden]
	public static void Throw<XT>(bool condition, string message, Exception innerEx = null) where XT : Exception
	{
		if (!condition)
		{
			AssertInternal(message, GetException<XT>(message, innerEx));
		}
	}

	[DebuggerHidden]
	public static void Throw<XT>(string message, Func<Exception> innerEx) where XT : Exception
	{
		AssertInternal(message, GetException<XT>(message, innerEx()));
	}

	[DebuggerHidden]
	public static void Throw<XT>(string message, Exception innerEx = null) where XT : Exception
	{
		AssertInternal(message, GetException<XT>(message, innerEx));
	}

	public static bool IsCriticalException(Exception e)
	{
		if (!(e is StackOverflowException) && !(e is AccessViolationException) && !(e is AppDomainUnloadedException) && !(e is BadImageFormatException) && !(e is DivideByZeroException) && !(e is NullReferenceException) && !(e is OutOfMemoryException))
		{
			return e is ThreadAbortException;
		}
		return true;
	}

	public static bool SwallowNonCriticalExceptionWrapper(Action action, Action finalAction = null)
	{
		ThrowIfNull(action, "action");
		bool result = false;
		try
		{
			action();
			result = true;
		}
		catch (Exception e)
		{
			if (IsCriticalException(e))
			{
				throw;
			}
		}
		finally
		{
			finalAction?.Invoke();
		}
		return result;
	}

	public static Exception GetException<XT>(params object[] xtParams) where XT : Exception
	{
		try
		{
			return Activator.CreateInstance(typeof(XT), xtParams) as Exception;
		}
		catch
		{
			throw;
		}
	}

	public static Exception GetException<XT>(string message, Exception innerException) where XT : Exception
	{
		return typeof(XT).GetConstructor(new Type[2]
		{
			typeof(string),
			typeof(Exception)
		}).Invoke(new object[2] { message, innerException }) as Exception;
	}

	[DebuggerHidden]
	private static void AssertInternal(string message, Exception ex)
	{
		if (ex != null)
		{
			throw ex;
		}
	}
}
