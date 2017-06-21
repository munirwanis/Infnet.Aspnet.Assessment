using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infnet.Aspnet.Assessment.Data
{
    public class DataAuthors : IData<Author>
    {
        public bool Delete(int id)
        {
            using (var db = new LibraryDataModel())
            {
                var author = db.Author.Find(id);
                if (author == null) { return false; }
                db.Author.Remove(author);
                return db.SaveChanges() > 0;
            }
        }

        public Author Get(int id)
        {
            using (var db = new LibraryDataModel())
            {
                var author = db.Author.Find(id);
                return author;
            }
        }

        public List<Author> GetAll()
        {
            using (var db = new LibraryDataModel())
            {
                return db.Author.ToList();
            }
        }

        public bool Insert(Author entry)
        {
            using (var db = new LibraryDataModel())
            {
                db.Author.Add(entry);
                return db.SaveChanges() > 0;
            }
        }

        public bool Update(int id, Author entry)
        {
            using (var db = new LibraryDataModel())
            {
                db.Author.Attach(entry);
                db.Entry(entry).State = EntityState.Modified;
                return db.SaveChanges() > 0;
            }
        }
    }
}
