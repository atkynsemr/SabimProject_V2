using FluentValidation;
using Sabim.Domain.DTOs.KanGrubuDtos;
using Sabim.Services.Contracts;

namespace Sabim.Services.Validators.KanGrubuValidators
{
    public class UpdateKanGrubuDtoValidator : AbstractValidator<UpdateKanGrubuDto>
    {
        private readonly IServiceManager _manager;
        public UpdateKanGrubuDtoValidator(IServiceManager manager)
        {
            _manager = manager;
            RuleFor(x => x.KanGrubuID).NotEmpty().WithMessage("Kan Grubu ID boş değer alamaz.");
            RuleFor(x => x.KanGrubuAdi)
                .NotEmpty().WithMessage("Kan Grubu boş bırakılamaz.")
                .MinimumLength(3).WithMessage("Kan Grubu en az 3 karakter olmalıdır.")
                .MaximumLength(10).WithMessage("Kan Grubu en fazla 10 karakter olmalıdır.")
                .Must((dto, kanGrubuAdi) => BeUniqueName(kanGrubuAdi, dto.KanGrubuID)).WithMessage("Kan Grubu adı zaten kayıtlı!");
        }
        private bool BeUniqueName(string kanGrubuAdi, short? excludeId)
        {
            return !_manager.KanGrubuService.TIsAny(x => x.KanGrubuAdi == kanGrubuAdi, excludeId);
        }
    }
}
