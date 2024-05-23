using CarInfoBFF.Services;
using CarInfoManagement.Models.API;
using CarInfoManagement.Models;
using CarInfoManagement.Models.AuthendicationModel;
using CarInfoManagement.Models.MappingModel;
using CarInfoManagement.Utility;
using Newtonsoft.Json;

namespace CarInfoBFF.Identity
{
    public class IdentityServices:IIdentityService
    {
        private readonly IBaseService _baseService;

        public IdentityServices(IBaseService baseService)
        {
            this._baseService = baseService;
        }
        public async Task<Status> LoginAsync(LoginModel model)
        {
            string url = string.Empty;
            if(model.role == UsersRole.Admin)
            {
                url = $"{SD.CarDetailsAPIBase}/UserAuthentication/LogIn";
            }
            else
            {
               url = $"{SD.CarDetailsUserAPIBase}/UserAuthentication/UserLogIn";
            }
            var requestDTO = new RequestDTO
            {
                ApiType = SD.ApiType.POST,
                Url = url,
                Data = model
            };
            var respone = await _baseService.SendAsync(requestDTO);
            Status status = await DeserializeResponse<Status>(respone);
            return status;
        }

        public async Task<Status> LogoutAsync()
        {
            string url = $"{SD.CarDetailsAPIBase}/UserAuthentication/logOff";
            var requestDTO = new RequestDTO
            {
                ApiType = SD.ApiType.GET,
                Url = url
            };
            var respone = await _baseService.SendAsync(requestDTO);
            Status status = await DeserializeResponse<Status>(respone);
            return status;
        }

        public async Task<Status> RegisterAsync(RegistrationModel model)
        {
            string url = string.Empty;
            if (model.Role == UsersRole.Admin)
            {
                url = $"{SD.CarDetailsAPIBase}/UserAuthentication/Register";
            }
            else
            {
                url = $"{SD.CarDetailsUserAPIBase}/UserAuthentication/UserRegister";
            }
            var requestDTO = new RequestDTO
            {
                ApiType = SD.ApiType.POST,
                Url = url,
                Data = model
            };
            var respone = await _baseService.SendAsync(requestDTO);
            Status status = await DeserializeResponse<Status>(respone);
            return status;
        }
        private async Task<T?> DeserializeResponse<T>(string response)
        {
            return await Task.Run(() => JsonConvert.DeserializeObject<T>(response));
        }
    }
}
