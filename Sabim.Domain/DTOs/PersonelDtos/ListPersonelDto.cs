namespace Sabim.Domain.DTOs.PersonelDtos
{
    public record ListPersonelDto : PersonelBaseDto
    {
        public short PersonelID { get; init; }
        public bool Selected { get; init; }
    }
}
