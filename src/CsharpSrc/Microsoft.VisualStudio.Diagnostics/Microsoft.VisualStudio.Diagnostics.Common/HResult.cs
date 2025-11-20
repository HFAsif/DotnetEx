namespace Microsoft.VisualStudio.Diagnostics.Common;

internal static class HResult
{
	public const int S_OK = 0;

	public const int S_FALSE = 1;

	public const int E_NOTIMPL = -2147467263;

	public const int E_ABORT = -2147467260;

	public const int E_FAIL = -2147467259;

	public const int E_UNEXPECTED = -2147418113;

	public const int E_INVALIDARG = -2147024809;

	public const int E_OUTOFMEMORY = -2147024882;

	public const int RPC_E_DISCONNECTED = -2147417848;

	public const int RPC_E_CANTCALLOUT_INEXTERNALCALL = -2147418107;

	public const int CO_E_OBJNOTCONNECTED = -2147220995;

	public const int E_PROGRAM_DESTROY_PENDING = -2147218687;

	public const int E_DESTROYED = -2147220991;

	public const int E_TRACE_DETACH_UNSUPPORTED = -2147218176;

	public const int E_THREAD_NOT_FOUND = -2147218175;

	public const int E_OBJECT_OUT_OF_SYNC = -2147218173;

	public const int CORDBG_E_BAD_THREAD_STATE = -2146233555;

	public const int CORDBG_E_IL_VAR_NOT_AVAILABLE = -2146233596;

	public const int CORDBG_E_BAD_REFERENCE_VALUE = -2146233595;

	public const int E_PDB_NOT_FOUND = -2140340219;

	internal const int FACILITY_WIN32 = 7;

	public static bool Failed(int hr)
	{
		return hr < 0;
	}

	public static bool Succeeded(int hr)
	{
		return !Failed(hr);
	}

	public static int HResultFromWin32(int errorcode)
	{
		if (errorcode > 0)
		{
			return (errorcode & 0xFFFF) | -2147024896;
		}
		return errorcode;
	}
}
