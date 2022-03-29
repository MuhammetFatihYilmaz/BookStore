using System;
using AutoMapper;
using FluentAssertions;
using Tests.TestSetup;
using WebApi;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.Entities;
using Xunit;

namespace Tests.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandTests: IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateAuthorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenAlreadyExistAuthorNameGiven_InvalidOperationExeption_ShouldBeReturn()
        {
            //Arrange
            var author = new Author(){Name="WhenAlreadyExistAuthorNameGiven_InvalidOperationExeption_ShouldBeReturn", Surname = "Test Surname", BirthDate = DateTime.Now.Date.AddYears(-80) };
            _context.Authors.Add(author);
            _context.SaveChanges();
            
            CreateAuthorCommand command = new CreateAuthorCommand(_context,_mapper);
            command.Model = new CreateAuthorModel(){Name = author.Name, Surname = author.Surname, BirthDate = DateTime.Now.Date.AddYears(-10)};
            //Act & Assert
            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yazar zaten mevcut");
        }

    }

}