using AutoShop_Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoShop_Shared.Services
{
    public interface IArticleService
    {
        public List<Article> GetArticle();
        public Article GetArticle(string id = "", string partitionkey = "");

        public Article AddArticle(Article item);

        public Article UpdateArticle(Article item);

        public void DeleteArticle(string id, string PartitionKey = "");
    }
}
