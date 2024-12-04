namespace Sabim.Entities.Models
{
    public class JobTitle:BaseEntity
    {
        public int JobTitleID { get; set; }
        public string TitleName { get; set; }
        public int PriorityOrder { get; set; }

    }
}
