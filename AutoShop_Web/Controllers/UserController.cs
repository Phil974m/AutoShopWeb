using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoShop_Shared.Controllers;
using AutoShop_Shared.Models;
using AutoShop_Shared.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Auth;
using Microsoft.Azure.Storage.Blob;

namespace AutoShop_Web.Controllers
{
    public class UserController : Controller
    //variable privée de type interface
    {
        private readonly IUserService _userService;
        private readonly  UserManager<IdentityUser> _userManager;

        //constructeur 
        public UserController(IUserService u,
            UserManager<IdentityUser> userManager)
        {
          _userService = u;
          _userManager = userManager;
        }

    
        public IActionResult Index()
        {
            List<User> users = new List<User>();
                
               users.Add(_userService.GetUserByEmail(_userManager.GetUserName(User)));
            return View(users);
        }
        //UserService svc = new UserService();
        //users = svc.GetUsers();
        //users = UserService.GetUsers();

        //User u = new User
        //{
        //    Email = "fred@aisense.fr",
        //    LastName = "wickert",
        //    FirstName = "fred",
        //    Photo = "singe2.jpg",
        //    Level=60,
        //    Experience =55000
        //};

        //users.Add(u);

        //u = new User
        //{
        //    Email = "itab@aisense.fr",
        //    LastName = "you",
        //    FirstName = "itab",
        //    Photo = "singe1.jpg",
        //    Level = 30,
        //    Experience = 10000
        //};
        //users.Add(u);
        //u = new User
        //{
        //    Email = "line@aisense.fr",
        //    LastName = "you",
        //    FirstName = "line",
        //    Photo = "mer.jpg",
        //    Level = 40,
        //    Experience = 37000
        //};
        //users.Add(u);




        public ActionResult Detail(string id)
        {
            User user = _userService.GetUser(id, id);//puisque sa clé de partition=id normalement (id, partition key)
            return View("Detail", user);
        }
        [HttpGet]
        public ActionResult Add()
        {
            return View("Add");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(User item)
        {
            if (ModelState.IsValid)
            {
                _userService.AddUser(item);

                return View("Detail", item);
            }
            return View("Add", item);
        }

        // GET: test/Edit/5
        public ActionResult Edit(string id)
        {
            User user = _userService.GetUser(id, id);
            return View("Edit", user);
        }
       
        // POST: test/Edit/5
        [HttpPost]//il appelle cette methodes si tous les champs sdu formulaire sont ajoutés correctement
        [ValidateAntiForgeryToken]//jouer la securité
        public ActionResult Edit(string id, User item)
        {
            if(ModelState.IsValid)
            { 
             _userService.UpdateUser(item);
            return View("Detail", item);
            }
            return View("Edit", item);
        }
        [HttpGet]
        public ActionResult Delete()
        {
            return View("Delete");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id)
        {
            if (ModelState.IsValid)
            {
                _userService.DeleteUser(id, id);

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");// A remplacer par une action avec page d'erreur
        }

        [HttpGet("/Upload/{id}")]
        public ActionResult Upload(string id)
        {
            return View("Upload", id);
        }
         /* 
        [HttpPost("/Upload/{id}")]
     
        public IActionResult UploadPostAsync(string id,IFormFile file)
        {
            //code pour upload la photo=> paramèrtre file
            //Enregistrer le fichier dans le CLOUD =>
            //AzureBlobRepository<MyFile> azureBlobRepository = new AzureBlobRepository<MyFile>();
            MyFile myFile = new MyFile();   
            myFile.Name = file.Name;
            myFile.File = file.FileName;
           // MyFile.File 

            //code pour mettre à jour l'utilisateur =< mettre à jour le champ photo
            return RedirectToAction("Index");// A remplacer par une action avec page d'erreur
        }
        */
        [HttpPost("/Upload/{id}")]

        public static async Task<bool> UploadFileToStorage(Stream fileStream, string fileName, AzureStorageConfig _storageConfig)
        {
            // Create storagecredentials object by reading the values from the configuration (appsettings.json)
            StorageCredentials storageCredentials = new StorageCredentials(_storageConfig.AccountName, _storageConfig.AccountKey);

            // Create cloudstorage account by passing the storagecredentials
            CloudStorageAccount storageAccount = new CloudStorageAccount(storageCredentials, true);

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Get reference to the blob container by passing the name by reading the value from the configuration (appsettings.json)
            CloudBlobContainer container = blobClient.GetContainerReference(_storageConfig.ImageContainer);

            // Get the reference to the block blob from the container
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(fileName);

            // Upload the file
            await blockBlob.UploadFromStreamAsync(fileStream);

            return await Task.FromResult(true);
        }
        /*
        public IActionResult Upload(string id, IFormFile file)
        {
            // Extract file name from whatever was posted by browser
           
            MyFile myFile = new MyFile();
            myFile.Name = file.Name;
            myFile.File = file.FileName;

            var fileName = Path.GetFileName(myFile.File);

            // If file with same name exists delete it
            if (System.IO.File.Exists(fileName))
            {
                System.IO.File.Delete(fileName);
            }

            // Create new local file and copy contents of uploaded file
            using (var localFile = System.IO.File.OpenWrite(fileName))
                NewMethod(file, localFile);

            ViewBag.Message = "File successfully uploaded";

           // return View();
            return RedirectToAction("Index");
        }

        private static void NewMethod(IFormFile file, FileStream localFile)
        {
            using (var uploadedFile = file.OpenReadStream())
            {
                uploadedFile.CopyTo(localFile);
            }
        }*/
    }

}