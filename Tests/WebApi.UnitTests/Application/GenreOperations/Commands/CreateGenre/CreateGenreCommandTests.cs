using System;
using AutoMapper;
using FluentAssertions;
using Tests.TestSetup;
using WebApi;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using WebApi.Entities;
using Xunit;

namespace Tests.Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommandTests: IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateGenreCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        
        [Fact]
        public void WhenAlreadyExistGenreNameGiven_InvalidOperationExeption_ShouldBeReturn()
        {
            //Arrange
            var genre = new Genre(){Name="WhenAlreadyExistGenreNameGiven_InvalidOperationExeption_ShouldBeReturn"};
            _context.Genres.Add(genre);
            _context.SaveChanges();
            
            CreateGenreCommand command = new CreateGenreCommand(_context);
            command.Model = new CreateGenreModel(){Name = genre.Name};
            //Act & Assert
            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap Türü Zaten Mevcut.");
        }

    }

}