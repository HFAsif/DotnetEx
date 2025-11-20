
//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Runtime.InteropServices;
//using System.Security;
//using System.Security.Permissions;
//using System.Text;
//using System.Threading.Tasks;
//using TaskTwoModed;

//namespace System.Threading;
///// <summary>Represents a lightweight alternative to <see cref="T:System.Threading.Semaphore" /> that limits the number of threads that can access a resource or pool of resources concurrently.</summary>

//[DebuggerDisplay("Current Count = {m_currentCount}")]
//[__DynamicallyInvokable]
//[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
//public partial class SemaphoreSlim
//{

//    private TaskNode m_asyncHead;
//    private TaskNode m_asyncTail;
//    private static readonly Task<bool> s_trueTask = new Task<bool>(canceled: false, result: true, (TaskCreationOptions)16384, default(CancellationToken));

//    private sealed class TaskNode : Task<bool>, IThreadPoolWorkItem
//    {
//        internal TaskNode Prev;

//        internal TaskNode Next;

//        internal TaskNode() : base()
//        {
//        }

//        [SecurityCritical]
//        void IThreadPoolWorkItem.ExecuteWorkItem()
//        {
//            bool flag = TrySetResult(result: true);
//        }

//        [SecurityCritical]
//        void IThreadPoolWorkItem.MarkAborted(ThreadAbortException tae)
//        {
//        }
//    }

//    //
//    // Summary:
//    //     Asynchronously waits to enter the System.Threading.SemaphoreSlim.
//    //
//    // Returns:
//    //     A task that will complete when the semaphore has been entered.
//    //
//    // Exceptions:
//    //   T:System.ObjectDisposedException:
//    //     The System.Threading.SemaphoreSlim has been disposed.
//    [__DynamicallyInvokable]
//    public Task WaitAsync()
//    {
//        return WaitAsync(-1, default(CancellationToken));
//    }

//    //
//    // Summary:
//    //     Asynchronously waits to enter the System.Threading.SemaphoreSlim, while observing
//    //     a System.Threading.CancellationToken.
//    //
//    // Parameters:
//    //   cancellationToken:
//    //     The System.Threading.CancellationToken token to observe.
//    //
//    // Returns:
//    //     A task that will complete when the semaphore has been entered.
//    //
//    // Exceptions:
//    //   T:System.ObjectDisposedException:
//    //     The current instance has already been disposed.
//    //
//    //   T:System.OperationCanceledException:
//    //     cancellationToken was canceled.
//    [__DynamicallyInvokable]
//    public Task WaitAsync(CancellationToken cancellationToken)
//    {
//        return WaitAsync(-1, cancellationToken);
//    }

//    //
//    // Summary:
//    //     Asynchronously waits to enter the System.Threading.SemaphoreSlim, using a 32-bit
//    //     signed integer to measure the time interval.
//    //
//    // Parameters:
//    //   millisecondsTimeout:
//    //     The number of milliseconds to wait, System.Threading.Timeout.Infinite (-1) to
//    //     wait indefinitely, or zero to test the state of the wait handle and return immediately.
//    //
//    //
//    // Returns:
//    //     A task that will complete with a result of true if the current thread successfully
//    //     entered the System.Threading.SemaphoreSlim, otherwise with a result of false.
//    //
//    //
//    // Exceptions:
//    //   T:System.ObjectDisposedException:
//    //     The current instance has already been disposed.
//    //
//    //   T:System.ArgumentOutOfRangeException:
//    //     millisecondsTimeout is a negative number other than -1, which represents an infinite
//    //     timeout -or- timeout is greater than System.Int32.MaxValue.
//    [__DynamicallyInvokable]
//    public Task<bool> WaitAsync(int millisecondsTimeout)
//    {
//        return WaitAsync(millisecondsTimeout, default(CancellationToken));
//    }

//    //
//    // Summary:
//    //     Asynchronously waits to enter the System.Threading.SemaphoreSlim, using a System.TimeSpan
//    //     to measure the time interval.
//    //
//    // Parameters:
//    //   timeout:
//    //     A System.TimeSpan that represents the number of milliseconds to wait, a System.TimeSpan
//    //     that represents -1 milliseconds to wait indefinitely, or a System.TimeSpan that
//    //     represents 0 milliseconds to test the wait handle and return immediately.
//    //
//    // Returns:
//    //     A task that will complete with a result of true if the current thread successfully
//    //     entered the System.Threading.SemaphoreSlim, otherwise with a result of false.
//    //
//    //
//    // Exceptions:
//    //   T:System.ObjectDisposedException:
//    //     The current instance has already been disposed.
//    //
//    //   T:System.ArgumentOutOfRangeException:
//    //     millisecondsTimeout is a negative number other than -1, which represents an infinite
//    //     timeout -or- timeout is greater than System.Int32.MaxValue.
//    [__DynamicallyInvokable]
//    public Task<bool> WaitAsync(TimeSpan timeout)
//    {
//        return WaitAsync(timeout, default(CancellationToken));
//    }

