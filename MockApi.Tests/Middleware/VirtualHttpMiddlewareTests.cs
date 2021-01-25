using System.Threading.Tasks;
using MockApi.Middleware;
using MockApi.Model;
using Moq;
using Xunit;

namespace MockApi.Tests.Middleware
{
    public class VirtualHttpMiddlewareTests : MiddlewareTestsBase
    {
        private readonly VirtualHttpMiddleware _middleware;
        private bool _nextCalled;

        public VirtualHttpMiddlewareTests()
        {
            _middleware = new VirtualHttpMiddleware(_ =>
            {
                _nextCalled = true;
                return Task.CompletedTask;
            }, MockResponseCache.Object, MockHttpHelper.Object);
        }

        [Fact]
        public async void Invoke_WhenIsVirtualHttpMethod_CachesSubmittedResponse()
        {
            MockResponseCache.Setup(x => x.IsVirtualHttpMethod(It.IsAny<string>())).Returns(true);

            await _middleware.Invoke(Context);

            MockResponseCache.Verify(x => x.SetResponse(
                It.Is<string>(s => s == "key"),
                It.Is<VirtualResponse>(v => v == ExpectedResponse)), Times.Once);
            Assert.False(_nextCalled);
        }

        [Fact]
        public async void Invoke_WhenIsVirtualHttpMethod_WritesCorrectStatusCode()
        {
            MockResponseCache.Setup(x => x.IsVirtualHttpMethod(It.IsAny<string>())).Returns(true);

            await _middleware.Invoke(Context);

            Assert.Equal(200, Context.Response.StatusCode);
            Assert.False(_nextCalled);
        }

        [Fact]
        public async void Invoke_WhenNotVirtualHttpMethod_CallsNext()
        {
            MockResponseCache.Setup(x => x.IsVirtualHttpMethod(It.IsAny<string>())).Returns(false);

            await _middleware.Invoke(Context);

            Assert.True(_nextCalled);
        }
    }
}