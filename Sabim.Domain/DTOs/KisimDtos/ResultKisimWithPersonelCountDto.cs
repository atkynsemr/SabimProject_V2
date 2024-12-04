namespace Sabim.Domain.DTOs.KisimDtos
{
    public record ResultKisimWithPersonelCountDto :KisimBaseDto
    {
        public short KisimID { get; init; }
        public ushort PersonelSayisi { get; init; }
    }
}
