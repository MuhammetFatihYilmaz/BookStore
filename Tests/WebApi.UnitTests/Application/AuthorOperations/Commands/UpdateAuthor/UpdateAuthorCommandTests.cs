using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using Tests.TestSetup;
using WebApi;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using Xunit;

namespace Tests.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandTests: IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public UpdateAuthorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenThereIsNoAuthorToUpdateByEnteredId_InvalidOperationExeption_ShouldBeReturn()
        {
            //Arrange
            UpdateAuthorCommand command = new UpdateAuthorCommand(_context,_mapper);
            int testAuthorId = _context.Authors.Count()+2;
            command.AuthorId = testAuthorId;
            
            //Act & Assert
            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Güncellenecek yazar bulunamadı.");
        }

        [Fact]
        public void WhenLessThanThreeCharacterInputToName_InvalidOperationExeption_ShouldBeReturn()
        {
            //Arrange
            UpdateAuthorCommand command = new UpdateAuthorCommand(_context,_mapper);
            command.Model = new UpdateAuthorModel(){Name = "Te"};
            //Act & Assert
            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>("Yazar ismi üç karakterden büyük veya eşit olmalıdır. ");
        }
    }

}