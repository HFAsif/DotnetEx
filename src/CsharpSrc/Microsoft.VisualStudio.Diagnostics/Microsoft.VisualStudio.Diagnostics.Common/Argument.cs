using System;
using System.Collections.Generic;

namespace Microsoft.VisualStudio.Diagnostics.Common;

internal static class Argument
{
	public unsafe static void AssertNotNull(byte* value, string name)
	{
		if (value == null)
		{
			throw new ArgumentNullException(name);
		}
	}

	public static void AssertNotNull<T>(T value, string name) where T : class
	{
		if (value == null)
		{
			throw new ArgumentNullException(name);
		}
	}

	public static void AssertRange<T>(T value, string name, T min, T max) where T : struct
	{
		if (Comparer<T>.Default.Compare(min, value) > 0 || Comparer<T>.Default.Compare(value, max) > 0)
		{
			throw new ArgumentOutOfRangeException(name);
		}
	}

	public static void AssertRange(bool expression, string name)
	{
		if (!expression)
		{
			throw new ArgumentOutOfRangeException(name);
		}
	}

	public static void AssertRange(bool expression, string name, string message)
	{
		if (!expression)
		{
			throw new ArgumentOutOfRangeException(name, message);
		}
	}

	public static void AssertValid(bool expression, string name)
	{
		if (!expression)
		{
			throw new ArgumentException(string.Empty, name);
		}
	}

	public static void AssertValid(bool expression, string name, string message)
	{
		if (!expression)
		{
			throw new ArgumentException(message, name);
		}
	}
}
