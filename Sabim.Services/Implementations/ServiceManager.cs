using Sabim.Services.Contracts;

namespace Sabim.Services.Implementations
{
    public class ServiceManager : IServiceManager
    {
        private readonly ICalismaDurumuService _calismaDurumuService;
        private readonly ICinsiyetService _cinsiyetService;
        private readonly IDurumService _durumService;
        private readonly IGorevlendirilmeTuruService _gorevlendirilmeTuruService;
        private readonly IKadroTuruService _kadroTuruService;
        private readonly IKanGrubuService _kanGrubuService;
        private readonly IKurumService _kurumService;
        private readonly IKurumTipiService _kurumTipiService;
        private readonly IPersonelService _personelService;
        private readonly ISehirService _sehirService;
        private readonly IUnvanService _unvanService;
        private readonly IBolumService _bolumService;
        private readonly IBirimService _birimService;
        private readonly IKisimService _kisimService;
        private readonly IKabinetBazliBolumService _kabinetBazliBolumService;
        private readonly IGorevlendirilmeTipiService _gorevlendirilmeTipiService;
        private readonly IPersonelGorevlendirilmeService _personelGorevlendirilmeService;
        private readonly IAppRoleService _appRoleService;
        private readonly IAppUserService _appUserService;
        private readonly ISidebarMenuService _sidebarMenuService;
        private readonly IEkranService _ekranService;
        private readonly IEmailService _emailService;
        private readonly ILoggerService _loggerService;

        public ServiceManager(ICalismaDurumuService calismaDurumuService, ICinsiyetService cinsiyetService, IDurumService durumService,
            IGorevlendirilmeTuruService gorevlendirilmeTuruService, IKadroTuruService kadroTuruService,
            IKanGrubuService kanGrubuService, IKurumService kurumService, IKurumTipiService kurumTipiService, IPersonelService personelService,
            ISehirService sehirService, IUnvanService unvanService, IBolumService bolumService, IBirimService birimService, IKisimService kisimService, IKabinetBazliBolumService kabinetBazliBolumService,
            IGorevlendirilmeTipiService gorevlendirilmeTipiService, IPersonelGorevlendirilmeService personelGorevlendirilmeService, IAppRoleService appRoleService, IAppUserService appUserService,
            ISidebarMenuService sidebarMenuService, IEkranService ekranService, IEmailService emailService, ILoggerService loggerService)
        {
            _calismaDurumuService = calismaDurumuService;
            _cinsiyetService = cinsiyetService;
            _durumService = durumService;
            _gorevlendirilmeTuruService = gorevlendirilmeTuruService;
            _kadroTuruService = kadroTuruService;
            _kanGrubuService = kanGrubuService;
            _kurumService = kurumService;
            _kurumTipiService = kurumTipiService;
            _personelService = personelService;
            _sehirService = sehirService;
            _unvanService = unvanService;
            _bolumService = bolumService;
            _birimService = birimService;
            _kisimService = kisimService;
            _kabinetBazliBolumService = kabinetBazliBolumService;
            _gorevlendirilmeTipiService = gorevlendirilmeTipiService;
            _personelGorevlendirilmeService = personelGorevlendirilmeService;
            _appRoleService = appRoleService;
            _appUserService = appUserService;
            _sidebarMenuService = sidebarMenuService;
            _ekranService = ekranService;
            _emailService = emailService;
            _loggerService = loggerService;
        }

        public ICalismaDurumuService CalismaDurumuService => _calismaDurumuService;
        public ICinsiyetService CinsiyetService => _cinsiyetService;
        public IDurumService DurumService => _durumService;
        public IGorevlendirilmeTuruService GorevlendirilmeTuruService => _gorevlendirilmeTuruService;
        public IKadroTuruService KadroTuruService => _kadroTuruService;
        public IKanGrubuService KanGrubuService => _kanGrubuService;
        public IKurumService KurumService => _kurumService;
        public IKurumTipiService KurumTipiService => _kurumTipiService;
        public IPersonelService PersonelService => _personelService;
        public ISehirService SehirService => _sehirService;
        public IUnvanService UnvanService => _unvanService;
        public IBolumService BolumService => _bolumService;
        public IBirimService BirimService => _birimService;
        public IKisimService KisimService => _kisimService;
        public IKabinetBazliBolumService KabinetBazliBolumService => _kabinetBazliBolumService;
        public IGorevlendirilmeTipiService GorevlendirilmeTipiService => _gorevlendirilmeTipiService;
        public IPersonelGorevlendirilmeService PersonelGorevlendirilmeService => _personelGorevlendirilmeService;
        public IAppRoleService AppRoleService => _appRoleService;
        public IAppUserService AppUserService => _appUserService;
        public ISidebarMenuService SidebarMenuService => _sidebarMenuService;
        public IEkranService EkranService => _ekranService;
        public IEmailService EmailService => _emailService;
        public ILoggerService LoggerService => _loggerService;
    }
}
