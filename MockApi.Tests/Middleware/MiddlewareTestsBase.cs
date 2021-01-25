using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MockApi.Contracts;
using MockApi.Model;
using Moq;

namespace MockApi.Tests.Middleware
{
    public class MiddlewareTestsBase
    {
        protected readonly Mock<IResponseCache> MockResponseCache;
        protected readonly Mock<IHttpHelper> MockHttpHelper;

        protected readonly HttpContext Context;
        protected readonly VirtualResponse ExpectedResponse;

        protected MiddlewareTestsBase()
        {
            MockResponseCache = new Mock<IResponseCache>();
            MockHttpHelper = new Mock<IHttpHelper>();

            Context = new DefaultHttpContext();
            ExpectedResponse = new VirtualResponse
            {
                StatusCode = 999,
                HttpMethod = "GET",
                ResponseBody = "response-body"
            };

            MockResponseCache.Setup(x => x.CalculateKey(
                It.IsAny<string>(), It.IsAny<string>())).Returns("key");
            MockResponseCache.Setup(x => x.GetResponse("key")).Returns(ExpectedResponse);

            MockHttpHelper.Setup(x => x.GetResponse(It.IsAny<Stream>())).Returns(Task.FromResult(ExpectedResponse));
        }
    }
}