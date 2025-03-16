using FluentValidation;
using Sabim.Domain.DTOs.MalzemeModelDtos;
using Sabim.Services.Contracts;

namespace Sabim.Services.Validators.MalzemeModelValidators
{
    public class UpdateMalzemeModelValidator : AbstractValidator<UpdateMalzemeModelDto>
    {
        private readonly IServiceManager _manager;

        public UpdateMalzemeModelValidator(IServiceManager manager)
        {
            _manager = manager;

            RuleFor(x => x.ModelAdi)
                .NotEmpty().WithMessage("Model adı boş bırakılamaz.")
                .MaximumLength(50).WithMessage("Model adı en fazla 50 karakter olmalıdır.")
                .MinimumLength(2).WithMessage("Model adı en az 2 karakter olmalıdır.")
                .Must((dto, modelAdi) => BeUniqueName(modelAdi, dto.MalzemeModelID)).WithMessage("Bu model zaten kayıtlı!");

            RuleFor(x => x.MalzemeMarkaId)
                .NotEmpty().WithMessage("Marka ID boş bırakılamaz.");

            //RuleFor(x => x.MalzemeCinsiId)
            //    .NotEmpty().WithMessage("Cins ID boş bırakılamaz.");

            RuleFor(x => x.DurumId)
                .NotNull().WithMessage("Durum alanı boş olamaz.");
        }

        private bool BeUniqueName(string modelAdi, byte? excludeId)
        {
            return !_manager.MalzemeModelService.TIsAny(x => x.ModelAdi == modelAdi && x.MalzemeModelID != excludeId);
        }
    }
}
