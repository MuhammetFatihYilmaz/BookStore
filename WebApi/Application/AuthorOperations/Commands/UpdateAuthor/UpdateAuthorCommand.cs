using System;
using System.Linq;
using AutoMapper;

namespace WebApi.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommand
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public int AuthorId { get; set; }
        public UpdateAuthorModel Model { get; set; }
        public UpdateAuthorCommand(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var author = _context.Authors.SingleOrDefault(x=> x.Id == AuthorId);
            if(author is null)
                throw new InvalidOperationException("Güncellenecek yazar bulunamadı.");

            author.Name = string.IsNullOrEmpty(Model.Name.Trim()) ? author.Name : Model.Name;
            author.Surname = string.IsNullOrEmpty(Model.Surname.Trim()) ? author.Name : Model.Name;
            author.BirthDate = string.IsNullOrEmpty(Model.BirthDate.Date.ToString()) ? author.BirthDate : Model.BirthDate;

            _context.SaveChanges();
        }
    }

    public class UpdateAuthorModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
    }
}