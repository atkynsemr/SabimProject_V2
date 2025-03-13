using FluentValidation;
using Sabim.Domain.DTOs.MalzemeDurumuDtos;
using Sabim.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabim.Services.Validators.MalzemeDurumuValidators
{
    public class UpdateMalzemeDurumuValidator : AbstractValidator<UpdateMalzemeDurumuDto>
    {
        private readonly IServiceManager _manager;

        public UpdateMalzemeDurumuValidator(IServiceManager manager)
        {
            _manager = manager;

            RuleFor(x => x.MalzemeDurumuAdi)
                .NotEmpty().WithMessage("Malzeme Durumu adı boş bırakılamaz.")
                .MaximumLength(20).WithMessage("Malzeme Durumu adı en fazla 20 karakter olmalıdır.")
                .MinimumLength(3).WithMessage("Malzeme Durumu adı en az 3 karakter olmalıdır.")
                .Must((dto, malzemeDurumuAdi) => BeUniqueName(malzemeDurumuAdi, dto.MalzemeDurumuID)).WithMessage("Bu malzeme durumu zaten kayıtlı!");

            RuleFor(x => x.DurumId)
                .NotNull().WithMessage("Durum alanı boş olamaz.");
        }

        private bool BeUniqueName(string malzemeDurumuAdi, byte? excludeId)
        {
            return !_manager.MalzemeDurumuService.TIsAny(x => x.MalzemeDurumuAdi == malzemeDurumuAdi && x.MalzemeDurumuID != excludeId);
        }
    }
}
