using System.Net.Mime;
using static CarInfoManagement.Utility.SD;
namespace CarInfoManagement.Models.API
{
    public class RequestDTO
    {
        public ApiType ApiType { get; set; } = ApiType.GET;

       public string? Url { get; set; }
        public object? Data { get; set; }
        public string? AccessToken { get; set; }
        public Utility.SD.ContentType ContentType { get; set; } = Utility.SD.ContentType.Json;
    }
}
