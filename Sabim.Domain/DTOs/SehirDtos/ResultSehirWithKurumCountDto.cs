namespace Sabim.Domain.DTOs.SehirDtos
{
    public record ResultSehirWithKurumCountDto :SehirBaseDto
    {
        public short SehirID { get; init; }
        public ushort KurumSayisi { get; init; }
    }
}
