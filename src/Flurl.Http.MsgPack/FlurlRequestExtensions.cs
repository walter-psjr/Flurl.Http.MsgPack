using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Flurl.Http.MsgPack
{
    public static class FlurlRequestExtensions
    {
        public static async Task<IFlurlResponse> SendMsgPackAsync(this IFlurlRequest request, HttpMethod httpMethod, object data, CancellationToken cancellationToken = default, HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
        {
            if (data == null)
            {
                return await request.SendAsync(httpMethod, null, cancellationToken, completionOption);
            }

            var msgPackSerializer = request.Settings.MsgPackSerializer();
            var content = new ByteArrayContent(msgPackSerializer.Serialize(data));

            return await request.SendAsync(httpMethod, content, cancellationToken, completionOption);
        }

        public static Task<T> GetMsgPackAsync<T>(this IFlurlRequest request, CancellationToken cancellationToken = default, HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
        {
            return request
                .SendMsgPackAsync(HttpMethod.Get, null, cancellationToken, completionOption)
                .ReceiveMsgPack<T>();
        }

        public static Task<IFlurlResponse> PostMsgPackAsync(this IFlurlRequest request, object data, CancellationToken cancellationToken = default, HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
        {
            return request.SendMsgPackAsync(HttpMethod.Post, data, cancellationToken, completionOption);
        }

        public static Task<IFlurlResponse> PutMsgPackAsync(this IFlurlRequest request, object data, CancellationToken cancellationToken = default, HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
        {
            return request.SendMsgPackAsync(HttpMethod.Put, data, cancellationToken, completionOption);
        }
    }
}