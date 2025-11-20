using System;
using System.Collections.Generic;
using System.Text;

namespace DotThreeFiveHelpers.Cores;
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface, AllowMultiple = true, Inherited = false)]
internal sealed class TypeDependencyAttribute : Attribute
{
    private string typeName;

    public TypeDependencyAttribute(string typeName)
    {
        if (typeName == null)
        {
            throw new ArgumentNullException("typeName");
        }

        this.typeName = typeName;
    }
}
#if false // Decompilation log
'11' items in cache
#endif
