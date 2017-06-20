using System;
using System.Collections.Generic;

namespace Infnet.Aspnet.Assessment.Entities
{
    public class Book
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Isbn { get; set; }

        public DateTime LauchDate { get; set; }

        public List<Author> Authors { get; set; }
    }
}
