namespace Sabim.Domain.DTOs.MalzemeMarkaDtos
{
    public record ResultMalzemeMarkaDto : MalzemeMarkaBaseDto
    {
        public byte MalzemeMarkaID { get; init; }
        public bool Selected { get; init; }
    }
}
