using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Jam3iyati.Enums;

namespace Jam3iyati.Models
{
    public class Demande
    {
        [Key]
        public int DemandeID { get; set; }
        public DateTime date { get; set; }
        public DemandeType type { get; set; }
        public DemandeState result { get; set; }
        public int AssociationID { get; set; }

        public virtual Association Association { get; set; }
        public virtual ICollection<AdministrationDemande> AdministrationDemande { get; set; }

    }
}