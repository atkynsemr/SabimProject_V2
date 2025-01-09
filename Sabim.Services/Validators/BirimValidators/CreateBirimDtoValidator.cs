using FluentValidation;
using Sabim.Domain.DTOs.BirimDtos;
using Sabim.Services.Contracts;

namespace Sabim.Services.Validators.BirimValidators
{
    public class CreateBirimDtoValidator : AbstractValidator<CreateBirimDto>
    {
        private readonly IServiceManager _manager;
        public CreateBirimDtoValidator(IServiceManager manager)
        {
            _manager = manager;
            RuleFor(x => x.BirimAdi)
                          .NotEmpty().WithMessage("Birim Adı boş bırakılamaz.")
                          .MaximumLength(75).WithMessage("Birim Adı en fazla 75 karakter olmalıdır.")
                          .MinimumLength(5).WithMessage("Birim Adı en az 5 karakter olmalıdır.")
                          .Must(BeUniqueName).WithMessage("Aynı birim adı zaten kayıtlı!");
            RuleFor(x => x.BolumId).NotEmpty().WithMessage("Bölüm alanı boş bırakılamaz.");
        }
        private bool BeUniqueName(string birimAdi)
        {
            return !_manager.BirimService.TIsAny(x => x.BirimAdi == birimAdi);
        }
    }
}
