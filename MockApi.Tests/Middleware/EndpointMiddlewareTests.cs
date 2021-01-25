using Microsoft.AspNetCore.Http;
using MockApi.Middleware;
using MockApi.Model;
using Moq;
using Xunit;

namespace MockApi.Tests.Middleware
{
    public class EndpointMiddlewareTests : MiddlewareTestsBase
    {
        private readonly EndpointMiddleware _middleware;

        public EndpointMiddlewareTests()
        {
            _middleware = new EndpointMiddleware(null, MockResponseCache.Object, MockHttpHelper.Object);
        }

        [Fact]
        public async void Invoke_WhenResponseExists_WritesCorrectResponse()
        {
            MockResponseCache.Setup(x => x.ContainsResponse("key")).Returns(true);

            await _middleware.Invoke(Context);

            MockHttpHelper.Verify(x =>
                x.WriteResponse(
                    It.Is<HttpContext>(c => c == Context),
                    It.Is<int>(i => i == ExpectedResponse.StatusCode),
                    It.Is<VirtualResponse>(o => o == ExpectedResponse)), Times.Once);
        }

        [Fact]
        public async void Invoke_WhenResponseNotFound_WritesErrorResponse()
        {
            MockResponseCache.Setup(x => x.ContainsResponse("key")).Returns(false);

            await _middleware.Invoke(Context);

            MockHttpHelper.Verify(x =>
                x.WriteResponse(
                    It.Is<HttpContext>(c => c == Context),
                    It.Is<int>(i => i == 404),
                    It.Is<string>(s => s.Contains("Cannot find response"))), Times.Once);
        }
    }
}