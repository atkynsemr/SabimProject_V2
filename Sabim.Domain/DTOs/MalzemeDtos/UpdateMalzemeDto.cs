namespace Sabim.Domain.DTOs.MalzemeDtos
{
    public record UpdateMalzemeDto : MalzemeBaseDto
    {
        public short MalzemeID { get; init; }
        public short? GuncelleyenPersonelId { get; set; }
        public DateTime? GuncellenmeTarihi { get; set; }
    }

}
