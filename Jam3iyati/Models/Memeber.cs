using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Jam3iyati.Models
{
    public class Memeber
    {
        [Key]
        public int MemeberID { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public DateTime birthday { get; set; }
        public string birthplace { get; set; }
        public int cardId { get; set; }

        public virtual ICollection<AssociationMemeber> AssociationMemebers { get; set; }
    }
}