using System;
using System.Collections.Generic;

namespace Microsoft.VisualStudio.Diagnostics.Utilities;

internal class DumpLog
{
	private static DumpLog instance = null;

	private static object syncRoot = new object();

	private const int MaxMessageCount = 100;

	private long _totalMessages;

	private Queue<DumpMessage> _log = new Queue<DumpMessage>();

	public static DumpLog Instance
	{
		get
		{
			if (instance == null)
			{
				lock (syncRoot)
				{
					if (instance == null)
					{
						instance = new DumpLog();
					}
				}
			}
			return instance;
		}
	}

	private DumpLog()
	{
	}

	public void Log(MessageKind kind, string message, Exception exception = null)
	{
		lock (syncRoot)
		{
			_totalMessages++;
			while (_log.Count > 100)
			{
				_log.Dequeue();
			}
			DumpMessage item = new DumpMessage(DateTime.Now, kind, message, exception);
			_log.Enqueue(item);
		}
	}

	public static void LogException(Exception e)
	{
		Instance.Log(MessageKind.Exception, e.Message, e);
	}
}
