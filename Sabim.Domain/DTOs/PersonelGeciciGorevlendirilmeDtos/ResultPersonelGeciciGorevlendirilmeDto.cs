namespace Sabim.Domain.DTOs.PersonelGeciciGorevlendirilmeDtos
{
    public record ResultPersonelGeciciGorevlendirilmeDto : PersonelGeciciGorevlendirilmeBaseDto
    {
        public short PersonelGeciciGorevlendirilmeID { get; init; }
        public string KurumAdi { get; init; }
        public string GorevlendirilmeTipiAciklama { get; init; }
        public bool Selected { get; init; }
    }
}
