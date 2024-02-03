using System;
using System.Collections.Generic;

namespace store_procedure.Models
{
    public partial class Borrowing
    {
        public int Id { get; set; }
        public int? ReaderId { get; set; }
        public int? BookId { get; set; }
        public DateTime? BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        public virtual Book? Book { get; set; }
        public virtual Reader? Reader { get; set; }
    }
}
