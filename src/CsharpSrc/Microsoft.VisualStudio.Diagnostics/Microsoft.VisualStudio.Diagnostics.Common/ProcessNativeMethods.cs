using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace Microsoft.VisualStudio.Diagnostics.Common;

internal static class ProcessNativeMethods
{
	private static volatile int _IsWow64CurrentProcess = -1;

	[DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
	public static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

	[DllImport("kernel32", CharSet = CharSet.Unicode, EntryPoint = "GetModuleHandleW", SetLastError = true)]
	public static extern IntPtr GetModuleHandle(string moduleName);

	[DllImport("kernel32", ExactSpelling = true)]
	public static extern IntPtr GetCurrentProcess();

	public static bool IsWow64CurrentProcess()
	{
		int isWow64CurrentProcess = _IsWow64CurrentProcess;
		if (isWow64CurrentProcess < 0)
		{
			bool flag = IsWow64Process(GetCurrentProcess());
			_IsWow64CurrentProcess = (flag ? 1 : 0);
			return flag;
		}
		return isWow64CurrentProcess > 0;
	}

	public static bool IsWow64Process(IntPtr processHandle)
	{
		IntPtr procAddress = GetProcAddress(GetModuleHandle("kernel32"), "IsWow64Process");
		if (procAddress == IntPtr.Zero)
		{
			return false;
		}
		NativeIsWow64Process nativeIsWow64Process = (NativeIsWow64Process)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(NativeIsWow64Process));
		if (!nativeIsWow64Process(processHandle, out var Wow64Process))
		{
			Check.Throw<Win32Exception>();
		}
		return Wow64Process;
	}
}
