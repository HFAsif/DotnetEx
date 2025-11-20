using System;
using System.Collections.Generic;
using System.Text;
namespace System.Threading.Tasks;

public partial class TaskFactory<TResult>
{
    private sealed class FromAsyncTrimPromise<TInstance> : Task<TResult> where TInstance : class
    {
        internal static readonly AsyncCallback s_completeFromAsyncResult = CompleteFromAsyncResult;

        private TInstance m_thisRef;

        private Func<TInstance, IAsyncResult, TResult> m_endMethod;

        internal FromAsyncTrimPromise(TInstance thisRef, Func<TInstance, IAsyncResult, TResult> endMethod)
        {
            m_thisRef = thisRef;
            m_endMethod = endMethod;
        }

        internal static void CompleteFromAsyncResult(IAsyncResult asyncResult)
        {
            if (asyncResult == null)
            {
                throw new ArgumentNullException("asyncResult");
            }
            if (!(asyncResult.AsyncState is FromAsyncTrimPromise<TInstance> { m_thisRef: var thisRef, m_endMethod: var endMethod } fromAsyncTrimPromise))
            {
                throw new ArgumentException(Environment2.GetResourceString("InvalidOperation_WrongAsyncResultOrEndCalledMultiple"), "asyncResult");
            }
            fromAsyncTrimPromise.m_thisRef = null;
            fromAsyncTrimPromise.m_endMethod = null;
            if (endMethod == null)
            {
                throw new ArgumentException(Environment2.GetResourceString("InvalidOperation_WrongAsyncResultOrEndCalledMultiple"), "asyncResult");
            }
            if (!asyncResult.CompletedSynchronously)
            {
                fromAsyncTrimPromise.Complete(thisRef, endMethod, asyncResult, requiresSynchronization: true);
            }
        }

        internal void Complete(TInstance thisRef, Func<TInstance, IAsyncResult, TResult> endMethod, IAsyncResult asyncResult, bool requiresSynchronization)
        {
            bool flag = false;

            try
            {
                TResult result = endMethod(thisRef, asyncResult);
                if (requiresSynchronization)
                {
                    flag = TrySetResult(result);
                    return;
                }
                DangerousSetResult(result);
                flag = true;
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (OperationCanceledException ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                // flag = TrySetCanceled(ex.CancellationToken, ex);
                //flag = TrySetException(ex);
            }
            catch (Exception exceptionObject)
            {
                flag = TrySetException(exceptionObject);
            }

        }


    }


    public static Task<TResult> FromAsyncTrim<TInstance, TArgs>(TInstance thisRef, TArgs args, Func<TInstance, TArgs, AsyncCallback, object, IAsyncResult> beginMethod, Func<TInstance, IAsyncResult, TResult> endMethod) where TInstance : class
    {
        FromAsyncTrimPromise<TInstance> fromAsyncTrimPromise = new FromAsyncTrimPromise<TInstance>(thisRef, endMethod);
        IAsyncResult asyncResult = beginMethod(thisRef, args, FromAsyncTrimPromise<TInstance>.s_completeFromAsyncResult, fromAsyncTrimPromise);
        if (asyncResult.CompletedSynchronously)
        {
            fromAsyncTrimPromise.Complete(thisRef, endMethod, asyncResult, requiresSynchronization: false);
        }
        return fromAsyncTrimPromise;
    }
}