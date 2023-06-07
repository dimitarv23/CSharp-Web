namespace AspNetCoreDemo.Controllers
{
    using AspNetCoreDemo.ViewModels;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Net.Http.Headers;
    using Newtonsoft.Json;
    using System.Text;

    public class ProductController : Controller
    {
        private List<ProductViewModel> products
            = new List<ProductViewModel>()
            {
                new ProductViewModel()
                {
                    Id = 1,
                    Name = "Cheese",
                    Price = (decimal)6.99
                },
                new ProductViewModel()
                {
                    Id = 2,
                    Name = "Ham",
                    Price = (decimal)5.49
                },
                new ProductViewModel()
                {
                    Id = 3,
                    Name = "Bread",
                    Price = (decimal)1.49
                }
            };

        [Route("Product/All")]
        [Route("Product/My-Products")]
        public IActionResult All(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return View(this.products);
            }

            var productsToReturn = products
                .Where(p => p.Name.ToLower()
                    .Contains(keyword.ToLower()))
                .ToList();

            return View(productsToReturn);
        }

        public IActionResult ById(int pId)
        {
            var product = this.products
                .FirstOrDefault(p => p.Id == pId);

            if (product == null)
            {
                return BadRequest();
            }

            return View(product);
        }

        public ActionResult<string> AllAsJson()
        {
            return JsonConvert.SerializeObject(
                this.products, new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented
                });
        }

        public ActionResult<string> AllAsText()
        {
            var result = new StringBuilder();

            foreach (var p in this.products)
            {
                result.AppendLine($"Product {p.Id}: {p.Name} - {p.Price:f2} lv.");
            }

            return result.ToString().TrimEnd();
        }

        public ActionResult AllAsTextFile()
        {
            var result = new StringBuilder();

            foreach (var p in this.products)
            {
                result.AppendLine($"Product {p.Id}: {p.Name} - {p.Price:f2} lv.");
            }

            Response.Headers.Add(HeaderNames.ContentDisposition,
                @"attachment;filename=products.txt");
            var returnText = result.ToString().TrimEnd();

            return File(Encoding.UTF8.GetBytes(returnText),
                "text/plain");
        }
    }
}