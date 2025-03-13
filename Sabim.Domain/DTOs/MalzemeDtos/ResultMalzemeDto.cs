namespace Sabim.Domain.DTOs.MalzemeDtos
{
    public record ResultMalzemeDto : MalzemeBaseDto
    {
        public short MalzemeID { get; init; }
        public string TurAdi { get; init; }
        public string MarkaAdi { get; init; }
        public string ModelAdi { get; init; }
        public string MalzemeCinsiAdi { get; init; }
        public bool Selected { get; init; }
    }
}
