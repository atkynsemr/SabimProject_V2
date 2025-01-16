using FluentValidation;
using Sabim.Domain.DTOs.PersonelDtos;
using Sabim.Services.Contracts;

namespace Sabim.Services.Validators.PersonelValidators
{
    public class CreatePersonelDtoValidator : AbstractValidator<CreatePersonelDto>
    {
        private readonly IServiceManager _manager;
        public CreatePersonelDtoValidator(IServiceManager manager)
        {
            _manager = manager;
            RuleFor(x => x.Ad)
                      .NotEmpty().WithMessage("Personel Adı boş bırakılamaz.")
                      .MaximumLength(30).WithMessage("Personel Adı en fazla 30 karakter olmalıdır.")
                      .MinimumLength(5).WithMessage("Personel Adı en az 3 karakter olmalıdır.")
                      .Matches("^[a-zA-ZçÇğĞıİöÖşŞüÜ\\s]+$").WithMessage("Personel Adı  yalnızca harf içermelidir.");
            RuleFor(x => x.Soyad)
                      .NotEmpty().WithMessage("Personel Soyadı boş bırakılamaz.")
                      .MaximumLength(25).WithMessage("Personel Soyadı en fazla 25 karakter olmalıdır.")
                      .MinimumLength(2).WithMessage("Personel Soyadı en az 2 karakter olmalıdır.")
                      .Matches("^[a-zA-ZçÇğĞıİöÖşŞüÜ\\s]+$").WithMessage("Personel Soyadı yalnızca harf içermelidir.");
            RuleFor(x => x.SicilNumarasi)
                      .NotNull().WithMessage("Personel Sicil Numarası boş bırakılamaz.")
                      .GreaterThanOrEqualTo(10000).WithMessage("Personel Sicil Numarası en az 5 basamaklı olmalıdır.")
                      .LessThanOrEqualTo(9999999).WithMessage("Personel Sicil Numarası en fazla 7 basamaklı olabilir.")
                      .Must(BeUniqueSicil).WithMessage("Aynı sicil numarası zaten kayıtlı!");
            RuleFor(x => x.CepTelefonu)
                   .MaximumLength(11).WithMessage("Cep Telefonu 11 haneli olmalıdır.")
                   .MinimumLength(11).WithMessage("Cep Telefonu 11 haneli olmalıdır.");
            RuleFor(x => x.CinsiyetId).NotEmpty().WithMessage("Cinsiyet alanı boş bırakılamaz.");
            RuleFor(x => x.KanGrubuId).NotEmpty().WithMessage("Kan Grubu alanı boş bırakılamaz.");
            RuleFor(x => x.DogumYeri)
                .MaximumLength(30).WithMessage("Doğum Yeri en fazla 30 karakter olmalıdır.")
                .MinimumLength(3).WithMessage("Doğum Yeri en az 3 karakter olmalıdır.")
                .When(x => !string.IsNullOrWhiteSpace(x.DogumYeri), ApplyConditionTo.CurrentValidator);
            RuleFor(x => x.Derece)
                .Must(derece => !derece.HasValue || (derece.Value >= 1 && derece.Value <= 15))
                .WithMessage("Derece 1 ile 15 arasında bir değer olmalıdır.");
            RuleFor(x => x.Kademe)
                .Must(kademe => !kademe.HasValue || (kademe.Value >= 1 && kademe.Value <= 4))
                .WithMessage("Kademe 1 ile 4 arasında bir değer olmalıdır.");
            RuleFor(x => x.KimlikNo)
                .Matches("^[1-9][0-9]{10}$").WithMessage("Kimlik Numarası yalnızca 11 haneli rakamlardan oluşmalıdır")
                .Must(BeUniqueKimlikNo).WithMessage("Aynı kimlik numarası zaten kayıtlı!")
                .When(x => !string.IsNullOrWhiteSpace(x.KimlikNo), ApplyConditionTo.CurrentValidator);
            RuleFor(x => x.AracPlakasi)
                .Matches("^[a-zA-Z0-9]{1,11}$").WithMessage("Araç Plakası yalnızca harf ve rakamlardan oluşabilir ve en fazla 11 karakter uzunluğunda olabilir.")
                .When(x => !string.IsNullOrWhiteSpace(x.AracPlakasi), ApplyConditionTo.CurrentValidator);
            RuleFor(x => x.UnvanId).NotEmpty().WithMessage("Unvan alanı boş bırakılamaz.");
            RuleFor(x => x.GorevlendirilmeTuruId).NotEmpty().WithMessage("Görevlendirilme Türü boş bırakılamaz.");
            RuleFor(x => x.KurumId).NotEmpty().WithMessage("Kadrosunun Bulunduğu Kurum boş bırakılamaz.");
            RuleFor(x => x.KadroTuruId).NotEmpty().WithMessage("Kadro Türü boş bırakılamaz.");
            RuleFor(x => x.CalismaDurumuId).NotEmpty().WithMessage("Çalışma durumu boş bırakılamaz.");
        }
        private bool BeUniqueKimlikNo(string kimlikNumarasi)
        {
            return !_manager.PersonelService.TIsAny(x => x.KimlikNo == kimlikNumarasi);
        }
        private bool BeUniqueSicil(int sicilNumarasi)
        {
            return !_manager.PersonelService.TIsAny(x => x.SicilNumarasi == sicilNumarasi);
        }
    }
}
