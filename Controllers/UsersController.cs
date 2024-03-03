using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PruebasArticulos.Web.Models;
using System.Text;

namespace PruebasArticulos.Web.Controllers
{
    public class UsersController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7118/api");
        private readonly HttpClient _client;

        public UsersController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<UsersViewModel> UserList = new List<UsersViewModel>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Users/GetUsers").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                UserList = JsonConvert.DeserializeObject<List<UsersViewModel>>(data);

            }

            return View(UserList);
        }

        [HttpGet]
        public IActionResult SaveUsers()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SaveUsers(UsersViewModel cart)
        {
            try
            {
                string data = JsonConvert.SerializeObject(cart);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/Users/CreateUser", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "User created.";
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
    }
}
