using System.ComponentModel.DataAnnotations;

namespace PlantShop.FormValidations
{
    public class SignInForm
    {
        [Required(ErrorMessage = "Phone is required")]
        [RegularExpression("^[0][3|5|7|8|9][0-9]{8}$", ErrorMessage = "Invalid phone format")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}