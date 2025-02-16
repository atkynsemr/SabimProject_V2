using Sabim.Domain.Entities;

namespace Sabim.Infrastructure.Persistence.Repository.Contracts
{
    public interface IRepositoryManager
    {
        ICalismaDurumuRepository CalismaDurumu { get; }
        ICinsiyetRepository Cinsiyet { get; }
        IDurumRepository Durum { get; }
        IGorevlendirilmeTuruRepository GorevlendirilmeTuru { get; }
        IKadroTuruRepository KadroTuru { get; }
        IKanGrubuRepository KanGrubu { get; }
        IKurumRepository Kurum { get; }
        IKurumTipiRepository KurumTipi { get; }
        IPersonelRepository Personel { get; }
        ISehirRepository Sehir { get; }
        IUnvanRepository Unvan { get; }
        IBolumRepository Bolum { get; }
        IBirimRepository Birim { get; }
        IKisimRepository Kisim { get; }
        IKabinetBazliBolumRepository KabinetBazliBolum { get; }
        IGorevlendirilmeTipiRepository GorevlendirilmeTipi { get; }
        IPersonelGorevlendirilmeRepository PersonelGorevlendirilme { get; }
        IAppUserRepository AppUser { get; }
        IAppRoleRepository AppRole { get; }
        ISidebarMenuRepository SidebarMenu { get; } 
        IEkranRepository Ekran { get; }
        ISavciCalisilanKatipServiceRepository SavciCalisilanKatip { get; }
    }
}
