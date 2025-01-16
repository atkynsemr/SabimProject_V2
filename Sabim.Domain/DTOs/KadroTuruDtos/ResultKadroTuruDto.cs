namespace Sabim.Domain.DTOs.KadroTuruDtos
{
    public record class ResultKadroTuruDto : KadroTuruBaseDto
    {
        public short KadroTuruID { get; init; }
        public bool Selected { get; init; }
    }
}
