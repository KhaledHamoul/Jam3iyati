using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Jam3iyati.Enums;


namespace Jam3iyati.Models
{
    public class Administration
    {
        [Key]
        public int AdministrationID { get; set; }
        public AdministrationType type { get; set; }
        public string name { get; set; }
        public string userID { get; set; }
        public int adminID { get; set; }      // id de la wilaya , daira ou l'APC

        public virtual ICollection<AdministrationDemande> AdministrationDemande { get; set; }
        public virtual ICollection<Association> Associations { get; set; }

    }
}