using System.Collections.Generic;
using MockApi.Contracts;
using MockApi.Model;

namespace MockApi.Services
{
    public class ResponseCache : IResponseCache
    {
        private readonly IDictionary<string, VirtualResponse> _cache = new Dictionary<string, VirtualResponse>();

        public void SetResponse(string methodPlusPath, VirtualResponse response)
        {
            _cache[methodPlusPath] = response;
        }

        public bool ContainsResponse(string methodPlusPath)
        {
            return _cache.ContainsKey(methodPlusPath);
        }

        public VirtualResponse GetResponse(string methodPlusPath)
        {
            return _cache[methodPlusPath];
        }
    }
}