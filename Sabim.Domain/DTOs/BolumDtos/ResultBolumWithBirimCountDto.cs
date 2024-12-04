namespace Sabim.Domain.DTOs.BolumDtos
{
    public record ResultBolumWithBirimCountDto : BolumBaseDto
    {
        public short BolumID { get; init; }
        public ushort BirimSayisi { get; init; }
    }
}
