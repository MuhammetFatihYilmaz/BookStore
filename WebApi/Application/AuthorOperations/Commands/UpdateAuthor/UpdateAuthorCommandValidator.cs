using System;
using FluentValidation;

namespace WebApi.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandValidator: AbstractValidator<UpdateAuthorCommand>
    {
        public UpdateAuthorCommandValidator()
        {
            RuleFor(command=> command.AuthorId).GreaterThan(0);
            RuleFor(command=> command.Model.Name).MinimumLength(3).When(command => command.Model.Name.Trim() != string.Empty);
            RuleFor(command=> command.Model.Surname).MinimumLength(2).When(command => command.Model.Name.Trim() != string.Empty);;
            RuleFor(Command=> Command.Model.BirthDate).NotEmpty().LessThan(DateTime.Now.Date);
        }
    }
}