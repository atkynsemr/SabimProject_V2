using FluentValidation;
using Sabim.Domain.DTOs.KanGrubuDtos;
using Sabim.Services.Contracts;

namespace Sabim.Services.Validators.KanGrubuValidators
{
    public class CreateKanGrubuDtoValidator : AbstractValidator<CreateKanGrubuDto>
    {
        private readonly IServiceManager _manager;
        public CreateKanGrubuDtoValidator(IServiceManager manager)
        {
            _manager = manager;
            RuleFor(x => x.KanGrubuAdi)
                .NotEmpty().WithMessage("Kan Grubu boş bırakılamaz.")
                .MinimumLength(3).WithMessage("Kan Grubu en az 3 karakter olmalıdır.")
                .MaximumLength(10).WithMessage("Kan Grubu en fazla 10 karakter olmalıdır.")
                //.Matches("^[a-zA-ZçÇğĞıİöÖşŞüÜ\\s]+$").WithMessage("Kan Grubu yalnızca harf içermelidir.")
                .Must(BeUniqueName).WithMessage("Kan Grubu adı zaten kayıtlı!");
        }
        private bool BeUniqueName(string kanGrubuAdi)
        {
            return !_manager.KanGrubuService.TIsAny(x => x.KanGrubuAdi == kanGrubuAdi);
        }
    }
}
