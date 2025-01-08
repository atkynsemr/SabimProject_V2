namespace Sabim.Domain.DTOs.BirimDtos
{
    public record ResultBirimWithKisimCountDto: BirimBaseDto
    {
        public short BirimID { get; init; }
        public ushort KisimSayisi { get; init; }
    }
}
