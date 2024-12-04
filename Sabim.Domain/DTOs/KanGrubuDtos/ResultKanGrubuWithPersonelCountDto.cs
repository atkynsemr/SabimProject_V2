namespace Sabim.Domain.DTOs.KanGrubuDtos
{
    public record ResultKanGrubuWithPersonelCountDto: KanGrubuBaseDto
    {
        public short KanGrubuID { get; init; }
        public ushort PersonelSayisi { get; init; }
    }
}
