using FluentValidation;

namespace Application.Validators
{
    public class ParamsIdValidator : AbstractValidator<string>
    {
        public ParamsIdValidator()
        {
            RuleFor(id => id)
            .NotEmpty().WithMessage("O ID não pode estar vazio.")
            .Length(36).WithMessage("O ID deve ter exatamente 36 caracteres.");
        }
    }
}
