namespace Sabim.Domain.DTOs.CalismaDurumuDtos
{
    public abstract record CalismaDurumuBaseDto
    {      
        public string? CalismaDurumAdi { get; init; } 
        public bool KurumPersonelListesineDahilMi { get; init; }
        public short DurumId  { get; init; }
        public string? DurumAdi { get; init; }
    }
}

