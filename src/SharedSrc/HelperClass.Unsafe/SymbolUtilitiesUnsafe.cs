namespace HelperClass.Unsafe;
using Microsoft.VisualStudio.Diagnostics.Common;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

internal class SymbolUtilitiesUnsafe
{
    public static IntPtr StructToPtr<T>(out T _T) where T : struct
    {
        _T = new T();
        var _intPtr = Marshal.AllocHGlobal(Marshal.SizeOf(_T));
        try
        {
            Marshal.StructureToPtr(_T, _intPtr, true);
            return _intPtr;
        }
        catch (Exception ex)
        {
            throw new GettingExceptions(ex.Message, ex);
        }
    }

    public static T PtrToStruct<T>(IntPtr _pnt) where T : struct
    {
        try
        {
            //var _members = (T)Marshal.PtrToStructure(_pnt, typeof(T));
            return (T)Marshal.PtrToStructure(_pnt, typeof(T));
        }
        catch (Exception ex)
        {
            throw new GettingExceptions(ex.Message, ex);
        }
        finally
        {
            Marshal.FreeHGlobal(_pnt);
        }
    }


    public static T BytesToStructure<T>(byte[] bytes) where T : struct
    {
        Check.ThrowIfNull(bytes, "bytes");
        int num = Marshal.SizeOf(typeof(T));
        IntPtr intPtr = Marshal.AllocHGlobal(num);
        try
        {
            Check.Throw<ArgumentOutOfRangeException>(bytes.Length >= num);
            Marshal.Copy(bytes, 0, intPtr, num);
            return (T)Marshal.PtrToStructure(intPtr, typeof(T));
        }
        finally
        {
            Marshal.FreeHGlobal(intPtr);
        }
    }
}
