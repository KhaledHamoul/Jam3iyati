using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Jam3iyati.Enums;

namespace Jam3iyati.Models
{
    public class Notification
    {
        [Key]
        public int notificationID { get; set; }
        public NotificationSubject subject { get; set; }
        public string content { get; set; }
        public bool read { get; set; }
        public DateTime date { get; set; }

        public int AssociationID { get; set; }

        public virtual Association Association { get; set; }
    }
}