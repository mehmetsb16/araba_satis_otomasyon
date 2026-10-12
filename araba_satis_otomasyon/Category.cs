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
    [Table("Table_Category")]
    internal class Category
    {
        [Key]
        public int Category_Id { get; set; }

        [Required, MaxLength(50)]
        public string Category_Name { get; set; }

        public virtual ICollection<Car> Cars { get; set; }


    }

}
