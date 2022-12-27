﻿using InvoiceApp.Identity.Constants;
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
        public async Task<IActionResult> Profile()
        {
            var user = await _userService.GetById(User.GetId()); ;
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
        public async Task<IActionResult> Create(UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var newUser = await _userService.Create(model);

            return View("UserInfo", new UserInfoViewModel(newUser, model, "Registration Info"));
        }


        [HttpGet]
        [Authorize(Roles = $"{UserRoles.Admin}")]
        public async Task<IActionResult> List([FromQuery] UserRequestParameters parameters)
        {
            var dictionary = parameters.ToDictionary();
            return RedirectToAction(nameof(ListWithFilteredParams), dictionary);
        }


        [HttpGet]
        [Authorize(Roles = $"{UserRoles.Admin}")]
        [Route("List")]
        public async Task<IActionResult> ListWithFilteredParams([FromQuery] UserRequestParameters parameters)
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
        [Authorize(Roles = $"{UserRoles.Admin}")]
        public async Task<IActionResult> Edit_Post(UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", model);
            }

            var user = await _userService.Update(model);

            return View("UserInfo", new UserInfoViewModel(user, model, "User Update Info"));
        }


        [HttpGet]
        [Authorize(Roles = $"{UserRoles.Admin}")]
        public async Task<IActionResult> Delete(string id)
        {
            var isDeleted = await _userService.Delete(id);

            return RedirectToAction(nameof(List));
        }
    }
}
