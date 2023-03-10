using System.ComponentModel.DataAnnotations;

namespace PlantShop.FormValidations
{
    public class RegisterForm
    {
        [Required(ErrorMessage = "First name is required")]
        [StringLength(255, ErrorMessage = "Length must not exceed 255")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name is required")]
        [StringLength(255, ErrorMessage = "Length must not exceed 255")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [RegularExpression("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$", ErrorMessage = "Invalid email format")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Phone is required")]
        [RegularExpression("^[0][3|5|7|8|9][0-9]{8}$", ErrorMessage = "Invalid phone format")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Repeat password is required")]
        [Compare("Password", ErrorMessage ="Passwords must match.")]
        public string RepeatPassword { get; set; }
    }
}