using LoLiAnProject.Models;
using LoLiAnProject.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoLiAnProject.Controllers
{
    public class UserController : Controller
    {
        UserService userService;


        public UserController(UserService userService)
        {
            this.userService = userService;
        }


        public IActionResult Index()
        {
            return View();
        }

        //---------Register new user--------//
        [Route("user/signup")]
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [Route("user/signup")]
        [HttpPost]
        public async Task<IActionResult> SignUp(SignupVM signUpVM)
        {
            if (!ModelState.IsValid)
                return View(signUpVM);

            // Try to register user
            var success = await userService.TrySignUpAsync(signUpVM);
            if (!success)
            {
                // Show error
                ModelState.AddModelError(string.Empty, "Kan ej skapa användare, lösenordet måste innehålla ett specialtecken");
                //TODO mer informativt felmeddelande om vad som krävs för lösen, if-sats?
                return View(signUpVM);
            }

            // Redirect user
            //return RedirectToAction(nameof(Login));
            return RedirectToAction(nameof(Login));


            //---------Login user--------//

        }
        [Route("/user/login")]
        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            return View(new LoginVM { ReturnUrl = returnUrl });
        }

        [Route("/user/Login")]
        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginVM loginVM)
        {
            if (!ModelState.IsValid)
                return View(loginVM);

            var success = await userService.TryLoginAsync(loginVM);


            if (!success)
            {
                ModelState.AddModelError(nameof(LoginVM.Email), "Login failed");
                return View(loginVM);
            }

            if (string.IsNullOrWhiteSpace(loginVM.ReturnUrl))
                return RedirectToAction(nameof(HomeController.Overview), "Home");
            else
                return Redirect(loginVM.ReturnUrl);
        }

        //---------Logout user--------//


        [Route("user/logout")]
        public async Task<IActionResult> Logout()
        {
            await userService.LogoutAsync();
            return RedirectToAction(nameof(Login));
        }

        //----------Profile Page-------------//
        [Authorize]
        [Route("user/profilepage")]
        [HttpGet]
        public IActionResult ProfilePage(ProfilePageVM profilePage)
        {
            var details = userService.GetProfilePageById(profilePage);

            var detailsVM = userService.GetProfilePageVM(details);
            return View(detailsVM);
        }

        //public IActionResult ProfilePage()
        //{
        //    return View(new ProfilePageVM { UserName = User.Identity.Name });
        //}


        //---------Edit user--------//

        [Authorize]
        [Route("user/edituser/{id}")]
        [HttpGet]
        //public IActionResult EditUser(int id)
        //{
        //    var details = userService.GetDetailsById(id);
        //    EditUserVM editVM = userService.GetUserEditVM(details);
        //    return View(editVM);
        //}

        [Authorize]
        [Route("home/edituser/{id}")]
        [HttpPost]
        public IActionResult EditUser(EditUserVM editVM)
        {
            if (!ModelState.IsValid)
                return View(editVM);

            //userService.EditUser(editVM);
            return RedirectToAction(nameof(ProfilePage));
        }

    }
}
