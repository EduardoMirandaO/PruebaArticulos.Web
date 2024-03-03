using System.ComponentModel.DataAnnotations;

namespace PruebasArticulos.Web.Models
{
    public class ShoppingCartItemViewModel
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int ShoppingCartId { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
    }
}
