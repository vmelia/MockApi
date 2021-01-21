using Microsoft.AspNetCore.Http;
using MockApi.Contracts;
using MockApi.Model;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace MockApi.Services
{
    public class HttpHelper : IHttpHelper
    {
        public bool IsVirtualHttpMethod(string method)
        {
            return method.ToUpper() == "PUT";
        }

        public async Task<VirtualResponse> GetResponse(Stream stream)
        {
            var body = await ReadFromStream(stream);
            return JsonSerializer.Deserialize<VirtualResponse>(body);
        }

        public async Task WriteResponse(HttpContext context, int statusCode, string body)
        {
            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsync(body);
        }

        private async Task<string> ReadFromStream(Stream stream)
        {
            if (stream.CanSeek)
                stream.Position = 0;

            return await new StreamReader(stream).ReadToEndAsync();
        }
    }
}