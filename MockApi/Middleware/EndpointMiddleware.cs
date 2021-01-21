using Microsoft.AspNetCore.Http;
using MockApi.Contracts;
using System.Threading.Tasks;

namespace MockApi.Middleware
{
    public class EndpointMiddleware
    {
        private readonly IResponseCache _responseCache;
        private readonly IHttpHelper _httpHelper;

        public EndpointMiddleware(RequestDelegate _, IResponseCache responseCache, IHttpHelper httpHelper)
        {
            _responseCache = responseCache;
            _httpHelper = httpHelper;
        }

        public async Task Invoke(HttpContext context)
        {
            var key = _responseCache.CalculateKey(context.Request.Method, context.Request.Path);
            if (!_responseCache.ContainsResponse(key))
            {
                await _httpHelper.WriteResponse(context, StatusCodes.Status404NotFound, $"Cannot find response for: {key}");
                return;
            }

            var response = _responseCache.GetResponse(key);
            await _httpHelper.WriteResponse(context, response.StatusCode, response);
        }
    }
}