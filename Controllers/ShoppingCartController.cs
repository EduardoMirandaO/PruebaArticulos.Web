using Microsoft.AspNetCore.Mvc;
using PruebasArticulos.Web.Models;
using Newtonsoft.Json;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PruebasArticulos.Web.Controllers
{
    public class ShoppingCartController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7118/api");
        private readonly HttpClient _client;

        public ShoppingCartController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        public IActionResult Index()
        {
            List<ShoppingCartViewModel> shoppingCartsList = new List<ShoppingCartViewModel>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/ShoppingCart/GetShoppingCarts").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                shoppingCartsList = JsonConvert.DeserializeObject<List<ShoppingCartViewModel>>(data);

            }

            return View(shoppingCartsList);
        }

        public IActionResult SaveShoppingCart()
        {
            return View();
        }

        public IActionResult SaveShoppingCart(ShoppingCartViewModel cart)
        {
            try
            {
                string data = JsonConvert.SerializeObject(cart);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/ShoppingCart/CreateShoppingCart", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Shopping cart created.";
                    return RedirectToAction("Index");

                }

            }
            catch (Exception ex) {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
            return View();
        }

        public void populateProductList() 
        {
            List<ProductsViewModel> ProductsList = new List<ProductsViewModel>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Products/GetProducts").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                ProductsList = JsonConvert.DeserializeObject<List<ProductsViewModel>>(data);
                //IEnumerable<SelectListItem> produc
            }

        }

        public IActionResult DeleteShoppingCart(int shoppingCartId)
        {
            try
            {
                HttpResponseMessage response = _client.DeleteAsync(_client.BaseAddress + $"/ShoppingCart/DeleteShoppingCartById/{shoppingCartId}").Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Shopping cart deleted.";
                    return View("Index");
                }
            }
            catch (Exception ex) {
                TempData["errorMessage"] = ex.Message;
                return View("Index");
            }
            return View("Index");
        }

    }
}

