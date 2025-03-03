namespace Sabim.Domain.DTOs.PersonelGorevlendirilmeDtos
{
    public record ResultPersonelGorevlendirilmeDto : PersonelGorevlendirilmeBaseDto
    {
        public short PersonelGorevlendirilmeID { get; init; }
        public bool Selected { get; init; }
        public short BolumId { get; init; }
        public string? BolumAdi { get; init; }
        public short BirimId { get; init; }
        public string? BirimAdi { get; init; }
        public string? KisimAdi { get; init; }
    }
}
