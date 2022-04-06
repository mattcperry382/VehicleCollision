using Microsoft.AspNetCore.Authentication;
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
        //fido
        private readonly IFidoAuthentication fido;
        private UserManager<IdentityUser> userManager;
        private SignInManager<IdentityUser> signInManager;

        public AccountController(IFidoAuthentication fido, UserManager<IdentityUser> userM, SignInManager<IdentityUser> signM)
        {//

            this.fido = fido;
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
            if (ModelState.IsValid)
            {
                IdentityUser user = await userManager.FindByNameAsync(account.UserName);
                if (user != null)
                {
                    await signInManager.SignOutAsync();
                    //potentially change false to true for lockout
                    if ((await signInManager.PasswordSignInAsync(user, account.Password, false, false)).Succeeded)
                    {
                        return Redirect(account?.ReturnUrl ?? "/Admin");
                    }
                }
            }
            ModelState.AddModelError("", "Invalid Username or Password");
            return View(account);
        }

        public async Task<RedirectResult> Logout(string returnUrl = "/")
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
            var challenge = await fido.InitiateRegistration(User.Identity.Name, model.Device);

            return View(challenge.ToBase64Dto());
        }

        [HttpPost]
        public async Task<IActionResult> CompleteRegistration(
            [FromBody] Base64FidoRegistrationResponse registrationResponse)
        {
            var result = await fido.CompleteRegistration(registrationResponse.ToFidoResponse());

            if (result.IsError) return BadRequest(result.ErrorDescription);
            return Ok();
        }

        //pulls up authenticator requirment
        public async Task<IActionResult> Authen()
        {
            var result = await HttpContext.AuthenticateAsync(IdentityConstants.TwoFactorUserIdScheme);
            if (result.Succeeded)
            {
                var claims = result.Principal.Claims.ToList();
                string userName = claims.FirstOrDefault(c => c.Type == "userName")?.Value;

                var challenge = await fido.InitiateAuthentication(userName);

                return View(challenge.ToBase64Dto());

            }
            return new RedirectResult("/Home/Error");

        }

        [HttpPost]
        public async Task<IActionResult> CompleteLogin(
           [FromBody] Base64FidoAuthenticationResponse authenticationResponse)
        {
            var authenticationResult = await fido.CompleteAuthentication(authenticationResponse.ToFidoResponse());

            if (authenticationResult.IsSuccess)
            {
                var result = await HttpContext.AuthenticateAsync(IdentityConstants.TwoFactorUserIdScheme);

                var claims = result.Principal.Claims.ToList();
                string rememberMeClaim = claims.FirstOrDefault(c => c.Type == "rememberme")?.Value;
                bool rememberMe = bool.Parse(rememberMeClaim ?? "false");
                string userName = claims.FirstOrDefault(c => c.Type == "userName")?.Value;

                var user = await userManager.FindByNameAsync(userName);
                await signInManager.SignInAsync(user, rememberMe);
            }

            await HttpContext.SignOutAsync(IdentityConstants.TwoFactorUserIdScheme);

            if (authenticationResult.IsError) return BadRequest(authenticationResult.ErrorDescription);

            return Ok();

        }

    }
}
