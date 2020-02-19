using AutoShop_Shared.Models;

using System.Collections.Generic;


namespace AutoShop_Shared.Services
{
   public interface IUserService
    {
        public List<User> GetUsers();
        public User GetUser(string id="", string partitionkey="");

        public User AddUser(User item);

        public User UpdateUser(User item);

        public void DeleteUser(string id, string PartitionKey = "");

        public User GetUserByEmail(string email);

    
    }
}
