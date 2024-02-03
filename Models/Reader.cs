using System;
using System.Collections.Generic;

namespace store_procedure.Models
{
    public partial class Reader
    {
        public Reader()
        {
            Borrowings = new HashSet<Borrowing>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime? DateOfBirth { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<Borrowing> Borrowings { get; set; }
    }
}
