using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Jam3iyati.Models;

namespace Jam3iyati.Models
{
    public class ApcViewModel
    {
        public Apc apc { get; set; }
        public Administration admin { get; set; }
        public List<AdministrationDemande> notTreatedApcDemandes { get; set; }
        public List<AdministrationDemande> treatedApcDemandes { get; set; }
        public List<AdministrationDemande> notValidatedWilayaDemandes { get; set; }
        public List<AdministrationDemande> validatedWilayaDemandes { get; set; }
        public List<AdministrationDemande> archievedDemandes { get; set; }
    }
}