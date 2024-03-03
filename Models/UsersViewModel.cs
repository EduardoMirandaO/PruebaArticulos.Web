using System.ComponentModel.DataAnnotations;

namespace PruebasArticulos.Web.Models
{
    public class UsersViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Boolean isAdmin {get; set; }
        public string email { get; set; }
        public string Password { get; set; }
    }
}
