namespace HelperClass;
using System;
using System.Diagnostics;

public class GettingExceptions : Exception
{
    public readonly int code;
    public GettingExceptions(int code) => this.code = code;

    public GettingExceptions(string msd, Exception exception)
    {
        if (string.IsNullOrEmpty(msd))
        {
            Debugger.Break();
        }
    }


    public GettingExceptions(string msd)
    {
        if (string.IsNullOrEmpty(msd))
        {
            Debugger.Break();
        }
    }

    public GettingExceptions(Type clsN, string msd)
    {
        if (string.IsNullOrEmpty(msd))
        {
            Debugger.Break();
        }
    }

    public GettingExceptions(params string[] msd)
    {
        for (int i = 0; i < msd.Length; i++)
        {
            if (string.IsNullOrEmpty(msd[0]))
            {
                Debugger.Break();
            }
        }

    }

    #region MyRegion
    //public readonly int code;
    //public GettingExceptions(int code) => this.code = code;

    //public GettingExceptions(string msd)
    //{
    //    if (string.IsNullOrEmpty(msd))
    //    {
    //        Debugger.Break();
    //    }
    //}

    //public GettingExceptions(Type clsN, string msd)
    //{
    //    if (string.IsNullOrEmpty(msd))
    //    {
    //        Debugger.Break();
    //    }
    //}

    //public GettingExceptions(params string[] msd)
    //{
    //    for (int i = 0; i < msd.Length; i++)
    //    {
    //        if (string.IsNullOrEmpty(msd[0]))
    //        {
    //            Debugger.Break();
    //        }
    //    }

    //} 
    #endregion

}