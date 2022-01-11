using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Jam3iyati.Models;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using Jam3iyati.Enums;
using HtmlAgilityPack;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data.Entity;
using System.Web.Script.Serialization;

namespace Jam3iyati.Controllers
{
    public class AssociationController : Controller
    {

        

        public ActionResult Index()
        {
            if(User.Identity.IsAuthenticated) return View(getAssociationViewModel());
            else return RedirectToRoute(new
            {
                controller = "Home",
                action = "Index",
                message = "NotConnected"
            });
        }

        public ActionResult Register()
        {
            AssociationViewModel model = new AssociationViewModel();
             
            model.Wilayas = (new ApplicationDbContext()).Administrations.Where(c => c.type == AdministrationType.wilaya ).ToList();
            
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Register(FormCollection collection)
        {
            int members;                     
            List<Memeber> existMembers = new List<Memeber>();
            if (collection["typeAsso"].ToString() == "1") members = 3;
            else members = 2;
            var db = new ApplicationDbContext();
                 
                        foreach (var item in db.Members)
                        
                            for (int i = 0; i < members; i++)
                                if ((item.cardId == Convert.ToInt32(collection["cardId" + i])) && (item.birthday == Convert.ToDateTime(collection["birthday" + i])))
                                using(var db2 = new ApplicationDbContext())
                                {
                                foreach (var item3 in db2.AssociationMemeber)

                                    if (item3.MemeberID == item.MemeberID)
                                        using (var db3 = new ApplicationDbContext())
                                        {
                                            if (db3.Associations.Find(item3.AssociationID).field == Convert.ToInt32(collection["field"]))
                                                existMembers.Add(new Memeber()
                                                {
                                                    firstName = item.firstName,
                                                    lastName = item.lastName,
                                                    birthday = item.birthday,
                                                    birthplace = item.birthplace,
                                                    cardId = item.cardId
                                                });
                                            
                                        }
                                }
                             
                
            if (existMembers.Count() > 0) return RedirectToRoute(new
            {
                controller = "Association",
                action = "register",
                message = existMembers.Count() + " members not allowed"
            });
            else
            {

                var dbContext = new ApplicationDbContext();
                var userStore = new UserStore<ApplicationUser>(dbContext);
                var appUserManager = new ApplicationUserManager(userStore);
                var signInManager = new ApplicationSignInManager(appUserManager, HttpContext.GetOwinContext().Authentication);

                int adminID;
                int apcID;

                Memeber member;

                if (collection["typeAsso"].ToString() == "1") adminID = 5590;   // only wilaya of tiaret
                else
                {
                    apcID = Convert.ToInt32(collection["apc"]);
                    adminID = db.Administrations.Where(c => c.adminID == apcID).First().AdministrationID;
                }

                Association asso = new Association()
                {
                    name = collection["associationName"].ToString(),
                    field = Convert.ToInt32(collection["field"]),
                    subject = collection["subject"].ToString(),
                    date = DateTime.Now,
                    AdministrationID = adminID,


                };
                db.Associations.Add(asso);
                db.SaveChanges();

                for (int i = 0; i < members; i++)
                {
                    member = new Memeber()
                    {
                        firstName = collection["firstName" + i],
                        lastName = collection["lastName" + i],
                        birthday = Convert.ToDateTime(collection["birthday" + i]),
                        birthplace = collection["birthplace" + i],
                        cardId = Convert.ToInt32(collection["cardId" + i])
                    };
                    db.Members.Add(member);
                    db.SaveChanges();

                    db.AssociationMemeber.Add(new AssociationMemeber()
                    {
                        AssociationID = asso.AssociationID,
                        MemeberID = member.MemeberID
                    });
                    db.SaveChanges();
                }

                RegisterViewModel model = new RegisterViewModel()
                {
                    Email = collection["email"].ToString(),
                    Password = "Khaled30-",
                    ConfirmPassword = "Khaled30-",
                    association = asso
                };

                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await appUserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {

                    db.Associations.Find(asso.AssociationID).userId = user.Id;
                    db.SaveChanges();

                    await signInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
   
                    return RedirectToAction("index", "Association");
                }
                return RedirectToAction("Association", "register");

            }

                
        }

        public ActionResult Notifications()
        {
            AssociationViewModel vm = getAssociationViewModel();
            var db = new ApplicationDbContext();
            var notifs = db.Associations.Find(vm.Association.AssociationID).Notifications.Where(c => c.read == false);
            foreach (var item in notifs) item.read = true;
            db.SaveChanges();
            return View(vm);
        }

        public ActionResult Demandes()
        {
            return View(getAssociationViewModel()); 
        }

        [HttpPost]
        public ActionResult Demandes(FormCollection collection)
        {
            var db = new ApplicationDbContext();
            AssociationViewModel vm = getAssociationViewModel();

            Demande demande = new Demande()
            {
                AssociationID = vm.Association.AssociationID,
                date = DateTime.Now,
                result = DemandeState.notTreated,
                type = (DemandeType)Convert.ToInt32(collection["type"])
            };  
            db.Demandes.Add(demande);

            int demandeID = demande.DemandeID;
           
            db.AdministrationDemande.Add(new AdministrationDemande()
            {
                AdministrationID = vm.Association.AdministrationID,
                DemandeID = demandeID,
                state = DemandeState.notTreated
            });

            db.SaveChanges();
           
            return RedirectToAction("Demandes", getAssociationViewModel());
        }

        public ActionResult Account()
        {
            return View(getAssociationViewModel());
        }

        // return association information for the administrations
        public JsonResult getAssociationInfo(int id)
        {
            
            if (User.Identity.IsAuthenticated)
            {
                var db = new ApplicationDbContext();
                string userID;
                userID = User.Identity.GetUserId();

                if ( db.Administrations.Where(c => c.userID == userID).Count() == 1 )
                {
                    Demande demande = db.Demandes.Find(id);
                    var members = db.Members.Join(db.AssociationMemeber.Where(c => c.AssociationID == demande.Association.AssociationID), f => f.MemeberID , s => s.MemeberID , (f,s) => new { f.firstName,f.lastName,f.birthday,f.birthplace,f.cardId } ).ToArray();
                    return Json(new {
                        associationName = demande.Association.name,
                        date = demande.Association.date.Date.ToShortDateString(),
                        field = demande.Association.field,
                        demandeID = demande.DemandeID,
                        demandeType = demande.type,
                        demandeDate = demande.date.Date.ToShortDateString(),
                        members = members 

                    }, JsonRequestBehavior.AllowGet);  
                }
            }

            
            return Json(new
                            {
                                message = "Not allowed !"
                            }, JsonRequestBehavior.AllowGet); 
        } 

              
        // helper  
        private AssociationViewModel getAssociationViewModel()
        {
            var userID = User.Identity.GetUserId();
            using (var db = new ApplicationDbContext())
            {
                Association association = db.Associations.Where(c => c.userId == userID).First();
                ICollection<Demande> demandes = db.Demandes.Where(c => c.AssociationID == association.AssociationID).ToList();
                ICollection<Notification> notifs = db.Notifications.Where(c => c.AssociationID == association.AssociationID).ToList();
                return new AssociationViewModel() { Association = association, Demandes = demandes ,Notifications = notifs };
            }

        }

       
    }
}