namespace Sabim.Domain.DTOs.SidebarMenuDtos
{
    public abstract record SidebarMenuBaseDto
    {
        public string? SidebarMenuAdi { get; init; }
        public short DurumId { get; init; }
        public string? DurumAdi { get; init; }
    }
}
