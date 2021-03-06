﻿using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using FinalProject.DAL;



//TODO: Change this using statement to match your project
using FinalProject.Models;

//TODO: Change this namespace to match your project
namespace FinalProject.Controllers
{
    [Authorize]
    public class AccountsController : Controller
    {
        private AppDbContext db = new AppDbContext();
        private ApplicationSignInManager _signInManager;
        private AppUserManager _userManager;

        public AccountsController()
        {
        }

        public AccountsController(AppUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public AppUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Accounts/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            if (HttpContext.User.Identity.IsAuthenticated) //user has been redirected here from a page they're not authorized to see
            {
                return View("Error", new string[] { "Access Denied" });
            }
            AuthenticationManager.SignOut(); //this removes any old cookies hanging around
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Accounts/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }


        //
        // GET: /Accounts/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            RegisterViewModel customer = new RegisterViewModel();

            int BiggestAdvantage = 5001;

            if (db.Users.ToList().Any() == false)
            {
                BiggestAdvantage = 5001;
                //ViewBag.FlightNumber = BiggestFlight;
                customer.AdvantageNumber = BiggestAdvantage;
            }
            else
            {
                foreach (AppUser f in db.Users.ToList())
                {
                    if (f.AdvantageNumber >= BiggestAdvantage)
                    {
                        BiggestAdvantage = f.AdvantageNumber + 1;
                        //ViewBag.FlightNumber = BiggestFlight + 1;
                        customer.AdvantageNumber = BiggestAdvantage;
                    }
                }
            }
            if (ViewBag.FlightNumber == null)
            {
                //ViewBag.FlightNumber = 100;
                customer.AdvantageNumber = BiggestAdvantage;
            }

            return View(customer);
        }

        //
        // POST: /Accounts/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {

            if (ModelState.IsValid)
            {
               
                //TODO: Add fields to user here so they will be saved to do the database
                var user = new AppUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    MiddleInitial = model.MiddleInitial,
                    LastName = model.LastName,
                    DateofBirth = model.DateofBirth,
                    Address = model.Address,
                    City = model.City,
                    State = model.State,
                    Zip = model.Zip,
                    PhoneNumber = model.PhoneNumber, 
                    AdvantageNumber = model.AdvantageNumber
                };
                var result = await UserManager.CreateAsync(user, model.Password);

                //TODO:  Once you get roles working, you may want to add users to roles upon creation
                await UserManager.AddToRoleAsync(user.Id, "Customers");
                // --OR--
                // await UserManager.AddToRoleAsync(user.Id, "Employee");


                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //[Authorize(Roles = "Managers")]
        [AllowAnonymous]
        public ActionResult RegisterEmployee()
        {

            return View();
        }

        //
        // POST: /Accounts/RegisterEmployee
        [HttpPost]
        [Authorize(Roles = "Managers")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegisterEmployee(RegisterEmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                
                //TODO: Add fields to user here so they will be saved to do the database
                var user = new AppUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    FirstName = model.FirstName,
                    MiddleInitial = model.MiddleInitial,
                    LastName = model.LastName,
                    EmployeeType = model.EmployeeType,
                    Address = model.Address,
                    City = model.City,
                    State = model.State,
                    Zip = model.Zip,
                    DateofBirth = DateTime.Now
                };
                var result = await UserManager.CreateAsync(user, model.Password);
                

                
               //TODO:  Once you get roles working, you may want to add users to roles upon creation
                // --OR--
                // await UserManager.AddToRoleAsync(user.Id, "Employee");


                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //Logic for change password
        // GET: /Accounts/ChangePassword
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Accounts/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", "Home");
            }
            AddErrors(result);
            return View(model);
        }

        //

        // POST: /Accounts/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        // GET: /Manage/Index
        public ActionResult Index(ManageMessageId? message)
        {
            var userId = User.Identity.GetUserId();
            var model = new IndexViewModel
            {
                HasPassword = HasPassword(),
                UserName = userId,
                Email = UserManager.GetEmail(userId),
                UserID = userId
            };
            return View(model);
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region 

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            Error
        }

        #endregion


    }
}