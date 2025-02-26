namespace Sabim.Domain.DTOs.GorevlendirilmeTipiDtos
{
    public record ResultGorevlendirilmeTipiDto : GorevlendirilmeTipiBaseDto
    {
        public short GorevlendirilmeTipiID { get; init; }
        public bool Selected { get; init; }
    }
}
