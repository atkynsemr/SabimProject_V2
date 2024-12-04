namespace Sabim.Domain.DTOs.SehirDtos
{
    public record UpdateSehirDto:SehirBaseDto
    {
        public short SehirID { get; init; }
    }
}
