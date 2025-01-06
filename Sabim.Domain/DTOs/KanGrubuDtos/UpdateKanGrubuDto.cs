namespace Sabim.Domain.DTOs.KanGrubuDtos
{
    public record UpdateKanGrubuDto:KanGrubuBaseDto
    {
        public short KanGrubuID { get; init; }
        public short? GuncelleyenPersonelId { get; set; }
        public DateTime? GuncellenmeTarihi { get; set; }
    }
}
