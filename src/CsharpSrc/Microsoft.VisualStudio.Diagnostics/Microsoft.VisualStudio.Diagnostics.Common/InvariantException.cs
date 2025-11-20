using System;
using System.Runtime.Serialization;

namespace Microsoft.VisualStudio.Diagnostics.Common;

[Serializable]
internal class InvariantException : Exception
{
	public InvariantException()
	{
	}

	public InvariantException(string message)
		: base(message)
	{
	}

	public InvariantException(string message, Exception innerException)
		: base(message, innerException)
	{
	}

	protected InvariantException(SerializationInfo info, StreamingContext context)
		: base(info, context)
	{
	}
}
