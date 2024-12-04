namespace Sabim.Domain.DTOs.AppRoleDtos
{
    public record ResultAppRoleWithPersonelCountDto : AppRoleBaseDto
    {
        public int Id { get; init; }
        public ushort PersonelSayisi { get; init; }
    }
}
