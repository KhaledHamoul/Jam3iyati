using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Jam3iyati.Models;
using Jam3iyati.Enums;
using Microsoft.AspNet.Identity;

namespace Jam3iyati.Controllers
{
    public class DemandeController : Controller
    {
        // GET: Demande
        public ActionResult Index()
        {
            return View();
        }

        // APC
        public ActionResult ValidateDacForApc(int id)
        {
            var db = new ApplicationDbContext();
            AdministrationDemande apcAdminDemand = db.AdministrationDemande.Where(c => (c.DemandeID == id) && (c.state == DemandeState.notTreated)).First();
            int dairaID = db.Apcs.Find(apcAdminDemand.Administration.adminID).DairaID;

            db.AdministrationDemande.Add(new AdministrationDemande()
            {
                AdministrationID = (new ApplicationDbContext()).Administrations.Where(c => c.adminID == dairaID).First().AdministrationID,
                DemandeID = id,
                state = DemandeState.notValidated
            });

            apcAdminDemand.state = DemandeState.treated;
            db.SaveChanges();
            return RedirectToRoute(new
            {
                controller = "Apc",
                action = "DacNotTreated"
            });
        }

        public ActionResult ValidateDawForApc(int id)
        {
            var db = new ApplicationDbContext();
            var userID = User.Identity.GetUserId();
            Administration admin = db.Administrations.Where(c => c.userID == userID).First();
            AdministrationDemande apcAdminDemand = db.AdministrationDemande.Where(c => (c.AdministrationID == admin.AdministrationID) && (c.DemandeID == id) && (c.state == DemandeState.notValidated)).First();

            apcAdminDemand.state = DemandeState.validated;
            db.SaveChanges();
            return RedirectToRoute(new
            {
                controller = "Apc",
                action = "DawNotValidated"
            }); ;
        }

        [HttpPost]
        public ActionResult CancelDac(FormCollection collection , int id)
        {
            var db = new ApplicationDbContext();
            AdministrationDemande apcAdminDemand = db.AdministrationDemande.Where(c => (c.DemandeID == id) && (c.state == DemandeState.notTreated)).First();

            db.Notifications.Add(new Notification()
            {
                AssociationID = apcAdminDemand.Demande.AssociationID,
                subject = NotificationSubject.Canceling,
                content = "T'a demande a été annulé et archivé à cause de la raison suivante : " + collection["remarque"],
                date = DateTime.Now.Date

            });

            db.Demandes.Find(id).result = DemandeState.canceled;
            apcAdminDemand.state = DemandeState.archived;

            db.SaveChanges();

            return RedirectToRoute(new
            {
                controller = "Apc",
                action = "DacNotTreated"
            });
        }

        public ActionResult DACNotify(int id)
        {
            var db = new ApplicationDbContext();
            AdministrationDemande apcAdminDemand = db.AdministrationDemande.Where(c => (c.DemandeID == id) && (c.state == DemandeState.treated)).First();
            var DWAdminDemandes = db.AdministrationDemande.Where(c => (c.DemandeID == id) && (c.state == DemandeState.validated));
            
            if(DWAdminDemandes.Count() == 2)
            {
                foreach (var item in DWAdminDemandes) item.state = DemandeState.archived;
                db.Notifications.Add(new Notification()
                {
                    AssociationID = apcAdminDemand.Demande.AssociationID,
                    subject = NotificationSubject.Accepting,
                    content = "Votre demande a été acceptée ",
                    date = DateTime.Now.Date

                });

                apcAdminDemand.Demande.result = DemandeState.accepted;
                apcAdminDemand.state = DemandeState.archived;

            }

            db.SaveChanges();
            return RedirectToRoute(new
            {
                controller = "Apc",
                action = "DacTreated"
            });
        }

        // DAIRA
        public ActionResult ValidateDacForDaira(int id)
        {
            var db = new ApplicationDbContext();
            AdministrationDemande dairaAdminDemand = db.AdministrationDemande.Where(c => (c.DemandeID == id) && (c.state == DemandeState.notValidated)).First();
            int wilayaID = db.Dairas.Find(dairaAdminDemand.Administration.adminID).WilayaID;

            db.AdministrationDemande.Add(new AdministrationDemande()
            {
                AdministrationID = (new ApplicationDbContext()).Administrations.Where(c => c.adminID == wilayaID).First().AdministrationID,
                DemandeID = id,
                state = DemandeState.notValidated
            });

            dairaAdminDemand.state = DemandeState.validated;
            db.SaveChanges();
            return RedirectToRoute(new
            {
                controller = "Daira",
                action = "DacNotValidated"
            }); ;
        }

        public ActionResult ValidateDawForDaira(int id)
        {
            var db = new ApplicationDbContext();
            var userID = User.Identity.GetUserId();
            Administration admin = db.Administrations.Where(c => c.userID == userID ).First();
            AdministrationDemande dairaAdminDemand = db.AdministrationDemande.Where(c => (c.AdministrationID == admin.AdministrationID ) && (c.DemandeID == id) && (c.state == DemandeState.notTreated)).First();

            var apcs = db.Dairas.Find(dairaAdminDemand.Administration.adminID).Apcs;

            foreach (var apc in apcs)
            {
                admin = db.Administrations.Where(c => c.adminID == apc.ApcID).First();
                db.AdministrationDemande.Where(c => (c.DemandeID == id) && (c.AdministrationID == admin.AdministrationID)).First().state = DemandeState.notValidated;
            }

            dairaAdminDemand.state = DemandeState.treated;
            db.SaveChanges();
            return RedirectToRoute(new
            {
                controller = "Daira",
                action = "DawNotValidated"
            }); ;
        }

