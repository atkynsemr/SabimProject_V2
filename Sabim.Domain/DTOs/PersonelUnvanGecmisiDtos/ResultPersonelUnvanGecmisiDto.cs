namespace Sabim.Domain.DTOs.PersonelUnvanGecmisiDtos
{
    public record  ResultPersonelUnvanGecmisiDto : PersonelUnvanGecmisiBaseDto
    {
        public short PersonelUnvanGecmisiID { get; init; }
        public short? OlusturanPersonelId { get; init; }
        public DateTime? OlusturulmaTarihi { get; init; }
    }
}
