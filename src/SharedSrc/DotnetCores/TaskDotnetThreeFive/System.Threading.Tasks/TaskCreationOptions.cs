namespace System.Threading.Tasks;

/// <summary>
/// Specifies flags that control optional behavior for the creation and execution of tasks.
/// </summary>
[Serializable]
[Flags]
public enum TaskCreationOptions
{
	/// <summary>
	/// Specifies that the default behavior should be used.
	/// </summary>
	None = 0,
	/// <summary>
	/// A hint to a <see cref="T:System.Threading.Tasks.TaskScheduler">TaskScheduler</see> to schedule a
	/// task in as fair a manner as possible, meaning that tasks scheduled sooner will be more likely to
	/// be run sooner, and tasks scheduled later will be more likely to be run later.
	/// </summary>
	PreferFairness = 1,
	/// <summary>
	/// Specifies that a task will be a long-running, course-grained operation. It provides a hint to the
	/// <see cref="T:System.Threading.Tasks.TaskScheduler">TaskScheduler</see> that oversubscription may be
	/// warranted. 
	/// </summary>
	LongRunning = 2,
	/// <summary>
	/// Specifies that a task is attached to a parent in the task hierarchy.
	/// </summary>
	AttachedToParent = 4
}
