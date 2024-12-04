namespace Sabim.Domain.DTOs.CinsiyetDtos
{
    public record ResultCinsiyetWithPersonelCountDto :CinsiyetBaseDto
    {
        public short CinsiyetID { get; init; }
        public ushort PersonelSayisi { get; init; }
    }
}
