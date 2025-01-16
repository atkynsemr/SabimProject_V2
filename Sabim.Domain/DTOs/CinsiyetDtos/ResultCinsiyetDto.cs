namespace Sabim.Domain.DTOs.CinsiyetDtos
{
    public record ResultCinsiyetDto : CinsiyetBaseDto
    {
        public short CinsiyetID { get; init; }
        public bool Selected { get; init; }
    }
}
