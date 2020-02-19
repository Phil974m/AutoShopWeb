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
        private readonly IArticleService _articleService;

        //constructeur 
        public ArticleController(IArticleService b)
        {
            _articleService = b;
        }

        // GET: Badge
        public ActionResult Index()
        {
            List<Article> article = _articleService.GetArticle();
            return View(article);
        }

        // GET: Article/Details/5
        public ActionResult Detail(string id)
        {
            Article article = _articleService.GetArticle(id);//puisque sa clé de partition=id normalement (id, partition key)
            return View("Detail", article);
        }

        // GET: Badge/Add
        public ActionResult Add()
        {
            return View("Add");
        }

        // POST: Article/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Article item)
        {
            if (ModelState.IsValid)
            {
                _articleService.AddArticle(item);

                return View("Detail", item);
            }
            return View("Add", item);
        }

        // GET: test/Edit/5
        public ActionResult Edit(string id)
        {
            Article article = _articleService.GetArticle(id);
            return View("Edit", article);
        }

        // POST: test/Edit/5
        [HttpPost]//il appelle cette methodes si tous les champs sdu formulaire sont ajoutés correctement
        [ValidateAntiForgeryToken]//jouer la securité
        public ActionResult Edit(string id, Article item)
        {
            if (ModelState.IsValid)
            {
                _articleService.UpdateArticle(item);
                return View("Detail", item);
            }
            return View("Edit", item);
        }

        [HttpGet]
        public ActionResult Delete()
        {
            return View("Delete");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id)
        {
            if (ModelState.IsValid)
            {
                _articleService.DeleteArticle(id);

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");// A remplacer par une action avec page d'erreur
        }
    }
    /*
       public IActionResult Index()
       {
           List<Article> articles = new List<Article>();
           articles = ArticleService.GetArticle();
           return View(articles);
       }
       */
}
