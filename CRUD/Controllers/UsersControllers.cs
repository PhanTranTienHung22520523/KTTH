using Microsoft.AspNetCore.Mvc;
using CRUD.Data;
using CRUD.Models;
using System.Security.Cryptography.X509Certificates;
using System.Globalization;


namespace CRUD.Controllers
{
    public class UsersControllers: Controller
    {
		private readonly AppDataContext context;
		private readonly IWebHostEnvironment environment;
		public UsersControllers(AppDataContext context, IWebHostEnvironment environment)
		{
			this.context = context;
			this.environment = environment;
		}
		public IActionResult Index()
		{
			var user = context.Users.ToList();
			return View(user);
		}
		public IActionResult Create()
		{
			return View();
		}


		[HttpPost]
		public IActionResult Create(UserDTO userdto)
		{

			if (!ModelState.IsValid)
			{
				return View(userdto);
			}

			User user = new User ()
			{
				User_Name=userdto.User_Name,
				User_Email=userdto.User_Email,
				User_Password=userdto.User_Password,
				User_Phone=userdto.User_Phone,
			};

			context.Users.Add(user);
			context.SaveChanges();


			return RedirectToAction("Index", "Users");
		}


		public IActionResult Delete(int id)
		{
			var user = context.Users.Find(id);
			if (user == null)
			{
				return RedirectToAction("Index", "Users");
			}
			context.Users.Remove(user);
			context.SaveChanges();
			return RedirectToAction("Index", "Users");
		}
	}
}
