using System.Collections.Generic;
using MockApi.Contracts;
using MockApi.Model;

namespace MockApi.Services
{
    public class InMemoryResponseCache : IResponseCache
    {
        private readonly IDictionary<string, VirtualResponse> _cache = new Dictionary<string, VirtualResponse>();

        public bool IsVirtualHttpMethod(string method)
        {
            return method.ToUpper() == "PUT";
        }
        
        public string CalculateKey(string method, string path)
        {
            return $"{method}{path}";
        }

        public void SetResponse(string key, VirtualResponse response)
        {
            _cache[key] = response;
        }

        public bool ContainsResponse(string key)
        {
            return _cache.ContainsKey(key);
        }

        public VirtualResponse GetResponse(string key)
        {
            return _cache[key];
        }
    }
}