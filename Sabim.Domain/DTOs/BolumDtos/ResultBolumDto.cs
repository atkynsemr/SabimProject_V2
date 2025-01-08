namespace Sabim.Domain.DTOs.BolumDtos
{
    public record ResultBolumDto : BolumBaseDto
    {
        public short BolumID { get; init; }
        public bool Selected { get; init; }
    }
}
