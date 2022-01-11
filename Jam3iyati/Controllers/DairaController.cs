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
    public class DairaController : Controller
    {
        // GET: Daira
        public ActionResult Index()
        {
            return View(getDairaViewModel());
        }

        // demandes d'associations communales non traitées
        public ActionResult DawNotValidated()
        {
            return View(getDairaViewModel());
        }

        // demandes d'associations communales traitées
        public ActionResult DawValidated()
        {
            return View(getDairaViewModel());
        }

        // demandes d'associations de wilaya non validées
        public ActionResult DacNotValidated()
        {
            return View(getDairaViewModel());
        }

        // demandes d'associations de wilaya validées
        public ActionResult DacValidated()
        {
            return View(getDairaViewModel());
        }

        // demandes archivées
        public ActionResult Archieved()
        {
            return View(getDairaViewModel());
        }

        // prepare and return the view model for the different views
        private DairaViewModel getDairaViewModel()
        {
            string userID = User.Identity.GetUserId();
            var db = new ApplicationDbContext();
            Administration admin = db.Administrations.Where(c => c.userID == userID).First();

            List<AdministrationDemande> notValidatedWilayaDemandes = db.AdministrationDemande.Where(c => (c.AdministrationID == admin.AdministrationID) && (c.state == DemandeState.notTreated)).Include(c => c.Demande.Association).ToList();
            List<AdministrationDemande> validatedWilayaDemandes = db.AdministrationDemande.Where(c => (c.AdministrationID == admin.AdministrationID) && (c.state == DemandeState.treated)).Include(c => c.Demande.Association).ToList();
            List<AdministrationDemande> notValidatedApcDemandes = db.AdministrationDemande.Where(c => (c.AdministrationID == admin.AdministrationID) && (c.state == DemandeState.notValidated)).Include(c => c.Demande.Association).ToList();
            List<AdministrationDemande> validatedApcDemandes = db.AdministrationDemande.Where(c => (c.AdministrationID == admin.AdministrationID) && (c.state == DemandeState.validated)).Include(c => c.Demande.Association).ToList();
            List<AdministrationDemande> archievedDemandes = db.AdministrationDemande.Where(c => (c.AdministrationID == admin.AdministrationID) && (c.state == DemandeState.archived)).Include(c => c.Demande.Association).ToList();


            DairaViewModel model = new DairaViewModel()
            {
                admin = admin,
                daira = db.Dairas.Find(admin.adminID),
                notValidatedWilayaDemandes = notValidatedWilayaDemandes,
                validatedWilayaDemandes = validatedWilayaDemandes,
                notValidatedApcDemandes = notValidatedApcDemandes,
                validatedApcDemandes = validatedApcDemandes,
                archievedDemandes = archievedDemandes
            };
            return model;
        }
    }
}