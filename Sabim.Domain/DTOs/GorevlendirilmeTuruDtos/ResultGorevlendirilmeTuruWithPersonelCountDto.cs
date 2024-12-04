namespace Sabim.Domain.DTOs.GorevlendirilmeTuruDtos
{
    public record ResultGorevlendirilmeTuruWithPersonelCountDto: GorevlendirilmeTuruBaseDto
    {
        public short GorevlendirilmeTuruID { get; init; }
        public ushort PersonelSayisi { get; init; }
    }
}
