using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace SecureLoginWebApp.Models.ManageViewModels
{
    public class DeleteProfileViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string StatusMessage { get; set; }
    }
}
