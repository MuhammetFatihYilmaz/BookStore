using AutoMapper;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetail;
using WebApi.Application.AuthorOperations.Queries.GetAuthors;
using WebApi.Application.BookOperations.Queries.GetBookDetail;
using WebApi.Application.BookOperations.Queries.GetBooks;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.Application.GenreOperations.Queries.GetGenres;
using WebApi.Entities;
using static WebApi.Application.BookOperations.Commands.CreateBook.CreateBookCommand;

namespace WebApi.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBookModel, Book>();//Source-Target
            
            CreateMap<Book,BookDetailViewModel>().ForMember(dest=> dest.Genre, opt=> opt.MapFrom(src => src.Genre.Name))
            .ForMember(dest=> dest.Author, opt=> opt.MapFrom(src=> src.Author.Name + " " + src.Author.Surname));

            CreateMap<Book,BooksViewModel>().ForMember(dest=> dest.Genre, opt=> opt.MapFrom(src => src.Genre.Name))
            .ForMember(dest=> dest.Author, opt=> opt.MapFrom(src=> src.Author.Name + " " + src.Author.Surname));
            //
            CreateMap<Genre, GenresViewModel>();
            CreateMap<Genre, GenreDetailViewModel>();
            //
            CreateMap<Author,AuthorsViewModel>();
            CreateMap<Author,AuthorDetailViewModel>();
            CreateMap<CreateAuthorModel, Author>();
        }
    }
}