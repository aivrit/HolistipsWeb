using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// This ViewModel was created for the index view of the Home controller

namespace Holistips.Models
{
    public class TipAndPackage
    {

        public string TipTitle { get; set; }

        public string TipExplanation { get; set; }

        public string TipWhenTo { get; set;}

        public string TipWhere { get; set; }

        public string TipAnalogy { get; set; }

        public string TipHashtags { get; set; }

        public string TipRefs { get; set; }

        public DateTime TipCreationDate { get; set; }

        public string PackageTitle { get; set; }
    }
}
