using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using Tests.TestSetup;
using WebApi;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetail;
using Xunit;

namespace Tests.Application.AuthorOperations.Queries.GetAuthorDetail
{
    public class GetAuthorDetailQueryTests: IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetAuthorDetailQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenThereIsNoAuthorToGetDetailByEnteredId_InvalidOperationExeption_ShouldBeReturn()
        {
            //Arrange
            GetAuthorDetailQuery command = new GetAuthorDetailQuery(_context,_mapper);
            int testAuthorId = _context.Books.Count()+4;
            command.AuthorId = testAuthorId;
            
            //Act & Assert
            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>("Yazar Bulunamadı.");
        }
    }

}