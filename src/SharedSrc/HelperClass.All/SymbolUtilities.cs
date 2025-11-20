namespace HelperClass;


using System;
using System.IO;
using System.Runtime.InteropServices;
public static class SymbolUtilities
{

#if _Unsafe_
    public static T AddressToDelegate<T>(this nint _Address) where T : class
    {
        return Marshal.GetDelegateForFunctionPointer(_Address, typeof(T)) as T;
    }
#endif

    public static string TryGetFileName(string path)
    {
        try
        {
            return Path.GetFileName(path);
        }
        catch (IOException)
        {
            return null;
        }
        catch (ArgumentException)
        {
            return null;
        }
    }

    public static string TryGetDirectory(string path)
    {
        try
        {
            return Path.GetDirectoryName(path);
        }
        catch (IOException)
        {
            return null;
        }
        catch (ArgumentException)
        {
            return null;
        }
    }
    [CLSCompliant(false)]
    public static ulong TimeDateStampToFileTime(uint timeDateStamp)
    {
        return (ulong)((long)timeDateStamp * 10000000L + 116444736000000000L);
    }
}
