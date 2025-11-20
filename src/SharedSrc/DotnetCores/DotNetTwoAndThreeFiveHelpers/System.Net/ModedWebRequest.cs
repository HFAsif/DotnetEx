

namespace System.Net;
public class ModedWebRequest : WebRequest
{
    [__DynamicallyInvokable]
    public static HttpWebRequest CreateHttp(Uri requestUri)
    {
        if (requestUri == null)
        {
            throw new ArgumentNullException("requestUri");
        }

        if (requestUri.Scheme != Uri.UriSchemeHttp && requestUri.Scheme != Uri.UriSchemeHttps)
        {
            throw new NotSupportedException(SR.GetString("net_unknown_prefix"));
        }

        return (HttpWebRequest)CreateDefault(requestUri);
    }
}
