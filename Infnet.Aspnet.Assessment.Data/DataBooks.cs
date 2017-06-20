using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infnet.Aspnet.Assessment.Data
{
    public class DataBooks : IData<Books>
    {
        public bool Delete(int id)
        {
            using (var db = new LibraryDataModel())
            {
                var book = db.Books.Find(id);
                if (book == null) { return false; }
                db.Books.Remove(book);
                return db.SaveChanges() > 0;
            }
        }

        public Books Get(int id)
        {
            using (var db = new LibraryDataModel())
            {
                var book = db.Books.Find(id);
                return book;
            }
        }

        public List<Books> GetAll()
        {
            using (var db = new LibraryDataModel())
            {
                return db.Books.ToList();
            }
        }

        public bool Insert(Books entry)
        {
            using (var db = new LibraryDataModel())
            {
                db.Books.Add(entry);
                return db.SaveChanges() > 0;
            }
        }

        public bool Update(int id, Books entry)
        {
            using (var db = new LibraryDataModel())
            {
                db.Books.Attach(entry);
                db.Entry(entry).State = EntityState.Modified;
                return db.SaveChanges() > 0;
            }
        }
    }
}
