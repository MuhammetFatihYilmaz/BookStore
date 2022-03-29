using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using Tests.TestSetup;
using WebApi;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.Application.AuthorOperations.Commands.DeleteAuthor;
using WebApi.Entities;
using Xunit;

namespace Tests.Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandTests: IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public DeleteAuthorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenThereIsNoAuthorToDeleteByEnteredId_InvalidOperationExeption_ShouldBeReturn()
        {
            //Arrange
            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
            int testBookId = _context.Books.Count()+1;
            command.AuthorId = testBookId;
            
            //Act & Assert
            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Silinecek yazar bulunamadı.");
        }
        
        [Fact]
        public void WhenThereIsAPublishedBookAuthorToDelete_InvalidOperationExeption_ShouldBeReturn()
        {
            //Arrange
            var author = new Author(){Name="Test", Surname = "Test", BirthDate = DateTime.Now.Date.AddYears(-80) };
            _context.Authors.Add(author);
            _context.SaveChanges();

            var book = new Book(){Title = "Test Book", GenreId = 1 , AuthorId = author.Id, PageCount = 100, PublishDate = new DateTime(2002,05,24)};
            _context.Books.Add(book);
            _context.SaveChanges();
            
            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
            int testAuthorId = author.Id;
            command.AuthorId = testAuthorId;
            
            //Act & Assert
            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yazarın yayında kitabı var. Öncelikle kitabının silinmesi gerekli.");
        }


    }

}