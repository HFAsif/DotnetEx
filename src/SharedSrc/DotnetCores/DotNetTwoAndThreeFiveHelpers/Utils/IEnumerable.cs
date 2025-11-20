
using DotThreeFiveHelpers.Cores;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DotThreeFiveHelpers.Utils;

//
// Summary:
//     Exposes the enumerator, which supports a simple iteration over a collection of
//     a specified type.
//
// Type parameters:
//   T:
//     The type of objects to enumerate.
[TypeDependency("System.SZArrayHelper")]
[__DynamicallyInvokable]
public interface IEnumerable<out T> : IEnumerable
{
    //
    // Summary:
    //     Returns an enumerator that iterates through the collection.
    //
    // Returns:
    //     An enumerator that can be used to iterate through the collection.
    [__DynamicallyInvokable]
    new IEnumerator<T> GetEnumerator();
}
#if false // Decompilation log
'11' items in cache
#endif
