using FluentValidation;
using Sabim.Domain.DTOs.MalzemeMarkaDtos;
using Sabim.Services.Contracts;

namespace Sabim.Services.Validators.MalzemeMarkaValidators
{
    public class CreateMalzemeMarkaValidator : AbstractValidator<CreateMalzemeMarkaDto>
    {
        private readonly IServiceManager _manager;

        public CreateMalzemeMarkaValidator(IServiceManager manager)
        {
            _manager = manager;

            RuleFor(x => x.MarkaAdi)
                .NotEmpty().WithMessage("Marka adı boş bırakılamaz.")
                .MaximumLength(50).WithMessage("Marka adı en fazla 50 karakter olmalıdır.")
                .MinimumLength(2).WithMessage("Marka adı en az 2 karakter olmalıdır.")
                .Must(BeUniqueName).WithMessage("Bu marka zaten kayıtlı!");

            RuleFor(x => x.DurumId)
                .NotNull().WithMessage("Durum alanı boş olamaz.");
        }

        private bool BeUniqueName(string markaAdi)
        {
            return !_manager.MalzemeMarkaService.TIsAny(x => x.MarkaAdi == markaAdi);
        }
    }
}
