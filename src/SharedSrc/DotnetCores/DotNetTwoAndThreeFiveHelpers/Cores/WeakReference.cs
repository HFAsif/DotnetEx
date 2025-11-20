
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;


//using System.Security;
using System.Text;

namespace DotThreeFiveHelpers.Cores;

/// <summary>Represents a typed weak reference, which references an object while still allowing that object to be reclaimed by garbage collection.</summary>
/// <typeparam name="T">The type of the object referenced.</typeparam>
[Serializable]
[__DynamicallyInvokable]
public sealed class WeakReference<T> : ISerializable where T : class
{
    internal IntPtr m_handle;

    private extern T Target
    {
        [MethodImpl(MethodImplOptions.InternalCall)]
        [System.Security.SecuritySafeCritical]
        get;
        [MethodImpl(MethodImplOptions.InternalCall)]
        [System.Security.SecuritySafeCritical]
        set;
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.WeakReference`1" /> class that references the specified object.</summary>
    /// <param name="target">The object to reference, or <see langword="null" />.</param>
    [__DynamicallyInvokable]
    public WeakReference(T target)
        : this(target, trackResurrection: false)
    {
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.WeakReference`1" /> class that references the specified object and uses the specified resurrection tracking.</summary>
    /// <param name="target">The object to reference, or <see langword="null" />.</param>
    /// <param name="trackResurrection">
    ///   <see langword="true" /> to track the object after finalization; <see langword="false" /> to track the object only until finalization.</param>
    [__DynamicallyInvokable]
    public WeakReference(T target, bool trackResurrection)
    {
        Create(target, trackResurrection);
    }

    internal WeakReference(SerializationInfo info, StreamingContext context)
    {
        if (info == null)
        {
            throw new ArgumentNullException("info");
        }
        T target = (T)info.GetValue("TrackedObject", typeof(T));
        bool boolean = info.GetBoolean("TrackResurrection");
        Create(target, boolean);
    }

    /// <summary>Tries to retrieve the target object that is referenced by the current <see cref="T:System.WeakReference`1" /> object.</summary>
    /// <param name="target">When this method returns, contains the target object, if it is available. This parameter is treated as uninitialized.</param>
    /// <returns>
    ///   <see langword="true" /> if the target was retrieved; otherwise, <see langword="false" />.</returns>
#if NET20 || NET35
    [MethodImplAttributeEx(MethodImplOptionsEx.AggressiveInlining)]
#else
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
    [__DynamicallyInvokable]
    public bool TryGetTarget(out T target)
    {
        return (target = Target) != null;
    }

    /// <summary>Sets the target object that is referenced by this <see cref="T:System.WeakReference`1" /> object.</summary>
    /// <param name="target">The new target object.</param>
    [__DynamicallyInvokable]
    public void SetTarget(T target)
    {
        Target = target;
    }

    /// <summary>Discards the reference to the target that is represented by the current <see cref="T:System.WeakReference`1" /> object.</summary>
    [MethodImpl(MethodImplOptions.InternalCall)]
    [System.Security.SecuritySafeCritical]
    extern ~WeakReference();

    /// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with all the data necessary to serialize the current <see cref="T:System.WeakReference`1" /> object.</summary>
    /// <param name="info">An object that holds all the data necessary to serialize or deserialize the current <see cref="T:System.WeakReference`1" /> object.</param>
    /// <param name="context">The location where serialized data is stored and retrieved.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="info" /> is <see langword="null" />.</exception>
    [System.Security.SecurityCritical]
    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        if (info == null)
        {
            throw new ArgumentNullException("info");
        }
        info.AddValue("TrackedObject", Target, typeof(T));
        info.AddValue("TrackResurrection", IsTrackResurrection());
    }

    [MethodImpl(MethodImplOptions.InternalCall)]
    [System.Security.SecuritySafeCritical]
    private extern void Create(T target, bool trackResurrection);

    [MethodImpl(MethodImplOptions.InternalCall)]
    [System.Security.SecuritySafeCritical]
    private extern bool IsTrackResurrection();
}
