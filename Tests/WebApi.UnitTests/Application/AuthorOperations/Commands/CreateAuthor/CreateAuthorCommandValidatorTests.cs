using System;
using FluentAssertions;
using Tests.TestSetup;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using Xunit;

namespace Tests.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandValidatorTests: IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("Frank","H", "1920,10,08")]
        [InlineData("Fr","Herbert","1920,10,08")]
        [InlineData("Fr","H", "1920,10,08")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string name, string surname, string birtdate)
        {
            //Arrange
            CreateAuthorCommand command = new CreateAuthorCommand(null,null);
            command.Model = new CreateAuthorModel()
            {
                Name = name,
                Surname = surname,
                BirthDate = Convert.ToDateTime(birtdate)
            };
            //Act
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var result = validator.Validate(command);
            //Assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }

}