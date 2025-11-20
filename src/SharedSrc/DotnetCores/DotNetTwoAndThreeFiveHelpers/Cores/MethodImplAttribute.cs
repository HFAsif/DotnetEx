

using System;
using System.Reflection;

namespace DotThreeFiveHelpers.Cores;

/// <summary>Specifies the details of how a method is implemented. This class cannot be inherited.</summary>
[Serializable]
[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method, Inherited = false)]
[System.Runtime.InteropServices.ComVisible(true)]
[__DynamicallyInvokable]
public sealed class MethodImplAttributeEx : Attribute
{
    internal MethodImplOptionsEx _val;

    /// <summary>A <see cref="T:System.Runtime.CompilerServices.MethodCodeType" /> value indicating what kind of implementation is provided for this method.</summary>
    public MethodCodeTypeEx MethodCodeType;

    /// <summary>Gets the <see cref="T:System.Runtime.CompilerServices.MethodImplOptions" /> value describing the attributed method.</summary>
    /// <returns>The <see cref="T:System.Runtime.CompilerServices.MethodImplOptions" /> value describing the attributed method.</returns>
    [__DynamicallyInvokable]
    public MethodImplOptionsEx Value
    {
        [__DynamicallyInvokable]
        get
        {
            return _val;
        }
    }

    internal MethodImplAttributeEx(MethodImplAttributes methodImplAttributes)
    {
        MethodImplOptionsEx methodImplOptions = MethodImplOptionsEx.Unmanaged | MethodImplOptionsEx.ForwardRef | MethodImplOptionsEx.PreserveSig | MethodImplOptionsEx.InternalCall | MethodImplOptionsEx.Synchronized | MethodImplOptionsEx.NoInlining | MethodImplOptionsEx.AggressiveInlining | MethodImplOptionsEx.NoOptimization | MethodImplOptionsEx.SecurityMitigations;
        _val = (MethodImplOptionsEx)((int)methodImplAttributes & (int)methodImplOptions);
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Runtime.CompilerServices.MethodImplAttribute" /> class with the specified <see cref="T:System.Runtime.CompilerServices.MethodImplOptions" /> value.</summary>
    /// <param name="methodImplOptions">A <see cref="T:System.Runtime.CompilerServices.MethodImplOptions" /> value specifying properties of the attributed method.</param>
    [__DynamicallyInvokable]
    public MethodImplAttributeEx(MethodImplOptionsEx methodImplOptions)
    {
        _val = methodImplOptions;
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Runtime.CompilerServices.MethodImplAttribute" /> class with the specified <see cref="T:System.Runtime.CompilerServices.MethodImplOptions" /> value.</summary>
    /// <param name="value">A bitmask representing the desired <see cref="T:System.Runtime.CompilerServices.MethodImplOptions" /> value which specifies properties of the attributed method.</param>
    public MethodImplAttributeEx(short value)
    {
        _val = (MethodImplOptionsEx)value;
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Runtime.CompilerServices.MethodImplAttribute" /> class.</summary>
    public MethodImplAttributeEx()
    {
    }
}
