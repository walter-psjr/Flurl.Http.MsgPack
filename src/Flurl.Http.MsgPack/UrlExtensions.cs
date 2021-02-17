﻿using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Flurl.Http.MsgPack
{
    public static class UrlExtensions
    {
        public static Task<T> GetMsgPackAsync<T>(this Url url, CancellationToken cancellationToken = default, HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead) =>
            new FlurlRequest(url).GetMsgPackAsync<T>(cancellationToken, completionOption);

        public static Task<IFlurlResponse> PostMsgPackAsync(this Url url, object data, CancellationToken cancellationToken = default, HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead) =>
            new FlurlRequest(url).PostMsgPackAsync(data, cancellationToken, completionOption);

        public static Task<IFlurlResponse> PutMsgPackAsync(this Url url, object data, CancellationToken cancellationToken = default, HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead) =>
            new FlurlRequest(url).PutMsgPackAsync(data, cancellationToken, completionOption);

        public static Task<IFlurlResponse> SendMsgPackAsync(this Url url, HttpMethod httpMethod, object data, CancellationToken cancellationToken = default, HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead) =>
            new FlurlRequest(url).SendMsgPackAsync(httpMethod, data, cancellationToken, completionOption);
    }
}