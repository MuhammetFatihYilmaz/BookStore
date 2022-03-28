using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using Tests.TestSetup;
using WebApi;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.Application.BookOperations.Commands.DeleteBook;
using WebApi.Entities;
using Xunit;
using static WebApi.Application.BookOperations.Commands.CreateBook.CreateBookCommand;

namespace Tests.Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandTests: IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public DeleteBookCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenThereIsNoBookToDeleteByEnteredId_InvalidOperationExeption_ShouldBeReturn()
        {
            //Arrange
            DeleteBookCommand command = new DeleteBookCommand(_context);
            int testBookId = _context.Books.Count()+1;
            var book = _context.Books.SingleOrDefault(x=> x.Id == testBookId);

            command.BookId = testBookId;
            
            //Act & Assert
            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Silinecek kitap bulunamadı.");
        }

        [Fact]
        public void WhenValidInputToDelete_Book_ShouldBeDeleted()
        {
            //Arrange
            DeleteBookCommand command = new DeleteBookCommand(_context);
            int testBookId = _context.Books.Count();
            var book = _context.Books.SingleOrDefault(x=> x.Id == testBookId);

            command.BookId = testBookId;
            
            //Act & Assert
            FluentActions
                .Invoking(()=> command.Handle());
        }
    }
}