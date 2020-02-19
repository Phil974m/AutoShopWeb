using AutoShop_Shared.Models;
using System;
using System.Collections.Generic;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System.IO;

namespace AutoShop_Shared.Services.Repository
{
    public class AzureBlobRepository<T> : IRepository<T>
    {
        public void DeleteItem(string id, string PrtitionKey = "")
        {
            throw new NotImplementedException();
        }

        public T GetItem(string id = "", string partitionKey = "")
        {
            throw new NotImplementedException();
        }

        public List<T> GetItems(AppSettings settings)
        {
            throw new NotImplementedException();
        }

        public List<Article> GetItems(string v)
        {
            throw new NotImplementedException();
        }

        public T InsertItem(T item)
        {
            throw new NotImplementedException();
        }

        public T UpdateItem(T item)
        {
            string cnx = "DefaultEndpointsProtocol=https;AccountName=stockageautoshopsocrate;AccountKey=FkLD+SMX4tMK9MHBM5xldiohQOZq2k5LT0VdEX79YKmpAfBV+QuOl8oq9Yk1cyfuB0ObWsnLpq8I3f5WFH+0hg==;EndpointSuffix=core.windows.net";

            // Create a BlobServiceClient object which will be used to create a container client
            BlobServiceClient blobServiceClient = new BlobServiceClient(cnx);

            //Create a unique name for the container
            string containerName = "profils" + Guid.NewGuid().ToString();

            // Create the container and return a container client object
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);

            //dynamic file = item;
            dynamic file = item;

            FileStream s = (FileStream)file;
            //MyFile s = (MyFile)file;

            BlobClient blobClient = containerClient.GetBlobClient(s.Name);

            blobClient.UploadAsync(file);

            return item;
        }
       
    }
}