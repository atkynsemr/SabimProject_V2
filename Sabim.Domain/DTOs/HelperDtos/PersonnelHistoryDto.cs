namespace Sabim.Domain.DTOs.HelperDtos
{
    public record class PersonnelHistoryDto
    {
        public short Id { get; set; }
        public string IslemTuru { get; set; }
        public DateTime? BaslamaTarihi { get; set; }
        public DateTime? BitisTarihi { get; set; }
        public string? Aciklama { get; set; }       
    }
}