        [HttpPost]
        public ActionResult CancelDacDaira(FormCollection collection, int id)
        {
            var db = new ApplicationDbContext();
            AdministrationDemande dairaAdminDemand = db.AdministrationDemande.Where(c => (c.DemandeID == id) && (c.state == DemandeState.notValidated)).First();
            AdministrationDemande apcAdminDemand = db.AdministrationDemande.Where(c => (c.DemandeID == id) && (c.state == DemandeState.treated)).First();

            db.Notifications.Add(new Notification()
            {
                AssociationID = dairaAdminDemand.Demande.AssociationID,
                subject = NotificationSubject.Canceling,
                content = "T'a demande a été annulé et archivé à cause de la raison suivante : " + collection["remarque"],
                date = DateTime.Now.Date

            });

            db.Demandes.Find(id).result = DemandeState.canceled;
            dairaAdminDemand.state = DemandeState.archived;
            apcAdminDemand.state = DemandeState.archived;

            db.SaveChanges();

            return RedirectToRoute(new
            {
                controller = "Daira",
                action = "DacNotValidated"
            });
        }
    

        // WILAYA
        public ActionResult ValidateDacForWilaya(int id)
        {
            var db = new ApplicationDbContext();
            AdministrationDemande wilayaAdminDemand = db.AdministrationDemande.Where(c => (c.DemandeID == id) && (c.state == DemandeState.notValidated)).First();
            wilayaAdminDemand.state = DemandeState.validated;
            db.SaveChanges();
            return RedirectToRoute(new
            {
                controller = "Wilaya",
                action = "DacNotValidated"
            }); ;
        }

        public ActionResult ValidateDawForWilaya(int id)
        {
            var db = new ApplicationDbContext();

            AdministrationDemande wilayaAdminDemand = db.AdministrationDemande.Where(c => (c.DemandeID == id) && (c.state == DemandeState.notTreated)).First();

            var dairas = db.Wilayas.Find(wilayaAdminDemand.Administration.adminID).Dairas;

            foreach (var daira in dairas)
            {
                db.AdministrationDemande.Add(new AdministrationDemande()
                {
                    AdministrationID = (new ApplicationDbContext()).Administrations.Where(c => c.adminID == daira.DairaID).First().AdministrationID,
                    DemandeID = id,
                    state = DemandeState.notTreated
                });
                foreach (var apc in daira.Apcs)
                {
                    db.AdministrationDemande.Add(new AdministrationDemande()
                    {
                        AdministrationID = (new ApplicationDbContext()).Administrations.Where(c => c.adminID == apc.ApcID).First().AdministrationID,
                        DemandeID = id,
                        state = DemandeState.notAllowed
                    });
                }
            }

            wilayaAdminDemand.state = DemandeState.treated;
            db.SaveChanges();
            return RedirectToRoute(new
            {
                controller = "Wilaya",
                action = "DawNotTreated"
            }); ;
        }

        [HttpPost]
        public ActionResult CancelDaw(FormCollection collection, int id)
        {
            var db = new ApplicationDbContext();
            AdministrationDemande wilayaAdminDemand = db.AdministrationDemande.Where(c => (c.DemandeID == id) && (c.state == DemandeState.notTreated)).First();

            db.Notifications.Add(new Notification()
            {
                AssociationID = wilayaAdminDemand.Demande.AssociationID,
                subject = NotificationSubject.Canceling,
                content = "T'a demande a été annulé et archivé à cause de la raison suivante : " + collection["remarque"],
                date = DateTime.Now.Date

            });

            db.Demandes.Find(id).result = DemandeState.canceled;
            wilayaAdminDemand.state = DemandeState.archived;

            db.SaveChanges();

            return RedirectToRoute(new
            {
                controller = "Wilaya",
                action = "DawNotTreated"
            });
        }

        public ActionResult DAWNotify(int id)
        {
            var db = new ApplicationDbContext();
            AdministrationDemande wilayaAdminDemand = db.AdministrationDemande.Where(c => (c.DemandeID == id) && (c.state == DemandeState.treated)).First();
            int i = db.AdministrationDemande.Where(c => (c.DemandeID == id) && (c.state != DemandeState.validated) && (c.state != DemandeState.treated)).Count();
           
            if( i == 0)
            {
                var adminDemandes = db.AdministrationDemande.Where(c => c.DemandeID == id);
                foreach (var item in adminDemandes) item.state = DemandeState.archived;

                db.Notifications.Add(new Notification()
                {
                    AssociationID = wilayaAdminDemand.Demande.AssociationID,
                    subject = NotificationSubject.Accepting,
                    content = "Votre demande a été acceptée ",
                    date = DateTime.Now.Date

                });

                wilayaAdminDemand.Demande.result = DemandeState.accepted;

                db.SaveChanges();
            }
            
            return RedirectToRoute(new
            {
                controller = "Wilaya",
                action = "DawTreated"
            });
        }

    }
}