using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Sabim.Domain.Entities;
using Sabim.Infrastructure.Identity.CustomErrors;
using Sabim.Infrastructure.Persistence.Context;

namespace Sabim.Infrastructure.Identity.Configurations
{
    public static class IdentityConfiguration
    {
        public static void ConfigureIdentity(IServiceCollection services)
        {
            services.AddIdentity<AppUser, AppRole>(options =>
            {
                // Parola Kuralları
                options.Password.RequireDigit = false; // En az bir rakam zorunlu
                options.Password.RequireLowercase = false; // En az bir küçük harf zorunlu
                options.Password.RequireUppercase = false; // En az bir büyük harf zorunlu
                options.Password.RequireNonAlphanumeric = false; // Özel karakter zorunlu
                options.Password.RequiredLength = 5; // Minimum uzunluk
                options.Password.RequiredUniqueChars = 0; // En az 3 benzersiz karakter
                // Kullanıcı Kuralları
                options.User.RequireUniqueEmail = true; // E-posta benzersiz olmalı
                options.User.AllowedUserNameCharacters = "abcçdefgğhıijklmnoöprsştuüvyzABCÇDEFGĞHIİJKLMNOÖPRSŞTUÜVYZ0123456789 -._";
                // Kilitlenme Kuralları (Kilitlemeyi devre dışı bırak)
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.Zero; // Kilitlenme süresi yok
                options.Lockout.MaxFailedAccessAttempts = int.MaxValue; // Sınırsız giriş hakkı
                options.Lockout.AllowedForNewUsers = false; // Yeni kullanıcılar için kilitlenme devre dışı
                // İki faktörlü doğrulamayı tamamen devre dışı bırak
                options.SignIn.RequireConfirmedAccount = false; // Hesap onayı gereksinimi yok
                options.SignIn.RequireConfirmedEmail = false; // E-posta onayı gereksinimi yok
                options.SignIn.RequireConfirmedPhoneNumber = false; // Telefon onayı gereksinimi yok
            })
            .AddEntityFrameworkStores<SabimDbContext>()
            .AddDefaultTokenProviders()
            .AddErrorDescriber<CustomIdentityErrorDescriber>();
        }
    }
}
