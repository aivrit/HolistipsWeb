using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Holistips.Data;
using Microsoft.EntityFrameworkCore;
using Holistips.Models;
using Microsoft.AspNetCore.Identity;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Holistips.Controllers
{

    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        QoD quote = new Models.QoD();

        // Single HttpClient Instance for the application 
        // (To avoid exhausting number of sockets available)
        static HttpClient client = new HttpClient();

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        

        public ActionResult Index()
        {
            //Calling quote of the day API
            QoD();
            
            List <TipAndPackage> model = new List<TipAndPackage>();

            var query = from tip in _context.Tips
                        join package in _context.TipPackages on tip.TipPackage equals package into gj
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

        private async void QoD()
        {
            try
            {
                // Web API Request
                var response = await client.GetAsync("http://quotes.rest/qod.json");
                var json = await response.Content.ReadAsStringAsync();

                // get JSON result objects into a list
                JObject jsonString = JObject.Parse(json);
                IList<JToken> jsonContent = jsonString["contents"]["quotes"].Children().ToList();

                // serialize JSON results into .NET objects
                this.quote = JsonConvert.DeserializeObject<QoD>(jsonContent[0].ToString());
            }
            catch
            {
                this.quote.quote = "The Cake is a lie...";
                this.quote.author = "GLaDOS";
            }
            finally
            {
                ViewBag.quoteContent = this.quote.quote;
                ViewBag.quoteAuthor = this.quote.author;
            }
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
