namespace Sabim.Domain.DTOs.PersonelGorevlendirilmeDtos
{
    public record ResultPersonelGorevlendirilmeDto : PersonelGorevlendirilmeBaseDto
    {
        public short PersonelGorevlendirilmeID { get; init; }
        public bool Selected { get; init; }
    }
}
