namespace Sabim.Domain.DTOs.CalismaDurumuDtos
{
    public record ResultCalismaDurumuWithPersonelCountDto : CalismaDurumuBaseDto
    {
        public short CalismaDurumuID { get; init; }
        public ushort PersonelSayisi { get; init; }
    }
}
