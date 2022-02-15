using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Entities;

namespace WebApi.DBOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using(var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                if(context.Books.Any())
                    return;

                context.Genres.AddRange(
                    new Genre
                    {
                        Name = "Personal Growth"
                    },
                    new Genre
                    {
                        Name = "Science Fiction"
                    },
                    new Genre
                    {
                        Name = "Novel"
                    }
                );

                context.Books.AddRange(
                    new Book
                    {
                        Title = "Lean Startup",
                        GenreId = 1, //Personal Growth
                        AuthorId = 1,
                        PageCount = 200,
                        PublishDate = new DateTime(2002,05,24)
                    },
                    new Book
                    {
                        Title = "Herland",
                        GenreId = 2, //Science Fiction
                        AuthorId = 2,
                        PageCount = 250,
                        PublishDate = new DateTime(2015,01,19)
                    },
                    new Book
                    {
                        Title = "Dune",
                        GenreId = 2, //Novel
                        AuthorId = 3,
                        PageCount = 540,
                        PublishDate = new DateTime(2010,07,16)
                    }
                );

                context.Authors.AddRange(
                    new Author
                    {
                        Name = "Eric",
                        Surname = "Ries",
                        BirthDate = new DateTime(1978,09,22)
                    },
                    new Author
                    {
                        Name = "Charlotte Perkins",
                        Surname = "Gilman",
                        BirthDate = new DateTime(1860,07,03)
                    },
                    new Author
                    {
                        Name = "Frank",
                        Surname = "Herbert",
                        BirthDate = new DateTime(1920,10,08)
                    }
                );

                context.SaveChanges();
                
            }
        }
    }
}