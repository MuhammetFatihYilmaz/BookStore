using System;
using FluentAssertions;
using Tests.TestSetup;
using WebApi.Application.BookOperations.Commands.DeleteBook;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using Xunit;
using static WebApi.Application.BookOperations.Commands.UpdateBook.UpdateBookCommand;

namespace Tests.Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandValidatorTests: IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("Lord Of The",0,0)]
        [InlineData("Lord Of The",1,0)]
        [InlineData("Lord Of The",0,1)]
        [InlineData("",0,0)]
        [InlineData("",1,1)]
        [InlineData("",0,1)]
        [InlineData("Lor",1,1)]
        [InlineData("Lord",0,1)]
        [InlineData("Lord",1,0)]
        [InlineData(" ",1,1)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string title, int genreId , int authorId)
        {
            //Arrange
            UpdateBookCommand command = new UpdateBookCommand(null);
            command.Model = new UpdateBookModel()
            {
                Title = title,
                GenreId = genreId,
                AuthorId = authorId
            };
            //Act
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var result = validator.Validate(command);
            //Assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenLessThanFourCharacterInputToTitle_Validator_ShouldBeReturnError()
        {
            //Arrange
            UpdateBookCommand command = new UpdateBookCommand(null);
            command.Model = new UpdateBookModel(){Title = "Tes"};
            //Act
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var result = validator.Validate(command);
            //Assert
            result.Errors.Count.Should().BeGreaterThan(0);
            
        }
    }
}

