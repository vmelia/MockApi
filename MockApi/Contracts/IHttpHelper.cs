using Microsoft.AspNetCore.Http;
using MockApi.Model;
using System.IO;
using System.Threading.Tasks;

namespace MockApi.Contracts
{
    public interface IHttpHelper
    {
        bool IsVirtualHttpMethod(string method);
        Task<VirtualResponse> GetResponse(Stream stream);
        Task WriteResponse(HttpContext context, int statusCode, string body);
    }
}