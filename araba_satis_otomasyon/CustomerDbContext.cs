using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace araba_satis_otomasyon
{
    internal class CustomerDbContext:DbContext
    {

        public CustomerDbContext() : base("name=CustomerDbContext")
        {

        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CustomerCar> CustomerCars { get; set; }
    }
}
