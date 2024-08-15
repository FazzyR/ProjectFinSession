using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectFinSession.Models
{
    public class cart
    {
        [Key]
        public int Id { get; set; }

        public List<Product> Products { get; set; } = new List<Product>();
    }
}
