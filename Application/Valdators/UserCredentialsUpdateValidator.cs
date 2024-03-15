using Application.Crosscuting.DTO.Credentials;
using FluentValidation;

namespace Application.Validators
{
    public class UserCredentialsUpdateValidator : AbstractValidator<UserCredentialsUpdate>
    {
        public UserCredentialsUpdateValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("O ID é obrigatório.")
                .Length(36).WithMessage("O ID deve ter exatamente 36 caracteres.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("A senha é obrigatória.")
                .MaximumLength(12).WithMessage("A senha deve ter no máximo 12 caracteres.");

            RuleFor(x => x.PasswordConfirmation)
                .NotEmpty().When(x => !string.IsNullOrEmpty(x.NewPassword))
                .Equal(x => x.NewPassword).When(x => !string.IsNullOrEmpty(x.NewPassword))
                .WithMessage("A confirmação da senha deve ser igual à nova senha.");

            RuleFor(x => x.NewPassword)
                .MaximumLength(12).When(x => !string.IsNullOrEmpty(x.NewPassword))
                .WithMessage("A nova senha deve ter no máximo 12 caracteres.");

            RuleFor(x => x.Email)
                .EmailAddress().When(x => !string.IsNullOrEmpty(x.Email))
                .WithMessage("O email fornecido não é válido.");
        }
    }
}
