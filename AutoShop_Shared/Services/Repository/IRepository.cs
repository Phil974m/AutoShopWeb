using AutoShop_Shared.Models;
using System.Collections.Generic;

namespace AutoShop_Shared.Services
{
    //CRUD :  Create, Read, Update, Delete
    //Q : Query
    public interface IRepository<T>
    {
        public List<T> GetItems(AppSettings settings);

        public T GetItem(string id="", string partitionKey="");

        public T InsertItem(T item);

        public T UpdateItem(T item);

        public void DeleteItem(string id, string PartitionKey ="");
        List<Article> GetItems(string id);
    }
}
