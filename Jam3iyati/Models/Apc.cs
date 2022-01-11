using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jam3iyati.Models
{
    public class Apc : IAdmin
    {
        [Key]
        public int ApcID { get; set; }
        public int apcCode { get; set; }
        public int DairaID { get; set; }

        public virtual Daira Daira { get; set; }

    }
}