namespace Sabim.Domain.DTOs.CalismaDurumuDtos
{
    public record UpdateCalismaDurumuDto :CalismaDurumuBaseDto
    {
        public short CalismaDurumuID { get; init; }
    }
}
