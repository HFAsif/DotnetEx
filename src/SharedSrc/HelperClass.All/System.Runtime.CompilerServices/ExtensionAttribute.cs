#if NET20 || NET30 || NET40 

namespace System.Runtime.CompilerServices;

[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Method)]
public sealed class ExtensionAttribute : Attribute
{
}

#endif