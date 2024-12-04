namespace Sabim.Domain.DTOs.KurumDtos
{
    public record ResultKurumWithPersonelCountDto : KurumBaseDto
    {
        public short KurumID { get; init; }
        public ushort PersonelSayisi { get; init; }
    }
}
