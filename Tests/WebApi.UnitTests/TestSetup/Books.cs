using System;
using WebApi;
using WebApi.Entities;

namespace Tests.TestSetup
{
    public static class Books
    {
        public static void AddBooks(this BookStoreDbContext context)
        {
            context.Books.AddRange(
                    new Book{Title = "Lean Startup", GenreId = 1, AuthorId = 1, PageCount = 200, PublishDate = new DateTime(2002,05,24)},
                    new Book{Title = "Herland", GenreId = 2, AuthorId = 2, PageCount = 250, PublishDate = new DateTime(2015,01,19)},
                    new Book{Title = "Dune", GenreId = 2, AuthorId = 3, PageCount = 540, PublishDate = new DateTime(2010,07,16)}
                );
        }
    }
}