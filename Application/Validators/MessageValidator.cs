using FluentValidation;
using Application.Crosscuting.DTO.Message;

namespace Application.Validators
{
    public class MessageRequestValidator : AbstractValidator<MessageRequest>
    {
        public MessageRequestValidator()
        {
            RuleFor(m => m.Text)
                .NotEmpty().WithMessage("O Texto não pode ser nulo.")
                .MaximumLength(500).WithMessage("O Texto não pode ter mais de 500 caracteres.");

            RuleFor(m => m.UserId)
                .NotEmpty().WithMessage("O UserId não pode ser nulo.");

            RuleFor(m => m.TalkId)
                .NotEmpty().WithMessage("O TalkId não pode ser nulo.");
        }
    }
}