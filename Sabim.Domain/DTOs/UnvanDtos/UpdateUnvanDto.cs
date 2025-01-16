namespace Sabim.Domain.DTOs.UnvanDtos
{
    public record UpdateUnvanDto:UnvanBaseDto
    {
        public short UnvanID { get; init; }
        public short SeciliUnvanId { get; set; }
        public short? GuncelleyenPersonelId { get; set; }
        public DateTime? GuncellenmeTarihi { get; set; }
        public byte OncelikDurumu { get; set; }
    }
}
