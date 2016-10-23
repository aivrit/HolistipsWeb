using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Holistips.Data;
using Microsoft.EntityFrameworkCore;
using Holistips.Models;

namespace Holistips.Controllers
{

    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            List<TipAndPackage> model = new List<TipAndPackage>();

            var query = from tip in _context.Tips
                        join package in _context.TipPackages on tip.TipPackageID equals package.ID into gj
                        from parentpackage in gj.DefaultIfEmpty()
                        select new
                        {
                            tip.TipAuthorID,
                            tip.TipTitle,
                            tip.TipExplanation,
                            tip.TipWhenTo,
                            tip.TipWhere,
                            tip.TipAnalogy,
                            tip.TipHashtags,
                            tip.TipRefs,
                            tip.TipCreationDate,
                            PacakgeTitle = (parentpackage == null ? String.Empty : parentpackage.PacakgeTitle)
                        };

            foreach (var item in query) //retrieve each item and assign to model
            {
                model.Add(new TipAndPackage()
                {
                    PackageTitle = item.PacakgeTitle,
                    TipAnalogy = item.TipAnalogy,
                    TipCreationDate = item.TipCreationDate,
                    TipExplanation = item.TipExplanation,
                    TipHashtags = item.TipHashtags,
                    TipRefs = item.TipRefs,
                    TipTitle = item.TipTitle,
                    TipWhenTo = item.TipWhenTo,
                    TipWhere = item.TipWhere
                });
            }

            

                return View(model);
        }
        

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
