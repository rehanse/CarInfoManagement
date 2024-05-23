using CarInfoManagement.Models;
using CarInfoManagement.Models.AuthendicationModel;

namespace CarInfoManagement.Services.CarInfoDetails
{
    public interface ICarInfoDetailsServices
    {
        Task<List<CarViewModel>> GetCarInfoDetails(string accessToken,string term,string role);
        Task<Status> Add(CarViewModel model);
        Task<Status> Update(int Id,CarViewModel model);
        Task<CarViewModel> GetById(int id, string accessToken,string role);
        Task<Status> Delete(int id, string accessToken);
        Task<CarReferenceViewModel> GetListOfCarReference(string? token,string role);
    }
}
