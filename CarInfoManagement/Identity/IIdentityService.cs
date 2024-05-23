using CarInfoManagement.Models.AuthendicationModel;

namespace CarInfoBFF.Identity
{
    public interface IIdentityService
    {
        Task<Status> LoginAsync(LoginModel model);
        Task<Status> LogoutAsync();
        Task<Status> RegisterAsync(RegistrationModel model);
    }
}
