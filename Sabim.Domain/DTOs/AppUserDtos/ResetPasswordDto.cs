namespace Sabim.Domain.DTOs.AppUserDtos
{
    public record ResetPasswordDto
    {
        public int Id { get; init; }
        public string Email { get; init; } = string.Empty;
        public string recoveryCode { get; init; } = string.Empty;
        public string NewPassword { get; init; }
        public string ConfirmPassword { get; init; }
    }
}
