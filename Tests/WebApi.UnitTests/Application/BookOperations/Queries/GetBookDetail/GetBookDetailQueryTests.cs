using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using Tests.TestSetup;
using WebApi;
using WebApi.Application.BookOperations.Queries.GetBookDetail;
using Xunit;

namespace Tests.Application.BookOperations.Queries.GetBookDetail
{
    public class GetBookDetailQueryTests: IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetBookDetailQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenThereIsNoBookToGetDetailByEnteredId_InvalidOperationExeption_ShouldBeReturn()
        {
            //Arrange
            GetBookDetailQuery command = new GetBookDetailQuery(_context,_mapper);
            int testBookId = _context.Books.Count()+1;
            command.BookId = testBookId;
            
            //Act & Assert
            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap bulunamadı.");
        }
    }

}