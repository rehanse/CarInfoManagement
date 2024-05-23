using CarInfoBFF.Identity;
using CarInfoBFF.Services;
using CarInfoManagement.Models;
using CarInfoManagement.Models.API;
using CarInfoManagement.Models.MappingModel;
using CarInfoManagement.Utility;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Rendering;
using static CarInfoManagement.Utility.SD;
using CarInfoManagement.Models.AuthendicationModel;
using System.Data;

namespace CarInfoManagement.Services.CarInfoDetails
{
    public class CarInforDetailsServices:ICarInfoDetailsServices
    {
        private readonly IBaseService _baseService;
        //private readonly IUserService _userService;
        private readonly MappingData _mappingData;

        public CarInforDetailsServices(IBaseService baseService,MappingData mappingData)
        {
            this._baseService = baseService;
            this._mappingData = mappingData;
            //this._userService = userService;
        }

        public async Task<Status> Add(CarViewModel model)
        {
            string url = $"{SD.CarDetailsAPIBase}/CarInfo/AddCar";
            var requestDTO = new RequestDTO
            {
                ApiType = SD.ApiType.POST,
                Url = url,
                Data = model,
                AccessToken = model.accessToken
            };
            var respone = await _baseService.SendAsync(requestDTO);
            Status status = await DeserializeResponse<Status>(respone);
            return status;
        }

        public async Task<Status> Delete(int id, string accessToken)
        {
            string url = $"{SD.CarDetailsAPIBase}/CarInfo/DeleteCarById?id=" + id ;
            var requestDTO = new RequestDTO
            {
                ApiType = SD.ApiType.DELETE,
                Url = url,
                AccessToken= accessToken
            };
            var respone = await _baseService.SendAsync(requestDTO);
            Status status = await DeserializeResponse<Status>(respone);
            return status;
        }

        public async Task<CarViewModel> GetById(int id,string accessToken,string role)
        {
            string url = string.Empty;
            if (role == UsersRole.Admin)
            {
                url = $"{SD.CarDetailsAPIBase}/CarInfo/GetCarById?id=" + id;
            }
            else
            {
                url = $"{SD.CarDetailsUserAPIBase}/UserCarInfo/GetCarByIdFromUser?id=" + id;
            }
            var requestDTO = new RequestDTO
            {
                ApiType = SD.ApiType.GET,
                Url = url,
                AccessToken = accessToken
            };
            var respone = await _baseService.SendAsync(requestDTO);
            CarViewModel carViewModel = await DeserializeResponse<CarViewModel>(respone);
            _mappingData.GetReferenceCarData(carViewModel,id);
            return carViewModel;
        }

        public async Task<List<CarViewModel>> GetCarInfoDetails(string accessToken, string term, string role)
        {
            string url = string.Empty;
            if (role == "Admin")
            {
                url = $"{SD.CarDetailsAPIBase}/CarInfo/GetCars";
            }
            else
            {
                url = $"{SD.CarDetailsUserAPIBase}/UserCarInfo/GetCarsFromUser";
            }
            var requestDTO = new RequestDTO
            {
                ApiType = SD.ApiType.GET,
                Url = url,
                AccessToken = accessToken
            };
            var respone = await _baseService.SendAsync(requestDTO);
            List<CarDetails> carDetails = await DeserializeResponse<List<CarDetails>>(respone);
            List<CarViewModel> carViewModel = _mappingData.MapsCarData(carDetails);
            if(!string.IsNullOrEmpty(term))
            {
                carViewModel = carViewModel.Where(a => (a.carName.ToLower().StartsWith(term.ToLower()) || a.manufacturerName.ToLower().StartsWith(term.ToLower()))).ToList();
            }
            return carViewModel;
        }
        public async Task<CarReferenceViewModel> GetListOfCarReference(string? token,string role)
        {
            string url = string.Empty;
            if (role == "Admin")
            {
                url = $"{SD.CarDetailsAPIBase}/CarInfo/CarRefernceList";
            }
            else
            {
                url = $"{SD.CarDetailsUserAPIBase}/UserCarInfo/CarRefernceListFromUser";
            }
            var requestDTO = new RequestDTO
            {
                ApiType = SD.ApiType.GET,
                Url = url,
                AccessToken = token
            };
            var respone = await _baseService.SendAsync(requestDTO);
            CarReferenceViewModel carDetails = await DeserializeResponse<CarReferenceViewModel>(respone);
            //carDetails =  _mappingData.ApplySelectListItem(carDetails);
            return carDetails;
        }

        public async Task<Status> Update(int Id, CarViewModel model)
        {
            string url = $"{SD.CarDetailsAPIBase}/CarInfo/UpdateCarDetails?id="+Id;
            var requestDTO = new RequestDTO
            {
                ApiType = SD.ApiType.PUT,
                Url = url,
                Data = model,
                AccessToken = model.accessToken
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
