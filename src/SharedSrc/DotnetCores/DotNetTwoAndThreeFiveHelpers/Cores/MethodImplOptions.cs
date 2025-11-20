
using System;
namespace DotThreeFiveHelpers.Cores;

/// <summary>Defines the details of how a method is implemented.</summary>
[Serializable]
[Flags]
[System.Runtime.InteropServices.ComVisible(true)]
[__DynamicallyInvokable]
public enum MethodImplOptionsEx
{
    /// <summary>The method is implemented in unmanaged code.</summary>
    Unmanaged = 4,
    /// <summary>The method is declared, but its implementation is provided elsewhere.</summary>
    ForwardRef = 0x10,
    /// <summary>The method signature is exported exactly as declared.</summary>
    [__DynamicallyInvokable]
    PreserveSig = 0x80,
    /// <summary>The call is internal, that is, it calls a method that is implemented within the common language runtime.</summary>
    InternalCall = 0x1000,
    /// <summary>The method can be executed by only one thread at a time. Static methods lock on the type, whereas instance methods lock on the instance. Only one thread can execute in any of the instance functions, and only one thread can execute in any of a class's static functions.</summary>
    Synchronized = 0x20,
    /// <summary>The method cannot be inlined. Inlining is an optimization by which a method call is replaced with the method body.</summary>
    [__DynamicallyInvokable]
    NoInlining = 8,
    /// <summary>The method should be inlined if possible.</summary>
    [System.Runtime.InteropServices.ComVisible(false)]
    [__DynamicallyInvokable]
    AggressiveInlining = 0x100,
    /// <summary>The method is not optimized by the just-in-time (JIT) compiler or by native code generation (see Ngen.exe) when debugging possible code generation problems.</summary>
    [__DynamicallyInvokable]
    NoOptimization = 0x40,
    /// <summary>The JIT compiler should look for security mitigation attributes, such as the user-defined <see langword="System.Runtime.CompilerServices.SecurityMitigationsAttribute" />. If found, the JIT compiler applies any related security mitigations. Available starting with .NET Framework 4.8.</summary>
    SecurityMitigations = 0x400
}
