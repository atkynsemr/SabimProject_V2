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
 
        }
    }
}
