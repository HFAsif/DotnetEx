using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace System.Threading.Tasks;
public partial class Task<TResult>
{
    internal Task()
    {
    }

    internal Task(bool canceled, TResult result, TaskCreationOptions creationOptions, CancellationToken ct)
        : base(canceled, creationOptions, ct)
    {
        if (!canceled)
        {
            m_result = result;
        }
    }

    internal void DangerousSetResult(TResult result)
    {
        if (m_parent != null)
        {
            bool flag = TrySetResult(result);
            return;
        }

        m_result = result;
        m_stateFlags |= 16777216;
    }

}

public partial class Task
{
    internal Task(bool canceled, TaskCreationOptions creationOptions, CancellationToken ct)
    {
        if (canceled)
        {
            m_stateFlags = (int)((TaskCreationOptions)5242880 | creationOptions);
            ContingentProperties contingentProperties = (m_contingentProperties = new ContingentProperties());
            contingentProperties.m_cancellationToken = ct;
            contingentProperties.m_internalCancellationRequested = 1;
        }
        else
        {
            m_stateFlags = (int)((TaskCreationOptions)16777216 | creationOptions);
        }
    }

    internal Task()
    {
        m_stateFlags = 33555456;
    }

    [FriendAccessAllowed]
    internal static Task FromCancellation(CancellationToken cancellationToken)
    {
        if (!cancellationToken.IsCancellationRequested)
        {
            throw new ArgumentOutOfRangeException("cancellationToken");
        }
        return new Task(canceled: true, TaskCreationOptions.None, cancellationToken);
    }

    [FriendAccessAllowed]
    public static Task<TResult> FromCancellation<TResult>(CancellationToken cancellationToken)
    {
        if (!cancellationToken.IsCancellationRequested)
        {
            throw new ArgumentOutOfRangeException("cancellationToken");
        }
        return new Task<TResult>(canceled: true, default(TResult), TaskCreationOptions.None, cancellationToken);
    }
}