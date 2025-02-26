namespace Sabim.Domain.DTOs.GorevlendirilmeTipiDtos
{
    public abstract record GorevlendirilmeTipiBaseDto
    {
        public string? GorevlendirilmeTipiAciklama { get; init; }  
        public short DurumId  { get; init; }
        public string? DurumAdi { get; init; }
    }
}
