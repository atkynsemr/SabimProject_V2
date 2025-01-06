namespace Sabim.Domain.DTOs.HelperDtos
{
    public record class AuditTrailDto
    {
        public object Id { get; set; }
        public short? OlusturanPersonelId { get; set; }
        public string? OlusturanAdSoyad { get; set; }
        public DateTime? OlusturulmaTarihi { get; set; }
        public short? GuncelleyenPersonelId { get; set; }
        public string? GuncelleyenAdSoyad { get; set; }
        public DateTime? GuncellenmeTarihi { get; set; }
        public short? SilenPersonelId { get; set; }
        public string? SilenAdSoyad { get; set; }
        public DateTime? SilinmeTarihi { get; set; }
    }
}
