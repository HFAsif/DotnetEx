
namespace System.Collections.Generic;

//
// Summary:
//     Defines a provider for progress updates.
//
// Type parameters:
//   T:
//     The type of progress update value.
[__DynamicallyInvokable]
public interface IProgress<in T>
{
    //
    // Summary:
    //     Reports a progress update.
    //
    // Parameters:
    //   value:
    //     The value of the updated progress.
    [__DynamicallyInvokable]
    void Report(T value);
}
#if false // Decompilation log
'10' items in cache
#endif
