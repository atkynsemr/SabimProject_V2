namespace Sabim.Domain.DTOs.PersonelAyrilisNedenleriDtos
{
    public record ResultPersonelAyrilisNedenleriDto : PersonelAyrilisNedenleriBaseDto
    {
        public short PersonelAyrilisNedenleriID { get; init; }
        public bool Selected { get; init; }
    }
}
