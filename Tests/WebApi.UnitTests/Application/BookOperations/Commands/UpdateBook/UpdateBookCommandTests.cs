using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using Tests.TestSetup;
using WebApi;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.Entities;
using Xunit;
using static WebApi.Application.BookOperations.Commands.CreateBook.CreateBookCommand;
using static WebApi.Application.BookOperations.Commands.UpdateBook.UpdateBookCommand;

namespace Tests.Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandTests: IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public UpdateBookCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

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
        public void WhenLessThanFourCharacterInputToTitle_InvalidOperationExeption_ShouldBeReturn()
        {
            //Arrange
            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.Model = new UpdateBookModel(){Title = "Tes"};
            //Act & Assert
            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>("Kitap ismi dört karakterden büyük veya eşit olmalıdır. ");
        }
    }

}