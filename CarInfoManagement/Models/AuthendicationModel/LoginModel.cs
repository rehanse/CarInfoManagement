using System.ComponentModel.DataAnnotations;

namespace CarInfoManagement.Models.AuthendicationModel
{
    public class LoginModel
    {
        [Required(ErrorMessage = "userName is Required")]
        public string userName { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        public string password { get; set; }
        [Required(ErrorMessage = "Role is Required")]
        public string role { get; set; }
    }
}
