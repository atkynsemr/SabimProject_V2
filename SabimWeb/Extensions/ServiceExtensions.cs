using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Sabim.Domain.Entities;
using Sabim.Infrastructure.Helper;
using Sabim.Infrastructure.Identity.Configurations;
using Sabim.Infrastructure.Persistence.Context;
using Sabim.Infrastructure.Persistence.Repository.Contracts;
using Sabim.Infrastructure.Persistence.Repository.Implementations;
using Sabim.Services.Contracts;
using Sabim.Services.Implementations;
using Sabim.Services.Validators.AppUserValidators;
using Sabim.Web.Filters;

namespace Sabim.Web.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SabimDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("sqlDbConnection"),
                 b => b.MigrationsAssembly("Sabim.Infrastructure"));
            });
        }
        public static void ConfigureRegisterRepositories(this IServiceCollection services)
        {
            //BaseRepository
            services.AddScoped<IRepositoryBase<CalismaDurumu>, CalismaDurumuRepository>();
            services.AddScoped<IRepositoryBase<Cinsiyet>, CinsiyetRepository>();
            services.AddScoped<IRepositoryBase<Durum>, DurumRepository>();
            services.AddScoped<IRepositoryBase<GorevlendirilmeTuru>, GorevlendirilmeTuruRepository>();
            services.AddScoped<IRepositoryBase<KadroTuru>, KadroTuruRepository>();
            services.AddScoped<IRepositoryBase<KanGrubu>, KanGrubuRepository>();
            services.AddScoped<IRepositoryBase<Kurum>, KurumRepository>();
            services.AddScoped<IRepositoryBase<KurumTipi>, KurumTipiRepository>();
            services.AddScoped<IRepositoryBase<Personel>, PersonelRepository>();
            services.AddScoped<IRepositoryBase<Sehir>, SehirRepository>();
            services.AddScoped<IRepositoryBase<Unvan>, UnvanRepository>();
            services.AddScoped<IRepositoryBase<Bolum>, BolumRepository>();
            services.AddScoped<IRepositoryBase<Birim>, BirimRepository>();
            services.AddScoped<IRepositoryBase<Kisim>, KisimRepository>();
            services.AddScoped<IRepositoryBase<KabinetBazliBolum>, KabinetBazliBolumRepository>();
            services.AddScoped<IRepositoryBase<GorevlendirilmeTipi>, GorevlendirilmeTipiRepository>();
            services.AddScoped<IRepositoryBase<PersonelGorevlendirilme>, PersonelGorevlendirilmeRepository>();
            services.AddScoped<IRepositoryBase<AppRole>, AppRoleRepository>();
            services.AddScoped<IRepositoryBase<AppUser>, AppUserRepository>();
            services.AddScoped<IRepositoryBase<SidebarMenu>, SidebarMenuRepository>();
            services.AddScoped<IRepositoryBase<Ekran>, EkranRepository>();
            //Repository
            services.AddScoped<ICalismaDurumuRepository, CalismaDurumuRepository>();
            services.AddScoped<ICinsiyetRepository, CinsiyetRepository>();
            services.AddScoped<IDurumRepository, DurumRepository>();
            services.AddScoped<IGorevlendirilmeTuruRepository, GorevlendirilmeTuruRepository>();
            services.AddScoped<IKadroTuruRepository, KadroTuruRepository>();
            services.AddScoped<IKanGrubuRepository, KanGrubuRepository>();
            services.AddScoped<IKurumRepository, KurumRepository>();
            services.AddScoped<IKurumTipiRepository, KurumTipiRepository>();
            services.AddScoped<IPersonelRepository, PersonelRepository>();
            services.AddScoped<ISehirRepository, SehirRepository>();
            services.AddScoped<IUnvanRepository, UnvanRepository>();
            services.AddScoped<IBolumRepository, BolumRepository>();
            services.AddScoped<IBirimRepository, BirimRepository>();
            services.AddScoped<IKisimRepository, KisimRepository>();
            services.AddScoped<IKabinetBazliBolumRepository, KabinetBazliBolumRepository>();
            services.AddScoped<IGorevlendirilmeTipiRepository, GorevlendirilmeTipiRepository>();
            services.AddScoped<IPersonelGorevlendirilmeRepository, PersonelGorevlendirilmeRepository>();
            services.AddScoped<IAppRoleRepository, AppRoleRepository>();
            services.AddScoped<IAppUserRepository, AppUserRepository>();
            services.AddScoped<ISidebarMenuRepository, SidebarMenuRepository>();
            services.AddScoped<IEkranRepository, EkranRepository>();
        }
        public static void ConfigureRepositoryManager(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryManager, RepositoryManager>();
        }
        public static void ConfigureUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork>(provider => provider.GetRequiredService<RepositoryManager>());
        }
        public static void ConfigureServiceRegistration(this IServiceCollection services)
        {
            services.AddScoped<ICalismaDurumuService, CalismaDurumuService>();
            services.AddScoped<ICinsiyetService, CinsiyetService>();
            services.AddScoped<IDurumService, DurumService>();
            services.AddScoped<IGorevlendirilmeTuruService, GorevlendirilmeTuruService>();
            services.AddScoped<IKadroTuruService, KadroTuruService>();
            services.AddScoped<IKanGrubuService, KanGrubuService>();
            services.AddScoped<IKurumService, KurumService>();
            services.AddScoped<IKurumTipiService, KurumTipiService>();
            services.AddScoped<IPersonelService, PersonelService>();
            services.AddScoped<ISehirService, SehirService>();
            services.AddScoped<IUnvanService, UnvanService>();
            services.AddScoped<IBolumService, BolumService>();
            services.AddScoped<IBirimService, BirimService>();
            services.AddScoped<IKisimService, KisimService>();
            services.AddScoped<IKabinetBazliBolumService, KabinetBazliBolumService>();
            services.AddScoped<IGorevlendirilmeTipiService, GorevlendirilmeTipiService>();
            services.AddScoped<IPersonelGorevlendirilmeService, PersonelGorevlendirilmeService>();
            services.AddScoped<IAppRoleService, AppRoleService>();
            services.AddScoped<IAppUserService, AppUserService>();
            services.AddScoped<ISidebarMenuService, SidebarMenuService>();
            services.AddScoped<IEkranService, EkranService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddSingleton<ILoggerService, LoggerService>();
        }
        public static void ConfigureServiceManager(this IServiceCollection services)
        {
            services.AddScoped<IServiceManager, ServiceManager>();
        }
        public static IApplicationBuilder UseMvcWithAreas(this IApplicationBuilder app)
        => app.UseEndpoints(routes =>
        {
            routes.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
            );

            routes.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}"
            );           
        });
        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }
        public static void ConfigureIdentity(this IServiceCollection services)
        {
            IdentityConfiguration.ConfigureIdentity(services);
        }
        public static void ConfigureApplicationCookie(this IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {
                options.LoginPath = new PathString("/Account/Login");
                options.Cookie.Name = "SabimWEBCookie";
                options.Cookie.HttpOnly = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.None;
                options.Cookie.SameSite = SameSiteMode.Lax;
                options.SlidingExpiration = true;

                // Kullanıcı "Beni Hatırla" seçmemişse varsayılan süre 4 saat
                options.ExpireTimeSpan = TimeSpan.FromHours(4);
            });
            services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = new PathString("/Error/403");
                options.ExpireTimeSpan = TimeSpan.FromDays(2); // Beni Hatırla durumu için maksimum süre
            });
        }
        public static void ConfigureSession(this IServiceCollection services)
        {
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromHours(8); // Oturum süresi
                options.Cookie.HttpOnly = true; // Güvenlik için
                options.Cookie.IsEssential = true; // GDPR için
            });
        }
        public static void ConfigureAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("ClaimBasedPolicy", policy =>
                    policy.Requirements.Add(new ClaimBasedAuthorizationAttribute("Dashboard", "Tam Erişim")));
                options.AddPolicy("ClaimBasedPolicyForBolum", policy =>
                    policy.Requirements.Add(new ClaimBasedAuthorizationAttribute("Bölüm", "Tam Erişim")));
                options.AddPolicy("ClaimBasedPolicyForCinsiyet", policy =>
                    policy.Requirements.Add(new ClaimBasedAuthorizationAttribute("Cinsiyet", "Tam Erişim")));
            });
        }
        public static void ConfigureAddValidators(this IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.ModelValidatorProviders.Clear(); // Data Annotations doğrulayıcılarını devre dışı bırakır
            });
            services.AddValidatorsFromAssemblyContaining<LoginDtoValidator>();
            // Eğer Validation otomatik çalışsın istiyorsanız (ValidatorFactory kullanarak):
            services.AddFluentValidationAutoValidation(); // Bu, ModelState ile entegrasyonu sağlar
            services.AddFluentValidationClientsideAdapters(); // İstemci tarafı adaptörlerini ekler
        }
        public static void ConfigureMailSettings(this IServiceCollection services,IConfiguration configuration)
        {
            services.Configure<MailSettings>(configuration.GetSection("EmailSettings"));
            // EmailHelper ve EmailService'i ekleyin
            services.AddSingleton<EmailHelper>(provider =>
            {
                var mailSettings = provider.GetRequiredService<IOptions<MailSettings>>().Value;
                return new EmailHelper(mailSettings);
            });
        }

    }
}
