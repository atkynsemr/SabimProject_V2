namespace Sabim.Domain.DTOs.PersonelUnvanGecmisiDtos
{
    public abstract record PersonelUnvanGecmisiBaseDto
    {
        public short PersonelId { get; init; }
        public short UnvanId { get; init; }
        public string? UnvanAdi { get; init; }
        public DateTime UnvanaSahipOlduguTarih { get; init; } = DateTime.Now;
        public DateTime? UnvanDegisimTarihi { get; init; }
        public short DurumId { get; init; }
        public string? DurumAdi { get; init; }
    }
}
