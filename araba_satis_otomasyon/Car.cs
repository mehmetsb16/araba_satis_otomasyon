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
    [Table("Table_Car")]
    internal class Car
    {
        [Key]
    public int Car_Id { get; set; }

        [Required, MaxLength(50)]
        public string Car_Name { get; set; }

        [Required]
        public decimal Car_Price { get; set; }

        [ForeignKey("Category")]
        public int Category_Id { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<CustomerCar> CustomerCars { get; set; }



    }

}

