using Jam3iyati.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using Jam3iyati.Enums;


namespace Jam3iyati.Controllers
{
    public class ApcController : Controller
    {
        // GET: Apc
        public ActionResult Index()
        {  
            return View(getApcViewModel());
        }

        // demandes d'associations communales non traitées
        public ActionResult DacNotTreated()
        {
            return View(getApcViewModel());
        }

        // demandes d'associations communales traitées
        public ActionResult DacTreated()
        {
            return View(getApcViewModel());
        }

        // demandes d'associations de wilaya non validées
        public ActionResult DawNotValidated()
        {
            return View(getApcViewModel());
        }

        // demandes d'associations de wilaya validées
        public ActionResult DawValidated()
        {
            return View(getApcViewModel());
        }

        // demandes archivées
        public ActionResult Archieved()
        {
            return View(getApcViewModel());
        }

        // prepare and return the view model for the different views
        private ApcViewModel getApcViewModel()
        {
            string userID = User.Identity.GetUserId();
            var db = new ApplicationDbContext();
            Administration admin = db.Administrations.Where(c => c.userID == userID).First();

            List<AdministrationDemande> notTreatedApcDemandes = db.AdministrationDemande.Where(c => (c.AdministrationID == admin.AdministrationID) && (c.state == DemandeState.notTreated)).Include(c => c.Demande.Association).ToList();
            List<AdministrationDemande> treatedApcDemandes = db.AdministrationDemande.Where(c => (c.AdministrationID == admin.AdministrationID) && (c.state == DemandeState.treated)).Include(c => c.Demande.Association).ToList();
            List<AdministrationDemande> notValidatedWilayaDemandes = db.AdministrationDemande.Where(c => (c.AdministrationID == admin.AdministrationID) && (c.state == DemandeState.notValidated)).Include(c => c.Demande.Association).ToList();
            List<AdministrationDemande> validatedWilayaDemandes = db.AdministrationDemande.Where(c => (c.AdministrationID == admin.AdministrationID) && (c.state == DemandeState.validated)).Include(c => c.Demande.Association).ToList();
            List<AdministrationDemande> archievedDemandes = db.AdministrationDemande.Where(c => (c.AdministrationID == admin.AdministrationID) && (c.state == DemandeState.archived)).Include(c => c.Demande.Association).ToList();


            ApcViewModel model = new ApcViewModel()
            {
                admin = admin,
                apc = db.Apcs.Find(admin.adminID),
                notTreatedApcDemandes = notTreatedApcDemandes,
                treatedApcDemandes = treatedApcDemandes,
                notValidatedWilayaDemandes = notValidatedWilayaDemandes,
                validatedWilayaDemandes = validatedWilayaDemandes,
                archievedDemandes = archievedDemandes

            };
            return model;
        }

        public HtmlString getApcs(int id)
        {
            var db1 = new ApplicationDbContext();
            var db2 = new ApplicationDbContext();
            Administration admin = null;

            var apcs = db1.Apcs.Where(c => c.Daira.Wilaya.code == id).ToList();
            
            string str2 = "";
            foreach (var item in apcs)
            {
                admin = db2.Administrations.Where(c => c.adminID == item.ApcID).First();
                str2 += @" <option value = " + '"' + item.ApcID + '"' + @" > " + admin.name + @" </option>";
            }
               

             string str = @"<div class =" + '"' + "mt - 10" + '"' + @">   
                    <label for=" + '"' + @"apc" + '"' + @" >Commune</label>
                  <select class =" + '"' +@"form-select" + '"' +@" max=" + '"' +@"10" + '"' +@" name=" + '"' + @"apc" + '"' + @" required >
                    <option value = " + '"' + @"14" + '"' + @" selected disabled>---</option> " +

                           str2 +
                    
                @"</select>
            </div><br>";

            HtmlString html = new HtmlString(str);
            return html;
        }
    }
}