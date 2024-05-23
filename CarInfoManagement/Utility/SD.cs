namespace CarInfoManagement.Utility
{
    public static class SD
    {


        public static string? CarDetailsAPIBase { get; set; }
        public static string? CarDetailsUserAPIBase { get; set; }
        public static string? Api_id { get; set; }
        public enum ApiType
        {
            GET,
            POST,
            PUT,
            DELETE
        }
        public enum ContentType
        {
            Json
        }






    }
}
