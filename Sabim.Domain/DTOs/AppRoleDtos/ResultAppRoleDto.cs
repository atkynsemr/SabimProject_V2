namespace Sabim.Domain.DTOs.AppRoleDtos
{
    public record ResultAppRoleDto :AppRoleBaseDto
    {
        public int Id { get; init; }
        public bool Selected { get; init; }
    }
}
