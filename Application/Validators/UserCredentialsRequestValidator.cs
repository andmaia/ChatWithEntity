using Application.Crosscuting.DTO.Credentials;
using FluentValidation;

namespace Application.Validators
{
    public class UserCredentialsRequestValidator : AbstractValidator<UserCredentialsRequest>
    {
        public UserCredentialsRequestValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("O nome de usuário é obrigatório.")
                .MaximumLength(14).WithMessage("O nome de usuário deve ter no máximo 14 caracteres.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("O email é obrigatório.")
                .EmailAddress().WithMessage("O email fornecido não é válido.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("A senha é obrigatória.")
                .MaximumLength(12).WithMessage("A senha deve ter no máximo 12 caracteres.");

            RuleFor(x => x.PasswordConfirmation)
                .NotEmpty().WithMessage("A confirmação da senha é obrigatória.")
                .Equal(x => x.Password).WithMessage("A confirmação da senha deve ser igual à senha.");
        }
    }
}
