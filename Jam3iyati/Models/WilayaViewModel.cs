using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jam3iyati.Models
{
    public class WilayaViewModel
    {
        public Wilaya wilaya { get; set; }
        public Administration admin { get; set; }
        public List<AdministrationDemande> notTreatedWilayaDemandes { get; set; }
        public List<AdministrationDemande> treatedWilayaDemandes { get; set; }
        public List<AdministrationDemande> notValidatedApcDemandes { get; set; }
        public List<AdministrationDemande> validatedApcDemandes { get; set; }
        public List<AdministrationDemande> archievedDemandes { get; set; }
    }
}