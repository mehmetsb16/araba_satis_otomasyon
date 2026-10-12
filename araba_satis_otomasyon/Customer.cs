using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;


namespace araba_satis_otomasyon
{
    [Table("Table_Customer")]
    internal class Customer
    {

        [Key]
        public int Customer_Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Customer_Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string Customer_Surname { get; set; }

        [Required]
        [MaxLength(50)]
        public string Customer_Email { get; set; }

        [Required]
        [MaxLength(50)]
        public string Customer_Telephone { get; set; }

        public virtual ICollection<CustomerCar> CustomerCars { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Category> Categories { get; set; }



    }

}
