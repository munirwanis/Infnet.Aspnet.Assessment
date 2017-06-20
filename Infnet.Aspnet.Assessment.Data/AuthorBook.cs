namespace Infnet.Aspnet.Assessment.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AuthorBook")]
    public partial class AuthorBook
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int AuthorId { get; set; }

        public int BookId { get; set; }

        public virtual Author Author { get; set; }

        public virtual Books Books { get; set; }
    }
}
