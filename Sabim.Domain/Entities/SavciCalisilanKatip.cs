namespace Sabim.Domain.Entities
{
    public class SavciCalisilanKatip : BaseEntity
    {
        public short SavciCalisilanKatipID { get; set; }  
        public short SavciId { get; set; }  
        public short KatipId { get; set; }  
        public DateTime GorevlendirilmeBaslamaTarihi { get; set; }
        public DateTime? GorevlendirilmeBitisTarihi { get; set; }
        public bool GorevlendirilmeAktifMi { get; set; }
        //  Navigation Properties
        public virtual Personel Savci { get; set; }  
        public virtual Personel Katip { get; set; } 
    }
}
