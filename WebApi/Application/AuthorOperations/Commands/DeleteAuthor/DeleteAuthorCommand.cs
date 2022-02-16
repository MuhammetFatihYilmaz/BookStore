using System;
using System.Linq;

namespace WebApi.Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommand
    {
        private readonly BookStoreDbContext _context;
        public int AuthorId { get; set; }
        public DeleteAuthorCommand(BookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var author = _context.Authors.SingleOrDefault(x=> x.Id == AuthorId);
            if(author is null)
                throw new InvalidOperationException("Silinecek yazar bulunamadı.");

            var isAuthorBookPublish = _context.Books.SingleOrDefault(x=> x.AuthorId == AuthorId);
            if(isAuthorBookPublish is not null)
                throw new InvalidOperationException("Yazarın yayında kitabı var. Öncelikle kitabının silinmesi gerekli.");

            _context.Authors.Remove(author);
            _context.SaveChanges();
        }
    }
}