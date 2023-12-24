using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PTPM.Models;
using System.Diagnostics;
using static System.Net.WebRequestMethods;

namespace PTPM.Controllers
{
    public class ProductController : Controller
    {
        private readonly PtpmContext _context;

        private readonly IHttpClientFactory _httpClientFactory;
        public ProductController(PtpmContext context, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;

        }

        public async Task<IActionResult> Index()
        {
            List<Product> productList = new List<Product>();

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var u = "https://192.168.217.153/api/ProductApi/4";
                    var api = Url.Action("Get", "ProductAPI", new { Id = 4 }, Request.Scheme);
                    HttpResponseMessage response = await client.GetAsync(api);
                    if (response.IsSuccessStatusCode)
                    {
                        string jsonContent = await response.Content.ReadAsStringAsync();
                        Console.WriteLine(jsonContent);
                        Product product = JsonConvert.DeserializeObject<Product>(jsonContent);

                        // Add the product to the list
                        productList.Add(product);
                    }
                    else
                    {
                        throw new Exception($"Failed to fetch product list. Status code: {response.StatusCode}");
                    }
                }
                catch (HttpRequestException ex)
                {
                    // Log the error or handle it accordingly
                    Console.WriteLine($"HTTP request error: {ex.Message}");
                    // Optionally, return an error view or redirect to a specific error page
                    // return View("Error"); // You should have an "Error" view in your Views folder
                    // Or redirect to a specific action that handles errors
                    // return RedirectToAction("HandleError", "Error"); // Adjust accordingly
                }
            }

            // Continue with the rest of your code
            return View(productList);
        }



        public IActionResult Detail(int id)
        {
            var product = _context.Products.Include(x => x.Cat).FirstOrDefault(x => x.ProductId == id);
            if (product == null)
            {
                return RedirectToAction("Index");
            }

            return View(product);
        }
    }
}