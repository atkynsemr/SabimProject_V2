namespace Sabim.Domain.DTOs.AppUserDtos
{
    public abstract record AppUserBaseDto
    {
        public int RoleId { get; init; }
        public string? Name { get; init; }
        public string? UserName { get; init; }
        public string? Email { get; init; }
        public short PersonelId { get; set; }
        public string? PersonelAdSoyad { get; init; }
        public short DurumId { get; init; }
        public string? DurumAdi { get; init; }
    }
}
