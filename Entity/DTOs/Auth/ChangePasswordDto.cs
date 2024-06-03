namespace Entity.DTOs.Auth
{
    public class ChangePasswordDto
    {
        public string EmailAddress { get; set; }
        public string currentPassword { get; set; }
        public string newPassword { get; set; }
    }
}
