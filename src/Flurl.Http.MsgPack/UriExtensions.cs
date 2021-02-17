using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Flurl.Http.MsgPack
{
    public static class UriExtensions
    {
        public static Task<T> GetMsgPackAsync<T>(this Uri uri, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead) =>
            new FlurlRequest(uri).GetMsgPackAsync<T>(cancellationToken, completionOption);

        public static Task<IFlurlResponse> PostMsgPackAsync(this Uri uri, object data, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead) =>
            new FlurlRequest(uri).PostMsgPackAsync(data, cancellationToken, completionOption);

        public static Task<IFlurlResponse> PutMsgPackAsync(this Uri uri, object data, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead) =>
            new FlurlRequest(uri).PutMsgPackAsync(data, cancellationToken, completionOption);

        public static Task<IFlurlResponse> SendMsgPackAsync(this Uri uri, HttpMethod httpMethod, object data, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead) =>
            new FlurlRequest(uri).SendMsgPackAsync(httpMethod, data, cancellationToken, completionOption);
    }
}