namespace Sabim.Domain.DTOs.AppUserDtos
{
    public record ResultAppUserDto :AppUserBaseDto
    {
        public int AppUserId { get; init; }
    }
}
