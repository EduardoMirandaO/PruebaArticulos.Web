using System.ComponentModel.DataAnnotations;

namespace PruebasArticulos.Web.Models
{
    public class ProductsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Cost { get; set; }
        public string Department { get; set; }

    }
}

