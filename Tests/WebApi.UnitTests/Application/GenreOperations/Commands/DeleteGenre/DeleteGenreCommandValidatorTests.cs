using FluentAssertions;
using Tests.TestSetup;
using WebApi.Application.GenreOperations.Commands.DeleteGenre;
using Xunit;

namespace Tests.Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommandValidatorTests: IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenGenreIdEqualOrLessThanZero_Validator_ShouldBeReturnError()
        {
            DeleteGenreCommand command = new DeleteGenreCommand(null);
            command.GenreId = -1;

            DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }

}