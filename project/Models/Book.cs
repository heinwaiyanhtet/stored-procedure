using System;
using System.Collections.Generic;

namespace store_procedure.Models
{
    public partial class Book
    {
        public Book()
        {
            BookGenres = new HashSet<BookGenre>();
            Borrowings = new HashSet<Borrowing>();
        }

        public int Id { get; set; }
        public string? Title { get; set; }
        public int? AuthorId { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }

        public virtual Author? Author { get; set; }
        public virtual ICollection<BookGenre> BookGenres { get; set; }
        public virtual ICollection<Borrowing> Borrowings { get; set; }
    }
}
