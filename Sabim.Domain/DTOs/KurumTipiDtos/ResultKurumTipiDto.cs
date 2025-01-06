namespace Sabim.Domain.DTOs.KurumTipiDtos
{
    public record ResultKurumTipiDto:KurumTipiBaseDto
    {
        public short KurumTipiID { get; init; }
        public bool Selected { get; init; }
    }
}
