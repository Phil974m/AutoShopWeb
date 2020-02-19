
using AutoShop_Shared.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoShop_Web.Controllers
{
    public class SuggestionController : Controller
    {
        public ActionResult Index()
        {
            
       Suggestion s = new Suggestion();
            s.Title = "Suggestion 1";   
            
           Suggestion s2 = new Suggestion();
            s2.Title = "Suggestion 2";

            Suggestion s3 = new Suggestion();
            s3.Title = "Suggestion 3";

            List<Suggestion> Liste = new List<Suggestion>();
            Liste.Add(s);
            Liste.Add(s2);
            Liste.Add(s3);

            return View(Liste);
        }
    }
}
