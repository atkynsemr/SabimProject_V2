namespace Sabim.Domain.DTOs.PersonelGeciciGorevlendirilmeDtos
{
    public record UpdatePersonelGeciciGorevlendirilmeDto : PersonelGeciciGorevlendirilmeBaseDto
    {
        public short PersonelGeciciGorevlendirilmeID { get; init; }
        public short? GuncelleyenPersonelId { get; set; }
        public DateTime? GuncellenmeTarihi { get; set; }
    }
}
