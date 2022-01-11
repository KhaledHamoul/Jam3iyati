using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Jam3iyati.Models
{
    public class Daira : IAdmin
    {
        [Key]
        public int DairaID { get; set; }
        public int dairaCode { get; set; }
        public int WilayaID { get; set; }

        public virtual Wilaya Wilaya { get; set; }
        public virtual ICollection<Apc> Apcs { get; set; }
    }
}