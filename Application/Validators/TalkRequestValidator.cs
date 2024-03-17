using FluentValidation;
using Application.Crosscuting.DTO.Talk;

namespace Application.Validators
{
    public class TalkRequestValidator : AbstractValidator<TalkRequest>
    {
        public TalkRequestValidator()
        {
            RuleFor(t => t.IdBegin)
                .NotEmpty().WithMessage("O IdBegin não pode ser nulo.")
                .Length(36).WithMessage("O IdBegin deve ter exatamente 36 caracteres.");

            RuleFor(t => t.IdEnd)
                .NotEmpty().WithMessage("O IdEnd não pode ser nulo.")
                .Length(36).WithMessage("O IdEnd deve ter exatamente 36 caracteres.");
        }
    }
}