using System.Net.Http;
using Flurl.Http.Testing;
using MessagePack;
using MessagePack.Resolvers;

namespace Flurl.Http.MsgPack
{
    public static class FlurlHttpTestMsgPackExtensions
    {
        public static HttpTestSetup RespondWithMsgPack<T>(this HttpTest httpTest, T body, int status = 200, object headers = null, object cookies = null, bool replaceUnderscoreWithHyphen = true)
        {
            var messagePackSerializerOptions = MessagePackSerializerOptions.Standard.WithResolver(TypelessContractlessStandardResolver.Instance);
            var content = new ByteArrayContent(MessagePackSerializer.Serialize<T>(body, messagePackSerializerOptions));

            return httpTest.RespondWith(() => content, status, headers, cookies, replaceUnderscoreWithHyphen);
        }
    }
}