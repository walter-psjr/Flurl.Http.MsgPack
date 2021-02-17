using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using Flurl.Http.Configuration;
using Flurl.Http.MsgPack.Test.Models;
using MessagePack;
using MessagePack.Resolvers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

namespace Flurl.Http.MsgPack.Test.Factories
{
    [ExcludeFromCodeCoverage]
    internal class TypelessModelHttpClientFactory : DefaultHttpClientFactory
    {
        private readonly TypelessModel _model;

        public TypelessModelHttpClientFactory(TypelessModel model)
        {
            _model = model;
        }

        public override HttpClient CreateHttpClient(HttpMessageHandler handler)
        {
            var messagePackSerializerOptions = MessagePackSerializerOptions.Standard;
            messagePackSerializerOptions = messagePackSerializerOptions.WithResolver(TypelessContractlessStandardResolver.Instance);
            var serializedModel = MessagePackSerializer.Serialize(_model, messagePackSerializerOptions);
            var builder = new WebHostBuilder()
                .Configure(app =>
                {
                    app.Use(async (context, next) =>
                    {
                        await context.Response.Body.WriteAsync(serializedModel);
                    });
                });

            var server = new TestServer(builder);
            var httpClient = server.CreateClient();

            return httpClient;
        }
    }
}