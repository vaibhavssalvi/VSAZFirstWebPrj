namespace MyFirstWeb.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Students
    {
        public int Id { get; set; }

        [Required] [StringLength(50)]
        public string Name { get; set; }

        [Required] [Range(1,1000)]
        public int? TotalMarks { get; set; }

        public int? Rating { get; set; }

        public virtual Ratings Ratings { get; set; }
    }
}
