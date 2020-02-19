using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using AutoShop_Shared.reco;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AutoShop_Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();

            //1. Il faut utiliser la classe HttpClient pour pouvoir faire des appels HTTP
            using (HttpClient client = new HttpClient())
            {
                //Pour faire la detection d'objets il faut appeler l'url de pr�diction avec plusieurs param�tres
                //1. La cl� de pr�diction
                string predictionKey = "73b79a2b09b544a1a06ea206bf5529c8";
                client.DefaultRequestHeaders.Add("Prediction-Key", predictionKey);

                //2. Modifier le header Http de l'appel pour que le retour soit application/json    

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //3. Modifier le body Http de l'appel pour qu'il est cette forme : {"Url": "https://example.com/image.png"}                
                StringContent sc = new StringContent(@"{""Url"": ""https://www.carrefour.fr/media/1500x1500/Photosite/PGC/P.L.S./3292070005154_PHOTOSITE_20191127_130027_0.jpg?placeholder=1""}", Encoding.UTF8, "application/json"); //Une classe pour pr�parer le body du POST


                //L'url de la pr�diction
                string url = "https://aienvironnement.cognitiveservices.azure.com/customvision/v3.0/Prediction/da4d7f74-22bc-4bbe-b0ea-9af1e9b565ea/classify/iterations/Iteration3/url";

                //4. Appel de la pr�diction
                HttpResponseMessage result = client.PostAsync(url, sc).GetAwaiter().GetResult(); //Le poste retourne un objet de Type HttpResponseMessage

                if (result.StatusCode == System.Net.HttpStatusCode.OK) //Si l'appel a bien retourn� 200
                {
                    //On peut lire le r�sultat  : la pr�dictin restourn du JSON qu'on peut mettre dans un objet 
                    //Pour cela j'ai cr�� la classe Result : Resultat qui contient une liste de pr�dictions
                    //Il faut du coup juste d�s�rialiser le json

                    Result pred = JsonConvert.DeserializeObject<Result>(result.Content.ReadAsStringAsync().GetAwaiter().GetResult());

                    //Comme il peut y avoir plusieurs d�tection d'objets, on va boucler pour r�cup�rer tous les articles qui ont une probabilit� >.8 (80%)
                    foreach (var item in pred.Predictions.Where(q => q.Probability >= 0.8))
                    {
                        //On affiche le tagName correspondant
                        Console.WriteLine($"Merci d'avoir achet� : {item.TagName}");
                    }

                }
            }


            Console.ReadLine();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
           //on commence � Host et puis Host. quelquechose . quelquechose et on fini � );
            Host.CreateDefaultBuilder(args)

                .ConfigureAppConfiguration((hostingContext, config)  =>//methode sans nom
                {
                    if (hostingContext.HostingEnvironment.IsDevelopment())
                    {
                         config.AddJsonFile("appsettings.Development.json",
                                 optional: false, reloadOnChange: true);
                    }
                    else { 
                         config.AddJsonFile("appsettings.json", 
                                optional:false, reloadOnChange:true);
                    }
                })

                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

    }
}
