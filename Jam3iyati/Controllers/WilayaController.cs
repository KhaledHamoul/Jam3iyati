using Jam3iyati.Enums;
using Jam3iyati.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Jam3iyati.Controllers
{
    public class WilayaController : Controller
    {
        // GET: Wilaya
        public ActionResult Index()
        {
            return View(getWilayaViewModel());
        }
        // demandes d'associations communales non traitées
        public ActionResult DawNotTreated()
        {
            return View(getWilayaViewModel());
        }

        // demandes d'associations communales traitées
        public ActionResult DawTreated()
        {
            return View(getWilayaViewModel());
        }

        // demandes d'associations de wilaya non validées
        public ActionResult DacNotValidated()
        {
            return View(getWilayaViewModel());
        }

        // demandes d'associations de wilaya validées
        public ActionResult DacValidated()
        {
            return View(getWilayaViewModel());
        }

        // demandes archivées
        public ActionResult Archieved()
        {
            return View(getWilayaViewModel());
        }

        // prepare and return the view model for the different views
        private WilayaViewModel getWilayaViewModel()
        {
            string userID = User.Identity.GetUserId();
            var db = new ApplicationDbContext();
            Administration admin = db.Administrations.Where(c => c.userID == userID).First();

            List<AdministrationDemande> notTreatedWilayaDemandes = db.AdministrationDemande.Where(c => (c.AdministrationID == admin.AdministrationID) && (c.state == DemandeState.notTreated)).Include(c => c.Demande.Association).ToList();
            List<AdministrationDemande> treatedWilayaDemandes = db.AdministrationDemande.Where(c => (c.AdministrationID == admin.AdministrationID) && (c.state == DemandeState.treated)).Include(c => c.Demande.Association).ToList();
            List<AdministrationDemande> notValidatedApcDemandes = db.AdministrationDemande.Where(c => (c.AdministrationID == admin.AdministrationID) && (c.state == DemandeState.notValidated)).Include(c => c.Demande.Association).ToList();
            List<AdministrationDemande> validatedApcDemandes = db.AdministrationDemande.Where(c => (c.AdministrationID == admin.AdministrationID) && (c.state == DemandeState.validated)).Include(c => c.Demande.Association).ToList();
            List<AdministrationDemande> archievedDemandes = db.AdministrationDemande.Where(c => (c.AdministrationID == admin.AdministrationID) && (c.state == DemandeState.archived)).Include(c => c.Demande.Association).ToList();


            WilayaViewModel model = new WilayaViewModel()
            {
                admin = admin,
                wilaya = db.Wilayas.Find(admin.adminID),
                notTreatedWilayaDemandes = notTreatedWilayaDemandes,
                treatedWilayaDemandes = treatedWilayaDemandes,
                notValidatedApcDemandes = notValidatedApcDemandes,
                validatedApcDemandes = validatedApcDemandes,
                archievedDemandes = archievedDemandes
            };
            return model;
        }
          
    }
}