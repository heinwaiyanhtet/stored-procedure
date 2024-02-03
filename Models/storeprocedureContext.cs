using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace store_procedure.Models
{
    public partial class storeprocedureContext : DbContext
    {
        public storeprocedureContext()
        {
        }

        public storeprocedureContext(DbContextOptions<storeprocedureContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Author> Authors { get; set; } = null!;
        public virtual DbSet<Book> Books { get; set; } = null!;
        public virtual DbSet<BookGenre> BookGenres { get; set; } = null!;
        public virtual DbSet<Borrowing> Borrowings { get; set; } = null!;
        public virtual DbSet<Genre> Genres { get; set; } = null!;
        public virtual DbSet<Reader> Readers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL("Server=127.0.0.1;Port=3307;Database=store-procedure;User=hein;Password=hein@123;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>(entity =>
            {
                entity.ToTable("authors");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AuthorName)
                    .HasMaxLength(255)
                    .HasColumnName("authorName");

                entity.Property(e => e.Bio)
                    .HasColumnType("text")
                    .HasColumnName("bio");

                entity.Property(e => e.BirthdayName)
                    .HasColumnType("date")
                    .HasColumnName("birthdayName");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("createdAt")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnName("updatedAt")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.ToTable("books");

                entity.HasIndex(e => e.AuthorId, "author_id");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AuthorId).HasColumnName("author_id");

                entity.Property(e => e.Description)
                    .HasColumnType("text")
                    .HasColumnName("description");

                entity.Property(e => e.Image)
                    .HasColumnType("text")
                    .HasColumnName("image");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .HasColumnName("title");

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.AuthorId)
                    .HasConstraintName("books_ibfk_1");
            });

            modelBuilder.Entity<BookGenre>(entity =>
            {
                entity.ToTable("book_genres");

                entity.HasIndex(e => e.BookId, "book_id");

                entity.HasIndex(e => e.GenreId, "genre_id");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BookId).HasColumnName("book_id");

                entity.Property(e => e.GenreId).HasColumnName("genre_id");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.BookGenres)
                    .HasForeignKey(d => d.BookId)
                    .HasConstraintName("book_genres_ibfk_1");

                entity.HasOne(d => d.Genre)
                    .WithMany(p => p.BookGenres)
                    .HasForeignKey(d => d.GenreId)
                    .HasConstraintName("book_genres_ibfk_2");
            });

            modelBuilder.Entity<Borrowing>(entity =>
            {
                entity.ToTable("borrowings");

                entity.HasIndex(e => e.BookId, "bookId");

                entity.HasIndex(e => e.ReaderId, "readerId");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BookId).HasColumnName("bookId");

                entity.Property(e => e.BorrowDate)
                    .HasColumnType("date")
                    .HasColumnName("borrowDate");

                entity.Property(e => e.ReaderId).HasColumnName("readerId");

                entity.Property(e => e.ReturnDate)
                    .HasColumnType("date")
                    .HasColumnName("returnDate");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.Borrowings)
                    .HasForeignKey(d => d.BookId)
                    .HasConstraintName("borrowings_ibfk_2");

                entity.HasOne(d => d.Reader)
                    .WithMany(p => p.Borrowings)
                    .HasForeignKey(d => d.ReaderId)
                    .HasConstraintName("borrowings_ibfk_1");
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.ToTable("genres");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("createdAt")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.GenreName)
                    .HasMaxLength(255)
                    .HasColumnName("genreName");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnName("updatedAt")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            modelBuilder.Entity<Reader>(entity =>
            {
                entity.ToTable("readers");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .HasColumnName("address");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.DateOfBirth)
                    .HasColumnType("date")
                    .HasColumnName("dateOfBirth");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .HasColumnName("email");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnName("updated_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
