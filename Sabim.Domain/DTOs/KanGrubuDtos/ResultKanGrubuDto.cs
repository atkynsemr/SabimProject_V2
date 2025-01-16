namespace Sabim.Domain.DTOs.KanGrubuDtos
{
    public record ResultKanGrubuDto :KanGrubuBaseDto
    {
        public short KanGrubuID { get; init; }
        public bool Selected { get; init; }
    }
}
