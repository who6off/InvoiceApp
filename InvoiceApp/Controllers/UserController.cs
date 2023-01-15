using InvoiceApp.Helpers.Exceptions;
using InvoiceApp.Identity.Constants;
using InvoiceApp.Identity.Helpers;
using InvoiceApp.Identity.RequestParameters;
using InvoiceApp.Identity.Services.Interfaces;
using InvoiceApp.Identity.ViewModels;
using InvoiceApp.ViewModels.User;
using Microsoft.AspNetCore.Authorization;
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
		public IActionResult SignIn(string? returnUrl)
		{
			ViewData["returnUrl"] = returnUrl;
			return View(new SignInViewModel());
		}


		[HttpPost]
		public async Task<IActionResult> SignIn(SignInViewModel model, string? returnUrl)
		{
			var user = await _userService.SignIn(model);

			if (user is null)
			{
				ModelState.AddModelError("", "Invalid user credentials!");
				return View(model);
			}

			return string.IsNullOrEmpty(returnUrl)
				? RedirectToAction("Index", "Home")
				: Redirect(returnUrl);
		}


		[HttpGet]
		public async Task<IActionResult> SignOut()
		{
			await _userService.SignOut();

			return RedirectToAction("Index", "Home");
		}


		[HttpGet]
		[Authorize]
		public async Task<IActionResult> Profile()
		{
			var user = await _userService.GetById(User.GetId());

			return View(new ProfileViewModel()
			{
				User = user
			});
		}


		[HttpGet]
		[Authorize(Roles = $"{UserRoles.Admin}")]
		public IActionResult Index()
		{
			return View();
		}


		[HttpGet]
		[Authorize(Roles = $"{UserRoles.Admin}")]
		public async Task<IActionResult> Create()
		{
			return View(new UserViewModel());
		}


		[HttpPost]
		[Authorize(Roles = $"{UserRoles.Admin}")]
		public async Task<IActionResult> Create(UserViewModel model, string? returnUrl)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var newUser = await _userService.Create(model);

			if (newUser is null)
			{
				throw new AppException("User is not created!");
			}

			return View("Details", new UserDetailsViewModel(newUser, model, "User successfully registered!")
			{
				AppUser = newUser,
				UserViewModel = model,
				Header = "User successfully registered!",
				ReturnUrl = returnUrl,
			});
		}


		[HttpGet]
		[Authorize(Roles = $"{UserRoles.Admin}")]
		public async Task<IActionResult> List([FromQuery] UserRequestParameters parameters)
		{
			var users = await _userService.Get(parameters);
			return View("List", new UserListViewModel()
			{
				Users = users,
				Parameters = parameters
			});
		}


		[HttpGet]
		[Authorize(Roles = $"{UserRoles.Admin}")]
		[Route("{controller}/{action}/{id:required}")]
		public async Task<IActionResult> Edit(string id)
		{
			var user = await _userService.GetById(id);

			if (user is null)
			{
				throw new NotFoundException("User not found.");
			}

			return View(new UserViewModel()
			{
				Id = user.Id,
				Name = user.Name,
				Surname = user.Surname,
				DateOfBirth = user.DateOfBirth,
				Email = user.Email,
				Role = user.RoleName
			});
		}


		[HttpPost]
		[Authorize(Roles = $"{UserRoles.Admin}")]
		public async Task<IActionResult> Edit_Post(UserViewModel model, string? returnUrl)
		{
			if (!ModelState.IsValid)
			{
				return View("Edit", model);
			}

			var user = await _userService.Update(model);

			if (user is null)
			{
				throw new AppException("The error has occurred while update.");
			}

			return View("Details", new UserDetailsViewModel()
			{
				AppUser = user,
				UserViewModel = model,
				Header = "User's information updated",
				ReturnUrl = returnUrl,
			});
		}


		[HttpGet]
		[Authorize(Roles = $"{UserRoles.Admin}")]
		public async Task<IActionResult> Delete(string id, string? returnUrl)
		{
			var isDeleted = await _userService.Delete(id);

			if (!isDeleted)
			{
				throw new AppException("The error has occurred while deletion.");
			}

			return string.IsNullOrEmpty(returnUrl)
				? RedirectToAction("List", "User")
				: Redirect(returnUrl);
		}


		[HttpDelete]
		[Authorize(Roles = $"{UserRoles.Admin}")]
		public async Task<IActionResult> Delete(string id)
		{
			var isDeleted = await _userService.Delete(id);

			return isDeleted ? Ok() : BadRequest();
		}


		[HttpGet]
		[Authorize(Roles = $"{UserRoles.Admin}")]
		public async Task<IActionResult> Details(string id)
		{
			var user = await _userService.GetById(id);

			if (user is null)
			{
				throw new AppException("User is not found!");
			}

			return View(new UserDetailsViewModel(user, "User's information"));
		}
	}
}
