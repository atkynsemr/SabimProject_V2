namespace Sabim.Domain.DTOs.KadroTuruDtos
{
    public record ResultKadroTuruWithPersonelCountDto : KadroTuruBaseDto
    {
        public short KadroTuruID { get; init; }
        public ushort PersonelSayisi { get; init; }
    }
}
