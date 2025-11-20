

namespace HelperClass;

using System;
using System.Security;
using Sri = System.Runtime.InteropServices;

[System.Security.SuppressUnmanagedCodeSecurity]
public static class MarshalEx
{
#if NET20 || NET35 || NET40 || NET45
    [SecurityCritical]
    public static TDelegate GetDelegateForFunctionPointer<TDelegate>(IntPtr ptr)
    {
        return (TDelegate)(object)Sri.Marshal.GetDelegateForFunctionPointer(ptr, typeof(TDelegate));
    }

    [SecurityCritical]
    public static IntPtr GetFunctionPointerForDelegate<TDelegate>(TDelegate d)
    {
        var result = (Delegate)(object)d;
        var MarshalMethod = typeof(Sri.Marshal).GetMethod("GetFunctionPointerForDelegateInternal", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
        var _compilationCallBackPtr = (IntPtr)(MarshalMethod.Invoke(null, new object[1] { result }));
        return _compilationCallBackPtr;
    }

#else
    public static TDelegate GetDelegateForFunctionPointer<TDelegate>(IntPtr ptr)
    {
        return Sri.Marshal.GetDelegateForFunctionPointer<TDelegate>(ptr);
    }

    public static IntPtr GetFunctionPointerForDelegate<TDelegate>(TDelegate d)
    {
        return Sri.Marshal.GetFunctionPointerForDelegate<TDelegate>(d);
    }
#endif


    [SecurityCritical]
    public static void DestroyStructure<T>(IntPtr ptr)
    {
#if NET20 || NET35 || NET40 || NET45
        Sri.Marshal.DestroyStructure(ptr, typeof(T));
#else
        Sri.Marshal.DestroyStructure<T>(ptr);
#endif

    }


    public static T PtrToStructure<T>(System.IntPtr ptr) 
    {
#if NET20 || NET35 || NET40 || NET45
        return (T)PtrToStructure(ptr, typeof(T));
#else
        return Sri.Marshal.PtrToStructure<T>(ptr);
#endif
    }

    [SecurityCritical]
    public static void StructureToPtr<T>(T structure, IntPtr ptr, bool fDeleteOld)
    {
#if NET20 || NET35 || NET40 || NET45
        Sri.Marshal.StructureToPtr((T)structure, ptr, fDeleteOld);
        //return (T)PtrToStructure(ptr, typeof(T));
#else
        Sri.Marshal.StructureToPtr<T>(structure, ptr, fDeleteOld);
#endif
    }

    //static T PtrToStructure<T>(System.IntPtr ptr) where T : struct
    //{
    //    return (T)PtrToStructure(ptr, typeof(T));
    //}

    //[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.InternalCall)]
    //[SecurityCritical]
    //[System.Runtime.ConstrainedExecution.ReliabilityContract(System.Runtime.ConstrainedExecution.Consistency.WillNotCorruptState, System.Runtime.ConstrainedExecution.Cer.MayFail)]
    //[ComVisible(true)]
    //public static extern void StructureToPtr(object structure, IntPtr ptr, bool fDeleteOld)
    //{

    //}

    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
    [System.Security.SecurityCritical]
    [Sri.ComVisible(true)]
    static object PtrToStructure(System.IntPtr ptr, System.Type structureType)
    {
        return Sri.Marshal.PtrToStructure(ptr, structureType);
        
    }
}

