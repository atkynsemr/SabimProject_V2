namespace Sabim.Domain.DTOs.UnvanDtos
{
    public record UpdateUnvanDto:UnvanBaseDto
    {
        public short UnvanID { get; init; }
    }
}
