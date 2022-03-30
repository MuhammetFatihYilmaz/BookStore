using System;
using FluentAssertions;
using Tests.TestSetup;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using Xunit;

namespace Tests.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandValidatorTests: IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("Frank","H", "1920,10,08")]
        [InlineData("Fr","Herbert","1920,10,08")]
        [InlineData("Fr","H", "1920,10,08")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string name, string surname, string birtdate)
        {
            //Arrange
            UpdateAuthorCommand command = new UpdateAuthorCommand(null,null);
            command.Model = new UpdateAuthorModel()
            {
                Name = name,
                Surname = surname,
                BirthDate = Convert.ToDateTime(birtdate)
            };
            //Act
            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            var result = validator.Validate(command);
            //Assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

    }

}