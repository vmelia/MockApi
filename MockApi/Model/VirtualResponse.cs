namespace MockApi.Model
{
    public class VirtualResponse
    {
        public int StatusCode { get; set; }
        public string HttpMethod { get; set; }
        public object ResponseBody { get; set; }
    }
}