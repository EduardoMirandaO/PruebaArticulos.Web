using Microsoft.AspNetCore.Mvc;
using PruebasArticulos.Web.Models;
using Newtonsoft.Json;
using System.Text;

namespace PruebasArticulos.Web.Controllers
{
    public class ProductsController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7118/api");
        private readonly HttpClient _client;

        public ProductsController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<ProductsViewModel> ProductsList = new List<ProductsViewModel>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Products/GetProducts").Result;


            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                ProductsList = JsonConvert.DeserializeObject<List<ProductsViewModel>>(data);
            }

            return View(ProductsList);
        }
        [HttpGet]
        public IActionResult SaveProducts()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SaveProducts(ProductsViewModel cart)
        {
            try
            {
                string data = JsonConvert.SerializeObject(cart);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/Products/CreateProduct", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Product created.";
                    return RedirectToAction("Index");

                }

            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
            return View();
        }

        public IActionResult DeleteProduct(int id)
        {
            try
            {

                HttpResponseMessage response = _client.DeleteAsync(_client.BaseAddress + "/Products/DeleteProductById/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Product deleted.";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

    }
}
