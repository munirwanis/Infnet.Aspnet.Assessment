namespace Infnet.Aspnet.Assessment.Data
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class LibraryDataModel : DbContext
    {
        public LibraryDataModel()
            : base("name=LibraryDataModel")
        {
        }

        public virtual DbSet<Author> Author { get; set; }
        public virtual DbSet<AuthorBook> AuthorBook { get; set; }
        public virtual DbSet<Books> Books { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Author>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<Author>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Author>()
                .HasMany(e => e.AuthorBook)
                .WithRequired(e => e.Author)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Books>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<Books>()
                .Property(e => e.Isbn)
                .IsUnicode(false);

            modelBuilder.Entity<Books>()
                .HasMany(e => e.AuthorBook)
                .WithRequired(e => e.Books)
                .HasForeignKey(e => e.BookId)
                .WillCascadeOnDelete(false);
        }
    }
}
