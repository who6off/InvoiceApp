using InvoiceApp.Identity.Services.Interfaces;
using InvoiceApp.Identity.ViewModels;
using InvoiceApp.ViewModels.User;
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


		public IActionResult Index() => View();


		[HttpGet]
		public async Task<IActionResult> Create()
		{
			return View("CreateTest", new UserViewModel());
		}


		[HttpPost]
		public async Task<IActionResult> Create(UserViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View("CreateTest", model);
			}

			var newUser = await _userService.CreateUser(model);

			return View("UserInfo", new UserInfoViewModel(newUser, model));
		}


		[HttpGet]
		public async Task<IActionResult> Edit()
		{
			var users = await _userService.GetAll();
			return View(new UserEditViewModel()
			{
				Users = users,
			});
		}
	}
}
