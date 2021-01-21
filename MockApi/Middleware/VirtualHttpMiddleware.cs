using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using MockApi.Contracts;

namespace MockApi.Middleware
{
    public class VirtualHttpMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IResponseCache _responseCache;
        private readonly IHttpHelper _httpHelper;

        public VirtualHttpMiddleware(RequestDelegate next, IResponseCache responseCache, IHttpHelper httpHelper)
        {
            _next = next;
            _responseCache = responseCache;
            _httpHelper = httpHelper;
        }

        public async Task Invoke(HttpContext context)
        {
            if (_httpHelper.IsVirtualHttpMethod(context.Request.Method))
            {
                var response = await _httpHelper.GetResponse(context.Request.Body);
                var key = _responseCache.CalculateKey(response?.HttpMethod, context.Request.Path);

                _responseCache.SetResponse(key, response);
                context.Response.StatusCode = StatusCodes.Status200OK;
                return;
            }

            await _next(context);
        }
    }
}