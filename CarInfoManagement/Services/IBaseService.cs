using CarInfoManagement.Models;
using CarInfoManagement.Models.API;

namespace CarInfoBFF.Services
{
    public interface IBaseService
    {
        Task<string?> SendAsync(RequestDTO requestDto);
       
    }
}