//    //
//    // Summary:
//    //     Asynchronously waits to enter the System.Threading.SemaphoreSlim, using a System.TimeSpan
//    //     to measure the time interval, while observing a System.Threading.CancellationToken.
//    //
//    //
//    // Parameters:
//    //   timeout:
//    //     A System.TimeSpan that represents the number of milliseconds to wait, a System.TimeSpan
//    //     that represents -1 milliseconds to wait indefinitely, or a System.TimeSpan that
//    //     represents 0 milliseconds to test the wait handle and return immediately.
//    //
//    //   cancellationToken:
//    //     The System.Threading.CancellationToken token to observe.
//    //
//    // Returns:
//    //     A task that will complete with a result of true if the current thread successfully
//    //     entered the System.Threading.SemaphoreSlim, otherwise with a result of false.
//    //
//    //
//    // Exceptions:
//    //   T:System.ArgumentOutOfRangeException:
//    //     millisecondsTimeout is a negative number other than -1, which represents an infinite
//    //     timeout -or- timeout is greater than System.Int32.MaxValue.
//    //
//    //   T:System.OperationCanceledException:
//    //     cancellationToken was canceled.
//    //
//    //   T:System.ObjectDisposedException:
//    //     The System.Threading.SemaphoreSlim has been disposed.
//    [__DynamicallyInvokable]
//    public Task<bool> WaitAsync(TimeSpan timeout, CancellationToken cancellationToken)
//    {
//        long num = (long)timeout.TotalMilliseconds;
//        if (num < -1 || num > int.MaxValue)
//        {
//            throw new ArgumentOutOfRangeException("timeout", timeout, GetResourceString("SemaphoreSlim_Wait_TimeoutWrong"));
//        }

//        return WaitAsync((int)timeout.TotalMilliseconds, cancellationToken);
//    }

//    /// <summary>Asynchronously waits to enter the <see cref="T:System.Threading.SemaphoreSlim" />, using a 32-bit signed integer to measure the time interval, while observing a <see cref="T:System.Threading.CancellationToken" />.</summary>
//	/// <param name="millisecondsTimeout">The number of milliseconds to wait, <see cref="F:System.Threading.Timeout.Infinite" /> (-1) to wait indefinitely, or zero to test the state of the wait handle and return immediately.</param>
//	/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> to observe.</param>
//	/// <returns>A task that will complete with a result of <see langword="true" /> if the current thread successfully entered the <see cref="T:System.Threading.SemaphoreSlim" />, otherwise with a result of <see langword="false" />.</returns>
//	/// <exception cref="T:System.ArgumentOutOfRangeException">
//	///   <paramref name="millisecondsTimeout" /> is a number other than -1, which represents an infinite timeout -or- timeout is greater than <see cref="F:System.Int32.MaxValue" />.</exception>
//	/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed.</exception>
//	/// <exception cref="T:System.OperationCanceledException">
//	///   <paramref name="cancellationToken" /> was canceled.</exception>
//	[__DynamicallyInvokable]
//    public Task<bool> WaitAsync(int millisecondsTimeout, CancellationToken cancellationToken)
//    {
//        CheckDispose();
//        if (millisecondsTimeout < -1)
//        {
//            throw new ArgumentOutOfRangeException("totalMilliSeconds", millisecondsTimeout, GetResourceString("SemaphoreSlim_Wait_TimeoutWrong"));
//        }
//        if (cancellationToken.IsCancellationRequested)
//        {
//            return Task.FromCancellation<bool>(cancellationToken);
//        }
//        lock (m_lockObj)
//        {
//            if (m_currentCount > 0)
//            {
//                m_currentCount--;
//                if (m_waitHandle != null && m_currentCount == 0)
//                {
//                    m_waitHandle.Reset();
//                }
//                return s_trueTask;
//            }
//            TaskNode taskNode = CreateAndAddAsyncWaiter();
//            return (millisecondsTimeout == -1 && !cancellationToken.CanBeCanceled) ? taskNode : WaitUntilCountOrTimeoutAsync(taskNode, millisecondsTimeout, cancellationToken);
//        }
//    }

//    private TaskNode CreateAndAddAsyncWaiter()
//    {
//        TaskNode taskNode = new TaskNode();
//        if (m_asyncHead == null)
//        {
//            m_asyncHead = taskNode;
//            m_asyncTail = taskNode;
//        }
//        else
//        {
//            m_asyncTail.Next = taskNode;
//            taskNode.Prev = m_asyncTail;
//            m_asyncTail = taskNode;
//        }
//        return taskNode;
//    }

//    private async Task<bool> WaitUntilCountOrTimeoutAsync(TaskNode asyncWaiter, int millisecondsTimeout, CancellationToken cancellationToken)
//    {
//        using (CancellationTokenSource cts = (cancellationToken.CanBeCanceled ? CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, default(CancellationToken)) : new CancellationTokenSource()))
//        {
//            Task<Task> task = Task.WhenAny(asyncWaiter, Task.Delay(millisecondsTimeout, cts.Token));
//            if (asyncWaiter == await task.ConfigureAwait(continueOnCapturedContext: false))
//            {
//                cts.Cancel();
//                return true;
//            }
//        }
//        lock (m_lockObj)
//        {
//            if (RemoveAsyncWaiter(asyncWaiter))
//            {
//                cancellationToken.ThrowIfCancellationRequested();
//                return false;
//            }
//        }
//        return await asyncWaiter.ConfigureAwait(continueOnCapturedContext: false);
//    }

//    private bool RemoveAsyncWaiter(TaskNode task)
//    {
//        bool result = m_asyncHead == task || task.Prev != null;
//        if (task.Next != null)
//        {
//            task.Next.Prev = task.Prev;
//        }
//        if (task.Prev != null)
//        {
//            task.Prev.Next = task.Next;
//        }
//        if (m_asyncHead == task)
//        {
//            m_asyncHead = task.Next;
//        }
//        if (m_asyncTail == task)
//        {
//            m_asyncTail = task.Prev;
//        }
//        task.Next = (task.Prev = null);
//        return result;
//    }
//}