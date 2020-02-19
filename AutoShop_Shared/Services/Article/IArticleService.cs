using AutoShop_Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoShop_Shared.Services
{
    interface IArticleService
    {
        public List<Article> GetUsers();
        public Article GetUser(string id = "", string partitionkey = "");

        public Article AddUser(Article item);

        public Article UpdateUser(Article item);

        public void DeleteArticle(string id, string PartitionKey = "");
    }
}
