namespace Sabim.Domain.DTOs.MalzemeMarkaDtos
{
    public record UpdateMalzemeMarkaDto : MalzemeMarkaBaseDto
    {
        public byte MalzemeMarkaID { get; init; }
        public short? GuncelleyenPersonelId { get; set; }
        public DateTime? GuncellenmeTarihi { get; set; }
    }
}
