using FluentValidation;
using Sabim.Domain.DTOs.MalzemeMarkaDtos;
using Sabim.Services.Contracts;

namespace Sabim.Services.Validators.MalzemeMarkaValidators
{
    public class UpdateMalzemeMarkaValidator : AbstractValidator<UpdateMalzemeMarkaDto>
    {
        private readonly IServiceManager _manager;

        public UpdateMalzemeMarkaValidator(IServiceManager manager)
        {
            _manager = manager;

            RuleFor(x => x.MarkaAdi)
                .NotEmpty().WithMessage("Marka adı boş bırakılamaz.")
                .MaximumLength(50).WithMessage("Marka adı en fazla 50 karakter olmalıdır.")
                .MinimumLength(2).WithMessage("Marka adı en az 2 karakter olmalıdır.")
                .Must((dto, markaAdi) => BeUniqueName(markaAdi, dto.MalzemeCinsiId, dto.MalzemeMarkaID))
                .WithMessage("Bu marka zaten kayıtlı!");


            RuleFor(x => x.MalzemeCinsiId).NotEmpty().WithMessage("Cins ID boş bırakılamaz.");

            RuleFor(x => x.DurumId)
                .NotNull().WithMessage("Durum alanı boş olamaz.");
        }

        private bool BeUniqueName(string markaAdi, byte malzemeCinsiId, byte? excludeId)
        {
            return !_manager.MalzemeMarkaService.TIsAny(x =>
                x.MarkaAdi == markaAdi &&
                x.MalzemeCinsiId == malzemeCinsiId &&
                x.MalzemeMarkaID != excludeId);
        }
    }
}
