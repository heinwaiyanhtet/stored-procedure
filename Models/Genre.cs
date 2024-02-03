using System;
using System.Collections.Generic;

namespace store_procedure.Models
{
    public partial class Genre
    {
        public Genre()
        {
            BookGenres = new HashSet<BookGenre>();
        }

        public int Id { get; set; }
        public string GenreName { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<BookGenre> BookGenres { get; set; }
    }
}
