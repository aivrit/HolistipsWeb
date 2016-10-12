using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Holistips.Models
{
    public class TipPackage
    {
        public int ID { get; set;}

        public string PacakgeTitle { get; set; }

        public string PackageDescription { get; set; }

        public List<Tip> Tips { get; set; }

        public TipPackage()
        {
            Tips = new List<Tip>();
        }
    }
}
