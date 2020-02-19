using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoShop_Shared.Models;
using AutoShop_Shared.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AutoShop_Web.Controllers
{
    public class BadgeController : Controller
   
        {
        private readonly IBadgeService _badgeService;

        //constructeur 
        public BadgeController(IBadgeService b)
        {
            _badgeService = b;
        }

        // GET: Badge
        public ActionResult Index()
        {
            List<Badge> badges = _badgeService.GetBadges();
            return View(badges);
        }

        // GET: Badge/Details/5
        public ActionResult Detail(string id)
        {
           Badge badge = _badgeService.GetBadge(id);//puisque sa clé de partition=id normalement (id, partition key)
            return View("Detail", badge);
        }

        // GET: Badge/Add
        public ActionResult Add()
        {
            return View("Add");
        }

        // POST: Badge/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Badge item)
        {
            if (ModelState.IsValid)
            {
                _badgeService.AddBadge(item);

                return View("Detail", item);
            }
            return View("Add", item);
        }

        // GET: test/Edit/5
        public ActionResult Edit(string id)
        {
            Badge badge = _badgeService.GetBadge(id);
            return View("Edit", badge);
        }

        // POST: test/Edit/5
        [HttpPost]//il appelle cette methodes si tous les champs sdu formulaire sont ajoutés correctement
        [ValidateAntiForgeryToken]//jouer la securité
        public ActionResult Edit(string id, Badge item)
        {
            if (ModelState.IsValid)
            {
                _badgeService.UpdateBadge(item);
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
                _badgeService.DeleteBadge(id);

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");// A remplacer par une action avec page d'erreur
        }
    }
}