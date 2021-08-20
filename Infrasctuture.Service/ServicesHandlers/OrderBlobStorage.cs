using Azure.Storage.Blobs;
using Infrasctuture.Service.Contracts;
using Infrasctuture.Service.Interfaces;
using Infrasctuture.Service.Interfaces.settings;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Infrasctuture.Service.ServicesHandlers
{
    public class OrderBlobStorage : IBlobStorage
    {
        public Interfaces.settings.IBlobSettings _blobSettings;
        public OrderBlobStorage(Interfaces.settings.IBlobSettings blobSettings)
        {
            _blobSettings = blobSettings;
        }
        public async Task<string> UploadFile(IFormFile file)
        {
            // Create a BlobServiceClient object which will be used to create a container client
            BlobServiceClient blobServiceClient = new BlobServiceClient(_blobSettings.ConnectionString);

            //Create a unique name for the container
            string containerName = Guid.NewGuid().ToString() + ".csv";
            containerName = containerName.Replace(" ", "").Replace(".", "").ToLower();

            // Create the container and return a container client object
            BlobContainerClient containerClient = await blobServiceClient.CreateBlobContainerAsync(containerName);
            BlobClient blobClient = containerClient.GetBlobClient(containerName);

            await blobClient.UploadAsync(file.OpenReadStream());

            return await Task.FromResult(containerName);
        }
    }
}
