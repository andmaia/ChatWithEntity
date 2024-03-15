using Application.Crosscuting.DTO.User;
using FluentValidation;

namespace Application.Valdators
{
    public class UserUpdateValidator: AbstractValidator<UserUpdate>
    {
        public UserUpdateValidator() 
        {
            RuleFor(x => x.Id).NotEmpty().Length(36).WithMessage("Formato de Id inválido.");
            RuleFor(x => x.Name).NotEmpty().MaximumLength(30).WithMessage("Nome deve ter no máximo 30 caracteres");
        }
    }
}
