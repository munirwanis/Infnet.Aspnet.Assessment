using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AuthorEntity = Infnet.Aspnet.Assessment.Entities.Author;
using BookEntity = Infnet.Aspnet.Assessment.Entities.Book;

namespace Infnet.Aspnet.Assessment.Data
{
    public class DataBooks : IData<BookEntity>
    {
        public bool Delete(int id)
        {
            using (var db = new LibraryDataModel())
            {
                var book = db.Books.Find(id);
                if (book == null) { return false; }
                var authorBook = (from ab in db.AuthorBook where ab.BookId == id select ab).ToList();
                authorBook.ForEach(ab => db.AuthorBook.Remove(ab));
                db.Books.Remove(book);
                return db.SaveChanges() > 0;
            }
        }

        public BookEntity Get(int id)
        {
            using (var db = new LibraryDataModel())
            {
                var book = (from b in db.Books
                            where b.Id == id
                            select new BookEntity
                            {
                                Id = b.Id,
                                Isbn = b.Isbn,
                                LauchDate = b.LauchDate,
                                Title = b.Title,
                                Authors = (from a in db.Author
                                           join ab in db.AuthorBook on b.Id equals ab.BookId into abs
                                           from ab in abs.DefaultIfEmpty()
                                           where a.Id == ab.AuthorId
                                           select new AuthorEntity
                                           {
                                               Birthdate = a.Birthdate,
                                               Email = a.Email,
                                               Id = a.Id,
                                               LastName = a.LastName,
                                               Name = a.Name
                                           }).DefaultIfEmpty().ToList()
                            }).SingleOrDefault();
                return book;
            }
        }

        public List<BookEntity> GetAll()
        {
            using (var db = new LibraryDataModel())
            {
                var books = (from b in db.Books
                             select new BookEntity
                             {
                                 Id = b.Id,
                                 Isbn = b.Isbn,
                                 LauchDate = b.LauchDate,
                                 Title = b.Title,
                                 Authors = (from a in db.Author
                                            join ab in db.AuthorBook on b.Id equals ab.BookId into abs
                                            from ab in abs.DefaultIfEmpty()
                                            where a.Id == ab.AuthorId
                                            select new AuthorEntity
                                            {
                                                Birthdate = a.Birthdate,
                                                Email = a.Email,
                                                Id = a.Id,
                                                LastName = a.LastName,
                                                Name = a.Name
                                            }).DefaultIfEmpty().ToList()
                             }).ToList();
                return books;
            }
        }

        public bool Insert(BookEntity entry)
        {
            var book = MapBook(entry);
            using (var db = new LibraryDataModel())
            {
                db.Books.Add(book);
                var success = db.SaveChanges() > 0;
                if (success) { entry.Id = book.Id; }
                return success;
            }
        }

        public bool Update(int id, BookEntity entry)
        {
            var book = MapBook(entry);
            using (var db = new LibraryDataModel())
            {
                db.Books.Attach(book);
                db.Entry(entry).State = EntityState.Modified;
                return db.SaveChanges() > 0;
            }
        }

        private Books MapBook(BookEntity entry)
        {
            var book = new Books
            {
                Isbn = entry.Isbn,
                LauchDate = entry.LauchDate,
                Title = entry.Title,
                Id = entry.Id
            };

            if (entry.Authors != null && entry.Authors.Count > 0)
            {
                var authorBooks = new List<AuthorBook>();
                foreach (var author in entry.Authors)
                {
                    var authorBook = new AuthorBook
                    {
                        AuthorId = author.Id,
                        BookId = entry.Id
                    };
                    authorBooks.Add(authorBook);
                }
                book.AuthorBook = authorBooks;
            }

            return book;
        }
    }
}
