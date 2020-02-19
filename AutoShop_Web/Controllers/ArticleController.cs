using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoShop_Shared.Models;
using AutoShop_Shared.Services;
using Microsoft.AspNetCore.Mvc;

namespace AutoShop_Web.Controllers
{
    public class ArticleController : Controller
    {
     
        public IActionResult Index()
        {
            List<Article> articles = new List<Article>();
            articles = ArticleService.GetArticles();
            return View(articles);
        }
    }
}