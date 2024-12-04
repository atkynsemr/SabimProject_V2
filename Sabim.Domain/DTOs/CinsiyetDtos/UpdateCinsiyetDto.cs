namespace Sabim.Domain.DTOs.CinsiyetDtos
{
    public record UpdateCinsiyetDto:CinsiyetBaseDto
    {
        public short CinsiyetID { get; init; }
    }
}
