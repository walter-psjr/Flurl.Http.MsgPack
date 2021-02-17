using System.Threading.Tasks;
using MessagePack;
using MessagePack.Resolvers;

namespace Flurl.Http.MsgPack
{
    public static class FlurlResponseExtensions
    {
        public static async Task<T> ReceiveMsgPack<T>(this Task<IFlurlResponse> response)
        {
            if (response == null)
                return default;

            var call = response.Result.ResponseMessage.RequestMessage.GetFlurlCall();
            var msgPackSerializer = call.Request.Settings.MsgPackSerializer();
            var stream = await response.ReceiveStream();
            var content = msgPackSerializer.Deserialize<T>(stream);

            return content;
        }
    }
}