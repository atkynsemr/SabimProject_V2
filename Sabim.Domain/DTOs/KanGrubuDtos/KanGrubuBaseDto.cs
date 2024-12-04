namespace Sabim.Domain.DTOs.KanGrubuDtos
{
    public abstract record KanGrubuBaseDto
    {
        public string? KanGrubuAdi { get; init; }
        public short DurumId  { get; init; }
        public string? DurumAdi { get; init; }
    }
}
