namespace Sabim.Domain.DTOs.BirimDtos
{
    public record ResultBirimWithKisimCount: BirimBaseDto
    {
        public short BirimID { get; init; }
        public ushort KisimSayisi { get; init; }
    }
}
