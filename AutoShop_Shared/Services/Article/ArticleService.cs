using AutoShop_Shared.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoShop_Shared.Services
{
    public class ArticleService
    {
       // public static List<Article> GetArticle()
       // {
        private readonly IRepository<Article> _repository;
        private readonly AppSettings _settings;

        //Constructeur avec l'injection de la dépendance en paramètre
        public ArticleService(IRepository<Article> repo, IOptionsMonitor<AppSettings> s)
        {
            //Ma variable privée = l'instance de l'injection de dépendance
            _repository = repo;
            _settings = s.CurrentValue;

        }


        public List<Article> GetArticle()
        {
            _settings.QuerySelect = "SELECT * FROM c ";
            _settings.QueryWhere = "WHERE c.partitionKey = 'Articles' ";
            List<Article> articles = _repository.
                GetItems(_settings);

            return articles;

        }

        public Article GetArticle(string id)
        {
            Article articles = _repository.GetItem(id, "Articles");
            return articles;
        }

        public Article AddArticle(Article item)
        {
            item.ID = Guid.NewGuid().ToString();
            item.PartitionKey = "Articles";
            return _repository.InsertItem(item);
        }

        public Article UpdateArticle(Article item)
        {
            return _repository.UpdateItem(item);
        }



        public void DeleteArticle(string id)
        {
            _repository.DeleteItem(id, "Articles");
        }

        //IRepository<Article> repository = new JsonFileRepository<Article>();
        //    List<Article> articles = repository.
        //        GetItems(@"C:\Users\campus\source\repos\AutoShop\AutoShop_Web\Files\articles.json");


        //ouvrir lr fichier
        //lire le fichier 
        //string result2 = System.IO.File.ReadAllText(@"C:\Users\campus\source\repos\AutoShop\AutoShop_Web\Files\articles.json");
        //transformer le contenu en objet c#
        // List<Article> articles = JsonConvert.DeserializeObject<List<Article>>(result2);
        //retourner le resultat

      //  return articles;

    }
    }
