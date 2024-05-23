namespace CarInfoManagement.Models.AuthendicationModel
{
    public class Status
    {
        public int statusCode { get; set; }
        public string? message { get; set; }
        public string? role { get; set; }
        public string? token { get ; set; }
    }
}
