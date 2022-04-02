using FluentValidation;

namespace WebApi.Application.UserOperations.Commands.CreateUser
{
    public class CreateUserCommandValidator: AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(command => command.Model.Name).NotEmpty().MinimumLength(3);
            RuleFor(command => command.Model.Surname).NotEmpty().MinimumLength(2);
            RuleFor(command => command.Model.Email).NotEmpty().WithMessage("Email adresi girmeniz gerekli.")
                     .EmailAddress().WithMessage("Geçerli bir Email adresi giriniz.");
            RuleFor(command => command.Model.Password).NotEmpty().MinimumLength(8);

        }
    }

}