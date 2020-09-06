using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp2.Entity;

namespace WpfApp2
{
    class ApplicationContext : DbContext
    {
        public ApplicationContext() : base("DefaultConnection")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }
        public DbSet<Phone> Phones { get; set; }

        public DbSet<Clients> Clients { get; set; }

        public DbSet<RoomTypes> RoomTypes { get; set; }
        public DbSet<Rooms> Rooms { get; set; }

        public DbSet<Order_entity> Orders { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Rooms>()
            .HasOptional(p => p.RoomTypes)
            .WithMany(b => b.Rooms)
            .HasForeignKey( r => r.RoomTypesId);
        }

        public void kek()
        {

            var phones = from p in Phones
                         where p.Id == 1
                         select p;
        }

    }
}



