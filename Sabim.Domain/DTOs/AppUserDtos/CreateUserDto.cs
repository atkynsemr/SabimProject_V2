namespace Sabim.Domain.DTOs.AppUserDtos
{
    public record CreateUserDto : AppUserBaseDto
    {
        public int? Id { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public short? OlusturanPersonelId { get; set; }
        public DateTime? OlusturulmaTarihi { get; set; }
    }
}
