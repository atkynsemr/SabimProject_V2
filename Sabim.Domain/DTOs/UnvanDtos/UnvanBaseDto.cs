namespace Sabim.Domain.DTOs.UnvanDtos
{
    public abstract record UnvanBaseDto
    {
        public string? UnvanAdi { get; init; }
        public byte OncelikSirasi { get; init; }
        public short DurumId  { get; init; }
        public string? DurumAdi { get; init; }
    }
}
