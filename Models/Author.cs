using System;
using System.Collections.Generic;

namespace store_procedure.Models
{
    public partial class Author
    {
        public Author()
        {
            Books = new HashSet<Book>();
        }
        
        public int Id { get; set; }
        public string AuthorName { get; set; } = null!;
        public DateTime? BirthdayName { get; set; }
        public string? Bio { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public virtual ICollection<Book> Books { get; set; }
    }
}
