
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DotThreeFiveHelpers.Utils;
//
// Summary:
//     Supports a simple iteration over a generic collection.
//
// Type parameters:
//   T:
//     The type of objects to enumerate.
[__DynamicallyInvokable]
public interface IEnumerator<out T> : IDisposable, IEnumerator
{
    //
    // Summary:
    //     Gets the element in the collection at the current position of the enumerator.
    //
    //
    // Returns:
    //     The element in the collection at the current position of the enumerator.
    [__DynamicallyInvokable]
    new T Current
    {
        [__DynamicallyInvokable]
        get;
    }
}
#if false // Decompilation log
'11' items in cache
#endif
