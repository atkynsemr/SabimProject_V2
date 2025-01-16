namespace Sabim.Domain.DTOs.GorevlendirilmeTuruDtos
{
    public record ResultGorevlendirilmeTuruDto : GorevlendirilmeTuruBaseDto
    {
        public short GorevlendirilmeTuruID { get; init; }
        public bool Selected { get; init; }
    }
}
