using AutoShop_Shared.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Azure.Cosmos;

namespace AutoShop_Shared.Services
{
    public class CosmosDBRepository<T> : IRepository<T>
    {
        private readonly Container _container;
        public CosmosDBRepository(IOptionsMonitor<AppSettings> settings)
        {
            AppSettings appSettings = settings.CurrentValue;
            CosmosClient client = new
                    CosmosClient(appSettings.CosmosDBUrl, appSettings.CosmosDBKey);

            Database database = client.GetDatabase(appSettings.CosmosDatabase);
            _container = database.GetContainer(appSettings.CosmosContainer);

        }


        public List<T> GetItems(AppSettings settings)
        {
            string sqlQueryText = settings.QuerySelect + " " + settings.QueryWhere;
            QueryDefinition queryDefinition = new QueryDefinition(sqlQueryText);

            FeedIterator<T> queryResultSetIterator =
                _container.GetItemQueryIterator<T>(queryDefinition);

            List<T> results = new List<T>();
            try
            {
                while (queryResultSetIterator.HasMoreResults)
                {
                    FeedResponse<T> currentResultSet =
                        queryResultSetIterator.ReadNextAsync().GetAwaiter().GetResult();

                    results.AddRange(currentResultSet);
                    //foreach (T item in currentResultSet)
                    //{
                    //    results.Add(item);
                    //}
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("",e);
            }
            return results;
        }

        public T GetItem(string id = "", string partitionKey = "")
        {
            ItemResponse<T> response =
                _container.ReadItemAsync<T>(id, new PartitionKey(partitionKey)).GetAwaiter().GetResult();
            return response.Resource;
        }
       

        public T InsertItem(T item)
        {
            ItemResponse<T> response =
                _container.CreateItemAsync<T>(item).GetAwaiter().GetResult();
            return response.Resource;
        }

        public T UpdateItem(T item)
        {
            ItemResponse<T> response =
                _container.UpsertItemAsync<T>(item).GetAwaiter().GetResult();
            return response.Resource;
        }

        public void DeleteItem(string id, string partitionKey = "")
        {
           if(! string.IsNullOrEmpty(partitionKey))
            {
                _container.DeleteItemAsync<T>(id, new PartitionKey(partitionKey))
               .GetAwaiter().GetResult();
            }
                
            
        }

        public List<Article> GetItems(string v)
        {
            throw new NotImplementedException();
        }
    }
}
