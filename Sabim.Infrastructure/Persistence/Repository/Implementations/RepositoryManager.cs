using Sabim.Infrastructure.Persistence.Context;
using Sabim.Infrastructure.Persistence.Repository.Contracts;

namespace Sabim.Infrastructure.Persistence.Repository.Implementations
{
    public class RepositoryManager : IRepositoryManager, IUnitOfWork
    {
        private readonly SabimDbContext _context;
        private readonly ICalismaDurumuRepository _calismaDurumuRepository;
        private readonly ICinsiyetRepository _cinsiyetRepository;
        private readonly IDurumRepository _durumRepository;
        private readonly IGorevlendirilmeTuruRepository _gorevlendirilmeTuruRepository;
        private readonly IKadroTuruRepository _kadroTuruRepository;
        private readonly IKanGrubuRepository _kanGrubuRepository;
        private readonly IKurumRepository _kurumRepository;
        private readonly IKurumTipiRepository _kurumTipiRepository;
        private readonly IPersonelRepository _personelRepository;
        private readonly ISehirRepository _sehirRepository;
        private readonly IUnvanRepository _unvanRepository;
        private readonly IBolumRepository _bolumRepository;
        private readonly IBirimRepository _birimRepository;
        private readonly IKisimRepository _kisimRepository;
        private readonly IKabinetBazliBolumRepository _kabinetBazliBolumRepository;
        private readonly IGorevlendirilmeTipiRepository _gorevlendirilmeTipiRepository;
        private readonly IPersonelGorevlendirilmeRepository _personelGorevlendirilmeRepository;
        private readonly IAppUserRepository _appUserRepository;
        private readonly IAppRoleRepository _appRoleRepository;
        private readonly ISidebarMenuRepository _sidebarMenuRepository;
        private readonly IEkranRepository _ekranRepository;

        public RepositoryManager(SabimDbContext context, ICalismaDurumuRepository calismaDurumuRepository,
            ICinsiyetRepository cinsiyetRepository, IDurumRepository durumRepository,
            IGorevlendirilmeTuruRepository gorevlendirilmeTuruRepository, IKadroTuruRepository kadroTuruRepository,
            IKanGrubuRepository kanGrubuRepository, IKurumRepository kurumRepository,
            IKurumTipiRepository kurumTipiRepository, IPersonelRepository personelRepository,
            ISehirRepository sehirRepository, IUnvanRepository unvanRepository, IBolumRepository bolumRepository, IBirimRepository birimRepository, IKisimRepository kisimRepository,
            IKabinetBazliBolumRepository kabinetBazliBolumRepository, IGorevlendirilmeTipiRepository gorevlendirilmeTipiRepository, IPersonelGorevlendirilmeRepository personelGorevlendirilmeRepository, 
            IAppUserRepository appUserRepository, IAppRoleRepository appRoleRepository, ISidebarMenuRepository sidebarMenuRepository, 
            IEkranRepository ekranRepository)
        {
            _context = context;
            _calismaDurumuRepository = calismaDurumuRepository;
            _cinsiyetRepository = cinsiyetRepository;
            _durumRepository = durumRepository;
            _gorevlendirilmeTuruRepository = gorevlendirilmeTuruRepository;
            _kadroTuruRepository = kadroTuruRepository;
            _kanGrubuRepository = kanGrubuRepository;
            _kurumRepository = kurumRepository;
            _kurumTipiRepository = kurumTipiRepository;
            _personelRepository = personelRepository;
            _sehirRepository = sehirRepository;
            _unvanRepository = unvanRepository;
            _bolumRepository = bolumRepository;
            _birimRepository = birimRepository;
            _kisimRepository = kisimRepository;
            _kabinetBazliBolumRepository = kabinetBazliBolumRepository;
            _gorevlendirilmeTipiRepository = gorevlendirilmeTipiRepository;
            _personelGorevlendirilmeRepository = personelGorevlendirilmeRepository;
            _appUserRepository = appUserRepository;
            _appRoleRepository = appRoleRepository;
            _sidebarMenuRepository = sidebarMenuRepository;
            _ekranRepository = ekranRepository;
        }
        public ICalismaDurumuRepository CalismaDurumu => _calismaDurumuRepository;
        public ICinsiyetRepository Cinsiyet => _cinsiyetRepository;
        public IDurumRepository Durum => _durumRepository;
        public IGorevlendirilmeTuruRepository GorevlendirilmeTuru => _gorevlendirilmeTuruRepository;
        public IKadroTuruRepository KadroTuru => _kadroTuruRepository;
        public IKanGrubuRepository KanGrubu => _kanGrubuRepository;
        public IKurumRepository Kurum => _kurumRepository;
        public IKurumTipiRepository KurumTipi => _kurumTipiRepository;
        public IPersonelRepository Personel => _personelRepository;
        public ISehirRepository Sehir => _sehirRepository;
        public IUnvanRepository Unvan => _unvanRepository;
        public IBolumRepository Bolum => _bolumRepository;
        public IBirimRepository Birim => _birimRepository;
        public IKisimRepository Kisim => _kisimRepository;
        public IKabinetBazliBolumRepository KabinetBazliBolum => _kabinetBazliBolumRepository;   
        public IGorevlendirilmeTipiRepository GorevlendirilmeTipi => _gorevlendirilmeTipiRepository;
        public IPersonelGorevlendirilmeRepository PersonelGorevlendirilme => _personelGorevlendirilmeRepository;
        public IAppUserRepository AppUser => _appUserRepository;
        public IAppRoleRepository AppRole => _appRoleRepository;
        public ISidebarMenuRepository SidebarMenu => _sidebarMenuRepository;
        public IEkranRepository Ekran => _ekranRepository;
        public IRepositoryBase<T> GetRepository<T>() where T : class
        {
            return new RepositoryBase<T>(_context);
        }
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
