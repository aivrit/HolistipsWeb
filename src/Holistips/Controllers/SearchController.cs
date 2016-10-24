using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Holistips.Data;
using Holistips.Models;
using Newtonsoft.Json;

namespace Holistips.Controllers
{
    public class SearchController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SearchController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string SearchQuery, string PackagesOnly)
        {
            List<TipAndPackage> model = new List<TipAndPackage>();
            const string HtmlCheckboxON = "on";
            if (SearchQuery != null)
            {
               
                ViewBag.SearchQuery = "true";

                if(PackagesOnly != HtmlCheckboxON)
                {
                    model = NormalSearch(SearchQuery);
                }
                else
                {
                    return RedirectToAction("SearchPackagesOnly",new { SearchQuery = SearchQuery });
                }
            }

            return View(model);
        }

        public List<TipAndPackage> NormalSearch(string SearchQuery)
        {
            List<TipAndPackage> model = new List<TipAndPackage>();

            var linqQuery = from tip in _context.Tips
                            join package in _context.TipPackages on tip.TipPackage equals package into gj
                            from parentpackage in gj.DefaultIfEmpty()
                            where (tip.TipTitle.IndexOf(SearchQuery, StringComparison.CurrentCultureIgnoreCase) >= 0) || (parentpackage != null && parentpackage.PacakgeTitle.IndexOf(SearchQuery, StringComparison.CurrentCultureIgnoreCase) >=0)
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

            foreach (var item in linqQuery) //retrieve each item and assign to model
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

            return model;
        }

        public string NormalSearchJson(string term)
        {
            List<TipAndPackage> model = new List<TipAndPackage>();

            var linqQuery = from tip in _context.Tips
                            join package in _context.TipPackages on tip.TipPackage equals package into gj
                            from parentpackage in gj.DefaultIfEmpty()
                            where (tip.TipTitle.IndexOf(term, StringComparison.CurrentCultureIgnoreCase) >= 0) || (parentpackage != null && parentpackage.PacakgeTitle.IndexOf(term, StringComparison.CurrentCultureIgnoreCase) >= 0)
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

            foreach (var item in linqQuery) //retrieve each item and assign to model
            {
                model.Add(new TipAndPackage()
                {
                    TipTitle = item.TipTitle,
                });
            }

            string Json = JsonConvert.SerializeObject(model);

            return Json;
        }

        [HttpPost]
        [HttpGet]
        public IActionResult SearchPackagesOnly(string SearchQuery)
        {
            List<TipPackage> model = new List<TipPackage>();

            if (SearchQuery != null)
            {
                var query = from package in _context.TipPackages
                            where package.PacakgeTitle.IndexOf(SearchQuery, StringComparison.CurrentCultureIgnoreCase) >= 0
                            select package;

                foreach (var item in query) //retrieve each item and assign to model
                {
                    model.Add(new TipPackage()
                    {
                        ID = item.ID,
                        PacakgeTitle = item.PacakgeTitle,
                        PackageAuthorID = item.PackageAuthorID,
                        PackageDescription = item.PackageDescription,
                        TipPackageCreationDate = item.TipPackageCreationDate,
                        Tips = item.Tips
                    });
                }
            }
            return View(model);
        }


         



    }
}