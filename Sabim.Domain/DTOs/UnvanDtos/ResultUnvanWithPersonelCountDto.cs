namespace Sabim.Domain.DTOs.UnvanDtos
{
    public record ResultUnvanWithPersonelCountDto : UnvanBaseDto
    {
        public short UnvanID { get; init; }
        public ushort PersonelSayisi {  get; init; } 
    }
}
