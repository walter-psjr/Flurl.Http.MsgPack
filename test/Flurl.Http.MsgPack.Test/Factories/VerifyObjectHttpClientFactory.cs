using Flurl.Http.Configuration;
using Flurl.Http.MsgPack.Test.Models;
using MessagePack;
using MessagePack.Resolvers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System.Net.Http;
using Xunit;

namespace Flurl.Http.MsgPack.Test.Factories
{
    internal class VerifyObjectHttpClientFactory : DefaultHttpClientFactory
    {
        private readonly TypelessModel _model;

        public VerifyObjectHttpClientFactory(TypelessModel model)
        {
            _model = model;
        }

        public override HttpClient CreateHttpClient(HttpMessageHandler handler)
        {
            var builder = new WebHostBuilder().Configure(app =>
            {
                app.Use(async (context, next) =>
                {
                    var messagePackSerializerOptions = MessagePackSerializerOptions.Standard.WithResolver(TypelessContractlessStandardResolver.Instance);
                    var body = await MessagePackSerializer.DeserializeAsync<TypelessModel>(context.Request.Body, messagePackSerializerOptions);

                    Assert.Equal(_model.Id, body.Id);
                    Assert.Equal(_model.BirthDate, body.BirthDate);
                    Assert.Equal(_model.Name, body.Name);

                    context.Response.StatusCode = 200;
                });
            });
            var server = new TestServer(builder);
            var httpClient = server.CreateClient();

            return httpClient;
        }
    }
}