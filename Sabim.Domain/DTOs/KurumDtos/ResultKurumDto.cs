namespace Sabim.Domain.DTOs.KurumDtos
{
    public record ResultKurumDto :KurumBaseDto
    {
        public short KurumID { get; init; }
        public bool Selected { get; init; }
    }
}
