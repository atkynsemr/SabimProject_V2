using FluentValidation;
using Sabim.Domain.DTOs.PersonelGorevlendirilmeDtos;
using Sabim.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabim.Services.Validators.PersonelGorevlendirilmeValidators
{
    public class CreatePersonelGorevlendirilmeDtoValidator : AbstractValidator<CreatePersonelGorevlendirilmeDto>
    {

        public CreatePersonelGorevlendirilmeDtoValidator()
        {
            RuleFor(x => x.PersonelId).NotEmpty().WithMessage("Personel alanı boş bırakılamaz.");
            //RuleFor(x => x.GorevlendirilmeTuruId).NotEmpty().WithMessage("Görevlendirilme Türü boş bırakılamaz.");
            //RuleFor(x => x.KurumId).NotEmpty().WithMessage("Kadrosunun Bulunduğu Kurum boş bırakılamaz.");
            //RuleFor(x => x.KadroTuruId).NotEmpty().WithMessage("Kadro Türü boş bırakılamaz.");
            //RuleFor(x => x.CalismaDurumuId).NotEmpty().WithMessage("Çalışma durumu boş bırakılamaz.");
        }
    }
}
