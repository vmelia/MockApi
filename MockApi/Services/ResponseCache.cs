using System.Collections.Generic;
using MockApi.Contracts;
using MockApi.Model;

namespace MockApi.Services
{
    public class ResponseCache : IResponseCache
    {
        private readonly IDictionary<string, VirtualResponse> _cache = new Dictionary<string, VirtualResponse>();

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