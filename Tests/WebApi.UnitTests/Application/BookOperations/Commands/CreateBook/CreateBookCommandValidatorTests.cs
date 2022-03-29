using System;
using FluentAssertions;
using Tests.TestSetup;
using WebApi.Application.BookOperations.Commands.CreateBook;
using Xunit;
using static WebApi.Application.BookOperations.Commands.CreateBook.CreateBookCommand;

namespace Tests.Application.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandValidatorTests: IClassFixture<CommonTestFixture>
    {

        [Theory]
        [InlineData("Lord Of The",0,0)]
        [InlineData("Lord Of The",0,1)]
        [InlineData("Lord Of The",100,0)]
        [InlineData("",0,0)]
        [InlineData("",100,1)]
        [InlineData("",0,1)]
        [InlineData("Lor",100,1)]
        [InlineData("Lord",100,0)]
        [InlineData("Lord",0,1)]
        [InlineData(" ",100,1)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string title, int pageCount, int genreId)
        {
            //Arrange
            CreateBookCommand command = new CreateBookCommand(null,null);
            command.Model = new CreateBookModel()
            {
                Title = title,
                PageCount = pageCount,
                PublishDate = DateTime.Now.Date.AddYears(-1),
                GenreId = genreId
            };
            //Act
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);
            //Assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError()
        {
            CreateBookCommand command = new CreateBookCommand(null,null);
            command.Model = new CreateBookModel()
            {
                Title = "Lord Of The Rings",
                PageCount = 100,
                PublishDate = DateTime.Now.Date,
                GenreId = 1
            };

            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            CreateBookCommand command = new CreateBookCommand(null,null);
            command.Model = new CreateBookModel()
            {
                Title = "Lotr",
                AuthorId = 1,
                PageCount = 1000,
                PublishDate = DateTime.Now.Date.AddYears(-1),
                GenreId = 2
            };

            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}