using Flurl.Http.MsgPack.Test.Factories;
using Flurl.Http.MsgPack.Test.Models;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Threading.Tasks;
using Flurl.Http.Configuration;
using Xunit;

namespace Flurl.Http.MsgPack.Test
{
    [ExcludeFromCodeCoverage]
    public class FlurlRequestExtensionsTests
    {
        [Fact]
        public async Task GetMsgPackAsync_TypelessContract()
        {
            // Arrange
            var typelessModel = new TypelessModel
            {
                Id = Guid.NewGuid(),
                Name = "Steve Jobs",
                BirthDate = new DateTime(1970, 01, 01)
            };
            var factory = new TypelessModelHttpClientFactory(typelessModel);

            // Act
            using (var client = new FlurlClient()
            {
                Settings = new ClientFlurlHttpSettings()
                {
                    HttpClientFactory = factory
                }
            })
            {
                var result = await new FlurlRequest("https://api.test.com").WithClient(client).GetMsgPackAsync<TypelessModel>();

                // Assert
                Assert.Equal(typelessModel.Id, result.Id);
                Assert.Equal(typelessModel.BirthDate, result.BirthDate);
                Assert.Equal(typelessModel.Name, result.Name);
            }
        }

        [Fact]
        public async Task PostMsgPackAsync_TypelessContract()
        {
            // Arrange
            var typelessModel = new TypelessModel
            {
                Id = Guid.NewGuid(),
                Name = "Steve Jobs - POST",
                BirthDate = new DateTime(1970, 01, 01)
            };
            var factory = new VerifyObjectHttpClientFactory(typelessModel);

            // Act
            using (var client = new FlurlClient()
            {
                Settings = new ClientFlurlHttpSettings()
                {
                    HttpClientFactory = factory
                }
            })
            {
                var result = await new FlurlRequest("https://api.test.com").WithClient(client).PostMsgPackAsync(typelessModel);

                // Assert
                Assert.Equal(200, result.StatusCode);
            }
        }

        [Fact]
        public async Task PutMsgPackAsync_TypelessContract()
        {
            // Arrange
            var typelessModel = new TypelessModel
            {
                Id = Guid.NewGuid(),
                Name = "Steve Jobs - PUT",
                BirthDate = new DateTime(1970, 01, 01)
            };
            var factory = new VerifyObjectHttpClientFactory(typelessModel);
            FlurlHttp.Configure(c => c.HttpClientFactory = new VerifyObjectHttpClientFactory(typelessModel));

            // Act
            using (var client = new FlurlClient()
            {
                Settings = new ClientFlurlHttpSettings()
                {
                    HttpClientFactory = factory
                }
            })
            {
                var result = await new FlurlRequest("https://api.test.com").WithClient(client).PutMsgPackAsync(typelessModel);

                // Assert
                Assert.Equal(200, result.StatusCode);
            }
        }

        [Fact]
        public async Task SendMsgPackAsync_TypelessContract()
        {
            // Arrange
            var typelessModel = new TypelessModel
            {
                Id = Guid.NewGuid(),
                Name = "Steve Jobs - PUT",
                BirthDate = new DateTime(1970, 01, 01)
            };
            var factory = new TypelessModelHttpClientFactory(typelessModel);

            // Act
            using (var client = new FlurlClient()
            {
                Settings = new ClientFlurlHttpSettings()
                {
                    HttpClientFactory = factory
                }
            })
            {
                var result = await new FlurlRequest("https://api.test.com").WithClient(client).SendMsgPackAsync(HttpMethod.Get, null).ReceiveMsgPack<TypelessModel>();

                // Assert
                Assert.Equal(typelessModel.Id, result.Id);
                Assert.Equal(typelessModel.BirthDate, result.BirthDate);
                Assert.Equal(typelessModel.Name, result.Name);
            }
        }
    }
}