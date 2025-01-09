namespace Sabim.Domain.DTOs.KabinetBazliBolumDtos
{
    public record ResultKabinetBazliBolumWithKisimCountDto : KabinetBazliBolumBaseDto
    {
        public short KabinetBazliBolumID { get; init; }
        public ushort KisimSayisi { get; init; }
    }
}
