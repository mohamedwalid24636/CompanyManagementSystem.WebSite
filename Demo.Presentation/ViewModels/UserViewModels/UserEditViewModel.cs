namespace Demo.Presentation.ViewModels.UserViewModels
{
    public class UserEditViewModel
    {
        public string FName { get; set; }
        public string? LName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Role { get; set; }
        public List<string> Roles { get; set; } = new();
    }
}
