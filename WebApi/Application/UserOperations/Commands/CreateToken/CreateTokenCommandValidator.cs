using FluentValidation;

namespace WebApi.Application.UserOperations.Commands.CreateToken
{
    public class CreateTokenCommandValidator: AbstractValidator<CreateTokenCommand>
    {
        public CreateTokenCommandValidator()
        {
            RuleFor(command => command.Model.Email).NotEmpty().WithMessage("Email adresi girmeniz gerekli.")
                     .EmailAddress().WithMessage("Geçerli bir Email adresi giriniz.");
            RuleFor(command => command.Model.Password).NotEmpty().MinimumLength(8);
        }
    }

}