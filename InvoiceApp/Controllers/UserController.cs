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
			return View(new UserViewModel());
		}


		[HttpPost]
		public async Task<IActionResult> Create(UserViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var newUser = await _userService.Create(model);

			return View("UserInfo", new UserInfoViewModel(newUser, model));
		}


		[HttpGet]
		public async Task<IActionResult> List()
		{
			var users = await _userService.GetAll();
			return View(new UserEditViewModel()
			{
				Users = users,
			});
		}


		[HttpGet]
		[Route("{action}/{id:required}")]
		public async Task<IActionResult> Edit(string id)
		{
			var user = await _userService.GetById(id);

			if (user is null)
				return RedirectToAction(nameof(List));

			return View(new UserViewModel()
			{
				Id = user.Id,
				Name = user.Name,
				Surname = user.Surname,
				DateOfBirth = user.DateObBirth,
				Email = user.Email,
				Role = user.RoleName
			});
		}


		[HttpPost]
		public async Task<IActionResult> EditPost(UserViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View("Edit", model);
			}

			var newUser = await _userService.Update(model);

			return RedirectToAction(nameof(List));
		}
	}
}
