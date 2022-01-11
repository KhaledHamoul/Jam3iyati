using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jam3iyati.Models
{
    public class DairaViewModel
    {
        public Daira daira { get; set; }
        public Administration admin { get; set; }
        public List<AdministrationDemande> notValidatedWilayaDemandes { get; set; }
        public List<AdministrationDemande> validatedWilayaDemandes { get; set; }
        public List<AdministrationDemande> notValidatedApcDemandes { get; set; }
        public List<AdministrationDemande> validatedApcDemandes { get; set; }
        public List<AdministrationDemande> archievedDemandes { get; set; }
    }
}