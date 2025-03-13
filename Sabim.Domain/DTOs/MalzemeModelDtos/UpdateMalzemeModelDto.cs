namespace Sabim.Domain.DTOs.MalzemeModelDtos
{
    public record UpdateMalzemeModelDto : MalzemeModelBaseDto
    {
        public byte MalzemeModelID { get; init; }
        public short? GuncelleyenPersonelId { get; set; }
        public DateTime? GuncellenmeTarihi { get; set; }
    }
}
