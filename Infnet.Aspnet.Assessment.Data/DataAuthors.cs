using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AuthorEntity = Infnet.Aspnet.Assessment.Entities.Author;
using BookEntity = Infnet.Aspnet.Assessment.Entities.Book;

namespace Infnet.Aspnet.Assessment.Data
{
    public class DataAuthors : IData<AuthorEntity>
    {
        public bool Delete(int id)
        {
            using (var db = new LibraryDataModel())
            {
                var author = db.Author.Find(id);
                if (author == null) { return false; }
                var authorBook = (from ab in db.AuthorBook where ab.AuthorId == id select ab).ToList();
                authorBook.ForEach(ab => db.AuthorBook.Remove(ab));
                db.Author.Remove(author);
                return db.SaveChanges() > 0;
            }
        }

        public AuthorEntity Get(int id)
        {
            using (var db = new LibraryDataModel())
            {
                var author = (from a in db.Author
                              join ab in db.AuthorBook on a.Id equals ab.AuthorId
                              where a.Id == id
                              select new AuthorEntity
                              {
                                  Birthdate = a.Birthdate,
                                  Email = a.Email,
                                  Id = a.Id,
                                  LastName = a.LastName,
                                  Name = a.Name,
                                  Books = (from b in db.Books
                                           where b.Id == ab.BookId
                                           select new BookEntity
                                           {
                                               Id = b.Id,
                                               Isbn = b.Isbn,
                                               LauchDate = b.LauchDate,
                                               Title = b.Title,
                                               Authors = new List<AuthorEntity>()
                                           }).ToList()
                              }).SingleOrDefault();
                return author;
            }
        }

        public List<AuthorEntity> GetAll()
        {
            using (var db = new LibraryDataModel())
            {
                var authors = (from a in db.Author
                               join ab in db.AuthorBook on a.Id equals ab.AuthorId
                               select new AuthorEntity
                               {
                                   Birthdate = a.Birthdate,
                                   Email = a.Email,
                                   Id = a.Id,
                                   LastName = a.LastName,
                                   Name = a.Name,
                                   Books = (from b in db.Books
                                            where b.Id == ab.BookId
                                            select new BookEntity
                                            {
                                                Id = b.Id,
                                                Isbn = b.Isbn,
                                                LauchDate = b.LauchDate,
                                                Title = b.Title,
                                                Authors = new List<AuthorEntity>()
                                            }).ToList()
                               }).ToList();
                return authors;
            }
        }

        public bool Insert(AuthorEntity entry)
        {
            var author = MapAuthor(entry);
            using (var db = new LibraryDataModel())
            {
                db.Author.Add(author);
                return db.SaveChanges() > 0;
            }
        }

        public bool Update(int id, AuthorEntity entry)
        {
            var author = MapAuthor(entry);
            using (var db = new LibraryDataModel())
            {
                db.Author.Attach(author);
                db.Entry(entry).State = EntityState.Modified;
                return db.SaveChanges() > 0;
            }
        }

        private Author MapAuthor(AuthorEntity entry)
        {
            var authorBooks = new List<AuthorBook>();
            foreach (var book in entry.Books)
            {
                var authorBook = new AuthorBook
                {
                    AuthorId = entry.Id,
                    Author = new Author
                    {
                        Id = entry.Id,
                        Birthdate = entry.Birthdate,
                        Email = entry.Email,
                        LastName = entry.LastName,
                        Name = entry.Name
                    },
                    BookId = book.Id,
                    Books = new Books
                    {
                        Id = book.Id,
                        Isbn = book.Isbn,
                        LauchDate = book.LauchDate,
                        Title = book.Title
                    }
                };
                authorBooks.Add(authorBook);
            }
            var author = new Author
            {
                Name = entry.Name,
                LastName = entry.LastName,
                Email = entry.Email,
                Birthdate = entry.Birthdate,
                Id = entry.Id,
                AuthorBook = authorBooks
            };

            return author;
        }
    }
}
