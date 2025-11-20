

namespace HelperClass;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Text;

public static partial class HelperViewsStatic
{
    [Obsolete]
    public static void SetupSecurityProtocol()
    {
#if NET20 || NET35
        // This property selects the version of the Secure Sockets Layer (SSL) or
        // existing connections aren't changed.
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | (SecurityProtocolType)DotThreeFiveHelpers.Cores.SecurityProtocolTypeN.Tls11
            | (SecurityProtocolType)DotThreeFiveHelpers.Cores.SecurityProtocolTypeN.Tls12;
//#elif NET40
//        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | ((SecurityProtocolType)(0x300)) | ((SecurityProtocolType)(0xC00));

#else
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
#endif

        // Accept the request for POST, PUT and PATCH verbs
        ServicePointManager.Expect100Continue = false;

        // Note: Any changes to the DefaultConnectionLimit property affect both HTTP 1.0 and HTTP 1.1 connections.
        // It is not possible to separately alter the connection limit for HTTP 1.0 and HTTP 1.1 protocols.
        ServicePointManager.DefaultConnectionLimit = 1000;

        // Set the maximum idle time of a ServicePoint instance to 10 seconds.
        // After the idle time expires, the ServicePoint object is eligible for
        // garbage collection and cannot be used by the ServicePointManager object.
        ServicePointManager.MaxServicePointIdleTime = 10000;

        ServicePointManager.ServerCertificateValidationCallback =
            new RemoteCertificateValidationCallback(All.ExceptionHelper.CertificateValidationCallBack);
    }

    [Obsolete]
    public static List<string> SNKArrayFromSnk(string Snklocation)
    {
        byte[] snk = File.ReadAllBytes(Snklocation);
        byte[] publicKey = GetPublicKeyFromSnk(snk);

        var _publicKey =
#if NET20 || NET35
            Join("", publicKey.ToArray());
#else
            string.Join("", publicKey.ToArray());
#endif

        byte[] publicKeyToken = GetPublicKeyTokenFromSnk(publicKey);

        var _publicKeyToken =
#if NET20 || NET35
            Join("", publicKeyToken.ToArray());
#else
            string.Join("", publicKey.ToArray());
#endif
        return [_publicKey, _publicKeyToken];
    }


#if NET20 || NET35
    public static string Join<T>(string separator, IEnumerable<T> values)
    {
        if (values == null)
        {
            throw new ArgumentNullException("values");
        }

        if ((object)separator == null)
        {
            separator = string.Empty;
        }

        using IEnumerator<T> enumerator = values.GetEnumerator();
        if (!enumerator.MoveNext())
        {
            return string.Empty;
        }

        StringBuilder stringBuilder = FrameWorkEngine.StringBuilderCache.Acquire();
        if (enumerator.Current != null)
        {
            string text = enumerator.Current.ToString();
            if ((object)text != null)
            {
                stringBuilder.Append(text);
            }
        }

        while (enumerator.MoveNext())
        {
            stringBuilder.Append(separator);
            if (enumerator.Current != null)
            {
                string text2 = enumerator.Current.ToString();
                if ((object)text2 != null)
                {
                    stringBuilder.Append(text2);
                }
            }
        }

        return FrameWorkEngine.StringBuilderCache.GetStringAndRelease(stringBuilder);
    }
#endif



}
