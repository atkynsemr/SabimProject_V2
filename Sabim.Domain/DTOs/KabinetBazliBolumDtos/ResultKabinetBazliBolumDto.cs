namespace Sabim.Domain.DTOs.KabinetBazliBolumDtos
{
    public record ResultKabinetBazliBolumDto : KabinetBazliBolumBaseDto
    {
        public short KabinetBazliBolumID { get; init; }
        public bool Selected { get; init; }
    }
}
