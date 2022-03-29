using FluentAssertions;
using Tests.TestSetup;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using Xunit;

namespace Tests.Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommandValidatorTests: IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenLessThanFourCharacterInputToName_Validator_ShouldBeReturnError()
        {
            //Arrange
            CreateGenreCommand command = new CreateGenreCommand(null);
            command.Model = new CreateGenreModel(){Name = "Tes"};
            //Act
            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            var result = validator.Validate(command);
            //Assert
            result.Errors.Count.Should().BeGreaterThan(0);
            
        }
    }

}