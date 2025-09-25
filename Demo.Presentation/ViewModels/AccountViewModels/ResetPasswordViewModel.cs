using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;

namespace Demo.Presentation.ViewModels.AccountViewModels
{
    public class ResetPasswordViewModel
    {
        [Required (ErrorMessage = "Password Is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password")]                                              //U Can Write Two Data Annotation Inside The Same [ , ]
        [Required(ErrorMessage = "Confirm Password Is Required")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
