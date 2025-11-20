using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace DotThreeFiveHelpers.Cores;
//
// Summary:
//     Defines how a method is implemented.
[Serializable]
[ComVisible(true)]
public enum MethodCodeTypeEx
{
    //
    // Summary:
    //     Specifies that the method implementation is in Microsoft intermediate language
    //     (MSIL).
    IL,
    //
    // Summary:
    //     Specifies that the method is implemented in native code.
    Native,
    //
    // Summary:
    //     Specifies that the method implementation is in optimized intermediate language
    //     (OPTIL).
    OPTIL,
    //
    // Summary:
    //     Specifies that the method implementation is provided by the runtime.
    Runtime
}
#if false // Decompilation log
'6' items in cache
#endif
