namespace Sabim.Domain.DTOs.KurumTipiDtos
{
    public record ResultKurumTipiWithKurumCountDto:KurumTipiBaseDto
    {
        public short KurumTipiID { get; init; }
        public ushort KurumSayisi { get; init; }
    }
}
