namespace Sabim.Domain.DTOs.PersonelAyrilisDtos
{
    public record  ResultPersonelAyrilisDto : PersonelAyrilisNedenleriBaseDto
    {
        public short PersonelAyrilisID { get; init; }
        public bool Selected { get; init; }
    }
}
