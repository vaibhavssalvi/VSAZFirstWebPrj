namespace MyFirstWeb.Controllers
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model : DbContext
    {
        public Model()
            : base("name=ConnMyWebNewDB")
        {
        }

        public virtual DbSet<Ratings> Ratings { get; set; }
        public virtual DbSet<Students> Students { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ratings>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Ratings>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Ratings>()
                .HasMany(e => e.Students)
                .WithOptional(e => e.Ratings)
                .HasForeignKey(e => e.Rating);
        }
    }
}
