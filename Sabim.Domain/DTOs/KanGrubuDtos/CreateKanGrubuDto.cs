namespace Sabim.Domain.DTOs.KanGrubuDtos
{
    public record CreateKanGrubuDto:KanGrubuBaseDto
    {
        public short? OlusturanPersonelId { get; set; }
        public DateTime? OlusturulmaTarihi { get; set; }
    }
}
