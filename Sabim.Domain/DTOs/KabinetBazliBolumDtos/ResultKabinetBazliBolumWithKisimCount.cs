namespace Sabim.Domain.DTOs.KabinetBazliBolumDtos
{
    public record ResultKabinetBazliBolumWithKisimCount:KabinetBazliBolumBaseDto
    {
        public short KabinetBazliBolumID { get; init; }
        public ushort KisimSayisi { get; init; }
    }
}
