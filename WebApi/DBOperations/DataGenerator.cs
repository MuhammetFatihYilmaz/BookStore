using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

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

                context.Books.AddRange(
                    new Book
                    {
                        Title = "Lean Startup",
                        GenreId = 1, //Personal Growth
                        PageCount = 200,
                        PublishDate = new DateTime(2002,05,24)
                    },
                    new Book
                    {
                        Title = "Herland",
                        GenreId = 2, //Science Fiction
                        PageCount = 250,
                        PublishDate = new DateTime(2015,01,19)
                    },
                    new Book
                    {
                        Title = "Dune",
                        GenreId = 2, //Noval
                        PageCount = 540,
                        PublishDate = new DateTime(2010,07,16)
                    }
                );

                context.SaveChanges();
                
            }
        }
    }
}