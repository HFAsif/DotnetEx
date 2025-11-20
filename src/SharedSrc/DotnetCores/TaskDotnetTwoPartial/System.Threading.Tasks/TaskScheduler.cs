using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security;
using System.Security.Permissions;

namespace System.Threading.Tasks;

public abstract partial class TaskScheduler
{
    private static object _unobservedTaskExceptionLockObject = new object();
    internal static void PublishUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs ueea)
    {
        lock (_unobservedTaskExceptionLockObject)
        {
            TaskScheduler.UnobservedTaskException?.Invoke(sender, ueea);
        }
    }
}