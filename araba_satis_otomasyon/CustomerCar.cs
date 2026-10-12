using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace araba_satis_otomasyon
{
    [Table("Table_CustomerCar")]
    internal class CustomerCar
    {
        
        
            [Key]

            public int Cuscar_Id { get; set; }

            [ForeignKey("Customer")]
            public int Customer_Id { get; set; }

            [ForeignKey("Car")]
            public int Car_Id { get; set; }

            public virtual Customer Customer { get; set; }
            public virtual Car Car { get; set; }
 
        public DbSet<Category> Categories { get; set; }
        public DbSet<CustomerCar> CustomerCars { get; set; }
    }
    }



