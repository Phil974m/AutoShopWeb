
using System;
using System.Collections.Generic;
using System.Linq;
using AutoShop_Shared.Models;
using Microsoft.Extensions.Options;

namespace AutoShop_Shared.Services
{
    public class UserService : IUserService
    {
        //Variable privée qui contiendra l'instance de la dépendance
        private readonly IRepository<User> _repository;
        private readonly AppSettings _settings;
        //Constructeur avec l'injection de la dépendance en paramètre
        public UserService(IRepository<User> repo, IOptionsMonitor<AppSettings> s)
        {
            //Ma variable privée = l'instance de l'injection de dépendance
            _repository = repo;
            _settings = s.CurrentValue;
        }

        public List<User> GetUsers()
        {
            _settings.QuerySelect = "SELECT * FROM c ";
            _settings.QueryWhere = "WHERE IS_DEFINED(c.email)";
            List<User> users = _repository.
                GetItems(_settings);
            return users;
        }

        /// <summary>
        /// Cette méthode permet de récuperer un utilisateur par son ID
        /// </summary>
        /// <param name="id">Représente l'ID de l'utilisateur sous forme d'un GUID</param>
        /// <returns>Retourne un objet User ou Null si aucun user</returns>
        public User GetUser(string id, string partitionKey)
        {
            User user = _repository.GetItem(id, partitionKey);
            return user;
        }

        public User AddUser(User item)
        {
            item.ID = Guid.NewGuid().ToString();
            item.PartitionKey = item.ID;
            return _repository.InsertItem(item);
        }

        public User UpdateUser(User item)
        {
            return _repository.UpdateItem(item);
        }

        public void DeleteUser(string id, string prtitionKey = "")
        {
            _repository.DeleteItem(id, prtitionKey);
        }

        public User GetUserByEmail(string email)
        {
            _settings.QuerySelect = "SELECT * FROM c ";
            _settings.QueryWhere = $"WHERE c.email = '{email}'";
            User u = null;
            try 
            { 
                 u = _repository.GetItems(_settings).First();
            }
            catch(Exception e) 
            {
                Console.WriteLine("", e);
            }
            return u;
        }


            }
}
