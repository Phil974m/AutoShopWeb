using AutoShop_Shared.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoShop_Shared.Services
{
    public class ArticleService
    {
        public static List<Article> GetArticles()
        {
            IRepository<Article> repository = new JsonFileRepository<Article>();
            List<Article> articles = repository.
                GetItems(@"C:\Users\campus\source\repos\AutoShop\AutoShop_Web\Files\articles.json");
            
            
            //ouvrir lr fichier
            //lire le fichier 
            //string result2 = System.IO.File.ReadAllText(@"C:\Users\campus\source\repos\AutoShop\AutoShop_Web\Files\articles.json");
            //transformer le contenu en objet c#
            // List<Article> articles = JsonConvert.DeserializeObject<List<Article>>(result2);
            //retourner le resultat

            return articles;

        }
    }
}