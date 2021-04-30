using LoLiAnProject.Models.Entities;
using LoLiAnProject.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoLiAnProject.Models
{
    public class UserService
    {
        readonly UserManager<MyIdentityUser> userManager; //används vid registerering
        readonly SignInManager<MyIdentityUser> signInManager; //används vid inloggning'
        private readonly IHttpContextAccessor accessor;
        MyIdentityContext context;



        public UserService(UserManager<MyIdentityUser> userManager, SignInManager<MyIdentityUser> signInManager, IHttpContextAccessor accessor, MyIdentityContext context) //konstruktor för userservice
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.accessor = accessor;
            this.context = context;
        }

        //---------Register new user--------//

        public async Task<bool> TrySignUpAsync(SignupVM signUpVM)
        {
            var result = await userManager.CreateAsync(
                  new MyIdentityUser { UserName = signUpVM.Email, Email = signUpVM.Email, FirstName = signUpVM.FirstName },
                  signUpVM.Password);

            return result.Succeeded;
        }

        //---------Login user--------//

        public async Task<bool> TryLoginAsync(LoginVM loginVM)
        {
            var result = await signInManager.PasswordSignInAsync(
                loginVM.Email, loginVM.Password, isPersistent: false, lockoutOnFailure: false);


            return result.Succeeded;
        }

        //---------Logout user--------//

        public async Task LogoutAsync()
        {
            await signInManager.SignOutAsync();
        }


        //---Get details for profilepage----//
        public MyIdentityUser GetProfilePageById(ProfilePageVM profile)
        {
            string userId = userManager.GetUserId(
                accessor.HttpContext.User);

            var details = context.Users
                .Where(x => x.Id == userId)
                .FirstOrDefault();

            return details;
        }

        internal ProfilePageVM GetProfilePageVM(MyIdentityUser myIdentityUser)
        {

            return new ProfilePageVM
            {
                UserName = myIdentityUser.UserName,
                FirstName = myIdentityUser.FirstName,
                Email = myIdentityUser.Email
            };
        }


        //---------Edit user--------//

        //internal object GetDetailsById(int id)
        //{
        //    throw new NotImplementedException();
        //}


        //internal UserEditVM GetUserEditVM(object details)
        //{
        //    throw new NotImplementedException();
        //}

        //internal void EditUser(UserEditVM editVM)
        //{
        //}
    }
}