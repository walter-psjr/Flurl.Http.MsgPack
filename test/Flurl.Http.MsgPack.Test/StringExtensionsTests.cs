using Flurl.Http.MsgPack.Test.Models;
using Flurl.Http.Testing;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Flurl.Http.MsgPack.Test
{
    [ExcludeFromCodeCoverage]
    public class StringExtensionsTests
    {
        private readonly HttpTest _httpTest;

        public StringExtensionsTests()
        {
            _httpTest = new HttpTest();
        }

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

            _httpTest.RespondWithMsgPack<TypelessModel>(typelessModel);

            // Act
            var result = await "https://api.test.com".GetMsgPackAsync<TypelessModel>();

            // Assert
            Assert.Equal(typelessModel.Id, result.Id);
            Assert.Equal(typelessModel.BirthDate, result.BirthDate);
            Assert.Equal(typelessModel.Name, result.Name);
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
            _httpTest.RespondWithMsgPack(typelessModel);

            // Act
            var result = await "https://api.test.com".PostMsgPackAsync(typelessModel);

            // Assert
            Assert.Equal(200, result.StatusCode);
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

            // Act
            var result = await "https://api.test.com".PutMsgPackAsync(typelessModel);

            // Assert
            Assert.Equal(200, result.StatusCode);
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

            _httpTest.RespondWithMsgPack(typelessModel);

            // Act
            var result = await "https://api.test.com".SendMsgPackAsync(HttpMethod.Get, null).ReceiveMsgPack<TypelessModel>();

            // Assert
            Assert.Equal(typelessModel.Id, result.Id);
            Assert.Equal(typelessModel.BirthDate, result.BirthDate);
            Assert.Equal(typelessModel.Name, result.Name);
        }
    }
}