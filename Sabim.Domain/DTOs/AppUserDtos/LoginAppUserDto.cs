namespace Sabim.Domain.DTOs.AppUserDtos
{
    public record LoginAppUserDto
    {
        public string UserName { get; init; }
        public string Password { get; init; }
        public bool RememberMe { get; init; }
        private string? _ReturnUrl;
        public string ReturnUrl
        {
            get
            {
                if (_ReturnUrl is null)
                    return "/";
                else
                    return _ReturnUrl;
            }
            set
            {
                _ReturnUrl = value;
            }
        }
        //public string Email { get; init; } = string.Empty;
        //public short DurumId { get; init; }
        //public string? DurumAdi { get; init; }
    }
}
