using System.IO;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using MockApi.Contracts;
using MockApi.Model;

namespace MockApi.Middleware
{
    public class VirtualHttpMiddleware
    {
        private const string _virtualToken = @"VirtualHttp";
        private readonly RequestDelegate _next;
        private readonly IResponseCache _responseCache;

        public VirtualHttpMiddleware(RequestDelegate next, IResponseCache responseCache)
        {
            _next = next;
            _responseCache = responseCache;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Method == "PUT")
            {
                if (context.Request.Body.CanSeek)
                    context.Request.Body.Position = 0;

                var body = await new StreamReader(context.Request.Body).ReadToEndAsync();
                var virtualResponse = JsonSerializer.Deserialize<VirtualResponse>(body);
                var methodPlusPath = $"{virtualResponse?.HttpMethod}{context.Request.Path}";

                _responseCache.SetResponse(methodPlusPath, virtualResponse);
                context.Response.StatusCode = 200;
                return;
            }

            await _next(context);
        }
    }
}