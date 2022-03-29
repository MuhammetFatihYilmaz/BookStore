using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using Tests.TestSetup;
using WebApi;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using WebApi.Application.GenreOperations.Commands.DeleteGenre;
using WebApi.Entities;
using Xunit;

namespace Tests.Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommandTests: IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        public DeleteGenreCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenThereIsNoGenreToDeleteByEnteredId_InvalidOperationExeption_ShouldBeReturn()
        {
            //Arrange
            DeleteGenreCommand command = new DeleteGenreCommand(_context);
            int testBookId = _context.Genres.Count()+1;
            command.GenreId = testBookId;
            
            //Act & Assert
            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap Türü Bulunamadı.");
        }

        [Fact]
        public void WhenValidInputToDelete_Genre_ShouldBeDeleted()
        {
            //Arrange
            DeleteGenreCommand command = new DeleteGenreCommand(_context);
            int testBookId = _context.Books.Count();

            command.GenreId = testBookId;
            
            //Act & Assert
            FluentActions
                .Invoking(()=> command.Handle());
        }
    }

}