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
        private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions { WriteIndented = true };

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

        public async Task WriteResponse(HttpContext context, int statusCode, VirtualResponse response)
        {
            var body = JsonSerializer.Serialize(response.ResponseBody, _jsonOptions);
            await WriteResponse(context, statusCode, body);
        }

        private static async Task<string> ReadFromStream(Stream stream)
        {
            if (stream.CanSeek)
                stream.Position = 0;

            return await new StreamReader(stream).ReadToEndAsync();
        }
    }
}