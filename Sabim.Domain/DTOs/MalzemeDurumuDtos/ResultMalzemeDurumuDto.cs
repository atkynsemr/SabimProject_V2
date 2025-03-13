namespace Sabim.Domain.DTOs.MalzemeDurumuDtos
{
    public record ResultMalzemeDurumuDto : MalzemeDurumuBaseDto
    {
        public byte MalzemeDurumuID { get; init; }
        public bool Selected { get; init; }
    }
}
