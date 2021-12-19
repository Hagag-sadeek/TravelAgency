using System.ComponentModel.DataAnnotations;

namespace TravelAgency.ViewModels
{
    public class LoginViewModel
    {
       

        [Required(ErrorMessage = "اسم المستخدم مطلوب")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "كلمه المرور مطلوبه")]
        public string Password { get; set; }


    }
}
