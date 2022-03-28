using System;
using AutoMapper;
using FluentAssertions;
using Tests.TestSetup;
using WebApi;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using Xunit;
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

        [Fact]
        public void WhenBookGenreIdEqualOrLessThanZero_InvalidOperationExeption_ShouldBeReturn()
        {
            //Arrange
            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.Model = new UpdateBookModel(){GenreId = -1};
            //Act & Assert
            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>("Kitabın türü sıfırdan büyük olmalıdır. ");
        }
    }

}