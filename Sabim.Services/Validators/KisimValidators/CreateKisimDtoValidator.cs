using FluentValidation;
using Sabim.Domain.DTOs.KisimDtos;
using Sabim.Domain.Entities;
using Sabim.Services.Contracts;

namespace Sabim.Services.Validators.KisimValidators
{
    public class CreateKisimDtoValidator : AbstractValidator<CreateKisimDto>
    {
        private readonly IServiceManager _manager;
        public CreateKisimDtoValidator(IServiceManager manager)
        {
            _manager = manager;
            RuleFor(x => x.KisimAdi)
                        .NotEmpty().WithMessage("Kısım Adı boş bırakılamaz.")
                        .MaximumLength(50).WithMessage("Kısım Adı en fazla 50 karakter olmalıdır.")
                        .MinimumLength(5).WithMessage("Kısım Adı en az 5 karakter olmalıdır.")
                        .Must((dto, kisimAdi) => BeUniqueName(kisimAdi, dto.BirimId)).WithMessage("Aynı birimde bu kısım adı zaten kayıtlı!");            
            RuleFor(x => x.BirimId).NotEmpty().WithMessage("Birim alanı boş bırakılamaz.");
            RuleFor(x => x.Aciklama)
                        .NotEmpty().WithMessage("Kısmın Bulunduğu Yer boş bırakılamaz.")
                        .MaximumLength(75).WithMessage("Kısmın Bulunduğu Yer en fazla 75 karakter olmalıdır.")
                        .MinimumLength(5).WithMessage("Kısmın Bulunduğu Yer en az 5 karakter olmalıdır.");
            RuleFor(x => x.DahiliTelefon)
                        .NotEmpty().WithMessage("Dahili Telefon boş bırakılamaz.")
                        .MaximumLength(25).WithMessage("Dahili Telefon en fazla 25 karakter olmalıdır.")
                        .MinimumLength(4).WithMessage("Dahili Telefon en az 4 karakter olmalıdır.")
                        .Matches(@"^[0-9\s\-]*$").WithMessage("Dahili Telefon yalnızca rakamlar, boşluk ve (-) içerebilir.");
        }
        private bool BeUniqueName(string kisimAdi, short birimId)
        {
            return !_manager.KurumService.TIsKurumExists(kisimAdi, birimId);
        }
    }
}
