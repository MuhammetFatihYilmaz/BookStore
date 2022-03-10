using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using Tests.TestSetup;
using WebApi;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.Entities;
using Xunit;
using static WebApi.Application.BookOperations.Commands.CreateBook.CreateBookCommand;

namespace Tests.Application.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandTests: IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateBookCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenAlreadyExistBookTitleGiven_InvalidOperationExeption_ShouldBeReturn()
        {
            //Arrange
            var book = new Book(){Title="Test_WhenAlreadyExistBookTitleGiven_InvalidOperationExeption_ShouldBeReturn", PageCount=100, PublishDate=new DateTime(1990,01,12),GenreId = 1};
            _context.Books.Add(book);
            _context.SaveChanges();
            
            CreateBookCommand command = new CreateBookCommand(_context,_mapper);
            command.Model = new CreateBookModel(){Title = book.Title};
            //Act & Assert
            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap zaten mevcut");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Book_ShouldBeCreated()
        {
            CreateBookCommand command = new CreateBookCommand(_context,_mapper);
            CreateBookModel model = new CreateBookModel()
            {
                Title ="Hobbit",
                PageCount = 1000,
                PublishDate = DateTime.Now.Date.AddYears(-10),
                GenreId = 1
            };
            command.Model = model;

            FluentActions.Invoking(()=> command.Handle()).Invoke();
            
            var book = _context.Books.SingleOrDefault(book=> book.Title == model.Title);

            book.Should().NotBeNull();
            book.PageCount.Should().Be(model.PageCount);
            book.PublishDate.Should().Be(model.PublishDate);
            book.GenreId.Should().Be(model.GenreId);

        }
    }
}