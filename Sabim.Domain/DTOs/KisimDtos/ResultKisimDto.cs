namespace Sabim.Domain.DTOs.KisimDtos
{
    public record ResultKisimDto : KisimBaseDto
    {
        public short KisimID { get; init; }
        public bool Selected { get; init; }
    }
}
