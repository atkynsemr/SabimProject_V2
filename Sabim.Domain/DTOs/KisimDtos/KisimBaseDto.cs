namespace Sabim.Domain.DTOs.KisimDtos
{
    public abstract record KisimBaseDto
    {
        public string? KisimAdi { get; init; }
        public short BirimId { get; init; }
        public string? BirimAdi { get; set; }
        public short KabinetBazliBolumId { get; init; }
        public string? KabinetBazliBolumAdi { get; init; }
        public string? Aciklama { get; init; }  
        public string? DahiliTelefon { get; init; }
        public short DurumId { get; init; }
        public string? DurumAdi { get; init; }
    }
}
