using FluentAssertions;
using Tests.TestSetup;
using WebApi.Application.AuthorOperations.Commands.DeleteAuthor;
using Xunit;

namespace Tests.Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandValidatorTests: IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenAuthorIdInputGivenZero_Validator_ShouldBeReturnError()
        {
            DeleteAuthorCommand command = new DeleteAuthorCommand(null);
            command.AuthorId = 0;

            DeleteAuthorCommandValidator validator = new DeleteAuthorCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }

}