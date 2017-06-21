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
    public class AuthorsController : ApiController
    {
        private DataAuthors DbAuthor { get; } = new DataAuthors();

        // GET: api/Authors
        public IEnumerable<AuthorEntity> Get()
        {
            return DbAuthor.GetAll();
        }

        // GET: api/Authors/5
        [ResponseType(typeof(AuthorEntity))]
        public IHttpActionResult Get(int id)
        {
            var author = DbAuthor.Get(id);
            if (author == null)
            {
                return NotFound();
            }
            return Ok(author);
        }

        // POST: api/Authors
        [ResponseType(typeof(AuthorEntity))]
        public IHttpActionResult Post([FromBody]AuthorEntity author)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var success = DbAuthor.Insert(author);

            if (!success) { return BadRequest(); }

            return CreatedAtRoute("DefaultApi", new { id = author.Id }, author);
        }

        // PUT: api/Authors/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, [FromBody]AuthorEntity author)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            try
            {
                var success = DbAuthor.Update(id, author);
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

        // DELETE: api/Authors/5
        [ResponseType(typeof(AuthorEntity))]
        public IHttpActionResult Delete(int id)
        {
            var success = DbAuthor.Delete(id);
            if (!success) { return NotFound(); }

            return Ok(success);
        }
    }
}
