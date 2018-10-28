namespace NasasivustonAPI.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class NasaUserDB : DbContext
    {
        public NasaUserDB()
            : base("name=NasaUserDB")
        {
        }

        public virtual DbSet<User> User { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(e => e.Nickname)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Description)
                .IsUnicode(false);
        }
    }
}
