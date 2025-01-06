namespace Sabim.Services.Contracts
{
    public interface IServiceManager
    {
        ICalismaDurumuService CalismaDurumuService { get; }
        ICinsiyetService CinsiyetService { get; }
        IDurumService DurumService { get; }
        IGorevlendirilmeTuruService GorevlendirilmeTuruService { get; }
        IKadroTuruService KadroTuruService { get; } 
        IKanGrubuService KanGrubuService { get; }
        IKurumService KurumService { get; }
        IKurumTipiService KurumTipiService { get; }
        IPersonelService PersonelService { get; }
        ISehirService SehirService { get; }
        IUnvanService UnvanService { get; }
        IBolumService BolumService { get; }
        IBirimService BirimService { get; }
        IKisimService KisimService { get; }
        IKabinetBazliBolumService KabinetBazliBolumService { get; }
        IGorevlendirilmeTipiService GorevlendirilmeTipiService { get; }
        IPersonelGorevlendirilmeService PersonelGorevlendirilmeService { get; }
        IAppRoleService AppRoleService { get; }
        IAppUserService AppUserService { get; }
        ISidebarMenuService SidebarMenuService { get; }
        IEkranService EkranService { get; }
        IEmailService EmailService { get; }
        ILoggerService LoggerService { get; }
    }
}
