namespace HelperClass;
using System;
using System.Collections.Generic;
using System.Text;

[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
public class SomeElementsInfos : Attribute
{
    public readonly string Details;

    public SomeElementsInfos(string details)
    {
        Details = details;
    }


}