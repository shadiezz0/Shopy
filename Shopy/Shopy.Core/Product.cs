using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopy.Core
{
    public class Product
    {
        [Key]
        [Display(Name = "ID")]
        public int ProductId { get; set; }
        
        [Display(Name = "Product")]
        [Column(TypeName = "varchar(200)")]
        public string ProductName { get; set; } = string.Empty;

        [Column(TypeName = "decimal(12,2)")]
        public decimal Price { get; set; }

        [Column(TypeName = "varchar(250)")]
        public string Image { get; set; } = string.Empty;

        [MaxLength(50)]
        [MinLength(1)]
        public string Discription { get; set; } = string.Empty;

        [Display(Name = "Category")]
        public int Category_Id { get; set; }

        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }
    }
}
