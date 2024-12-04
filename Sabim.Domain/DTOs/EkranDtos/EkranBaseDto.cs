namespace Sabim.Domain.DTOs.EkranDtos
{
    public abstract record EkranBaseDto
    {
        public string? EkranAdi { get; init; }
        public bool Varsayilan { get; init; }
        public short SidebarMenuId { get; init; }
        public string? SidebarMenuAdi { get; init; }
        public short DurumId { get; init; }
        public string? DurumAdi { get; init; }
    }
}
