using Microsoft.AspNetCore.Identity;

namespace CarInfoManagement.Models.Exception
{
    public class ExceptionDTO
    {
        public string? api_id { get; set; }
        public int? reponse_code { get; set; }
        public string? response_message { get; set; }
        public string? severity { get; set; }
        public int unique_logId { get; set; }
        public DateTime created_datetime { get; set; } = DateTime.Now;
    }
}
