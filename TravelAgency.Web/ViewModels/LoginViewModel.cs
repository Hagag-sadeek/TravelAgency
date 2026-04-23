using System.ComponentModel.DataAnnotations;

namespace TravelAgency.Web.Web.ViewModels
{
    public class LoginViewModel
    {
       

        [Required(ErrorMessage = "??? ???????? ?????")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "???? ?????? ??????")]
        public string Password { get; set; }


    }
}
