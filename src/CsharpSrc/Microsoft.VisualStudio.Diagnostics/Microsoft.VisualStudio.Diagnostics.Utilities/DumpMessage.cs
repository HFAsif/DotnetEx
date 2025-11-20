using System;

namespace Microsoft.VisualStudio.Diagnostics.Utilities;

internal class DumpMessage
{
	public MessageKind Kind { get; private set; }

	public string Message { get; private set; }

	public DateTime Timestamp { get; private set; }

	public Exception Exception { get; private set; }

	public DumpMessage(DateTime timestamp, MessageKind kind, string message, Exception exception)
	{
		Kind = kind;
		Timestamp = timestamp;
		Message = message;
		Exception = exception;
	}
}
