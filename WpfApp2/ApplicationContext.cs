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
        }
        public DbSet<Phone> Phones { get; set; }
        public DbSet<Clients> Clients { get; set; }

        public void kek()
        {

            var phones = from p in Phones
                         where p.Id == 1
                         select p;
        }

    }
}



