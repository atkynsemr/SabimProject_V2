using FluentValidation;
using Sabim.Domain.DTOs.MalzemeDtos;
using Sabim.Services.Contracts;

public class UpdateMalzemeValidator : AbstractValidator<UpdateMalzemeDto>
{
    private readonly IServiceManager _manager;
    public UpdateMalzemeValidator(IServiceManager manager)
    {
        _manager = manager;

        RuleFor(x => x.MalzemeID)
            .NotEmpty().WithMessage("Güncellenecek malzeme ID'si boş bırakılamaz.");

        RuleFor(x => x.SeriNumarasi)
            .NotEmpty().WithMessage("Seri Numarası boş bırakılamaz.")
            .MaximumLength(50).WithMessage("Seri Numarası en fazla 50 karakter olmalıdır.")
            .MinimumLength(5).WithMessage("Seri Numarası en az 5 karakter olmalıdır.")
            .Must((dto, seriNumarasi) => BeUniqueSerialNumber(dto.MalzemeID, seriNumarasi))
            .WithMessage("Bu seri numarası zaten başka bir kayıt ile kullanılıyor!");

        RuleFor(x => x.MalzemeModelId)
            .NotEmpty().WithMessage("Model ID boş bırakılamaz.");

        RuleFor(x => x.MalzemeDurumuId)
            .NotEmpty().WithMessage("Durum ID boş bırakılamaz.");

        RuleFor(x => x.DurumId)
            .NotNull().WithMessage("Durum alanı boş olamaz.");
    }

    private bool BeUniqueSerialNumber(int id, string seriNumarasi)
    {
        return !_manager.MalzemeService.TIsAny(x => x.SeriNumarasi == seriNumarasi && x.MalzemeID != id);
    }
}
