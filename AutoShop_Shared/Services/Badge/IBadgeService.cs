using AutoShop_Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoShop_Shared.Services
{
    public interface IBadgeService
    {
        public List<Badge> GetBadges();

        public Badge GetBadge(string id);
        public Badge AddBadge(Badge item);

        public Badge UpdateBadge(Badge item);

        public void DeleteBadge(string id);

    }
}
