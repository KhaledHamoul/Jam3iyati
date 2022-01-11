using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jam3iyati.Models
{
    public class AssociationViewModel
    {
        public Association Association { get; set; }
        public ICollection<Demande> Demandes { get; set; }
        public ICollection<Administration> Wilayas { get; set; }
        public ICollection<Notification> Notifications { get; set; }
        public int AdminID { get; set; }
    }
}