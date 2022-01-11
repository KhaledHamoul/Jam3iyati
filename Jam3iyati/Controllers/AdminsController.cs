using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Jam3iyati.Helpers;
using Jam3iyati.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using Jam3iyati.Enums;
using System.Diagnostics;

namespace Jam3iyati.Controllers
{
    public class AdminsController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> login(FormCollection collection , string returnUrl)
        {
            var dbContext = new ApplicationDbContext();
            var userStore = new UserStore<ApplicationUser>(dbContext);
            var appUserManager = new ApplicationUserManager(userStore);
            var signInManager = new ApplicationSignInManager(appUserManager, HttpContext.GetOwinContext().Authentication);

            var result = await signInManager.PasswordSignInAsync(collection["email"], collection["password"], Convert.ToBoolean(collection["rememberme"]), shouldLockout: false);

            switch (result)
            {
                case SignInStatus.Success:

                    var ID = signInManager.AuthenticationManager.AuthenticationResponseGrant.Identity.GetUserId();
                    Administration administration = dbContext.Administrations.Where(c => c.userID == ID).Single();
                    Debug.WriteLine("===========================" + administration.name );
                    switch (administration.type)
                    {
                        case AdministrationType.apc:
                            return RedirectToRoute(new
                            {
                                controller = "Apc",
                                action = "Index"
                            });
                            

                        case AdministrationType.daira:
                            return RedirectToRoute(new
                            {
                                controller = "Daira",
                                action = "Index"
                            });
                            

                        case AdministrationType.wilaya:
                            return RedirectToRoute(new
                            {
                                controller = "Wilaya",
                                action = "Index"
                            });
                            
                    }
                    break;
                    
                //return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = true });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return RedirectToRoute(new
                    {
                        controller = "Home",
                        action = "Index",
                        message = "error"
                    });

            }
            return View();
            
        }


        // get algeria's admins and make them in the database
        public async Task<string> ScrapeAdmins()
        {
            await scrapingAdminsHelper.scrape();
            return "Done !";
        }
    }
}