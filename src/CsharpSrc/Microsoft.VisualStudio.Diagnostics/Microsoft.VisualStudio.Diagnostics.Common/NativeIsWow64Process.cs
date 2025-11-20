using System;
using System.Runtime.InteropServices;

namespace Microsoft.VisualStudio.Diagnostics.Common;

[UnmanagedFunctionPointer(CallingConvention.Winapi)]
internal delegate bool NativeIsWow64Process(IntPtr hProcess, out bool Wow64Process);
