namespace Sabim.Domain.DTOs.CalismaDurumuDtos
{
    public record ResultCalismaDurumuDto :CalismaDurumuBaseDto
    {
        public short CalismaDurumuID { get; init; }
        public bool Selected {  get; init; }
    }
}
