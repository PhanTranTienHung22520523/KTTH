using Microsoft.AspNetCore.Mvc;
using CRUD.Data;
using CRUD.Models;
using System.Security.Cryptography.X509Certificates;
using System.Globalization;
namespace CRUD.Controllers
{
    public class ProductsController : Controller
    {

        private readonly AppDataContext context;
        private readonly IWebHostEnvironment environment;
        public ProductsController(AppDataContext context, IWebHostEnvironment environment)
        {
            this.context = context;
            this.environment = environment;
        }
        public IActionResult Index()
        {
            var product = context.Products.ToList();
            return View(product);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ProductDTO productdto)
        {
            if (productdto.FileImage == null)
            {
                ModelState.AddModelError("ImageFile", "Must be required");
            }

            if (!ModelState.IsValid)
            {
                return View(productdto);
            }

            string newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            newFileName += Path.GetExtension(productdto.FileImage!.FileName);

            string imageFullPath=environment.WebRootPath + "/Image/"+newFileName;
            using (var stream = System.IO.File.Create(imageFullPath)) { 
                productdto.FileImage.CopyTo(stream);
             }

            Product product = new Product()
            {
                Bla_Name = productdto.Bla_Name,
                Brand = productdto.Brand,
                Category = productdto.Category,
                Price = productdto.Price,
                Description = productdto.Description,
                FileImage = newFileName
            };

            context.Products.Add(product);
            context.SaveChanges();


            return RedirectToAction("Index","Products");
        }
        public IActionResult Edit(int id)
        {
            var product = context.Products.Find(id);
            if (product == null)
            {
                return RedirectToAction("Index", "Products");
             }

            var productdto = new ProductDTO()
            {
                Bla_Name = product.Bla_Name,
                Brand = product.Brand,
                Category = product.Category,
                Price = product.Price,
                Description = product.Description
            };

            ViewData["ProductId"] = product.Id;
            ViewData["ImageFile"] = product.FileImage;

            return View(productdto);
        }

        [HttpPost]
        public IActionResult Edit(int id, ProductDTO productdto)
        {
            var product = context.Products.Find(id);
            if (product == null)
            {
				return RedirectToAction("Index", "Products");
			}

            if (!ModelState.IsValid)
            {
                ViewData["ProductId"] = product.Id;
                ViewData["ImageFile"] = product.FileImage;

                return View(productdto);
            }

            string newFileName = product.FileImage;

            if (productdto.FileImage != null)
            {
				newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
				newFileName += Path.GetExtension(productdto.FileImage!.FileName);

				string imageFullPath = environment.WebRootPath + "/Image/" + newFileName;
				using (var stream = System.IO.File.Create(imageFullPath))
				{
					productdto.FileImage.CopyTo(stream);
				}

                string old = environment.WebRootPath + "" + product.FileImage;
                System.IO.File.Delete(old);
			}

            product.Bla_Name = productdto.Bla_Name;
            product.Brand = productdto.Brand;
            product.Price = productdto.Price;
            product.Description = productdto.Description;
            product.Category = productdto.Category;


            context.SaveChanges();
            return RedirectToAction("Index", "Products");
		}

        public IActionResult Delete(int id)
        {
            var product = context.Products.Find(id);
            if (product == null)
            {
                return RedirectToAction("Index", "Products");
            }

            string imagefullpath=environment.WebRootPath +""+product.FileImage;
            System.IO.File.Delete(imagefullpath);
            context.Products.Remove(product);
            context.SaveChanges();
            return RedirectToAction("Index", "Products");
        }



    }
}
