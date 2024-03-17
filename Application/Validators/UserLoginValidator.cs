using Application.Crosscuting.DTO.Credentials;
using FluentValidation;

namespace Application.Validators
{
    public class UserLoginValidator : AbstractValidator<UserCredentialsLogin>
    {
        public UserLoginValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("O email é obrigatório.")
                .EmailAddress().WithMessage("O email fornecido não é válido.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("A senha é obrigatória.")
                .MaximumLength(12).WithMessage("A senha deve ter no máximo 12 caracteres.");
        }
    }
}
