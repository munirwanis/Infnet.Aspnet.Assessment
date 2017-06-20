﻿using System;
using System.Collections.Generic;

namespace Infnet.Aspnet.Assessment.Entities
{
    public class Author
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public DateTime Birthdate { get; set; }

        public List<Book> Books { get; set; }
    }
}
