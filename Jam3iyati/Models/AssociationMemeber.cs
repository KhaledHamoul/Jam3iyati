using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Jam3iyati.Models
{
    public class AssociationMemeber
    {
        [Key]
        public int AssociationMemeberID { get; set; }
        public int MemeberID { get; set; }
        public int AssociationID { get; set; }

        public virtual Association Association { get; set; }
        public virtual Memeber Memebr { get; set; }
    }
}