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
        private static bool _created = false;


        public ApplicationContext()
        {
            if (!_created)
            {
                _created = true;
                Database.EnsureDeleted();
                Database.EnsureCreated();
            }
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionbuilder)
        {
            optionbuilder.UseSqlite(@"Data Source=d:\Sample.db");
        }

        //public ApplicationContext() : base("DefaultConnection")
        //{
        //}
        public DbSet<Phone> Phones { get; set; }
        public DbSet<Clients> Clients { get; set; }
    }
}
