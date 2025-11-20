using System;
using System.Diagnostics;
using Microsoft.VisualStudio.Diagnostics.Common;

namespace Microsoft.VisualStudio.Diagnostics.Utilities;

internal sealed class NonThrowingEventLog : IDisposable
{

    private EventLog _underlyingEventLog;

	private bool _isDisposed;

	public NonThrowingEventLog(string source)
	{
		NonThrowingEventLog nonThrowingEventLog = this;
		EventLogExecutor(delegate
		{
            nonThrowingEventLog._underlyingEventLog = new EventLog();
            nonThrowingEventLog._underlyingEventLog.Source = source;
        });
	}

	public void WriteEntry(string message, EventLogEntryType type)
	{
		if (_underlyingEventLog != null)
		{
			EventLogExecutor(delegate
			{
				_underlyingEventLog.WriteEntry(message, type);
			});
		}
	}

	public void Dispose()
	{
		if (!_isDisposed)
		{
			_isDisposed = true;
			if (_underlyingEventLog != null)
			{
				_underlyingEventLog.Dispose();
				_underlyingEventLog = null;
			}
		}
	}

	private static void EventLogExecutor(Action action)
	{
		try
		{
			action();
		}
		catch (Exception e) when (!Check.IsCriticalException(e))
		{
		}
	}
}
