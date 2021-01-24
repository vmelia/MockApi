using MockApi.Model;

namespace MockApi.Contracts
{
    public interface IResponseCache
    {
        bool IsVirtualHttpMethod(string method);
        string CalculateKey(string method, string path);

        void SetResponse(string key, VirtualResponse response);
        bool ContainsResponse(string key);
        VirtualResponse GetResponse(string key);
    }
}