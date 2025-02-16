namespace Sabim.Domain.DTOs.PersonelUnvanGecmisiDtos
{
    public record UpdatePersonelUnvanGecmisiDto : PersonelUnvanGecmisiBaseDto
    {
        public short PersonelUnvanGecmisiID { get; init; }
        public short? GuncelleyenPersonelId { get; set; }
        public DateTime? GuncellenmeTarihi { get; set; }
    }
}
