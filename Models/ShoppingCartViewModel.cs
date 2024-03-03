using System.ComponentModel.DataAnnotations;

namespace PruebasArticulos.Web.Models
{
    public class ShoppingCartViewModel
    {
        public int Id { get; set; }
        public double Total { get; set; }
        public DateTime CreatedOn { get; set; }
        public int UserId { get; set; }
        public int FirstName { get; set; }
        public int LastName { get; set; }

        public virtual ICollection<ShoppingCartItemViewModel> ShoppingCartItems { get; set; }

    }
}
