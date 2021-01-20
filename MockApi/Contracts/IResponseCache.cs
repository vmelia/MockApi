using MockApi.Model;

namespace MockApi.Contracts
{
    public interface IResponseCache
    {
        void SetResponse(string methodPlusPath, VirtualResponse response);
        bool ContainsResponse(string methodPlusPath);
        VirtualResponse GetResponse(string methodPlusPath);
    }
}