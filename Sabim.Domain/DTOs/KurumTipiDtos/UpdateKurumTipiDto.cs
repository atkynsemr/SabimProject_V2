namespace Sabim.Domain.DTOs.KurumTipiDtos
{
    public record UpdateKurumTipiDto:KurumTipiBaseDto
    {
        public short KurumTipiID { get; init; }
    }
}
