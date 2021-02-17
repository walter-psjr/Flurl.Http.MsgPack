using System.Net.Http;

namespace Flurl.Http.MsgPack
{
    /// <summary>
    /// Extension methods off HttpResponseMessage.
    /// </summary>
    internal static class HttpResponseMessageExtensions
    {
        /// <summary>
        /// Get the FlurlCall associated with this request, if any.
        /// </summary>
        /// <param name="httpRequestMessage"></param>
        /// <returns>FlurlCall</returns>
        internal static FlurlCall GetFlurlCall(this HttpRequestMessage httpRequestMessage)
        {
            if (httpRequestMessage?.Properties != null && httpRequestMessage.Properties.TryGetValue("FlurlHttpCall", out var obj) && obj is FlurlCall call)
                return call;

            return null;
        }
    }
}