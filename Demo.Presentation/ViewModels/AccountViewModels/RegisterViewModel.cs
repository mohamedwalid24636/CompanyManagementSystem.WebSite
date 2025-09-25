using System.ComponentModel.DataAnnotations;

namespace Demo.Presentation.ViewModels.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required (ErrorMessage ="FirstName Can Not Be Null")]
        [MaxLength(50)]
        public string FirstName { set; get; } = null!;

        [Required()]
        [MaxLength(50)]
        public string LastName { set; get; }

        [DataType(DataType.EmailAddress)]
        public string Email { set; get; }
        public string UserName { set; get; }

        [DataType(DataType.Password)]
        public string Password { set; get; }

        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { set; get; }
        public bool IsAgree { get; set; }
    }
}
