using FluentAssertions;
using Tests.TestSetup;
using WebApi.Application.BookOperations.Commands.DeleteBook;
using Xunit;

namespace Tests.Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandValidatorTests: IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenIdInputGivenZero_Validator_ShouldBeReturnError()
        {
            DeleteBookCommand command = new DeleteBookCommand(null);
            command.BookId = 0;

            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }

}