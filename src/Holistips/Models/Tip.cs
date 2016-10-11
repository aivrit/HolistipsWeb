using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Holistips.Models
{
    public class Tip
    {
        public int ID { get; set; }

        public string TipTitle { get; set; }

        public string TipExplanation { get; set; }

        public string TipWhenTo { get; set; }

        public string TipWhere { get; set; }

        public string TipAnalogy { get; set; }

        public string TipRefs { get; set; }

        public string TipHashtags { get; set; }
    }
}
