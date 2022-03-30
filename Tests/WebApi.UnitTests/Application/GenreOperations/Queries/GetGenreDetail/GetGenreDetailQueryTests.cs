using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using Tests.TestSetup;
using WebApi;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using Xunit;

namespace Tests.Application.GenreOperations.Queries.GetGenreDetail
{
    public class GetGenreDetailQueryTests: IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetGenreDetailQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenThereIsNoGenreToGetDetailByEnteredId_InvalidOperationExeption_ShouldBeReturn()
        {
            //Arrange
            GetGenreDetailQuery command = new GetGenreDetailQuery(_context,_mapper);
            int testGenreId = _context.Genres.Count()+1;
            command.GenreId = testGenreId;
            
            //Act & Assert
            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>("Kitap Türü Bulunamadı.");
        }
    }

}