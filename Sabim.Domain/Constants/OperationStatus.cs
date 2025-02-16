namespace Sabim.Domain.Constants
{
    public static class OperationStatus
    {
        public const string Success = "İşlem Başarılı";
        public const string NotFound = "Kayıt Bulunamadı!";
        public const string ForeignKeyConflict = "Bu kayıt başka bir tabloda kullanılıyor, silme işlemi gerçekleştirilemez!";
        public const string GlobalError = "İşlem sırasında bilinmeyen bir hata oluştu! Lütfen tekrar deneyiniz.";
        public const string PrimaryKeyNotDefined = "Birincil anahtar tanımlanmamış!";
        public const string Incomplete = "İşleminizi kontrol ediniz. İşlem tamamlanmamış ise lütfen tekrar deneyiniz!";
    }
}
