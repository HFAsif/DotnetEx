namespace HelperClass;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

public class InternalLogger
{
    public static void MyLogs(params string[] str)
    {
        Debug.WriteLine(str);
    }
}
