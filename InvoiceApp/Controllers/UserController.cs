using InvoiceApp.Identity.Services.Interfaces;
using InvoiceApp.Identity.ViewModels;
using InvoiceApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceApp.Controllers
{
	public class UserController : Controller
	{
		private readonly IUserService _userService;
		public UserController(
			IUserService userService)
		{
			_userService = userService;
		}


		[HttpGet]
		public async Task<IActionResult> Index()
		{
			return View(new UserViewModel());
		}


		[HttpPost]
		public async Task<IActionResult> Index(UserViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var newUser = await _userService.CreateUser(model);

			return View("UserInfo", new UserInfoViewModel(newUser, model));
		}


	}
}
