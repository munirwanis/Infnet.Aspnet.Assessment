using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Infnet.Aspnet.Assessment.Entities;
using Infnet.Aspnet.Assessment.Presentation.Models;
using Infnet.Aspnet.Assessment.Presentation.Helper;
using RestSharp;

namespace Infnet.Aspnet.Assessment.Presentation.Controllers
{
    public class BooksController : Controller
    {
        private const string URI = "api/book";

        // GET: Books
        public ActionResult Index()
        {
            var books = RequestHelper.MakeRequest<List<Book>>(URI, Method.GET);
            return View(books);
        }

        // GET: Books/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var book = RequestHelper.MakeRequest<Book>($"{URI}/{id}", Method.GET);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: Books/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Isbn,LauchDate")] Book book)
        {
            if (ModelState.IsValid)
            {
                var newBook = RequestHelper.MakeRequest<Book>(URI, Method.POST, book);
                return RedirectToAction("Index");
            }

            return View(book);
        }

        // GET: Books/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var book = RequestHelper.MakeRequest<Book>($"{URI}/{id}", Method.GET);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Isbn,LauchDate")] Book book)
        {
            if (ModelState.IsValid)
            {
                var newBook = RequestHelper.MakeRequest<Book>($"{URI}/{book.Id}", Method.PUT, book);
                return RedirectToAction("Index");
            }
            return View(book);
        }

        // GET: Books/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var book = RequestHelper.MakeRequest<Book>($"{URI}/{id}", Method.GET);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var book = RequestHelper.MakeRequest<Book>($"{URI}/{id}", Method.DELETE);
            return RedirectToAction("Index");
        }
    }
}
