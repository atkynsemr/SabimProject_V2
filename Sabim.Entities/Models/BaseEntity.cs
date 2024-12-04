namespace Sabim.Entities.Models
{
    public abstract class BaseEntity
    {
        public int? CreatedById { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedById { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? DeletedById { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int RecordStatusId { get; set; }
        // Navigation properties 
        public Employee CreatedBy { get; set; }  
        public Employee UpdatedBy { get; set; }  
        public Employee DeletedBy { get; set; }  
        public RecordStatus RecordStatus { get; set; }
    }
}
