using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Jam3iyati.Enums;

namespace Jam3iyati.Models
{
    public class AdministrationDemande
    {
        [Key]
        public int AdministrationDemandeID { get; set; }
        public int AdministrationID { get; set; }
        public int DemandeID { get; set; }
        public DemandeState state { get; set; }

        public virtual Administration Administration { get; set; }
        public virtual Demande Demande { get; set; }
    }
}