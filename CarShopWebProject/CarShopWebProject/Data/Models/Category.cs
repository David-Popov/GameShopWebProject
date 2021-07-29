using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarShopWebProject.Data.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        IEnumerable<Product> Products { get; init; } = new List<Product>();
    }
}
