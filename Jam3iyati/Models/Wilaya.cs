using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Jam3iyati.Models
{
    public class Wilaya : IAdmin
    {
        [Key]
        public int WilayaID { get; set; }
        public int code { get; set; }

        public virtual ICollection<Daira> Dairas { get; set; }
    }
}