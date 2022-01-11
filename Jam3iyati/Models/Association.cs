using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Jam3iyati.Enums;

namespace Jam3iyati.Models
{
    public class Association
    {
        [Key]
        public int AssociationID { get; set; }
        public string name { get; set; }
        public int field { get; set; }
        public string subject { get; set; }
        public DateTime date { get; set; }
        public string userId { get; set; }

        public int AdministrationID { get; set; }

        public virtual Administration Administration { get; set; }
        public virtual ICollection<AssociationMemeber> AssociationMemebers { get; set; }
        public virtual ICollection<Demande> Demandes { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }

    }
}