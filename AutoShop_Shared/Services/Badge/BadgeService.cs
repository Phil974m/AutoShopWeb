using AutoShop_Shared.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;


namespace AutoShop_Shared.Services
{
    public class BadgeService : IBadgeService
    {
        private readonly IRepository<Badge> _repository;
        private readonly AppSettings _settings;

        //Constructeur avec l'injection de la dépendance en paramètre
        public BadgeService(IRepository<Badge> repo, IOptionsMonitor<AppSettings> s)
        {
            //Ma variable privée = l'instance de l'injection de dépendance
            _repository = repo;
            _settings = s.CurrentValue;

        }


        public List<Badge> GetBadges()
        {
            _settings.QuerySelect = "SELECT * FROM c ";
            _settings.QueryWhere = "WHERE c.partitionKey = 'Badges' ";
            List<Badge> badges = _repository.
                GetItems(_settings);

            return badges;

        }

        public Badge GetBadge(string id)
        {
         Badge badge = _repository.GetItem(id, "Badges");
            return badge;
        }

        public Badge AddBadge(Badge item)
        {
            item.ID = Guid.NewGuid().ToString();
            item.PartitionKey = "Badges";
            return _repository.InsertItem(item);
        }

        public Badge UpdateBadge(Badge item)
        {
            return _repository.UpdateItem(item);
        }

        

        public void DeleteBadge(string id)
        {
            _repository.DeleteItem(id, "Badges");
        }
    }
}
