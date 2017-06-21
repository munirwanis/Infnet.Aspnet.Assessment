using Infnet.Aspnet.Assessment.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using AuthorEntity = Infnet.Aspnet.Assessment.Entities.Author;
using BookEntity = Infnet.Aspnet.Assessment.Entities.Book;

namespace Infnet.Aspnet.Assessment.Api.Controllers
{
    public class BookController : ApiController
    {
        private DataBooks DbBook { get; } = new DataBooks();

        // GET: api/Book
        public IEnumerable<BookEntity> Get()
        {
            return DbBook.GetAll();
        }

        // GET: api/Book/5
        [ResponseType(typeof(BookEntity))]
        public IHttpActionResult Get(int id)
        {
            var book = DbBook.Get(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        // POST: api/Book
        [ResponseType(typeof(BookEntity))]
        public IHttpActionResult Post([FromBody]BookEntity book)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            var success = DbBook.Insert(book);

            if (!success) { return BadRequest(); }

            return CreatedAtRoute("DefaultApi", new { id = book.Id }, book);
        }

        // PUT: api/Book/5
        public IHttpActionResult Put(int id, [FromBody]BookEntity book)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            try
            {
                var success = DbBook.Update(id, book);
                if (success)
                {
                    return Ok();
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        // DELETE: api/Book/5
        public IHttpActionResult Delete(int id)
        {
            var success = DbBook.Delete(id);
            if (!success) { return NotFound(); }

            return Ok(success);
        }
    }
}
