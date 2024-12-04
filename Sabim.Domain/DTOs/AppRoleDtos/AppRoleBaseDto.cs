namespace Sabim.Domain.DTOs.AppRoleDtos
{
    public abstract record AppRoleBaseDto
    {
        public string? Name { get; init; }
        public short DurumId { get; init; }
        public string? DurumAdi { get; init; }
    }
}
