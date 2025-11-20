

namespace System.ReflectionEx;
using System.Reflection;

//
// Summary:
//     Contains static methods for retrieving custom attributes.
[__DynamicallyInvokable]
public static class CustomAttributeExtensionsEx
{

    public static T GetCustomAttributeEx<T>(this Reflection.MemberInfo element) where T : Attribute
    {
        return (T)element.GetCustomAttribute<T>();
    }

#if !NET40_OR_GREATER
    [__DynamicallyInvokable]
    public static T GetCustomAttribute<T>(this Reflection.MemberInfo element) where T : Attribute
    {
        return (T)GetCustomAttribute(element, typeof(T));
    }

    [__DynamicallyInvokable]
    public static Attribute GetCustomAttribute(Reflection.MemberInfo element, Type attributeType)
    {
        return Attribute.GetCustomAttribute(element, attributeType);
    }
#endif

}
