using FluentAssertions;
using Tests.TestSetup;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using Xunit;

namespace Tests.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandValidatorTests: IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenLessThanFourCharacterInputToName_Validator_ShouldBeReturnError()
        {
            //Arrange
            UpdateGenreCommand command = new UpdateGenreCommand(null);
            command.Model = new UpdateGenreModel(){Name = "Tes"};
            //Act
            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            var result = validator.Validate(command);
            //Assert
            result.Errors.Count.Should().BeGreaterThan(0);
            
        }
    }

}