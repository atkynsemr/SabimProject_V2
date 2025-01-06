namespace Sabim.Domain.DTOs.SehirDtos
{
    public record ResultSehirDto:SehirBaseDto
    {
        public short SehirID { get; init; }
        public bool Selected { get; init; }
    }
}
