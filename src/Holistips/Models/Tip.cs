using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Holistips.Models
{
    public class Tip
    {
        public int ID { get; set; }

        public int? TipPackageID { get; set; }

        [ForeignKey("TipPackageID")]
        public TipPackage TipPackage { get; set; }

        public string TipTitle { get; set; }

        public string TipExplanation { get; set; }

        public string TipWhenTo { get; set; }

        public string TipWhere { get; set; }

        public string TipAnalogy { get; set; }

        public string TipRefs { get; set; }

        public string TipHashtags { get; set; }

        public DateTime TipCreationDate { get; set; }

        public int TipAuthorID { get; set; }

    }
}
