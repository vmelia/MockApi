﻿using System.Text.Json;
using Microsoft.AspNetCore.Http;
using MockApi.Contracts;
using System.Threading.Tasks;

namespace MockApi.Middleware
{
    public class EndpointMiddleware
    {
        private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions { WriteIndented = true };
        private readonly IResponseCache _responseCache;

        public EndpointMiddleware(RequestDelegate _, IResponseCache responseCache)
        {
            _responseCache = responseCache;
        }

        public async Task Invoke(HttpContext context)
        {
            var key = _responseCache.CalculateKey(context.Request.Method, context.Request.Path);
            if (!_responseCache.ContainsResponse(key))
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                await context.Response.WriteAsync($"Cannot find response for: {key}");
                return;
            }

            var virtualResponse = _responseCache.GetResponse(key);
            context.Response.StatusCode = virtualResponse.StatusCode;

            var responseBody = JsonSerializer.Serialize(virtualResponse.ResponseBody, _jsonOptions);
            await context.Response.WriteAsync(responseBody);
        }
    }
}