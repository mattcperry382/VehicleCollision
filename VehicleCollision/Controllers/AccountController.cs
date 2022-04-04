using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Rsk.AspNetCore.Fido;
using Rsk.AspNetCore.Fido.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleCollision.Models;

namespace VehicleCollision.Controllers
{
    public class AccountController : Controller
    {

        private UserManager<IdentityUser> userManager;
        private SignInManager<IdentityUser> signInManager;

        //fido
        private readonly IFidoAuthentication _fido;

        public AccountController(IFidoAuthentication fido, UserManager<IdentityUser> userM, SignInManager<IdentityUser> signM)
        {//

            _fido = fido;
            userManager = userM;
            signInManager = signM;
        }
        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            return View(new Account
            {
                ReturnUrl = returnUrl
            });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Login(Account account)
        {
            if(ModelState.IsValid)
            {
                IdentityUser user = await userManager.FindByNameAsync(account.UserName);
                if(user != null)
                {
                    await signInManager.SignOutAsync();
                    //potentially change false to true for lockout
                    if((await signInManager.PasswordSignInAsync(user, account.Password, false, false)).Succeeded)
                    {
                        return Redirect(account?.ReturnUrl ?? "/Admin");
                    }
                }
            }
            ModelState.AddModelError("", "Invalid Username or Password");
            return View(account);
        }

        public async Task<RedirectResult> Logout(string returnUrl ="/")
        {
            await signInManager.SignOutAsync();
            return Redirect(returnUrl);
        }



        //FIDO STUFF


        [Authorize]
        public IActionResult StartRegistration()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(FidoRegist model)
        {
            //init regis process and challenge device
            //change user name to different value. See vid 745min
            var challenge = await _fido.InitiateRegistration(User.Identity.Name, model.Device);

            return View(challenge.ToBase64Dto());
        }

        [HttpPost]
        public async Task<IActionResult> CompleteRegis(
            [FromBody] Base64FidoRegistrationResponse registrationResponse)
        {
            var result = await _fido.CompleteRegistration(registrationResponse.ToFidoResponse());

            if (result.IsError) return BadRequest(result.ErrorDescription);
            return Ok();
        }
    }
}
