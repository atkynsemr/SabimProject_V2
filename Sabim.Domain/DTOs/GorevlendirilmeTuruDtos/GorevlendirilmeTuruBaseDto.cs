namespace Sabim.Domain.DTOs.GorevlendirilmeTuruDtos
{
    public abstract record GorevlendirilmeTuruBaseDto
    {
        public string? GorevlendirilmeTuruAdi { get; init; }  
        public bool KurumPersonelListesineDahilMi { get; init; }
        public short DurumId  { get; init; }
        public string? DurumAdi { get; init; }
    }
}
