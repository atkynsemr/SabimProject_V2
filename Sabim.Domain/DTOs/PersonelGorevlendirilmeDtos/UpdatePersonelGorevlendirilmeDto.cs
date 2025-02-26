namespace Sabim.Domain.DTOs.PersonelGorevlendirilmeDtos
{
    public record UpdatePersonelGorevlendirilmeDto : PersonelGorevlendirilmeBaseDto
    {
        public short PersonelGorevlendirilmeID { get; init; }
        public short? GuncelleyenPersonelId { get; set; }
        public DateTime? GuncellenmeTarihi { get; set; }
    }
}
