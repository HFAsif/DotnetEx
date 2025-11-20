
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using DotThreeFiveHelpers.Cores;

namespace DotThreeFiveHelpers.Utils;
//
// Summary:
//     Represents a strongly-typed, read-only collection of elements.
//
// Type parameters:
//   T:
//     The type of the elements.
[TypeDependency("System.SZArrayHelper")]
[__DynamicallyInvokable]
public interface IReadOnlyCollection<out T> : IEnumerable<T>, IEnumerable
{
    //
    // Summary:
    //     Gets the number of elements in the collection.
    //
    // Returns:
    //     The number of elements in the collection.
    [__DynamicallyInvokable]
    int Count
    {
        [__DynamicallyInvokable]
        get;
    }
}
#if false // Decompilation log
'11' items in cache
#endif
