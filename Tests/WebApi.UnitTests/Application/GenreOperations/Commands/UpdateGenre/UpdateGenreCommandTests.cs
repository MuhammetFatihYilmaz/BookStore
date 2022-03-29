using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using Tests.TestSetup;
using WebApi;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using WebApi.Application.GenreOperations.Commands.DeleteGenre;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using WebApi.Entities;
using Xunit;

namespace Tests.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandTests: IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        public UpdateGenreCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenAlreadyExistGenreNameGivenForUpdate_InvalidOperationExeption_ShouldBeReturn()
        {
            //Arrange
            var genre = new Genre(){Name="WhenAlreadyExistGenreNameGivenForUpdate_InvalidOperationExeption_ShouldBeReturn", IsActive = true};
            _context.Genres.Add(genre);
            _context.SaveChanges();
            
            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            command.Model = new UpdateGenreModel(){Name = genre.Name};
            //Act & Assert
            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>("Aynı isimli bir kitap türü zaten mevcut.");
        }

        [Fact]
        public void WhenThereIsNoGenreToUpdateByEnteredId_InvalidOperationExeption_ShouldBeReturn()
        {
            //Arrange
            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            int testBookId = _context.Genres.Count()+1;
            command.GenreId = testBookId;
            
            //Act & Assert
            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap Türü Bulunamadı");
        }
    }

}