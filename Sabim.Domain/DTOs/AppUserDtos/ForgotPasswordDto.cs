namespace Sabim.Domain.DTOs.AppUserDtos
{
    public record ForgotPasswordDto
    {
        public string Eposta { get; init; }
        public bool EmailSent { get; set; }
    }
}
