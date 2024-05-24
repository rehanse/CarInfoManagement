using CarInfoManagement.Utility;

namespace CarInfoManagement.Extensions
{
    public static class SDApiUrlExtensions
    {
        //Get static value for API end points from the Appsettings.json file
        public static void AddSDApiUri(this WebApplicationBuilder builer)
        {
            SD.CarDetailsAPIBase = builer.Configuration["ApiUrls:CarInfoDetailsApiEndPoint"];
            SD.CarDetailsUserAPIBase = builer.Configuration["ApiUrls:UserCarInfoDetailsApiEndPoint"];
            SD.Api_id = builer.Configuration["ApiSettings:Api_id"];
        }
    }
}
